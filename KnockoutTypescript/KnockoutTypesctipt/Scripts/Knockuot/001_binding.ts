/// <reference path="../jquery.d.ts" />
/// <reference path="../knockout.d.ts"/>

// This is a simple *viewmodel* - JavaScript that defines the data and behavior of your UI
class AppViewModel {
    constructor(public firstName: string, public lastName: string) { 
    }
}

// Activates knockout.js
$(function () {
    ko.applyBindings(new AppViewModel("Eugeniusz", "Kowalski"));
});

// Non ts version:

//function AppViewModel() {
//    this.firstName = ko.observable("Eugeniusz");
//    this.lastName = ko.observable("Kowalski");
//}

//$(function () {
//    ko.applyBindings(new AppViewModel());
//});


