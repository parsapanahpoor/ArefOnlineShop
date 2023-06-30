$("#NewsLetterButton").click(function () {
    var mobile = $("#NewsLetterMobile").val();
    var email = $("#NewsLetterEmail").val();

    if ((mobile === null || mobile === "" || mobile === undefined) &&
        (email === null || email === "" || email === undefined)) {
        ShowMessage("هشدار", "لطفا یکی از موارد شماره تماس یا ایمیل را پر کنید", "warning");
    } else {
        $.ajax({
            url: "/SiteSetting/AddNewsLetter",
            type: "post",
            data: {
                Email: email,
                Mobile: mobile
            },
            success: function (response) {
                if (response.status === "Success") {
                    $("#NewsLetterMobile").val('');
                    $("#NewsLetterEmail").val('');
                    ShowMessage("اعلان", "عملیات با موفقیت انجام شد", "success");
                }
                if (response.status === "Error" && response.data === "DuplicatedEmail") {
                    ShowMessage("هشدار", "ایمیل وارد شده تکراری است ", "warning");
                }
                if (response.status === "Error" && response.data === "DuplicatedMobile") {
                    ShowMessage("هشدار", "موبایل وارد شده تکراری است ", "warning");
                }
                if (response.status === "Error" && response.data === "EmailAndMobilNotExist") {
                    ShowMessage("هشدار", "لطفا یکی از موارد شماره تماس یا ایمیل را پر کنید", "warning");
                }
                if (response.status === "Error" && response.data === "NotValid") {
                    ShowMessage("هشدار", "اطلاعات وارد شده معتبر نمی باشد", "warning");
                }
            },
            error: function () {
                ShowMessage("اعلان", "عملیات با شکست مواجه شد", "error");
            }
        });
    }
});