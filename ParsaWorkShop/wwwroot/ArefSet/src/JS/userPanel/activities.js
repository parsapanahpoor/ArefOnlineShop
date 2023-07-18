//----------------------------Order tab---------------------------------------

function openTabOrder(evt, formName) {
  var i, tabOrder, tabOrderLinks;
  tabOrder = document.getElementsByClassName("tabOrder");
  for (i = 0; i < tabOrder.length; i++) {
    tabOrder[i].style.display = "none";
  }
  tabOrderLinks = document.getElementsByClassName("tabOrderLinks");
  for (i = 0; i < tabOrderLinks.length; i++) {
    tabOrderLinks[i].className = tabOrderLinks[i].className.replace(
      " activeOrderItem",
      ""
    );
  }
  document.getElementById(formName).style.display = "block";
  evt.currentTarget.className += " activeOrderItem";
}

// Get the element with id="defaultOpen" and click on it
document.getElementById("defaultOrderOpen").click();

// --------------------------------------------------

const openActiviti = document.querySelector("#myActivities");
const mainContet = document.querySelector(".mainContet");
const sidebar = document.querySelector(".sidebar");
const backBtn = document.querySelector(".return");

openActiviti.addEventListener("click", function () {
  mainContet.classList.remove("hidden");
  sidebar.classList.add("hidden");
});
backBtn.addEventListener("click", function () {
  mainContet.classList.add("hidden");
  sidebar.classList.remove("hidden");
});


// ---------

// function resposFun(x) {
//     // var tablinks = document.getElementsByClassName("theme-button");
//     // var sideMenu=document.getElementById('asideCol')
//     // var i;
//     // for (i = 0; i < tablinks.length; i++) {
//     //   tablinks[i].addEventListener("click", function () {
//     //     sideMenu.style.display = "none";
//     //   });
//     // }
//     if (x.matches) {
//       // If media query matches
//       // document.body.style.backgroundColor = "yellow";
//     } else {
//       //  document.body.style.backgroundColor = "pink";
//     }
//   }

//   var x = window.matchMedia("(max-width: 700px)");
//   resposFun(x); // Call listener function at run time
//   x.addListener(resposFun); // Attach listener function on state changes
