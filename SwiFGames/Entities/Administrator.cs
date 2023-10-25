namespace SwiFGames.Entities
{
 internal class Administrator : User
    {

        public Administrator()
        {

        }
        public Administrator(int id, string name, string email, string phone, string password, string category)
            : base(id, name, email, phone, password, category)
        {
            Category = category;
        }
    }
}
