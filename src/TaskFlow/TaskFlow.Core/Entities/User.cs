namespace TaskFlow.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }


        public User()
        {
            Created = DateTime.Now;
        }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
            Created = DateTime.Now;
        }

    }
}
