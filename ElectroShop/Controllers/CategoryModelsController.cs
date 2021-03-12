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

        /// <summary>
        /// GET: CategoryModels/Details/5
        /// </summary>
        /// <param name="id">Category ID of customers chooise.</param>
        /// <returns>Returns a detail model of the category</returns>
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

        /// <summary>
        /// GET: CategoryModels/Create
        /// Creates a standard view of categorys registerd in the database.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// The Admin is allowed to create a new category repository
        /// Added functionality to add some image url and parent category.
        /// </summary>
        /// <param name="categoryModel">A new category model</param>
        /// <returns>A new added cattegory view model to the site</returns>
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

        /// <summary>
        ///  GET: CategoryModels/Edit/5
        ///  Shows a standard view of editor action.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a model of things to Edit. Only allowed by Admin.</returns>
        [Authorize(Roles = "Admin")]
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
        /// <summary>
        /// Gives the Admin Authorized action to edit a surten category of chooise.
        /// </summary>
        /// <param name="id">Binds the id to CategoryId,Name,Description,ParentCategoryId,ImageURL</param>
        /// <param name="categoryModel">the changed category model view</param>
        /// <returns>Returns The Edited CategoryViewModel</returns>
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
