/// <reference path="typings/signalr/signalr.d.ts" />
/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />

enum BugState
{
    Undefined,
    Backlog,
    Working,
    Done
}

class Bug
{
    Id: number;
    Title: string;
    Description: string;
    State: BugState;
}

class ViewModel {
    Backlog: KnockoutObservableArray<Bug>;
    Working: KnockoutObservableArray<Bug>;
    Done: KnockoutObservableArray<Bug>;
    draggingBug: KnockoutObservable<Bug>; // bug that is being dragged currently 
    dragOverBug: KnockoutObservable<Bug>; // target bug on which we drop 

    constructor(model: any) {
        this.Backlog = model.filter(bug => { return bug.State === BugState.Backlog });
        this.Working = model.filter(bug => { return bug.State === BugState.Working });
        this.Done = model.filter(bug => { return bug.State === BugState.Done });
    }

    changeState(bug: Bug, newState: BugState) {
        var self = this;
        $.post(apiUrl + BugState[newState], { '': bug.Id }, data => {
            self.moveBug(data);
        });
    }

    moveBug(bug: Bug) {
        [this.Backlog, this.Working, this.Done].forEach(list => {
            list().forEach(item => {
                if (item.Id == bug.Id) {
                    console.log('removing item ' + item.Id);
                    list.remove(item);
                }
            });
        });

        this[BugState[bug.State]].push(bug);
    }

}

var apiUrl = '/api/bugs/';

class DragAndDropUtility
{
    constructor() { 
    }

    static handleDragStart(bug: Bug, event) {
        viewModel.draggingBug(bug);

        // Knockout makes all events preventDefault / return false => I have to return true
        // see: http://stackoverflow.com/questions/7218171/knockout-html5-drag-and-drop

        return true;
    }

    static handleDragOver(bug: Bug, event) {
        if (event.preventDefault) { event.preventDefault(); } // Necessary. Allows to drop.
    }

    static handleDragEnter(bug: Bug, event) {
        if (event.preventDefault) { event.preventDefault(); }

        // this / e.target is the current hover target.

        if (viewModel.draggingBug().Id != bug.Id) {
            viewModel.dragOverBug(bug);
        }
    }

    static handleDrop(bug: Bug, event) {
        if (event.stopPropagation) { event.stopPropagation(); }

        if (bug.Id != viewModel.draggingBug().Id) {
            viewModel.changeState(viewModel.draggingBug(), bug.State);
        }

        

        return false;
    }

    static handleDragEnd(bug: Bug, event)
    {
        if (event.preventDefault) { event.preventDefault(); }

        viewModel.dragOverBug(null);
        viewModel.draggingBug(null);
    }
}

var viewModel;

function setUpSignalR()
{
    // Use this for debugging purposes
    $.connection.hub.logging = false;
    
    // Used [] notation to avoid TypeScript error
    // I could also add custom definition file (.d.ts)
    var bugsHub = $.connection["bugs"]; 

    bugsHub.client.moved = item => {
        viewModel.moveBug(item);
    };

    $.connection.hub.start().done(() => {
        console.log('hub connection opened');
    });
}

$(() => {
    setUpSignalR();

    $.getJSON(apiUrl, function (data) {
        var model = data;

        viewModel = {
            Backlog: ko.observableArray(
                model.filter(function (element) { return element.State === BugState.Backlog })),
            Working: ko.observableArray(
                model.filter(function (element) { return element.State === BugState.Working })),
            Done: ko.observableArray(
                model.filter(function (element) { return element.State === BugState.Done })),
            changeState: function (bug, newState) {
                var self = this;
                $.post(apiUrl + BugState[newState], { '': bug.Id }, function (data) {
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

                this[BugState[bug.State]].push(bug);
            },
            draggingBug: ko.observable(),
            dragOverBug: ko.observable(),
        };

        ko.applyBindings(viewModel);
    });
});