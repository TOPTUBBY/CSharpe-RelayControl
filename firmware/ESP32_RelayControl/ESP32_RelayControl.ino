#include <Arduino.h>

// ESP32 (original module): wire CH1..CH8 to these pins in order.
// Verify the pinout of your actual ESP32 board before wiring.
const int relayPins[] = {14, 27, 26, 25, 33, 32, 4, 16};
byte currentRelayState = 0x00; 

const byte STX = 0x02;
const byte ETX = 0x03;
const byte CMD_SET = 0x01;
const byte CMD_GET = 0x02;
const byte CMD_STATUS = 0x81;
byte rxFrame[5];
byte rxCount = 0;

void updateRelays(byte state);
void sendFeedback(byte state);

void setup() {
  // Power-up policy: always start with all relay channels OFF.
  currentRelayState = 0x00;

  for (int i = 0; i < 8; i++) {
    // Active-low module: HIGH = OFF. Set latch before enabling output.
    digitalWrite(relayPins[i], HIGH);
    pinMode(relayPins[i], OUTPUT);
  }
  Serial.begin(115200);
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
      // Keep a possible STX from the malformed frame for the next read.
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
