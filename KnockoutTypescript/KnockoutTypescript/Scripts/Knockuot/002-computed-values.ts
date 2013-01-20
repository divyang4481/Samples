/// <reference path="../jquery.d.ts" />
/// <reference path="../knockout.d.ts"/>

module ComputedValue {

    class ViewModel {
        public firstName : KnockoutObservableString;
        public lastName : KnockoutObservableString;
        public fullName : KnockoutComputed;

        constructor(firstName: string, lastName: string) {

            this.firstName = ko.observable(firstName);
            this.lastName = ko.observable(lastName);

            this.fullName = ko.computed(function () {
                return this.firstName() + " " + this.lastName();
            }, this);
        }
    }

    $(function () {
        ko.applyBindings(new ViewModel("Eugeniusz", "Kowalski"));
    });
}
