using Microsoft.AspNetCore.Mvc;
using TodoList.Models;
using TodoList.ViewModels;

namespace TodoList.Controllers;

public class TestController : Controller {
    public IActionResult Index()
    {
        return View();
    }

}