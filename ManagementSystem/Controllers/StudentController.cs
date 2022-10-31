using ManagementSystem.Data;
using ManagementSystem.Models;
using ManagementSystem.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;

namespace ManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        IStudentRepository _studentRepository;
        BaseDbContext _baseDbContext;   
        public StudentController(IStudentRepository studentRepository,BaseDbContext dbContext)
        {
             _studentRepository=studentRepository;  
            _baseDbContext=dbContext;   
        }
        // GET: StudentController
        public ActionResult Index()
        {
            return View(_studentRepository.GetAll());
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var students = _studentRepository.GetAll();

            var student = _baseDbContext.Students
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .AsNoTracking()
                .FirstOrDefault(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            try
            {
                _studentRepository.Add(student);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _studentRepository.Get(s=>s.Id==id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Student student)
        {
                try
                {
                    _studentRepository.Update(student);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Duzenlenmedi yeniden deneyin");
                }
            
            return View(student);
        }


        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var student = _studentRepository.Get(s=>s.Id==id);
            try
            {

                _studentRepository.Delete(student);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
