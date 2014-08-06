$(document).ready(function () {

    var gridSource =
    {
        datatype: "json",
        datafields: [
            { name: 'Id', type: 'string' },
            { name: 'Name', type: 'string' },
            { name: 'Year', type: 'int' },
            { name: 'Publisher', type: 'string' },
            { name: 'Type', type: 'string' },
            { name: 'InventoriesCount', type: 'int' }
        ],
        url: '/api/itemapi/getitems',
        root: 'Rows',
        beforeprocessing: function (data) {
            gridSource.totalrecords = data.TotalRows;
        }
    };

    var gridAdapter = new $.jqx.dataAdapter(gridSource);

    var theme = $.data(document.body, 'theme', theme);
    if (theme == undefined) theme = '';

    $("#jqxgrid").jqxGrid(
        {
            width: 1000,
            source: gridAdapter,
            pageable: true,
            autoheight: true,
            sortable: true,
            virtualmode: true,
            rendergridrows: function (obj) {
                return obj.data;
            },
            columnsresize: true,
            selectionmode: 'singlecell',
            columns: [
                {
                    text: 'Name',
                    datafield: 'Name',
                    width:200
                },
                {
                    text: 'Year',
                    datafield: 'Year',
                    width:50
                },
                {
                    text: 'Publisher/Producer',
                    datafield: 'Publisher'
                },
                {
                    text: 'Type',
                    datafield: 'Type',
                    width:50
                },
                {
                    text: 'Inventory count',
                    datafield: 'InventoriesCount'
                },
                {
                    text: 'Edit',
                    datafield: 'Edit',
                    columntype: 'button',
                    sortable: false,
                    width: 100,
                    cellsrenderer: function () {
                        return "Edit";
                    },
                    buttonclick: function (row) {
                        var id = $("#jqxgrid").jqxGrid('getcellvalue', row, 'Id');
                        window.location = 'Edit/' + id;
                    }
                },
                {
                    text: 'Details',
                    datafield: 'Details',
                    columntype: 'button',
                    sortable: false,
                    width: 100,
                    cellsrenderer: function () {
                        return 'Details';
                    },
                    buttonclick: function (row) {
                        var id = $("#jqxgrid").jqxGrid('getcellvalue', row, 'Id');
                        window.location = 'Details/' + id;
                    }
                },
                {
                    text: 'Manage consignment',
                    columntype: 'button',
                    sortable: false,
                    width: 180,
                    cellsrenderer: function () {
                        return 'Manage consignment';
                    },
                    buttonclick: function (row) {
                        var id = $("#jqxgrid").jqxGrid('getcellvalue', row, 'Id');
                        window.location = "/Consignment/ItemWidgets/" + id;
                    }
                }
            ]
        });
    $("#jqxgrid").on("pagechanged", function (event) {
        $('#jqxgrid').jqxGrid('clearselection');
    });
});