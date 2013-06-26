var Greeter = (function () {
    function Greeter(greeting) {
        this.greeting = greeting;
    }
    Greeter.prototype.greet = function () {
        return "Hello, " + this.greeting;
    };
    return Greeter;
})();
var TestModule;
(function (TestModule) {
    TestModule.foo = "BBB";
    function test() {
        return "AAAA";
    }
    TestModule.test = test;
    var Greeter = (function () {
        function Greeter(greeting) {
            this.greeting = greeting;
        }
        Greeter.prototype.greet = function () {
            return "Hello, " + this.greeting;
        };
        return Greeter;
    })();
    TestModule.Greeter = Greeter;    
})(TestModule || (TestModule = {}));
$(function () {
    var greeter = new TestModule.Greeter("Eugeniusz");
    greeter.greet();
});
