//#region Load Add Personal Link View Model

function LoadAddPersonalLinkModalBody() {
    $.ajax({
        url: "/Show-AddPErsonalLink-Modal",
        type: "get",
        data: {
            
        },
        success: function (response) {
            $("#NormalModalBody").html(response);

            $('#PersonalLinkForm').data('validator', null);
            $.validator.unobtrusive.parse('#PersonalLinkForm');

            $("#NormalModal").modal("show");
        }
    });
}

//#endregion

//#region Success Submited Personal Link Form 

function PersonalLinkFormSubmited(response) {
    if (response.status === "Success") {
        $("#NormalModal").modal("hide");
        ShowMessage("اعلان", "عملیات با موفقیت انجام شده است ", "success");
        location.reload();
    } else {
        ShowMessage("اعلان", "عملیات با شکست مواجه شد", "error");
    }
}

//#endregion

//#region Load Add Personal Work Sample View Model

function LoadAddPersonalWorkSampleModalBody() {
    $.ajax({
        url: "/Show-AddPersonalWorkSample-Modal",
        type: "get",
        data: {
            
        },
        success: function (response) {
            $("#NormalModalBody").html(response);

            $('#PersonalWorkSampleForm').data('validator', null);
            $.validator.unobtrusive.parse('#PersonalWorkSampleForm');

            $("#NormalModal").modal("show");
        }
    });
}

//#endregion

//#region Success Submited Personal Work Sample Form 

function PersonalWorkSampleFormSubmited(response) {
    if (response.status === "Success") {
        $("#NormalModal").modal("hide");
        ShowMessage("اعلان", "عملیات با موفقیت انجام شده است ", "success");
        location.reload();
    } else {
        ShowMessage("اعلان", "عملیات با شکست مواجه شد", "error");
    }
}

//#endregion