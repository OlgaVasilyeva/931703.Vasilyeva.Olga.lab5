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
    public class DoctorsController : Controller
    {
        private readonly HospitalContext _context;

        public DoctorsController(HospitalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Doctors.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorModel = await _context.Doctors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctorModel == null)
            {
                return NotFound();
            }

            return View(doctorModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Speciality")] DoctorModel doctorModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctorModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctorModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorModel = await _context.Doctors.FindAsync(id);
            if (doctorModel == null)
            {
                return NotFound();
            }
            return View(doctorModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Speciality")] DoctorModel doctorModel)
        {
            if (id != doctorModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctorModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorModelExists(doctorModel.Id))
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
            return View(doctorModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctorModel = await _context.Doctors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctorModel == null)
            {
                return NotFound();
            }

            return View(doctorModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctorModel = await _context.Doctors.FindAsync(id);
            _context.Doctors.Remove(doctorModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorModelExists(int id)
        {
            return _context.Doctors.Any(e => e.Id == id);
        }
    }
}
