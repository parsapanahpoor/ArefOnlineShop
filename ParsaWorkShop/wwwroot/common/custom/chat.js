
//#region Get Some Elements

var submitBtn = document.getElementById("SubmitBtn");
var messageInput = document.getElementById("MessageInput");
var fileInput = document.getElementById("FileInput");
var chatRoomId = document.getElementById("ChatRoomIdInput").value;
var userId = document.getElementById("UserId").value;

//#endregion

//#region Create Connection

var connection = new signalR.HubConnectionBuilder()
    .withAutomaticReconnect()
    .withUrl(`/hubs/chat?chatRoomId=${chatRoomId}`)
    .build();

//#endregion

//#region Handle Connection Problems

connection.onclose(error => {
    ShowMessage("اعلان", "اتصال با خطا مواجه شد .", "error");
});

connection.onreconnected(connectionId => {
    ShowMessage("اعلان", "اتصال مجدد برقرار شد .", "success");
});

connection.onreconnecting(error => {
    ShowMessage("اعلان", "در حال اتصال ...", "info");
});

//#endregion

//#region Send Message To Server

submitBtn.addEventListener("click", (event) => {
    var message = messageInput.value;
    messageInput.value = "";
    if (message !== "" && message.length && message !== undefined) {
        connection.invoke("SendMessage", message, chatRoomId, '').then(() => { }, (error) => { console.log(error) });
    }
});

//#endregion

//#region Send File To Server

fileInput.addEventListener("change", () => {
    if (!fileInput.files.length) {
        return;
    }

    // Enable Btn
    $("#ModalSubmitBtn").prop("disabled", false);

    // get file 
    var file = fileInput.files[0];

    if (GetFileSizeInMb(file) > 100) {
        showMessage("اعلان", "فایل انتخابی نمی تواند بیشتر از 100 Mb باشد .", "warning");
        return;
    }

    // get file info
    var fileName = file.name;
    var fileSize = GetFileSizeInMb(file);
    var fileType = file.type;
    if (fileType === '' || !fileType.length) {
        fileType = "Unknown"
    }

    // empty modal
    $("#ProgressBar").removeClass("progress-bar-danger");
    $("#ProgressBar").addClass("progress-bar-info");
    $("#BoxProgressBar").addClass("display-none");
    $("#ModalCaption").val(null);

    // fill image src by file type
    FillImageSrcByFileType(file);

    // fill file info in modal
    $("#FileNameSpan").html(`نام فایل : ${fileName}`);
    $("#FileSizeSpan").html(`حجم فایل : ${fileSize}`);
    $("#FileTypeSpan").html(`نوع فایل : ${fileType}`);

    // empty file input
    $("#FileInput").val(null);

    // Disable All Events on Send Btn
    $("#ModalSubmitBtn").off();

    // Show Upload Modal
    $("#SelectFileModal").modal("show");

    // manage file selected
    $("#ModalSubmitBtn").click(function () {
        ModalSubmitButtonClick(file, $("#FileInput").attr("data-url"));
    });
});

function ModalSubmitButtonClick(file, url) {
    // Disable Btn
    $("#ModalSubmitBtn").prop("disabled", true);

    // Create Form Data
    const formData = new FormData();
    formData.append("file", file);

    // Send File To Server By Ajax
    $.ajax({
        type: "post",
        url: url,
        data: formData,
        xhr: function () {
            const xhr = new window.XMLHttpRequest();
            xhr.upload.addEventListener('progress', e => {
                if (e.lengthComputable) {
                    const percent = e.loaded / e.total * 100;
                    $("#BoxProgressBar").removeClass("display-none");
                    $("#ProgressBar").css("width", `${percent}%`);
                    $("#ProgressBar").attr("aria-valuenow", percent);
                    $("#ProgressBar .percent").html(`${percent.toFixed(1)}%`);
                }
            });
            return xhr;
        },
        beforeSend: function () {
            ShowMessage("اعلان", "لطفا تا انتهای بارگذاری صبر کنید ...", "info");
        },
        success: function (response) {
            if (response.status === "Success") {
                // Show Success Messages
                ShowMessage("اعلان", "عملیات با موفقیت انجام شد .", "success");

                // Add New Classes To ProgressBar
                $("#ProgressBar").removeClass("progress-bar-info");
                $("#ProgressBar").addClass("progress-bar-success");

                // Hide Modal
                $("#SelectFileModal").modal("hide");

                // Now Send Data By SignalR
                var message = $("#ModalCaption").val();
                var fileName = response.data;
                connection.invoke("SendMessage", message, chatRoomId, fileName).then(() => { }, (error) => { console.log(error) });
            }
            else if (response.status === "Error") {
                ShowMessage("خطا", "فرمت فایل معتبر نیست .", "error");
                $("#ProgressBar").removeClass("progress-bar-info");
                $("#ProgressBar").addClass("progress-bar-danger");
            }
        },
        error: function () {
            ShowMessage("خطا", "در بارگذاری فایل خطایی رخ داده است .", "error");
            $("#ProgressBar").removeClass("progress-bar-info");
            $("#ProgressBar").addClass("progress-bar-danger");
        },
        contentType: false,
        processData: false
    })
}

