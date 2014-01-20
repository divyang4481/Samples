using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KatanaBugTracker.Models
{
    public class BugsRepository : IBugsRepository
    {
        private static List<Bug> Bugs = new List<Bug>
        {
            new Bug { Id = 1, Title = "Logging in does not work", Description = "Cannot login user test1", State = BugState.Backlog},
            new Bug { Id = 2, Title = "JavaScript error in IE8", Description = "Knockout js error during pdf export", State = BugState.Backlog},
            new Bug { Id = 3, Title = "NullReferenceException", Description = "NullReferenceException when trying to assign user to task", State = BugState.Working},
            new Bug { Id = 4, Title = "Cannot login with facebook", Description = "Cannot login using facebook account", State = BugState.Done},
            new Bug { Id = 5, Title = "SQL query too slow", Description = "Users seach query is too slow with no filters defined", State = BugState.Done}
        };

        public IEnumerable<Bug> GetBugs()
        {
            return Bugs;
        }

        public Bug GetBugById(int id)
        {
            return Bugs.FirstOrDefault(b => b.Id == id);
        }
    }
}