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
    public class PatientsController : Controller
    {
        private readonly HospitalContext _context;

        public PatientsController(HospitalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Patients.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientModel = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patientModel == null)
            {
                return NotFound();
            }

            return View(patientModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Disease")] PatientModel patientModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patientModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patientModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientModel = await _context.Patients.FindAsync(id);
            if (patientModel == null)
            {
                return NotFound();
            }
            return View(patientModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Disease")] PatientModel patientModel)
        {
            if (id != patientModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientModelExists(patientModel.Id))
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
            return View(patientModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientModel = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patientModel == null)
            {
                return NotFound();
            }

            return View(patientModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patientModel = await _context.Patients.FindAsync(id);
            _context.Patients.Remove(patientModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientModelExists(int id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
    }
}
