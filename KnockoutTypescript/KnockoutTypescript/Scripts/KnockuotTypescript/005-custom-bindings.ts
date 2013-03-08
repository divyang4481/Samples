/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/jqueryui/jqueryui.d.ts" />
/// <reference path="../typings/knockout/knockout.d.ts" />

// Expand knockout interface
interface KnockoutBindingHandlers {
    jqButton: KnockoutBindingHandler;
}

module CustomBindings {

    // New binding handler declaration
    ko.bindingHandlers["fadeVisible"] =
    {
         init: function(element, valueAccessor) {
            // Start visible/invisible according to initial value
            var shouldDisplay = valueAccessor();
            $(element).toggle(shouldDisplay);
        },

        update: function(element, valueAccessor) {
            // On update, fade in/out
            var shouldDisplay = valueAccessor();
            shouldDisplay ? $(element).fadeIn() : $(element).fadeOut();
        }
    };

    ko.bindingHandlers.jqButton = {
        init: function(element) {
           $(element).button(); // Turns the element into a jQuery UI button
        },
        update: function (element, valueAccessor) {
            var currentValue = valueAccessor();
            // Here we just update the "disabled" state, but you could update other properties too
            $(element).button("option", "disabled", currentValue.enable === false);
        }
    };

    ko.bindingHandlers["starRating"] = {
        init: function(element, valueAccessor) {
            $(element).addClass("starRating");
            for (var i = 0; i < 5; i++)
               $("<span>").appendTo(element);

            // Handle mouse events on the stars
            $("span", element).each(function(index) {
                $(this).hover(
                    function() { $(this).prevAll().add(this).addClass("hoverChosen") }, 
                    function() { $(this).prevAll().add(this).removeClass("hoverChosen") }                
                ).click(function() { 
                   var observable = valueAccessor();  // Get the associated observable
                   observable(index+1);               // Write the new rating to it
                 }); 
            });

        },
        update: function(element, valueAccessor) {
            // Give the first x stars the "chosen" class, where x <= rating
            var observable = valueAccessor();
            $("span", element).each(function(index) {
                $(this).toggleClass("chosen", index < observable());
            });
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
        public pointsUsed: KnockoutComputed;

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
        ko.applyBindings(new SurveyViewModel("Which factors affect your technology choices?", 10,
        [
           "Functionality, compatibility, pricing - all that boring stuff",
           "How often it is mentioned on Hacker News",
           "Number of gradients/dropshadows on project homepage",
           "Totally believable testimonials on project homepage"
        ]));
    });
}