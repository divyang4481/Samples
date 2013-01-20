var Student = (function () {
    function Student(firstname, middleinitial, lastname) {
        this.firstname = firstname;
        this.middleinitial = middleinitial;
        this.lastname = lastname;
        this.fullname = firstname + " " + middleinitial + " " + lastname;
    }
    return Student;
})();
function greeter(person) {
    return "Hello dear " + person.firstname + " " + person.lastname;
}
var user = new Student("Eugeniusz", "M.", "Kowalski");
$(function () {
    $("#messagebox").html(greeter(user));
});
//@ sourceMappingURL=greeter2.js.map
