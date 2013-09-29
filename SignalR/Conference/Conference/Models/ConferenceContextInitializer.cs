using System;
using System.Data.Entity;

namespace Conference.Models
{
    [Obsolete]
    public class ConferenceContextInitializer : DropCreateDatabaseIfModelChanges<ConferenceContext>
    {
        protected override void Seed(ConferenceContext context)
        {
            context.Sessions.Add(new Session
                {
                    Title = "Title1",
                    Abstract = "Abstract",
                    Speaker = context.Speakers.Add(new Speaker
                        {
                            Name = "Adam Smith",
                            EmailAddress = "adam.smith@foo.com"
                        })
                });

            context.SaveChanges();
        }
    }
}