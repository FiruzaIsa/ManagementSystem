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
using ManagementSystem.Dtos;

namespace ManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        // GET: Course
        public IActionResult Index()
        {
              return View(_courseRepository.GetAll());
        }

        // GET: Course/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _courseRepository == null)
            {
                return NotFound();
            }

            var course = _courseRepository
                .Get(m => m.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Course/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CourseID,Title,Credits")] Course course)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _courseRepository.Add(course);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    if (CourseExists(course.CourseID))
                    {
                        ModelState.AddModelError("CourseID", "Ders numarasi mevcut.");
                        return View();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            return View(course);
        }

        // GET: Course/Edit/5
        public IActionResult  Edit(int? id)
        {
            if (id == null || _courseRepository == null)
            {
                return NotFound();
            }

            var course =_courseRepository.Get(c=>c.CourseID==id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CourseID,Title,Credits")] Course course)
        {
            if (id != course.CourseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   _courseRepository.Update(course);
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                        return NotFound();
                  
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Course/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _courseRepository == null)
            {
                return NotFound();
            }

            var course = _courseRepository.Get(c => c.CourseID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_courseRepository == null)
            {
                return Problem("Error");
            }
            var course = _courseRepository.Get(c=>c.CourseID==id);
            if (course != null)
            {
               _courseRepository.Delete(course);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _courseRepository.CheckIfExists(e => e.CourseID == id);
        }
    }
}
