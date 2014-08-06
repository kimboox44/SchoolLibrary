function showCreateWindow() {
    $("#jqxwindow1").jqxWindow('open');
}

function showCreateMultipleWindow() {
    $("#jqxwindow2").jqxWindow('open');
}

$(document).ready(function () {
    $("#jqxwindow1").jqxWindow({ width: 500, isModal: true, autoOpen: false });
    $("#jqxwindow2").jqxWindow({ width: 500, isModal: true, autoOpen: false });
    $("#jqxwindow3").jqxWindow({ width: 500, isModal: true, autoOpen: false });
    $("#jqxwindow4").jqxWindow({ width: 500, isModal: true, autoOpen: false });
    // prepare the data
    // prepare the data
    var source =
    {
        datatype: "json",
        datafields: [
            { name: 'ReaderId', type: 'int' },
            { name: 'FirstName', type: 'string' },
            { name: 'LastName', type: 'string' },
            { name: 'Address', type: 'string' },
            { name: 'Birthday', type: 'date' },
            { name: 'EMail', type: 'string' },
            { name: 'Phone', type: 'string' },
        ],
        url: '/api/readerapi/readers',
        sort: function () {
            $("#grid").jqxGrid('updatebounddata', 'sort');
        },
        filter: function () {
            $("#grid").jqxGrid('updatebounddata', 'filter');
        },
        root: 'Rows',
        beforeprocessing: function (data) {
            source.totalrecords = data.TotalRows;
        }
    };
    var dataAdapter = new $.jqx.dataAdapter(source);

    // initialize jqxGrid
    $("#grid").jqxGrid(
        {
            width: 800,
            source: dataAdapter,
            showfilterrow: true,
            filterable: true,
            sortable: true,
            pageable: true,
            autoheight: true,
            virtualmode: true,
            rendergridrows: function (obj) {
                return obj.data;
            },
            columnsresize: true,
            columns: [
                { text: "First Name", datafield: "FirstName", filtertype: 'textbox', filtercondition: 'contains' },
                { text: "Last Name", datafield: "LastName", filtertype: 'textbox', filtercondition: 'contains' },
                {
                    text: 'Details',
                    datafield: 'Details',
                    columntype: 'button',
                    sortable: false,
                    filterable: false,
                    width: 100,
                    cellsrenderer: function () {
                        return "Details";
                    },
                    buttonclick: function (row) {
                        var id = $("#grid").jqxGrid('getcellvalue', row, "ReaderId");
                        getReaderDetails(id);
                    }
                },
                {
                    text: 'Edit',
                    datafield: 'Edit',
                    columntype: 'button',
                    sortable: false,
                    filterable: false,
                    width: 50,
                    cellsrenderer: function () {
                        return "Edit";
                    },
                    buttonclick: function (row) {
                        var id = $("#grid").jqxGrid('getcellvalue', row, "ReaderId");
                        getReaderEdit(id);
                        //window.location = '/Librarian/EditReader/?id=' + id;
                    }
                },
                {
                    text: 'Manage books',
                    datafield: 'Manage books',
                    columntype: 'button',
                    sortable: false,
                    filterable: false,
                    width: 200,
                    cellsrenderer: function () {
                        return "Manage books";
                    },
                    buttonclick: function (row) {
                        var id = $("#grid").jqxGrid('getcellvalue', row, "ReaderId");
                        window.location = '/Librarian/ReaderHistory/?readerId=' + id;
                    }
                }
            ]
        });
    initSimpleFileUpload();
    $("#hl-start-upload").on('click', function () {
        if (jqXHRData) {
            jqXHRData.submit();
            $("#jqxwindow2").jqxWindow('close');
        }
        return false;
    });
});