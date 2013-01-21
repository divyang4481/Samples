var Collections;
(function (Collections) {
    var SeatReservation = (function () {
        function SeatReservation(name, initialMeal) {
            this.name = name;
            this.meal = ko.observable(initialMeal);
            this.formattedPrice = ko.computed(function () {
                debugger;

                var price = this.meal().price;
                return price ? "$" + price.toFixed(2) : "None";
            });
        }
        return SeatReservation;
    })();    
    var Meal = (function () {
        function Meal(name, price) {
            this.name = name;
            this.price = price;
        }
        return Meal;
    })();    
    var ReservationsViewModel = (function () {
        function ReservationsViewModel() {
            this.availableMeals = [
                new Meal("Standard (sandwich)", 0), 
                new Meal("Premium (lobster)", 34.95), 
                new Meal("Ultimate (whole zebra)", 290), 
                new Meal("Bak³a¿an", 2)
            ];
            this.seats = ko.observableArray([
                new SeatReservation("Steve", this.availableMeals[0]), 
                new SeatReservation("Bert", this.availableMeals[0])
            ]);
        }
        ReservationsViewModel.prototype.addSeat = function () {
            this.seats.push(new SeatReservation("", this.availableMeals[0]));
        };
        return ReservationsViewModel;
    })();    
    $(function () {
        ko.applyBindings(new ReservationsViewModel());
    });
})(Collections || (Collections = {}));
//@ sourceMappingURL=003-collections.js.map
