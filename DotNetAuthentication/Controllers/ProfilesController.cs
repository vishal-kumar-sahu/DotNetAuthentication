using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DotNetAuthentication.Data;
using DotNetAuthentication.Models;
using Microsoft.AspNetCore.Identity;
using DotNetAuthentication.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace DotNetAuthentication.Controllers
{
    [Authorize]
    public class ProfilesController : Controller
    {
        private readonly AuthDbContext _context;
        private readonly UserManager<Users> _userManager;

        public ProfilesController(AuthDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Profiles
        public async Task<IActionResult> Index()
        {
            string userId = _userManager.GetUserId(this.User);
            var authDbContext = _context.Profiles.Where(item => item.UserId == userId);
            return View(await authDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Profiles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfileId,Platform,Link,UserId")] Profiles profiles)
        {
            if (ModelState.IsValid)
            {
                profiles.UserId = _userManager.GetUserId(this.User);

                _context.Add(profiles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(profiles);
        }

        // GET: Profiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profiles = await _context.Profiles.FindAsync(id);
            if (profiles == null)
            {
                return NotFound();
            }
            
            return View(profiles);
        }

        // POST: Profiles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfileId,Platform,Link,UserId")] Profiles profiles)
        {
            if (id != profiles.ProfileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profiles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfilesExists(profiles.ProfileId))
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

            return View(profiles);
        }

        // GET: Profiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profiles = await _context.Profiles
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProfileId == id);
            if (profiles == null)
            {
                return NotFound();
            }

            return View(profiles);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profiles = await _context.Profiles.FindAsync(id);
            if (profiles != null)
            {
                _context.Profiles.Remove(profiles);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfilesExists(int id)
        {
            return _context.Profiles.Any(e => e.ProfileId == id);
        }
    }
}
