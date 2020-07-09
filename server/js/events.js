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
  ev.target.innerHTML = data;
  removeColor(ev);
}

function setColor(ev) {
  ev.target.style.borderColor = "#0000ff";
}

function removeColor(ev) {
  ev.target.style.borderColor = "white";
}

function keyClick(ev) {
  
}