//#endregion

//#region Get Response From Server

connection.on("SendMessageToClient", result => {
    if (!result.hasFile) {
        if (result.userId.toString() === userId.toString()) {
            $("#MessagesBox").append(OwnerMessageDiv(result));
        }
        else {
            $("#MessagesBox").append(OthersMessageDiv(result));
        }
    }
    else {
        $("#MessagesBox").append(SelectDivByType(result));
    }

    var element = $("#ScrollBox");
    element.animate({scrollTop: element[0].scrollHeight});
});

connection.on("LoadAllMessages", data => {
    $("#MessagesBox").empty();
    $.each(data, function (index, result) {
        if (!result.hasFile) {
            if (result.userId.toString() === userId.toString()) {
                $("#MessagesBox").append(OwnerMessageDiv(result));
            }
            else {
                $("#MessagesBox").append(OthersMessageDiv(result));
            }
        }
        else {
            $("#MessagesBox").append(SelectDivByType(result));
        }
    });

    var element = $("#ScrollBox");
    element.animate({ scrollTop: element[0].scrollHeight }, 500);
});

connection.on("ChangeUserOnlineStatus", data => {
    if (data.status === "Online") {
        $(`div[data-user-id='${data.userId}']`).each(function (index, value) {
            $(value).removeClass("Offline_Box");
            $(value).addClass("Online_Box");
        });
    }
    else if (data.status === "Offline") {
        $(`div[data-user-id='${data.userId}']`).each(function (index, value) {
            $(value).removeClass("Online_Box");
            $(value).addClass("Offline_Box");
        });
    }
});

connection.on("GetOnlineUsersCount", data => {
    $("#OnlineUsersCount").html(data);
});

//#endregion

//#region Start Connection

function StartSuccess() {
    console.log("Connection Start Successfully .");
}

function StartFail() {
    console.log("Connection Failed .");
}

connection.start().then(StartSuccess, StartFail);

//#endregion

//#region Some Js Code

$("#MessageInput").keydown(function (e) {
    if (e.keyCode === 13) {
        e.preventDefault();
        $("#SubmitBtn").click();
    }
});

$("#SelectFileIcon").click(function () {
    $("#FileInput").trigger("click");
});

function ImageClicked(value) {
    $("#full-image").attr("src", $(value).attr("src"));
    $('#image-viewer').show();
}

$("#image-viewer .close").click(function () {
    $('#image-viewer').hide();
});

function GetFileSizeInMb(file) {
    return `${(file.size / (1024 * 1024)).toFixed(2)} Mb`;
}

function GetFileEXE(file) {
    return file.name.split('.').pop();
}

function GetFileEXEByName(fileName) {
    return fileName.split('.').pop();
}

function FillImageSrcByFileType(file) {
    var fileEXE = GetFileEXE(file);

    if (fileEXE === "png" || fileEXE === "jpg" || fileEXE === "jpeg") {
        var reader = new FileReader();
        reader.onload = function (e) {
            $("#ModalImagePreview").attr('src', e.target.result);
        };
        reader.readAsDataURL(file);
    }
    else if (fileEXE === "mp3" || fileEXE === "wav") {
        $("#ModalImagePreview").attr('src', "/content/FileTypeImages/sound.png");
    }
    else if (fileEXE === "mp4") {
        $("#ModalImagePreview").attr('src', "/content/FileTypeImages/video.png");
    }
    else {
        $("#ModalImagePreview").attr('src', "/content/FileTypeImages/file.png");
    }
}

