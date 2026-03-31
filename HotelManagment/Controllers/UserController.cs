using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HotelManagment.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotelManagment.Controllers
{
    [Authorize(Roles = "Admin")] 
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string searchString, int pageSize = 10, int pageNumber = 1)
        {
            var query = _userManager.Users.AsQueryable();

            // 1. Filter by Name or Email
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(u => u.FirstName.Contains(searchString) ||
                                         u.LastName.Contains(searchString) ||
                                         u.MiddleName.Contains(searchString)||
                                         u.UserName.Contains(searchString));
            }

            // 2. Pagination Logic
            int totalUsers = await query.CountAsync();
            var users = await query
                .OrderBy(u => u.LastName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            /*
            var model = new UserIndexViewModel
            {
                Users = users,
                SearchString = searchString,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalUsers / (double)pageSize)
            };

            return View(model);
            */
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUser model, string password)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    EGN = model.EGN,
                    PhoneNumber = model.PhoneNumber,
                    AppointmentDate = DateTime.Now,
                    IsActive = true
                };

                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    // By default, new users are "Employees"
                    await _userManager.AddToRoleAsync(user, "Employee");
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ApplicationUser model)
        {
            if (id != model.Id) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            // Update allowed fields
            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.EGN = model.EGN;
            user.Email = model.Email;
            user.UserName = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.IsActive = model.IsActive;

            // If the employee is being fired/deactivated
            if (!model.IsActive && user.DismissalDate == null)
            {
                user.DismissalDate = DateTime.Now;
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.IsActive = false;
                user.DismissalDate = DateTime.Now;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}