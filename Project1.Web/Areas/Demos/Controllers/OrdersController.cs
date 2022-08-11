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
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Demos/Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(o => o.Customers).Include(o => o.Foodmenu);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Demos/Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Customers)
                .Include(o => o.Foodmenu)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Demos/Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerID", "CustomerName","MobileNUmber");
            ViewData["FoodId"] = new SelectList(_context.Foodmenu, "FoodId", "FoodName");
            return View();
        }

        // POST: Demos/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("OrderId,OrderName,CustomerId,CustomerName,FoodId")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                // Sanitize the data before consumption
                orders.OrderName = orders.OrderName.Trim();

                // Check for Duplicate CategoryName
                bool isDuplicateFound
                    = _context.Orders.Any(c => c.OrderName == orders.OrderName);
                if (isDuplicateFound)
                {
                    ModelState.AddModelError("orderName", "Duplicate! Another category with same name exists");
                }
                else
                {
                    // Save the changes
                    _context.Add(orders);
                    await _context.SaveChangesAsync();              // update the database
                    return RedirectToAction(nameof(Index));
                }
            }

            // Something must have gone wrong, so return the View with the model error(s).
            return View(orders);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("OrderId,OrderName,CustomerId,FoodId,CustomerName")] Orders orders)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(orders);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerID", "CustomerName", orders.CustomerId);
        //    ViewData["FoodId"] = new SelectList(_context.Foodmenu, "FoodId", "FoodName", orders.FoodId);
        //    return View(orders);
        //}

        //// GET: Demos/Orders/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var orders = await _context.Orders.FindAsync(id);
        //    if (orders == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerID", "CustomerName", orders.CustomerId);
        //    ViewData["FoodId"] = new SelectList(_context.Foodmenu, "FoodId", "FoodName", orders.FoodId);
        //    return View(orders);
        //}

        // POST: Demos/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderName,CustomerId,FoodId,CustomerName")] Orders orders)
        {
            if (id != orders.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrderId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerID", "CustomerName", orders.CustomerId);
            ViewData["FoodId"] = new SelectList(_context.Foodmenu, "FoodId", "FoodName", orders.FoodId);
            return View(orders);
        }

        // GET: Demos/Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Customers)
                .Include(o => o.Foodmenu)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Demos/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