function OwnerMessageDiv(message) {
    var result = `
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <section>
                <div class="Box_User_Chat">
                    <div class="Img_profile_Box_User_Chat"><img src="${message.avatarAddress}"><div class="Offline_Box shadow-style" data-user-id=${message.userId}></div></div>
                    <div class="Part_Body_Chat_text_Box_User_Chat">
                        <span class="Name_user">${message.userShowName}</span>
                        <div class="Body_Chat_text_Box_User_Chat">
                            <div class="Date_info_Body_Chat_text_Box_User_Chat">
                                <div><span class="Data_Date_info_Body_Chat_text_Box_User_Chat">${message.createDate}</span></div>
                                <div><span class="Time_Date_info_Body_Chat_text_Box_User_Chat">${message.createTime}</span></div>
                            </div>
                            <div class="Text_Chat">
                                <span>${message.message}</span>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    `;

    return result;
}

function OthersMessageDiv(message) {
    var result = `
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="Box_Support_Chat">
                <div class="Img_profile_Box_Support_Chat"><img src="${message.avatarAddress}"><div class="Offline_Box shadow-style" data-user-id=${message.userId}></div></div>
                <div>
                    <span class="Name_user">${message.userShowName}</span>
                    <div class="Body_Chat_text_Box_Support_Chat">
                        <div class="Date_info_Body_Chat_text_Box_Support_Chat">
                            <div><span class="Data_Date_info_Body_Chat_text_Box_Support_Chat">${message.createDate}</span></div>
                            <div><span class="Time_Date_info_Body_Chat_text_Box_Support_Chat">${message.createTime}</span></div>
                        </div>
                        <div class="Text_Chat">
                            <span class="Text_Answer_Box_Support_Chat">${message.message}</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `;

    return result;
}

function OwnerImageDiv(message) {
    messagePart = '';

    if (message.message !== '' && message.message.length) {
        messagePart = `
            <div class="Part_Span_Chat_Caption">
                <span class="Span_Chat_Caption">${message.message}</span>
            </div>
        `;
    }

    var result = `
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="Box_User_Chat">
                <div class="Img_profile_Box_User_Chat"><img src="${message.avatarAddress}"><div class="Offline_Box shadow-style" data-user-id=${message.userId}></div></div>
                <div>
                    <span class="Name_user">${message.userShowName}</span>
                    <div class="Body_Chat_text_Box_User_Chat">
                        <div class="Date_info_Body_Chat_text_Box_User_Chat">
                            <div><span class="Data_Date_info_Body_Chat_text_Box_User_Chat">${message.createDate}</span></div>
                            <div><span class="Time_Date_info_Body_Chat_text_Box_User_Chat">${message.createTime}</span></div>
                        </div>
                        <div class="Main_Image_Chat">
                            <div class="Image_Chat">
                                <img onclick="ImageClicked($(this))" src="${message.fileName}">
                            </div>
                            ${messagePart}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `;

    return result;
}

function OthersImageDiv(message) {
    messagePart = '';

    if (message.message !== '' && message.message.length) {
        messagePart = `
            <div class="Part_Span_Chat_Caption">
                <span class="Span_Chat_Caption_Support">${message.message}</span>
            </div>
        `;
    }

    var result = `
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="Box_Support_Chat">
                <div class="Img_profile_Box_Support_Chat"><img src="${message.avatarAddress}"><div class="Offline_Box shadow-style" data-user-id=${message.userId}></div></div>
                <div>
                    <span class="Name_user">${message.userShowName}</span>
                    <div class="Body_Chat_text_Box_Support_Chat">
                        <div class="Date_info_Body_Chat_text_Box_Support_Chat">
                            <div><span class="Data_Date_info_Body_Chat_text_Box_Support_Chat">${message.createDate}</span></div>
                            <div><span class="Time_Date_info_Body_Chat_text_Box_Support_Chat">${message.createTime}</span></div>
                        </div>
                        <div class="Main_Image_Chat">
                            <div class="Image_Chat">
                                <img onclick="ImageClicked($(this))" src="${message.fileName}">
                            </div>
                            ${messagePart}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `;

    return result;
}

