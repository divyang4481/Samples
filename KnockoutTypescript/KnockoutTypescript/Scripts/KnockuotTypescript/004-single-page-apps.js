var SinglePageApps;
(function (SinglePageApps) {
    var Folder = (function () {
        function Folder(Id, Name, Mails) {
            this.Id = Id;
            this.Name = Name;
            this.Mails = Mails;
        }
        return Folder;
    })();    
    var Mail = (function () {
        function Mail(Id, From, To, Subject, Date, FolderId, MessageContent) {
            this.Id = Id;
            this.From = From;
            this.To = To;
            this.Subject = Subject;
            this.Date = Date;
            this.FolderId = FolderId;
            this.MessageContent = MessageContent;
        }
        return Mail;
    })();    
    var WebmailViewModel = (function () {
        function WebmailViewModel() {
            var _this = this;
            this.chosenFolderId = ko.observable();
            this.chosenFolderData = ko.observable();
            this.chosenMailData = ko.observable();
            var self = this;
            Sammy(function () {
                this.get('#:folder', function () {
                    self.chosenFolderId(this.params.folder);
                    self.chosenMailData(null);
                    $.get("/Folder/Get", {
                        id: this.params.folder
                    }, self.chosenFolderData);
                });
                this.get('#:folder/:mailId', function () {
                    self.chosenFolderId(this.params.folder);
                    self.chosenFolderData(null);
                    $.get("/Mail/Get", {
                        id: this.params.mailId
                    }, self.chosenMailData);
                });
                this.get('', function () {
                    this.app.runRoute('get', '#1');
                });
            }).run();
            $.ajax("/Folder", {
                async: false
            }).done(function (data) {
                _this.folders = data;
            });
            this.goToFolder = function (folder) {
                location.hash = folder.Id.toString();
            };
            this.goToMail = function (mail) {
                location.hash = mail.FolderId + '/' + mail.Id;
            };
        }
        return WebmailViewModel;
    })();    
    $(function () {
        ko.applyBindings(new WebmailViewModel());
    });
})(SinglePageApps || (SinglePageApps = {}));
//@ sourceMappingURL=004-single-page-apps.js.map
