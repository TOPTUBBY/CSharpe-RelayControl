#include <Arduino.h>

// ESP8266 NodeMCU/D1 mini: CH1..CH8 follow D0..D7 board labels.
// D3/GPIO0 and D4/GPIO2 are boot pins: both MUST remain HIGH at reset.
// Relay inputs must not pull either pin LOW before setup() runs.
// D8/GPIO15 must remain LOW at boot; TX/RX GPIO1/3 are for USB Serial.
const int relayPins[] = {16, 5, 4, 0, 2, 14, 12, 13}; 
// GPIO0/D3 and GPIO2/D4 must be HIGH during reset: default to active-low
// hardware so they remain OFF during boot. For active-high simulation, set true;
// real active-high hardware needs an external enable gate for these two pins.
const bool RELAY_ACTIVE_HIGH = false;
const int RELAY_ON_LEVEL = RELAY_ACTIVE_HIGH ? HIGH : LOW;
const int RELAY_OFF_LEVEL = RELAY_ACTIVE_HIGH ? LOW : HIGH;
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
  // Power-up policy: ignore all previously saved states and start OFF.
  currentRelayState = 0x00;

  for (int i = 0; i < 8; i++) {
    // Set the OFF level before enabling output, regardless of module polarity.
    digitalWrite(relayPins[i], RELAY_OFF_LEVEL);
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
    digitalWrite(relayPins[i], bitValue ? RELAY_ON_LEVEL : RELAY_OFF_LEVEL);
  }
}

void sendFeedback(byte state) {
  byte chk = (byte)(0xFF ^ CMD_STATUS ^ state);
  byte frame[] = {STX, CMD_STATUS, state, chk, ETX};
  Serial.write(frame, sizeof(frame));
}
