//#region Other Functions

function ShowMessage(title, text, theme) {
    window.createNotification({
        closeOnClick: true,
        displayCloseButton: false,
        positionClass: 'nfc-bottom-right',
        showDuration: 5000,
        theme: theme !== '' ? theme : 'success',
    })({
        title: title !== '' ? title : 'اعلان',
        message: text
    });
}

function CopyToClipboardByElementId(elementId) {
    var $temp = $("<input>");
    $("body").append($temp);
    var el = $("#" + elementId);
    $temp.val($(el).text()).select();
    document.execCommand("copy");
    $temp.remove();
    ShowMessage('عملیات موفق', 'اطلاعات مورد نظر با موفقیت کپی شد');
}

function LocationReload(target) {
    location.href = target;
}

$('.disabledEnter').on('keyup keypress',
    function (e) {
        var keyCode = e.keyCode || e.which;
        if (keyCode === 13) {
            e.preventDefault();
            return false;
        }
    });

function FillPageId(id) {
    $("#Page").val(id);
    $("#filter-search").submit();
}

function DeleteAjax(removeElementId, url) {
    Swal.fire({
        title: 'اعلان',
        text: "آیا از انجام عملیات مورد نظر اطمینان دارید ؟",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله',
        cancelButtonText: 'خیر',
    }).then((result) => {
        if (result.isConfirmed) {
            $.get(url).then(res => {
                if (removeElementId !== null &&
                    removeElementId !== undefined &&
                    removeElementId !== '' &&
                    res.status === "Success") {
                    ShowMessage("اعلان", "عملیات با موفقیت انجام شد", "success");
                    $('[remove-ajax-item=' + removeElementId + ']').fadeOut(1000);
                } else if (removeElementId !== null &&
                    removeElementId !== undefined &&
                    removeElementId !== '' &&
                    res.status === "Error") {
                    ShowMessage("اعلان", "عملیات با شکست مواجه شد", "error");
                }
            });
        }
    });
}

function ConfigurationAlert(url) {
    Swal.fire({
        title: 'اعلان',
        text: "آیا از انجام عملیات مورد نظر اطمینان دارید ؟",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله',
        cancelButtonText: 'خیر',
    }).then((result) => {
        if (result.isConfirmed) {
            location.href = url;
        }
    });
}

//#endregion

//#region Change User Avatar

function ClickAvatarInput() {
    $("#UserAvatar").trigger("click");
}

function ChangeUserAvatar() {
    var imageInput = document.getElementById("UserAvatar");
    if (imageInput.files !== null && imageInput.files.length) {
        var valid = ['png', 'jpg', 'jpeg'];
        var file = imageInput.files[0];
        var exe = file.name.split('.').pop();
        if (valid.some(e => e === exe)) {
            var formData = new FormData();
            formData.append('file', file);
            var url = $("#UserAvatar").attr("data-url");
            $.ajax({
                url: url,
                type: 'post',
                data: formData,
                contentType: false,
                processData: false,
                beforeSend: function () { open_waiting(); },
                success: function (response) {
                    close_waiting();
                    if (response.status === "Success") {
                        location.reload();
                    } else {
                        ShowMessage("اعلان", "فایل انتخابی معتبر نیست .", "warning");
                    }
                }
            });
        } else {
            ShowMessage("اعلان", "فایل انتخابی معتبر نیست .", "warning");
        }
    }
}

//#endregion

//#region Change User Avatar

function DeleteUserAvatar(url) {
    Swal.fire({
        title: 'اعلان',
        text: "آیا از انجام عملیات مورد نظر اطمینان دارید ؟",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'بله',
        cancelButtonText: 'خیر',
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "post",
                data: {},
                beforeSend: function () {
                    open_waiting();
                },
                success: function (response) {
                    close_waiting();
                    if (response.status === "Success") {
                        location.reload();
                    }
                    else {
                        ShowMessage("خطا", "عملیات با خطا مواجه شد لطفا مجدد تلاش کنید .", "error");
                    }
                },
                error: function () {
                    close_waiting();
                    ShowMessage("خطا", "عملیات با خطا مواجه شد لطفا مجدد تلاش کنید .", "error");
                }
            });
        }
    });
}

