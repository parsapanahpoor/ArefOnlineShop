//#region Document Ready

$(function () {
    var adminDatePickers = $("[AdminDatePicker]");
    if (adminDatePickers.length > 0) {
        $('<link/>',
            {
                rel: 'stylesheet',
                type: 'text/css',
                href: '/lib/admindatapicker/kamadatepicker.min.css'
            }).appendTo('head');
        $.getScript("/lib/admindatapicker/kamadatepicker.min.js", function (script, textStatus, jqXHR) { });
    }
});

//#endregion

//#region PageId For Pagings

function FillPageId(id) {
    $("#Page").val(id);
    $("#filter-search").submit();
}

//#endregion

//#region Show Notification

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

//#endregion

//#region tagify

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

//#region CkEditor

var editors = $("[ckeditor]");
if (editors.length > 0) {
    $.getScript("/lib/ckeditor/build/ckeditor.js",
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
                    $.getScript(`/lib/ckeditor/build/translations/${lang}.js`, function () {
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

//#region image preview

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
});

//#endregion

//#region auto numeric

var numericInputs = $("[NumericInput]");
if (numericInputs.length > 0) {
    $.getScript("/lib/autonumeric/AutoNumeric.js",
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
                        wheelStep: "1000"
                    });
            });
        });
}

//#endregion        

//#region Textarea

var textareas = $("[textarea]");
    if (textareas.length > 0) {
        $(textareas).each(function (index, value) {
            var id = $(value).attr('textarea');
            var data = $(value).attr('text-data');
            document.querySelector('[textarea="' + id + '"]').value = data;
        });
    }

//#endregion

//#region Search User Modal

function showNotificationToUser(notificationId) {
    $.ajax({
        url: "/User/Home/ShowNotificationToUser",
        type: "post",
        data: {
            notificationId: notificationId
        }
    });
}

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