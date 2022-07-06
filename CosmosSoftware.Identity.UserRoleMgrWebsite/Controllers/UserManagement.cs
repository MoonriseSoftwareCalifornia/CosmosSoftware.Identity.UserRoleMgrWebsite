using CosmosSoftware.Identity.UserRoleMgrWebsite.Data;
using CosmosSoftware.Identity.UserRoleMgrWebsite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CosmosSoftware.Identity.UserRoleMgrWebsite.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UsersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserManagement
        public async Task<IActionResult> Index()
        {
              return _userManager.Users != null ? 
                          View((await _userManager.Users.ToListAsync()).Select(s => new IdentityUserViewItem(s))) :
                          Problem("Entity set 'ApplicationDbContext.IdentityUserViewItem'  is null.");
        }

        // GET: UserManagement/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUser= await _userManager.FindByIdAsync(id);

            if (identityUser == null)
            {
                return NotFound();
            }

            return View(new IdentityUserViewItem(identityUser));
        }

        // GET: UserManagement/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,LockoutEnd,TwoFactorEnabled,PhoneNumberConfirmed,PhoneNumber,EmailConfirmed,UserName,LockoutEnabled,AccessFailedCount,Password,ConfirmPassword")] IdentityUserCreateViewItem model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser();
                user.Email = model.Email;
                user.LockoutEnd = model.LockoutEnd;
                user.PhoneNumber = model.PhoneNumber;
                user.PhoneNumberConfirmed = model.PhoneNumberConfirmed;
                user.UserName = model.UserName;
                user.EmailConfirmed = model.EmailConfirmed;
                
                var result = await _userManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        // GET: UserManagement/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.IdentityUserViewItem == null)
            {
                return NotFound();
            }

            var identityUserViewItem = await _context.IdentityUserViewItem.FindAsync(id);
            if (identityUserViewItem == null)
            {
                return NotFound();
            }
            return View(identityUserViewItem);
        }

        // POST: UserManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Email,LockoutEnd,TwoFactorEnabled,PhoneNumberConfirmed,PhoneNumber,EmailConfirmed,UserName,LockoutEnabled,AccessFailedCount")] IdentityUserViewItem identityUserViewItem)
        {
            if (id != identityUserViewItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(identityUserViewItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdentityUserViewItemExists(identityUserViewItem.Id))
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
            return View(identityUserViewItem);
        }

        // GET: UserManagement/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.IdentityUserViewItem == null)
            {
                return NotFound();
            }

            var identityUserViewItem = await _context.IdentityUserViewItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (identityUserViewItem == null)
            {
                return NotFound();
            }

            return View(identityUserViewItem);
        }

        // POST: UserManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.IdentityUserViewItem == null)
            {
                return Problem("Entity set 'ApplicationDbContext.IdentityUserViewItem'  is null.");
            }
            var identityUserViewItem = await _context.IdentityUserViewItem.FindAsync(id);
            if (identityUserViewItem != null)
            {
                _context.IdentityUserViewItem.Remove(identityUserViewItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IdentityUserViewItemExists(string id)
        {
          return (_context.IdentityUserViewItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
