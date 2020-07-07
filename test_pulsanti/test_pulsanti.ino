#include <WebUSB.h>
#include <Keyboard.h>
#include <Keypad.h>
#include "Tlc5940.h"

WebUSB WebUSBSerial(1 /* https:// */, "webusb.github.io/arduino/demos/console");
#define Serial WebUSBSerial

const byte ROWS = 2; //four rows
const byte COLS = 5; //three columns
char keys[ROWS][COLS] = {
  {'0','1','2','3','4'},
  {'5','6','7','8','9'}
};
byte rowPins[ROWS] = {2, 3}; //connect to the row pinouts of the keypad
byte colPins[COLS] = {A0, 8, 7, 6, 4}; //connect to the column pinouts of the keypad

int pins[10] = {7, 6, 5, 9, 8, 2, 1, 0, 15, 14};

Keypad kpd = Keypad( makeKeymap(keys), rowPins, colPins, ROWS, COLS );

void setup() {
    kpd.setHoldTime(500);
    
    Tlc.init();
    Tlc.clear();
    Tlc.update();
}

void loop() {
    if (Serial) {
      if (Serial.available() > 0) {
        String input = Serial.readString();
        Serial.write("ok");
        Serial.flush();
      }
      else {
        Serial.begin(9600);
      }
    }
    
    // Fills kpd.key[ ] array with up-to 10 active keys.
    // Returns true if there are ANY active keys.
    if (kpd.getKeys())
    {
        for (int i=0; i<LIST_MAX; i++)   // Scan the whole key list.
        {
            if ( kpd.key[i].stateChanged )   // Only find keys that have changed state.
            {
                switch (kpd.key[i].kstate) {  // Report active key state : IDLE, PRESSED, HOLD, or RELEASED
                    case PRESSED:
                    Keyboard.write(kpd.key[i].kchar);
                    /*Serial.print(kpd.key[i].kchar);
                    Serial.print(" ");*/
                    Tlc.set(pins[kpd.key[i].kchar - '0'], 4095);
                    Tlc.update();
                 break;
                  case RELEASED:
                    Tlc.set(pins[kpd.key[i].kchar - '0'], 0);
                    Tlc.update();
                }
            }
        }
    }
}  // End loop
