# CSharpe-RelayControl

<p align="left">
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C#" />
  <img src="https://img.shields.io/badge/.NET_4.5-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET" />
  <img src="https://img.shields.io/badge/Arduino-00979D?style=for-the-badge&logo=arduino&logoColor=white" alt="Arduino" />
  <img src="https://img.shields.io/badge/Version-2.0.9.2026-blue?style=for-the-badge" alt="Version" />
  <img src="https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge" alt="License" />
</p>

![หน้าตา Relay Control 8CH รุ่น 2.0.9.2026](RelayControl/RelayControl/screenshot.png)

ภาพตัวอย่างโปรแกรมเวอร์ชัน `2.0.9.2026` จาก GUI จริง (ภาพขณะยังไม่ได้เชื่อมต่อกับบอร์ด)

[🇹🇭 ภาษาไทย](#ภาษาไทย) | [🇬🇧 English](#english)

---

## 🇹🇭 ภาษาไทย

**CSharpe-RelayControl** เป็นโปรแกรมประยุกต์บน Windows (Windows Forms) ที่พัฒนาด้วย C# (.NET Framework 4.5) สำหรับควบคุมรีเลย์ 8 ช่อง (8-Channel Relay Module) ที่เชื่อมต่อกับไมโครคอนโทรลเลอร์ ESP32 ผ่านการสื่อสารทาง Serial Port (USB)

โปรแกรมนี้ส่งคำสั่งเปิด-ปิดรีเลย์ผ่าน ESP32 และอ่านสถานะที่เฟิร์มแวร์ส่งกลับเพื่ออัปเดต GUI โดยสถานะที่ส่งกลับเป็นค่าคำสั่งล่าสุด ไม่ใช่การวัดหน้าสัมผัสรีเลย์จริง โดยโปรโตคอล 5 ไบต์รองรับ bitmask ทุกค่า รวมถึง `0x05` (CH1+CH3)

### คุณสมบัติหลัก
*   **ควบคุมรีเลย์ 8 ช่อง:** คลิกปุ่ม CH1–CH8 เพื่อเปิด/ปิดแต่ละช่อง; สีเขียวหมายถึง ON
*   **ควบคุมพร้อมกัน:** ปุ่ม **ALL ON** และ **ALL OFF** สั่งทั้ง 8 ช่องในครั้งเดียว
*   **การเชื่อมต่อ Serial Port:** เชื่อมต่อผ่าน USB Serial (Baudrate: 115200); ปุ่ม **Refresh** โหลดรายชื่อพอร์ตใหม่เมื่อถอด/ต่อบอร์ด
*   **Communication Log:** แสดงคำสั่งที่ส่งและสถานะที่ได้รับ พร้อมเวลาและข้อมูลเฟรม 5 ไบต์ เมื่อมีการสื่อสารกับบอร์ด
*   **Status Feedback:** โปรแกรมแสดงค่าที่ ESP ส่งกลับเมื่อเชื่อมต่อหรือเปลี่ยนสถานะ โดยทุกครั้งที่บูตจะเริ่มจาก OFF ทั้ง 8 ช่อง; ไม่ได้ตรวจหน้าสัมผัสจริง
*   **โปรโตคอลแบบมี checksum:** ใช้โครงสร้างข้อมูลแบบ Frame `[STX] [CMD] [DATA] [CHECKSUM] [ETX]` เพื่อป้องกันข้อมูลผิดพลาด

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

ตารางตรงกับ `relayPins[]` ของสเก็ตช์แต่ละรุ่นด้านล่าง โดย CH1–CH4 ต่อ IN1–IN4 ของโมดูล A และ CH5–CH8 ต่อ IN1–IN4 ของโมดูล B; ตรวจสเก็ตช์ที่แฟลชจริงและป้ายขาของบอร์ดก่อนต่อสาย

- **ESP32-C3 SuperMini:** เดินสาย CH1–CH7 ตาม GPIO `0, 1, 3, 4, 5, 6, 7` บนฝั่งหนึ่ง โดยข้าม GPIO2 (strapping); CH8 ต่อ GPIO10 อีกฝั่ง ไม่ใช้ GPIO8/9 (strapping) หรือ GPIO18/19 (USB) ตรวจตำแหน่ง GPIO บนบอร์ด SuperMini รุ่นจริงก่อนเสียบสาย
- **ESP8266:** ตารางและสเก็ตช์เรียง CH1–CH8 ตามป้าย `D0–D7`; เดินสายไปตามลำดับขาจริงเท่าที่บอร์ดจัดไว้ แล้วข้ามขาไฟเลี้ยง/กราวด์หรือย้ายไปอีกฝั่งเมื่อจำเป็น ตำแหน่งขา NodeMCU และ D1 mini ต่างกัน ให้ยึด **ป้าย D0–D7 บนบอร์ดจริง** กับตารางนี้ GPIO0/D3 (CH4) และ GPIO2/D4 (CH5) เป็นสองขา ⚠ ที่เลี่ยงไม่ได้เมื่อขับรีเลย์ 8 ช่องตรงและคง USB Serial อย่าใช้ GPIO15/D8 ซึ่งต้อง LOW ตอนบูต หรือ GPIO1/3 ที่ใช้ Serial กับ GUI
- **สำคัญสำหรับ D3/D4:** GPIO0/2 ของ ESP8266 ต้อง HIGH ระหว่าง reset เพื่อบูตได้ สเก็ตช์ ESP8266 จึงเริ่มที่ `RELAY_ACTIVE_HIGH = false` เพื่อให้ HIGH เป็น OFF ขณะบูต; หากเปลี่ยนไปใช้โมดูล active-high สองช่องนี้อาจ ON ชั่วคราวก่อน `setup()` ตั้งค่า OFF; ซอฟต์แวร์รับประกัน OFF ตลอดช่วงบูตไม่ได้ ต้องใช้วงจร enable/interlock ภายนอก หรือย้ายไปบอร์ด/ตัวขยาย GPIO ที่มีขาปลอดข้อจำกัด ส่วนโมดูล active-low สามารถตั้ง `RELAY_ACTIVE_HIGH = false` แต่ยังต้องยืนยันว่าอินพุตไม่ดึงขาบูต LOW และไม่ป้อนไฟเกิน 3.3 V ดู [Espressif: ESP32-C3 GPIO](https://docs.espressif.com/projects/esp-idf/en/latest/esp32c3/api-reference/peripherals/gpio.html) และ [ESP8266 boot mode](https://docs.espressif.com/projects/esptool/en/latest/esp8266/advanced-topics/boot-mode-selection.html)

- ต่อ GND ของ ESP กับ GND ของโมดูล A/B ร่วมกัน **เมื่อใช้อินพุตควบคุมร่วมกัน**; ต่อ `IN1..IN4` ตามตาราง ตรวจตำแหน่ง `VCC/GND/IN` ที่พิมพ์บนบอร์ดจริงก่อนเสียบสาย
- กำหนดไฟเลี้ยง `VCC` จากสเปกโมดูลที่ซื้อและผลวัด ไม่ต่อ 5 V เข้าขา GPIO/3V3 ของ ESP โดยตรง หากโมดูลรับอินพุต 3.3 V ไม่แน่นอนหรือมี pull-up ไป 5 V ให้ใช้วงจรขับ/level interface ที่ระบุสเปกชัดเจน
- คอยล์ Songle 5 V รุ่นนี้กินประมาณ 71.4 mA/ตัว; 8 ตัวพร้อมกันเป็นประมาณ 0.57 A **เฉพาะคอยล์** (ยังไม่รวม LED/วงจรขับ/ESP) จึงควรเริ่มออกแบบด้วยแหล่งจ่าย 5 V ที่มีกระแสสำรอง เช่น 2 A **หากบอร์ดโมดูลระบุว่าใช้ไฟเลี้ยง 5 V** แล้ววัดกระแสจริงขณะทุกช่อง ON อย่าดึงกระแสคอยล์จาก GPIO หรือขา 3V3 ของบอร์ด ESP
- ขั้วโหลดแต่ละช่องเป็น `COM/NO/NC`: ตรวจ silk screen และวัด continuity เพื่อยืนยันตำแหน่งก่อนต่อโหลด `COM–NO` เหมาะเมื่ออยากให้หน้าสัมผัสเปิดตอนคอยล์ไม่มีไฟ ค่า 10 A ที่พิมพ์บนรีเลย์เป็นพิกัด **ชิ้นส่วน** ไม่ใช่การรับรองทั้งโมดูล/สาย/ขั้วต่อสำหรับโหลดทุกชนิด
- **ESP32-C3 กับโมดูลจริงของโปรเจกต์นี้:** ค่าเริ่มต้น `RELAY_ACTIVE_HIGH=0` (active-low): ON → LOW, OFF → HIGH; บูตเข้า `setup()` แล้วสั่ง OFF ทุกช่องแม้ยังไม่เชื่อมต่อ GUI สเก็ตช์ ESP32-C3 ใช้ `gpio_set_level()` ทั้งตอนเริ่มระบบและทุกคำสั่ง ON/OFF ให้ตรงกับขาที่ตั้งผ่าน ESP-IDF (Arduino-ESP32 รุ่นใหม่จะไม่สั่งขานี้ด้วย `digitalWrite()` หากไม่ได้ตั้งผ่าน `pinMode()`) สำหรับ Wokwi ที่ไฟ LED1 ติดเมื่อ `IN` เป็น HIGH ใช้ไฟล์จำลองแยก `firmware/ESP32C3_Wokwi/src/main.cpp` ซึ่งกำหนด active-high ไว้แล้ว ESP32 รุ่นปกติยังใช้ `true` ตาม Wokwi; ESP8266 ใช้ `false` ตามข้อจำกัดขาบูต
- **OFF ตั้งแต่เริ่มจ่ายไฟสำหรับ ESP32-C3 จริง:** สเก็ตช์ตั้งระดับ HIGH (OFF) ก่อนเปิดขาเป็น output ทันทีเมื่อเข้า `setup()` แต่ก่อน `setup()` รัน ขา ESP ยังควบคุมไม่ได้ด้วยโค้ด หากรีเลย์ติดช่วงนี้ ให้ทดลองปิด GUI แล้วจ่ายไฟใหม่: ถ้ารีเลย์ติดเพียงช่วงสั้น ๆ แล้วดับเอง เป็นปัญหาช่วงบูต; ถ้าติดค้าง ให้ตรวจว่าแฟลชสเก็ตช์ ESP32-C3 รุ่นล่าสุด, ค่า `RELAY_ACTIVE_HIGH=0`, โมดูลรับ 3.3 V ได้จริง และ ESP บูตสำเร็จ วัดแรงดันที่ `IN` เทียบกับ GND ของโมดูลเมื่อหยุดนิ่ง: ปกติควรใกล้ 3.3 V (OFF); ระหว่างสั่ง ON ควรใกล้ 0 V ตรวจที่หน้าสัมผัส `COM–NO` ด้วย เพราะไฟแสดงผลอย่างเดียวอาจไม่บอกสถานะหน้าสัมผัส
- **ถ้าต้องการไม่ให้คอยล์ติดแม้ชั่วขณะก่อนโค้ดรัน:** ออกแบบวงจรให้รีเลย์ถูก disable โดยปริยายจนกว่า ESP จะพร้อม เช่น วงจร enable/driver ที่มี pull ค้างสถานะ OFF หรือวงจรตัดไฟเลี้ยงคอยล์ที่เริ่มปิดและเปิดหลังตั้ง GPIO ครบ 8 ช่อง ตรวจแรงดันขา IN ของโมดูล, ลำดับการจ่ายไฟ ESP/รีเลย์ และกระแสคอยล์รวมก่อนเลือกอุปกรณ์ อย่าต่อ pull-up ไป 5 V เข้าขา ESP32-C3 โดยตรง และไม่ควรใช้ตัวต้านทาน pull-up อย่างเดียวเป็นหลักประกันหากแหล่งจ่ายรีเลย์มาก่อน 3.3 V ของ ESP
- รายการชิ้นส่วนและเงื่อนไขการเลือกซื้ออยู่ใน [BOM ของโปรเจกต์](BOM.md)

### ข้อจำกัดที่ควรทราบของโค้ดเวอร์ชันนี้

- GUI รับ **สถานะที่ ESP ส่งกลับตามคำสั่ง** ไม่ได้วัดหน้าสัมผัสรีเลย์หรือตรวจว่าโหลดทำงานจริง
- โปรโตคอลปัจจุบันแยก `CMD_SET` และ `CMD_GET` ออกจาก `DATA`: สั่ง CH1+CH3 ON เท่านั้นได้ด้วย `DATA=0x05`; ต้องอัปเดต GUI และเฟิร์มแวร์พร้อมกัน เพราะเฟรม 4 ไบต์รุ่นเก่าใช้ร่วมกับเฟรม 5 ไบต์ไม่ได้
- เฟิร์มแวร์ ESP32, ESP32-C3 และ ESP8266 ไม่อ่าน/บันทึกสถานะรีเลย์ใน Flash/EEPROM อีกต่อไป เมื่อไฟกลับหรือรีเซ็ตจะเริ่ม OFF ทั้งหมด; หากใช้ขั้ว NC ของโหลด ต้องคำนึงว่าหน้าสัมผัส NC ต่ออยู่เมื่อคอยล์ OFF
- ESP32-C3 เลี่ยง GPIO2/8/9 แล้ว; ESP8266 ยังใช้ GPIO0/2 เป็น CH4/CH5 ซึ่งต้อง HIGH ระหว่าง reset และอาจทำให้รีเลย์กระตุกตอนบูต ควรทดสอบ cold boot, reset และ flash พร้อมต่อโมดูลจริงด้วยโหลดแรงดันต่ำ ESP32-C3 ที่ใช้ USB ภายในควรเปิด **USB CDC On Boot** ตาม [คำแนะนำ Espressif](https://docs.espressif.com/projects/arduino-esp32/en/latest/tutorials/cdc_dfu_flash.html)

### การใช้งาน
1. อัปโหลดโค้ด Arduino ลงในบอร์ด ESP32 (ดูโค้ดด้านล่าง)
2. ต่อสายรีเลย์เข้ากับขา GPIO ของ ESP32 ตามที่กำหนดในโค้ด
3. เปิดโปรแกรม `RelayControlApp` จากไฟล์ที่ build ใน `RelayControl/RelayControl.sln`
4. เลือกพอร์ต COM ที่เชื่อมต่อกับ ESP32; ถ้าเพิ่งเสียบบอร์ด ให้กด **Refresh** ก่อนเลือกพอร์ต
5. กด **Connect** แล้วรอสถานะตอบกลับจากบอร์ดใน **Communication Log**
6. คลิก CH1–CH8 เพื่อสั่งแต่ละช่อง หรือกด **ALL ON / ALL OFF** เพื่อสั่งทุกช่อง
7. หากถอดและเสียบบอร์ดใหม่ ให้กด **Refresh** แล้วเชื่อมต่อกับพอร์ตที่ปรากฏอีกครั้ง (การ Refresh ขณะเชื่อมต่อจะตัดการเชื่อมต่อก่อน)

---

## 🇬🇧 English

**CSharpe-RelayControl** is a Windows Forms application developed in C# (.NET Framework 4.5) for controlling an 8-Channel Relay Module connected to an ESP32 microcontroller via Serial Port (USB).

The application sends ON/OFF commands to the ESP32 and displays its reported state. The response reflects the last commanded bitmask, not measured relay contacts. The five-byte protocol supports all bitmasks, including 0x05 for CH1+CH3.

### Key Features
*   **8-Channel Relay Control:** Click CH1–CH8 to toggle individual channels; green indicates ON.
*   **Bulk controls:** **ALL ON** and **ALL OFF** set all eight channels in one command.
*   **Serial Communication:** USB Serial at 115200 baud; **Refresh** rescans ports when a board is unplugged or reconnected.
*   **Communication Log:** Timestamped sent commands and received status frames while communicating with the board.
*   **Status Feedback:** Displays the ESP32-reported bitmask; firmware starts with all eight channels OFF after every power cycle or reset. It does not sense physical contacts.
*   **Framed protocol with XOR checksum:** Uses a framed data structure `[STX] [CMD] [DATA] [CHECKSUM] [ETX]` to prevent data corruption.

### Actual relay hardware

The pictured assembly uses **two 4-channel modules** for eight outputs. On ESP32-C3 SuperMini, CH1–CH7 use GPIO0,1,3–7 on one side and CH8 uses GPIO10 on the other side, skipping boot pin GPIO2. On ESP8266 NodeMCU/D1 mini, CH1–CH8 follow D0–D7 in board-label order; D3/GPIO0 and D4/GPIO2 remain boot-sensitive and must stay HIGH during reset. Test cold starts with the actual relay module. Each pictured relay is marked `SRD-05VDC-SL-C`, indicating a **5 V coil**. “3.3 V module” may describe a control-input rating; the module supply voltage and input compatibility cannot be confirmed from this photo alone. Verify the board documentation or measure the circuit before applying power. See the [Thai wiring map](#ฮาร์ดแวร์ที่ใช้จริง-โมดูลรีเลย์-4-ช่อง--2) and [project BOM](BOM.md).

The GUI displays ESP-reported state, not measured contact state. This version uses separate command bytes for setting relays and requesting status. After power returns or the board resets, firmware starts with all eight channels OFF and does not restore an earlier ON state. Check startup and contact behavior with a low-voltage test load before deploying.

### How to Use
1. Upload the provided Arduino code to your ESP32 board.
2. Connect the relay module to the ESP32 GPIO pins as defined in the code.
3. Launch `RelayControlApp` built from `RelayControl/RelayControl.sln`.
4. Select the board's COM port; click **Refresh** first if you just reconnected it.
5. Click **Connect** and check **Communication Log** for the board's status response.
6. Click CH1–CH8 for individual channels, or **ALL ON / ALL OFF** for all channels.
7. After unplugging and reconnecting the board, click **Refresh** and connect again; refreshing disconnects an active connection.

---

## 📡 Communication Protocol (current 5-byte version)

**Serial:** 115200 baud, 8N1. Every frame is `[STX=02] [CMD] [DATA] [CHECKSUM] [ETX=03]`, where `CHECKSUM = FF XOR CMD XOR DATA` (one byte). Bit 0 of DATA controls CH1; bit 7 controls CH8. ESP32-C3 ships as two separate files: active-low for the physical Arduino IDE target and active-high for Wokwi. ESP32 defaults to active-high and ESP8266 defaults to active-low. The checksum detects accidental corruption; it does not authenticate commands.

| CMD | Direction | Meaning | DATA |
| --- | --- | --- | --- |
| `01` SET | GUI → ESP | Set all eight channel bits at once | `00`–`FF` relay mask |
| `02` GET | GUI → ESP | Request current status without changing outputs | Must be `00` |
| `81` STATUS | ESP → GUI | Echo current commanded relay mask | `00`–`FF` |

| Action | Frame (hex) | Response (hex) |
| --- | --- | --- |
| All OFF | `02 01 00 FE 03` | `02 81 00 7E 03` |
| CH1 ON only | `02 01 01 FF 03` | `02 81 01 7F 03` |
| CH1 + CH3 ON only | `02 01 05 FB 03` | `02 81 05 7B 03` |
| All ON | `02 01 FF 01 03` | `02 81 FF 81 03` |
| Status request | `02 02 00 FD 03` | `02 81 xx (7E XOR xx) 03` |

The GUI reads STATUS feedback, updates checkboxes and writes the transmitted/received frames to Communication Log. Feedback reports the firmware's commanded state, **not** measured relay contacts. Invalid checksum, unknown commands, or a GET with nonzero DATA are ignored by the firmware. Frames may arrive in pieces over serial; both sides accumulate bytes before parsing and resynchronize after malformed frames.

**Upgrade together:** the previous four-byte frame `[02] [DATA] [FF XOR DATA] [03]` is incompatible. Upload the new sketch and run the updated GUI from the same branch. For Wokwi, use the new sketch with the same 115200 8N1 serial bridge; an older simulator sketch will not answer these frames.

**Checking the running GUI:** on startup, Communication Log shows `GUI protocol v2 (5 bytes). Running: <full path to RelayControl.exe>`. A connection status request then logs `Command Sent (5 bytes): 02 02 00 FD 03`. If you see a four-byte command, the running executable is an older build: close all RelayControl processes, open `RelayControl/RelayControl.sln`, choose the desired branch, rebuild and start with **F5**. Check the executable path printed in the log. Generated `bin/` and `obj/` files are no longer stored in this repository. A firmware reply of four bytes means the controller or Wokwi sketch still uses the old protocol; upload the matching sketch above.

**Power-up behavior:** all three sketches ignore previously saved states and start with the commanded state OFF. ESP32-C3 preloads `RELAY_OFF_LEVEL` with the ESP-IDF GPIO API before enabling each output and also uses that API for every subsequent relay change; mixing ESP-IDF pin setup with Arduino `digitalWrite()` can leave the physical relay unchanged while STATUS still updates. With active-high selected, OFF is LOW; with active-low selected, OFF is HIGH. ESP32-C3 and ESP8266 default to active-low. This takes effect once `setup()` runs, without waiting for a GUI connection; software cannot control GPIO during reset/boot. For guaranteed OFF before `setup()`, use a normally-disabled relay enable/driver or coil-supply gate sized for the actual module, then enable it only after the GPIOs are initialized. Pull-ups alone may not suffice if relay power rises before the ESP's 3.3 V rail, and GPIOs must never be exposed directly to 5 V. ESP8266 D3/D4 must stay HIGH during boot. Verify `COM–NO` and `COM–NC` contact behavior separately from the Wokwi LED indicator.

---

## 💻 Arduino firmware

Use the matching sketch for your board; the complete code lives in the files below so README examples cannot drift from the firmware:

- [ESP32 (original board)](firmware/ESP32_RelayControl/ESP32_RelayControl.ino): pins `14, 27, 26, 25, 33, 32, 4, 16` for CH1–CH8.
- [ESP32-C3 real relay / Arduino IDE](firmware/ESP32C3_RelayControl/ESP32C3_RelayControl.ino): active-low; pins `0, 1, 3, 4, 5, 6, 7, 10`; enable **USB CDC On Boot** when required by the board setup.
- [ESP32-C3 simulation / Wokwi PlatformIO](firmware/ESP32C3_Wokwi/src/main.cpp): active-high for the Wokwi LED1 behavior; same pins, channel order and protocol.
- [ESP8266 NodeMCU/D1 mini](firmware/ESP8266_RelayControl/ESP8266_RelayControl.ino): board pins D0–D7 in channel order; check D3/D4 boot levels described above.

**Arduino IDE / ESP32-C3 จริง:** เปิดไฟล์ `firmware/ESP32C3_RelayControl/ESP32C3_RelayControl.ino` ใน Arduino IDE โดยตรง (อย่าใช้ไฟล์ ESP32 รุ่นปกติหรือสำเนาเก่าจาก Wokwi) สเก็ตช์นี้กำหนด `RELAY_ACTIVE_HIGH = false` สำหรับโมดูลจริงอยู่แล้ว เลือกบอร์ด ESP32-C3 และพอร์ตของบอร์ดจริง แล้วกด Upload; หากใช้ USB ภายในให้เปิด USB CDC On Boot ตามการตั้งค่าบอร์ด `driver/gpio.h` มากับ ESP32 Arduino core และไม่ต้องติดตั้งไลบรารีแยก หลังอัปโหลดให้ทดลอง ON/OFF ทีละช่องกับโมดูลจริงและตรวจ `COM–NO` ด้วย

All firmware versions implement the five-byte protocol above. The GUI code is in [`Form1.cs`](RelayControl/RelayControl/Form1.cs). The current `main` GUI includes ALL ON, ALL OFF, serial-port Refresh, and the Communication Log; its Windows Forms layout is in [`Form1.Designer.cs`](RelayControl/RelayControl/Form1.Designer.cs).

**Relay polarity:** the two ESP32-C3 files are ready to use without editing settings. The Arduino IDE sketch has `RELAY_ACTIVE_HIGH = false`: status `00` drives all IN pins HIGH and status `FF` drives them LOW. The Wokwi `src/main.cpp` has `RELAY_ACTIVE_HIGH = true`: status `00` drives LOW and status `FF` drives HIGH. The original ESP32 sketch defaults to `true`; ESP8266 defaults to `false`. Check the real relay contacts before attaching loads. Both ESP32-C3 files use the same five-byte protocol and channel mapping.

### PlatformIO / Wokwi (`src/main.cpp`)

Copy [the Wokwi-specific ESP32-C3 file](firmware/ESP32C3_Wokwi/src/main.cpp) to **your Wokwi project's** `src/main.cpp`, replacing its previous contents. Use `framework = arduino` and your ESP32-C3 board ID in that project's `platformio.ini`, then rebuild the simulation. Do not add `-DRELAY_ACTIVE_HIGH` or change the polarity: the Wokwi file already sets `true`. Keep only one `setup()` and `loop()` pair in the Wokwi project. For the real board, open and upload the **separate** [Arduino IDE `.ino` file](firmware/ESP32C3_RelayControl/ESP32C3_RelayControl.ino); it already sets `false`. Never copy the Wokwi `main.cpp` to the physical board.

---
**Developer:** TOPTUBBY (Patiphan Phakdeeburi) | **Version:** 2.0.9.2026

### 📜 License / ลิขสิทธิ์
This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.  
โปรเจคนี้อยู่ภายใต้ลิขสิทธิ์ **MIT License** - สามารถอ่านรายละเอียดเพิ่มเติมได้ที่ไฟล์ [LICENSE](LICENSE)
