var jqXHRData;
function initSimpleFileUpload() {
    'use strict';
    $('#fu-my-simple-upload').fileupload({
        url: '/api/readerapi/addreadersfromfile',
        dataType: 'json',
        replaceFileInput: false,
        add: function (e, data) {
            console.log(data);
            jqXHRData = data;
            $(this).html(data.files[0].name);
        },
        done: function (event, data) {
            if (data.result.isUploaded) {
                $("#dialog").dialog({
                    modal: true,
                    buttons: {
                        Ok: function () {
                            $(this).dialog("close");
                        }
                    }
                }
                );
                $("#tabs-1").html("<pre>" + data.result.added + "</pre>");
                $("#tabs-2").html("<pre>" + data.result.errors + "</pre>");
                $("#tabs").tabs();
            } else {
                $("#dialog").html(data.result.errors);
                $("#dialog").dialog({
                    modal: true,
                    buttons: {
                        Ok: function () {
                            $(this).dialog("close");
                        }
                    }
                }
                );
            }
        },
    });
    
}