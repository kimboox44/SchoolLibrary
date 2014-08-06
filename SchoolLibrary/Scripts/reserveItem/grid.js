//grid
$(document).ready(function () {
    // prepare the data

    var source =
    {
        datatype: "json",
        datafields: [
            { name: 'Id', type: 'int' },
            { name: 'reservedItemId', type: 'int' },
            { name: 'Name', type: 'string' },
            { name: 'Category', type: 'string' },
            { name: 'Date', type: 'date' },
            { name: 'IsReady', type: 'bool' }
        ],
        url: '/api/reserveditemapi/items',
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

    var cellclassname = function (row, column, value, data) {
        if (data.IsReady == true) {
            return "greenClass";
        } else {
            return "redClass";
        }
    };
    

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
                { text: "Category", datafield: "Category", cellclassname: cellclassname },
                { text: "Item Name", datafield: "Name", cellclassname: cellclassname },
                { text: "Date", datafield: "Date", cellsformat: 'dd.MM.yyyy hh:mm', cellclassname: cellclassname, width: 150 },
                {
                    text: 'Info',
                    datafield: 'Info',
                    columntype: 'button',
                    sortable: false,
                    filterable: false,
                    width: 100,
                    cellsrenderer: function () {
                        return "Info";
                    }, buttonclick: function (row) {
                        var id = $("#grid").jqxGrid('getcellvalue', row, "Id");

                        $.ajax({
                            type: "GET",
                            url: "/api/reserveditemapi/info/" + id,
                            traditional: true,
                           // data: { id: id },
                            success: function (msg) {

                                $("#bookInfo").text(msg);

                            },
                            error: function () {

                                toastr.error('Sorry, there is no such item');
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
                },
                {
                    text: 'Delete',
                    datafield: 'Delete',
                    columntype: 'button',
                    sortable: false,
                    filterable: false,
                    width: 100,
                    cellsrenderer: function () {
                        return "Delete";
                    },
                    buttonclick: function (row) {
                        var resBookId = $("#grid").jqxGrid('getcellvalue', row, "reservedItemId");

                        $("#delete-one-confirm").dialog({
                            resizable: false,
                            height: 200,
                            modal: true,
                            buttons: {
                                "Delete": function () {

                                    $.ajax({
                                        type: "DELETE",
                                        url: "/api/reserveditemapi/delete/" + resBookId,
                                        traditional: true,

                                        success: function (msg) {

                                            $("#delete-one-confirm").dialog("close");

                                            toastr.info(msg);

                                            $("#grid").jqxGrid('updatebounddata');

                                        }
                                    });
                                },
                                Cancel: function () {

                                    $(this).dialog("close");
                                }
                            },
                        });
                    }
                }
            ]
        });

});