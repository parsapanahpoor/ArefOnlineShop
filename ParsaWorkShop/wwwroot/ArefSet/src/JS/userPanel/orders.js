const openOrder = document.querySelector("#myOrders");
const mainContet = document.querySelector(".mainContet");
const sidebar = document.querySelector(".sidebar");
const backBtn = document.querySelector(".return");

openOrder.addEventListener("click", function () {
  mainContet.classList.remove("hidden");
  sidebar.classList.add("hidden");
});
backBtn.addEventListener("click", function () {
  mainContet.classList.add("hidden");
  sidebar.classList.remove("hidden");
});

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
