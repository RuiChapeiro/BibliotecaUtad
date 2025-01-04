using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliotecaUtad.Data;
using BibliotecaUtad.Models;

namespace BibliotecaUtad.Controllers
{
    public class SubgendersController : Controller
    {
        private readonly BibliotecaUtadContext _context;

        public SubgendersController(BibliotecaUtadContext context)
        {
            _context = context;
        }

        // GET: Subgenders
        public async Task<IActionResult> Index()
        {
            var bibliotecaUtadContext = _context.Subgender.Include(s => s.Gender);
            return View(await bibliotecaUtadContext.ToListAsync());
        }

        // GET: Subgenders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subgender = await _context.Subgender
                .Include(s => s.Gender)
                .FirstOrDefaultAsync(m => m.SubGenderId == id);
            if (subgender == null)
            {
                return NotFound();
            }

            return View(subgender);
        }

        // GET: Subgenders/Create
        public IActionResult Create()
        {
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "GenderName");
            return View();
        }

        // POST: Subgenders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubGenderId,SubGenderName,GenderId")] Subgender subgender)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subgender);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "GenderName", subgender.GenderId);
            return View(subgender);
        }

        // GET: Subgenders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subgender = await _context.Subgender.FindAsync(id);
            if (subgender == null)
            {
                return NotFound();
            }
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "GenderName", subgender.GenderId);
            return View(subgender);
        }

        // POST: Subgenders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubGenderId,SubGenderName,GenderId")] Subgender subgender)
        {
            if (id != subgender.SubGenderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subgender);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubgenderExists(subgender.SubGenderId))
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
            ViewData["GenderId"] = new SelectList(_context.Gender, "GenderId", "GenderName", subgender.GenderId);
            return View(subgender);
        }

        // GET: Subgenders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subgender = await _context.Subgender
                .Include(s => s.Gender)
                .FirstOrDefaultAsync(m => m.SubGenderId == id);
            if (subgender == null)
            {
                return NotFound();
            }

            return View(subgender);
        }

        // POST: Subgenders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subgender = await _context.Subgender.FindAsync(id);
            if (subgender != null)
            {
                _context.Subgender.Remove(subgender);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubgenderExists(int id)
        {
            return _context.Subgender.Any(e => e.SubGenderId == id);
        }
    }
}
