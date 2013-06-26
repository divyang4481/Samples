/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/knockout/knockout.d.ts"/>

module ComputedValue 
{
    class ViewModel 
    {
        public firstName: KnockoutObservableString;
        public lastName: KnockoutObservableString;
        public fullName: KnockoutComputed;

        public capitalizeLastName() 
        {   
            var currentVal = this.lastName();        // Read the current value
            this.lastName(currentVal.toUpperCase()); // Write back a modified value
        }

        constructor(firstName: string, lastName: string) 
        {
            this.firstName = ko.observable(firstName);
            this.lastName = ko.observable(lastName);

            this.fullName = ko.computed(() => 
            {
                return this.firstName() + " " + this.lastName();
            }, this);
        }
    }

    $(() => {
        ko.applyBindings(new ViewModel("Eugeniusz", "Kowalski"));
    });
}
