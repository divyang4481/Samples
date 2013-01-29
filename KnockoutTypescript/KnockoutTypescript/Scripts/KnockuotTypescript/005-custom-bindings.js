var CustomBindings;
(function (CustomBindings) {
    ko.bindingHandlers["fadeVisible"] = {
        init: function (element, valueAccessor) {
            var shouldDisplay = valueAccessor();
            $(element).toggle(shouldDisplay);
        },
        update: function (element, valueAccessor) {
            var shouldDisplay = valueAccessor();
            shouldDisplay ? $(element).fadeIn() : $(element).fadeOut();
        }
    };
    ko.bindingHandlers["jqButton"] = {
        init: function (element) {
            $(element).button();
        },
        update: function (element, valueAccessor) {
            var currentValue = valueAccessor();
            $(element).button("option", "disabled", currentValue.enable === false);
        }
    };
    var Answer = (function () {
        function Answer(text) {
            this.text = text;
            this.points = ko.observable(1);
        }
        return Answer;
    })();    
    var SurveyViewModel = (function () {
        function SurveyViewModel(question, pointsBudget, answers) {
            this.question = question;
            this.pointsBudget = pointsBudget;
            var _this = this;
            this.answers = $.map(answers, function (text) {
                return new Answer(text);
            });
            this.save = function () {
                alert('To do');
            };
            this.pointsUsed = ko.computed(function () {
                var total = 0;
                for(var i = 0; i < _this.answers.length; i++) {
                    total += _this.answers[i].points();
                }
                return total;
            }, this);
        }
        return SurveyViewModel;
    })();    
    $(function () {
        ko.applyBindings(new SurveyViewModel("Which factors affect your technology choices?", 10, [
            "Functionality, compatibility, pricing - all that boring stuff", 
            "How often it is mentioned on Hacker News", 
            "Number of gradients/dropshadows on project homepage", 
            "Totally believable testimonials on project homepage"
        ]));
    });
})(CustomBindings || (CustomBindings = {}));
//@ sourceMappingURL=005-custom-bindings.js.map
