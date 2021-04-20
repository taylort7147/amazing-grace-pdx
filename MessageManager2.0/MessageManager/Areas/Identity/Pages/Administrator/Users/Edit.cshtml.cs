using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessageManager.Areas.Identity.Authorization;
using MessageManager.Areas.Identity.Data;
using MessageManager.Pages.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MessageManager.Areas.Identity.Pages.Administrator.Users
{
    [Authorize(Roles = "Administrator")]
    public class EditModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityContext _context;

        public EditModel(UserManager<IdentityUser> userManager,
                         IdentityContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public IdentityUser IdentityUser { get; set; }

        [BindProperty]
        public CheckBoxModel ReadOnlyPermission { get; set; }

        [BindProperty]
        public CheckBoxModel ReadWritePermission { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IdentityUser = await _userManager.FindByIdAsync(id);
            if (IdentityUser == null)
            {
                return NotFound();
            }

            ReadOnlyPermission = new CheckBoxModel { DisplayName = "Read Only" };
            ReadOnlyPermission.IsChecked = await _userManager.IsInRoleAsync(IdentityUser, Constants.ReadOnlyRole);

            ReadWritePermission = new CheckBoxModel { DisplayName = "Read/Write" };
            ReadWritePermission.IsChecked = await _userManager.IsInRoleAsync(IdentityUser, Constants.ReadWriteRole);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await UpdateRole(Constants.ReadOnlyRole, ReadOnlyPermission.IsChecked);
            await UpdateRole(Constants.ReadWriteRole, ReadWritePermission.IsChecked);

            return RedirectToPage("./Index");
        }

        private async Task UpdateRole(string role, bool isChecked)
        {
            if (!isChecked &&
                    await _userManager.IsInRoleAsync(IdentityUser, role))
            {
                await _userManager.UpdateSecurityStampAsync(IdentityUser);
                await _userManager.RemoveFromRoleAsync(IdentityUser, role);
                await _context.SaveChangesAsync();
            }
            else if (isChecked &&
                    !(await _userManager.IsInRoleAsync(IdentityUser, role)))
            {
                await _userManager.UpdateSecurityStampAsync(IdentityUser);
                await _userManager.AddToRoleAsync(IdentityUser, role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
