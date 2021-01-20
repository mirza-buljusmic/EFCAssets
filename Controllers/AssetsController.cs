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
            // Select only assets whose category is active
            var assetContext = _context.Assets.Include(a => a.Category).Include(a => a.Office).Where(a=>a.Category.CategoryActive == true);
            var assetContextSorted = assetContext.OrderBy(x => x.Office.OfficeName).ThenBy(x => x.Category.CategoryName);

            ViewData["CategoryId"] = new SelectList(_context.Categories, nameof(Category.CategoryId), nameof(Category.CategoryName));
            ViewData["OfficeId"] = new SelectList(_context.Offices, nameof(Office.OfficeId), nameof(Office.OfficeName));

            return View(await assetContextSorted.ToListAsync());
        }

       

        public async Task<IActionResult> InactiveAssets()
        {
            var assetContext = _context.Assets.Include(a => a.Category).Include(a => a.Office);
            var assetContextSorted = assetContext.OrderBy(x => x.Office.OfficeName).ThenBy(x => x.Category.CategoryName);

            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            ViewData["CategoryId"] = new SelectList(_context.Categories, nameof(Category.CategoryId), nameof(Category.CategoryName));
            //ViewData["OfficeId"] = new SelectList(_context.Offices, "OfficeId", "OfficeId");
            ViewData["OfficeId"] = new SelectList(_context.Offices, nameof(Office.OfficeId), nameof(Office.OfficeName));

            return View(await assetContextSorted.ToListAsync());
        }

        public async Task<IActionResult> ActivateAsset(int id)
        {
            var asset = await _context.Assets.FirstOrDefaultAsync(a => a.AssetId == id);
            asset.AssetActive = true;
            _context.Update(asset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(InactiveAssets));
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
                // Calculate End Of Life and Warning date

                var lifespan = await _context.Categories.FirstOrDefaultAsync(b => b.CategoryId == asset.CategoryId);
                asset.AssetExpirationDate = asset.AssetPurchaseDate.AddMonths(lifespan.CategoryEOLMonths);
                asset.AssetWarningDate = asset.AssetPurchaseDate.AddMonths(lifespan.CategoryEOLMonths-3);
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
                    // Calculate End Of Life and Warning date
                    var lifespan = await _context.Categories.FirstOrDefaultAsync(b => b.CategoryId == asset.CategoryId);
                    asset.AssetExpirationDate = asset.AssetPurchaseDate.AddMonths(lifespan.CategoryEOLMonths);
                    asset.AssetWarningDate = asset.AssetPurchaseDate.AddMonths(lifespan.CategoryEOLMonths - 3);
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
            // Assets should be deactivated instead of deleted
            asset.AssetActive = false;
            _context.Update(asset);
            //_context.Assets.Remove(asset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetExists(int id)
        {
            return _context.Assets.Any(e => e.AssetId == id);
        }

        /// <summary>
        /// Recalculates all the Expiration and Warning dates in case of inconstistencies in DB
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AdminRecalc()
        {
            var assetContext = _context.Assets.Include(a => a.Category).Include(a => a.Office).ToList();
            foreach (var item in assetContext)
            {
                // recalculate End Of Life and Warning date
                var lifespan = await _context.Categories.FirstOrDefaultAsync(b => b.CategoryId == item.CategoryId);
                item.AssetExpirationDate = item.AssetPurchaseDate.AddMonths(lifespan.CategoryEOLMonths);
                item.AssetWarningDate = item.AssetPurchaseDate.AddMonths(lifespan.CategoryEOLMonths - 3);
                _context.Update(item);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeprRecalc()
        {

            // Assumption: straight-line depreciation, with 10% salvage value of purchase price

            // Get all the assets
            var assetContext = _context.Assets.Include(a => a.Category).ToList();

            foreach (var item in assetContext)
            {
                // get the lifespan of asset
                var lifespan = (item.AssetExpirationDate.Year - item.AssetPurchaseDate.Year);
                // multiplier for acumulative depreciation
                var deprYearMult = (DateTime.Now.Year - item.AssetPurchaseDate.Year);
                // salvage value = 10% of purchase price
                var salvageValue = item.AssetPrice * 0.1m;
                // yearly depreciation
                decimal deprYear = decimal.Round((item.AssetPrice * 0.9m) / lifespan, 2);
                // acumulated depreciation
                var totalDepr = decimal.Round((deprYear * deprYearMult), 3);

                // If expiration date has passed set value to salvage value (10%)
                if (item.AssetExpirationDate < DateTime.Today)
                {
                    item.AssetValue = salvageValue;
                }
                // expiration date has not yet passed
                else
                {
                    if (GetMonthDifference(item.AssetExpirationDate, DateTime.Today) > 4)
                    {
                        item.AssetValue = item.AssetPrice - totalDepr;
                    }
                    else
                    {
                        // if asset value is eq or below yearly depreciation, set to salvage value
                        if (item.AssetValue <= deprYear)
                        {
                            item.AssetValue = salvageValue;
                        }
                        else
                        {
                            item.AssetValue = item.AssetPrice - totalDepr;
                        }
                    }
                } 
                _context.Update(item);
                await _context.SaveChangesAsync();

            }

            return RedirectToAction(nameof(Index));
        }

        public static int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }
    }
}