//#endregion

//#region Counter

function countdown(id, minutes, seconds, functionName) {
    function tick() {
        var counter = document.getElementById(id);
        try {
            counter.innerHTML =
                (minutes < 10 ? "0" : "") + minutes.toString() + ":" + (seconds < 10 ? "0" : "") + String(seconds);
            seconds--;
            if (seconds >= 0) {
                setTimeout(tick, 1000);
            } else {
                if (minutes >= 1) {
                    setTimeout(function () {
                        countdown(minutes - 1, 59);
                    }, 1000);
                }
                else if (minutes === 0) {
                    functionName();
                }
            }
        } catch (e) {
            // Do Nothing 
        }
    }
    tick();
}

function RedirectToWalletList() {
    location.href = $("#TimerPaymentResult").attr("data-url");
}

//#endregion

//#region Waiting

function open_waiting(selector = 'body') {
    $(selector).waitMe({
        effect: 'win8',
        text: 'لطفا صبر کنید ...',
        bg: 'rgba(255,255,255,0.7)',
        color: '#000'
    });
}

function close_waiting(selector = 'body') {
    $(selector).waitMe('hide');
}

//#endregion

//#region Load Cities

$("#CountryId").change(function () {
    if ($("#CountryId :selected").val() !== '') {
        $('#CityId option:not(:first)').remove();
        $('#StateId option:not(:first)').remove();
        $.get("/Home/LoadCities", { stateId: $("#CountryId :selected").val() }).then(res => {
            if (res.data !== null) {
                $.each(res.data, function () {
                    $("#StateId").append(
                        '<option value=' + this.id + '>' + this.title + '</option>'
                    );
                });
                $("#StateId").removeAttr("disabled");
            }
        });
    } else {
        $('#StateId option:not(:first)').remove();
        $('#CityId option:not(:first)').remove();
    }
});

$("#StateId").change(function () {
    if ($("#StateId :selected").val() !== '') {
        $('#CityId option:not(:first)').remove();
        $.get("/Home/LoadCities", { stateId: $("#StateId :selected").val() }).then(res => {
            if (res.data !== null) {
                $.each(res.data, function () {
                    $("#CityId").append(
                        '<option value=' + this.id + '>' + this.title + '</option>'
                    );
                });
                $("#CityId").removeAttr("disabled");
            }
        });
    } else {
        $('#CityId option:not(:first)').remove();
    }
});

//#endregion

//#region Document Ready

