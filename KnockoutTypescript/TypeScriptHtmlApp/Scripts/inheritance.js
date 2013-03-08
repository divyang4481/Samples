var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Inheritance;
(function (Inheritance) {
    var A = (function () {
        function A() {
        }
        A.prototype.f = function () {
        };
        return A;
    })();    
    var B = (function (_super) {
        __extends(B, _super);
        function B() {
                _super.call(this);
        }
        return B;
    })(A);    
})(Inheritance || (Inheritance = {}));
