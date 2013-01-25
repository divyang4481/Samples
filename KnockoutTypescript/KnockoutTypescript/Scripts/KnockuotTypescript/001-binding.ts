/// <reference path="../jquery.d.ts" />
/// <reference path="../knockout.d.ts"/>

module SimpleBinding {
    // This is a simple *viewmodel* - JavaScript that defines the data and behavior of your UI
    class ViewModel {
        public firstName : KnockoutObservableString;
        public lastName : KnockoutObservableString;

        constructor(firstName: string, lastName: string) {
            this.firstName = ko.observable(firstName);
            this.lastName = ko.observable(lastName);
        }
    }

    // Activates knockout.js
    $(function () {
        ko.applyBindings(new ViewModel("Eugeniusz", "Kowalski"));
    });
}

// Non ts version:

//function AppViewModel() {
//    this.firstName = ko.observable("Eugeniusz");
//    this.lastName = ko.observable("Kowalski");
//}

//$(function () {
//    ko.applyBindings(new AppViewModel());
//});


