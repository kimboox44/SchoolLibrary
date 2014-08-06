var initrowdetails = function (index, parentElement, gridElement, datarecord) {
    var tabsdiv = null;
    var information = null;
    tabsdiv = $($(parentElement).children()[0]);
    if (tabsdiv != null) {
        information = tabsdiv.find('.information');
        var title = tabsdiv.find('.title');
        title.text(datarecord.FirstName + datarecord.LastName);
        var container = $('<div style="margin: 5px;"></div>')
        container.appendTo($(information));
        var leftcolumn = $('<div style="float: left; width: 45%;"></div>');
        var rightcolumn = $('<div style="float: left; width: 40%;"></div>');
        container.append(leftcolumn);
        container.append(rightcolumn);
        var firstname = "<div style='margin: 10px;'><b>First Name:</b> " + datarecord.FirstName + "</div>";
        var lastname = "<div style='margin: 10px;'><b>Last Name:</b> " + datarecord.LastName + "</div>";
        var address = "<div style='margin: 10px;'><b>Address:</b> " + datarecord.Address + "</div>";
        $(leftcolumn).append(firstname);
        $(leftcolumn).append(lastname);
        $(leftcolumn).append(address);
        var birthday = "<div style='margin: 10px;'><b>Birthday:</b> " + datarecord.Birthday + "</div>";
        var email = "<div style='margin: 10px;'><b>Email:</b> " + datarecord.Email + "</div>";
        var phone = "<div style='margin: 10px;'><b>Phone:</b> " + datarecord.Phone + "</div>";
        $(rightcolumn).append(birthday);
        $(rightcolumn).append(email);
        $(rightcolumn).append(phone);
        $(tabsdiv).jqxTabs({ width: 600, height: 170, theme: theme });
    }
}