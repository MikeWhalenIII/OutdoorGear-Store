using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OutdoorGear_Store.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IEnumerable<Product> Products => new List<Product> {
            new Product { Name = "Wallet", Price = 25 },
            new Product { Name = "Backpack", Price = 56 },
            new Product { Name = "Running Shoes", Price = 95 }
            };
    }
}
