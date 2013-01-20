var SimpleBinding;
(function (SimpleBinding) {
    var ViewModel = (function () {
        function ViewModel(firstName, lastName) {
            this.firstName = ko.observable(firstName);
            this.lastName = ko.observable(lastName);
        }
        return ViewModel;
    })();    
    $(function () {
        ko.applyBindings(new ViewModel("Eugeniusz", "Kowalski"));
    });
})(SimpleBinding || (SimpleBinding = {}));
//@ sourceMappingURL=001-binding.js.map
