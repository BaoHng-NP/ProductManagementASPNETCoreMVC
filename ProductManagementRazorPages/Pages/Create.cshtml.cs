﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using DataAccessObjects;
using Services;
using Microsoft.AspNetCore.SignalR;
using SignalRLab;

namespace ProductManagementRazorPages.Pages
{
    public class CreateModel : PageModel
    {

        private readonly IProductService _contextProduct;
        private readonly ICategoryService _contextCategory;
        private readonly IHubContext<SignalRServer> _hubContext;

        public CreateModel(IProductService context, ICategoryService categoryService, IHubContext<SignalRServer> hubContext)
        {
            _contextProduct = context;
            _contextCategory = categoryService;
            _hubContext = hubContext;
        }


        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_contextCategory.GetCategories(), "CategoryId", "CategoryName");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _contextProduct.SaveProduct(Product);
            await _hubContext.Clients.All.SendAsync("LoadAllItems");
            return RedirectToPage("./Index");
        }

    }
}
