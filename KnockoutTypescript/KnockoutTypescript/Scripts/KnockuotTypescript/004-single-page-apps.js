var SinglePageApps;
(function (SinglePageApps) {
    var Folder = (function () {
        function Folder(name) {
            this.Name = name;
        }
        return Folder;
    })();    
    var WebmailViewModel = (function () {
        function WebmailViewModel() {
            this.folders = [
                new Folder('Inbox'), 
                new Folder('Archive'), 
                new Folder('Sent'), 
                new Folder('Spam')
            ];
        }
        return WebmailViewModel;
    })();    
    ; ;
    $(function () {
        ko.applyBindings(new WebmailViewModel());
    });
})(SinglePageApps || (SinglePageApps = {}));
//@ sourceMappingURL=004-single-page-apps.js.map
