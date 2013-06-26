module Inheritance {

    class A {
        constructor() { }

        public f() { }
    }

    interface I {
        f1(x: number): string;
    }

    class B extends A {
        constructor() { super(); }
    }
}