namespace RelayControlApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cmbPort = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.chkRelay1 = new System.Windows.Forms.CheckBox();
            this.chkRelay2 = new System.Windows.Forms.CheckBox();
            this.chkRelay3 = new System.Windows.Forms.CheckBox();
            this.chkRelay4 = new System.Windows.Forms.CheckBox();
            this.chkRelay5 = new System.Windows.Forms.CheckBox();
            this.chkRelay6 = new System.Windows.Forms.CheckBox();
            this.chkRelay7 = new System.Windows.Forms.CheckBox();
            this.chkRelay8 = new System.Windows.Forms.CheckBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.lstStatus = new System.Windows.Forms.ListBox();
            this.lblAbout = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbPort
            // 
            this.cmbPort.FormattingEnabled = true;
            this.cmbPort.Location = new System.Drawing.Point(12, 12);
            this.cmbPort.Name = "cmbPort";
            this.cmbPort.Size = new System.Drawing.Size(71, 21);
            this.cmbPort.TabIndex = 0;
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.LightGreen;
            this.btnConnect.Location = new System.Drawing.Point(89, 10);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(194, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // chkRelay1
            // 
            this.chkRelay1.AutoSize = true;
            this.chkRelay1.Location = new System.Drawing.Point(12, 49);
            this.chkRelay1.Name = "chkRelay1";
            this.chkRelay1.Size = new System.Drawing.Size(47, 17);
            this.chkRelay1.TabIndex = 2;
            this.chkRelay1.Tag = "0";
            this.chkRelay1.Text = "CH1";
            this.chkRelay1.UseVisualStyleBackColor = true;
            this.chkRelay1.CheckedChanged += new System.EventHandler(this.Relay_CheckedChanged);
            // 
            // chkRelay2
            // 
            this.chkRelay2.AutoSize = true;
            this.chkRelay2.Location = new System.Drawing.Point(12, 72);
            this.chkRelay2.Name = "chkRelay2";
            this.chkRelay2.Size = new System.Drawing.Size(47, 17);
            this.chkRelay2.TabIndex = 2;
            this.chkRelay2.Tag = "1";
            this.chkRelay2.Text = "CH2";
            this.chkRelay2.UseVisualStyleBackColor = true;
            this.chkRelay2.CheckedChanged += new System.EventHandler(this.Relay_CheckedChanged);
            // 
            // chkRelay3
            // 
            this.chkRelay3.AutoSize = true;
            this.chkRelay3.Location = new System.Drawing.Point(12, 95);
            this.chkRelay3.Name = "chkRelay3";
            this.chkRelay3.Size = new System.Drawing.Size(47, 17);
            this.chkRelay3.TabIndex = 2;
            this.chkRelay3.Tag = "2";
            this.chkRelay3.Text = "CH3";
            this.chkRelay3.UseVisualStyleBackColor = true;
            this.chkRelay3.CheckedChanged += new System.EventHandler(this.Relay_CheckedChanged);
            // 
            // chkRelay4
            // 
            this.chkRelay4.AutoSize = true;
            this.chkRelay4.Location = new System.Drawing.Point(12, 118);
            this.chkRelay4.Name = "chkRelay4";
            this.chkRelay4.Size = new System.Drawing.Size(47, 17);
            this.chkRelay4.TabIndex = 2;
            this.chkRelay4.Tag = "3";
            this.chkRelay4.Text = "CH4";
            this.chkRelay4.UseVisualStyleBackColor = true;
            this.chkRelay4.CheckedChanged += new System.EventHandler(this.Relay_CheckedChanged);
            // 
            // chkRelay5
            // 
            this.chkRelay5.AutoSize = true;
            this.chkRelay5.Location = new System.Drawing.Point(12, 141);
            this.chkRelay5.Name = "chkRelay5";
            this.chkRelay5.Size = new System.Drawing.Size(47, 17);
            this.chkRelay5.TabIndex = 2;
            this.chkRelay5.Tag = "4";
            this.chkRelay5.Text = "CH5";
            this.chkRelay5.UseVisualStyleBackColor = true;
            this.chkRelay5.CheckedChanged += new System.EventHandler(this.Relay_CheckedChanged);
            // 
            // chkRelay6
            // 
            this.chkRelay6.AutoSize = true;
            this.chkRelay6.Location = new System.Drawing.Point(12, 164);
            this.chkRelay6.Name = "chkRelay6";
            this.chkRelay6.Size = new System.Drawing.Size(47, 17);
            this.chkRelay6.TabIndex = 2;
            this.chkRelay6.Tag = "5";
            this.chkRelay6.Text = "CH6";
            this.chkRelay6.UseVisualStyleBackColor = true;
            this.chkRelay6.CheckedChanged += new System.EventHandler(this.Relay_CheckedChanged);
            // 
            // chkRelay7
            // 
            this.chkRelay7.AutoSize = true;
            this.chkRelay7.Location = new System.Drawing.Point(12, 187);
            this.chkRelay7.Name = "chkRelay7";
            this.chkRelay7.Size = new System.Drawing.Size(47, 17);
            this.chkRelay7.TabIndex = 2;
            this.chkRelay7.Tag = "6";
            this.chkRelay7.Text = "CH7";
            this.chkRelay7.UseVisualStyleBackColor = true;
            this.chkRelay7.CheckedChanged += new System.EventHandler(this.Relay_CheckedChanged);
            // 
            // chkRelay8
            // 
            this.chkRelay8.AutoSize = true;
            this.chkRelay8.Location = new System.Drawing.Point(12, 210);
            this.chkRelay8.Name = "chkRelay8";
            this.chkRelay8.Size = new System.Drawing.Size(47, 17);
            this.chkRelay8.TabIndex = 2;
            this.chkRelay8.Tag = "7";
            this.chkRelay8.Text = "CH8";
            this.chkRelay8.UseVisualStyleBackColor = true;
            this.chkRelay8.CheckedChanged += new System.EventHandler(this.Relay_CheckedChanged);
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // lstStatus
            // 
            this.lstStatus.FormattingEnabled = true;
            this.lstStatus.Location = new System.Drawing.Point(65, 49);
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.Size = new System.Drawing.Size(218, 186);
            this.lstStatus.TabIndex = 3;
            // 
            // lblAbout
            // 
            this.lblAbout.AutoSize = true;
            this.lblAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAbout.ForeColor = System.Drawing.Color.Blue;
            this.lblAbout.Location = new System.Drawing.Point(248, 238);
            this.lblAbout.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(35, 13);
            this.lblAbout.TabIndex = 10;
            this.lblAbout.Text = "About";
            this.lblAbout.Click += new System.EventHandler(this.lblAbout_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 256);
            this.Controls.Add(this.lblAbout);
            this.Controls.Add(this.lstStatus);
            this.Controls.Add(this.chkRelay8);
            this.Controls.Add(this.chkRelay7);
            this.Controls.Add(this.chkRelay6);
            this.Controls.Add(this.chkRelay5);
            this.Controls.Add(this.chkRelay4);
            this.Controls.Add(this.chkRelay3);
            this.Controls.Add(this.chkRelay2);
            this.Controls.Add(this.chkRelay1);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.cmbPort);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RELAY CONTROL v1.1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.CheckBox chkRelay1;
        private System.Windows.Forms.CheckBox chkRelay2;
        private System.Windows.Forms.CheckBox chkRelay3;
        private System.Windows.Forms.CheckBox chkRelay4;
        private System.Windows.Forms.CheckBox chkRelay5;
        private System.Windows.Forms.CheckBox chkRelay6;
        private System.Windows.Forms.CheckBox chkRelay7;
        private System.Windows.Forms.CheckBox chkRelay8;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ListBox lstStatus;
        private System.Windows.Forms.Label lblAbout;
    }
}

