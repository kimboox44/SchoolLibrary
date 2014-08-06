function createElements(theme) {
    $('#eventWindow').jqxWindow({
        maxHeight: 150,
        maxWidth: 280,
        minHeight: 30,
        minWidth: 250,
        height: 145,
        width: 270,
        theme: theme,
        autoOpen: false,
        resizable: false,
        isModal: true,
        modalOpacity: 0.3,
        okButton: $('#ok'),
        cancelButton: $('#cancel'),
        initContent: function () {
            $('#ok').jqxButton({ theme: theme, width: '65px' });
            $('#cancel').jqxButton({ theme: theme, width: '65px' });
            $('#ok').focus();
        }
    });
}

$(document).ready(function () {
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

    var GridModel = function () {
        this.items = ko.observableArray();
        var me = this;
        $.ajax({
            datatype: 'json',
            url: "/api/usersapi/getunconfirmed"
        }).done(function (data) {

            data = JSON.stringify(data);
            //        alert(data.length);
            var jsonData = JSON.parse(data);;
            //alert(jsonData);
            me.items(jsonData);
        });
    };

    var model = new GridModel();

    ko.applyBindings(model);

    // prepare the data
    var source =
    {
        datatype: "observablearray",
        datafields: [
            { name: 'UserName', type: 'string' },
            { name: 'ReaderName', type: 'string' },
            { name: 'RoleName', type: 'string' }
        ],
        id: 'id',
        localdata: model.items,
        // root: 'Rows',
        //  beforeprocessing: function (data) {
        //      source.totalrecords = data.TotalRows;
        //}
    };

    var dataAdapter = new $.jqx.dataAdapter(source);

    var theme = $.data(document.body, 'theme', theme);
    if (theme == undefined) theme = '';
    createElements(theme);

    $("#grid").jqxGrid(
    {
        width: 670,
        source: dataAdapter,
        pageable: true,
        autoheight: true,
        sortable: true,
        rendergridrows: function (obj) {
            return obj.data;
        },
        columnsresize: true,
        editable: true,
        theme: getDemoTheme(),
        columns: [
          {
              text: 'User name',
              datafield: 'UserName',
              editable: false
          },
                {
                    text: 'Reader name',
                    datafield: 'ReaderName',
                    editable: false
                },
                {
                    text: 'Role',
                    datafield: 'roleCode',
                    displayfield: 'RoleName',
                    columntype: 'dropdownlist',
                    createeditor: function (row, value, editor) {
                        editor.jqxDropDownList({ source: rolesAdapter, displayMember: 'label', valueMember: 'value', autoDropDownHeight: true });
                    }
                },
                {
                    text: 'Confirm',
                    datafield: 'Confirm',
                    columntype: 'button',
                    sortable: false,
                    width: 100,
                    cellsrenderer: function () {
                        return "Confirm";
                    },
                    buttonclick: function (row) {
                        var userName = $("#grid").jqxGrid('getcellvalue', row, "UserName");
                        var readerName = $("#grid").jqxGrid('getcellvalue', row, "ReaderName");
                        var role = $("#grid").jqxGrid('getcellvalue', row, "RoleName");
                        $('#eventWindow').jqxWindow('open');
                        $('#eventWindow').on('close', function (event) {
                            if (event.args.dialogResult.OK) {
                                //  alert("OK");
                                window.location = 'Confirm/?userName=' + userName + '&readerName=' + readerName + '&role=' + role;
                            } else if (event.args.dialogResult.Cancel) {
                                alert("Cancel");
                            }
                        });
                    }
                }
        ]
    });
    $("#grid").on("pagechanged", function (event) {
        $('#grid').jqxGrid('clearselection');
    });
});