/// <reference path="typings/signalr/signalr.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
var BugState;
(function (BugState) {
    BugState[BugState["Undefined"] = 0] = "Undefined";
    BugState[BugState["Backlog"] = 1] = "Backlog";
    BugState[BugState["Working"] = 2] = "Working";
    BugState[BugState["Done"] = 3] = "Done";
})(BugState || (BugState = {}));

var Bug = (function () {
    function Bug() {
    }
    return Bug;
})();

var ViewModel = (function () {
    function ViewModel(model) {
        this.Backlog = ko.observableArray(model.filter(function (bug) {
            return bug.State === 1 /* Backlog */;
        }));
        this.Working = ko.observableArray(model.filter(function (bug) {
            return bug.State === 2 /* Working */;
        }));
        this.Done = ko.observableArray(model.filter(function (bug) {
            return bug.State === 3 /* Done */;
        }));

        this.draggingBug = ko.observable();
        this.dragOverBug = ko.observable();
    }
    ViewModel.prototype.changeState = function (bug, newState) {
        var self = this;
        $.post(apiUrl + BugState[newState], { '': bug.Id }, function (data) {
            self.moveBug(data);
        });
    };

    ViewModel.prototype.moveBug = function (bug) {
        [this.Backlog, this.Working, this.Done].forEach(function (list) {
            list().forEach(function (item) {
                if (item.Id == bug.Id) {
                    console.log('removing item ' + item.Id);
                    list.remove(item);
                }
            });
        });

        this[BugState[bug.State]].push(bug);
    };
    return ViewModel;
})();

var apiUrl = '/api/bugs/';

var DragAndDropUtility = (function () {
    function DragAndDropUtility() {
    }
    DragAndDropUtility.handleDragStart = function (bug, event) {
        viewModel.draggingBug(bug);

        // Knockout makes all events preventDefault / return false => I have to return true
        // see: http://stackoverflow.com/questions/7218171/knockout-html5-drag-and-drop
        return true;
    };

    DragAndDropUtility.handleDragOver = function (bug, event) {
        if (event.preventDefault) {
            event.preventDefault();
        }
    };

    DragAndDropUtility.handleDragEnter = function (bug, event) {
        if (event.preventDefault) {
            event.preventDefault();
        }

        // this / e.target is the current hover target.
        if (viewModel.draggingBug().Id != bug.Id) {
            viewModel.dragOverBug(bug);
        }
    };

    DragAndDropUtility.handleDrop = function (bug, event) {
        if (event.stopPropagation) {
            event.stopPropagation();
        }

        if (bug.Id != viewModel.draggingBug().Id) {
            viewModel.changeState(viewModel.draggingBug(), bug.State);
        }

        return false;
    };

    DragAndDropUtility.handleDragEnd = function (bug, event) {
        if (event.preventDefault) {
            event.preventDefault();
        }

        viewModel.dragOverBug(null);
        viewModel.draggingBug(null);
    };
    return DragAndDropUtility;
})();

var viewModel;

function setUpSignalR() {
    // Use this for debugging purposes
    $.connection.hub.logging = false;

    // Used [] notation to avoid TypeScript error
    // I could also add custom definition file (.d.ts)
    var bugsHub = $.connection["bugs"];

    bugsHub.client.moved = function (item) {
        viewModel.moveBug(item);
    };

    $.connection.hub.start().done(function () {
        console.log('hub connection opened');
    });
}

$(function () {
    setUpSignalR();

    $.getJSON(apiUrl, function (data) {
        viewModel = new ViewModel(data);
        ko.applyBindings(viewModel);
    });
});
//# sourceMappingURL=app.js.map
