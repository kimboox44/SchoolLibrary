$(document).ready(function () {
    window.initPrint();
    // prepare the data
    var source =
    {
        datatype: 'json',
        datafields: [
            { name: 'InventoryId', type: 'int' },
            { name: 'Number', type: 'string' },
            { name: 'IsAvailable', type: 'bool' },
            { name: 'WriteOffDate', type: 'date' }
        ],
        url: '/api/consignmentapi/getallinventories',
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
                            'value="Write off inventory" onclick="writeOffInventory(' + row + ')"/>'
        }
    }

    // initialize jqxGrid
    $('#grid').jqxGrid(
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
                {
                    text: 'Inventory Number',
                    datafield: 'Number',
                    filtertype: 'date',
                },
                {
                    text: 'IsAvailable',
                    datafield: 'IsAvailable',
                    columntype: 'checkbox',
                    filterable: false,
                },
                {
                    text: 'Write off inventory',
                    datafield: 'WriteOffDate',
                    filterable: false,
                    width: 170,
                    cellsrenderer: cellsrenderer
                },
            {
                text: 'Print BarCodes',
                columntype: 'button',
                sortable: false,
                filterable: false,
                width: 120,
                cellsrenderer: function () {
                    return 'Print BarCodes';
                },
                buttonclick: function (row) {
                    var number = $('#grid').jqxGrid('getcellvalue', row, 'Number');
                    print(number);
                    window.initPrint();
                }
            },

            ]
        });

    $('#jqxcheckbox').bind('change', function (event) {
        var checked = event.args.checked;
        $('#log').html('checked: ' + checked);
    });
    $('#grid').bind('rowselect', function (event) {
        var row = event.args.rowindex;
    });
});

function writeOffInventory(row) {
    var inventoryId = $("#grid").jqxGrid('getRowData', row).InventoryId;
    if (confirm('Are you sure that you want write off this inventory?')) {
        $.ajax({
            type: "POST",
            url: "/Consignment/WriteOffInventory/?inventoryId=" + inventoryId,
            traditional: true,
            success: function () {
                $('#grid').jqxGrid('updatebounddata');
            }
        });
    }
}