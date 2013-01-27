/// <reference path="../jquery.d.ts" />
/// <reference path="../knockout.d.ts"/>

module SinglePageApps 
{
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
            
            $.ajax("/Folder", { async: false }).done((data) =>
            {
                this.folders = data;
            });

            this.goToFolder = (folder: Folder) => { 
                this.chosenFolderId(folder.Id); 
                this.chosenMailData(null); // Stop showing a mail
                $.get('/Folder/Get', { id: folder.Id }, this.chosenFolderData);
            };

            this.goToMail = (mail: Mail) => { 
                this.chosenFolderId(mail.FolderId);
                this.chosenFolderData(null); // Stop showing a folder
                $.get("/Mail/Get", { id: mail.Id }, this.chosenMailData);
            };

            // Show inbox by default
            this.goToFolder(this.folders[0]);
        }
    }

    $(() => { ko.applyBindings(new WebmailViewModel()) });
}