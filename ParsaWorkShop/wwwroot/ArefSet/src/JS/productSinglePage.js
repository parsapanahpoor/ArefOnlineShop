//-------------------------------------------counter
let btnAddCounter = document.querySelector(".addCount");
let btnRemoveCounter = document.querySelector(".removeCount");
let resultCounter = document.querySelector(".resultCounter");
let counter = 1;
resultCounter.value = counter;
btnAddCounter.addEventListener("click", function () {
  counter++;
  resultCounter.value = counter;
});
btnRemoveCounter.addEventListener("click", function () {
  if (counter === 1) {
    return 1;
  } else {
    counter--;
  }
  resultCounter.value = counter;
});

// -------------------------------------------------
//const imgContElm = document.querySelector(".img-container");
//const imgElm = document.querySelector(".img-container img");
//const listProductElm = document.querySelector(".list-product");

//// precentage of the zoom
//const ZOOM = 300;

////Event Mouse Enter
//imgContElm.addEventListener("mouseenter", function () {
//  imgElm.style.width = ZOOM + "%";
//});

////Event Mouse Leave
//imgContElm.addEventListener("mouseleave", function () {
//  imgElm.style.width = "100%";
//  imgElm.style.top = "0";
//  imgElm.style.left = "0";
//});

////Event Mouse Move:
//imgContElm.addEventListener("mousemove", function (mouseEvent) {
//  let obj = imgContElm;
//  let obj_left = 0;
//  let obj_top = 0;
//  let xpos;
//  let ypos;

//  while (obj.offsetParent) {
//    obj_left += obj.offsetLeft;
//    obj_top += obj.offsetTop;
//    obj = obj.offsetParent;
//  }
//  if (mouseEvent) {
//    xpos = mouseEvent.pageX;
//    ypos = mouseEvent.pageY;
//  } else {
//    xpos = window.event.x + document.body.scrollLeft - 2;
//    ypos = window.event.y + document.body.scrollTop - 2;
//  }
//  xpos -= obj_left;
//  ypos -= obj_top;

//  const imgWidth = imgElm.clientWidth;
//  const imgHeight = imgElm.clientHeight;

//  imgElm.style.top =
//    -(((imgHeight - this.clientHeight) * ypos) / this.clientHeight) + "px";
//  imgElm.style.left =
//    -(((imgWidth - this.clientWidth) * xpos) / this.clientWidth) + "px";
//});

//// change the current image
//Array.from(listProductElm.children).forEach((productElm, i, list) => {
//  productElm.addEventListener("click", function () {
//    const newSrc = productElm.querySelector("img").src;
//    imgElm.src = newSrc;

//    list.forEach((prod) => prod.classList.remove("active"));
//    productElm.classList.add("active");
//  });
//});

////

//function changeHeight() {
//  imgContElm.style.height = imgContElm.clientWidth + "px";
//}
//changeHeight();

//// function changeWidth() {
////   imgContElm.style.width=imgContElm.clientHeight+'px'
//// }
//// changeWidth()

//window.addEventListener("resize", changeHeight);

// -----------------------------Like btn------------------------------

var likeBtn = document.getElementsByClassName("likeBtn");
var i;
for (i = 0; i < likeBtn.length; i++) {
  likeBtn[i].addEventListener("click", function () {
    this.classList.toggle("text-red-500");
  });
}
