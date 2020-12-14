using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using ChristmassCardGenerator.Models;
using ChristmassCardGenerator.DAL;
using System.Collections.ObjectModel;

namespace ChristmassCardGenerator.Areas.Identity.Pages.Account.Manage
{
    public partial class EmailListModel : PageModel
    {
        public IList<EmailList> emailList = new List<EmailList>();

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public EmailListModel(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            IdentityUser identityUser = await _userManager.GetUserAsync(User);
            if (identityUser == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            ApplicationUser user = _db.Users.FirstOrDefault(u => u.UserName == identityUser.UserName);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            emailList = user.EmailLists.ToList();

            return Page();
        }
    }
}
