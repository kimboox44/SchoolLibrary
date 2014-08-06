function successAdd()
{
             
    var fullname = $("#FirstName").val()+" "+$("#LastName").val();
            
             
    $("#jqxwindow").jqxWindow('close');


    $("#token-input-combobox").val(fullname);
    $("#token-input-combobox").focus();
    $("#combobox").tokenInput('search');
             
    //alert(id);
}

function documentReady() {
    //alert("111");
    $("#combobox").tokenInput("/api/authorapi/getauthors", {
        queryParam: "search",
        theme: "facebook",
        preventDuplicates: true,
        prePopulate: values,
        minChars: 3,
        onResult: function(results) {
            if ($.isEmptyObject(results)) {
                var nores = "No results. <b>Add new</b>";
                results = [{ id: 0, name: nores }];
            }
            return results;
        },
        onAdd: function(item) {
            if (item.id == 0) {
                $(this).tokenInput("remove", { id: 0 });
                $("#jqxwindow").jqxWindow('open');

            }
        }
    });
    //alert("111");
    $("#jqxwindow").jqxWindow({ height: 300, width: 325, isModal: true, autoOpen: false });


}