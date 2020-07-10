var keys = {
  0: {
    label: "Function key",
    0: {
      label: "None"
    },
    104: {
      label: "F13"
    },
    105: {
      label: "F14"
    },
    106: {
      label: "F15"
    },
    107: {
      label: "F16"
    },
  },
  1: {
    label: "Media key",
    0: {
      label: "None"
    },
    233: {
      label: "V+"
    },
    234: {
      label: "V-"
    },
    182: {
      label: "<<"
    },
    205: {
      label: "play pausa"
    },
    181: {
      label: ">>"
    },
    226: {
      label: "Vx"
    }
  },
};
var port;
var textEncoder = new TextEncoder();

(function() {
  'use strict';

  document.addEventListener('DOMContentLoaded', event => {
    let connectButton = document.querySelector('#connect');
    let connectionStatus = document.querySelector('#connectionStatus');

    function connect() {
      connectionStatus.style.display = 'block';
      connectionStatus.innerHTML = 'Connecting to ' + port.device_.productName + '...';
      port.connect().then(() => {
        console.log(port);
        connectionStatus.innerHTML = 'Connected.';
        connectButton.textContent = 'Disconnect';
        
        port.send(textEncoder.encode("sendInit")).catch(error => {
          connectionStatus.innerHTML = 'Send error: ' + error;
        });
        port.onReceive = data => {
          let textDecoder = new TextDecoder();
          let receivedKeys = textDecoder.decode(data);
          receivedKeys = receivedKeys.trim().split(" ");
          if (receivedKeys[0] == "init") {
            console.log("init received correctly");
            getKeys(receivedKeys);
            getActions();
            document.querySelector('#noConnect').style.display = 'none';
            document.querySelector('#noSelect').style.display = 'flex';
          }
        }
        port.onReceiveError = error => {
          connectionStatus.innerHTML = 'Receive error: ' + error;
        };
      }, error => {
        connectionStatus.innerHTML = 'Connection error: ' + error;
      });
    };

    connectButton.addEventListener('click', function() {
      if (port) {
        port.disconnect();
        connectButton.textContent = 'Connect';
        connectionStatus.style.display = 'none';
        removeKeys();
        removeSettings();
        port = null;
      } else {
        serial.requestPort().then(selectedPort => {
          port = selectedPort;
          connect();
        }).catch(error => {
          connectionStatus.innerHTML = 'Connection error: ' + error;
        });
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
      let action = key[1];
      let value = key[0];
      let div = document.createElement('div');
      div.id = i-1;
      div.innerHTML = keys[action][value].label;
      div.setAttribute('data-action', action);
      div.setAttribute('data-value', value);
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
  function removeKeys() {
    keypad.innerHTML = '';
  }
  function removeSettings() {
    document.querySelector('#noConnect').style.display = 'flex';
    document.querySelector('#noSelect').style.display = 'none';
    document.querySelector('#settings').style.display = 'none';
  }
})();