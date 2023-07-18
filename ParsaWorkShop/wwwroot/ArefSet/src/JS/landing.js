// import Swiper from "swiper/swiper-bundle.mjs";
// import "swiper/swiper-bundle.css";
 
 // -----------------------------Like btn------------------------------

 var likeBtn = document.getElementsByClassName("likeBtn");
 var i;
 for (i = 0; i < likeBtn.length; i++) {
   likeBtn[i].addEventListener("click", function () {
     this.classList.toggle("text-red-500");
   });
 }

// // header slider
//  var swiper = new Swiper(".mySwiper", {
//     slidesPerView: 1,
//     effect: "fade",
//     loop: true,
//     pagination: {
//       el: ".swiper-pagination",
//       clickable: true,
//     },
//     navigation: {
//       nextEl: ".swiper-button-next",
//       prevEl: ".swiper-button-prev",
//     },
//     autoplay: {
//       delay: 3000,
//     },
// });


// // comment slider
// var swiper3 = new Swiper(".swiper3", {
//     direction: "vertical",
//     // spaceBetween: 30,
//     // effect: "fade",
//     loop: true,
//     pagination: {
//       el: ".swiper-pagination3",
//       clickable: true,
//     },
//     autoplay: {
//       delay: 3000,
//     },
// });