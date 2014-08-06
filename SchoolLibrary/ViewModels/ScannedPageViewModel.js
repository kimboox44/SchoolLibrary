function deleteScanedPage(a) {
    if (a.id != "delete-") {
        var id = a.id.replace("delete-", "");

        $("#delete-one-confirm").dialog({
            draggable: false,
            resizable: false,
            height: 200,
            modal: true,
            buttons: {
                "Delete": function () {

                    $.ajax({
                        type: "POST",
                        url: '/api/scannedpageapi/deletescanedpage',
                        traditional: true,
                        data: { Id: id },
                        success: function () {
                            $("#delete-one-confirm").dialog("close");
                            $("#grid").jqxGrid('updatebounddata');
                        }
                    });
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
    }
}


$(document).ready(function () {
    $("#jqxwindowedit").jqxWindow({ width: 500, isModal: true, autoOpen: false });
    //initializing jqxGrid
    var source =
    {
        datatype: "json",
        datafields: [
            { name: 'Id', type: 'int' },
            { name: 'OrderText', type: 'string' },
            { name: 'OrderDate', type: 'date' },
            { name: 'ExecutionDate', type: 'date' },
            { name: 'IsReady', type: 'bool' },
            { name: 'IsLocked', type: 'bool' },
            { name: 'Message', type: 'string' }
        ],
        url: '/api/scannedpageapi/getscanedpages',
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

    $("#grid").jqxGrid(
        {
            width: 900,
            source: dataAdapter,
            pageable: true,
            autoheight: true,
            virtualmode: true,
            showfilterrow: true,
            filterable: true,
            sortable: true,
            rendergridrows: function (obj) {
                return obj.data;
            },
            columnsresize: true,
            editable: true,
            selectionmode: 'checkbox',

            columns: [
                {
                    text: "Id",
                    filtertype: 'int',
                    filtercondition: 'contains',
                    datafield: "Id",
                    sortable: false,
                    editable: false
                },

                {
                    text: "Order Text",
                    filtertype: 'textbox',
                    filtercondition: 'contains',
                    datafield: "OrderText",
                    sortable: false,
                    editable: false
                },

                {
                    text: "Order Date",
                    filtertype: 'date',
                    datafield: "OrderDate",
                    cellsformat: 'dd.MM.yyyy',
                    sortable: true,
                    editable: false
                },

                {
                    text: "Execution Date",
                    filtertype: 'date',
                    datafield: "ExecutionDate",
                    cellsformat: 'dd.MM.yyyy',
                    sortable: true,
                    editable: false
                },

                {
                    text: "Message",
                    datafield: "Message",
                    sortable: false,
                    editable: false
                },

                {
                    text: "IsReady",
                    filtertype: 'bool',
                    datafield: "IsReady",
                    cellsformat: 'checkbox',
                    sortable: true,
                    filterable: false,
                    editable: false
                },

                {
                    text: "IsLocked",
                    filtertype: 'bool',
                    datafield: "IsLocked",
                    cellsformat: 'checkbox',
                    sortable: true,
                    filterable: false,
                    editable: false
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
                         var id = $("#grid").jqxGrid('getcellvalue', row, "Id");
                         scanPageEdit(id);
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
                        var id = $("#grid").jqxGrid('getcellvalue', row, "Id");

                        $("#delete-one-confirm").dialog({
                            resizable: false,
                            height: 200,
                            modal: true,
                            buttons: {
                                "Delete": function () {

                                    $.ajax({
                                        type: "DELETE",
                                        url: "/api/scannedpageapi/deletescanedpage/",
                                        traditional: true,
                                        data: { id: id },
                                        success: function () {
                                            $("#delete-one-confirm").dialog("close");
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

                },

            ]
        });

    $("#grid").on("pagechanged", function (event) {
        $('#grid').jqxGrid('clearselection');
    });


    function successAdd() {
        $("#jqxwindow1").jqxWindow('close');
        $('#grid').jqxGrid('updatebounddata');
    }


    var scanPage;

    ko.validation.init({ insertMessages: false });

    function ScannedPageViewModel(data) {
        this.Id = ko.observable(data.Id);
        this.OrderText = ko.observable(data.OrderText);
        this.OrderDate = ko.observable(data.OrderDate);
        this.ExecutionDate = ko.observable(data.ExecutionDate);
        this.IsReady = ko.observable(data.IsReady);
        this.IsLocked = ko.observable(data.IsLocked);
        this.Message = ko.observable(data.Message);

        var self = this;
        self.save = function () {
            $.validator.unobtrusive.parse(document);
            if ($('#form1').valid()) {
                $.ajax("/api/scannedpageapi/edit", {
                    data: ko.toJSON(this),
                    type: "put",
                    contentType: "application/json",
                    success: function () {
                        $('#grid').jqxGrid('updatebounddata');
                        $("#jqxwindowedit").jqxWindow('close');
                    },
                    error: function () {
                        alert("failed");
                    }
                });
            }
            return false;
        };
    }

    function scanPageEdit(id) {
        $.ajax({
            type: "GET",
            dataType: "json",
            contentType: "application/json",
            url: "/api/scannedpageapi/getbyid/" + id,
            success: function (data) {
                scanPage = new ScannedPageViewModel(data);
                ko.cleanNode(document.getElementById("jqxwindowedit"));
                ko.applyBindings(scanPage, document.getElementById("jqxwindowedit"));
                $("#jqxwindowedit").jqxWindow('open');
            }
        });
    }

});