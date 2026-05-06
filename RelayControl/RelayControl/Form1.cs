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
//v1.0.5.26                                                           06 May 2025
//  - 1st release version. Basic functionality implemented and tested with ESP32.
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
        private const string ProgramAuthor = "Patiphan Phakdeeburi.";
        private byte currentStatus = 0x00;
        private bool isUpdatingUI = false;
        private List<string> BlackList = new List<string>();
        private List<string> file_List = new List<string>();

        public Form1()
        {
            InitializeComponent();
            LoadAvailablePorts();
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

        private void LoadAvailablePorts()
        {
            cmbPort.Items.AddRange(SerialPort.GetPortNames());
            if (cmbPort.Items.Count > 0) cmbPort.SelectedIndex = 0;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    serialPort1.PortName = cmbPort.Text;
                    serialPort1.BaudRate = 115200;
                    serialPort1.DataReceived += serialPort1_DataReceived;
                    serialPort1.Open();
                    serialPort1.DtrEnable = false;
                    serialPort1.RtsEnable = false;

                    btnConnect.Text = "Disconnect";
                    btnConnect.BackColor = Color.Salmon;
                    AddStatusLog($"Connected to {serialPort1.PortName}");

                    // ถามสถานะหลังจากเชื่อมต่อ 1 วินาที
                    Timer t = new Timer { Interval = 1000 };
                    t.Tick += (s, ev) => {
                        if (serialPort1.IsOpen) serialPort1.Write(new byte[] { 0x05 }, 0, 1);
                        t.Stop();
                    };
                    t.Start();
                }
                else
                {
                    serialPort1.DataReceived -= serialPort1_DataReceived;
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
            int index = int.Parse(chk.Tag.ToString()); // อย่าลืมใส่ Tag 0-7 ในหน้า Design

            if (chk.Checked) currentStatus |= (byte)(1 << index);
            else currentStatus &= (byte)~(1 << index);

            byte[] frame = { 0x02, currentStatus, currentStatus, 0x03 };
            serialPort1.Write(frame, 0, 4);

            // แสดงผล Command Sent เป็น HEX (02 01 01 03)
            string hexStr = BitConverter.ToString(frame).Replace("-", " ");
            AddStatusLog($"Command Sent: {hexStr}");
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                while (serialPort1.IsOpen && serialPort1.BytesToRead > 0)
                {
                    if (serialPort1.ReadByte() == 0x02)
                    {
                        int wait = 0;
                        while (serialPort1.BytesToRead < 3 && wait < 50) { System.Threading.Thread.Sleep(1); wait++; }

                        if (serialPort1.BytesToRead >= 3)
                        {
                            byte data = (byte)serialPort1.ReadByte();
                            byte chksum = (byte)serialPort1.ReadByte();
                            byte stop = (byte)serialPort1.ReadByte();

                            if (data == chksum && stop == 0x03)
                            {
                                byte[] fullFrame = { 0x02, data, chksum, stop };
                                string hexStatus = BitConverter.ToString(fullFrame).Replace("-", " ");

                                this.Invoke(new MethodInvoker(delegate {
                                    AddStatusLog($"Status Synced: {hexStatus}");
                                    UpdateUIStatus(data);
                                }));
                            }
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


/*====================ARDUINO CODE FOR ESP32====================
#include <Preferences.h>

// กำหนดขา GPIO (ปรับตามการต่อจริง)
const int relayPins[] = {13, 12, 14, 27, 26, 25, 33, 32};
byte currentRelayState = 0x00; 

Preferences pref;

const byte STX = 0x02;
const byte ETX = 0x03;
const byte REQ_STATUS = 0x05;

void setup() {
  Serial.begin(115200);
  
  // เปิดโหมดเก็บข้อมูลถาวร
  pref.begin("relay-app", false);
  currentRelayState = pref.getUChar("state", 0x00);

  for (int i = 0; i < 8; i++) {
    pinMode(relayPins[i], OUTPUT);
    // ทำงานตามค่าล่าสุดที่จำได้ทันที
    bool bitValue = (currentRelayState >> i) & 0x01;
    digitalWrite(relayPins[i], bitValue ? LOW : HIGH); 
  }
}

void loop() {
  if (Serial.available() > 0) {
    byte firstByte = Serial.peek();

    if (firstByte == STX) {
      if (Serial.available() >= 4) {
        Serial.read(); // STX
        byte data = Serial.read();
        byte checksum = Serial.read();
        byte stopByte = Serial.read();

        if ((data == checksum) && (stopByte == ETX)) {
          updateRelays(data);
          sendFeedback(data);
        }
      }
    } 
    else if (firstByte == REQ_STATUS) {
      Serial.read(); // เคลียร์ 0x05
      sendFeedback(currentRelayState); 
    } 
    else {
      Serial.read(); // ทิ้งขยะ
    }
  }
}

void updateRelays(byte state) {
  currentRelayState = state;
  pref.putUChar("state", state); // บันทึกสถานะลง Flash
  
  for (int i = 0; i < 8; i++) {
    bool bitValue = (state >> i) & 0x01;
    digitalWrite(relayPins[i], bitValue ? LOW : HIGH);
  }
}

void sendFeedback(byte state) {
  byte frame[] = {STX, state, state, ETX};
  Serial.write(frame, 4);
}

====================ARDUINO CODE FOR ESP32====================*/