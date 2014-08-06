$(document).ready(function () {

    var gridModel = function () {
        this.items = ko.observableArray();
        var me = this;
        $.ajax({
            datatype: 'json',
            url: "/api/reserveditemapi/itemsknockout"
        }).done(function (data) {

            data = JSON.stringify(data);

            var jsonData = JSON.parse(data);

            me.items(jsonData);
        });
    };

    var model = new gridModel();

    ko.applyBindings(model);


    var source =
   {
       datatype: "observablearray",
       datafields: [
            { name: 'Id', type: 'int' },
            { name: 'reservedItemId', type: 'int' },
            { name: 'Name', type: 'string' },
            { name: 'Category', type: 'string' },
            { name: 'Date', type: 'date' },
            { name: 'IsReady', type: 'bool' }
       ],
       id: 'id',
       localdata: model.items,

   };

    var dataAdapter = new $.jqx.dataAdapter(source);

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

        theme: getDemoTheme(),
        columns: [
        { text: "Category", datafield: "Category"},
                { text: "Book Name", datafield: "Name"},
                { text: "Date", datafield: "Date", cellsformat: 'dd.MM.yyyy hh:mm', width: 150 },
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
                            data: { id: id },
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
                }
        ]
    });

});