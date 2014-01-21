// BugStatus enum
var BugStatus;
(function (BugStatus) {
    BugStatus[BugStatus["Undefined"] = 0] = "Undefined";
    BugStatus[BugStatus["Backlog"] = 1] = "Backlog";
    BugStatus[BugStatus["Working"] = 2] = "Working";
    BugStatus[BugStatus["Done"] = 3] = "Done";
})(BugStatus || (BugStatus = {}));

var apiUrl = '/api/bugs/';

$(function () {
    var viewModel;

    // SignalR {
    $.connection.hub.logging = true;
    var bugsHub = $.connection.bugs;

    bugsHub.client.moved = function (item) {
        viewModel.moveBug(item);
    };

    $.connection.hub.start().done(function () {
        console.log('hub connection opened');
    });

    // }

    $.getJSON(apiUrl, function (data) {
        var model = data;
        viewModel = {
            Backlog: ko.observableArray(
                model.filter(function (element) { return element.State === BugStatus.Backlog })),
            Working: ko.observableArray(
                model.filter(function (element) { return element.State === BugStatus.Working })),
            Done: ko.observableArray(
                model.filter(function (element) { return element.State === BugStatus.Done })),
            changeState: function (bug, newState) {
                var self = this;
                $.post(apiUrl + BugStatus[newState], { '': bug.Id }, function (data) {
                    self.moveBug(data);
                });
            },
            moveBug: function (bug) {

                [this.Backlog, this.Working, this.Done].forEach(function (list) {
                    list().forEach(function (item) {
                        if (item.Id == bug.Id) {
                            console.log('removing item ' + item.Id);
                            list.remove(item);
                        }
                    });
                });

                this[BugStatus[bug.State]].push(bug);
            }
        };

        ko.applyBindings(viewModel);
    })
});