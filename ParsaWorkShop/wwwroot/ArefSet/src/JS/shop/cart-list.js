//-------------------------------------------counter
let btnAddCounter = document.querySelector(".addCount");
let btnRemoveCounter = document.querySelector(".removeCount");
let resultCounter = document.querySelector(".resultCounter");
let counter = 1;
resultCounter.value  = counter;
btnAddCounter.addEventListener("click", function () {
  counter++;
  resultCounter.value  = counter;
});
btnRemoveCounter.addEventListener("click", function () {
  if (counter === 1) {
    return 1
  } else {
    
    counter--;
  }
  resultCounter.value = counter;
});


// ----