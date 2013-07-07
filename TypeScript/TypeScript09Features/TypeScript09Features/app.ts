/// <reference path="Scripts/typings/jquery/jquery.d.ts" />


// Generics

var array: Array<string> = ["a", "b", "c"];
var result = array.map((val, ind, arr) => val.length);

class A { }

class B extends A { }

class Tuple<First, Second extends A>
{
    Item1: First;
    Item2: Second;
}

// Enums

enum Color { Red, Green, Blue };

// Declaration merging
function readInput(separator = readInput.defaultSeparator) {
    // read input
}
module readInput {
    export var defaultSeparator = ":";
}

$(() => {
    var color = Color.Blue;
    var text = Color[color] + " = " + Color.Blue.toString(); // Blue = 2
    $('#content').text(text); 

    //Overloading on constants

    var paragraph = document.createElement('p');
    var cell = document.createElement('td');

    alert(cell.nodeName);
    alert(paragraph.nodeName);

    // Generics
    var myTuple = new Tuple<string, B>();

    myTuple.Item1 = "First";
    myTuple.Item2 = new B();

    // External modules
    //import log = require("log");    
    //log.message("hello");

    

    readInput.defaultSeparator
});

