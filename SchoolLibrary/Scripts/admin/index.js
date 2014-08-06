function deleteUser(a) {
    var userId = a.id.replace("delete-", "");

    $("#delete-one-confirm").dialog({
        resizable: false,
        height: 200,
        modal: true,
        buttons: {
            "Delete": function () {

                $.ajax({
                    type: "POST",
                    url: "/Admin/DeleteUser",
                    traditional: true,
                    data: { id: userId },
                    success: function () { document.location.reload(true); }
                });
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
}

function deleteSelectedUsers() {
    var users = new Array();
    var checkBoxes = $(":checkbox").toArray();
    for (i in checkBoxes) {
        if (checkBoxes[i].checked && checkBoxes[i].id != "checkAll") {
            users.push(checkBoxes[i].id.replace("checkBox-", ""));
        }
    }

    if (users.length > 0) {
        $("#delete-multiple-confirm").dialog({
            resizable: false,
            height: 200,
            modal: true,
            buttons: {
                "Delete": function () {
                    $.ajax({
                        type: "POST",
                        url: "/Admin/DeleteSelectedUsers",
                        traditional: true,
                        data: { usersId: users },
                        success: function () { document.location.reload(true); }
                    });
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
    }
}

function resetPassword(a) {
    var id = a.id.replace("resetpass-", "");
    $("#reset-password-confirm").dialog({
        resizable: false,
        height: 200,
        modal: true,
        buttons: {
            "Reset": function () {

                $.ajax({
                    type: "POST",
                    url: "/Admin/ResetPassword",
                    traditional: true,
                    data: { userId: id }
                });
                $(this).dialog("close");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
}

$(document).ready(function () {
    $('select').change(function () {
        var role = this.options[this.selectedIndex].label;
        var userid = this.id.replace("select-", "");

        $("#change-role-confirm").dialog({
            resizable: false,
            height: 200,
            modal: true,
            buttons: {
                "Change": function () {
                    $.ajax({
                        type: "POST",
                        url: "/Admin/ChangeRole",
                        traditional: true,
                        data: { id: userid, newRole: role }
                    });

                    $(this).dialog("close");
                },
                Cancel: function () {
                    document.location.reload(true);
                    $(this).dialog("close");
                }
            }
        });
    });
});