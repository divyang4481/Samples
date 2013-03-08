module Greeter {

    export function test(): string {
        return "AAAA";
    }

    export module Sayings {
        export class Greeter {

            constructor(public greeting: string) {

            }

            greet() {
                return "Hello, " + this.greeting;
            }
        }
    }

    var greeter = new Sayings.Greeter("world");

    Greeter.test();

}