const filterMenu = document.querySelector(".filterMenu");
const mainContent = document.querySelector(".mainContent");
const showFilterMenu = document.querySelector(".showFilterMenu");
const hideFilterMenu = document.querySelector(".hideFilterMenu");
const footer = document.querySelector(".footer");
const navbar = document.querySelector(".navbar");
const breadcrumb = document.querySelector(".breadcrumb");

// const body = document.querySelector("body");





var colorButton = document.getElementsByClassName("theme-button");
var i;
for (i = 0; i < colorButton.length; i++) {
  colorButton[i].addEventListener("click", function () {
    this.classList.toggle("active");
  });
}

//  price filter

var lowerSlider = document.querySelector("#lower");
var upperSlider = document.querySelector("#upper");

document.querySelector("#two").value = upperSlider.value;
document.querySelector("#one").value = lowerSlider.value;

var lowerVal = parseInt(lowerSlider.value);
var upperVal = parseInt(upperSlider.value);

upperSlider.oninput = function () {
  lowerVal = parseInt(lowerSlider.value);
  upperVal = parseInt(upperSlider.value);

  if (upperVal < lowerVal + 4) {
    lowerSlider.value = upperVal - 4;
    if (lowerVal == lowerSlider.min) {
      upperSlider.value = 4;
    }
  }
  document.querySelector("#two").value = this.value;
};

lowerSlider.oninput = function () {
  lowerVal = parseInt(lowerSlider.value);
  upperVal = parseInt(upperSlider.value);
  if (lowerVal > upperVal - 4) {
    upperSlider.value = lowerVal + 4;
    if (upperVal == upperSlider.max) {
      lowerSlider.value = parseInt(upperSlider.max) - 4;
    }
  } 
  document.querySelector("#one").value = this.value;
};



// ------------------------Filter Side Menu Responsive-------------------------------

showFilterMenu.addEventListener("click", function () {
  // menu.classList.replace("-right-[70%]","right-0")
  // menu.classList.remove("opacity-0");
  // menu.classList.remove("invisible");
  mainContent.classList.add("hidden");
  filterMenu.classList.remove("hidden");
  footer.classList.add("hidden");
  navbar.classList.add("hidden");
  breadcrumb.classList.add("hidden");

  // showFilterMenu
  // hideFilterMenu

  // body.style.overflow = "hidden";
});

// function hideFilterMenu() {
//   alert("hided")
// }

hideFilterMenu.addEventListener("click", function () {
  filterMenu.classList.add("hidden");
  // alert("hided")
  mainContent.classList.remove("hidden");
  footer.classList.remove("hidden");
  navbar.classList.remove("hidden");
  breadcrumb.classList.remove("hidden");
  // menu.classList.replace("right-0","-right-[70%]")
  // menu.classList.add("opacity-0");
  // menu.classList.add("invisible");
  // body.style.overflow = "auto";
});

// -----------------------------Like btn------------------------------

var likeBtn = document.getElementsByClassName("likeBtn");
var i;
for (i = 0; i < likeBtn.length; i++) {
  likeBtn[i].addEventListener("click", function () {
    this.classList.toggle("text-red-500");
  });
}