function OwnerVideoDiv(message) {
    messagePart = '';

    if (message.message !== '' && message.message.length) {
        messagePart = `
            <div class="Part_Span_Chat_Caption">
                <span class="Span_Chat_Caption">${message.message}</span>
            </div>
        `;
    }

    var result = `
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="Box_User_Chat">
                <div class="Img_profile_Box_User_Chat"><img src="${message.avatarAddress}"><div class="Offline_Box shadow-style" data-user-id=${message.userId}></div></div>
                <div>
                    <span class="Name_user">${message.userShowName}</span>
                    <div class="Body_Chat_text_Box_User_Chat">
                        <div class="Date_info_Body_Chat_text_Box_User_Chat">
                            <div><span class="Data_Date_info_Body_Chat_text_Box_User_Chat">${message.createDate}</span></div>
                            <div><span class="Time_Date_info_Body_Chat_text_Box_User_Chat">${message.createTime}</span></div>
                        </div>
                        <div class="Main_Video_Chat">
                            <video class="Part_video_chat" controls>
								<source src="${message.fileName}" type="video/mp4">
							</video>
                            ${messagePart}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `;

    return result;
}

function OthersVideoDiv(message) {
    messagePart = '';

    if (message.message !== '' && message.message.length) {
        messagePart = `
            <div class="Part_Span_Chat_Caption">
                <span class="Span_Chat_Caption_Support">${message.message}</span>
            </div>
        `;
    }

    var result = `
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="Box_Support_Chat">
                <div class="Img_profile_Box_Support_Chat"><img src="${message.avatarAddress}"><div class="Offline_Box shadow-style" data-user-id=${message.userId}></div></div>
                <div>
                    <span class="Name_user">${message.userShowName}</span>
                    <div class="Body_Chat_text_Box_Support_Chat">
                        <div class="Date_info_Body_Chat_text_Box_Support_Chat">
                            <div><span class="Data_Date_info_Body_Chat_text_Box_Support_Chat">${message.createDate}</span></div>
                            <div><span class="Time_Date_info_Body_Chat_text_Box_Support_Chat">${message.createTime}</span></div>
                        </div>
                        <div class="Main_Video_Chat">
                            <video class="Part_video_chat" controls="">
								<source src="${message.fileName}" type="video/mp4">
							</video>
                            ${messagePart}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `;

    return result;
}

function OwnerAudioDiv(message) {
    messagePart = '';

    if (message.message !== '' && message.message.length) {
        messagePart = `
            <div class="Part_Span_Chat_Caption">
                <span class="Span_Chat_Caption">${message.message}</span>
            </div>
        `;
    }

    var result = `
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="Box_User_Chat">
                <div class="Img_profile_Box_User_Chat"><img src="${message.avatarAddress}"><div class="Offline_Box shadow-style" data-user-id=${message.userId}></div></div>
                <div>
                    <span class="Name_user">${message.userShowName}</span>
                    <div class="Body_Chat_text_Box_User_Chat">
                        <div class="Date_info_Body_Chat_text_Box_User_Chat">
                            <div><span class="Data_Date_info_Body_Chat_text_Box_User_Chat">${message.createDate}</span></div>
                            <div><span class="Time_Date_info_Body_Chat_text_Box_User_Chat">${message.createTime}</span></div>
                        </div>
                        <div class="Main_Vice_Chat">
                            <audio class="Part_audio_chat" controls="">
					            <source src="${message.fileName}" type="audio/ogg">
					        </audio>
                            ${messagePart}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `;

    return result;
}

