namespace TaskFlow.Core.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsComplete { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public Task()
        {
            Created = DateTime.Now;
        }

        public Task(string title, string description, DateTime dueDate, int userId)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            UserId = userId;
            Created = DateTime.Now;
        }
    }
}
