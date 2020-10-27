#include <HID-Project.h>
#include <HID-Settings.h>
#include <WebUSB.h>
#include <Keypad.h>
#include <Tlc5940.h>
#include <EEPROM.h>

// Initialize webUSB
WebUSB WebUSBSerial(0, "localhost/");
// #define Serial WebUSBSerial

const byte ROWS = 2;
const byte COLS = 5;
const byte SPACING = 5;
char keysID[ROWS][COLS] = {
  {'0', '1', '2', '3', '4'},
  {'5', '6', '7', '8', '9'}
};

typedef enum : byte {FN, MEDIA, YOUTUBE, ATEM, LETTER} keyType;

//Row pinouts of the keypad
byte rowPins[ROWS] = {2, 3};
//Column pinouts of the keypad
byte colPins[COLS] = {A0, 8, 7, 6, 4};

// Array which points to which pin i button is connected to TLC5940
int ledPin[10] = {7, 6, 5, 9, 8, 2, 1, 0, 15, 14};
bool serialInit = false;

Keypad kpd = Keypad( makeKeymap(keysID), rowPins, colPins, ROWS, COLS );

void setup() {
  kpd.setHoldTime(600);

  Tlc.init();
  Tlc.clear();
  Tlc.update();

  Consumer.begin();
  Keyboard.begin();
}

void loop() {
  if (WebUSBSerial) {
    if (serialInit == false) {
      WebUSBSerial.begin(9600);
      Serial.begin(9600);
      serialInit = true;
    }
    else if (WebUSBSerial.available()) {
      String incomingMessage = WebUSBSerial.readStringUntil(' ');
      if (incomingMessage == "sendInit") {
        // Tells the PC the key configuration
        WebUSBSerial.write("init ");
        for (int i = 0; i < 10; i++) {
          int keyType = EEPROM.read(i * SPACING);
          int value = EEPROM.read(i * SPACING + 1);
          WebUSBSerial.print(value);
          WebUSBSerial.write(",");
          WebUSBSerial.print(keyType);
          if (keyType == LETTER) {
            WebUSBSerial.write(",");
            int ctrl = EEPROM.read(i * SPACING + 2);
            WebUSBSerial.print(ctrl);
            WebUSBSerial.write(",");
            int alt = EEPROM.read(i * SPACING + 3);
            WebUSBSerial.print(alt);
            WebUSBSerial.write(",");
            int shift = EEPROM.read(i * SPACING + 4);
            WebUSBSerial.print(shift);
          }
          WebUSBSerial.write(" ");
        }
        WebUSBSerial.flush();
      }
      else {
        if (incomingMessage == "changeKey") {
          int keyID = WebUSBSerial.readStringUntil(' ').toInt();
          int keyType = WebUSBSerial.readStringUntil(' ').toInt();
          int value = WebUSBSerial.readStringUntil(' ').toInt();
          // Ctrl/alt/shift
          if (keyType == LETTER) {
            int ctrl = WebUSBSerial.readStringUntil(' ').toInt();
            int alt = WebUSBSerial.readStringUntil(' ').toInt();
            int shift = WebUSBSerial.readStringUntil(' ').toInt();
            EEPROM.update(keyID * SPACING + 2, ctrl);
            EEPROM.update(keyID * SPACING + 3, alt);
            EEPROM.update(keyID * SPACING + 4, shift);
          }
          EEPROM.update(keyID * SPACING, keyType);
          EEPROM.update(keyID * SPACING + 1, value);
        }
      }
    }
  } else if (!WebUSBSerial) {
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
            sendKey(kpd.key[i].kchar - '0');

            if (WebUSBSerial) {
              WebUSBSerial.print("keyPressed ");
              WebUSBSerial.print(kpd.key[i].kchar - '0');
              WebUSBSerial.flush();
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

// Send a keystroke
void sendKey(int i) {
  keyType type = EEPROM.read(i * SPACING);
  if (type == MEDIA) {
    Consumer.write(EEPROM.read(i * SPACING + 1));
  } else if (type == LETTER) {
    if (EEPROM.read(i * SPACING + 2) == 1) {
      Keyboard.press(KEY_LEFT_CTRL);
    }
    if (EEPROM.read(i * SPACING + 3) == 1) {
      Keyboard.press(KEY_LEFT_ALT);
    }
    if (EEPROM.read(i * SPACING + 4) == 1) {
      Keyboard.press(KEY_LEFT_SHIFT);
    }
    Keyboard.press(KeyboardKeycode(EEPROM.read(i * SPACING + 1)));
    Keyboard.releaseAll();
  } else if (type == FN || type == YOUTUBE) {
    Keyboard.write(KeyboardKeycode(EEPROM.read(i * SPACING + 1)));
  } else if (type == ATEM) {
    Serial.print(EEPROM.read(i * SPACING + 1));
  }
}