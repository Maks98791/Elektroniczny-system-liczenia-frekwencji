using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Vision;
using System;
using System.Drawing;
using System.Windows.Forms;
using Accord.Math;
using System.ComponentModel;
using USBWebCam.Core;
using System.Threading;

namespace USBWebCam.Forms
{
    /// <summary>
    /// Okno główne programu.
    /// </summary>
    public partial class MainWindow : Form
    {
        /// <summary>
        /// Obiekt klasy sterownika kamery USB.
        /// </summary>
        private CameraDriver cameraDriver;
        private IPCameraDriver ipCameraDriver;
        private int counter;
        private int LR;
        private int RL; 
        private TCPConnection TCPCon;
        private Thread thread;
        enum CamType {
            None,
            USB,
            IP
        }
        CamType mode = CamType.None;
        /// <summary>
        /// Domyślny konstruktor głównego okna aplikacji.
        /// </summary>
        public MainWindow()
        {
            cameraDriver = new CameraDriver(MovementDetected, MovementDetectionFinished);
            ipCameraDriver = new IPCameraDriver(MovementDetected, MovementDetectionFinished);
            TCPCon = new TCPConnection();
            InitializeComponent();
        }
        
        /// <summary>
        /// Funkcja obsługująca zdarzenie wygenerowania nowej klatki obrazu.
        /// </summary>
        /// <param name="sender">Referencja do obiektu wywołującego zdarzenie.</param>
        /// <param name="eventArgs">Dane zdarzenia (wygenerowana bitmapa).</param>
        private void NewCameraFrame(object sender, NewFrameEventArgs eventArgs)
        {
            var frame = (Bitmap) eventArgs.Frame.Clone();
            if(mode == CamType.USB)
                CameraPreview.Image = cameraDriver.PrepareFrame(frame);
            if (mode == CamType.IP)
                CameraPreview.Image = ipCameraDriver.PrepareFrame(frame);
            
        }

