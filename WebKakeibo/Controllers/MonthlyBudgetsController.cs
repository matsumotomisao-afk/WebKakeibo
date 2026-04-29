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
    public class MonthlyBudgetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MonthlyBudgetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MonthlyBudgets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MonthlyBudget.Include(m => m.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MonthlyBudgets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monthlyBudget = await _context.MonthlyBudget
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MonthlyBudgetId == id);
            if (monthlyBudget == null)
            {
                return NotFound();
            }

            return View(monthlyBudget);
        }

        // GET: MonthlyBudgets/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: MonthlyBudgets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MonthlyBudgetId,YearNum,MonthNum,BudgetAmount,UserId")] MonthlyBudget monthlyBudget)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monthlyBudget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", monthlyBudget.UserId);
            return View(monthlyBudget);
        }

        // GET: MonthlyBudgets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monthlyBudget = await _context.MonthlyBudget.FindAsync(id);
            if (monthlyBudget == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", monthlyBudget.UserId);
            return View(monthlyBudget);
        }

        // POST: MonthlyBudgets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MonthlyBudgetId,YearNum,MonthNum,BudgetAmount,UserId")] MonthlyBudget monthlyBudget)
        {
            if (id != monthlyBudget.MonthlyBudgetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monthlyBudget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonthlyBudgetExists(monthlyBudget.MonthlyBudgetId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", monthlyBudget.UserId);
            return View(monthlyBudget);
        }

        // GET: MonthlyBudgets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monthlyBudget = await _context.MonthlyBudget
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MonthlyBudgetId == id);
            if (monthlyBudget == null)
            {
                return NotFound();
            }

            return View(monthlyBudget);
        }

        // POST: MonthlyBudgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monthlyBudget = await _context.MonthlyBudget.FindAsync(id);
            if (monthlyBudget != null)
            {
                _context.MonthlyBudget.Remove(monthlyBudget);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonthlyBudgetExists(int id)
        {
            return _context.MonthlyBudget.Any(e => e.MonthlyBudgetId == id);
        }
    }
}
