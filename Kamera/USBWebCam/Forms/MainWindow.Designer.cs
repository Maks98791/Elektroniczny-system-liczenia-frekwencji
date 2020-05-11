namespace USBWebCam.Forms
{
    partial class MainWindow
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.CameraPreview = new System.Windows.Forms.PictureBox();
            this.DeviceComboBox = new System.Windows.Forms.ComboBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.MovementDetectionButton = new System.Windows.Forms.Button();
            this.MovementDetectionStopButton = new System.Windows.Forms.Button();
            this.MovementLabel = new System.Windows.Forms.Label();
            this.MovementDetectionGroupBox = new System.Windows.Forms.GroupBox();
            this.DirectionRL = new System.Windows.Forms.RadioButton();
            this.DirectionLR = new System.Windows.Forms.RadioButton();
            this.DirectionLabel = new System.Windows.Forms.Label();
            this.numericUpDownY = new System.Windows.Forms.NumericUpDown();
            this.labelY = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.labelSize = new System.Windows.Forms.Label();
            this.numericUpDownX = new System.Windows.Forms.NumericUpDown();
            this.ButtonReset = new System.Windows.Forms.Button();
            this.CounterLabel = new System.Windows.Forms.Label();
            this.LabelPeople = new System.Windows.Forms.Label();
            this.maskedTextBoxIP = new System.Windows.Forms.MaskedTextBox();
            this.numericUpDownPort = new System.Windows.Forms.NumericUpDown();
            this.IPLabel = new System.Windows.Forms.Label();
            this.PortLabel = new System.Windows.Forms.Label();
            this.TCPGroupBox = new System.Windows.Forms.GroupBox();
            this.buttonSendPerTime = new System.Windows.Forms.Button();
            this.SendButton = new System.Windows.Forms.Button();
            this.IPConnectButton = new System.Windows.Forms.Button();
            this.IPAddressLabel = new System.Windows.Forms.Label();
            this.URLtextBox = new System.Windows.Forms.TextBox();
            this.IPDisconnectButton = new System.Windows.Forms.Button();
            this.IPCamGroupBox = new System.Windows.Forms.GroupBox();
            this.USBCamGroupBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.CameraPreview)).BeginInit();
            this.MovementDetectionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).BeginInit();
            this.TCPGroupBox.SuspendLayout();
            this.IPCamGroupBox.SuspendLayout();
            this.USBCamGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // CameraPreview
            // 
            this.CameraPreview.BackColor = System.Drawing.SystemColors.Control;
            this.CameraPreview.Location = new System.Drawing.Point(12, 153);
            this.CameraPreview.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CameraPreview.Name = "CameraPreview";
            this.CameraPreview.Size = new System.Drawing.Size(444, 300);
            this.CameraPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.CameraPreview.TabIndex = 0;
            this.CameraPreview.TabStop = false;
            // 
            // DeviceComboBox
            // 
            this.DeviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DeviceComboBox.FormattingEnabled = true;
            this.DeviceComboBox.Location = new System.Drawing.Point(26, 22);
            this.DeviceComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DeviceComboBox.Name = "DeviceComboBox";
            this.DeviceComboBox.Size = new System.Drawing.Size(331, 24);
            this.DeviceComboBox.TabIndex = 0;
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(363, 18);
            this.SearchButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(108, 31);
            this.SearchButton.TabIndex = 2;
            this.SearchButton.Text = "Wyszukaj";
            this.SearchButton.UseMnemonic = false;
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButtonPressed);
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(477, 18);
            this.ConnectButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(108, 31);
            this.ConnectButton.TabIndex = 1;
            this.ConnectButton.Text = "Połącz";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButtonPressed);
            // 
            // StopButton
            // 
            this.StopButton.Enabled = false;
            this.StopButton.Location = new System.Drawing.Point(591, 18);
            this.StopButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(108, 31);
            this.StopButton.TabIndex = 3;
            this.StopButton.Text = "Rozłącz";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButtonPressed);
            // 
            // MovementDetectionButton
            // 
            this.MovementDetectionButton.Enabled = false;
            this.MovementDetectionButton.Location = new System.Drawing.Point(5, 20);
            this.MovementDetectionButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MovementDetectionButton.Name = "MovementDetectionButton";
            this.MovementDetectionButton.Size = new System.Drawing.Size(191, 31);
            this.MovementDetectionButton.TabIndex = 13;
            this.MovementDetectionButton.Text = "Wykrywaj ruch";
            this.MovementDetectionButton.UseVisualStyleBackColor = true;
            this.MovementDetectionButton.Click += new System.EventHandler(this.StartMovementDetection);
            // 
            // MovementDetectionStopButton
            // 
            this.MovementDetectionStopButton.Enabled = false;
            this.MovementDetectionStopButton.Location = new System.Drawing.Point(5, 55);
            this.MovementDetectionStopButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MovementDetectionStopButton.Name = "MovementDetectionStopButton";
            this.MovementDetectionStopButton.Size = new System.Drawing.Size(191, 31);
            this.MovementDetectionStopButton.TabIndex = 15;
            this.MovementDetectionStopButton.Text = "Zakończ wykrywanie";
            this.MovementDetectionStopButton.UseVisualStyleBackColor = true;
            this.MovementDetectionStopButton.Click += new System.EventHandler(this.StopMovementDetection);
            // 
            // MovementLabel
            // 
            this.MovementLabel.AutoSize = true;
            this.MovementLabel.Location = new System.Drawing.Point(12, 140);
            this.MovementLabel.Name = "MovementLabel";
            this.MovementLabel.Size = new System.Drawing.Size(123, 17);
            this.MovementLabel.TabIndex = 21;
            this.MovementLabel.Text = "Nie wykryto ruchu!";
            // 
            // MovementDetectionGroupBox
            // 
            this.MovementDetectionGroupBox.Controls.Add(this.DirectionRL);
            this.MovementDetectionGroupBox.Controls.Add(this.DirectionLR);
            this.MovementDetectionGroupBox.Controls.Add(this.DirectionLabel);
            this.MovementDetectionGroupBox.Controls.Add(this.numericUpDownY);
            this.MovementDetectionGroupBox.Controls.Add(this.labelY);
            this.MovementDetectionGroupBox.Controls.Add(this.labelX);
            this.MovementDetectionGroupBox.Controls.Add(this.labelSize);
            this.MovementDetectionGroupBox.Controls.Add(this.numericUpDownX);
            this.MovementDetectionGroupBox.Controls.Add(this.ButtonReset);
            this.MovementDetectionGroupBox.Controls.Add(this.CounterLabel);
            this.MovementDetectionGroupBox.Controls.Add(this.LabelPeople);
            this.MovementDetectionGroupBox.Controls.Add(this.MovementDetectionButton);
            this.MovementDetectionGroupBox.Controls.Add(this.MovementLabel);
            this.MovementDetectionGroupBox.Controls.Add(this.MovementDetectionStopButton);
            this.MovementDetectionGroupBox.Location = new System.Drawing.Point(464, 153);
            this.MovementDetectionGroupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MovementDetectionGroupBox.Name = "MovementDetectionGroupBox";
            this.MovementDetectionGroupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MovementDetectionGroupBox.Size = new System.Drawing.Size(368, 198);
            this.MovementDetectionGroupBox.TabIndex = 19;
            this.MovementDetectionGroupBox.TabStop = false;
            this.MovementDetectionGroupBox.Text = "Wykrywanie ruchu";
            // 
            // DirectionRL
            // 
            this.DirectionRL.AutoSize = true;
            this.DirectionRL.Location = new System.Drawing.Point(206, 119);
            this.DirectionRL.Name = "DirectionRL";
            this.DirectionRL.Size = new System.Drawing.Size(122, 21);
            this.DirectionRL.TabIndex = 32;
            this.DirectionRL.Text = "Lewa <- Prawa";
            this.DirectionRL.UseVisualStyleBackColor = true;
            // 
            // DirectionLR
            // 
            this.DirectionLR.AutoSize = true;
            this.DirectionLR.Checked = true;
            this.DirectionLR.Location = new System.Drawing.Point(206, 95);
            this.DirectionLR.Name = "DirectionLR";
            this.DirectionLR.Size = new System.Drawing.Size(122, 21);
            this.DirectionLR.TabIndex = 31;
            this.DirectionLR.TabStop = true;
            this.DirectionLR.Text = "Lewa -> Prawa";
            this.DirectionLR.UseVisualStyleBackColor = true;
            // 
            // DirectionLabel
            // 
            this.DirectionLabel.AutoSize = true;
            this.DirectionLabel.Location = new System.Drawing.Point(203, 69);
            this.DirectionLabel.Name = "DirectionLabel";
            this.DirectionLabel.Size = new System.Drawing.Size(131, 17);
            this.DirectionLabel.TabIndex = 30;
            this.DirectionLabel.Text = "Kierunek WEJŚCIA:";
            // 
            // numericUpDownY
            // 
            this.numericUpDownY.Location = new System.Drawing.Point(312, 35);
            this.numericUpDownY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownY.Name = "numericUpDownY";
            this.numericUpDownY.Size = new System.Drawing.Size(56, 22);
            this.numericUpDownY.TabIndex = 29;
            this.numericUpDownY.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(291, 37);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(21, 17);
            this.labelY.TabIndex = 28;
            this.labelY.Text = "Y:";
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(203, 37);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(21, 17);
            this.labelX.TabIndex = 27;
            this.labelX.Text = "X:";
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(202, 15);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(138, 17);
            this.labelSize.TabIndex = 26;
            this.labelSize.Text = "Min. wymiary obiektu";
            // 
            // numericUpDownX
            // 
            this.numericUpDownX.Location = new System.Drawing.Point(230, 35);
            this.numericUpDownX.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownX.Name = "numericUpDownX";
            this.numericUpDownX.Size = new System.Drawing.Size(56, 22);
            this.numericUpDownX.TabIndex = 25;
            this.numericUpDownX.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // ButtonReset
            // 
            this.ButtonReset.Enabled = false;
            this.ButtonReset.Location = new System.Drawing.Point(6, 90);
            this.ButtonReset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ButtonReset.Name = "ButtonReset";
            this.ButtonReset.Size = new System.Drawing.Size(191, 31);
            this.ButtonReset.TabIndex = 24;
            this.ButtonReset.Text = "Reset detektora";
            this.ButtonReset.UseVisualStyleBackColor = false;
            this.ButtonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // CounterLabel
            // 
            this.CounterLabel.AutoSize = true;
            this.CounterLabel.Location = new System.Drawing.Point(114, 170);
            this.CounterLabel.Name = "CounterLabel";
            this.CounterLabel.Size = new System.Drawing.Size(16, 17);
            this.CounterLabel.TabIndex = 23;
            this.CounterLabel.Text = "0";
            // 
            // LabelPeople
            // 
            this.LabelPeople.AutoSize = true;
            this.LabelPeople.Location = new System.Drawing.Point(12, 170);
            this.LabelPeople.Name = "LabelPeople";
            this.LabelPeople.Size = new System.Drawing.Size(88, 17);
            this.LabelPeople.TabIndex = 22;
            this.LabelPeople.Text = "Liczba osób:";
            // 
            // maskedTextBoxIP
            // 
            this.maskedTextBoxIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.maskedTextBoxIP.Location = new System.Drawing.Point(108, 25);
            this.maskedTextBoxIP.Mask = "000.000.000.000";
            this.maskedTextBoxIP.Name = "maskedTextBoxIP";
            this.maskedTextBoxIP.Size = new System.Drawing.Size(254, 24);
            this.maskedTextBoxIP.TabIndex = 20;
            // 
            // numericUpDownPort
            // 
            this.numericUpDownPort.Location = new System.Drawing.Point(108, 55);
            this.numericUpDownPort.Maximum = new decimal(new int[] {
            85565,
            0,
            0,
            0});
            this.numericUpDownPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPort.Name = "numericUpDownPort";
            this.numericUpDownPort.Size = new System.Drawing.Size(100, 22);
            this.numericUpDownPort.TabIndex = 21;
            this.numericUpDownPort.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // IPLabel
            // 
            this.IPLabel.AutoSize = true;
            this.IPLabel.Location = new System.Drawing.Point(12, 25);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(65, 17);
            this.IPLabel.TabIndex = 22;
            this.IPLabel.Text = "Adres IP:";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(11, 55);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(91, 17);
            this.PortLabel.TabIndex = 23;
            this.PortLabel.Text = "Numer portu:";
            // 
            // TCPGroupBox
            // 
            this.TCPGroupBox.Controls.Add(this.buttonSendPerTime);
            this.TCPGroupBox.Controls.Add(this.SendButton);
            this.TCPGroupBox.Controls.Add(this.PortLabel);
            this.TCPGroupBox.Controls.Add(this.maskedTextBoxIP);
            this.TCPGroupBox.Controls.Add(this.IPLabel);
            this.TCPGroupBox.Controls.Add(this.numericUpDownPort);
            this.TCPGroupBox.Location = new System.Drawing.Point(464, 356);
            this.TCPGroupBox.Name = "TCPGroupBox";
            this.TCPGroupBox.Size = new System.Drawing.Size(368, 123);
            this.TCPGroupBox.TabIndex = 24;
            this.TCPGroupBox.TabStop = false;
            this.TCPGroupBox.Text = "TCP";
            // 
            // buttonSendPerTime
            // 
            this.buttonSendPerTime.Location = new System.Drawing.Point(202, 83);
            this.buttonSendPerTime.Name = "buttonSendPerTime";
            this.buttonSendPerTime.Size = new System.Drawing.Size(160, 31);
            this.buttonSendPerTime.TabIndex = 30;
            this.buttonSendPerTime.Text = "Wysyłaj co 10 min";
            this.buttonSendPerTime.UseVisualStyleBackColor = true;
            this.buttonSendPerTime.Click += new System.EventHandler(this.buttonSendPerTime_Click);
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(14, 83);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(182, 31);
            this.SendButton.TabIndex = 24;
            this.SendButton.Text = "Wysyłaj ";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // IPConnectButton
            // 
            this.IPConnectButton.Location = new System.Drawing.Point(474, 21);
            this.IPConnectButton.Name = "IPConnectButton";
            this.IPConnectButton.Size = new System.Drawing.Size(108, 31);
            this.IPConnectButton.TabIndex = 25;
            this.IPConnectButton.Text = "Połącz ";
            this.IPConnectButton.UseVisualStyleBackColor = true;
            this.IPConnectButton.Click += new System.EventHandler(this.IPConnectButton_Click);
            // 
            // IPAddressLabel
            // 
            this.IPAddressLabel.AutoSize = true;
            this.IPAddressLabel.Location = new System.Drawing.Point(20, 28);
            this.IPAddressLabel.Name = "IPAddressLabel";
            this.IPAddressLabel.Size = new System.Drawing.Size(166, 17);
            this.IPAddressLabel.TabIndex = 26;
            this.IPAddressLabel.Text = "Adres MJPEG kamery IP:";
            // 
            // URLtextBox
            // 
            this.URLtextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.URLtextBox.Location = new System.Drawing.Point(191, 23);
            this.URLtextBox.Name = "URLtextBox";
            this.URLtextBox.Size = new System.Drawing.Size(277, 24);
            this.URLtextBox.TabIndex = 27;
            // 
            // IPDisconnectButton
            // 
            this.IPDisconnectButton.Enabled = false;
            this.IPDisconnectButton.Location = new System.Drawing.Point(588, 21);
            this.IPDisconnectButton.Name = "IPDisconnectButton";
            this.IPDisconnectButton.Size = new System.Drawing.Size(108, 31);
            this.IPDisconnectButton.TabIndex = 28;
            this.IPDisconnectButton.Text = "Rozłącz";
            this.IPDisconnectButton.UseVisualStyleBackColor = true;
            this.IPDisconnectButton.Click += new System.EventHandler(this.IPDisconnectButton_Click);
            // 
            // IPCamGroupBox
            // 
            this.IPCamGroupBox.Controls.Add(this.IPAddressLabel);
            this.IPCamGroupBox.Controls.Add(this.IPDisconnectButton);
            this.IPCamGroupBox.Controls.Add(this.URLtextBox);
            this.IPCamGroupBox.Controls.Add(this.IPConnectButton);
            this.IPCamGroupBox.Location = new System.Drawing.Point(12, 82);
            this.IPCamGroupBox.Name = "IPCamGroupBox";
            this.IPCamGroupBox.Size = new System.Drawing.Size(810, 64);
            this.IPCamGroupBox.TabIndex = 29;
            this.IPCamGroupBox.TabStop = false;
            this.IPCamGroupBox.Text = "Kamera IP";
            // 
            // USBCamGroupBox
            // 
            this.USBCamGroupBox.Controls.Add(this.DeviceComboBox);
            this.USBCamGroupBox.Controls.Add(this.SearchButton);
            this.USBCamGroupBox.Controls.Add(this.ConnectButton);
            this.USBCamGroupBox.Controls.Add(this.StopButton);
            this.USBCamGroupBox.Location = new System.Drawing.Point(9, 12);
            this.USBCamGroupBox.Name = "USBCamGroupBox";
            this.USBCamGroupBox.Size = new System.Drawing.Size(807, 64);
            this.USBCamGroupBox.TabIndex = 30;
            this.USBCamGroupBox.TabStop = false;
            this.USBCamGroupBox.Text = "Kamera USB";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 491);
            this.Controls.Add(this.USBCamGroupBox);
            this.Controls.Add(this.IPCamGroupBox);
            this.Controls.Add(this.TCPGroupBox);
            this.Controls.Add(this.MovementDetectionGroupBox);
            this.Controls.Add(this.CameraPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "USB WebCam";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WindowClosed);
            ((System.ComponentModel.ISupportInitialize)(this.CameraPreview)).EndInit();
            this.MovementDetectionGroupBox.ResumeLayout(false);
            this.MovementDetectionGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).EndInit();
            this.TCPGroupBox.ResumeLayout(false);
            this.TCPGroupBox.PerformLayout();
            this.IPCamGroupBox.ResumeLayout(false);
            this.IPCamGroupBox.PerformLayout();
            this.USBCamGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox CameraPreview;
        private System.Windows.Forms.ComboBox DeviceComboBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button MovementDetectionButton;
        private System.Windows.Forms.Button MovementDetectionStopButton;
        private System.Windows.Forms.Label MovementLabel;
        private System.Windows.Forms.GroupBox MovementDetectionGroupBox;
        private System.Windows.Forms.Label LabelPeople;
        private System.Windows.Forms.Label CounterLabel;
        private System.Windows.Forms.Button ButtonReset;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxIP;
        private System.Windows.Forms.NumericUpDown numericUpDownPort;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.GroupBox TCPGroupBox;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.NumericUpDown numericUpDownY;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.NumericUpDown numericUpDownX;
        private System.Windows.Forms.Button buttonSendPerTime;
        private System.Windows.Forms.Button IPConnectButton;
        private System.Windows.Forms.Label IPAddressLabel;
        private System.Windows.Forms.TextBox URLtextBox;
        private System.Windows.Forms.Button IPDisconnectButton;
        private System.Windows.Forms.GroupBox IPCamGroupBox;
        private System.Windows.Forms.GroupBox USBCamGroupBox;
        private System.Windows.Forms.Label DirectionLabel;
        private System.Windows.Forms.RadioButton DirectionRL;
        private System.Windows.Forms.RadioButton DirectionLR;
    }
}

