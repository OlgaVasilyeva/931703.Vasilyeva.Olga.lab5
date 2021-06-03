using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Backend5.Data;
using Backend5.Models;

namespace Backend5.Controllers
{
    public class LabsController : Controller
    {
        private readonly HospitalContext _context;

        public LabsController(HospitalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Labs.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labModel = await _context.Labs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labModel == null)
            {
                return NotFound();
            }

            return View(labModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,Phones")] LabModel labModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(labModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labModel = await _context.Labs.FindAsync(id);
            if (labModel == null)
            {
                return NotFound();
            }
            return View(labModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Phones")] LabModel labModel)
        {
            if (id != labModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabModelExists(labModel.Id))
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
            return View(labModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labModel = await _context.Labs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (labModel == null)
            {
                return NotFound();
            }

            return View(labModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var labModel = await _context.Labs.FindAsync(id);
            _context.Labs.Remove(labModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabModelExists(int id)
        {
            return _context.Labs.Any(e => e.Id == id);
        }
    }
}
