/// <reference path="typings/jquery/jquery.d.ts" />

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
});