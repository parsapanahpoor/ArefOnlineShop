
const modal = document.querySelector(".modal");
//const showModal = document.querySelector(".showModal");
//const hideModal = document.querySelectorAll(".hideModal");
const menu = document.querySelector(".menu");
const showMenu = document.querySelector(".showMenu");
const hideMenu = document.querySelectorAll(".hideMenu");
const body = document.querySelector("body");

// ------------------------ modal -------------------------------

//showModal.addEventListener("click", function () {
//  modal.classList.remove("hidden");
//  body.style.overflow = "hidden";
//});

//hideModal.forEach((close) => {
//  close.addEventListener("click", function () {
//    modal.classList.add("hidden");
//    body.style.overflow = "auto";
//  });
//});

// ------------------------Side Menu Responsive-------------------------------

showMenu.addEventListener("click", function () {
  // menu.classList.replace("-right-[70%]","right-0")
  menu.classList.remove("opacity-0");
  menu.classList.remove("invisible");
  body.style.overflow = "hidden";
});
hideMenu.forEach((close) => {
  close.addEventListener("click", function () {
    // menu.classList.replace("right-0","-right-[70%]")
    menu.classList.add("opacity-0");
    menu.classList.add("invisible");
    body.style.overflow = "auto";
  });
});

function openForm(evt, formName) {
  var i, tabFormContent, tabFormLinks;
  tabFormContent = document.getElementsByClassName("tabFormContent");
  for (i = 0; i < tabFormContent.length; i++) {
    tabFormContent[i].style.display = "none";
  }
  tabFormLinks = document.getElementsByClassName("tabFormLinks");
  for (i = 0; i < tabFormLinks.length; i++) {
    tabFormLinks[i].className = tabFormLinks[i].className.replace(
      " activeFormItem",
      ""
    );
  }
  document.getElementById(formName).style.display = "block";
  evt.currentTarget.className += " activeFormItem";
}

// Get the element with id="defaultOpen" and click on it
document.getElementById("defaultFormOpen").click()


// body.addEventListener("click", function () {
//     menu.classList.add("opacity-0");
//     menu.classList.add("invisible");
//     modal.classList.add("hidden");
//     body.style.overflow = "auto";
// });
window.onclick = function(event) {
    if (event.target == modal) {
      modal.classList.add("hidden");
      body.style.overflow = "auto";
    }
    if (event.target == menu) {
        menu.classList.add("opacity-0");
        menu.classList.add("invisible");
        body.style.overflow = "auto";
    }
}
