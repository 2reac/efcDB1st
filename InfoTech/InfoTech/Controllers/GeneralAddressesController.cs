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
    public class GeneralAddressesController : Controller
    {
        private readonly ITContext _context;

        public GeneralAddressesController(ITContext context)
        {
            _context = context;
        }

        // GET: GeneralAddresses
        public async Task<IActionResult> Index()
        {
            return View(await _context.GeneralAddress.ToListAsync());
        }

        // GET: GeneralAddresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generalAddress = await _context.GeneralAddress
                .FirstOrDefaultAsync(m => m.AddressId == id);
            if (generalAddress == null)
            {
                return NotFound();
            }

            return View(generalAddress);
        }

        // GET: GeneralAddresses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AddressId,Country,Region,City,Street,Number,ZipCode")] GeneralAddress generalAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(generalAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(generalAddress);
        }

        // GET: GeneralAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generalAddress = await _context.GeneralAddress.FindAsync(id);
            if (generalAddress == null)
            {
                return NotFound();
            }
            return View(generalAddress);
        }

        // POST: GeneralAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AddressId,Country,Region,City,Street,Number,ZipCode")] GeneralAddress generalAddress)
        {
            if (id != generalAddress.AddressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(generalAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneralAddressExists(generalAddress.AddressId))
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
            return View(generalAddress);
        }

        // GET: GeneralAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generalAddress = await _context.GeneralAddress
                .FirstOrDefaultAsync(m => m.AddressId == id);
            if (generalAddress == null)
            {
                return NotFound();
            }

            return View(generalAddress);
        }

        // POST: GeneralAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var generalAddress = await _context.GeneralAddress.FindAsync(id);
            _context.GeneralAddress.Remove(generalAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeneralAddressExists(int id)
        {
            return _context.GeneralAddress.Any(e => e.AddressId == id);
        }
    }
}
