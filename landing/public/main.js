var property = document.getElementById("instructions");
var download = document.getElementById("yes");
download.addEventListener("click", instruct);


function instruct() {
  if (property.style.display == "block") {
    property.style.display = "none";
  }
  else {
    property.style.display = "block";
  }
}
