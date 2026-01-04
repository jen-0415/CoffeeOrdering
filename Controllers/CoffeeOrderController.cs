using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeOrdering.Data;
using CoffeeOrdering.Models;
using CoffeeOrdering.Models.Entities;

namespace CoffeeOrdering.Controllers
{
    public class CoffeeOrderController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public CoffeeOrderController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCoffeeOrderViewModel viewModel)
        {
            var price = CalculatePrice(viewModel.CoffeeType, viewModel.Size, viewModel.WhippedCream);

            var coffeeOrder = new CoffeeOrder
            {
                CustomerName = viewModel.CustomerName,
                CoffeeType = viewModel.CoffeeType,
                Size = viewModel.Size,
                MilkType = viewModel.MilkType,
                FlavorSyrup = viewModel.FlavorSyrup,
                WhippedCream = viewModel.WhippedCream,
                Price = price,
                OrderDate = DateTime.Now,
                Status = "Pending"
            };

            await dbContext.CoffeeOrders.AddAsync(coffeeOrder);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("List", "CoffeeOrder");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var orders = await dbContext.CoffeeOrders.OrderByDescending(o => o.OrderDate).ToListAsync();
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var order = await dbContext.CoffeeOrders.FindAsync(id);
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CoffeeOrder viewModel)
        {
            var order = await dbContext.CoffeeOrders.FindAsync(viewModel.Id);
            if (order is not null)
            {
                var price = CalculatePrice(viewModel.CoffeeType, viewModel.Size, viewModel.WhippedCream);

                order.CustomerName = viewModel.CustomerName;
                order.CoffeeType = viewModel.CoffeeType;
                order.Size = viewModel.Size;
                order.MilkType = viewModel.MilkType;
                order.FlavorSyrup = viewModel.FlavorSyrup;
                order.WhippedCream = viewModel.WhippedCream;
                order.Price = price;
                order.Status = viewModel.Status;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "CoffeeOrder");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CoffeeOrder viewModel)
        {
            var order = await dbContext.CoffeeOrders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);
            if (order is not null)
            {
                dbContext.CoffeeOrders.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "CoffeeOrder");
        }

        private decimal CalculatePrice(string coffeeType, string size, bool whippedCream)
        {
            decimal basePrice = coffeeType switch
            {
                "Sea Salt Latte" => 180m,
                "Americano" => 140m,
                "Matcha Latte" => 180m,
                "Cappuccino" => 150m,
                "Mocha" => 150,
                "Caramel Macchiato" => 170,
                _ => 3.00m
            };

            decimal sizeMultiplier = size switch
            {
                "Small" => 1.0m,
                "Medium" => 1.3m,
                "Large" => 1.6m,
                _ => 1.0m
            };

            decimal extras = whippedCream ? 25m : 0.00m;

            return (basePrice * sizeMultiplier) + extras;
        }
    }
}