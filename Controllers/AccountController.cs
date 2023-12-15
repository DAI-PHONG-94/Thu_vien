using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebLibary.Models.Entities;
using WebLibary.Repository;

namespace WebLibary.Controllers
{
    public class AccountController : Controller
    {
        

        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountController(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userRepository.GetUserByEmailAsync(model.Email);
                if (existingUser == null)
                {
                    model.PasswordHash = _passwordHasher.HashPassword(model, model.PasswordHash);
                    await _userRepository.CreateUserAsync(model,model.PasswordHash);

                   

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email đã được sử dụng.");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userRepository.GetUserByEmailAsync(model.Email);
                if (user != null && _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.PasswordHash) == PasswordVerificationResult.Success)
                {
                   

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không đúng.");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            
            await HttpContext.SignOutAsync();

            
            return RedirectToAction("Index", "Home");
        }
    }
}
