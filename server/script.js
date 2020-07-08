(function() {
  'use strict';

  var port;
  let textEncoder = new TextEncoder();

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
    let keypad = document.querySelector('#keypad');

    function connect() {
      connectionStatus.innerHTML = 'Connecting to ' + port.device_.productName + '...';
      port.connect().then(() => {
        console.log(port);
        connectionStatus.innerHTML = 'Connected.';
        connectButton.textContent = 'Disconnect';
        port.onReceive = data => {
          let textDecoder = new TextDecoder();
          let str = textDecoder.decode(data);
          str = str.split(" ");
          if (str[0] == "init") {
            console.log("init received correctly");
            for (var i = 1; i < str.length; i++) {
              let div = document.createElement('div');
              div.innerHTML = str[i];
              keypad.appendChild(div);
            }
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
})();