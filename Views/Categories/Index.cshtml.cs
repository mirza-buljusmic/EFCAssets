using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFCAssets.Models;

namespace EFCAssets.Views.Categories
{
    public class IndexModel : PageModel
    {
        private readonly EFCAssets.Models.AssetContext _context;

        public IndexModel(EFCAssets.Models.AssetContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await _context.Categories.ToListAsync();
        }
    }
}
