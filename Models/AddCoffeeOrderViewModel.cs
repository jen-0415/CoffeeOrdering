namespace CoffeeOrdering.Models
{
    public class AddCoffeeOrderViewModel
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CoffeeType { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string FlavorSyrup { get; set; } = string.Empty;
        public string MilkType { get; set; } = string.Empty;
        public bool WhippedCream { get; set; }
    }
}