        /// <summary>
        /// Funkcja obsługująca zdarzenie wciśnięcia przycisku wyszukiwania urządzeń.
        /// </summary>
        /// <param name="sender">Referencja do obiektu wywołującego zdarzenie.</param>
        /// <param name="e">Dane zdarzenia.</param>
        private void SearchButtonPressed(object sender, EventArgs e)
        {
            DeviceComboBox.Items.Clear();
            foreach (FilterInfo device in cameraDriver.SearchForDevices())
                DeviceComboBox.Items.Add(device.Name);

            //Wybranie pierwszego urządzenia jako aktywnego.
            if (DeviceComboBox.Items.Count > 0) DeviceComboBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Funkcja obsługująca zdarzenie wciśnięcia przycisku połączenia z kamerą.
        /// </summary>
        /// <param name="sender">Referencja do obiektu wywołującego zdarzenie.</param>
        /// <param name="e">Dane zdarzenia.</param>
        private void ConnectButtonPressed(object sender, EventArgs e)
        {
            if (DeviceComboBox.SelectedIndex > -1)
            {
                if (cameraDriver.ConnectToCamera(NewCameraFrame, DeviceComboBox.SelectedIndex))
                {
                    MovementDetectionButton.Enabled = true;
                    IPCamGroupBox.Enabled = false;
                    mode = CamType.USB;
                }
            }
        }

        /// <summary>
        /// Funkcja obsługująca zdarzenie wciśnięcia przycisku zatrzymania przechwytywania obrazu.
        /// </summary>
        /// <param name="sender">Referencja do obiektu wywołującego zdarzenie.</param>
        /// <param name="e">Dane zdarzenia.</param>
        private void StopButtonPressed(object sender, EventArgs e)
        {
            cameraDriver.StopCamera();
            MovementDetectionButton.Enabled = false;
            if (thread != null)
                if(thread.IsAlive)
                    thread.Abort();
            IPCamGroupBox.Enabled = true;
            mode = CamType.None;
        }

        /// <summary>
        /// Funkcja obłsugująca zdarzenie zamknięcia okna.
        /// </summary>
        /// <param name="sender">Referencja do obiektu wywołującego zdarzenie.</param>
        /// <param name="e">Dane zdarzenia.</param>
        private void WindowClosed(object sender, FormClosingEventArgs e)
        {
            cameraDriver.StopCamera();
        }

        /// <summary>
        /// Funkcja obsługująca zdarzenie wciśnięcia przycisku wykrywania ruchu.
        /// </summary>
        /// <param name="sender">Referencja do obiektu wywołującego zdarzenie.</param>
        /// <param name="e">Dane zdarzenia.</param>
        private void StartMovementDetection(object sender, EventArgs e)
        {
            //cameraDriver.SimilarityThreshold = decimal.ToInt32(SimilarityPicker.Value);
            //SimilarityPicker.Enabled = false;

            MovementDetectionButton.Enabled = false;
            MovementDetectionStopButton.Enabled = true;
            ButtonReset.Enabled = true;
            if(mode == CamType.USB)
            {
                cameraDriver.minHeight = Decimal.ToInt32(numericUpDownY.Value);
                cameraDriver.minWidth = Decimal.ToInt32(numericUpDownX.Value);
                cameraDriver.StartWorker();
            }
            if(mode == CamType.IP)
            {
                ipCameraDriver.minHeight = Decimal.ToInt32(numericUpDownY.Value);
                ipCameraDriver.minWidth = Decimal.ToInt32(numericUpDownX.Value);
                ipCameraDriver.StartWorker();
            }
        }

        /// <summary>
        /// Funkcja obsługująca zdarzenie wciśnięcia przycisku zatrzymania wykrywania ruchu.
        /// </summary>
        /// <param name="sender">Referencja do obiektu wywołującego zdarzenie.</param>
        /// <param name="e">Dane zdarzenia.</param>
        private void StopMovementDetection(object sender, EventArgs e)
        {
            if(mode == CamType.USB)
                cameraDriver.CancelWorker();
            if (mode == CamType.IP)
                ipCameraDriver.CancelWorker();
        }

        /// <summary>
        /// Funkcja obsługująca zdarzenie wykrycia ruchu.
        /// </summary>
        /// <param name="sender">Referencja do obiektu wywołującego zdarzenie.</param>
        /// <param name="args">Dane zdarzenia.</param>
        private void MovementDetected(object sender, ProgressChangedEventArgs args)
        {
            if (args.ProgressPercentage == 1)
            {
                MovementLabel.Text = "Wykryto ruch!";
                if (mode == CamType.USB)
                {
                    counter = cameraDriver.count;
                    LR = cameraDriver.LR;
                    RL = cameraDriver.RL; 
                }
                if (mode == CamType.IP)
                {
                    counter = ipCameraDriver.count;
                    LR = ipCameraDriver.LR;
                    RL = ipCameraDriver.RL;
                }
                CounterLabel.Text = counter.ToString();
            }
            else MovementLabel.Text = "Nie wykryto ruchu!";
        }
        /// <summary>
        /// Funkcja obsługująca zdarzenie zakończenia wykrywania ruchu.
        /// </summary>
        /// <param name="sender">Referencja do obiektu wywołującego zdarzenie.</param>
        /// <param name="args">Dane zdarzenia.</param>
        private void MovementDetectionFinished(object sender, RunWorkerCompletedEventArgs args)
        {
            //SimilarityPicker.Enabled = true;
            MovementDetectionButton.Enabled = true;
            MovementDetectionStopButton.Enabled = false;
        }

        private void ButtonReset_Click(object sender, EventArgs e)
        {
            if(mode == CamType.USB)
                cameraDriver.ResetDetector();
            if (mode == CamType.IP)
                ipCameraDriver.ResetDetector();
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            string ip = maskedTextBoxIP.Text;
            string message;
            if (DirectionLR.Checked)
            { message = "in: " + LR.ToString() + " out:" + RL.ToString(); }
            else
            { message = "in: " + RL.ToString() + " out:" + LR.ToString(); }

            int port = Decimal.ToInt32(numericUpDownPort.Value);
            if (TCPCon.MainFunction(ip, port, message) == 1)
            {
                MessageBox.Show("Poprawnie przesłano liczbę osób na podany adres", "Liczba osób przesłana", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Brak sluchacza na tym porcie lub na tym adresie IP", "Brak słuchacza", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSendPerTime_Click(object sender, EventArgs e)
        {
            thread = new Thread(sendMessage);
            thread.Start();
        }

        private void sendMessage()
        {
            do
            {
                string ip = maskedTextBoxIP.Text;
                string message;
                if (DirectionLR.Checked)
                { message = "in: " + LR.ToString() + " out:" + RL.ToString(); }
                else
                { message = "in: " + RL.ToString() + " out:" + LR.ToString(); }
                int port = Decimal.ToInt32(numericUpDownPort.Value);
                if (TCPCon.MainFunction(ip, port, message) == 1)
                {
                    MessageBox.Show("Poprawnie przesłano liczbę osób na podany adres", "Liczba osób przesłana", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Brak sluchacza na tym porcie lub na tym adresie IP", "Brak słuchacza", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Thread.Sleep(600000);
            } while (true);
        }
        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // get new frame
            Bitmap bitmap = eventArgs.Frame;
            CameraPreview.Image = cameraDriver.PrepareFrame(bitmap);
            // process the frame
        }

        private void IPConnectButton_Click(object sender, EventArgs e)
        {
            String URL = URLtextBox.Text;
            if (ipCameraDriver.ConnectToCamera(NewCameraFrame, URL))
            {
                IPDisconnectButton.Enabled = true;
                IPConnectButton.Enabled = false;
                MovementDetectionButton.Enabled = true;
                mode = CamType.IP;
                USBCamGroupBox.Enabled = false;
            }
        }

        private void IPDisconnectButton_Click(object sender, EventArgs e)
        {
            USBCamGroupBox.Enabled = true; 
            ipCameraDriver.StopCamera();
            IPConnectButton.Enabled = true;
            IPDisconnectButton.Enabled = false;
            mode = CamType.None;
        }
    }
}