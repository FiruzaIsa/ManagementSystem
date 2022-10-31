using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManagementSystem.Data;
using ManagementSystem.Models;

namespace ManagementSystem.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly BaseDbContext _context;

        public EnrollmentController(BaseDbContext context)
        {
            _context = context;
        }

        // GET: Enrollment
        public async Task<IActionResult> Index()
        {
            var baseDbContext = _context.Enrollments.Include(e => e.Course).Include(e => e.Student);
            return View(await baseDbContext.ToListAsync());
        }

        // GET: Enrollment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.EnrollmentID == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollment/Create
        public IActionResult Create()
        {
            ViewData["Course"] = new SelectList(_context.Courses, "CourseID", "Title");
            ViewData["Student"] = new SelectList(_context.Students, "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentID,CourseID,StudentID,Grade")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Course"] = new SelectList(_context.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewData["Student"] = new SelectList(_context.Students, "Id", "FullName", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: Enrollment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["Course"] = new SelectList(_context.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewData["Student"] = new SelectList(_context.Students, "Id", "FullName", enrollment.StudentID);
            return View(enrollment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrollmentID,CourseID,StudentID,Grade")] Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.EnrollmentID))
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
            ViewData["Course"] = new SelectList(_context.Courses, "CourseID", "Title", enrollment.CourseID);
            ViewData["Student"] = new SelectList(_context.Students, "Id", "FullName", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: Enrollment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.EnrollmentID == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Enrollments == null)
            {
                return Problem("Error");
            }
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
          return _context.Enrollments.Any(e => e.EnrollmentID == id);
        }
    }
}
