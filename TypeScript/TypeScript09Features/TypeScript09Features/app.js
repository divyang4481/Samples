var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Dog = (function () {
    function Dog() {
    }
    return Dog;
})();

var Point = (function () {
    function Point(x, y) {
        this.x = x;
        this.y = y;
    }
    Point.Origin = new Point(0, 0);
    return Point;
})();

// Generics
var array = ["a", "b", "c"];
var result = array.map(function (val, ind, arr) {
    return val.length;
});

var A = (function () {
    function A() {
    }
    return A;
})();

var B = (function (_super) {
    __extends(B, _super);
    function B() {
        _super.apply(this, arguments);
    }
    return B;
})(A);

var Tuple = (function () {
    function Tuple() {
    }
    return Tuple;
})();

// Enums
var Color;
(function (Color) {
    Color[Color["Red"] = 0] = "Red";
    Color[Color["Green"] = 1] = "Green";
    Color[Color["Blue"] = 2] = "Blue";
})(Color || (Color = {}));
;

// Declaration merging
function readInput(separator) {
    if (typeof separator === "undefined") { separator = readInput.defaultSeparator; }
}
var readInput;
(function (readInput) {
    readInput.defaultSeparator = ":";
})(readInput || (readInput = {}));

$(function () {
    var color = Color.Blue;
    var text = Color[color] + " = " + Color.Blue.toString();
    $('#content').text(text);

    //Overloading on constants
    var paragraph = document.createElement('p');
    var cell = document.createElement('td');

    //alert(cell.nodeName);
    //alert(paragraph.nodeName);
    // Generics
    var myTuple = new Tuple();

    myTuple.Item1 = "First";
    myTuple.Item2 = new B();

    // External modules
    //import log = require("log");
    //log.message("hello");
    readInput.defaultSeparator;

    alert(Point.Origin.x.toString());
});
//@ sourceMappingURL=app.js.map
