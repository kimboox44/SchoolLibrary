function magazineViewModel(model) {
    var self = this;

    self.Id = ko.observable(model.Id);
    self.Name = ko.observable(model.Name);
    self.Year = ko.observable(model.Year);
    self.Publisher = ko.observable(model.Publisher);
    self.PageCount = ko.observable(model.PageCount);
    self.Issue = ko.observable(model.Issue);


    self.save = function () {

        self.Tags = $("#tagsbox").tokenInput("get");
        ko.utils.postJson("/Item/EditMagazine/"+model.Id, { magazine: ko.toJS(self) });
    };
};