var keys = {
  0: {
    label: "Function key",
    0: {label: "None"},
    104: {label: "F13"},
    105: {label: "F14"},
    106: {label: "F15"},
    107: {label: "F16"},
  },
  1: {
    label: "Media key",
    0: {label: "None"},
    233: {label: "V+"},
    234: {label: "V-"},
    182: {label: "<<"},
    205: {label: "play pause"},
    181: {label: ">>"},
    226: {label: "Vx"}
  },
  2: {
    label: "Youtube",
    0: {label: "None"},
    9: {label: "Full screen"}
  },
  3: {
    label: "ATEM Mini",
    0: {label: "None"},
    1: {label: "Start"},
    2: {label: "Prepare next segment"},
    3: {label: "Next segment"},
    4: {label: "Pause"},
    5: {label: "Reset"},
  },
  4: {
    label: "Letter",
    0: {label: "None"},
    4: {label: "a"},
    5: {label: "b"},
    6: {label: "c"},
    7: {label: "d"},
    8: {label: "e"},
    9: {label: "f"},
    10: {label: "g"},
    11: {label: "h"},
    12: {label: "i"},
    13: {label: "j"},
    14: {label: "k"},
    15: {label: "l"},
    16: {label: "m"},
    17: {label: "n"},
    18: {label: "o"},
    19: {label: "p"},
    20: {label: "q"},
    21: {label: "r"},
    22: {label: "s"},
    23: {label: "t"},
    24: {label: "u"},
    25: {label: "v"},
    26: {label: "w"},
    27: {label: "x"},
    28: {label: "y"},
    29: {label: "z"},
    30: {label: "1"},
    31: {label: "2"},
    32: {label: "3"},
    33: {label: "4"},
    34: {label: "5"},
    35: {label: "6"},
    36: {label: "7"},
    37: {label: "8"},
    38: {label: "9"},
    39: {label: "0"},
  }
};
var ledAnimations = {
  0: {label: "Button"},
  1: {label: "Switch"}
}
let port;
let textEncoder = new TextEncoder();
let connectButton = document.querySelector('#connect');
let connecting = false;

(function() {
  'use strict';

  document.addEventListener('DOMContentLoaded', event => {
    let connectionStatus = document.querySelector('#connectionStatus');

    function connect() {
      connecting = true;
      port.connect().then(() => {
        console.log(port);
        connectionStatus.style.display = 'block';
        connectionStatus.innerHTML = 'Connecting to ' + port.device_.productName + '...';
        
        port.send(textEncoder.encode("sendInit")).catch(error => {
          connectionStatus.innerHTML = 'Send error: ' + error;
        });
        port.onReceive = data => {
          let textDecoder = new TextDecoder();
          let receivedKeys = textDecoder.decode(data);
          receivedKeys = receivedKeys.trim().split(" ");
          if (receivedKeys[0] == "init") {
            console.log("init received correctly");
            document.querySelector('#noConnect').style.display = 'none';
            document.querySelector('#noSelect').style.display = '';
            document.querySelector('#guide').style.display = 'none';
            document.querySelector('#keypad').style.display = '';
            connectionStatus.innerHTML = 'Connected.';
            connectButton.textContent = 'Disconnect';
            getActions();
            getKeys(receivedKeys);
            connecting = false;
          }
          else if (receivedKeys[0] == "keyPressed") {
            document.getElementById(receivedKeys[1]).style.borderColor = 'blue';
            setTimeout(function() {
              document.getElementById(receivedKeys[1]).style.borderColor = 'white';
            }, 300);
          }
        }
        port.onReceiveError = error => {
          if (port) {
            disconnect();
          }
          // connectionStatus.innerHTML = 'Receive error: ' + error;
        };
      }, error => {
        connecting = false;
        connectionStatus.innerHTML = 'Connection error: ' + error;
      });
    };

    connectButton.addEventListener('click', function() {
      if (!connecting) {
        if (port) {
          disconnect();
        } else {
          serial.requestPort().then(selectedPort => {
            port = selectedPort;
            connect();
          }).catch(error => {
            connectionStatus.innerHTML = 'Connection error: ' + error;
          });
        }
      }
    });

    serial.getPorts().then(ports => {
      if (ports.length == 0) {
        connectionStatus.innerHTML = 'No devices found.';
      } else {
        port = ports[0];
        connect();
      }
    });
  });

  function getKeys(receivedKeys) {
    let keypad = document.querySelector('#keypad');

   for (var i = 1; i < receivedKeys.length; i++) {
      let key = receivedKeys[i].split(",");
      let value = key[0];
      let action = key[1];
      let ledAnimation = key[2];
      let div = document.createElement('div');
      div.id = i-1;
      div.innerHTML = keys[action][value].label;
      div.setAttribute('data-action', action);
      div.setAttribute('data-value', value);
      div.setAttribute('data-led', ledAnimation);
      if (action == 4) {
        let ctrl = key[3];
        let alt = key[4];
        let shift = key[5];
        div.setAttribute('data-ctrl', ctrl);
        div.setAttribute('data-alt', alt);
        div.setAttribute('data-shift', shift);
      }

      div.addEventListener("click", function(event) {
        keyClick(event)
      });
      div.addEventListener("drop", function(event) {
        drop(event)
      });
      div.addEventListener("dragover", function(event) {
        allowDrop(event)
      });
      div.addEventListener("dragenter", function(event) {
        setColor(event)
      });
      div.addEventListener("dragleave", function(event) {
        removeColor(event)
      });
      keypad.appendChild(div);
    }
  }
  function getActions() {
    let actions = document.querySelector('#actions');
    actions.innerHTML = '';
    for (let action in keys) {
      let div = document.createElement('div');
      div.id = action;
      div.innerHTML = keys[action].label;
      div.setAttribute('draggable', true);
      div.addEventListener("dragstart", function(event) {
        drag(event)
      });
      actions.appendChild(div);
    }
  }
})();