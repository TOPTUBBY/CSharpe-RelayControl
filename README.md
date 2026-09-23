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

| GUI / บิตสถานะ | โมดูล | อินพุตบนโมดูล | GPIO ESP32 ตามโค้ดใน README | GPIO ESP32-C3 ตามโค้ดใน README | GPIO ESP8266 ตามโค้ดใน README |
| --- | --- | --- | --- | --- | --- |
| CH1 / bit 0 | A | IN1 | 14 | 0 | 16 (D0) |
| CH2 / bit 1 | A | IN2 | 27 | 1 | 5 (D1) |
| CH3 / bit 2 | A | IN3 | 26 | 2 ⚠ | 4 (D2) |
| CH4 / bit 3 | A | IN4 | 25 | 3 | 0 (D3) ⚠ |
| CH5 / bit 4 | B | IN1 | 33 | 4 | 2 (D4) ⚠ |
| CH6 / bit 5 | B | IN2 | 32 | 5 | 14 (D5) |
| CH7 / bit 6 | B | IN3 | 4 | 6 | 12 (D6) |
| CH8 / bit 7 | B | IN4 | 16 | 7 | 13 (D7) |

หมายเลข GPIO ในตารางมาจาก **โค้ดตัวอย่างใน README** เท่านั้น โค้ด ESP32 ที่คอมเมนต์ไว้ท้าย `Form1.cs` ใช้ขาคนละชุด; ตรวจว่าบอร์ดจริงแฟลชด้วยสเก็ตช์ใดและเดินสายตาม `relayPins[]` ของสเก็ตช์นั้นก่อนใช้งาน ⚠ GPIO2 บน ESP32-C3 และ GPIO0/2 บน ESP8266 เป็นขาที่มีผลต่อการบูต บอร์ดรีเลย์อาจดึงระดับไฟขณะเริ่มทำงาน; ดู [ข้อจำกัด GPIO ของ ESP32-C3](https://docs.espressif.com/projects/esp-idf/en/v4.4/esp32c3/api-reference/peripherals/gpio.html) และ pinout บอร์ดที่ใช้จริง

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
- โค้ด ESP32-C3 ใช้ `GPIO2` ซึ่งเป็น strapping pin; โค้ด ESP8266 ใช้ `GPIO0/2` ที่มีผลต่อการบูต ต้องทดสอบ power cycle พร้อมโมดูลจริง ESP32-C3 ที่ใช้ USB ภายในควรเปิด **USB CDC On Boot** ตาม [คำแนะนำ Espressif](https://docs.espressif.com/projects/arduino-esp32/en/latest/tutorials/cdc_dfu_flash.html); บอร์ดที่ใช้ USB-to-UART ภายนอกอาจมีขั้นตอนต่างกัน

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

The pictured assembly uses **two 4-channel modules** for eight outputs. Each pictured relay is marked `SRD-05VDC-SL-C`, indicating a **5 V coil**. “3.3 V module” may describe a control-input rating; the module supply voltage and input compatibility cannot be confirmed from this photo alone. Verify the board documentation or measure the circuit before applying power. See the [Thai wiring map](#ฮาร์ดแวร์ที่ใช้จริง-โมดูลรีเลย์-4-ช่อง--2) and [project BOM](BOM.md).

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

// ESP32-C3: ตรวจ GPIO2 (strapping pin) และผังขาของบอร์ดจริงก่อนต่อรีเลย์
const int relayPins[] = {0, 1, 2, 3, 4, 5, 6, 7};
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
    bool bitValue = (currentRelayState >> i) & 0x01;
    digitalWrite(relayPins[i], bitValue ? LOW : HIGH); 
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

// ESP8266: ใช้ขา D0 - D7 ตามลำดับ (ห้ามใช้ D8)
const int relayPins[] = {16, 5, 4, 0, 2, 14, 12, 13}; 
byte currentRelayState = 0x00; 

const byte STX = 0x02;
const byte ETX = 0x03;

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
    pinMode(relayPins[i], OUTPUT);
    bool bitValue = (currentRelayState >> i) & 0x01;
    digitalWrite(relayPins[i], bitValue ? LOW : HIGH); 
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
