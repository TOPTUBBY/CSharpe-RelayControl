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
