using Microsoft.AspNetCore.Mvc;
using Modules.Models; 
using Modules.Data;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
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
            
            if (_context.Users.Any(u => u.UserName == model.UserName))
            {
                ModelState.AddModelError("Username", "Bu kullanıcı adı zaten alınmış.");
                return View(model);
            }

            
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            
         

            _context.Users.Add(model);
            _context.SaveChanges();

            
            return RedirectToAction("Index", "Home");
        }

        
        return View(model);
    }



    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]


    public async Task<IActionResult>  Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
        if (user == null)
        {
            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            ModelState.AddModelError("", "Invalid email or password.");
            return View(model);
        }

        HttpContext.Session.SetString("UserId", user.Id.ToString());
        HttpContext.Session.SetString("UserName", user.UserName);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}

