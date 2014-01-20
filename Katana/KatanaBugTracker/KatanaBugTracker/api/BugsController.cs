using System;
using System.Collections.Generic;
using System.Linq;
using KatanaBugTracker.Models;
using System.Web.Http;

namespace KatanaBugTracker.Api
{
    [RoutePrefix("api/bugs")]
    public class BugsController : ApiController
    {
        IBugsRepository _bugsRepository = new BugsRepository();

        public IEnumerable<Bug> Get()
        {
            return _bugsRepository.GetBugs();
        }
        
        [Route("backlog")]
        public Bug MoveToBacklog([FromBody] int id)
        {
            var bug = _bugsRepository.GetBugById(id);
            bug.State = BugState.Backlog;
            return bug;
        }

        [Route("working")]
        public Bug MoveToWorking([FromBody] int id)
        {
            var bug = _bugsRepository.GetBugById(id);
            bug.State = BugState.Working;
            return bug;
        }

        [Route("done")]
        public Bug MoveToDone([FromBody] int id)
        {
            var bug = _bugsRepository.GetBugById(id);
            bug.State = BugState.Done;
            return bug;
        }
    }
}