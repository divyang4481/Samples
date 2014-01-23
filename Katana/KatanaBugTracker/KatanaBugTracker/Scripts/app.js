// BugStatus enum
var BugStatus;
(function (BugStatus) {
    BugStatus[BugStatus["Undefined"] = 0] = "Undefined";
    BugStatus[BugStatus["Backlog"] = 1] = "Backlog";
    BugStatus[BugStatus["Working"] = 2] = "Working";
    BugStatus[BugStatus["Done"] = 3] = "Done";
})(BugStatus || (BugStatus = {}));

var apiUrl = '/api/bugs/';

function handleDragStart(bug, event) {
    viewModel.draggingBug(bug);

    // Knockout makes all events preventDefault / return false => I have to return true
    // see: http://stackoverflow.com/questions/7218171/knockout-html5-drag-and-drop

    return true;
}

function handleDragOver(bug, event) {
    if (event.preventDefault) { event.preventDefault(); } // Necessary. Allows to drop.
}

function handleDragEnter(bug, event) {
    if (event.preventDefault) { event.preventDefault(); }

    // this / e.target is the current hover target.
    viewModel.dragOverBug(bug);
}

function handleDrop(bug, event) {
    if (event.stopPropagation) { event.stopPropagation(); }

    if (bug.Id != viewModel.draggingBug().Id) {
        viewModel.changeState(viewModel.draggingBug(), bug.State);
    }

    viewModel.dragOverBug(null);

    return false;
}

var viewModel;

$(function () {

    // SignalR {
    $.connection.hub.logging = false;
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
            },
            draggingBug: ko.observable(),
            dragOverBug: ko.observable(),
        };

        ko.applyBindings(viewModel);
    });
});