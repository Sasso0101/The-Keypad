#include <HID-Project.h>
#include <HID-Settings.h>
#include <WebUSB.h>
#include <Keypad.h>
#include <Tlc5940.h>
#include <EEPROM.h>

// Initialize webUSB
WebUSB WebUSBSerial(0, "localhost/");
#define Serial WebUSBSerial

const byte ROWS = 2;
const byte COLS = 5;
char keysID[ROWS][COLS] = {
  {'0', '1', '2', '3', '4'},
  {'5', '6', '7', '8', '9'}
};

typedef enum:byte {FN, MEDIA} keyType;
typedef struct {
  keyType type;
  byte value;
} key;

/*key keys[10] = {
  {MEDIA, MEDIA_VOLUME_UP},
  {MEDIA, MEDIA_VOLUME_DOWN},
  {MEDIA, MEDIA_PREVIOUS},
  {MEDIA, MEDIA_PLAY_PAUSE},
  {MEDIA, MEDIA_NEXT},
  {MEDIA, MEDIA_VOLUME_MUTE},
  {FN, KEY_F13},
  {FN, KEY_F14},
  {FN, KEY_F15},
  {FN, KEY_F16}
};*/

//Row pinouts of the keypad
byte rowPins[ROWS] = {2, 3};
//Column pinouts of the keypad
byte colPins[COLS] = {A0, 8, 7, 6, 4};

// Array which points to which pin i button is connected to TLC5940
int ledPin[10] = {7, 6, 5, 9, 8, 2, 1, 0, 15, 14};
bool serialInit = false;

Keypad kpd = Keypad( makeKeymap(keysID), rowPins, colPins, ROWS, COLS );

void setup() {
  kpd.setHoldTime(500);

  Tlc.init();
  Tlc.clear();
  Tlc.update();

  Consumer.begin();
}

void loop() {
  if (Serial) {
    if (serialInit == false) {
      Serial.begin(9600);
      serialInit = true;
    }
    else if(Serial.available()) {
      String incomingMessage = Serial.readStringUntil(' ');
      if (incomingMessage == "sendInit") {
        // Tells the PC the key configuration
        Serial.write("init ");
        for (int i = 0; i < 10; i++) {
          Serial.print(EEPROM.read(i*2+1));
          Serial.write(",");
          Serial.print(EEPROM.read(i*2));
          Serial.write(" ");
        }
        Serial.flush();
      }
      else {
        if (incomingMessage == "changeKey") {
          int keyID = Serial.readStringUntil(' ').toInt();
          int keyType = Serial.readStringUntil(' ').toInt();
          int value = Serial.readStringUntil(' ').toInt();

          EEPROM.update(keyID*2, keyType);
          EEPROM.update(keyID*2+1, value);
        }
      }
    }
  } else if (!Serial) {
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
            
            if (Serial) {
              Serial.print("keyPressed ");
              Serial.print(kpd.key[i].kchar - '0');
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

// Send a keystroke
void sendKey(int i) {
  if (EEPROM.read(i*2) == MEDIA) {
    Consumer.write(EEPROM.read(i*2 + 1));
  }
  else if (EEPROM.read(i*2) == FN) {
    Keyboard.write(KeyboardKeycode(EEPROM.read(i*2 + 1)));
  }
}
