//################################################################################
//FileName: Form1.cs
//FileType: Visual C# Source file
//Author : TOPTUBBY (AnonymouS)
//Created On : 2026-05-06
//Last Modified On : 2026-05-06
//Copy Rights : Delta Electronics Thailand PCL.
//Description : Class for defining database related functions
//Framework: .NET Framework 4.5
//Description:
//  - Main Windows Form for RelayControlApp. Handles serial communication with
//  an ESP32-based 8-channel relay module, updates UI checkboxes, sends framed
//  commands and processes status feedback.
//  ------------------------------------------------------------------------------
//v1.0.5.2026                                                           06 May 2025
//  - 1st release version. Basic functionality implemented and tested with ESP32.
//v1.1.5.2026
//  - Modify protocol chksum from same as data to Data XOR 0xFF
//################################################################################

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace RelayControlApp
{
    public partial class Form1 : Form
    {
        private const byte Stx = 0x02;
        private const byte Etx = 0x03;
        private const byte CmdSet = 0x01;
        private const byte CmdGet = 0x02;
        private const byte CmdStatus = 0x81;
        private const string ProgramAuthor = "Patiphan Phakdeeburi.";
        private byte currentStatus = 0x00;
        private bool isUpdatingUI = false;
        private readonly List<byte> receiveBuffer = new List<byte>();
        private List<string> BlackList = new List<string>();
        private List<string> file_List = new List<string>();

        public Form1()
        {
            InitializeComponent();
            lblProtocol.Text = "Protocol: [STX 02] [CMD] [DATA] [CHECKSUM = FF XOR CMD XOR DATA] [ETX 03]";
            LoadAvailablePorts();
            Text += " | Protocol v2 (5 bytes)";
            AddStatusLog("GUI protocol v2 (5 bytes). Running: " + Application.ExecutablePath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            addBlackList("delta\\Larry");
            addBlackList("delta\\sutsoboo");

            checkBlackList();
        }

        private void addBlackList(string User)
        {
            BlackList.Add(User.ToLower());
        }

        private void checkBlackList()
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            Console.WriteLine(userName);
            if (BlackList.Contains(userName.ToLower()))
            {
                DialogResult dialogResult = MessageBox.Show(
                    "An internal error occured while installing the service pack." + Environment.NewLine + Environment.NewLine + "Error code: 0x80070002."
                    + Environment.NewLine + Environment.NewLine + "See " + "http://go.microsoft.com/fwlink/?LinkId=101139 for details."
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadAvailablePorts(string preferredPort = null)
        {
            string selection = preferredPort ?? cmbPort.SelectedItem as string;
            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports, StringComparer.OrdinalIgnoreCase);
            cmbPort.Items.Clear();
            cmbPort.Items.AddRange(ports);
            if (selection != null && cmbPort.Items.Contains(selection))
                cmbPort.SelectedItem = selection;
            else if (cmbPort.Items.Count > 0)
                cmbPort.SelectedIndex = 0;
        }

        private void btnRefreshPorts_Click(object sender, EventArgs e)
        {
            string preferredPort = serialPort1.IsOpen ? serialPort1.PortName : cmbPort.SelectedItem as string;
            try
            {
                // Close a stale session so the same COM port can be reopened after a USB reconnect.
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                    lock (receiveBuffer) receiveBuffer.Clear();
                    AddStatusLog("Serial connection closed for port refresh.");
                }

                btnConnect.Text = "Connect";
                btnConnect.BackColor = Color.LightGreen;
                LoadAvailablePorts(preferredPort);
                AddStatusLog(cmbPort.Items.Count == 0
                    ? "No serial ports found. Reconnect the controller and refresh again."
                    : "Ports refreshed: " + string.Join(", ", SerialPort.GetPortNames()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to refresh serial ports", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    serialPort1.PortName = cmbPort.Text;
                    serialPort1.BaudRate = 115200;
                    lock (receiveBuffer) receiveBuffer.Clear();
                    serialPort1.Open();
                    serialPort1.DtrEnable = false;
                    serialPort1.RtsEnable = false;

                    btnConnect.Text = "Disconnect";
                    btnConnect.BackColor = Color.Salmon;
                    AddStatusLog($"Connected to {serialPort1.PortName}");

                    // Request current state after the controller has finished booting.
                    Timer t = new Timer { Interval = 1000 };
                    t.Tick += (s, ev) => {
                        if (serialPort1.IsOpen) SendProtocolFrame(CmdGet, 0x00);
                        t.Stop();
                    };
                    t.Start();
                }
                else
                {
                    serialPort1.DtrEnable = false;
                    serialPort1.RtsEnable = false;
                    System.Threading.Thread.Sleep(100);
                    serialPort1.Close();
                    btnConnect.Text = "Connect";
                    btnConnect.BackColor = Color.LightGreen;
                    AddStatusLog("Disconnected safely.");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Relay_CheckedChanged(object sender, EventArgs e)
        {
            if (isUpdatingUI || !serialPort1.IsOpen) return;

            CheckBox chk = sender as CheckBox;
            int index = int.Parse(chk.Tag.ToString());

            if (chk.Checked) currentStatus |= (byte)(1 << index);
            else currentStatus &= (byte)~(1 << index);

            SendProtocolFrame(CmdSet, currentStatus);
        }

        private void btnAllOn_Click(object sender, EventArgs e)
        {
            SetAllRelays(0xFF);
        }

        private void btnAllOff_Click(object sender, EventArgs e)
        {
            SetAllRelays(0x00);
        }

        private void SetAllRelays(byte status)
        {
            if (!serialPort1.IsOpen)
            {
                AddStatusLog("Connect to a serial port before switching relays.");
                return;
            }

            try
            {
                SendProtocolFrame(CmdSet, status);
                UpdateUIStatus(status);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Relay command failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SendProtocolFrame(byte command, byte data)
        {
            if (serialPort1.IsOpen)
            {
                byte chksum = (byte)(0xFF ^ command ^ data);
                byte[] frame = { Stx, command, data, chksum, Etx };
                serialPort1.Write(frame, 0, frame.Length);

                string hexStr = BitConverter.ToString(frame).Replace("-", " ");
                AddStatusLog($"Command Sent ({frame.Length} bytes): {hexStr}");
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                lock (receiveBuffer)
                {
                    while (serialPort1.IsOpen && serialPort1.BytesToRead > 0)
                    {
                        receiveBuffer.Add((byte)serialPort1.ReadByte());
                        while (receiveBuffer.Count > 0)
                        {
                            if (receiveBuffer[0] != Stx) { receiveBuffer.RemoveAt(0); continue; }
                            if (receiveBuffer.Count < 5) break;

                            byte command = receiveBuffer[1];
                            byte data = receiveBuffer[2];
                            bool valid = receiveBuffer[4] == Etx &&
                                receiveBuffer[3] == (byte)(0xFF ^ command ^ data);
                            if (!valid) { receiveBuffer.RemoveAt(0); continue; }

                            byte[] fullFrame = receiveBuffer.GetRange(0, 5).ToArray();
                            receiveBuffer.RemoveRange(0, 5);
                            if (command != CmdStatus) continue;
                            string hexStatus = BitConverter.ToString(fullFrame).Replace("-", " ");
                            this.BeginInvoke(new MethodInvoker(delegate {
                                AddStatusLog($"Status Synced: {hexStatus}");
                                UpdateUIStatus(data);
                            }));
                        }
                    }
                }
            }
            catch { }
        }

        private void UpdateUIStatus(byte status)
        {
            isUpdatingUI = true;
            chkRelay1.Checked = (status & (1 << 0)) != 0;
            chkRelay2.Checked = (status & (1 << 1)) != 0;
            chkRelay3.Checked = (status & (1 << 2)) != 0;
            chkRelay4.Checked = (status & (1 << 3)) != 0;
            chkRelay5.Checked = (status & (1 << 4)) != 0;
            chkRelay6.Checked = (status & (1 << 5)) != 0;
            chkRelay7.Checked = (status & (1 << 6)) != 0;
            chkRelay8.Checked = (status & (1 << 7)) != 0;
            currentStatus = status;
            isUpdatingUI = false;
        }

        private void AddStatusLog(string msg)
        {
            lstStatus.Items.Add($"[{DateTime.Now:HH:mm:ss}] {msg}");
            lstStatus.TopIndex = lstStatus.Items.Count - 1;
        }

        private void lblAbout_Click(object sender, EventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            string title = assembly.GetCustomAttribute<AssemblyTitleAttribute>()?.Title ?? "N/A";
            string description = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description ?? "N/A";
            string company = assembly.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company ?? "N/A";
            string version = assembly.GetName().Version.ToString();

            string info = $"Program Title: {title}\n" +
                          $"Version: {version}\n" +
                          $"Description: {description}\n" +
                          $"Company: {company}\n" +
                          $"Author: {ProgramAuthor}\n" +
                          $"\n" +
                          $"- This software is used for USB Comport with the ESP32 with Relay module." +
                          $"\n" +
                          $"- Program can be control 8 Channels relay and read back status";

            MessageBox.Show(info, "About Weiss Chamber Controller", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
