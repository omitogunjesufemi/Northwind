﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NorthwindMvc.Models;
using System;
using Packt.Shared;

namespace NorthwindMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private Northwind db;

    public HomeController(ILogger<HomeController> logger, Northwind injectedContext)
    {
        _logger = logger;
        db = injectedContext;
    }

    public IActionResult Index()
    {
        var model = new HomeIndexViewModel
        {
            VisitorCount = (new Random()).Next(1,1001),
            Categories = db.Categories.ToList(),
            Products = db.Products.ToList()
        };
        return View(model);
    }

    public IActionResult ProductDetail(int? id)
    {
        if (!id.HasValue)
        {
            return NotFound("You must pass a product ID in the route, for example, /Home/ProductDetail/21");
        }
        var model = db.Products.SingleOrDefault(p=>p.ProductID == id);

        if (model == null)
        {
            return NotFound($"Product with ID of {id} not found.");
        }
        return View(model);
    }
    

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
