/// <reference path="typings/jquery/jquery.d.ts" />

class Greeter {
    public greeting: string;

    constructor(greeting: string) {
        this.greeting = greeting;
    }

    greet(): string {
        return "Hello, " + this.greeting;
    }
}

module TestModule {

    export var foo = "BBB";

    export function test(): string {
        return "AAAA";
    }

    export class Greeter {
        
        constructor(public greeting: string) {
        }

        greet() {
            return "Hello, " + this.greeting;
        }
    }
}

$(() => {
    var greeter = new TestModule.Greeter("Eugeniusz");

    greeter.greet();
});