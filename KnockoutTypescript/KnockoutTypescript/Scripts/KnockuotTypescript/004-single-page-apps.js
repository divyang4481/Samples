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
            $.ajax("/Folder", {
                async: false
            }).done(function (data) {
                _this.folders = data;
            });
            this.goToFolder = function (folder) {
                _this.chosenFolderId(folder.Id);
                _this.chosenMailData(null);
                $.get('/Folder/Get', {
                    id: folder.Id
                }, _this.chosenFolderData);
            };
            this.goToMail = function (mail) {
                _this.chosenFolderId(mail.FolderId);
                _this.chosenFolderData(null);
                $.get("/Mail/Get", {
                    id: mail.Id
                }, _this.chosenMailData);
            };
            this.goToFolder(this.folders[0]);
        }
        return WebmailViewModel;
    })();    
    $(function () {
        ko.applyBindings(new WebmailViewModel());
    });
})(SinglePageApps || (SinglePageApps = {}));
//@ sourceMappingURL=004-single-page-apps.js.map
