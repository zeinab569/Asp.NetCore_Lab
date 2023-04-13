using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ITIApp.Models;

namespace ITIApp.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly TheITIContext _context = new TheITIContext();

        

        // GET: Departments
        public async Task<IActionResult> Index()
        {
              return _context.Departments != null ? 
                          View(await _context.Departments.ToListAsync()) :
                          Problem("Entity set 'TheITIContext.Departments'  is null.");
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentId,DepartmentName,DepartmentDescription,Capacity")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentId,DepartmentName,DepartmentDescription,Capacity")] Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DepartmentId))
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
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Departments == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.DepartmentId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Departments == null)
            {
                return Problem("Entity set 'TheITIContext.Departments'  is null.");
            }
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult updateCourses(int id)
        {
            var allcoueses=_context.Courses.ToList();
            var dept= _context.Departments.Include(a => a.Courses).FirstOrDefault(a => a.DepartmentId == id) ;
            var courseindept = dept.Courses;
            var coursenotindept = (allcoueses.Except(courseindept.ToList()));

            ViewBag.courseindept = new SelectList(courseindept, "CrsId", "Crs_Name");
            ViewBag.coursenotindept = new SelectList(coursenotindept, "CrsId", "Crs_Name");


            return View();
        }

        [HttpPost]
        public IActionResult updateCourses(int id,int[] CrtoAdd, int[] CrtoRemove)
        {
           // var allcoueses = _context.Courses.ToList();
            var dept = _context.Departments.Include(a => a.Courses).FirstOrDefault(a => a.DepartmentId == id);
            foreach (var c in CrtoRemove)
            {
                var cor = _context.Courses.FirstOrDefault(a=>a.CrsId== c);
                dept.Courses.Remove(cor);
            }

            foreach (var c in CrtoAdd)
            {
                var cor = _context.Courses.FirstOrDefault(a => a.CrsId == c);
                dept.Courses.Add(cor);
            }
            _context.SaveChanges();

            return RedirectToAction("updateCourses", new { id = id });
        }

        public IActionResult ShowAllCoueses(int id)
        {
            return View(_context.Departments.Include(a=>a.Courses).FirstOrDefault(a=>a.DepartmentId==id));
        }
        public IActionResult updateStudentDegree(int deptid,int crsid)
        {
            //var stds = _context.Students.Where(a=>a.DeptNo== deptid).ToList();
            var deps = _context.Departments.Include(a=>a.Students).FirstOrDefault(a=>a.DepartmentId== deptid);
            var crs=_context.Courses.FirstOrDefault(a=>a.CrsId==crsid);
            ViewBag.deps=deps;
            ViewBag.crs=crs;
            ViewBag.students = deps.Students;
            return View();
        }
        [HttpPost]
        public IActionResult updateStudentDegree(int deptid, int crsid,Dictionary<int,int> std)
        {
           
            foreach(var i in std)
            {
                var std_degree = _context.StudentCourses.FirstOrDefault(a => a.StdId == i.Key && a.CrsId==crsid);
                if (std_degree == null)
                {
                    _context.StudentCourses.Add(new StudentCourse()
                    {
                        CrsId = crsid,
                        StdId = i.Key,
                        Degree = i.Value

                    });
                }
                else
                {
                    std_degree.Degree = i.Value;
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        private bool DepartmentExists(int id)
        {
          return (_context.Departments?.Any(e => e.DepartmentId == id)).GetValueOrDefault();
        }


    }
}
