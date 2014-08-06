function successAdd()
{
    //get fullname from modal window   
    var fullname =$("#FirstName").val()+" "+ $("#MiddleName").val()+" "+ $("#LastName").val();
    // close modal window
    $("#jqxwindow").jqxWindow("close");
    
    // put fullname in combobox and begin searching 
    $("#token-input-combobox").val(fullname);
    $("#token-input-combobox").focus();
    $("#combobox").tokenInput("search");
             
   
}

function initAuthorsInput(model) {
    $("#combobox").tokenInput("/api/authorapi/getauthors", {
        queryParam: "search",
        theme: "facebook",
        preventDuplicates: true,
        prePopulate: model.Authors,
        minChars: 3,
        onResult: function(results) {
            if ($.isEmptyObject(results)) {
                var noresults = "No results. <b>Add new</b>";
                results = [{ id: 0, name: noresults }];
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
    
    $("#jqxwindow").jqxWindow({ height: 300, width: 325, isModal: true, autoOpen: false });

    
}