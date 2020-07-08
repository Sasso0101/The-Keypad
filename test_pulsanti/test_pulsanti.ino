#include <HID-Project.h>
#include <HID-Settings.h>
#include <WebUSB.h>
#include <Keypad.h>
#include <Tlc5940.h>

// Initialize webUSB
WebUSB WebUSBSerial(0, "localhost/");
#define Serial WebUSBSerial

const byte ROWS = 2;
const byte COLS = 5;
char keys[ROWS][COLS] = {
  {'0', '1', '2', '3', '4'},
  {'5', '6', '7', '8', '9'}
};
//Row pinouts of the keypad
byte rowPins[ROWS] = {2, 3};
//Column pinouts of the keypad
byte colPins[COLS] = {A0, 8, 7, 6, 4};

// Array which points to which pin i button is connected to TLC5940
int ledPin[10] = {7, 6, 5, 9, 8, 2, 1, 0, 15, 14};
bool serialInit = false;

Keypad kpd = Keypad( makeKeymap(keys), rowPins, colPins, ROWS, COLS );

void setup() {
  kpd.setHoldTime(500);

  Tlc.init();
  Tlc.clear();
  Tlc.update();

  Consumer.begin();
}

void loop() {
  if (Serial && !serialInit) {
    Serial.begin(9600);
    serialInit = true;
    // Tells the PC the key configuration
    Serial.write("init ");
    for (int row = 0; row < ROWS; row++) {
      for (int column = 0; column < COLS; column++) {
        Serial.write(keys[row][column]);
        Serial.write(" ");
      }
    }
    Serial.flush();
  } else if (!Serial && serialInit) {
    serialInit = false;
  }

  // Fills kpd.key[ ] array with up-to 10 active keys.
  // Returns true if there are ANY active keys.
  if (kpd.getKeys())
  {
    for (int i = 0; i < LIST_MAX; i++) // Scan the whole key list.
    {
      if ( kpd.key[i].stateChanged )   // Only find keys that have changed state.
      {
        switch (kpd.key[i].kstate) {  // Report active key state : IDLE, PRESSED, HOLD, or RELEASED
          case PRESSED:
            Keyboard.write(kpd.key[i].kchar);
            if (Serial) {
              Serial.write(kpd.key[i].kchar);
              Serial.write(" ");
              Serial.flush();
            }
            Tlc.set(ledPin[kpd.key[i].kchar - '0'], 4095);
            Tlc.update();
            break;
          case RELEASED:
            Tlc.set(ledPin[kpd.key[i].kchar - '0'], 0);
            Tlc.update();
        }
      }
    }
  }
}  // End loop
