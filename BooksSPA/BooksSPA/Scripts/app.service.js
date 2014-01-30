/// <reference path="typings/app.d.ts" />
var BooksSPA;
(function (BooksSPA) {
    ko.observable.fn["store"] = function () {
        var self = this;
        var oldValue = this();

        var observable = ko.computed({
            read: function () {
                return self();
            },
            write: function (value) {
                oldValue = self();
                self(value);
            }
        });

        this.revert = function () {
            self(oldValue);
        };
        this.commit = function () {
            oldValue = self();
        };

        return this;
    };

    var Book = (function () {
        function Book(data) {
            this.Id = data.Id;
            this.Year = ko.observable(data.Year).store();
            this.Title = ko.observable(data.Title).store();
            this.Rating = ko.observable(data.Rating).store();
            this.Genre = ko.observable(data.Genre).store();

            this.editing = ko.observable(false);
        }
        return Book;
    })();
    BooksSPA.Book = Book;

    var BooksService = (function () {
        function BooksService() {
            var _this = this;
            this.baseUrl = "/api/books/";
            this.serviceUrls = {
                books: function () {
                    return _this.baseUrl;
                },
                byGenre: function (genre) {
                    return _this.baseUrl + "?genre=" + genre;
                },
                byId: function (id) {
                    return _this.baseUrl + id;
                }
            };
        }
        BooksService.prototype.ajaxRequest = function (type, url, data) {
            var ajaxSettings = {};

            ajaxSettings.url = url;
            ajaxSettings.headers = { Accept: "application/json" };
            ajaxSettings.contentType = "application/json";
            ajaxSettings.cache = false;
            ajaxSettings.type = type;
            ajaxSettings.data = data ? ko.toJSON(data) : null;

            return $.ajax(ajaxSettings);
        };

        BooksService.prototype.getAllBooks = function () {
            return this.ajaxRequest('get', this.serviceUrls.books());
        };

        BooksService.prototype.getBooksByGenre = function (genre) {
            return this.ajaxRequest('get', this.serviceUrls.byGenre(genre));
        };

        BooksService.prototype.update = function (book) {
            return this.ajaxRequest('put', this.serviceUrls.byId(book.Id), book);
        };
        return BooksService;
    })();
    BooksSPA.BooksService = BooksService;
})(BooksSPA || (BooksSPA = {}));
//# sourceMappingURL=app.service.js.map
