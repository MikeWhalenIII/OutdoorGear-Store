using Microsoft.AspNetCore.Mvc;
using OutdoorGear_Store.Models;

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
    }
}
