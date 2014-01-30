/// <reference path="app.service.ts" />

module BooksSPA
{
    class ViewModel
    {
        books: KnockoutObservableArray<Book>;
        error: KnockoutObservable<string>;
        workInProgress: KnockoutObservable<boolean>;
        currentGenre: KnockoutObservable<string>;
        genres: string[];
        ratings: string[];
        service: BooksService;

        constructor()
        {
            this.books = ko.observableArray<Book>();
            this.error = ko.observable<string>('');
            this.currentGenre = ko.observable<string>('');
            this.workInProgress = ko.observable<boolean>(false);

            this.genres = ['Drama', 'Fantasy', 'Novel', 'Documentary'];
            this.ratings = ['1', '2', '3', '4', '5'];

            this.service = new BooksService();
        }

        addBooks(data) {
            var mapped = ko.utils.arrayMap(data, book => {
                return new Book(book); 
            });

            this.books(mapped);
        }

        onError(error) {
            this.error('Error: ' + error.status + ' ' + error.statusText);
        }

        getByGenre(genre: string) {
            this.error('');
            this.currentGenre(genre);
            this.workInProgress(true);
            return this.service.getBooksByGenre(genre)
                .then((data) => { this.addBooks(data); this.workInProgress(false); })
                .fail(this.onError);
        }

        edit(book: Book) {
            book.editing(true);
        }

        cancel(book: Book) {
            this.revertChanges(book);
            book.editing(false);
        }

        save(book: Book) {
            this.service.update(book)
                .then(() => { this.commitChanges(item); })
                .fail((error) => { this.onError(error); this.revertChanges(book); })
                .always(() => { book.editing(false); });
        }

        applyFn(item, fn) {
            for (var prop in item) {
                if (item.hasOwnProperty(prop) && item[prop][fn]) {
                    item[prop][fn].apply();
                }
            }
        }

        commitChanges(item) { this.applyFn(item, 'commit'); }
        revertChanges(item) { this.applyFn(item, 'revert'); }
    }

    var viewModel: ViewModel;

    $(() => {
        viewModel = new ViewModel();
        ko.applyBindings(viewModel);

        viewModel.getByGenre(viewModel.genres[0]);
    });
}