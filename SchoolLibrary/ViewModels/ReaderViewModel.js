var reader;

ko.validation.init({ insertMessages: false });

function ReaderViewModel(data) {
    this.ReaderId = ko.observable(data.ReaderId);
    this.FirstName = ko.observable(data.FirstName);
    this.LastName = ko.observable(data.LastName);
    this.Address = ko.observable(data.Address);
    this.Birthday = ko.observable(moment(data.Birthday).format('L'));
    this.Phone = ko.observable(data.Phone);
    this.EMail = ko.observable(data.EMail);

    var self = this;
    self.save = function () {
        $.validator.unobtrusive.parse(document);
        if ($('#form1').valid()) {
            $.ajax("/api/readerapi/update", {
                data: ko.toJSON(this),
                type: "put",
                contentType: "application/json",
                success: function () {
                    $('#grid').jqxGrid('updatebounddata');
                    $("#jqxwindow4").jqxWindow('close');
                },
                error: function () {
                    alert("failed");
                }
            });
        }
        return false;
    };
}

function getReaderDetails(id) {
    $.ajax({
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        url: "/api/readerapi/getbyid/" + id,
        success: function (data) {
            reader = new ReaderViewModel(data);
            ko.cleanNode(document.getElementById("jqxwindow3"));
            ko.applyBindings(reader, document.getElementById("jqxwindow3"));
            $("#jqxwindow3").jqxWindow('open');
            console.log(data);
        }
    });
}

function getReaderEdit(id) {
    $.ajax({
        type: "GET",
        dataType: "json",
        contentType: "application/json",
        url: "/api/readerapi/getbyid/" + id,
        success: function (data) {
            reader = new ReaderViewModel(data);
            ko.cleanNode(document.getElementById("jqxwindow4"));
            ko.applyBindings(reader, document.getElementById("jqxwindow4"));
            $("#jqxwindow4").jqxWindow('open');
        }
    });
}
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

$('#form2').submit(function (event) {
    $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "/api/readerapi/create",
            data: JSON.stringify($('#form2').serializeObject()),
            success: function() {
                $("#jqxwindow1").jqxWindow('close');
                $('#grid').jqxGrid('updatebounddata');
            }
        });
    return false;
});
