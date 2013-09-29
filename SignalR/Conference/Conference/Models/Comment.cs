namespace Conference.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int SessionId { get; set; }
        public virtual Session Session { get; set; }
    }
}