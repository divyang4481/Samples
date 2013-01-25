/// <reference path="../jquery.d.ts" />
/// <reference path="../knockout.d.ts"/>

module SinglePageApps 
{
    class Folder {
        public Name: string;

        constructor(name: string) {
            this.Name = name;
        }
    }

    class WebmailViewModel {

        public folders: Folder[];

        constructor() {
            this.folders = [
                new Folder('Inbox'), 
                new Folder('Archive'), 
                new Folder('Sent'), 
                new Folder('Spam') ];
        }
    };

    $(() => { ko.applyBindings(new WebmailViewModel()) });
}