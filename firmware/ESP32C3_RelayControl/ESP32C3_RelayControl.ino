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
