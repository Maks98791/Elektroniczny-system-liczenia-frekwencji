using Accord.Math;
using Accord.Video.FFMPEG;
using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Vision.Motion;
using AForge.Imaging;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.IO;

namespace USBWebCam.Core
{
    /// <summary>
    /// Klasa sterownika kamery USB.
    /// </summary>
    public class IPCameraDriver 
    {
        private MotionDetector detector = null;
        static private CountingObjectsProcessing process = null;
        /// <summary>
        /// Obiekt klasy kamery USB.
        /// </summary>
        private MJPEGStream stream;
        /// <summary>
        /// Komponent pozwalający na asynchroniczne wykonywanie zadanej metody.
        /// </summary>
        private BackgroundWorker worker;

        /// <summary>
        /// Ostatnia zapamiętana klatka obrazu.
        /// </summary>
        private Bitmap lastFrame;
        private float motionlevel;
        private bool startProcess = false;

        public int count { get; private set; }
        public int LR { get; private set; }
        public int RL { get; private set; }

        public int minHeight = 70;
        public int minWidth = 70; 
        public bool motionLevel { get; private set; }

        /// <summary>
        /// Próg wykrywania ruchu.
        /// </summary>
        public int SimilarityThreshold { get; set; }

        /// <summary>
        /// Konstruktor klasy sterownika kamery USB.
        /// </summary>
        /// <param name="detectionHandler">Funkcja obsługująca zdarzenie wykrycia ruchu.</param>
        /// <param name="detectionFinishedHandler">Funkcja obsługująca zdarzenie zakończenia wykrywania ruchu.</param>
        public IPCameraDriver(ProgressChangedEventHandler detectionHandler, RunWorkerCompletedEventHandler detectionFinishedHandler)
        {
            //Konfiguracja wątku działającego w tle.
            worker = new BackgroundWorker();
            worker.DoWork += MovementDetectionLoop;
            worker.ProgressChanged += detectionHandler;
            worker.RunWorkerCompleted += detectionFinishedHandler;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
        }
        public MotionDetector GetDefaultMotionDetector()
        {
            IMotionDetector motionDetector = null;
            MotionDetector detector = null;
            process = new CountingObjectsProcessing();
            //AForge.Vision.Motion.IMotionDetector detector = null;
            //AForge.Vision.Motion.IMotionProcessing processor = null;
            // AForge.Vision.Motion.MotionDetector motionDetector = null;
            process.MinObjectsHeight = minHeight;
            process.MinObjectsWidth = minWidth;
            //process.blobCounter.ObjectsOrder = ObjectsOrder.None;

            motionDetector = new SimpleBackgroundModelingDetector()
            {
                DifferenceThreshold = 50, //ilosc roznicy pomiedzy pikselami 
                //FramesPerBackgroundUpdate = 10000,
                MillisecondsPerBackgroundUpdate = 60000, //po tylu milisekundach restartowane jest tlo 
                SuppressNoise = true
            };
            
            detector = new MotionDetector(motionDetector, process);

            return (detector);
        }
        /// <summary>
        /// Funkcja przygotowująca nową klatkę do wyświetlenia.
        /// </summary>
        /// <param name="currentFrame">Klatka do przetworzenia.</param>
        /// <returns>Klatka zmodyfikowana o zastosowane efekty.</returns>
        public Bitmap PrepareFrame(Bitmap currentFrame)
        {
            lastFrame = (Bitmap) currentFrame.Clone();
            if (startProcess)
            {
                motionlevel = detector.ProcessFrame(lastFrame);
                count = process.getPersonCount();
                LR = process.getLR();
                RL = process.getRL();
            }
            //lastFrame.RotateFlip(RotateFlipType.RotateNoneFlipX);
            return lastFrame;
        }

        /// <summary>
        /// Funkcja przeprowadzająca procedurę łączenia z kamerą.
        /// </summary>
        /// <param name="frameEventHandler">Funkcja obsługująca zdarzenie wygenerowania nowej klatki obrazu.</param>
        /// <param name="selectedIndex">Indeks wybranego urządzenia.</param>
        /// <returns>true - jeśli połączenie się powiodło, w przeciwnym wypadku false.</returns>
        public bool ConnectToCamera(NewFrameEventHandler frameEventHandler, String URL)
        {
            stream = new MJPEGStream(URL);
            stream.NewFrame += new NewFrameEventHandler(frameEventHandler);
            try
            {stream.Start();}
            catch(System.ArgumentException e)
            { Console.WriteLine(e); }
            return stream.IsRunning;
        }

        /// <summary>
        /// Funkcja zatrzymująca działanie kamery.
        /// </summary>
        public void StopCamera()
        {
            if (stream == null) return;
            else if (stream.IsRunning) stream.SignalToStop();
        }

        /// <summary>
        /// Funkcja uruchamiająca zadanie wykrywania ruchu.
        /// </summary>
        public void StartWorker()
        {
            detector = GetDefaultMotionDetector();
            worker.RunWorkerAsync();
            startProcess = true; 
        }

        /// <summary>
        /// Funkcja zatrzymująca zadanie wykrywania ruchu.
        /// </summary>
        public void CancelWorker()
        {
            worker.CancelAsync();
            startProcess = false; 
        }

        /// <summary>
        /// Funkcja zwracająca status działania kamery.
        /// </summary>
        /// <returns>true - jeśli kamera jest w użyciu, w przeciwnym wypadku false.</returns>
        public bool GetCameraStatus()
        {
            return stream.IsRunning;
        }

        /// <summary>
        /// Funkcja zadania, która jest wykonywana na osobnym wątku.
        /// </summary>
        /// <param name="sender">Referencja do obiektu wywołującego zdarzenie.</param>
        /// <param name="args">Dane zdarzenia (rezultat wykonania zadania).</param>
        private void MovementDetectionLoop(object sender, DoWorkEventArgs args)
        {
            while (!worker.CancellationPending)
            {
                Thread.Sleep(2);
                if (motionlevel > 0.02)
                {
                    worker.ReportProgress(1);
                }
                else
                    worker.ReportProgress(2);
            }
        }
        public void ResetDetector()
        {
            detector.Reset();
            process.Reset();
            startProcess = false;
            count = 0;
            LR = 0;
            RL = 0;
            startProcess = true; 
        }
    }
}