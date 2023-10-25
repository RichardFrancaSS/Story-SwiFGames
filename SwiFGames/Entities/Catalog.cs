namespace SwiFGames.Entities
{
    internal class Catalog
    {

        public List<Product> products = new List<Product>();

        public void AddToTheCatalog(Product product){ products.Add(product);}
        public void RemoveTheCatalog(Product product){ products.Remove(product);}
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("*******************************");
            foreach (Product prod in products)
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
                sb.Append("*******************************");
            }
            return sb.ToString();

        }
    }
}