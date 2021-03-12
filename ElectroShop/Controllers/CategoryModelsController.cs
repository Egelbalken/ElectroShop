using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectroShop.Data;
using ElectroShop.Models;
using Microsoft.AspNetCore.Authorization;

namespace ElectroShop.Controllers
{
    public class CategoryModelsController : Controller
    {
        private readonly ApplicationDbContext _ApplicationDbcontext;

        /// <summary>
        /// Constructor injection oj 
        /// </summary>
        /// <param name="context"></param>
        public CategoryModelsController(ApplicationDbContext Dbcontext)
        {
            _ApplicationDbcontext = Dbcontext;
        }

        /// <summary>
        /// GET: CategoryModels
        /// Show the Parentcategory registered in the database as a list.
        /// </summary>
        /// <returns>Returns a list of categorys to the view from the database.</returns>
        public async Task<IActionResult> Index()
        {
            var applicationCategoryDbContext = _ApplicationDbcontext.Categories.Include(c => c.ParentCategory);
            return View(await applicationCategoryDbContext.ToListAsync());
        }

        // GET: CategoryModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = await _ApplicationDbcontext.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            return View(categoryModel);
        }

        // GET: CategoryModels/Create
        public IActionResult Create()
        {
            ViewData["ParentCategoryName"] = new SelectList(
                _ApplicationDbcontext.Categories, 
                nameof(CategoryModel.CategoryId),
                nameof(CategoryModel.Name)
            );
            return View();
        }

        // POST: CategoryModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Name,Description,ParentCategoryId,ImageURL")] CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                _ApplicationDbcontext.Add(categoryModel);
                await _ApplicationDbcontext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentCategoryId"] = new SelectList(_ApplicationDbcontext.Categories, "CategoryId", "CategoryId", categoryModel.ParentCategoryId);
            return View(categoryModel);
        }

        // GET: CategoryModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = await _ApplicationDbcontext.Categories.FindAsync(id);
            if (categoryModel == null)
            {
                return NotFound();
            }
            ViewData["ParentCategoryId"] = new SelectList(_ApplicationDbcontext.Categories, "CategoryId", "CategoryId", categoryModel.ParentCategoryId);
            return View(categoryModel);
        }

        // POST: CategoryModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name,Description,ParentCategoryId,ImageURL")] CategoryModel categoryModel)
        {
            if (id != categoryModel.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _ApplicationDbcontext.Update(categoryModel);
                    await _ApplicationDbcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryModelExists(categoryModel.CategoryId))
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
            ViewData["ParentCategoryId"] = new SelectList(_ApplicationDbcontext.Categories, "CategoryId", "CategoryId", categoryModel.ParentCategoryId);
            return View(categoryModel);
        }

        // GET: CategoryModels/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = await _ApplicationDbcontext.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            return View(categoryModel);
        }

        // POST: CategoryModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoryModel = await _ApplicationDbcontext.Categories.FindAsync(id);
            _ApplicationDbcontext.Categories.Remove(categoryModel);
            await _ApplicationDbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryModelExists(int id)
        {
            return _ApplicationDbcontext.Categories.Any(e => e.CategoryId == id);
        }
    }
}
