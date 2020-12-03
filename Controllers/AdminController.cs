using Microsoft.AspNetCore.Mvc;
using OutdoorGear_Store.Models;
using System.Linq;

namespace OutdoorGear_Store.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository repository;
        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index() => View(repository.Products);

        public ViewResult Edit(int productId) =>
            View(repository.Products.FirstOrDefault(p => p.ProductID == productId));
    }
}
