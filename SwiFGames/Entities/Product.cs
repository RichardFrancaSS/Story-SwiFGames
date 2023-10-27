namespace SwiFGames.Entities
{
    internal class Product
    {
        public int ProductId { get; set; }
        public  string?  Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Product()
        {
             
        }

        public Product(int id,string? name, string? description, double price, int quantity)
        {
            ProductId = id;
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
        }
    }

    
}
