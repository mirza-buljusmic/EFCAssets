using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFCAssets.Models;

namespace EFCAssets.Controllers
{
    public class AssetsController : Controller
    {
        private readonly AssetContext _context;

        public AssetsController(AssetContext context)
        {
            _context = context;
        }

        // GET: Assets
        public async Task<IActionResult> Index()
        {
            var assetContext = _context.Assets.Include(a => a.Category).Include(a => a.Office);
            var assetContextSorted = assetContext.OrderBy(x => x.Office.OfficeName).ThenBy(x => x.Category.CategoryName);

            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            ViewData["CategoryId"] = new SelectList(_context.Categories, nameof(Category.CategoryId), nameof(Category.CategoryName));
            //ViewData["OfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeId");
            ViewData["OfficeId"] = new SelectList(_context.Offices, nameof(Office.OfficeId), nameof(Office.OfficeName));

            return View(await assetContextSorted.ToListAsync());
        }

        // GET: Assets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                .Include(a => a.Category)
                .Include(a => a.Office)
                .FirstOrDefaultAsync(m => m.AssetId == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // GET: Assets/Create
        public IActionResult Create()
        {
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            ViewData["CategoryId"] = new SelectList(_context.Categories, nameof(Category.CategoryId), nameof(Category.CategoryName));
            //ViewData["OfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeId");
            ViewData["OfficeId"] = new SelectList(_context.Offices, nameof(Office.OfficeId), nameof(Office.OfficeName));
            return View();
        }

        // POST: Assets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssetId,AssetName,AssetPurchaseDate,AssetExpirationDate,AssetWarningDate,AssetPrice,AssetActive,OfficeId,CategoryId")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", asset.CategoryId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeName", asset.OfficeId);
            return View(asset);
        }

        // GET: Assets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", asset.CategoryId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeName", asset.OfficeId);
            return View(asset);
        }

        // POST: Assets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssetId,AssetName,AssetPurchaseDate,AssetExpirationDate,AssetWarningDate,AssetPrice,AssetActive,OfficeId,CategoryId")] Asset asset)
        {
            if (id != asset.AssetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetExists(asset.AssetId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", asset.CategoryId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeName", asset.OfficeId);
            return View(asset);
        }

        // GET: Assets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                .Include(a => a.Category)
                .Include(a => a.Office)
                .FirstOrDefaultAsync(m => m.AssetId == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // POST: Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asset = await _context.Assets.FindAsync(id);
            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetExists(int id)
        {
            return _context.Assets.Any(e => e.AssetId == id);
        }
    }
}
