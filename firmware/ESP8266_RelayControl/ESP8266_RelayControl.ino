#include <EEPROM.h>

// ESP8266 NodeMCU/D1 mini: CH1..CH8 follow D0..D7 board labels.
// D3/GPIO0 and D4/GPIO2 are boot pins: both MUST remain HIGH at reset.
// Relay inputs must not pull either pin LOW before setup() runs.
// D8/GPIO15 must remain LOW at boot; TX/RX GPIO1/3 are for USB Serial.
const int relayPins[] = {16, 5, 4, 0, 2, 14, 12, 13}; 
byte currentRelayState = 0x00; 

const byte STX = 0x02;
const byte ETX = 0x03;
const byte CMD_SET = 0x01;
const byte CMD_GET = 0x02;
const byte CMD_STATUS = 0x81;
const byte EEPROM_MARKER = 0xA5;
byte rxFrame[5];
byte rxCount = 0;

void updateRelays(byte state);
void sendFeedback(byte state);

void setup() {
  Serial.begin(115200);
  
  // ESP8266 ใช้ EEPROM ในการจำค่า (จองพื้นที่ 512 bytes)
  EEPROM.begin(512);
  byte savedState = EEPROM.read(0);
  // Address 1 distinguishes erased EEPROM from a valid ALL ON (0xFF) state.
  // Preserve previously saved states other than 0xFF during the upgrade.
  if (EEPROM.read(1) == EEPROM_MARKER || savedState != 0xFF)
    currentRelayState = savedState;

  for (int i = 0; i < 8; i++) {
    // Preset output latch before enabling output; this cannot change boot straps.
    bool bitValue = (currentRelayState >> i) & 0x01;
    digitalWrite(relayPins[i], bitValue ? LOW : HIGH);
    pinMode(relayPins[i], OUTPUT);
  }
}

void loop() {
  while (Serial.available() > 0) {
    byte incoming = (byte)Serial.read();
    if (rxCount == 0 && incoming != STX) continue;
    rxFrame[rxCount++] = incoming;
    if (rxCount < sizeof(rxFrame)) continue;

    byte cmd = rxFrame[1];
    byte data = rxFrame[2];
    if (rxFrame[4] == ETX && rxFrame[3] == (byte)(0xFF ^ cmd ^ data)) {
      if (cmd == CMD_SET) {
        updateRelays(data); // 0x05 now means CH1 + CH3 ON.
        sendFeedback(currentRelayState);
      } else if (cmd == CMD_GET && data == 0x00) {
        sendFeedback(currentRelayState);
      }
      rxCount = 0;
    } else {
      for (byte i = 1; i < sizeof(rxFrame); ++i) rxFrame[i - 1] = rxFrame[i];
      rxCount = sizeof(rxFrame) - 1;
      while (rxCount > 0 && rxFrame[0] != STX) {
        for (byte i = 1; i < rxCount; ++i) rxFrame[i - 1] = rxFrame[i];
        --rxCount;
      }
    }
  }
}

void updateRelays(byte state) {
  currentRelayState = state;
  
  // บันทึกสถานะลง EEPROM ของ ESP8266 ที่ Address 0
  EEPROM.write(0, state);
  EEPROM.write(1, EEPROM_MARKER);
  EEPROM.commit(); 
  
  for (int i = 0; i < 8; i++) {
    bool bitValue = (state >> i) & 0x01;
    digitalWrite(relayPins[i], bitValue ? LOW : HIGH);
  }
}

void sendFeedback(byte state) {
  byte chk = (byte)(0xFF ^ CMD_STATUS ^ state);
  byte frame[] = {STX, CMD_STATUS, state, chk, ETX};
  Serial.write(frame, sizeof(frame));
}
