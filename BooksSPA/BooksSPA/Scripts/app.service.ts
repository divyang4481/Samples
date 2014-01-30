/// <reference path="typings/app.d.ts" />

module BooksSPA
 {
     ko.observable.fn["store"] = function () {
         var self = this;
         var oldValue = this();

         var observable = ko.computed({
             read: function () { return self(); },
             write: function (value) { oldValue = self(); self(value); }
         });

        this.revert = function () { self(oldValue); }
        this.commit = function () { oldValue = self(); }

        return this;
    }

     export class Book
     {
         constructor(data)
         {
             this.Id = data.Id;
             this.Year = ko.observable<number>(data.Year).store();
             this.Title = ko.observable<string>(data.Title).store();
             this.Rating = ko.observable<string>(data.Rating).store();
             this.Genre = ko.observable<string>(data.Genre).store();

             this.editing = ko.observable<boolean>(false);
         }

         Id: number;
         Title: KnockoutObservable<string>;
         Rating: KnockoutObservable<string>;
         Genre: KnockoutObservable<string>;
         Year: KnockoutObservable<number>;

         editing: KnockoutObservable<boolean>;
     }

     export class BooksService
     {
         private baseUrl = "/api/books/";
         private serviceUrls = {
             books: () => { return this.baseUrl; },
             byGenre: (genre: string) => { return this.baseUrl + "?genre=" + genre; },
             byId: (id) => { return this.baseUrl + id; }
         };

         private ajaxRequest(type: string, url: string, data?) : JQueryXHR
         {
             var ajaxSettings: JQueryAjaxSettings = {};

             ajaxSettings.url = url;
             ajaxSettings.headers = { Accept: "application/json" };
             ajaxSettings.contentType = "application/json";
             ajaxSettings.cache = false;
             ajaxSettings.type = type;
             ajaxSettings.data = data ? ko.toJSON(data) : null;

             return $.ajax(ajaxSettings);
         }

         getAllBooks()
         {
             return this.ajaxRequest('get', this.serviceUrls.books());
         }

         getBooksByGenre(genre: string)
         {
             return this.ajaxRequest('get', this.serviceUrls.byGenre(genre));
         }

         update(book: Book)
         {
             return this.ajaxRequest('put', this.serviceUrls.byId(book.Id), book);
         }
     }
 }