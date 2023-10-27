using SwiFGames.Entities.Enums;
namespace SwiFGames.Entities
{
 internal class Order
    {
        public DateTime Moment { get; set; }
        Status Status { get; set; }
        Customer? Customer { get; set; }
        public  List<Product> Products { get; set; } = new List<Product>();

        public Order()
        {
             
        }

        public Order(DateTime moment, Status status, Customer? customer)
        {
            Moment = moment;
            Status = status;
            Customer = customer;
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
