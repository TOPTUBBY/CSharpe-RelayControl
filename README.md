# CSharpe-RelayControl

<p align="left">
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C#" />
  <img src="https://img.shields.io/badge/.NET_4.5-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET" />
  <img src="https://img.shields.io/badge/Arduino-00979D?style=for-the-badge&logo=arduino&logoColor=white" alt="Arduino" />
  <img src="https://img.shields.io/badge/Version-1.1.5.26-blue?style=for-the-badge" alt="Version" />
  <img src="https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge" alt="License" />
</p>

![Relay Control GUI](RelayControl/RelayControl/screenshot.png) 

[🇹🇭 ภาษาไทย](#ภาษาไทย) | [🇬🇧 English](#english)

---

## 🇹🇭 ภาษาไทย

**CSharpe-RelayControl** เป็นโปรแกรมประยุกต์บน Windows (Windows Forms) ที่พัฒนาด้วย C# (.NET Framework 4.5) สำหรับควบคุมรีเลย์ 8 ช่อง (8-Channel Relay Module) ที่เชื่อมต่อกับไมโครคอนโทรลเลอร์ ESP32 ผ่านการสื่อสารทาง Serial Port (USB)

โปรแกรมนี้ส่งคำสั่งเปิด-ปิดรีเลย์ผ่าน ESP32 และอ่านสถานะที่เฟิร์มแวร์ส่งกลับเพื่ออัปเดต GUI โดยสถานะที่ส่งกลับเป็นค่าคำสั่งล่าสุด ไม่ใช่การวัดหน้าสัมผัสรีเลย์จริง (โปรโตคอลปัจจุบันมีข้อจำกัดสำหรับ bitmask 0x05 ดูด้านล่าง)

### คุณสมบัติหลัก
*   **ควบคุมรีเลย์ 8 ช่อง:** สามารถเปิด/ปิด รีเลย์แต่ละช่องได้อย่างอิสระผ่าน CheckBox บน GUI
*   **การเชื่อมต่อ Serial Port:** เชื่อมต่อผ่าน USB Serial (Baudrate: 115200)
*   **Status Feedback:** โปรแกรมแสดงค่าที่ ESP ส่งกลับเมื่อเชื่อมต่อหรือเปลี่ยนสถานะ และ ESP บันทึกค่าไว้ใน Flash; ไม่ได้ตรวจหน้าสัมผัสจริง
*   **โปรโตคอลแบบมี checksum:** ใช้โครงสร้างข้อมูลแบบ Frame `[STX] [DATA] [CHECKSUM] [ETX]` เพื่อป้องกันข้อมูลผิดพลาด

### ฮาร์ดแวร์ที่ใช้จริง: โมดูลรีเลย์ 4 ช่อง × 2

จากภาพที่แนบมา โมดูลหนึ่งตัวมีรีเลย์ 4 ตัว รุ่นบนตัวรีเลย์อ่านได้ว่า `SRD-05VDC-SL-C`; ใช้ 2 โมดูล รวม 8 ช่อง ตัวอักษร **05VDC** บนตัวรีเลย์หมายถึงคอยล์ 5 V ไม่ได้ยืนยันว่าไฟเลี้ยงทั้งโมดูลเป็น 3.3 V หรือว่าขา `IN` รองรับสัญญาณ 3.3 V ต้องตรวจสเปกของ **โมดูลทั้งบอร์ด** หรือวัดวงจรจริงก่อนต่อไฟ แม้ร้านค้าจะเรียกว่า “3.3 V relay module” ก็ตาม อ้างอิง [datasheet รีเลย์ SRD ที่จัดทำโดย Songle](https://www.handsontec.com/dataspecs/relay/SRD-05VDC-SL-C.pdf)

| GUI / บิตสถานะ | โมดูล | อินพุตบนโมดูล | ESP32 GPIO | ESP32-C3 SuperMini GPIO | ESP8266 NodeMCU/D1 mini |
| --- | --- | --- | --- | --- | --- |
| CH1 / bit 0 | A | IN1 | 14 | 0 | 16 (D0) |
| CH2 / bit 1 | A | IN2 | 27 | 1 | 5 (D1) |
| CH3 / bit 2 | A | IN3 | 26 | 3 | 4 (D2) |
| CH4 / bit 3 | A | IN4 | 25 | 4 | 0 (D3) ⚠ |
| CH5 / bit 4 | B | IN1 | 33 | 5 | 2 (D4) ⚠ |
| CH6 / bit 5 | B | IN2 | 32 | 6 | 14 (D5) |
| CH7 / bit 6 | B | IN3 | 4 | 7 | 12 (D6) |
| CH8 / bit 7 | B | IN4 | 16 | 10 | 13 (D7) |

ตารางตรงกับ `relayPins[]` ของสเก็ตช์แต่ละรุ่นด้านล่าง โดย CH1–CH4 ต่อ IN1–IN4 ของโมดูล A และ CH5–CH8 ต่อ IN1–IN4 ของโมดูล B; GPIO ในคอมเมนต์ท้าย `Form1.cs` ของ ESP32 รุ่นเดิมเป็นคนละชุด จึงต้องตรวจสเก็ตช์ที่แฟลชจริงก่อนต่อสาย

- **ESP32-C3 SuperMini:** เดินสาย CH1–CH7 ตาม GPIO `0, 1, 3, 4, 5, 6, 7` บนฝั่งหนึ่ง โดยข้าม GPIO2 (strapping); CH8 ต่อ GPIO10 อีกฝั่ง ไม่ใช้ GPIO8/9 (strapping) หรือ GPIO18/19 (USB) ตรวจตำแหน่ง GPIO บนบอร์ด SuperMini รุ่นจริงก่อนเสียบสาย
- **ESP8266:** ตารางและสเก็ตช์เรียง CH1–CH8 ตามป้าย `D0–D7`; เดินสายไปตามลำดับขาจริงเท่าที่บอร์ดจัดไว้ แล้วข้ามขาไฟเลี้ยง/กราวด์หรือย้ายไปอีกฝั่งเมื่อจำเป็น ตำแหน่งขา NodeMCU และ D1 mini ต่างกัน ให้ยึด **ป้าย D0–D7 บนบอร์ดจริง** กับตารางนี้ GPIO0/D3 (CH4) และ GPIO2/D4 (CH5) เป็นสองขา ⚠ ที่เลี่ยงไม่ได้เมื่อขับรีเลย์ 8 ช่องตรงและคง USB Serial อย่าใช้ GPIO15/D8 ซึ่งต้อง LOW ตอนบูต หรือ GPIO1/3 ที่ใช้ Serial กับ GUI
- **สำคัญสำหรับ D3/D4:** ใช้เฉพาะอินพุตรีเลย์ active-low ที่ไม่ดึง GPIO0/2 ลง LOW ระหว่าง reset และไม่ป้อนไฟเกิน 3.3 V; ควรมี pull-up ไป 3.3 V ตามวงจรบอร์ดจริง (เช่น 10 kΩ หากยังไม่มี) แล้ววัดว่าทั้งสองขา HIGH ตั้งแต่ก่อนจ่ายไฟรีเลย์/เปิดเครื่อง หากบอร์ดบูตไม่ขึ้นหรือมีรีเลย์กระตุกระหว่างรีเซ็ต ต้องแก้ฮาร์ดแวร์หรือใช้ตัวขยาย GPIO ไม่สามารถแก้ด้วย `digitalWrite()` หลังบูตได้ ดู [Espressif: ESP32-C3 GPIO](https://docs.espressif.com/projects/esp-idf/en/latest/esp32c3/api-reference/peripherals/gpio.html) และ [ESP8266 boot mode](https://docs.espressif.com/projects/esptool/en/latest/esp8266/advanced-topics/boot-mode-selection.html)

- ต่อ GND ของ ESP กับ GND ของโมดูล A/B ร่วมกัน **เมื่อใช้อินพุตควบคุมร่วมกัน**; ต่อ `IN1..IN4` ตามตาราง ตรวจตำแหน่ง `VCC/GND/IN` ที่พิมพ์บนบอร์ดจริงก่อนเสียบสาย
- กำหนดไฟเลี้ยง `VCC` จากสเปกโมดูลที่ซื้อและผลวัด ไม่ต่อ 5 V เข้าขา GPIO/3V3 ของ ESP โดยตรง หากโมดูลรับอินพุต 3.3 V ไม่แน่นอนหรือมี pull-up ไป 5 V ให้ใช้วงจรขับ/level interface ที่ระบุสเปกชัดเจน
- คอยล์ Songle 5 V รุ่นนี้กินประมาณ 71.4 mA/ตัว; 8 ตัวพร้อมกันเป็นประมาณ 0.57 A **เฉพาะคอยล์** (ยังไม่รวม LED/วงจรขับ/ESP) จึงควรเริ่มออกแบบด้วยแหล่งจ่าย 5 V ที่มีกระแสสำรอง เช่น 2 A **หากบอร์ดโมดูลระบุว่าใช้ไฟเลี้ยง 5 V** แล้ววัดกระแสจริงขณะทุกช่อง ON อย่าดึงกระแสคอยล์จาก GPIO หรือขา 3V3 ของบอร์ด ESP
- ขั้วโหลดแต่ละช่องเป็น `COM/NO/NC`: ตรวจ silk screen และวัด continuity เพื่อยืนยันตำแหน่งก่อนต่อโหลด `COM–NO` เหมาะเมื่ออยากให้หน้าสัมผัสเปิดตอนคอยล์ไม่มีไฟ ค่า 10 A ที่พิมพ์บนรีเลย์เป็นพิกัด **ชิ้นส่วน** ไม่ใช่การรับรองทั้งโมดูล/สาย/ขั้วต่อสำหรับโหลดทุกชนิด
- โค้ดนี้เป็น active-low (`LOW` = สั่ง ON) และคืนค่าสถานะล่าสุดหลังไฟกลับ จึงอาจมีโหลด ON ทันทีหลังบูต; ทดสอบการเปิดเครื่อง/รีเซ็ตกับ LED หรือโหลดแรงดันต่ำก่อนต่อระบบจริง
- รายการชิ้นส่วนและเงื่อนไขการเลือกซื้ออยู่ใน [BOM ของโปรเจกต์](BOM.md)

### ข้อจำกัดที่ควรทราบของโค้ดเวอร์ชันนี้

- GUI รับ **สถานะที่ ESP ส่งกลับตามคำสั่ง** ไม่ได้วัดหน้าสัมผัสรีเลย์หรือตรวจว่าโหลดทำงานจริง
- โปรโตคอลเดิมใช้ `DATA=0x05` เป็นคำสั่งถามสถานะด้วย จึงสั่งชุด CH1+CH3 ON เพียงสองช่องนี้ (bitmask `0x05`) ไม่ได้ และ GUI จะข้ามการอัปเดตเมื่อได้รับสถานะ `0x05`; ไม่ควรใช้สถานะนี้ในงานจริงจนกว่าทั้ง GUI และเฟิร์มแวร์จะได้รับการแก้ไขและทดสอบร่วมกัน
- โค้ด ESP8266 ใช้ `0xFF` เป็นตัวบ่งชี้ EEPROM ว่าง จึงคืนค่าสถานะทุกช่อง ON หลังรีบูตไม่ได้ตามที่เขียนไว้ในตัวอย่างปัจจุบัน
- ESP32-C3 เลี่ยง GPIO2/8/9 แล้ว; ESP8266 ยังใช้ GPIO0/2 เป็น CH4/CH5 ซึ่งต้อง HIGH ระหว่าง reset และอาจทำให้รีเลย์กระตุกตอนบูต ควรทดสอบ cold boot, reset และ flash พร้อมต่อโมดูลจริงด้วยโหลดแรงดันต่ำ ESP32-C3 ที่ใช้ USB ภายในควรเปิด **USB CDC On Boot** ตาม [คำแนะนำ Espressif](https://docs.espressif.com/projects/arduino-esp32/en/latest/tutorials/cdc_dfu_flash.html)

### การใช้งาน
1. อัปโหลดโค้ด Arduino ลงในบอร์ด ESP32 (ดูโค้ดด้านล่าง)
2. ต่อสายรีเลย์เข้ากับขา GPIO ของ ESP32 ตามที่กำหนดในโค้ด
3. เปิดโปรแกรม `RelayControlApp`
4. เลือกพอร์ต COM ที่เชื่อมต่อกับ ESP32
5. กดปุ่ม **Connect**
6. สามารถคลิกที่ CheckBox ของแต่ละ Relay เพื่อสั่งเปิด/ปิดได้ทันที

---

## 🇬🇧 English

**CSharpe-RelayControl** is a Windows Forms application developed in C# (.NET Framework 4.5) for controlling an 8-Channel Relay Module connected to an ESP32 microcontroller via Serial Port (USB).

The application sends ON/OFF commands to the ESP32 and displays its reported state. The response reflects the last commanded bitmask, not measured relay contacts; bitmask 0x05 has a known collision in this version.

### Key Features
*   **8-Channel Relay Control:** Independently control each relay via GUI CheckBoxes.
*   **Serial Communication:** Connects via USB Serial (Baudrate: 115200).
*   **Status Feedback:** Displays the ESP32-reported bitmask; firmware saves it in flash for restoration on reboot. It does not sense physical contacts.
*   **Framed protocol with XOR checksum:** Uses a framed data structure `[STX] [DATA] [CHECKSUM] [ETX]` to prevent data corruption.

### Actual relay hardware

The pictured assembly uses **two 4-channel modules** for eight outputs. On ESP32-C3 SuperMini, CH1–CH7 use GPIO0,1,3–7 on one side and CH8 uses GPIO10 on the other side, skipping boot pin GPIO2. On ESP8266 NodeMCU/D1 mini, CH1–CH8 follow D0–D7 in board-label order; D3/GPIO0 and D4/GPIO2 remain boot-sensitive and must stay HIGH during reset. Test cold starts with the actual relay module. Each pictured relay is marked `SRD-05VDC-SL-C`, indicating a **5 V coil**. “3.3 V module” may describe a control-input rating; the module supply voltage and input compatibility cannot be confirmed from this photo alone. Verify the board documentation or measure the circuit before applying power. See the [Thai wiring map](#ฮาร์ดแวร์ที่ใช้จริง-โมดูลรีเลย์-4-ช่อง--2) and [project BOM](BOM.md).

The GUI displays ESP-reported state, not measured contact state. This version reserves data value `0x05` for a status request, so CH1+CH3-only cannot be commanded. The firmware restores its last saved state after power returns. Check startup and contact behavior with a low-voltage test load before deploying.

### How to Use
1. Upload the provided Arduino code to your ESP32 board.
2. Connect the relay module to the ESP32 GPIO pins as defined in the code.
3. Launch the `RelayControlApp`.
4. Select the COM port connected to the ESP32.
5. Click the **Connect** button.
6. Toggle the CheckBoxes to control each relay.

---

## 📡 Communication Protocol (current 4-byte version)

**Baudrate:** 115200, 8N1. The GUI sends a four-byte frame `[STX=02] [DATA] [CHECKSUM] [ETX=03]`, where `CHECKSUM = DATA XOR FF`. The ESP sends its last commanded relay bitmask back using the same frame. Bit 0 is CH1; bit 7 is CH8. The checksum is for error detection only; it does not authenticate commands.

| Action | Frame (hex) | Notes |
| --- | --- | --- |
| All OFF | `02 00 FF 03` | DATA = 00 |
| CH1 ON only | `02 01 FE 03` | DATA = 01 |
| Status request | `02 05 FA 03` | `05` is reserved, not a valid controllable bitmask in this implementation |
| CH8 ON only | `02 80 7F 03` | DATA = 80 |

The current status-request frame is four bytes, **not** a single standalone `05` byte. A payload `05` would also mean CH1+CH3 ON as a bitmask, creating a command collision; see the limitation above. Firmware and GUI must use this same protocol.

---

## 💻 Arduino Code (For ESP32)

คุณสามารถนำโค้ดด้านล่างนี้ไปอัปโหลดลงบน ESP32 ผ่าน Arduino IDE ได้เลย
You can upload the following code to your ESP32 using the Arduino IDE.

```cpp
#include <Preferences.h>

// กำหนดขา GPIO (หลีกเลี่ยงขา 12, 34, 35, 36, 39)
// ตัวอย่างนี้ใช้ขา: 14, 27, 26, 25, 33, 32, 4, 16
const int relayPins[] = {14, 27, 26, 25, 33, 32, 4, 16};
byte currentRelayState = 0x00; 
Preferences pref;

const byte STX = 0x02;
const byte ETX = 0x03;

void setup() {
  Serial.begin(115200);
  pref.begin("relay-app", false);
  currentRelayState = pref.getUChar("state", 0x00);

  for (int i = 0; i < 8; i++) {
    pinMode(relayPins[i], OUTPUT);
    // ทำงานตามสถานะล่าสุดที่บันทึกไว้ (Active Low)
    bool bitValue = (currentRelayState >> i) & 0x01;
    digitalWrite(relayPins[i], bitValue ? LOW : HIGH); 
  }
}

void loop() {
  // รอจนกว่าข้อมูลจะเข้ามาอย่างน้อย 4 ไบต์
  if (Serial.available() >= 4) {
    if (Serial.peek() == STX) {
      Serial.read(); // เคลียร์ STX ออกจาก Buffer
      byte data = Serial.read();
      byte checksum = Serial.read();
      byte stopByte = Serial.read();

      // ตรวจสอบ XOR Checksum และ Stop Byte
      if (((data ^ checksum) == 0xFF) && (stopByte == ETX)) {
        if (data == 0x05) {
          // หากเป็นคำสั่ง 0x05 (ถามสถานะ) ให้ตอบกลับทันที
          sendFeedback(currentRelayState);
        } else {
          // หากเป็นคำสั่งสั่งงาน Relay ให้อัปเดตและตอบกลับ
          updateRelays(data);
          sendFeedback(data);
        }
      }
    } else {
      // หากไบต์แรกไม่ใช่ STX ให้ทิ้งขยะไปทีละ 1 ไบต์
      Serial.read(); 
    }
  }
}

void updateRelays(byte state) {
  currentRelayState = state;
  pref.putUChar("state", state); // บันทึกการเปลี่ยนแปลงลง Flash
  
  for (int i = 0; i < 8; i++) {
    bool bitValue = (state >> i) & 0x01;
    digitalWrite(relayPins[i], bitValue ? LOW : HIGH);
  }
}

void sendFeedback(byte state) {
  byte chk = 0xFF ^ state; // คำนวณ XOR Checksum ฝั่งส่งกลับ
  byte frame[] = {STX, state, chk, ETX};
  Serial.write(frame, 4);
}
```
---

## 💻 Arduino Code (For ESP32-C3)
note : USB CDC On Boot need to Enable

```cpp
#include <Preferences.h>

// ESP32-C3 SuperMini: CH1..CH7 = GPIO0,1,3,4,5,6,7; CH8 = GPIO10.
// Skip boot strapping GPIO2/8/9. Verify your board's pin labels.
const int relayPins[] = {0, 1, 3, 4, 5, 6, 7, 10};
byte currentRelayState = 0x00; 
Preferences pref;

const byte STX = 0x02;
const byte ETX = 0x03;

void updateRelays(byte state);
void sendFeedback(byte state);

void setup() {
  Serial.begin(115200);
  pref.begin("relay-app", false);
  currentRelayState = pref.getUChar("state", 0x00);

  for (int i = 0; i < 8; i++) {
    // Preset output latch before enabling output; this cannot change boot straps.
    bool bitValue = (currentRelayState >> i) & 0x01;
    digitalWrite(relayPins[i], bitValue ? LOW : HIGH);
    pinMode(relayPins[i], OUTPUT);
  }
}

void loop() {
  if (Serial.available() >= 4) {
    if (Serial.peek() == STX) {
      Serial.read(); // เคลียร์ STX 
      byte data = Serial.read();
      byte checksum = Serial.read();
      byte stopByte = Serial.read();

      // ตรวจสอบ XOR Checksum
      if (((data ^ checksum) == 0xFF) && (stopByte == ETX)) {
        if (data == 0x05) {
          sendFeedback(currentRelayState);
        } else {
          updateRelays(data);
          sendFeedback(data);
        }
      }
    } else {
      Serial.read(); // ทิ้งขยะ
    }
  }
}

void updateRelays(byte state) {
  currentRelayState = state;
  pref.putUChar("state", state); 
  
  for (int i = 0; i < 8; i++) {
    bool bitValue = (state >> i) & 0x01;
    digitalWrite(relayPins[i], bitValue ? LOW : HIGH);
  }
}

void sendFeedback(byte state) {
  byte chk = 0xFF ^ state; 
  byte frame[] = {STX, state, chk, ETX};
  Serial.write(frame, 4);
}
```
---

## 💻 Arduino Code (For ESP8266)

```cpp
#include <EEPROM.h>

// ESP8266 NodeMCU/D1 mini: CH1..CH8 follow D0..D7 board labels.
// D3/GPIO0 and D4/GPIO2 are boot pins: both MUST remain HIGH at reset.
// Relay inputs must not pull either pin LOW before setup() runs.
// D8/GPIO15 must remain LOW at boot; TX/RX GPIO1/3 are for USB Serial.
const int relayPins[] = {16, 5, 4, 0, 2, 14, 12, 13}; 
byte currentRelayState = 0x00; 

const byte STX = 0x02;
const byte ETX = 0x03;

void updateRelays(byte state);
void sendFeedback(byte state);

void setup() {
  Serial.begin(115200);
  
  // ESP8266 ใช้ EEPROM ในการจำค่า (จองพื้นที่ 512 bytes)
  EEPROM.begin(512);
  currentRelayState = EEPROM.read(0);
  
  // กรณีบอร์ดใหม่ ค่าเริ่มต้นใน EEPROM จะเป็น 255 (0xFF) ให้เซ็ตกลับเป็น 0
  if (currentRelayState == 0xFF) {
    currentRelayState = 0x00;
  }

  for (int i = 0; i < 8; i++) {
    // Preset output latch before enabling output; this cannot change boot straps.
    bool bitValue = (currentRelayState >> i) & 0x01;
    digitalWrite(relayPins[i], bitValue ? LOW : HIGH);
    pinMode(relayPins[i], OUTPUT);
  }
}

void loop() {
  if (Serial.available() >= 4) {
    if (Serial.peek() == STX) {
      Serial.read(); 
      byte data = Serial.read();
      byte checksum = Serial.read();
      byte stopByte = Serial.read();

      if (((data ^ checksum) == 0xFF) && (stopByte == ETX)) {
        if (data == 0x05) {
          sendFeedback(currentRelayState);
        } else {
          updateRelays(data);
          sendFeedback(data);
        }
      }
    } else {
      Serial.read(); 
    }
  }
}

void updateRelays(byte state) {
  currentRelayState = state;
  
  // บันทึกสถานะลง EEPROM ของ ESP8266 ที่ Address 0
  EEPROM.write(0, state);
  EEPROM.commit(); 
  
  for (int i = 0; i < 8; i++) {
    bool bitValue = (state >> i) & 0x01;
    digitalWrite(relayPins[i], bitValue ? LOW : HIGH);
  }
}

void sendFeedback(byte state) {
  byte chk = 0xFF ^ state; 
  byte frame[] = {STX, state, chk, ETX};
  Serial.write(frame, 4);
}
```

---
**Developer:** TOPTUBBY (Patiphan Phakdeeburi) | **Version:** 1.1.5.26

### 📜 License / ลิขสิทธิ์
This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.  
โปรเจคนี้อยู่ภายใต้ลิขสิทธิ์ **MIT License** - สามารถอ่านรายละเอียดเพิ่มเติมได้ที่ไฟล์ [LICENSE](LICENSE)
