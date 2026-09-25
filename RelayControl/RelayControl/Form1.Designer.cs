namespace RelayControlApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.grpConnection = new System.Windows.Forms.GroupBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.btnRefreshPorts = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblSerialSettings = new System.Windows.Forms.Label();
            this.lblProtocol = new System.Windows.Forms.Label();
            this.grpRelays = new System.Windows.Forms.GroupBox();
            this.chkRelay1 = new System.Windows.Forms.CheckBox();
            this.chkRelay2 = new System.Windows.Forms.CheckBox();
            this.chkRelay3 = new System.Windows.Forms.CheckBox();
            this.chkRelay4 = new System.Windows.Forms.CheckBox();
            this.chkRelay5 = new System.Windows.Forms.CheckBox();
            this.chkRelay6 = new System.Windows.Forms.CheckBox();
            this.chkRelay7 = new System.Windows.Forms.CheckBox();
            this.chkRelay8 = new System.Windows.Forms.CheckBox();
            this.btnAllOff = new System.Windows.Forms.Button();
            this.btnAllOn = new System.Windows.Forms.Button();
            this.grpLog = new System.Windows.Forms.GroupBox();
            this.lstStatus = new System.Windows.Forms.ListBox();
            this.lblFooter = new System.Windows.Forms.Label();
            this.lblAbout = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.grpConnection.SuspendLayout();
            this.grpRelays.SuspendLayout();
            this.grpLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpConnection
            // 
            this.grpConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpConnection.Controls.Add(this.lblAbout);
            this.grpConnection.Controls.Add(this.lblPort);
            this.grpConnection.Controls.Add(this.cmbPort);
            this.grpConnection.Controls.Add(this.btnRefreshPorts);
            this.grpConnection.Controls.Add(this.btnConnect);
            this.grpConnection.Controls.Add(this.lblSerialSettings);
            this.grpConnection.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpConnection.Location = new System.Drawing.Point(16, 14);
            this.grpConnection.Name = "grpConnection";
            this.grpConnection.Size = new System.Drawing.Size(544, 90);
            this.grpConnection.TabIndex = 0;
            this.grpConnection.TabStop = false;
            this.grpConnection.Text = "Serial Connection";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPort.Location = new System.Drawing.Point(20, 39);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(32, 15);
            this.lblPort.TabIndex = 0;
            this.lblPort.Text = "Port:";
            // 
            // cmbPort
            // 
            this.cmbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPort.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Location = new System.Drawing.Point(65, 34);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(143, 25);
            this.cmbPort.TabIndex = 0;
            // 
            // btnRefreshPorts
            // 
            this.btnRefreshPorts.BackColor = System.Drawing.Color.White;
            this.btnRefreshPorts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshPorts.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRefreshPorts.Location = new System.Drawing.Point(226, 29);
            this.btnRefreshPorts.Name = "btnRefreshPorts";
            this.btnRefreshPorts.Size = new System.Drawing.Size(90, 35);
            this.btnRefreshPorts.TabIndex = 1;
            this.btnRefreshPorts.Text = "Refresh";
            this.btnRefreshPorts.UseVisualStyleBackColor = false;
            this.btnRefreshPorts.Click += new System.EventHandler(this.btnRefreshPorts_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.LightGreen;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnConnect.Location = new System.Drawing.Point(329, 29);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(130, 35);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblSerialSettings
            // 
            this.lblSerialSettings.AutoSize = true;
            this.lblSerialSettings.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSerialSettings.ForeColor = System.Drawing.Color.DimGray;
            this.lblSerialSettings.Location = new System.Drawing.Point(20, 67);
            this.lblSerialSettings.Name = "lblSerialSettings";
            this.lblSerialSettings.Size = new System.Drawing.Size(206, 15);
            this.lblSerialSettings.TabIndex = 3;
            this.lblSerialSettings.Text = "115200 baud  |  8 data bits  |  1 stop bit";
            // 
            // lblProtocol
            // 
            this.lblProtocol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProtocol.AutoEllipsis = true;
            this.lblProtocol.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblProtocol.Location = new System.Drawing.Point(20, 116);
            this.lblProtocol.Name = "lblProtocol";
            this.lblProtocol.Size = new System.Drawing.Size(70, 21);
            this.lblProtocol.TabIndex = 1;
            this.lblProtocol.Text = "Protocol:  [STX 02] [DATA] [CHECKSUM = DATA XOR FF] [ETX 03]     Status request: " +
    "02 05 FA 03";
            // 
            // grpRelays
            // 
            this.grpRelays.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRelays.Controls.Add(this.chkRelay1);
            this.grpRelays.Controls.Add(this.chkRelay2);
            this.grpRelays.Controls.Add(this.chkRelay3);
            this.grpRelays.Controls.Add(this.chkRelay4);
            this.grpRelays.Controls.Add(this.chkRelay5);
            this.grpRelays.Controls.Add(this.chkRelay6);
            this.grpRelays.Controls.Add(this.chkRelay7);
            this.grpRelays.Controls.Add(this.chkRelay8);
            this.grpRelays.Controls.Add(this.btnAllOff);
            this.grpRelays.Controls.Add(this.btnAllOn);
            this.grpRelays.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpRelays.Location = new System.Drawing.Point(16, 133);
            this.grpRelays.Name = "grpRelays";
            this.grpRelays.Size = new System.Drawing.Size(544, 260);
            this.grpRelays.TabIndex = 3;
            this.grpRelays.TabStop = false;
            this.grpRelays.Text = "Relay Channels";
            // 
            // chkRelay1
            // 
            this.chkRelay1.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRelay1.BackColor = System.Drawing.Color.White;
            this.chkRelay1.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.chkRelay1.FlatAppearance.BorderSize = 2;
            this.chkRelay1.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightGreen;
            this.chkRelay1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRelay1.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.chkRelay1.Location = new System.Drawing.Point(18, 31);
            this.chkRelay1.Name = "chkRelay1";
            this.chkRelay1.Size = new System.Drawing.Size(108, 69);
            this.chkRelay1.TabIndex = 1;
            this.chkRelay1.Tag = "0";
            this.chkRelay1.Text = "CH1";
            this.chkRelay1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkRelay1.UseVisualStyleBackColor = false;
            this.chkRelay1.CheckedChanged += new System.EventHandler(this.Relay_CheckedChanged);
            // 
            // chkRelay2
            // 
            this.chkRelay2.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRelay2.BackColor = System.Drawing.Color.White;
            this.chkRelay2.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.chkRelay2.FlatAppearance.BorderSize = 2;
            this.chkRelay2.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightGreen;
            this.chkRelay2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRelay2.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.chkRelay2.Location = new System.Drawing.Point(150, 31);
            this.chkRelay2.Name = "chkRelay2";
            this.chkRelay2.Size = new System.Drawing.Size(108, 69);
            this.chkRelay2.TabIndex = 2;
            this.chkRelay2.Tag = "1";
            this.chkRelay2.Text = "CH2";
            this.chkRelay2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkRelay2.UseVisualStyleBackColor = false;
            this.chkRelay2.CheckedChanged += new System.EventHandler(this.Relay_CheckedChanged);
            // 
            // chkRelay3
            // 
            this.chkRelay3.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRelay3.BackColor = System.Drawing.Color.White;
            this.chkRelay3.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.chkRelay3.FlatAppearance.BorderSize = 2;
            this.chkRelay3.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightGreen;
            this.chkRelay3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRelay3.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.chkRelay3.Location = new System.Drawing.Point(284, 31);
            this.chkRelay3.Name = "chkRelay3";
            this.chkRelay3.Size = new System.Drawing.Size(108, 69);
            this.chkRelay3.TabIndex = 3;
            this.chkRelay3.Tag = "2";
            this.chkRelay3.Text = "CH3";
            this.chkRelay3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkRelay3.UseVisualStyleBackColor = false;
            this.chkRelay3.CheckedChanged += new System.EventHandler(this.Relay_CheckedChanged);
            // 
            // chkRelay4
            // 
            this.chkRelay4.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRelay4.BackColor = System.Drawing.Color.White;
            this.chkRelay4.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.chkRelay4.FlatAppearance.BorderSize = 2;
            this.chkRelay4.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightGreen;
            this.chkRelay4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRelay4.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.chkRelay4.Location = new System.Drawing.Point(417, 31);
            this.chkRelay4.Name = "chkRelay4";
            this.chkRelay4.Size = new System.Drawing.Size(108, 69);
            this.chkRelay4.TabIndex = 4;
            this.chkRelay4.Tag = "3";
            this.chkRelay4.Text = "CH4";
            this.chkRelay4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkRelay4.UseVisualStyleBackColor = false;
            this.chkRelay4.CheckedChanged += new System.EventHandler(this.Relay_CheckedChanged);
            // 
            // chkRelay5
            // 
            this.chkRelay5.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRelay5.BackColor = System.Drawing.Color.White;
            this.chkRelay5.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.chkRelay5.FlatAppearance.BorderSize = 2;
            this.chkRelay5.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightGreen;
            this.chkRelay5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRelay5.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.chkRelay5.Location = new System.Drawing.Point(18, 115);
            this.chkRelay5.Name = "chkRelay5";
            this.chkRelay5.Size = new System.Drawing.Size(108, 69);
            this.chkRelay5.TabIndex = 5;
            this.chkRelay5.Tag = "4";
            this.chkRelay5.Text = "CH5";
            this.chkRelay5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkRelay5.UseVisualStyleBackColor = false;
            this.chkRelay5.CheckedChanged += new System.EventHandler(this.Relay_CheckedChanged);
            // 
            // chkRelay6
            // 
            this.chkRelay6.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRelay6.BackColor = System.Drawing.Color.White;
            this.chkRelay6.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.chkRelay6.FlatAppearance.BorderSize = 2;
            this.chkRelay6.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightGreen;
            this.chkRelay6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRelay6.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.chkRelay6.Location = new System.Drawing.Point(150, 115);
            this.chkRelay6.Name = "chkRelay6";
            this.chkRelay6.Size = new System.Drawing.Size(108, 69);
            this.chkRelay6.TabIndex = 6;
            this.chkRelay6.Tag = "5";
            this.chkRelay6.Text = "CH6";
            this.chkRelay6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkRelay6.UseVisualStyleBackColor = false;
            this.chkRelay6.CheckedChanged += new System.EventHandler(this.Relay_CheckedChanged);
            // 
            // chkRelay7
            // 
            this.chkRelay7.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRelay7.BackColor = System.Drawing.Color.White;
            this.chkRelay7.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.chkRelay7.FlatAppearance.BorderSize = 2;
            this.chkRelay7.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightGreen;
            this.chkRelay7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRelay7.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.chkRelay7.Location = new System.Drawing.Point(284, 115);
            this.chkRelay7.Name = "chkRelay7";
            this.chkRelay7.Size = new System.Drawing.Size(108, 69);
            this.chkRelay7.TabIndex = 7;
            this.chkRelay7.Tag = "6";
            this.chkRelay7.Text = "CH7";
            this.chkRelay7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkRelay7.UseVisualStyleBackColor = false;
            this.chkRelay7.CheckedChanged += new System.EventHandler(this.Relay_CheckedChanged);
            // 
            // chkRelay8
            // 
            this.chkRelay8.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkRelay8.BackColor = System.Drawing.Color.White;
            this.chkRelay8.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.chkRelay8.FlatAppearance.BorderSize = 2;
            this.chkRelay8.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightGreen;
            this.chkRelay8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRelay8.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.chkRelay8.Location = new System.Drawing.Point(417, 115);
            this.chkRelay8.Name = "chkRelay8";
            this.chkRelay8.Size = new System.Drawing.Size(108, 69);
            this.chkRelay8.TabIndex = 8;
            this.chkRelay8.Tag = "7";
            this.chkRelay8.Text = "CH8";
            this.chkRelay8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkRelay8.UseVisualStyleBackColor = false;
            this.chkRelay8.CheckedChanged += new System.EventHandler(this.Relay_CheckedChanged);
            // 
            // btnAllOff
            // 
            this.btnAllOff.BackColor = System.Drawing.Color.White;
            this.btnAllOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllOff.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAllOff.Location = new System.Drawing.Point(18, 203);
            this.btnAllOff.Name = "btnAllOff";
            this.btnAllOff.Size = new System.Drawing.Size(108, 34);
            this.btnAllOff.TabIndex = 9;
            this.btnAllOff.Text = "ALL OFF";
            this.btnAllOff.UseVisualStyleBackColor = false;
            this.btnAllOff.Click += new System.EventHandler(this.btnAllOff_Click);
            // 
            // btnAllOn
            // 
            this.btnAllOn.BackColor = System.Drawing.Color.LightGreen;
            this.btnAllOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllOn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAllOn.Location = new System.Drawing.Point(139, 203);
            this.btnAllOn.Name = "btnAllOn";
            this.btnAllOn.Size = new System.Drawing.Size(108, 34);
            this.btnAllOn.TabIndex = 10;
            this.btnAllOn.Text = "ALL ON";
            this.btnAllOn.UseVisualStyleBackColor = false;
            this.btnAllOn.Click += new System.EventHandler(this.btnAllOn_Click);
            // 
            // grpLog
            // 
            this.grpLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpLog.Controls.Add(this.lstStatus);
            this.grpLog.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpLog.Location = new System.Drawing.Point(16, 406);
            this.grpLog.Name = "grpLog";
            this.grpLog.Size = new System.Drawing.Size(544, 236);
            this.grpLog.TabIndex = 4;
            this.grpLog.TabStop = false;
            this.grpLog.Text = "Communication Log";
            // 
            // lstStatus
            // 
            this.lstStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(29)))), ((int)(((byte)(34)))));
            this.lstStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstStatus.Font = new System.Drawing.Font("Consolas", 9F);
            this.lstStatus.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lstStatus.FormattingEnabled = true;
            this.lstStatus.ItemHeight = 14;
            this.lstStatus.Location = new System.Drawing.Point(11, 24);
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.Size = new System.Drawing.Size(522, 196);
            this.lstStatus.TabIndex = 0;
            // 
            // lblFooter
            // 
            this.lblFooter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFooter.AutoEllipsis = true;
            this.lblFooter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFooter.ForeColor = System.Drawing.Color.DimGray;
            this.lblFooter.Location = new System.Drawing.Point(19, 645);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(541, 22);
            this.lblFooter.TabIndex = 5;
            this.lblFooter.Text = "Relay Control 8CH   |   USB serial connection   |   See the log for sent commands" +
    " and status feedback";
            // 
            // lblAbout
            // 
            this.lblAbout.AutoSize = true;
            this.lblAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAbout.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Underline);
            this.lblAbout.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblAbout.Location = new System.Drawing.Point(498, 0);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(40, 15);
            this.lblAbout.TabIndex = 6;
            this.lblAbout.Text = "About";
            this.lblAbout.Click += new System.EventHandler(this.lblAbout_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(574, 673);
            this.Controls.Add(this.grpConnection);
            this.Controls.Add(this.lblProtocol);
            this.Controls.Add(this.grpRelays);
            this.Controls.Add(this.grpLog);
            this.Controls.Add(this.lblFooter);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relay Control 8CH";
            this.grpConnection.ResumeLayout(false);
            this.grpConnection.PerformLayout();
            this.grpRelays.ResumeLayout(false);
            this.grpLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.GroupBox grpConnection;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.Button btnRefreshPorts;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblSerialSettings;
        private System.Windows.Forms.Label lblProtocol;
        private System.Windows.Forms.GroupBox grpRelays;
        private System.Windows.Forms.CheckBox chkRelay1;
        private System.Windows.Forms.CheckBox chkRelay2;
        private System.Windows.Forms.CheckBox chkRelay3;
        private System.Windows.Forms.CheckBox chkRelay4;
        private System.Windows.Forms.CheckBox chkRelay5;
        private System.Windows.Forms.CheckBox chkRelay6;
        private System.Windows.Forms.CheckBox chkRelay7;
        private System.Windows.Forms.CheckBox chkRelay8;
        private System.Windows.Forms.Button btnAllOff;
        private System.Windows.Forms.Button btnAllOn;
        private System.Windows.Forms.GroupBox grpLog;
        private System.Windows.Forms.ListBox lstStatus;
        private System.Windows.Forms.Label lblFooter;
        private System.Windows.Forms.Label lblAbout;
        private System.IO.Ports.SerialPort serialPort1;
    }
}
