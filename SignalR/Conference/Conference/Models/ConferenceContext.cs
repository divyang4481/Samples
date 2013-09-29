using System.Data.Entity;

namespace Conference.Models
{
    public class ConferenceContext : DbContext
    {
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}