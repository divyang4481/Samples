/// <reference path="../jquery.d.ts" />
/// <reference path="../knockout.d.ts" />
/// <reference path="../jqueryui.d.ts" />

module LoadSaveData {

    class Task {
        public Title: KnockoutObservableString;
        public IsDone: KnockoutObservableBool;

        constructor(data: any) {
            this.Title = ko.observable(data.Title);
            this.IsDone = ko.observable(data.IsDone);
        }
    }

    class TaskListViewModel {

        // Data
        public tasks: KnockoutObservableArray;
        public newTaskText: KnockoutObservableString;
        public incompleteTasks: KnockoutComputed;

        // Operations
        public addTask: Function;
        public removeTask: Function;
        public save: Function;

        constructor() {
            // Data
            this.tasks = ko.observableArray([]);
            this.newTaskText = ko.observable();
            this.incompleteTasks = ko.computed(() => {
                return ko.utils.arrayFilter(this.tasks(), (task: Task) => { return !task.IsDone() });
            });

            // Operations
            this.addTask = () => {
                this.tasks.push(new Task({ Title: this.newTaskText() }));
                this.newTaskText("");
            };
            this.removeTask = (task: Task) => { this.tasks.remove(task) };

            this.save = () => {
                $.ajax("/Task/Save", {
                    data: ko.toJSON({ tasks: this.tasks }),
                    type: "post", 
                    success: function (result) 
                    { 
                        alert(result); 
                    }
                });
            }; 

             // Load initial state from server, convert it to Task instances, then populate self.tasks
            $.getJSON("/Task", (allData) => {
                var mappedTasks = $.map(allData, (item) => { return new Task(item) });
                this.tasks(mappedTasks);
            });  
        }
    }

    $(() => { 
        ko.applyBindings(new TaskListViewModel());        
    });
}