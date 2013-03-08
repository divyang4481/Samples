/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/knockout/knockout.d.ts"/>

module Collections 
{
    class Meal 
    {
        constructor(public mealName: string, public price: number) {}
    }

    // Class to represent a row in the seat reservations grid
    class SeatReservation 
    {
        public name;
        public meal;
        public formattedPrice;

        constructor(name: string, initialMeal: Meal) 
        {
            this.name = name;
            this.meal = ko.observable(initialMeal);

            this.formattedPrice = ko.computed(() =>
            {
                var price = this.meal().price;
                return price ? "$" + price.toFixed(2) : "N/A";
            });
        }
    }

    // Overall viewmodel for this screen, along with initial state
    class ReservationsViewModel 
    {    
        public availableMeals: Meal[];
        public seats: KnockoutObservableArray;

        public addSeat: Function;
        public removeSeat: Function;
        public totalSurcharge: KnockoutComputed;

        constructor() 
        {
            // Non-editable catalog data - would come from the server
            this.availableMeals = [
                new Meal("Standard (sandwich)", 0),
                new Meal("Premium (lobster)", 34.95),
                new Meal("Ultimate (whole zebra)", 290)
            ];

            // Editable data
            this.seats = ko.observableArray([
                new SeatReservation("Steve", this.availableMeals[0]),
                new SeatReservation("Bert", this.availableMeals[0])
            ]);

            // Operations
            this.addSeat = () => {
                this.seats.push(new SeatReservation("", this.availableMeals[0]));
            }

            this.removeSeat = (seat) => { this.seats.remove(seat) }

            this.totalSurcharge = ko.computed(() => {
                var total = 0;
                for (var i = 0; i < this.seats().length; i++)
                    total += this.seats()[i].meal().price;
                return total;
            });
        }
    }

    $(() => ko.applyBindings(new ReservationsViewModel()));
}