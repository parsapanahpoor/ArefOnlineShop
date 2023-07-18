const openOrder = document.querySelector("#myInformation");
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