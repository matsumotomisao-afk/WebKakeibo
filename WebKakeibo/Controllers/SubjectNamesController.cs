using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebKakeibo.Data;
using WebKakeibo.Models;

namespace WebKakeibo.Controllers
{
    public class SubjectNamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubjectNamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SubjectNames
        public async Task<IActionResult> Index()
        {
            return View(await _context.SubjectName.ToListAsync());
        }

        // GET: SubjectNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectName = await _context.SubjectName
                .FirstOrDefaultAsync(m => m.SubjectNameId == id);
            if (subjectName == null)
            {
                return NotFound();
            }

            return View(subjectName);
        }

        // GET: SubjectNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubjectNames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectNameId,Name,ImageUrl")] SubjectName subjectName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subjectName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subjectName);
        }

        // GET: SubjectNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectName = await _context.SubjectName.FindAsync(id);
            if (subjectName == null)
            {
                return NotFound();
            }
            return View(subjectName);
        }

        // POST: SubjectNames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubjectNameId,Name,ImageUrl")] SubjectName subjectName)
        {
            if (id != subjectName.SubjectNameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subjectName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectNameExists(subjectName.SubjectNameId))
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
            return View(subjectName);
        }

        // GET: SubjectNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectName = await _context.SubjectName
                .FirstOrDefaultAsync(m => m.SubjectNameId == id);
            if (subjectName == null)
            {
                return NotFound();
            }

            return View(subjectName);
        }

        // POST: SubjectNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subjectName = await _context.SubjectName.FindAsync(id);
            if (subjectName != null)
            {
                _context.SubjectName.Remove(subjectName);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectNameExists(int id)
        {
            return _context.SubjectName.Any(e => e.SubjectNameId == id);
        }
    }
}
