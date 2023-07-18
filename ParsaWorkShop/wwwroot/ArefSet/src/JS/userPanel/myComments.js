const openComments = document.querySelector("#myComments");
const mainContet = document.querySelector(".mainContet");
const sidebar = document.querySelector(".sidebar");
const backBtn = document.querySelector(".return");

openComments.addEventListener("click", function () {
  mainContet.classList.remove("hidden");
  sidebar.classList.add("hidden");
});
backBtn.addEventListener("click", function () {
  mainContet.classList.add("hidden");
  sidebar.classList.remove("hidden");
});

//----------------------------comment tab---------------------------------------

function openComment(evt, formName) {
  var i, tabComment, tabCommentLinks;
  tabComment = document.getElementsByClassName("tabComment");
  for (i = 0; i < tabComment.length; i++) {
    tabComment[i].style.display = "none";
  }
  tabCommentLinks = document.getElementsByClassName("tabCommentLinks");
  for (i = 0; i < tabCommentLinks.length; i++) {
    tabCommentLinks[i].className = tabCommentLinks[i].className.replace(
      " activeCommentTab",
      ""
    );
  }
  document.getElementById(formName).style.display = "block";
  evt.currentTarget.className += " activeCommentTab";
}

// Get the element with id="defaultOpen" and click on it
document.getElementById("defaultCommentOpen").click();
