using hotelASP.Data;
using hotelASP.Entities;
using hotelASP.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace hotelASP.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Registration()
        {
            ViewBag.Roles = _context.Roles.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid) {
                var existingUser = _context.Users.Any(u => u.Email == model.Email || u.Username == model.Username);
                if (existingUser)
                {
                    ModelState.AddModelError("", "Email lub nazwa użytkownika już istnieje.");
                    ViewBag.Roles = _context.Roles.ToList();
                    return View(model);
                }

                UserAccount account = new UserAccount
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    Username = model.Username,
                    RoleId = model.RoleId
                };

                _context.Users.Add(account);
                _context.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = $"Użytkownika {account.FirstName} {account.LastName} zarejestrowano pomyślnie. Proszę zaloguj się.";
                ViewBag.Roles = _context.Roles.ToList();
            }

            return View(model);

        }


        public IActionResult Login()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid) 
            {
                var user = _context.Users
                    .Include(u => u.Role)
                    .Where(x => (x.Username == model.UsernameOrEmail || x.Email == model.UsernameOrEmail) && x.Password == model.Password).FirstOrDefault();
                if (user != null)
                {
                    //success
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim("Name", user.FirstName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role.Name),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Username/Email lub hasło jest niepoprawne.");
                }

            }

            return View(model);
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

    }
}
