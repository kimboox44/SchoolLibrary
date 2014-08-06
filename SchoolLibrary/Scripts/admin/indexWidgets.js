var oldRole;

function changeUserRole(userId, userName, newRole) {
    //$('#change-role-confirm').html = "Role of " + userName + " will be changed from " + oldRole +
    //    " to " + newRole;

    $("<p>Role of " + userName + " will be changed from " + oldRole +
        " to " + newRole + ". Are you shure?</p>").dialog({
            draggable: false,
            resizable: false,
            height: 200,
            modal: true,
            title: "Change role?",
            buttons: {
                "Change": function() {
                    $.ajax({
                        type: "POST",
                        url: "/api/usersapi/ChangeRole",
                        traditional: true,
                        data: { id: userId, newRole: newRole }
                    });

                    $(this).dialog("close");
                },
                Cancel: function() {
                    document.location.reload(true);
                    $(this).dialog("close");
                }
            }
        });
}

function deleteUser(a) {
    if (a.id != "delete-") {
        var userId = a.id.replace("delete-", "");

        $("#delete-one-confirm").dialog({
            draggable: false,
            resizable: false,
            height: 200,
            modal: true,
            buttons: {
                "Delete": function() {

                    $.ajax({
                        type: "POST",
                        url: "/api/usersapi/deleteuser",
                        traditional: true,
                        data: { id: userId },
                        success: function () {
                            $("#delete-one-confirm").dialog("close");
                            $("#grid").jqxGrid('updatebounddata');
                        }
                    });
                },
                Cancel: function() {
                    $(this).dialog("close");
                }
            }
        });
    }
}

function deleteSelectedUsers() {
    var users = new Array();
    var grid = $('#grid');
    var selectedrowindexes = grid.jqxGrid('selectedrowindexes');
    $.each(selectedrowindexes, function() {
        var id = grid.jqxGrid('getcellvalue', this, "UserId");
        if (id != null) {
            users.push(id);
        }
    });

    if (users.length > 0) {
        $("#delete-multiple-confirm").dialog({
            draggable: false,
            resizable: false,
            height: 200,
            modal: true,
            buttons: {
                "Delete": function() {
                    $.ajax({
                        type: "POST",
                        url: "/api/usersapi/deleteusers",
                        traditional: true,
                        data: { usersId: users },
                        success: function() {
                            $("#delete-multiple-confirm").dialog("close");
                            $("#grid").jqxGrid('updatebounddata');
                        }
                    });
                },
                Cancel: function() {
                    $(this).dialog("close");
                }
            }
        });
    }
}

function resetPassword(a) {
    if (a.id != "resetpass-") {
        var id = a.id.replace("resetpass-", "");
        $("#reset-password-confirm").dialog({
            draggable: false,
            resizable: false,
            height: 200,
            modal: true,
            buttons: {
                "Reset": function() {

                    $.ajax({
                        type: "POST",
                        url: "/api/usersapi/ResetPassword",
                        traditional: true,
                        data: { userId: id },
                        success: function() {
                            $("#reset-password-confirm").dialog("close");
                        }
                    });
                },
                Cancel: function() {
                    $(this).dialog("close");
                }
            }
        });
    }
}
        
$(document).ready(function () {
    //initializing jqxGrid
    var roles = [
        { value: "Unregistered", label: "Unregistered" },
        { value: "Registered", label: "Registered" },
        { value: "Librarian", label: "Librarian" },
        { value: "Admin", label: "Admin" }
    ];

    var rolesSource =
    {
        datatype: "array",
        datafields: [
            { name: 'label', type: 'string' },
            { name: 'value', type: 'string' }
        ],
        localdata: roles
    };

    var rolesAdapter = new $.jqx.dataAdapter(rolesSource, {
        autoBind: true
    });

    var source =
    {
        datatype: "json",
        datafields: [
            { name: 'UserId', type: 'int' },
            { name: 'UserName', type: 'string' },
            { name: 'Email', type: 'string' },
            { name: 'Role', type: 'string' },
            { name: 'CreationDate', type: 'date' }
        ],
        url: '/api/usersapi/getusers',
        filter: function() {
            $("#grid").jqxGrid('updatebounddata');
        },
        root: 'Rows',
        beforeprocessing: function(data) {
            source.totalrecords = data.TotalRows;
        }
    };

    var dataAdapter = new $.jqx.dataAdapter(source);

    $("#grid").jqxGrid(
        {
            width: 900,
            source: dataAdapter,
            pageable: true,
            autoheight: true,
            virtualmode: true,
            showfilterrow: true,
            filterable: true,
            rendergridrows: function(obj) {
                return obj.data;
            },
            columnsresize: true,
            editable: true,
            selectionmode: 'checkbox',
            columns: [
                {
                    text: "Username",
                    filtertype: 'textbox',
                    filtercondition: 'starts_with',
                    datafield: "UserName",
                    editable: false
                },
                {
                    text: "Email",
                    filtertype: 'textbox',
                    filtercondition: 'starts_with',
                    datafield: "Email",
                    editable: false
                },
                {
                    text: "Creation date",
                    filtertype: 'date',
                    datafield: "CreationDate",
                    cellsformat: 'dd.MM.yyyy',
                    editable: false
                },
                {
                    text: 'Role',
                    datafield: 'roleCode',
                    displayfield: 'Role',
                    columntype: 'dropdownlist',
                    filterable: false,
                    createeditor: function(row, value, editor) {
                        editor.jqxDropDownList({
                            source: rolesAdapter,
                            displayMember: 'label',
                            valueMember: 'value',
                            autoDropDownHeight: true
                        });
                    }
                },
                {
                    text: "Actions",
                    width: 173,
                    sortable: false,
                    filterable: false,
                    editable: false,
                    cellsrenderer: function(row) {
                        var id = $("#grid").jqxGrid('getcellvalue', row, "UserId");
                        return "<a href='#' id='delete-" + id + "' onclick='deleteUser(this)'>Delete</a> |" +
                            "<a href='#' id='resetpass-" + id + "' onclick='resetPassword(this)'>Reset password</a>";
                    }
                }
            ]
        });

    $("#grid").on('cellendedit', function(event) {
        var rowindex = args.rowindex;
        var userId = $("#grid").jqxGrid('getcellvalue', rowindex, "UserId");
        var newRole = args.value.value;
        if (oldRole != newRole) {
            var userName = $("#grid").jqxGrid('getcellvalue', rowindex, "UserName");
            changeUserRole(userId, userName, newRole);
        }
    });
            
    $("#grid").on("pagechanged", function (event) {
        $('#grid').jqxGrid('clearselection');
    });

    //gets old role of the user
    $("#grid").on('cellbeginedit', function(event) {
        var rowindex = event.args.rowindex;
        oldRole = $("#grid").jqxGrid('getcellvalue', rowindex, "Role");
    });
});
