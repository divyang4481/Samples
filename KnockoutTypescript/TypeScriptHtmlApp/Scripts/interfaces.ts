/// <reference path="typings/jquery/jquery.d.ts" />

module TestInterface {

    export class Student {
        fullname: string;

        constructor(public firstname: string, public middleinitial: string, public lastname: string) {
            this.fullname = firstname + " " + middleinitial + " " + lastname;
        }
    }

    export interface Person {
        firstname: string;
        lastname: string;
    }

    export function greeter(person: Person) {
        return "Hello dear " + person.firstname + " " + person.lastname;
    }

    export var user = { firstname: "Eugeniusz", lastname: "Kowalski" };
    //var user = new Student("Eugeniusz", "M.", "Kowalski");
 
}

$(function () {
    $("#content2").html(TestInterface.greeter(TestInterface.user));
});