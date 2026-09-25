#include <Arduino.h>
#include <driver/gpio.h>

// ESP32-C3 SuperMini: CH1..CH7 = GPIO0,1,3,4,5,6,7; CH8 = GPIO10.
// Skip boot strapping GPIO2/8/9. Verify your board's pin labels.
const int relayPins[] = {0, 1, 3, 4, 5, 6, 7, 10};
// The physical relay board is active-low. For the Wokwi LED1 simulation,
// set RELAY_ACTIVE_HIGH=1 in the build flags (or change the default below).
#ifndef RELAY_ACTIVE_HIGH
#define RELAY_ACTIVE_HIGH 0
#endif
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
  // Power-up policy: always start with all relay channels OFF.
  currentRelayState = 0x00;

  for (int i = 0; i < 8; i++) {
    // Preload the OFF level before enabling the output driver. Arduino's
    // digitalWrite() is documented for pins already configured as OUTPUT.
    const gpio_num_t pin = static_cast<gpio_num_t>(relayPins[i]);
    gpio_set_level(pin, RELAY_OFF_LEVEL);
    gpio_set_direction(pin, GPIO_MODE_OUTPUT);
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
    // setup() configured this pin through ESP-IDF, so keep writes on that API.
    gpio_set_level(static_cast<gpio_num_t>(relayPins[i]),
                   bitValue ? RELAY_ON_LEVEL : RELAY_OFF_LEVEL);
  }
}

void sendFeedback(byte state) {
  byte chk = (byte)(0xFF ^ CMD_STATUS ^ state);
  byte frame[] = {STX, CMD_STATUS, state, chk, ETX};
  Serial.write(frame, sizeof(frame));
}
