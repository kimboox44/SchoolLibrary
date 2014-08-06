function diskViewModel(model) {
    var self = this;

    self.Id = ko.observable(model.Id);
    self.Name = ko.observable(model.Name);
    self.Year = ko.observable(model.Year);
    self.Producer = ko.observable(model.Producer);
    self.Type = ko.observable(model.Type);


    self.save = function () {

        self.Tags = $("#tagsbox").tokenInput("get");
        ko.utils.postJson("/Item/EditDisk/"+model.Id, { disk: ko.toJS(self) });
    };
};