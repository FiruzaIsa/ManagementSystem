using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManagementSystem.Data;
using ManagementSystem.Models;
using ManagementSystem.Repositories;
using ManagementSystem.ViewModels;

namespace ManagementSystem.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly BaseDbContext _context;

        public InstructorController(IInstructorRepository instructorRepository,BaseDbContext context)
        {
            _instructorRepository = instructorRepository;
            _context = context; 
        }

        // GET: Instructor
        public IActionResult Index(int? id, int? courseID)
        {
            InstructorViewModel instructor = new InstructorViewModel();
            instructor.Instructors=_context.Instructors.Include(i => i.Courses)
          .OrderBy(i => i.LastName)
          .ToList();

            if (id != null)
            {
                ViewData["InstructorID"] = id.Value;
                Instructor instruct = instructor.Instructors.Where(
                    i => i.Id == id.Value).Single();
                instructor.Courses = instruct.Courses.Select(s => s);
            }

            if (courseID != null)
            {
                ViewData["CourseID"] = courseID.Value;
                var selectedCourse = instructor.Courses.Where(x => x.CourseID == courseID).Single();
                 _context.Entry(selectedCourse).Collection(x => x.Enrollments).Load();
                foreach (Enrollment enrollment in selectedCourse.Enrollments)
                {
                    _context.Entry(enrollment).Reference(x => x.Student).Load();
                }
                instructor.Enrollments = selectedCourse.Enrollments;
            }
            return View(instructor);
        }

        // GET: Instructor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _instructorRepository == null)
            {
                return NotFound();
            }

            var instructor = _instructorRepository.Get(m => m.Id == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // GET: Instructor/Create
        public IActionResult Create()
        {
           // Instructor instructor = new Instructor();
           // instructor.Courses = new List<Course>();

           //List< Course> allCourses = _context.Courses.ToList(); ;
           // var instructorCourses = new HashSet<int>(instructor.Courses.Select(c => c.CourseID));
           // var viewModel = new List<Course>();

           // ViewData["Courses"] = new SelectList(_context.Courses, "CourseID", "Title"); 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,LastName,FirstName,StarDate")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                _instructorRepository.Add(instructor);
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        // GET: Instructor/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _instructorRepository == null)
            {
                return NotFound();
            }

            var instructor = _instructorRepository.Get(i=>i.Id==id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, /*[Bind("Id,LastName,FirstName,StarDate")]*/ Instructor instructor)
        {
            if (id != instructor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _instructorRepository.Update(instructor);
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!InstructorExists(instructor.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(instructor);
        }

        // GET: Instructor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _instructorRepository == null)
            {
                return NotFound();
            }

            var instructor =  _instructorRepository.Get(m => m.Id == id);
            if (instructor == null)
            {
                return NotFound();
            }

            return View(instructor);
        }

        // POST: Instructor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteConfirmed(int id)
        {
            if (_instructorRepository == null)
            {
                return Problem("Entity set 'BaseDbContext.Instructors'  is null.");
            }
            var instructor = _instructorRepository.Get(i=>i.Id==id);
            if (instructor != null)
            {
                _instructorRepository.Delete(instructor);
            }
            
            return RedirectToAction(nameof(Index));
        }

        //private bool InstructorExists(int id)
        //{
        //  return _context.Instructors.Any(e => e.Id == id);
        //}
 
    }
}
