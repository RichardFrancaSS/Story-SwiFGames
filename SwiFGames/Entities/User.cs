namespace SwiFGames.Entities
{
    internal class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; private set; }
        public  string Category { get; set; }

        public User()
        {

        }

        public User(int id,string name, string email, string phone, string password, string category)
        {
            UserId = id;
            Name = name;
            Email = email;
            Phone = phone;
            Password = password;
            Category = category;
        }
    }
}
