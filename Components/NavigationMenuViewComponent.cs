﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OutdoorGear_Store.Models;

namespace OutdoorGear_Store.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRepository repository; public NavigationMenuViewComponent(IProductRepository repo) { repository = repo; }
        public IViewComponentResult Invoke()
        {
            return View(repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}