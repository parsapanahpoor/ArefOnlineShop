const openAddresses = document.querySelector("#myAddresses");
      const mainContet = document.querySelector(".mainContet");
      const sidebar = document.querySelector(".sidebar");
      const backBtn = document.querySelector(".return");

      openAddresses.addEventListener("click", function () {
        mainContet.classList.remove("hidden");
        sidebar.classList.add("hidden");
      });
      backBtn.addEventListener("click", function () {
        mainContet.classList.add("hidden");
        sidebar.classList.remove("hidden");
      });



// -------------------------------------------

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