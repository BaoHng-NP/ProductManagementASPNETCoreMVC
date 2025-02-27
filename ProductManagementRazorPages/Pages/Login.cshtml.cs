using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Services;


namespace ProductManagementRazorPages.Pages
{
    public class LoginModel : PageModel
    {

        private IAccountService _accountService; // using Dependency Injection
        public LoginModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var loginId = HttpContext.Session.GetInt32("Account").ToString();
            if (!string.IsNullOrEmpty(loginId))
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        [BindProperty]
        public AccountMember AccountMember { get; set; } = default!;
        public string ErrorMessage { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var loginId = HttpContext.Session.GetInt32("Account").ToString();
            if (!string.IsNullOrEmpty(loginId))
            {
                return RedirectToPage("/Index");
            }

            var memberAccount = _accountService.GetAccountById(AccountMember.MemberId);

            if (memberAccount == null)
            {
                ErrorMessage = "You do not have permission to do this function!";
                ModelState.AddModelError(string.Empty, ErrorMessage);

                return Page();
            }
            else if (memberAccount.MemberRole == 1 || memberAccount.MemberRole == 2)
            {
                HttpContext.Session.SetInt32("Account", memberAccount.MemberRole);
                return RedirectToPage("/Index");
            }
            else
            {
                ErrorMessage = "You do not have permission to do this function!";
                ModelState.AddModelError(string.Empty, ErrorMessage);
                return Page();
            }
        }

    }
}
