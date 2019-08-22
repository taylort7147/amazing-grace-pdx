using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Editor.Areas.Identity.Data;
using Editor.Authorization;
using Editor.Pages.Shared;

namespace Editor.Areas.Identity.Pages.Administrator.Users
{
    public class EditModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly EditorIdentityDbContext _context;

        public EditModel(UserManager<IdentityUser> userManager,
                         EditorIdentityDbContext context)
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

            ReadOnlyPermission = new CheckBoxModel{DisplayName="Read Only"};
            ReadOnlyPermission.IsChecked = await _userManager.IsInRoleAsync(IdentityUser, Constants.ReadOnlyRole);

            ReadWritePermission = new CheckBoxModel{DisplayName="Read/Write"};
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
            if(!isChecked &&
                    await _userManager.IsInRoleAsync(IdentityUser, role))
            {
                await _userManager.UpdateSecurityStampAsync(IdentityUser);
                await _userManager.RemoveFromRoleAsync(IdentityUser, role);
                await _context.SaveChangesAsync();
            }
            else if(isChecked &&
                    !(await _userManager.IsInRoleAsync(IdentityUser, role)))
            {
                await _userManager.UpdateSecurityStampAsync(IdentityUser);
                await _userManager.AddToRoleAsync(IdentityUser, role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
