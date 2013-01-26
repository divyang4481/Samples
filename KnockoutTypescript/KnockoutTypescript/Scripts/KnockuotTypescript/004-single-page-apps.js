var SinglePageApps;
(function (SinglePageApps) {
    var Folder = (function () {
        function Folder(id, name) {
            this.Name = name;
            this.Id = id;
        }
        return Folder;
    })();    
    var WebmailViewModel = (function () {
        function WebmailViewModel() {
            var _this = this;
            this.folders = [
                new Folder(1, 'Inbox'), 
                new Folder(2, 'Archive'), 
                new Folder(3, 'Sent'), 
                new Folder(4, 'Spam')
            ];
            this.chosenFolderId = ko.observable();
            this.chosenFolderData = ko.observable();
            this.goToFolder = function (folder) {
                _this.chosenFolderId(folder.Id);
                var result = $.get('/Mail', {
                    folderId: folder.Id
                }, _this.chosenFolderData);
            };
        }
        return WebmailViewModel;
    })();    
    $(function () {
        ko.applyBindings(new WebmailViewModel());
    });
})(SinglePageApps || (SinglePageApps = {}));
//@ sourceMappingURL=004-single-page-apps.js.map