function OthersAudioDiv(message) {
    messagePart = '';

    if (message.message !== '' && message.message.length) {
        messagePart = `
            <div class="Part_Span_Chat_Caption">
                <span class="Span_Chat_Caption_Support">${message.message}</span>
            </div>
        `;
    }

    var result = `
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="Box_Support_Chat">
                <div class="Img_profile_Box_Support_Chat"><img src="${message.avatarAddress}"><div class="Offline_Box shadow-style" data-user-id=${message.userId}></div></div>
                <div>
                    <span class="Name_user">${message.userShowName}</span>
                    <div class="Body_Chat_text_Box_Support_Chat">
                        <div class="Date_info_Body_Chat_text_Box_Support_Chat">
                            <div><span class="Data_Date_info_Body_Chat_text_Box_Support_Chat">${message.createDate}</span></div>
                            <div><span class="Time_Date_info_Body_Chat_text_Box_Support_Chat">${message.createTime}</span></div>
                        </div>
                        <div class="Main_Vice_Chat">
                            <audio class="Part_audio_chat" controls="">
					            <source src="${message.fileName}" type="audio/ogg">
					        </audio>
                            ${messagePart}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `;

    return result;
}

function OwnerFileDiv(message) {
    messagePart = '';

    if (message.message !== '' && message.message.length) {
        messagePart = `
            <div class="Part_Span_Chat_Caption">
                <span class="Span_Chat_Caption">${message.message}</span>
            </div>
        `;
    }

    var result = `
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="Box_User_Chat">
                <div class="Img_profile_Box_User_Chat"><img src="${message.avatarAddress}"><div class="Offline_Box shadow-style" data-user-id=${message.userId}></div></div>
                <div>
                    <span class="Name_user">${message.userShowName}</span>
                    <div class="Body_Chat_text_Box_User_Chat">
                        <div class="Date_info_Body_Chat_text_Box_User_Chat">
                            <div><span class="Data_Date_info_Body_Chat_text_Box_User_Chat">${message.createDate}</span></div>
                            <div><span class="Time_Date_info_Body_Chat_text_Box_User_Chat">${message.createTime}</span></div>
                        </div>
                        <div class="Main_Vice_Chat">
                            <button style="min-width: 150px" class="btn btn-success">دانلود فایل</button>
                            ${messagePart}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `;

    return result;
}

function OthersFileDiv(message) {
    messagePart = '';

    if (message.message !== '' && message.message.length) {
        messagePart = `
            <div class="Part_Span_Chat_Caption">
                <span class="Span_Chat_Caption_Support">${message.message}</span>
            </div>
        `;
    }

    var result = `
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="Box_Support_Chat">
                <div class="Img_profile_Box_Support_Chat"><img src="${message.avatarAddress}"><div class="Offline_Box shadow-style" data-user-id=${message.userId}></div></div>
                <div>
                    <span class="Name_user">${message.userShowName}</span>
                    <div class="Body_Chat_text_Box_Support_Chat">
                        <div class="Date_info_Body_Chat_text_Box_Support_Chat">
                            <div><span class="Data_Date_info_Body_Chat_text_Box_Support_Chat">${message.createDate}</span></div>
                            <div><span class="Time_Date_info_Body_Chat_text_Box_Support_Chat">${message.createTime}</span></div>
                        </div>
                        <div class="Main_Vice_Chat">
                            <button style="min-width: 150px" class="btn btn-success">دانلود فایل</button>
                            ${messagePart}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    `;

    return result;
}

function SelectDivByType(message) {
    fileEXE = GetFileEXEByName(message.fileName);

    if (fileEXE === 'png' || fileEXE === 'jpg' || fileEXE === 'jpeg') {
        if (message.userId.toString() === userId.toString()) {
            return OwnerImageDiv(message);
        }
        else {
            return OthersImageDiv(message);
        }
    }

    if (fileEXE === 'mp3' || fileEXE === 'wav') {
        if (message.userId.toString() === userId.toString()) {
            return OwnerAudioDiv(message);
        }
        else {
            return OthersAudioDiv(message);
        }
    }

    if (fileEXE === 'mp4') {
        if (message.userId.toString() === userId.toString()) {
            return OwnerVideoDiv(message);
        }
        else {
            return OthersVideoDiv(message);
        }
    }

    if (message.userId.toString() === userId.toString()) {
        return OwnerFileDiv(message);
    }
    else {
        return OthersFileDiv(message);
    }
}

//#endregion
