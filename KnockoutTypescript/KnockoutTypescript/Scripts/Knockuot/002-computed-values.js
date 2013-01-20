var ComputedValue;
(function (ComputedValue) {
    var ViewModel = (function () {
        function ViewModel(firstName, lastName) {
            this.firstName = ko.observable(firstName);
            this.lastName = ko.observable(lastName);
            this.fullName = ko.computed(function () {
                return this.firstName() + " " + this.lastName();
            }, this);
        }
        return ViewModel;
    })();    
    $(function () {
        ko.applyBindings(new ViewModel("Eugeniusz", "Kowalski"));
    });
})(ComputedValue || (ComputedValue = {}));
//@ sourceMappingURL=002-computed-values.js.map
