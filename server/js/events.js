function allowDrop(ev) {
  ev.preventDefault();
}

function drag(ev) {
  ev.dataTransfer.setData("text", ev.target.id);
}

function drop(ev) {
  ev.preventDefault();
  var data=ev.dataTransfer.getData("text");
  ev.target.setAttribute('data-action', data);
  ev.target.innerHTML = keys[data].label;
  ev.target.setAttribute('data-value', '');
  removeColor(ev);
}

function setColor(ev) {
  ev.target.style.borderColor = "#0000ff";
}

function removeColor(ev) {
  ev.target.style.borderColor = "white";
}

function keyClick(ev) {
  document.querySelector('#noSelect').style.display = 'none';
  document.querySelector('#settings').style.display = 'flex';
  document.querySelector('#buttonNumber').innerHTML = parseInt(ev.target.id) + 1;
  document.querySelector('#keyPreview').innerHTML = ev.target.innerHTML;

  let action = parseInt(ev.target.getAttribute('data-action'));
  let value = parseInt(ev.target.getAttribute('data-value'));
  document.querySelector('#action').innerHTML = keys[action].label;

  let select = document.querySelector('#options')
  select.innerHTML = '';

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