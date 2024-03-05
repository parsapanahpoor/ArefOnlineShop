$(document).ready(function (l) {
  // **************  fixed header
  var pageHeaderheight = $("header.main-header").height();
  var navWrapperheight = $("header.main-header .bottom-header").height();
  if ($("header.main-header").length) {
    $(".main-content").css(
      "margin-top",
      pageHeaderheight + navWrapperheight + 25
    );
  }
  $(window).scroll(function () {
    if ($(this).scrollTop() > 100) {
      $("header.main-header").addClass("fixed");
      $("header.main-header .ads-header-wrapper").slideUp(200);
    } else {
      $("header.main-header").removeClass("fixed");
      $("header.main-header .ads-header-wrapper").slideDown(200);
    }
  });
  var lastScrollTop = 0;
  window.addEventListener("scroll", function () {
    if (window.pageYOffset > 200) {
      var scrollTop = window.pageYOffset || document.documentElement.scrollTop;
      if (scrollTop > lastScrollTop && !$(".main-menu").hasClass("is-active")) {
        $("header.main-header .bottom-header").addClass(
          "hidden--bottom-header"
        );
      } else {
        $("header.main-header .bottom-header").removeClass(
          "hidden--bottom-header"
        );
      }
      lastScrollTop = scrollTop;
    }
  });
  $("header.main-header .main-menu").on("mouseenter", function () {
    $(this).addClass("is-active");
  });
  $("header.main-header .main-menu").on("mouseleave", function () {
    $(this).removeClass("is-active");
  });
  $(".category-list>ul>li:first-child").addClass("active");
  $(".category-list>ul>li").on("mouseenter", function () {
    $(this).addClass("active").siblings().removeClass("active");
  });

  // **************  search
  $("header.main-header .search-area form.search input").keyup(function () {
    if ($(this).val().length == 0) {
      // Hide the element
      $(
        "header.main-header .search-area form.search .search-result"
      ).removeClass("open");
      $(
        "header.main-header .search-area form.search .close-search-result"
      ).removeClass("show");
    } else {
      // Otherwise show it
      $("header.main-header .search-area form.search .search-result").addClass(
        "open"
      );
      $(
        "header.main-header .search-area form.search .close-search-result"
      ).addClass("show");
    }
  });
  $("header.main-header .search-area form.search .close-search-result").on(
    "click",
    function () {
      $(this).removeClass("show");
      $(
        "header.main-header .search-area form.search .search-result"
      ).removeClass("open");
    }
  );

  // **************  category slider
  $(".category-slider").owlCarousel({
    rtl: true,
    margin: 10,
    nav: true,
    navText: [
      '<i class="mdi mdi mdi-chevron-right"></i>',
      '<i class="mdi mdi mdi-chevron-left"></i>',
    ],
    dots: false,
    responsiveClass: true,
    responsive: {
      0: {
        items: 2,
        slideBy: 1,
      },
      576: {
        items: 3,
        slideBy: 2,
      },
      768: {
        items: 4,
        slideBy: 2,
      },
      992: {
        items: 6,
        slideBy: 3,
      },
      1400: {
        items: 8,
        slideBy: 4,
      },
    },
  });

  /* **************  tooltip */
  $('[data-toggle="tooltip"]').tooltip();

  /* **************  product-carousel */
  /* carousel-lg */
  $(".carousel-lg").owlCarousel({
    rtl: true,
    margin: 10,
    nav: true,
    navText: [
      '<i class="mdi mdi mdi-chevron-right"></i>',
      '<i class="mdi mdi mdi-chevron-left"></i>',
    ],
    dots: true,
    responsiveClass: true,
    responsive: {
      0: {
        items: 2,
        slideBy: 1,
      },
      480: {
        items: 2,
        slideBy: 1,
      },
      576: {
        items: 3,
        slideBy: 1,
      },
      768: {
        items: 4,
        slideBy: 2,
      },
      992: {
        items: 4,
        slideBy: 2,
      },
      1200: {
        items: 5,
        slideBy: 3,
      },
      1400: {
        items: 6,
        slideBy: 4,
      },
    },
  });
  /* carousel-md */
  $(".carousel-md").owlCarousel({
    rtl: true,
    margin: 10,
    nav: true,
    navText: [
      '<i class="mdi mdi mdi-chevron-right"></i>',
      '<i class="mdi mdi mdi-chevron-left"></i>',
    ],
    dots: true,
    responsiveClass: true,
    responsive: {
      0: {
        items: 2,
        slideBy: 1,
      },
      480: {
        items: 2,
        slideBy: 1,
      },
      576: {
        items: 3,
        slideBy: 1,
      },
      768: {
        items: 3,
        slideBy: 2,
      },
      992: {
        items: 4,
        slideBy: 2,
      },
      1200: {
        items: 4,
        slideBy: 3,
      },
      1400: {
        items: 5,
        slideBy: 4,
      },
    },
  });

  /* ************** suggestion-slider */
  $("#suggestion-slider").owlCarousel({
    rtl: true,
    items: 1,
    autoplay: true,
    autoplayTimeout: 5000,
    loop: true,
    dots: true,
    onInitialized: startProgressBar,
    onTranslate: resetProgressBar,
    onTranslated: startProgressBar,
  });

  function startProgressBar() {
    // apply keyframe animation
    $(".slide-progress").css({
      width: "100%",
      transition: "width 5000ms",
    });
  }

  function resetProgressBar() {
    $(".slide-progress").css({
      width: 0,
      transition: "width 0s",
    });
  }

  /* ************** product-gallery */
  var e = document;
  $(".product-carousel").owlCarousel({
    rtl: true,
    items: 1,
    loop: false,
    dots: false,
    nav: true,
    navText: [
      '<i class="mdi mdi mdi-chevron-right"></i>',
      '<i class="mdi mdi mdi-chevron-left"></i>',
    ],
    thumbs: true,
    thumbsPrerendered: true,
  });

  /* ************** sidebar-sticky */
  if ($(".container .sticky-sidebar").length) {
    $(".container .sticky-sidebar").theiaStickySidebar();
  }

  /* ************** product-params */
  $(".product-params .sum-more").click(function () {
    var sumaryBox = $(this).parents(".product-params");
    sumaryBox.toggleClass("active");

    $(this).find("i").toggleClass("active");

    $(this).find(".show-more").fadeToggle(0);
    $(this).find(".show-less").fadeToggle(0);
  });

  /* ************** horizontal-menu */
  $(".ah-tab-wrapper").horizontalmenu({
    itemClick: function (item) {
      $(".ah-tab-content-wrapper .ah-tab-content").removeAttr(
        "data-ah-tab-active"
      );
      $(
        ".ah-tab-content-wrapper .ah-tab-content:eq(" + $(item).index() + ")"
      ).attr("data-ah-tab-active", "true");
      return false; //if this finction return true then will be executed http request
    },
  });

  /* ************** shopping */
  $("#btn-checkout-contact-location").click(function () {
    $(".checkout-address").addClass("show");
    $(".checkout-contact-content").addClass("hidden");
  });

  $("#cancel-change-address-btn").click(function () {
    $(".checkout-address").removeClass("show");
    $(".checkout-contact-content").removeClass("hidden");
  });

  /* ************** search-sidebar */
  $(".btn-filter-sidebar").on("click", function () {
    $(".filter-options-sidebar").addClass("toggled");
  });
  $(".btn-close-filter-sidebar").on("click", function () {
    $(".filter-options-sidebar").removeClass("toggled");
  });

  /* ************** product-quantity */
  $(".num-in span").click(function () {
    var $input = $(this).parents(".num-block").find("input.in-num");
    if ($(this).hasClass("minus")) {
      var count = parseFloat($input.val()) - 1;
      count = count < 1 ? 1 : count;
      if (count < 2) {
        $(this).addClass("dis");
      } else {
        $(this).removeClass("dis");
      }
      $input.val(count);
    } else {
      var count = parseFloat($input.val()) + 1;
      $input.val(count);
      if (count > 1) {
        $(this).parents(".num-block").find(".minus").removeClass("dis");
      }
    }

    $input.change();
    return false;
  });

  /* ************** nice-select */
  if ($(".custom-select-ui").length) {
    $(".custom-select-ui select").niceSelect();
  }

  /* ************** verify-phone-number */
  if ($("#countdown-verify-end").length) {
    var $countdownOptionEnd = $("#countdown-verify-end");

    $countdownOptionEnd.countdown({
      date: new Date().getTime() + 120 * 1000, // 2 minute later
      text: '<span class="day">%s</span><span class="hour">%s</span><span>: %s</span><span>%s</span>',
      end: function () {
        $countdownOptionEnd.html(
          "<a href='' class='btn-link-border'>ارسال مجدد</a>"
        );
      },
    });

    $(".line-number").keyup(function () {
      $(this).next().focus();
    });
  }

  /* ************** back-to-top */
  $(".back-to-top a").click(function () {
    $("body,html").animate(
      {
        scrollTop: 0,
      },
      700
    );
    return false;
  });

  /* ************** responsive-header */
  $("header.main-header button.btn-menu").click(function () {
    $("header.main-header .side-menu").addClass("open");
    $("header.main-header .overlay-side-menu").addClass("show");
  });

  $("header.main-header .overlay-side-menu.show").click(function () {
    $(this).removeClass("show");
    $("header.main-header .side-menu").removeClass("open");
  });
  $("button.btn-menu").on("click", function () {
    $(".overlay-side-menu").fadeIn(200);
    $("header.main-header .side-menu").addClass("open");
  });

  $(".overlay-side-menu").on("click", function () {
    if ($("header.main-header .side-menu").hasClass("open")) {
      $("header.main-header .side-menu").removeClass("open");
    }
    $(this).fadeOut(200);
  });
  $("header.main-header .side-menu li.active")
    .addClass("open")
    .children("ul")
    .show();
  $("header.main-header .side-menu li.sub-menu> a").on("click", function () {
    $(this).removeAttr("href");
    var e = $(this).parent("li");
    if (e.hasClass("open")) {
      e.removeClass("open");
      e.find("li").removeClass("open");
      e.find("ul").slideUp(400);
    } else {
      e.addClass("open");
      e.children("ul").slideDown(400);
      e.siblings("li").children("ul").slideUp(400);
      e.siblings("li").removeClass("open");
    }
  });

  /* ************** favorites product */
  $("ul.gallery-options button.add-favorites").on("click", function () {
    $(this).toggleClass("favorites");
  });

  /* ************** nice-scroll */
  if ($(".do-nice-scroll").length) {
    $(".do-nice-scroll").niceScroll({
      railalign: "left",
      autohidemode: false,
    });
  }

  /* ************** stack-menu */
  if ($("#stack-menu").length) {
    $("#stack-menu").stackMenu();
  }

  /* ************** colorswitch */
  if ($("#colorswitch-option").length) {
    $("#colorswitch-option button").on("click", function () {
      $("#colorswitch-option ul").toggleClass("show");
    });
    $("#colorswitch-option ul li").on("click", function () {
      $("#colorswitch-option ul li").removeClass("active");
      $(this).addClass("active");
      var colorPath = $(this).attr("data-path");
      $("#colorswitch").attr("href", colorPath);
    });
  }
});
