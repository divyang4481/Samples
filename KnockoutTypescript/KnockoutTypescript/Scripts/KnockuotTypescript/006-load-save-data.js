var LoadSaveData;
(function (LoadSaveData) {
    var Task = (function () {
        function Task(data) {
            this.title = ko.observable(data.Title);
            this.isDone = ko.observable(data.IsDone);
        }
        return Task;
    })();    
    var TaskListViewModel = (function () {
        function TaskListViewModel() {
            var _this = this;
            this.tasks = ko.observableArray([]);
            this.newTaskText = ko.observable();
            this.incompleteTasks = ko.computed(function () {
                return ko.utils.arrayFilter(_this.tasks(), function (task) {
                    return !task.isDone();
                });
            });
            this.addTask = function () {
                _this.tasks.push(new Task({
                    title: _this.newTaskText()
                }));
                _this.newTaskText("");
            };
            this.removeTask = function (task) {
                _this.tasks.remove(task);
            };
            $.getJSON("/Task", function (allData) {
                var mappedTasks = $.map(allData, function (item) {
                    return new Task(item);
                });
                _this.tasks(mappedTasks);
            });
        }
        return TaskListViewModel;
    })();    
    $(function () {
        return ko.applyBindings(new TaskListViewModel());
    });
})(LoadSaveData || (LoadSaveData = {}));
//@ sourceMappingURL=006-load-save-data.js.map
