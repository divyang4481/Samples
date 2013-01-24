var Collections;
(function (Collections) {
    var Meal = (function () {
        function Meal(mealName, price) {
            this.mealName = mealName;
            this.price = price;
        }
        return Meal;
    })();    
    var SeatReservation = (function () {
        function SeatReservation(name, initialMeal) {
            var _this = this;
            this.name = name;
            this.meal = ko.observable(initialMeal);
            this.formattedPrice = ko.computed(function () {
                var price = _this.meal().price;
                return price ? "$" + price.toFixed(2) : "N/A";
            });
        }
        return SeatReservation;
    })();    
    var ReservationsViewModel = (function () {
        function ReservationsViewModel() {
            var _this = this;
            this.availableMeals = [
                new Meal("Standard (sandwich)", 0), 
                new Meal("Premium (lobster)", 34.95), 
                new Meal("Ultimate (whole zebra)", 290)
            ];
            this.seats = ko.observableArray([
                new SeatReservation("Steve", this.availableMeals[0]), 
                new SeatReservation("Bert", this.availableMeals[0])
            ]);
            this.addSeat = function () {
                _this.seats.push(new SeatReservation("", _this.availableMeals[0]));
            };
            this.removeSeat = function (seat) {
                _this.seats.remove(seat);
            };
            this.totalSurcharge = ko.computed(function () {
                var total = 0;
                for(var i = 0; i < _this.seats().length; i++) {
                    total += _this.seats()[i].meal().price;
                }
                return total;
            });
        }
        return ReservationsViewModel;
    })();    
    $(function () {
        return ko.applyBindings(new ReservationsViewModel());
    });
})(Collections || (Collections = {}));
//@ sourceMappingURL=003-collections.js.map
