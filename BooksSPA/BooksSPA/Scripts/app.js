/// <reference path="app.service.ts" />
var BooksSPA;
(function (BooksSPA) {
    var ViewModel = (function () {
        function ViewModel() {
            this.books = ko.observableArray();
            this.error = ko.observable('');
            this.currentGenre = ko.observable('');
            this.workInProgress = ko.observable(false);

            this.genres = ['Drama', 'Fantasy', 'Novel', 'Documentary'];
            this.ratings = ['1', '2', '3', '4', '5'];

            this.service = new BooksSPA.BooksService();
        }
        ViewModel.prototype.addBooks = function (data) {
            var mapped = ko.utils.arrayMap(data, function (book) {
                return new BooksSPA.Book(book);
            });

            this.books(mapped);
        };

        ViewModel.prototype.onError = function (error) {
            this.error('Error: ' + error.status + ' ' + error.statusText);
        };

        ViewModel.prototype.getByGenre = function (genre) {
            var _this = this;
            this.error('');
            this.currentGenre(genre);
            this.workInProgress(true);
            return this.service.getBooksByGenre(genre).then(function (data) {
                _this.addBooks(data);
                _this.workInProgress(false);
            }).fail(this.onError);
        };

        ViewModel.prototype.edit = function (book) {
            book.editing(true);
        };

        ViewModel.prototype.cancel = function (book) {
            this.revertChanges(book);
            book.editing(false);
        };

        ViewModel.prototype.save = function (book) {
            var _this = this;
            this.service.update(book).then(function () {
                _this.commitChanges(item);
            }).fail(function (error) {
                _this.onError(error);
                _this.revertChanges(book);
            }).always(function () {
                book.editing(false);
            });
        };

        ViewModel.prototype.applyFn = function (item, fn) {
            for (var prop in item) {
                if (item.hasOwnProperty(prop) && item[prop][fn]) {
                    item[prop][fn].apply();
                }
            }
        };

        ViewModel.prototype.commitChanges = function (item) {
            this.applyFn(item, 'commit');
        };
        ViewModel.prototype.revertChanges = function (item) {
            this.applyFn(item, 'revert');
        };
        return ViewModel;
    })();

    var viewModel;

    $(function () {
        viewModel = new ViewModel();
        ko.applyBindings(viewModel);

        viewModel.getByGenre(viewModel.genres[0]);
    });
})(BooksSPA || (BooksSPA = {}));
//# sourceMappingURL=app.js.map
