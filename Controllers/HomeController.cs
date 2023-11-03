using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sesion_Taller.Models;

namespace Sesion_Taller.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SetName(GameModel user)
    {
        HttpContext.Session.SetString("name", user.Name);
        HttpContext.Session.SetInt32("Value", 22);
        return RedirectToAction("Game");
    }

    [HttpPost]
    public IActionResult UpdateValue(int increment)
    {
        int currentValue = HttpContext.Session.GetInt32("Value") ?? 0;
        switch (increment)
        {
            case 1:
                currentValue += 1;
                break;
            case -1:
                currentValue -= 1;
                break;
            case 2:
                currentValue *= 2;
                break;
            case 0:
                Random random = new();
                int randomIncrement = random.Next(1, 11);
                currentValue += randomIncrement;
                break;
        }

        HttpContext.Session.SetInt32("Value", currentValue);

        return RedirectToAction("Game");
    }

    public IActionResult Game()
    {
        string Name = HttpContext.Session.GetString("name");

        int value = HttpContext.Session.GetInt32("value") ?? 22;

        GameModel user = new GameModel { Name = Name, Value = value };

        return View("Game", user);
    }
}
