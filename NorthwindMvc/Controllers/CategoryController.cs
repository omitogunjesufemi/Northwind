using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NorthwindMvc.Models;
using System;
using Packt.Shared;
using Microsoft.EntityFrameworkCore;

namespace NorthwindMvc.Controllers
{
    public class CategoryController:Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private Northwind db;
        public CategoryController(ILogger<CategoryController> logger, Northwind injectedContext)
        {
            _logger = logger;
            db = injectedContext;
        }

        public async Task<IActionResult> CategoryDetail(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound("Please pass in a query parameter to locate the category");
            }
            var model = await db.Categories
            .Include(p=>p.Products)
            .SingleOrDefaultAsync(c=>c.CategoryID == id);

            if (model == null)
            {
                return NotFound($"Category with ID of {id} not found.");
            }

            ViewData["ProductCount"] = Convert.ToString(model.Products.Count());

            return View(model);
        }
        
    }
}