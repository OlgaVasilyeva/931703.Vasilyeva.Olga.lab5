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
    public class HospitalsController : Controller
    {
        private readonly HospitalContext _context;

        public HospitalsController(HospitalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Hospitals.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospitalModel = await _context.Hospitals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hospitalModel == null)
            {
                return NotFound();
            }

            return View(hospitalModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,Phones")] HospitalModel hospitalModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hospitalModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hospitalModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospitalModel = await _context.Hospitals.FindAsync(id);
            if (hospitalModel == null)
            {
                return NotFound();
            }
            return View(hospitalModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Phones")] HospitalModel hospitalModel)
        {
            if (id != hospitalModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hospitalModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HospitalModelExists(hospitalModel.Id))
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
            return View(hospitalModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospitalModel = await _context.Hospitals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hospitalModel == null)
            {
                return NotFound();
            }

            return View(hospitalModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hospitalModel = await _context.Hospitals.FindAsync(id);
            _context.Hospitals.Remove(hospitalModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HospitalModelExists(int id)
        {
            return _context.Hospitals.Any(e => e.Id == id);
        }
    }
}
