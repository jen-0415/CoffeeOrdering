namespace CoffeeOrdering.Models.Entities
{
    public class CoffeeOrder
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CoffeeType { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string MilkType { get; set; } = string.Empty;
        public string FlavorSyrup { get; set; } = string.Empty;
        public bool WhippedCream { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}