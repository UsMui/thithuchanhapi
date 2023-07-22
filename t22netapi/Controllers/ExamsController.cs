using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using t22netapi.Entities;

namespace t22netapi.Controllers
{
    public class ExamsController : Controller
    {
        private readonly ThiThucHanhContext _context;

        public ExamsController(ThiThucHanhContext context)
        {
            _context = context;
        }

        // GET: Exams
        public async Task<IActionResult> Index()
        {
            var thiThucHanhContext = _context.Exams.Include(e => e.Classroom).Include(e => e.Subject).Include(e => e.Teacher);
            return View(await thiThucHanhContext.ToListAsync());
        }

        // GET: Exams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.Classroom)
                .Include(e => e.Subject)
                .Include(e => e.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // GET: Exams/Create
        public IActionResult Create()
        {
            ViewData["Classroomid"] = new SelectList(_context.Classrooms, "Id", "Id");
            ViewData["Subjectid"] = new SelectList(_context.Subjects, "Id", "Id");
            ViewData["Teacherid"] = new SelectList(_context.Teachers, "Id", "Id");
            return View();
        }

        // POST: Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Starttime,Examdate,Examduration,Classroomid,Subjectid,Teacherid,Status")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Classroomid"] = new SelectList(_context.Classrooms, "Id", "Id", exam.Classroomid);
            ViewData["Subjectid"] = new SelectList(_context.Subjects, "Id", "Id", exam.Subjectid);
            ViewData["Teacherid"] = new SelectList(_context.Teachers, "Id", "Id", exam.Teacherid);
            return View(exam);
        }

        // GET: Exams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            ViewData["Classroomid"] = new SelectList(_context.Classrooms, "Id", "Id", exam.Classroomid);
            ViewData["Subjectid"] = new SelectList(_context.Subjects, "Id", "Id", exam.Subjectid);
            ViewData["Teacherid"] = new SelectList(_context.Teachers, "Id", "Id", exam.Teacherid);
            return View(exam);
        }

        // POST: Exams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Starttime,Examdate,Examduration,Classroomid,Subjectid,Teacherid,Status")] Exam exam)
        {
            if (id != exam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamExists(exam.Id))
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
            ViewData["Classroomid"] = new SelectList(_context.Classrooms, "Id", "Id", exam.Classroomid);
            ViewData["Subjectid"] = new SelectList(_context.Subjects, "Id", "Id", exam.Subjectid);
            ViewData["Teacherid"] = new SelectList(_context.Teachers, "Id", "Id", exam.Teacherid);
            return View(exam);
        }

        // GET: Exams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.Classroom)
                .Include(e => e.Subject)
                .Include(e => e.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exams == null)
            {
                return Problem("Entity set 'ThiThucHanhContext.Exams'  is null.");
            }
            var exam = await _context.Exams.FindAsync(id);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamExists(int id)
        {
          return (_context.Exams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
