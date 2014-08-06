function bookViewModel(model) {
    var self = this;
    self.Id = ko.observable(model.Id);
    self.Name = ko.observable(model.Name);
    self.Year = ko.observable(model.Year);
    self.Publisher = ko.observable(model.Publisher);
    self.PageCount = ko.observable(model.PageCount);
    

    self.save = function () {
        $.validator.unobtrusive.parse(document);
        if ($('#BookForm').valid()) {
            self.Authors = $("#combobox").tokenInput("get");
            self.Tags = $("#tagsbox").tokenInput("get");
            ko.utils.postJson(location.href, { viewModel: ko.toJS(self) });
        }
        return false;
    };
};