using Microsoft.AspNetCore.Mvc;
using Modules.Models; 
using Modules.Data;   

public class AccountController : Controller
{
    private readonly DataContext _context;

    public AccountController(DataContext context)
    {
        _context = context;
    }

    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(Register model) 
    {
       
        if (ModelState.IsValid)
        {
            
            if (_context.Users.Any(u => u.Username == model.Username))
            {
                ModelState.AddModelError("Username", "Bu kullanıcı adı zaten alınmış.");
                return View(model);
            }

           
            _context.Users.Add(model);
            _context.SaveChanges();

            
            return RedirectToAction("Index", "Home");
        }

        
        return View(model);
    }
}
