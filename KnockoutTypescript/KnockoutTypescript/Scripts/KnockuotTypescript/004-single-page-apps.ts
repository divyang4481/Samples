/// <reference path="../jquery.d.ts" />
/// <reference path="../knockout.d.ts" />
/// <reference path="../sammyjs.d.ts" />
/// <reference path="../moment.d.ts" />

module SinglePageApps 
{
    ko.bindingHandlers["datetext"] = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            // Provide a custom text value
            var value = valueAccessor(), allBindings = allBindingsAccessor();
            var dateFormat = allBindingsAccessor.dateFormat || "M/D/YYYY";
            var strDate = ko.utils.unwrapObservable(value);
            if (strDate) {
                if (moment(strDate).year() > 1970) {
                    var date = moment(strDate).format(dateFormat);
                    $(element).text(date);
                }
                else {
                    $(element).text("-");
                }
            }
        },
        update: function (element, valueAccessor, allBindingsAccessor) {
            // Provide a custom text value
            var value = valueAccessor(), allBindings = allBindingsAccessor();
            var dateFormat = allBindingsAccessor.dateFormat || "M/D/YYYY";
            var strDate = ko.utils.unwrapObservable(value);
            if (strDate) {
                if (moment(strDate).year() > 1970) {
                    var date = moment(strDate).format(dateFormat);
                    $(element).text(date);
                }
                else {
                    $(element).text("-");
                }
            }
        }
    };

    class Folder {
        constructor(
            public Id: number,
            public Name: string,
            public Mails: Mail[]) { }
    }

    class Mail {
        constructor(
            public Id: number,
            public From: string, 
            public To: string, 
            public Subject: string, 
            public Date: Date,
            public FolderId: number,
            public MessageContent: string) {}
    }

    class WebmailViewModel {

        // Data
        public folders: Folder[];
        public chosenFolderId: KnockoutObservableAny;
        public chosenFolderData: KnockoutObservableAny;
        public chosenMailData: KnockoutObservableAny;

        // Actions
        public goToFolder;
        public goToMail;

        constructor() {            
            this.chosenFolderId = ko.observable();
            this.chosenFolderData = ko.observable();
            this.chosenMailData = ko.observable();
            
            var self = this;

            // Client-side routes    
            Sammy(function() {
                this.get('#:folder', function() {
                    self.chosenFolderId(this.params.folder);
                    self.chosenMailData(null);
                    $.get("/Folder/Get", { id: this.params.folder }, self.chosenFolderData);
                });

                this.get('#:folder/:mailId', function() {
                    self.chosenFolderId(this.params.folder);
                    self.chosenFolderData(null);
                    $.get("/Mail/Get", { id: this.params.mailId }, self.chosenMailData);
                });

                this.get('', function() { this.app.runRoute('get', '#1') }); // 1 = Inbox
            }).run();

            $.ajax("/Folder", { async: false }).done((data) =>
            {
                this.folders = data;
            });

            this.goToFolder = (folder: Folder) => { location.hash = folder.Id.toString() };
            this.goToMail = (mail: Mail) => { location.hash = mail.FolderId + '/' + mail.Id };

            //this.goToFolder = (folder: Folder) => { 
            //    this.chosenFolderId(folder.Id); 
            //    this.chosenMailData(null); // Stop showing a mail
            //    $.get('/Folder/Get', { id: folder.Id }, this.chosenFolderData);
            //};

            //this.goToMail = (mail: Mail) => { 
            //    this.chosenFolderId(mail.FolderId);
            //    this.chosenFolderData(null); // Stop showing a folder
            //    $.get("/Mail/Get", { id: mail.Id }, this.chosenMailData);
            //};

            //// Show inbox by default
            //this.goToFolder(this.folders[0]);
        }
    }

    $(() => { ko.applyBindings(new WebmailViewModel()) });
}