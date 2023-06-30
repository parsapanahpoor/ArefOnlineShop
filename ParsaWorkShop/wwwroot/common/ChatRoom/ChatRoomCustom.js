



//#region Add Group To The User Group List

function appendGroup(groupName, token, imageName) {

    if (groupName === "Error") {
        ShowMessage("اعلان", "عملیات باشکست مواجه شده است.", "error");
    }
    else {
        ShowMessage("اعلان", "عملیات باموفقیت انجام شده است.", "success");
        $(".rooms #user_groups ul").append(`
                                                     <li onclick="joinInGroup('${token}')">
                                                                    ${groupName}
                                                                    <img src="/content/images/user/thumb/${imageName}" />
                                                            <span></span>
                                                        </li>
                                                            `);
        $("#exampleModal").modal({ show: false });
    }
}

//#endregion

//#region Create Group

function insertGroup(event) {
    event.preventDefault();
    var groupName = event.target[0].value;
    var imageFile = event.target[1].files[0];
    var formData = new FormData();
    formData.append("GroupName", groupName);
    formData.append("ImageFile", imageFile);
    $.ajax({
        url: "/ChatRoom/home/CreateGroup",
        type: "post",
        data: formData,
        encytype: "multipart/form-data",
        processData: false,
        contentType: false
    });
}

//#endregion

//#region Search

function search() {
    var text = $("#search_input").val();
    if (text) {
        $("#search_result").show();
        $("#user_groups").hide();
        $.ajax({
            url: "/ChatRoom/home/Search?title=" + text,
            type: "get"
        }).done(function (data) {
            $("#search_result ul").html("");
            for (var i in data) {
                if (data[i].isUser) {
                    $("#search_result ul").append(`
                                 <li onclick="joinInPrivateGroup(${data[i].token})">
                                                                    ${data[i].title}
                                                               <img src="/content/images/user/thumb/${data[i].imageName}" />
                                                            <span></span>
                                                        </li>
                                        `);
                } else {
                    $("#search_result ul").append(`
                                         <li onclick="joinInGroup('${data[i].token}')">
                                                                    ${data[i].title}
                                                                    <img src="/content/images/user/thumb/${data[i].imageName}" />
                                                            <span></span>
                                                        </li>

                                        `);
                }
            }

        });
    } else {
        $("#search_result").hide();
        $("#user_groups").show();
    }
}

        //#endregion