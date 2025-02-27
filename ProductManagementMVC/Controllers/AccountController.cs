using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace ProductManagementMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService; // Inject your account service

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([Bind("EmailAddress,MemberPassword")] AccountMember model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(x => x.Value.Errors.Count > 0)
    .Select(x => new { x.Key, x.Value.Errors })
    .ToArray();
            }
            if (ModelState.IsValid)
            {
                var user = _accountService.GetAccountById(model.EmailAddress);

                if (user != null && user.MemberPassword.Trim() == model.MemberPassword)
                    if (user != null && user.MemberPassword.Trim() == model.MemberPassword)
                {
                    // Store user information in session
                    HttpContext.Session.SetString("UserId", user.MemberId);
                    HttpContext.Session.SetString("Username", user.FullName);

                    return RedirectToAction("Index", "Products"); // Redirect to home page
                    }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }


            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear session data
            return RedirectToAction("Login");
        }
    }

}
