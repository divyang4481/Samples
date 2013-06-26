var Greeter;
(function (Greeter) {
    function test() {
        return "AAAA";
    }
    Greeter.test = test;
    (function (Sayings) {
        var Greeter = (function () {
            function Greeter(greeting) {
                this.greeting = greeting;
            }
            Greeter.prototype.greet = function () {
                return "Hello, " + this.greeting;
            };
            return Greeter;
        })();
        Sayings.Greeter = Greeter;        
    })(Greeter.Sayings || (Greeter.Sayings = {}));
    var Sayings = Greeter.Sayings;
    var greeter = new Sayings.Greeter("world");
    Greeter.test();
})(Greeter || (Greeter = {}));
//@ sourceMappingURL=app.js.map
