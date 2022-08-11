using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project1.Web.Data;
using Project1.Web.Models;

namespace Project1.Web.Areas.Demos.Controllers
{
    [Area("Demos")]
    public class FoodmenusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodmenusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Demos/Foodmenus
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Foodmenu.Include(f => f.FoodCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Demos/Foodmenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodmenu = await _context.Foodmenu
                .Include(f => f.FoodCategory)
                .FirstOrDefaultAsync(m => m.FoodId == id);
            if (foodmenu == null)
            {
                return NotFound();
            }

            return View(foodmenu);
        }

        // GET: Demos/Foodmenus/Create
        public IActionResult Create()
        {
            ViewData["FoodcategoryId"] = new SelectList(_context.FoodCategory, "FoodcategoryId", "FoodcategoryName");
            return View();
        }

        // POST: Demos/Foodmenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodId,FoodName,FoodcategoryId,Quantity,Confirmed")] Foodmenu foodmenu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foodmenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FoodcategoryId"] = new SelectList(_context.FoodCategory, "FoodcategoryId", "FoodcategoryName", foodmenu.FoodcategoryId);
            return View(foodmenu);
        }

        // GET: Demos/Foodmenus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodmenu = await _context.Foodmenu.FindAsync(id);
            if (foodmenu == null)
            {
                return NotFound();
            }
            ViewData["FoodcategoryId"] = new SelectList(_context.FoodCategory, "FoodcategoryId", "FoodcategoryName", foodmenu.FoodcategoryId);
            return View(foodmenu);
        }

        // POST: Demos/Foodmenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodId,FoodName,FoodcategoryId,Quantity,Confirmed")] Foodmenu foodmenu)
        {
            if (id != foodmenu.FoodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodmenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodmenuExists(foodmenu.FoodId))
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
            ViewData["FoodcategoryId"] = new SelectList(_context.FoodCategory, "FoodcategoryId", "FoodcategoryName", foodmenu.FoodcategoryId);
            return View(foodmenu);
        }

        // GET: Demos/Foodmenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodmenu = await _context.Foodmenu
                .Include(f => f.FoodCategory)
                .FirstOrDefaultAsync(m => m.FoodId == id);
            if (foodmenu == null)
            {
                return NotFound();
            }

            return View(foodmenu);
        }

        // POST: Demos/Foodmenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodmenu = await _context.Foodmenu.FindAsync(id);
            _context.Foodmenu.Remove(foodmenu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodmenuExists(int id)
        {
            return _context.Foodmenu.Any(e => e.FoodId == id);
        }
    }
}
