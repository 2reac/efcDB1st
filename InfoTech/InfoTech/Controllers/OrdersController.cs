using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InfoTech.Models;

namespace InfoTech.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ITContext _context;

        public OrdersController(ITContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var iTContext = _context.Order.Include(o => o.Customer).Include(o => o.DeliveryAddress).Include(o => o.DiscountCodeNavigation).Include(o => o.Payment).Include(o => o.Store);
            return View(await iTContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.DeliveryAddress)
                .Include(o => o.DiscountCodeNavigation)
                .Include(o => o.Payment)
                .Include(o => o.Store)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Email");
            ViewData["DeliveryAddressId"] = new SelectList(_context.DeliveryAddress, "DeliveryAddressId", "Email");
            ViewData["DiscountCode"] = new SelectList(_context.Discount, "DiscountCode", "DiscountCode");
            ViewData["PaymentId"] = new SelectList(_context.Payment, "PaymentId", "PaymentId");
            ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "City");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CustomerId,OrderDate,DeliveryAddressId,StoreId,DiscountCode,PaymentId,OrderStatus")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Email", order.CustomerId);
            ViewData["DeliveryAddressId"] = new SelectList(_context.DeliveryAddress, "DeliveryAddressId", "Email", order.DeliveryAddressId);
            ViewData["DiscountCode"] = new SelectList(_context.Discount, "DiscountCode", "DiscountCode", order.DiscountCode);
            ViewData["PaymentId"] = new SelectList(_context.Payment, "PaymentId", "PaymentId", order.PaymentId);
            ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "City", order.StoreId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Email", order.CustomerId);
            ViewData["DeliveryAddressId"] = new SelectList(_context.DeliveryAddress, "DeliveryAddressId", "Email", order.DeliveryAddressId);
            ViewData["DiscountCode"] = new SelectList(_context.Discount, "DiscountCode", "DiscountCode", order.DiscountCode);
            ViewData["PaymentId"] = new SelectList(_context.Payment, "PaymentId", "PaymentId", order.PaymentId);
            ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "City", order.StoreId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CustomerId,OrderDate,DeliveryAddressId,StoreId,DiscountCode,PaymentId,OrderStatus")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Email", order.CustomerId);
            ViewData["DeliveryAddressId"] = new SelectList(_context.DeliveryAddress, "DeliveryAddressId", "Email", order.DeliveryAddressId);
            ViewData["DiscountCode"] = new SelectList(_context.Discount, "DiscountCode", "DiscountCode", order.DiscountCode);
            ViewData["PaymentId"] = new SelectList(_context.Payment, "PaymentId", "PaymentId", order.PaymentId);
            ViewData["StoreId"] = new SelectList(_context.Store, "StoreId", "City", order.StoreId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .Include(o => o.DeliveryAddress)
                .Include(o => o.DiscountCodeNavigation)
                .Include(o => o.Payment)
                .Include(o => o.Store)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.OrderId == id);
        }
    }
}
