using Microsoft.AspNetCore.Mvc;
using OutdoorGear_Store.Models;

namespace OutdoorGear_Store.Controllers
{
    public class OrderController : Controller
    {
        public ViewResult Checkout() => View(new Order());
    }
}