$(function () {
    $("[ImageInput]").change(function () {
        var x = $(this).attr("ImageInput");
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $("[ImageFile=" + x + "]").attr('src', e.target.result);
            };
            reader.readAsDataURL(this.files[0]);
        }
    });

    $('[remove-ajax-button]').on('click',
        function (e) {
            e.preventDefault();
            var url = $(this).attr('href');
            var removeElementId = $(this).attr('remove-ajax-button');
            Swal.fire({
                title: 'اعلان',
                text: "آیا از انجام عملیات مورد نظر اطمینان دارید ؟",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'بله',
                cancelButtonText: 'خیر',
            }).then((result) => {
                if (result.isConfirmed) {
                    $.get(url).then(res => {
                        if (removeElementId !== null &&
                            removeElementId !== undefined &&
                            removeElementId !== '' &&
                            res.status === "Success") {
                            ShowMessage("اعلان", "عملیات با موفقیت انجام شد", "success");
                            $('[remove-ajax-item=' + removeElementId + ']').fadeOut(1000);
                        } else if (removeElementId !== null &&
                            removeElementId !== undefined &&
                            removeElementId !== '' &&
                            res.status === "Error") {
                            ShowMessage("اعلان", "عملیات با شکست مواجه شد", "error");
                        }
                    });
                }
            });
        });

    var datePickers = $("[DatePicker]");
    if (datePickers.length > 0) {
        $('<link/>',
            {
                rel: 'stylesheet',
                type: 'text/css',
                href: '/common/kamadatepicker/kamadatepicker.min.css'
            }).appendTo('head');
        $.getScript("/common/kamadatepicker/kamadatepicker.min.js", function (script, textStatus, jqXHR) { });
    }

    var numericInputs = $("[NumericInput]");
    if (numericInputs.length > 0) {
        $.getScript("/common/autonumeric/AutoNumeric.js",
            function (script, textStatus, jqXHR) {
                $(numericInputs).each(function (index, value) {
                    var id = $(value).attr('NumericInput');
                    new AutoNumeric(document.querySelector('[NumericInput="' + id + '"]'),
                        {
                            currencySymbol: '  ریال  ',
                            outputFormat: "number",
                            allowDecimalPadding: false,
                            currencySymbolPlacement: "s",
                            digitGroupSeparator: ',',
                            unformatOnSubmit: true,
                            wheelStep: "100000"
                        });
                });
            });
    }

    var timers = $("[timer]");
    if (timers.length > 0) {
        $.getScript("/common/jQuery-Simple-Timer/jquery.simple.timer.js",
            function (script, textStatus, jqXHR) {
                $(timers).each(function (index, value) {
                    var id = $(value).attr('timer');
                    $(value).css("direction", "ltr");
                    $('[timer="' + id + '"]').startTimer({
                        elementContainer: "span",
                        onComplete: function () {
                            location.reload();
                        }
                    });
                });
            });
    }
    //#region CkEditor

    var editors = $("[ckeditor]");
    if (editors.length > 0) {
        $.getScript("/common/ckeditor/build/ckeditor.js",
            function () {
                $(editors).each(function (index, value) {
                    var id = $(value).attr('ckeditor');
                    var data = $(value).attr('ck-data');
                    var lang = $(value).attr('ck-lang');
                    if (lang === "fa") {
                        ClassicEditor.create(document.querySelector('[ckeditor="' + id + '"]'),
                            {
                                toolbar: {
                                    items: [
                                        'heading',
                                        '|',
                                        'bold',
                                        'italic',
                                        'underline',
                                        'blockQuote',
                                        'link',
                                        '|',
                                        'fontColor',
                                        'fontSize',
                                        '|',
                                        'alignment',
                                        'numberedList',
                                        'bulletedList',
                                        'indent',
                                        'outdent',
                                        '|',
                                        'imageUpload',
                                        'insertTable',
                                        '|',
                                        'codeBlock',
                                        'removeFormat',
                                    ]
                                },
                                language: lang,
                                image: {
                                    toolbar: [
                                        'imageTextAlternative',
                                        'imageStyle:full',
                                        'imageStyle:side'
                                    ]
                                },
                                table: {
                                    contentToolbar: [
                                        'tableColumn',
                                        'tableRow',
                                        'mergeTableCells',
                                        'tableCellProperties',
                                        'tableProperties'
                                    ]
                                },
                                simpleUpload: {
                                    uploadUrl: '/Uploader/UploadImage'
                                },
                                licenseKey: '',
                            })
                            .then(editor => {
                                window.editor = editor;
                                if (data !== undefined && data !== null) {
                                    editor.setData(data);
                                }
                            })
                            .catch(error => {
                                console.error(error);
                            });
                    }
                    else {
                        $.getScript(`/common/ckeditor/build/translations/${lang}.js`, function () {
                            ClassicEditor.create(document.querySelector('[ckeditor="' + id + '"]'),
                                {
                                    toolbar: {
                                        items: [
                                            'heading',
                                            '|',
                                            'bold',
                                            'italic',
                                            'underline',
                                            'blockQuote',
                                            'link',
                                            '|',
                                            'fontColor',
                                            'fontSize',
                                            '|',
                                            'alignment',
                                            'numberedList',
                                            'bulletedList',
                                            'indent',
                                            'outdent',
                                            '|',
                                            'imageUpload',
                                            'insertTable',
                                            '|',
                                            'codeBlock',
                                            'removeFormat',
                                        ]
                                    },
                                    language: lang,
                                    image: {
                                        toolbar: [
                                            'imageTextAlternative',
                                            'imageStyle:full',
                                            'imageStyle:side'
                                        ]
                                    },
                                    table: {
                                        contentToolbar: [
                                            'tableColumn',
                                            'tableRow',
                                            'mergeTableCells',
                                            'tableCellProperties',
                                            'tableProperties'
                                        ]
                                    },
                                    simpleUpload: {
                                        uploadUrl: '/Uploader/UploadImage'
                                    },
                                    licenseKey: '',
                                })
                                .then(editor => {
                                    window.editor = editor;
                                    if (data !== undefined && data !== null) {
                                        editor.setData(data);
                                    }
                                })
                                .catch(error => {
                                    console.error(error);
                                });
                        });
                    }
                });
            });
    }

    //#endregion

    var timePickers = $("[TimePicker]");
    if (timePickers.length > 0) {
        $('<link/>',
            {
                rel: 'stylesheet',
                type: 'text/css',
                href: '/common/timepicker/mdtimepicker.css'
            }).appendTo('head');
        $.getScript("/common/timepicker/mdtimepicker.js",
            function (script, textStatus, jqXHR) {
                $(timePickers).each(function (index, value) {
                    var id = $(value).attr('TimePicker');
                    $('[TimePicker="' + id + '"]').mdtimepicker({
                        // time format
                        timeFormat: 'hh:mm:ss.000',
                        // format of the input value
                        format: 'hh:mm:ss',
                        // readonly mode
                        readOnly: true,
                        // determines if display value has zero padding for hour value less than 10 (i.e. 05:30 PM); 24-hour format has padding by default
                        hourPadding: false,
                        // theme of the timepicker
                        // 'red', 'purple', 'indigo', 'teal', 'green', 'dark'
                        theme: 'teal',
                        // custom label text
                        okLabel: 'تائید',
                        cancelLabel: 'انصراف',
                    });
                });
            });
    }

    var tagifys = $("[tagify]");
    if (tagifys.length > 0) {
        $('<link/>',
            {
                rel: 'stylesheet',
                type: 'text/css',
                href: '/common/tagify-master/dist/tagify.css'
            }).appendTo('head');
        $.getScript("/common/tagify-master/dist/jQuery.tagify.min.js",
            function (script, textStatus, jqXHR) {
                $(tagifys).each(function (index, value) {
                    var id = $(value).attr('tagify');
                    new Tagify(document.querySelector('[tagify="' + id + '"]'),
                        {
                            duplicates: false,
                            trim: true,
                            delimiters: ",",
                            originalInputValueFormat: valueArr => valueArr.map(item => item.value).join(', ')
                        });
                });
            });
    }

    countdown("TimerPaymentResult", 0, 20, RedirectToWalletList);
});

