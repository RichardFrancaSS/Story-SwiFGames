using SwiFGames.Entities.Enums;
namespace SwiFGames.Entities
{
 internal class Order
    {
        public int OrderId { get; set; }
        public DateTime Moment { get; set; }
        public StatusOrder Status { get; set; }
        public Customer? Customer { get; set; }
        public  List<Product> Products { get; set; } = new List<Product>();

        public Order()
        {
             
        }

        public Order(DateTime moment, StatusOrder status, Customer? customer, int orderId)
        {
            Moment = moment;
            Status = status;
            Customer = customer;
            OrderId = orderId;
        }
        void AddProducttotheOrder(Product product)
        {
            Products.Add(product);
        }
        void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }
    }
    
}
