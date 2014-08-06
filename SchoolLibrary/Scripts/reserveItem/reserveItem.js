//function makes "Delete checked"-button visible or invisible
function setButtonVisible() {

    var isAnyChecked = false;

    var checkBoxes = $(".bookCheck").toArray();

    for (i in checkBoxes) {
        if (checkBoxes[i].checked) {
            isAnyChecked = true;
            break;
        }
    }

    if (isAnyChecked) {
        $("#deleteButton").
            show(100);

    } else {
        $("#deleteButton").
            hide(100);
    }

}

function deleteReservedItem(a) {

    var resItemId = a.id.replace("delete-", "");

    $("#delete-one-confirm").dialog({
        resizable: false,
        height: 200,
        modal: true,
        buttons: {
            "Delete": function () {

                $.ajax({
                    type: "DELETE",
                    url: "/api/reserveditemapi/delete/" + resItemId,
                    traditional: true,
                    success: function (msg) {

                        $("#delete-one-confirm").dialog("close");

                        toastr.info(msg);

                        setTimeout(function () {
                            document.location.reload(true);
                        }, 1000);

                    }
                });
            },
            Cancel: function () {

                $(this).dialog("close");
            }
        },
    });
}

//function deletes all checked items

function deleteReservedItems() {

    var resBooks = new Array();
    var checkBoxes = $(".bookCheck").toArray();

    for (i in checkBoxes) {
        if (checkBoxes[i].checked) {
            resBooks.push(checkBoxes[i].id.replace("checkBox-", ""));
        }
    }

    if (resBooks.length > 0) {
        $("#delete-multiple-confirm").dialog({
            resizable: false,
            height: 200,
            modal: true,
            buttons: {
                "Delete": function () {

                    $.ajax({
                        type: "DELETE",
                        url: "/api/reserveditemapi/deletechecked",
                        traditional: true,
                        data: { resItemsId: resBooks },
                        success: function (msg) {

                            $("#delete-multiple-confirm").dialog("close");

                            toastr.info(msg);

                            setTimeout(function () {
                                document.location.reload(true);
                            }, 1000);

                        }
                    });
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
    }
}

//functions shows information about book

function viewItemInfo(a) {
    var id = a.id.replace("info-", "");


    $.ajax({
        type: "GET",
        url: "/api/reserveditemapi/info/" + id,
        traditional: true,
        
        success: function (msg) {

            $("#bookInfo").text(msg);

        },
        error: function () {

            toastr.error('Sorry, there is no such book');
        }
    }
    );


    $(function () {

        $("#dialog-modal").dialog({
            height: 200,
            width: 400,
            modal: true
        });
    });
}

//function checks all reserved books

function checkAll() {

    var checkBoxes = $(".bookCheck").toArray();

    if ($("#CheckAll").prop("checked")) {

        for (i in checkBoxes) {
            checkBoxes[i].checked = true;
        }

        setButtonVisible();
    }
    else {

        for (i in checkBoxes) {
            checkBoxes[i].checked = false;
        }

        setButtonVisible();
    }


}
//function reserve selected item

function reserveItem(a) {
    var id = a.id.replace("reserve-", "");

    $.ajax({
        type: "POST",
        url: "/api/reserveditemapi/reserve/" + id,
        traditional: true,
        success: function (msg) {

            if (msg.indexOf("Successfully") != -1) {
                toastr.success(msg);
                
            }

            else {

                toastr.warning(msg);
            }

        },
        error: function () {

            toastr.error('Sorry, there is some problem');
        }
    }
    );

}

