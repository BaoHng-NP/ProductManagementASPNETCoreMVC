using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;
using Services;

namespace ProductManagementRazorPages.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IProductService _context;
        private readonly ICategoryService _categoryService;

        public DeleteModel(IProductService context, ICategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }


        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.GetProductById((int)id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = product;
            }
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.GetProductById((int)id);
            if (product != null)
            {
                Product = product;
                _context.DeleteProduct(product);
            }

            return RedirectToPage("./Index");
        }

    }
}
