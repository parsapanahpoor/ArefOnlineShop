const swiperSlider = new Swiper(".swiper", {
  loop: true,
  direction: "vertical", // عمودی

  navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev",
  },

  pagination: {
    el: ".swiper-pagination",
  },
  autoplay: {
    delay: 3000,
  },
});
