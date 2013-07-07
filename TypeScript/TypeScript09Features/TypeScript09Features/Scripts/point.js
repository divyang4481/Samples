define(["require", "exports"], function(require, exports) {
    
    var Point = (function () {
        function Point(x, y) {
            this.x = x;
            this.y = y;
        }
        Point.origin = new Point(0, 0);
        return Point;
    })();
    return Point;
});
//@ sourceMappingURL=point.js.map
