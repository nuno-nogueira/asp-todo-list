using Microsoft.AspNetCore.Mvc;
using TodoList.Contexts;
using TodoList.Models;
using TodoList.ViewModels;

namespace TodoList.Controllers;

public class TodoController : Controller
{
    private readonly AppDbContext _context;

    public TodoController(AppDbContext context)
    {
        _context = context;    
    }

    public IActionResult Index()
    {
        var todos = _context.Todos.ToList();
        var viewModel = new ListTodoViewModel { Todos = todos };
        ViewData["Title"] = "Todo List";
        return View(viewModel);
    }

    public IActionResult Delete(int id)
    {
        var todo = _context.Todos.Find(id);

        if (todo is null) return NotFound();
    
        _context.Remove(todo);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Create()
    {
        ViewData["Title"] = "Create a task";
        return View();
    }

    
    [HttpPost]
    public IActionResult Create(CreateTodoViewModel data)
    {
        var todo = new Todo(data.Title, data.Date);
        _context.Add(todo);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var todo = _context.Todos.Find(id);
        if (todo is null) return NotFound();

        ViewData["Title"] = "Edit a task";

        var viewModel = new EditTodoViewModel {Title = todo.Title, Date = todo.Date};

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Edit(int id, EditTodoViewModel data)
    {
        var todo = _context.Todos.Find(id);
        if (todo is null) return NotFound();

        todo.Title = data.Title;
        todo.Date = data.Date;
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult ToComplete(int id)
    {
        var todo = _context.Todos.Find(id);
        if (todo is null) return NotFound();

        todo.IsCompleted = true;
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}

