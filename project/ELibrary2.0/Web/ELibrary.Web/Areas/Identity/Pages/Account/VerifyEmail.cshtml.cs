using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ELibrary.Data;
using ELibrary.Data.Models;

namespace ELibrary.Web
{
    public class VerifyEmailModel : PageModel
    {
        private readonly ELibrary.Data.ApplicationDbContext _context;

        public VerifyEmailModel(ELibrary.Data.ApplicationDbContext context)
        {
            this._context = context;
        }

        public IActionResult OnGet()
        {
            return this.Page();
        }

        [BindProperty]
        public VerificatedCode VerificatedCode { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!this.ModelState.IsValid)
            {
                return this.Page();
            }

            this._context.VerificatedCodes.Add(this.VerificatedCode);
            await this._context.SaveChangesAsync();

            return this.RedirectToPage("./Index");
        }
    }
}
