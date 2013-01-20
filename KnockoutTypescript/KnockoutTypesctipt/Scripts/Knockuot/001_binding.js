var AppViewModel = (function () {
    function AppViewModel(firstName, lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
    }
    return AppViewModel;
})();
$(function () {
    ko.applyBindings(new AppViewModel("Eugeniusz", "Kowalski"));
});
//@ sourceMappingURL=001_binding.js.map
