/// <reference path="../jquery.d.ts" />
/// <reference path="../knockout.d.ts"/>

module Collections {
    class SeatReservation {
        public meal: KnockoutObservableAny;
        public formattedPrice: KnockoutComputed;

        constructor(public name: string, initialMeal: Meal) {
            this.meal = ko.observable(initialMeal);

            this.formattedPrice = ko.computed(function() {
                var price: number = this.meal().price;
                return price ? "$" + price.toFixed(2) : "None";        
            }, this);
        }
    }

    class Meal {
        constructor(public name: string, public price: number) {
        }
    }

    class ReservationsViewModel {
        // Non-editable data from server
        public availableMeals: Meal[];

        // Editable data
        public seats: KnockoutObservableArray;

        constructor() {
          this.availableMeals  = [
          new Meal("Standard (sandwich)", 0),
          new Meal("Premium (lobster)", 34.95),
          new Meal("Ultimate (whole zebra)", 290),
          new Meal("Bak³a¿an", 2)]; 
            
          this.seats = ko.observableArray([
            new SeatReservation("Steve", this.availableMeals[0]),
            new SeatReservation("Bert", this.availableMeals[0])]);
        }

        public addSeat() {
            this.seats.push(new SeatReservation("", this.availableMeals[0]));
        }
    }

    $(function () { 
        ko.applyBindings(new ReservationsViewModel());
    });
}

