$(document).ready(function () {
    window.initPrint();
    // prepare the data
    var source =
    {
        datatype: 'json',
        datafields: [
            { name: 'Id', type: 'int' },
            { name: 'Number', type: 'int' },
            { name: 'ArrivalDate', type: "date" },
            { name: 'WriteOffDate', type: "date" },
            { name: 'InventoriesCount', type: 'int' }
        ],
        url: '/api/consignmentapi/getconsignmentsbyitemid?id=' + $("#itemId").val(),
        sort: function () {
            $('#grid').jqxGrid('updatebounddata', 'sort');
        },
        filter: function () {
            $("#grid").jqxGrid('updatebounddata', 'filter');
        },
        root: 'Rows',
        beforeprocessing: function (data) {
            source.rows = data.Rows;
            source.totalrecords = data.TotalRows;
        }
    };
    var dataAdapter = new $.jqx.dataAdapter(source);
    var cellsrenderer = function (row, column, value) {
        if (value) {
            var d = new Date(Date.parse(value));
            var curr_date = d.getDate();
            var curr_month = d.getMonth() + 1;
            var curr_year = d.getFullYear();
            return curr_date + "." + curr_month + "." + curr_year;
        }
        else {
            return '<input type="button" class="jqx-rc-all jqx-button jqx-widget jqx-fill-state-hover" ' +
                        'style="margin-top: 2px; margin-left: 2px; width: 98%; height: 21px; background-color: #e8e8e8;" ' +
                            'value="Write off consignment" onclick="writeOffConsignment(' + row + ')"/>'
        }
    }

    // initialize jqxGrid
    $('#grid').jqxGrid(
        {
            width: 900,
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
                {
                    text: 'Number',
                    datafield: 'Number',
                    filtertype: 'textbox',
                },
                {
                    text: 'Arrival date',
                    datafield: 'ArrivalDate',
                    cellsformat: 'dd.MM.yyyy',
                    filtertype: 'date',
                    width: 200
                },
                {
                    text: 'Inventory count',
                    datafield: 'InventoriesCount',
                    filterable: false,
                },
                {
                    text: 'Details',
                    datafield: 'Details',
                    columntype: 'button',
                    sortable: false,
                    filterable: false,
                    width: 100,
                    cellsrenderer: function () {
                        return 'Details';
                    },
                    buttonclick: function (row) {
                        var id = $("#grid").jqxGrid('getcellvalue', row, 'Id');
                        window.location = '/Consignment/Details/' + id;
                    }
                },
                {
                    text: 'Write off consignment',
                    datafield: 'WriteOffDate',
                    width: 170,
                    filterable: false,
                    cellsrenderer: cellsrenderer,
                },
                {
                    text: 'Print BarCodes',
                    //datafield: 'Details',
                    columntype: 'button',
                    sortable: false,
                    filterable: false,
                    width: 120,
                    cellsrenderer: function () {
                        return 'Print BarCodes';
                    },
                    buttonclick: function (row) {
                        var id = $('#grid').jqxGrid('getcellvalue', row, 'Id');
                        print(id);
                        window.initPrint();
                    }
                },
                {
                    text: 'Inventory list',
                    datafield: 'Inventory list',
                    columntype: 'button',
                    sortable: false,
                    filterable: false,
                    width: 100,
                    cellsrenderer: function () {
                        return 'Inventory list';
                    },
                    buttonclick: function (row) {
                        var id = $('#grid').jqxGrid('getcellvalue', row, 'Id');
                        window.location = '/Consignment/InventoriesWidgets/' + id;
                    }
                }
            ]
        });

    $('#grid').bind('rowselect', function (event) {
        var row = event.args.rowindex;
    });
});

function writeOffConsignment(row) {
    var id = $("#grid").jqxGrid('getRowData', row).Id;
    if (confirm('Are you sure that you want write off this consignment?')) {
        $.ajax({
            type: "POST",
            url: "/Consignment/WriteOff/" + id,
            traditional: true,
            success: function () {
                $('#grid').jqxGrid('updatebounddata');
            }
        });
    }
}