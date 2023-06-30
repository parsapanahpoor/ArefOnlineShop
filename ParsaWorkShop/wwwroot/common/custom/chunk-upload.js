
//#region common variables

let fileUploaded = false;
let baseProcessBar = " .progress .progress-bar";

//#endregion

//#region convert byte to size fileUpload

function bytesToSize(bytes) {
    let sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB'];
    if (bytes === 0) return '0 Byte';
    let i = parseInt(Math.floor(Math.log(bytes) / Math.log(1024)));
    return Math.round(bytes / Math.pow(1024, i), 2) + sizes[i];
}

//#endregion

//#region uuidv4

function uuidv4() {
    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
        (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    );
}

//#endregion

//#region read files

function readfiles(files, element) {
    
    if (files.length < 1) {
        return;
    }

    let upload = {
        "file": files[0],
        "baseElement": element,
        "uploadUrl": GetChunkUploadUrl(element),
        "inputId": GetChunkInputId(element)
    };

    $("#" + element).removeClass("display-none");
    $("#SubmitBtn").prop("disabled", true);
    $(GetDownloadBoxId(element)).css("display", "none");
    
    UploadFile(upload);
}

//#endregion

//#region upload file chunk

function UploadFileChunk(upload) {
    
    let formData = new FormData();
    
    formData.append('videoFile', upload.chunk, upload.fileName);
    formData.append('partCount', upload.currentChunkCount);
    formData.append('totalParts', upload.totalChunkCount);
    formData.append("upload_file", true);

    $.ajax({
        type: "POST",
        url: upload.uploadUrl,
        success: function (data) {
            if (data.status === "Success") {
                let percent = 0;
                let progressBarId = GetProcessBarId(upload.baseElement);

                if (upload.currentChunkCount > 0) {
                    percent = Math.ceil(upload.currentChunkCount * 100 / upload.totalChunkCount);
                }

                upload.uploadedCount += 1;
                $(progressBarId).css("width", percent + "%");
                $(progressBarId + " .percent").html(percent + " % ");
                $(progressBarId).attr("aria-valuenow", percent);

                let chunk = upload.fileChunks.shift();
                
                if (chunk !== undefined) {
                    upload.chunk = chunk;
                    upload.currentChunkCount += 1;
                    upload.fileName = upload.suffixFileName + ".part_" + upload.currentChunkCount + "." + upload.totalChunkCount;
                    UploadFileChunk(upload);
                }
                
                if (data.data !== undefined && data.data != null && data.data.file !== "" && data.data.length) {
                    fileUploaded = true;
                    $(progressBarId).removeClass("progress-bar-blue");
                    $(progressBarId).addClass("progress-bar-success");
                    $("#" + upload.inputId).val(data.data);
                    $("#SubmitBtn").prop("disabled", false);
                    ShowMessage("اعلان", "عملیات با موفقیت انجام شد .", "success");
                }

            } else if (data.status === "Error") {
                ShowMessage("هشدار", "خطایی رخ داده است لطفا مجدد تلاش کنید .", "error");
            }
        },
        error: function (error) {
            UploadFileChunk(upload);
        },
        async: true,
        data: formData,
        cache: false,
        contentType: false,
        processData: false
    });
}

//#endregion

//#region create array to store the buffer chunks

function UploadFile(upload) {
    // set up other initial vars
    let MaxFileSizeMB = 1;
    let BufferChunkSize = MaxFileSizeMB * (1024 * 1024);
    let ReadBuffer_Size = 1024;
    let FileStreamPos = 0;
    let EndPos = BufferChunkSize;
    let Size = upload.file.size;

    let FileChunk = [];
    
    // add to the FileChunk array until we get to the end of the file
    while (FileStreamPos < Size) {
        // "slice" the file from the starting position/offset, to  the required length
        FileChunk.push(upload.file.slice(FileStreamPos, EndPos));
        FileStreamPos = EndPos; // jump by the amount read
        EndPos = FileStreamPos + BufferChunkSize; // set next chunk length
    }

    upload.fileChunks = FileChunk;

    // get total number of "files" we will be sending
    let totalParts = upload.fileChunks.length;
    
    // loop through, pulling the first item from the array each time and sending it
    let suffixFileName = uuidv4() + upload.file.name;
    
    upload.chunk = upload.fileChunks.shift();
    upload.totalChunkCount = totalParts;
    upload.currentChunkCount = 1;
    upload.fileName = suffixFileName + ".part_" + upload.currentChunkCount + "." + totalParts;
    upload.suffixFileName = suffixFileName;
    upload.uploadedCount = 0;

    UploadFileChunk(upload);
}

//#endregion

//#region document ready

$(function (){

    let chunks = $("[chunk-upload]");
    if (chunks.length > 0) {
        $(chunks).each(function (index, value) {
            $(value).change(function () {
                let files = value.files;
                let element = $(value).attr('chunk-upload');
                readfiles(files, element);
            })
        });
    }

});

function ChunkUpload(element) {
    var files = document.getElementById(`${element}Input`).files;
    readfiles(files, element);
}

function GetChunkUploadUrl(element) {
    return $("#" + element).attr("upload-url");
}

function GetChunkInputId(element) {
    return $("#" + element).attr("inputId");
}

function GetProcessBarId(element) {
    return "#" + element + baseProcessBar;
}

function GetDownloadBoxId(element) {
    return "#" + element + "DownloadBox";
}

//#endregion








