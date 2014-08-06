function initTagsBox(tags) {
    $("#tagsbox").tokenInput("/api/tagsapi/gettags", {
        queryParam: "token",
        theme: "facebook",
        preventDuplicates: true,
        prePopulate: tags,
        onResult: function (results) {
            if ($.isEmptyObject(results)) {
                var nores = "No results. <b>Add new</b>";
                results = [{ id: 0, name: nores }];
            }
            return results;
        },
        onAdd: function (item) {
            if (item.id == 0) {
                addNewTagClicked();
                $(this).tokenInput("remove", { id: 0 });
            }
        }
    });
    
    $("#tagname").on("change paste keyup", onChange);
}

function addNewTagClicked() {
    $("#create-tag-popup").dialog({
        draggable: false,
        resizable: false,
        height: 220,
        modal: true,
        buttons: {
            "Create": function () {
                $.ajax({
                    type: "POST",
                    url: "/api/tagsapi/create",
                    traditional: true,
                    data: { tag: $("#tagname").val() },
                    success: function () {
                        $("#create-tag-popup").dialog("close");
                        $("#token-input-tagsbox").val($("#tagname").val());
                        $("#token-input-tagsbox").focus();
                        $("#tagsbox").tokenInput('search');
                    }
                });
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
    
    $(".ui-dialog-buttonset button:contains('Create')").button("disable");
}

function onChange() {
    if ($("#tagname").val().length >= 3) {
        $(".ui-dialog-buttonset button:contains('Create')").button("enable");
    }
}