using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChristmassCardGenerator.DAL;
using ChristmassCardGenerator.Models;

using Microsoft.AspNetCore.Identity;

namespace ChristmassCardGenerator.Controllers
{
    [Route("/Identity/Account/Manage/EmailLists/[Action]")]
    public class EmailListsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> UserManager;

        public EmailListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmailLists
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmailLists.Include(c => c.ApplicationUser).Where(c => c.ApplicationUser.UserName == User.Identity.Name).ToListAsync());
        }

        // GET: EmailLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailList = await _context.EmailLists
                .FirstOrDefaultAsync(m => m.ID == id);
            if (emailList == null)
            {
                return NotFound();
            }

            return View(emailList);
        }

        // GET: EmailLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmailLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Email")] EmailList emailList)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            emailList.ApplicationUser = user;
            if (ModelState.IsValid)
            {
                _context.Add(emailList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emailList);
        }

        // GET: EmailLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailList = await _context.EmailLists.FindAsync(id);
            if (emailList == null)
            {
                return NotFound();
            }
            return View(emailList);
        }

        // POST: EmailLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Email")] EmailList emailList)
        {
            if (id != emailList.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emailList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailListExists(emailList.ID))
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
            return View(emailList);
        }

        // GET: EmailLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailList = await _context.EmailLists
                .FirstOrDefaultAsync(m => m.ID == id);
            if (emailList == null)
            {
                return NotFound();
            }

            return View(emailList);
        }

        // POST: EmailLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emailList = await _context.EmailLists.FindAsync(id);
            _context.EmailLists.Remove(emailList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailListExists(int id)
        {
            return _context.EmailLists.Any(e => e.ID == id);
        }
    }
}
