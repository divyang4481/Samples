/// <reference path="../jquery.d.ts" />
/// <reference path="../knockout.d.ts"/>

module SinglePageApps 
{
    class Folder {
        public Name: string;
        public Id: number;

        constructor(id: number, name: string) {
            this.Name = name;
            this.Id = id;
        }
    }

    class WebmailViewModel {

        // Data
        public folders: Folder[];
        public chosenFolderId: KnockoutObservableAny;
        public chosenFolderData: KnockoutObservableAny;

        // Actions
        public goToFolder;
        
        constructor() {
            this.folders = [
                new Folder(1, 'Inbox'),
                new Folder(2, 'Archive'),
                new Folder(3, 'Sent'),
                new Folder(4, 'Spam') ];

            this.chosenFolderId = ko.observable();
            this.chosenFolderData = ko.observable();

            this.goToFolder = (folder: Folder) => { 
                this.chosenFolderId(folder.Id); 
                var result = $.get('/Mail', { folderId: folder.Id }, this.chosenFolderData);
            };
        }
    }

    $(() => { ko.applyBindings(new WebmailViewModel()) });
}