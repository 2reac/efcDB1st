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
    public class DeliveryAddressesController : Controller
    {
        private readonly ITContext _context;

        public DeliveryAddressesController(ITContext context)
        {
            _context = context;
        }

        // GET: DeliveryAddresses
        public async Task<IActionResult> Index()
        {
            var iTContext = _context.DeliveryAddress.Include(d => d.Address);
            return View(await iTContext.ToListAsync());
        }

        // GET: DeliveryAddresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryAddress = await _context.DeliveryAddress
                .Include(d => d.Address)
                .FirstOrDefaultAsync(m => m.DeliveryAddressId == id);
            if (deliveryAddress == null)
            {
                return NotFound();
            }

            return View(deliveryAddress);
        }

        // GET: DeliveryAddresses/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.GeneralAddress, "AddressId", "City");
            return View();
        }

        // POST: DeliveryAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeliveryAddressId,FirstName,LastName,Phone,Email,AddressId")] DeliveryAddress deliveryAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.GeneralAddress, "AddressId", "City", deliveryAddress.AddressId);
            return View(deliveryAddress);
        }

        // GET: DeliveryAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryAddress = await _context.DeliveryAddress.FindAsync(id);
            if (deliveryAddress == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.GeneralAddress, "AddressId", "City", deliveryAddress.AddressId);
            return View(deliveryAddress);
        }

        // POST: DeliveryAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeliveryAddressId,FirstName,LastName,Phone,Email,AddressId")] DeliveryAddress deliveryAddress)
        {
            if (id != deliveryAddress.DeliveryAddressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryAddressExists(deliveryAddress.DeliveryAddressId))
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
            ViewData["AddressId"] = new SelectList(_context.GeneralAddress, "AddressId", "City", deliveryAddress.AddressId);
            return View(deliveryAddress);
        }

        // GET: DeliveryAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryAddress = await _context.DeliveryAddress
                .Include(d => d.Address)
                .FirstOrDefaultAsync(m => m.DeliveryAddressId == id);
            if (deliveryAddress == null)
            {
                return NotFound();
            }

            return View(deliveryAddress);
        }

        // POST: DeliveryAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deliveryAddress = await _context.DeliveryAddress.FindAsync(id);
            _context.DeliveryAddress.Remove(deliveryAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryAddressExists(int id)
        {
            return _context.DeliveryAddress.Any(e => e.DeliveryAddressId == id);
        }
    }
}