//#endregion

//#region Filter Articles

function FilterArticlesForm(id) {
    $("#ArticleCategoryId").val(id);
    $("#filter-search").submit();
}

function FilterCoursesForm(id) {
    $("#CategoryId").val(id);
    $("#filter-search").submit();
}

//#endregion

//#region Document Ready

$(function () {
    var adminDatePickers = $("[AdminDatePicker]");
    if (adminDatePickers.length > 0) {
        $('<link/>',
            {
                rel: 'stylesheet',
                type: 'text/css',
                href: '/common/admindatapicker/kamadatepicker.min.css'
            }).appendTo('head');
        $.getScript("/common/admindatapicker/kamadatepicker.min.js", function (script, textStatus, jqXHR) { });
    }
});

//#endregion

//#region Document Ready

$(function () {
    var DatePickers = $("[DatePicker]");
    if (DatePickers.length > 0) {
        $('<link/>',
            {
                rel: 'stylesheet',
                type: 'text/css',
                href: '/common/admindatapicker/kamadatepicker.min.css'
            }).appendTo('head');
        $.getScript("/common/admindatapicker/kamadatepicker.min.js", function (script, textStatus, jqXHR) { });
    }
});

//#endregion

//#region Replace Persion Number to English In Input

$(function () {
    $('input').keyup(function (e) {
        var ctrlKey = 67, vKey = 86;
        if (e.keyCode != ctrlKey && e.keyCode != vKey) {
            $(this).val(persianToEnglish($(this).val()));
        //            console.log($(this).val());
        }
    });
});
function persianToEnglish(input) {
    var inputstring = input;
    var persian = ["۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹"]
    var english = ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]
    for (var i = 0; i < 10; i++) {
        inputstring = inputstring.toString().replace(persian[i], english[i]);
    }
    return inputstring;
}

//#endregion