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
            { name: 'readerName', type: 'string'},
            { name: 'Date', type: 'date' },
            { name: 'IsReady', type: 'bool' },
            { name: 'ReadyDate', type: 'date' }

        ],
        url: '/api/reserveditemapi/itemsforlibrarian',
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

    var textReadyBtn;

    var cellclassname = function (row, column, value, data) {
        if (data.IsReady == true) {

            textReadyBtn = "Set Unready";
            return "greenClass";

        } else {

            textReadyBtn = "Set Ready";
            return "redClass";

        }
    };



    // initialize jqxGrid
    $("#grid").jqxGrid(
        {
            width: 1000,
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
                { text: "Reader", datafield: "readerName", width: 200,cellclassname: cellclassname},
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
                     text: 'Set Status',
                     datafield: 'SetReady',
                     columntype: 'button',
                     sortable: false,
                     filterable: false,
                     width: 100,
                     cellsrenderer: function () {
                         return textReadyBtn;
                     }, buttonclick: function (row) {
                         var id = $("#grid").jqxGrid('getcellvalue', row, "reservedItemId");

                         $.ajax({
                             type: "PUT",
                             url: "/api/reserveditemapi/setready/" + id,
                             traditional: true,
                             data: { id: id },
                             success: function (msg) {

                                 if (msg) {
                                     toastr.info('Successfully done');

                                     $("#grid").jqxGrid('updatebounddata');

                                 } else {
                                     toastr.error('Sorry, was not done');
                                 }

                             },
                             error: function () {

                                 toastr.error('Sorry, there is some error');
                             }
                         }
                         );

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