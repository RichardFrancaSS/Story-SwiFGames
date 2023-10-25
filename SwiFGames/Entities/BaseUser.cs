using System.Text;
namespace SwiFGames.Entities
{
    internal class BaseUser
    {
        public List<User> users { get; set; } = new List<User>();
        public void AddNewUserAtBase(User user){ users.Add(user);}
        public void RemoveUserAtBase(User user){ users.Remove(user);}
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("*******************************");
            foreach (User user in users)
            {
                sb.AppendLine();
                sb.Append("Id: ");
                sb.AppendLine(user.UserId.ToString());
                sb.Append("Name: ");
                sb.AppendLine(user.Name);
                sb.Append("E-mail: ");
                sb.AppendLine(user.Email);
                sb.Append("Phone: ");
                sb.AppendLine(user.Phone);
                sb.Append("Category: ");
                sb.AppendLine(user.Category);
                sb.Append("*******************************");
            }
            return sb.ToString();
        }
    }
}
