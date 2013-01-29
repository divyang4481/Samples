/// <reference path="../jquery.d.ts" />
/// <reference path="../knockout.d.ts" />

module CustomBindings {

    ko.bindingHandlers["fadeVisible"] =
    {
        update: function(element, valueAccessor) {
            // On update, fade in/out
            var shouldDisplay = valueAccessor();
            shouldDisplay ? $(element).fadeIn() : $(element).fadeOut();
        }
    };

    class Answer {
        public points: KnockoutObservableNumber;

        constructor(public text: string) {
            this.points = ko.observable(1);
        }
    }

    class SurveyViewModel {

        public answers: Answer[];
        public pointsUsed: KnockoutObservableNumber;

        // Actions
        public save;

        constructor(public question: string, public pointsBudget: number, answers: string[]) {
            
            this.answers = $.map(answers, (text) => { return new Answer(text); });
            this.save = () => { alert('To do') };
                       
            this.pointsUsed = ko.computed(() => {
                var total: number = 0;
                for (var i = 0; i < this.answers.length; i++)
                    total += this.answers[i].points();
                return total;        
            }, this);
        }
    }

    $(() => {
        ko.applyBindings(new SurveyViewModel("Which factors affect your technology choices?", 10, [
           "Functionality, compatibility, pricing - all that boring stuff",
           "How often it is mentioned on Hacker News",    
           "Number of gradients/dropshadows on project homepage",        
           "Totally believable testimonials on project homepage"
        ]))
    });
}