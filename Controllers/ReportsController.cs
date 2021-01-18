using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCAssets.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFCAssets.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AssetContext _context;

        public ReportsController(AssetContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DashboardValue()
        {
            var officeGroup = _context.Assets.GroupBy(a => a.Office.OfficeName)
                .Select(x => new VM_OfcLIst
                {
                    OfficeName = x.Key,
                    AssetSum = x.Sum(y => y.AssetPrice)
                }).ToList();

            ViewBag.ofcList = officeGroup;

            return View(officeGroup);
        }
        
        public IActionResult DashboardCategory()
        {
            var categoryGroup = _context.Assets.GroupBy(a => a.Category.CategoryName)
                .Select(x => new VM_CategorySummary
                {
                    CategoryName = x.Key,
                    CategorySum = x.Sum(y => y.AssetPrice)
                }).ToList();
            //ViewBag.ofcList = officeGroup;

            return View(categoryGroup);
        }

        public async Task<IActionResult> DeactivatedCurrencies()
        {
            return View(await _context.Currencies.Where(a => a.CurrencyActive == false).ToListAsync());
        }

        public IActionResult ActivateCurrency(int id)
        {
            var selectedCurrency = _context.Currencies.Where(a => a.Id == id).FirstOrDefault();
            selectedCurrency.CurrencyActive = true;
            _context.Update(selectedCurrency);
            _context.SaveChanges();
            return RedirectToAction(nameof(DeactivatedCurrencies));
        }

        public async Task<IActionResult> DeactivatedCategories()
        {
            return View(await _context.Categories.Where(a => a.CategoryActive == false).ToListAsync());
        }

        public IActionResult ActivateCategory(int id)
        {
            var selectedCategory = _context.Categories.Where(a => a.CategoryId == id).FirstOrDefault();
            selectedCategory.CategoryActive = true;
            _context.Update(selectedCategory);
            _context.SaveChanges();
            return RedirectToAction(nameof(DeactivatedCategories));
        }

        public async Task<IActionResult> DeactivatedOffices()
        {
            return View(await _context.Offices.Where(a => a.OfficeActive == false).ToListAsync());
        }

        public IActionResult ActivateOffice(int id)
        {
            var selectedOffice = _context.Offices.Where(a => a.OfficeId == id).FirstOrDefault();
            selectedOffice.OfficeActive = true;
            _context.Update(selectedOffice);
            _context.SaveChanges();
            return RedirectToAction(nameof(DeactivatedOffices));
        }

        public IActionResult ChooseOfficeForActiveAssets()
        {
            // Preprare dropdown list content
            ViewData["OfficeId"] = new SelectList(_context.Offices, nameof(Office.OfficeId), nameof(Office.OfficeName));
            return View();
        }

        public async Task<IActionResult> ExpiredAssets()
        {
            var assetContext = await _context.Assets.Include(a => a.Category).Include(b => b.Office).ThenInclude(c => c.Currency).Where(a => a.AssetActive == true).ToListAsync();
            var filteredResult = assetContext.Where(x => x.AssetExpirationDate <= DateTime.Today && x.AssetActive == true);
            var offices = _context.Offices.Include(a => a.Currency).ToList();
            //foreach (var item in assetContext)
            //{
            //var price = item.AssetPrice;
            //var office = item.OfficeId;
            //var ofcCur = _context.Offices.FirstOrDefault(b => b.CurrencyId == office);
            //var currency = _context.Currencies.FirstOrDefault(a => a.Id == ofcCur.CurrencyId);
            //var exchangeRate = Convert.ToInt32(currency.CurrensyToUSD);
            //var localPrice = item.AssetPrice * exchangeRate;
            //item.AssetPrice = localPrice;



            //}
            ViewData["sumPrice"] = filteredResult.Select(c => c.AssetPrice).Sum();

            return View(filteredResult);
        }

        public async Task<IActionResult> OfcActiveLocalCurrency(int id)
        {
            var assetContext = _context.Assets
                .Include(a => a.Category)
                .Include(b => b.Office)
                    .ThenInclude(c => c.Currency)
                .Where(a => a.AssetActive == true && a.OfficeId == id)
                .ToList();

            var filteredResult = assetContext
                .Where(x => x.OfficeId == id && x.AssetActive == true)
                .OrderBy(x=>x.Category.CategoryName);

            ViewData["office"] = _context.Offices.FirstOrDefault(x => x.OfficeId == id).OfficeName;
            ViewData["currency"] = _context.Offices
                .Include(a => a.Currency)
                .FirstOrDefault(x => x.OfficeId == id)
                .Currency.CurrencyName;

            return View(filteredResult);
        }
    }
}
