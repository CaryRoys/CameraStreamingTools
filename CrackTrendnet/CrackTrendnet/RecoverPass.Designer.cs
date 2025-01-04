namespace CrackTrendnet
{
    partial class RecoverPass
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnBrowseDictFile = new Button();
            txtDictFile = new TextBox();
            lblDictFile = new Label();
            progressBar1 = new ProgressBar();
            lblCameraMac = new Label();
            txtCameraMAC = new TextBox();
            lblCameraIP = new Label();
            txtCameraIP = new TextBox();
            lblGatewayIP = new Label();
            txtGatewayIP = new TextBox();
            lblSubnetMask = new Label();
            txtSubnetMask = new TextBox();
            txtLog = new TextBox();
            lblLog = new Label();
            btnCrack = new Button();
            btnCopyLogToClipboard = new Button();
            btnTest = new Button();
            lblNetAdapter = new Label();
            cmbNetAdapter = new ComboBox();
            btnOpenSocket = new Button();
            SuspendLayout();
            // 
            // btnBrowseDictFile
            // 
            btnBrowseDictFile.Location = new Point(296, 41);
            btnBrowseDictFile.Name = "btnBrowseDictFile";
            btnBrowseDictFile.Size = new Size(142, 23);
            btnBrowseDictFile.TabIndex = 0;
            btnBrowseDictFile.Text = "Browse";
            btnBrowseDictFile.UseVisualStyleBackColor = true;
            btnBrowseDictFile.Click += btnBrowseDictFile_Click;
            // 
            // txtDictFile
            // 
            txtDictFile.Location = new Point(12, 41);
            txtDictFile.Name = "txtDictFile";
            txtDictFile.Size = new Size(268, 23);
            txtDictFile.TabIndex = 1;
            // 
            // lblDictFile
            // 
            lblDictFile.AutoSize = true;
            lblDictFile.Location = new Point(12, 23);
            lblDictFile.Name = "lblDictFile";
            lblDictFile.Size = new Size(85, 15);
            lblDictFile.TabIndex = 2;
            lblDictFile.Text = "Dictionary File:";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 419);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(708, 19);
            progressBar1.TabIndex = 3;
            // 
            // lblCameraMac
            // 
            lblCameraMac.AutoSize = true;
            lblCameraMac.Location = new Point(12, 67);
            lblCameraMac.Name = "lblCameraMac";
            lblCameraMac.Size = new Size(81, 15);
            lblCameraMac.TabIndex = 5;
            lblCameraMac.Text = "Camera MAC:";
            // 
            // txtCameraMAC
            // 
            txtCameraMAC.Location = new Point(12, 85);
            txtCameraMAC.Name = "txtCameraMAC";
            txtCameraMAC.Size = new Size(268, 23);
            txtCameraMAC.TabIndex = 4;
            txtCameraMAC.Text = "3c-8c-f8-a1-92-91";
            // 
            // lblCameraIP
            // 
            lblCameraIP.AutoSize = true;
            lblCameraIP.Location = new Point(12, 111);
            lblCameraIP.Name = "lblCameraIP";
            lblCameraIP.Size = new Size(96, 15);
            lblCameraIP.TabIndex = 7;
            lblCameraIP.Text = "Camera IP to set:";
            // 
            // txtCameraIP
            // 
            txtCameraIP.Location = new Point(12, 129);
            txtCameraIP.Name = "txtCameraIP";
            txtCameraIP.Size = new Size(268, 23);
            txtCameraIP.TabIndex = 6;
            txtCameraIP.Text = "192.168.2.200";
            // 
            // lblGatewayIP
            // 
            lblGatewayIP.AutoSize = true;
            lblGatewayIP.Location = new Point(452, 23);
            lblGatewayIP.Name = "lblGatewayIP";
            lblGatewayIP.Size = new Size(144, 15);
            lblGatewayIP.TabIndex = 9;
            lblGatewayIP.Text = "Camera Gateway IP to set:";
            // 
            // txtGatewayIP
            // 
            txtGatewayIP.Location = new Point(452, 41);
            txtGatewayIP.Name = "txtGatewayIP";
            txtGatewayIP.Size = new Size(268, 23);
            txtGatewayIP.TabIndex = 8;
            txtGatewayIP.Text = "192.168.2.1";
            // 
            // lblSubnetMask
            // 
            lblSubnetMask.AutoSize = true;
            lblSubnetMask.Location = new Point(452, 67);
            lblSubnetMask.Name = "lblSubnetMask";
            lblSubnetMask.Size = new Size(154, 15);
            lblSubnetMask.TabIndex = 11;
            lblSubnetMask.Text = "Camera Subnet Mask to set:";
            // 
            // txtSubnetMask
            // 
            txtSubnetMask.Location = new Point(452, 85);
            txtSubnetMask.Name = "txtSubnetMask";
            txtSubnetMask.Size = new Size(268, 23);
            txtSubnetMask.TabIndex = 10;
            txtSubnetMask.Text = "255.255.255.0";
            // 
            // txtLog
            // 
            txtLog.Location = new Point(12, 190);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.Size = new Size(708, 223);
            txtLog.TabIndex = 12;
            // 
            // lblLog
            // 
            lblLog.AutoSize = true;
            lblLog.Location = new Point(12, 172);
            lblLog.Name = "lblLog";
            lblLog.Size = new Size(30, 15);
            lblLog.TabIndex = 13;
            lblLog.Text = "Log:";
            // 
            // btnCrack
            // 
            btnCrack.Enabled = false;
            btnCrack.Location = new Point(296, 101);
            btnCrack.Name = "btnCrack";
            btnCrack.Size = new Size(142, 25);
            btnCrack.TabIndex = 14;
            btnCrack.Text = "Crack!";
            btnCrack.UseVisualStyleBackColor = true;
            btnCrack.Click += btnCrack_Click;
            // 
            // btnCopyLogToClipboard
            // 
            btnCopyLogToClipboard.Location = new Point(296, 132);
            btnCopyLogToClipboard.Name = "btnCopyLogToClipboard";
            btnCopyLogToClipboard.Size = new Size(142, 25);
            btnCopyLogToClipboard.TabIndex = 15;
            btnCopyLogToClipboard.Text = "Copy Log to Clipboard";
            btnCopyLogToClipboard.UseVisualStyleBackColor = true;
            btnCopyLogToClipboard.Click += btnCopyLogToClipboard_Click;
            // 
            // btnTest
            // 
            btnTest.Enabled = false;
            btnTest.Location = new Point(296, 70);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(142, 23);
            btnTest.TabIndex = 16;
            btnTest.Text = "Test Packet";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += btnTest_Click;
            // 
            // lblNetAdapter
            // 
            lblNetAdapter.AutoSize = true;
            lblNetAdapter.Location = new Point(452, 111);
            lblNetAdapter.Name = "lblNetAdapter";
            lblNetAdapter.Size = new Size(93, 15);
            lblNetAdapter.TabIndex = 17;
            lblNetAdapter.Text = "Network Device:";
            // 
            // cmbNetAdapter
            // 
            cmbNetAdapter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNetAdapter.FormattingEnabled = true;
            cmbNetAdapter.Location = new Point(452, 132);
            cmbNetAdapter.Name = "cmbNetAdapter";
            cmbNetAdapter.Size = new Size(268, 23);
            cmbNetAdapter.TabIndex = 18;
            cmbNetAdapter.SelectedIndexChanged += cmbNetAdapter_SelectedIndexChanged;
            // 
            // btnOpenSocket
            // 
            btnOpenSocket.Location = new Point(452, 159);
            btnOpenSocket.Name = "btnOpenSocket";
            btnOpenSocket.Size = new Size(267, 25);
            btnOpenSocket.TabIndex = 19;
            btnOpenSocket.Text = "Open Socket";
            btnOpenSocket.UseVisualStyleBackColor = true;
            btnOpenSocket.Click += btnOpenSocket_Click;
            // 
            // RecoverPass
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(729, 450);
            Controls.Add(btnOpenSocket);
            Controls.Add(cmbNetAdapter);
            Controls.Add(lblNetAdapter);
            Controls.Add(btnTest);
            Controls.Add(btnCopyLogToClipboard);
            Controls.Add(btnCrack);
            Controls.Add(lblLog);
            Controls.Add(txtLog);
            Controls.Add(lblSubnetMask);
            Controls.Add(txtSubnetMask);
            Controls.Add(lblGatewayIP);
            Controls.Add(txtGatewayIP);
            Controls.Add(lblCameraIP);
            Controls.Add(txtCameraIP);
            Controls.Add(lblCameraMac);
            Controls.Add(txtCameraMAC);
            Controls.Add(progressBar1);
            Controls.Add(lblDictFile);
            Controls.Add(txtDictFile);
            Controls.Add(btnBrowseDictFile);
            Name = "RecoverPass";
            Text = "Recover IPCam Pass";
            Load += RecoverPass_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBrowseDictFile;
        private TextBox txtDictFile;
        private Label lblDictFile;
        private ProgressBar progressBar1;
        private Label lblCameraMac;
        private TextBox txtCameraMAC;
        private Label lblCameraIP;
        private TextBox txtCameraIP;
        private Label lblGatewayIP;
        private TextBox txtGatewayIP;
        private Label lblSubnetMask;
        private TextBox txtSubnetMask;
        private TextBox txtLog;
        private Label lblLog;
        private Button btnCrack;
        private Button btnCopyLogToClipboard;
        private Button btnTest;
        private Label lblNetAdapter;
        private ComboBox cmbNetAdapter;
        private Button btnOpenSocket;
    }
}
