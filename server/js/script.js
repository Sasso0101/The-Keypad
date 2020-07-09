(function() {
  'use strict';

  var port;
  let textEncoder = new TextEncoder();
  let keys = {
    233: {
      label: "V+",
      action: "media"
    },
    234: {
      label: "V-",
      action: "media"
    },
    182: {
      label: "<<",
      action: "media"
    },
    205: {
      label: "play pausa",
      action: "media"
    },
    181: {
      label: ">>",
      action: "media"
    },
    226: {
      label: "Vx",
      action: "media"
    },
    104: {
      label: "F13",
      action: "hotkey"
    },
    105: {
      label: "F14",
      action: "hotkey"
    },
    106: {
      label: "F15",
      action: "hotkey"
    },
    107: {
      label: "F16",
      action: "hotkey"
    },
  };

  /*t.onTerminalReady = () => {
    console.log('Terminal ready.');
    let io = t.io.push();

    io.onVTKeystroke = str => {
      if (port !== undefined) {
        port.send(textEncoder.encode(str)).catch(error => {
          connectionStatus.innerHTML = 'Send error: ' + error;
        });
      }
    };

    io.sendString = str => {
      if (port !== undefined) {
        port.send(textEncoder.encode(str)).catch(error => {
          connectionStatus.innerHTML = 'Send error: ' + error;
        });
      }
    };
  };*/

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
            getKeys(receivedKeys)
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
        removeKeys()
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
      let div = document.createElement('div');
      div.id = i-1;
      div.innerHTML = keys[receivedKeys[i]].label;
      div.setAttribute('data-action', keys[receivedKeys[i]].action);
      div.setAttribute('data-value', receivedKeys[i]);
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
  function removeKeys() {
    keypad.innerHTML = '';
  }
})();