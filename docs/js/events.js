function removeKeys() {
  keypad.innerHTML = '';
}
function removeSettings() {
  document.querySelector('#noConnect').style.display = '';
  document.querySelector('#noSelect').style.display = 'none';
  document.querySelector('#settings').style.display = 'none';
}
function disconnect() {
  port.disconnect();
  connectButton.textContent = 'Connect';
  connectionStatus.style.display = 'none';
  removeKeys();
  removeSettings();
  document.querySelector('#guide').style.display = '';
  document.querySelector('#keypad').style.display = 'none';
  port = null;
}

function allowDrop(ev) {
  ev.preventDefault();
}

function drag(ev) {
  ev.dataTransfer.setData("text", ev.target.id);
}

function drop(ev) {
  ev.preventDefault();
  let data=ev.dataTransfer.getData("text");
  ev.target.setAttribute('data-action', data);
  ev.target.innerHTML = keys[data].label;
  ev.target.setAttribute('data-value', '');
  removeColor(ev);
  ev.target.click();
}

function setColor(ev) {
  ev.target.style.borderColor = "#0000ff";
}

function removeColor(ev) {
  ev.target.style.borderColor = "white";
}

function keyClick(ev) {
  document.querySelector('#noSelect').style.display = 'none';
  document.querySelector('#settings').style.display = '';
  document.querySelector('#buttonNumber').innerHTML = parseInt(ev.target.id) + 1;
  document.querySelector('#keyPreview').innerHTML = ev.target.innerHTML;

  let action = parseInt(ev.target.getAttribute('data-action'));
  let value = parseInt(ev.target.getAttribute('data-value'));
  document.querySelector('#action').innerHTML = keys[action].label;
  document.querySelector('#action').setAttribute('data-keyType', action);

  let select = document.querySelector('#options');
  select.innerHTML = '';
  select.addEventListener("change", function(event){
    selectChange(event);
  });

  for (let selectValue in keys[action]) {
    entry = keys[action][parseInt(selectValue)];
    if (entry) {
      let option = document.createElement('option');
      option.value = selectValue;
      option.innerHTML = entry.label;
      if (value == selectValue) {
        option.setAttribute('selected', true);
      }
      select.appendChild(option);
    }
  }
}

function selectChange(ev) {
  let keyPreview = document.querySelector('#keyPreview');
  let keyID = parseInt(document.querySelector('#buttonNumber').innerHTML) - 1;
  let key = document.getElementById(keyID);
  let selectedLabel = ev.target.options[ev.target.selectedIndex].text;
  let selectedValue = parseInt(ev.target.value);
  let keyType = document.querySelector('#action').getAttribute('data-keyType');
  keyPreview.innerHTML = selectedLabel;
  key.innerHTML = selectedLabel;
  key.setAttribute('data-value', selectedValue);


  let message = 'changeKey ' + keyID + ' ' + keyType + ' ' + selectedValue;
  if (port !== undefined) {
    port.send(textEncoder.encode(message)).catch(error => {
      let connectionStatus = document.querySelector('#connectionStatus');
      connectionStatus.innerHTML = 'Send error: ' + error;
    });
  }
}