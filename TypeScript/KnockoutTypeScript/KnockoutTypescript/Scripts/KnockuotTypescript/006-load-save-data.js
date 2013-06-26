var LoadSaveData;
(function (LoadSaveData) {
    var Task = (function () {
        function Task(data) {
            this.Title = ko.observable(data.Title);
            this.IsDone = ko.observable(data.IsDone);
            this._destroy = data._destroy;
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
                    return !task.IsDone() && !task._destroy;
                });
            });
            this.addTask = function () {
                _this.tasks.push(new Task({
                    Title: _this.newTaskText()
                }));
                _this.newTaskText("");
            };
            this.removeTask = function (task) {
                _this.tasks.destroy(task);
            };
            this.save = function () {
                $.ajax("/Task/Save", {
                    data: ko.toJSON({
                        tasks: _this.tasks
                    }),
                    type: "post",
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {
                        $('#updated').text(result);
                    }
                });
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
        ko.applyBindings(new TaskListViewModel());
    });
})(LoadSaveData || (LoadSaveData = {}));
//@ sourceMappingURL=006-load-save-data.js.map
