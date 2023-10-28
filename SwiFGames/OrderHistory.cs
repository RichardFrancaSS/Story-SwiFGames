using SwiFGames.Entities;
using System.Globalization;
using System.Text;

namespace SwiFGames
{
    internal class OrderHistory
    {
       public List<Order> orders { get; set; } = new List<Order>();
        

        public void AddOrder(Order order)
        {
            orders.Add(order);
        }
        public void RemoveOrder(Order order)
        {
            orders.Remove(order);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Order order in orders)
            {
               
                sb.Append("ID: ");
                sb.Append(order.OrderId);
                sb.AppendLine();
                sb.Append ("Data do Pedido: ");
                sb.Append(order.Moment);
                sb.AppendLine();
                sb.Append("Status: ");
                sb.Append(order.Status);
                sb.AppendLine();
                sb.AppendLine();
                sb.Append("***********************");
                sb.Append("Produtos no Carrinho: ");
                sb.Append("***********************");
                sb.AppendLine();

                foreach (Product prod in order.Products)
                {
                    sb.AppendLine();
                    sb.Append("Id: ");
                    sb.AppendLine(prod.ProductId.ToString());
                    sb.Append("Name: ");
                    sb.AppendLine(prod.Name);
                    sb.Append("Description: ");
                    sb.AppendLine(prod.Description);
                    sb.Append("Price: ");
                    sb.AppendLine(prod.Price.ToString("F2", CultureInfo.InvariantCulture));
                }

                sb.AppendLine();
                sb.Append("================================================================================================================");
                sb.AppendLine();

            }
            return sb.ToString();
        }

    }
}
