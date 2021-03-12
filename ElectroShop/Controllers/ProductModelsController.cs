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

    public class ProductModelsController : Controller
    {
        // Instance of the database
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Constructor Injection to the class of the database context
        /// </summary>
        /// <param name="context">The context of database</param>
        public ProductModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        ///////////////////////////////////////////////////////////////////
        ////////////////////////  Skaffolded /////////////////////////////
        /////////////////////////////////////////////////////////////////

        /// <summary>
        /// GET: ProductModels
        /// Standrad 
        /// </summary>
        /// <returns>Returns a list of products</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        /// <summary>
        /// GET: ProductModels/Details/5
        /// Shows a view of the datail of a product
        /// </summary>
        /// <param name="id">Product Id of a item</param>
        /// <returns>Returns a choosen product to look at.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        /// <summary>
        /// GET: ProductModels/Create
        /// Fetching all the products under category from database.
        /// </summary>
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        { 
            ViewData["Categories"] = new SelectList(
               _context.Categories.OrderBy(category => category.Name),
               nameof(CategoryModel.CategoryId),
               nameof(CategoryModel.Name)
            );

            return View();
        }

        // POST: ProductModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// POST: ProductModels/Create
        /// Creates a new product and adds it to the context and then we readirects to the indexsite.
        /// </summary>
        /// <param name="productModel">The productModel which is going to be added</param>
        /// <returns>Returns the new added productmodel</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Description,Price,CategoryId,ImageURL")] ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(productModel);
        }

        /// <summary>
        /// GET: ProductModels/Edit/5
        /// Edit Method to edit a specific item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the productModel to edit</returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products.FindAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }

            ViewData["Categories"] = new SelectList(
               _context.Categories.OrderBy(category => category.Name),
               nameof(CategoryModel.CategoryId),
               nameof(CategoryModel.Name)
            );

            return View(productModel);
        }

        // POST: ProductModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Edit the product model and binds the id to "ProductId,Name,Description,Price,CategoryId,ImageURL" in database.
        /// Saves the changes to the productModel and returns it.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productModel"></param>
        /// <returns>Returns the edited productModel</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Description,Price,CategoryId,ImageURL")] ProductModel productModel)
        {
            if (id != productModel.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductModelExists(productModel.ProductId))
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
            return View(productModel);
        }

        /// <summary>
        /// GET: ProductModels/Delete/5
        /// Gets and shows the product you want to delete.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a productModel thats is about to get deleted</returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        /// <summary>
        /// POST: ProductModels/Delete/5
        /// Takes in tje Id of te product that is choosen to be deleted.
        /// Deletes it from the database and the returns to the index site of productModel
        /// </summary>
        /// <param name="id">the Id of the product that is choosen to be deleted</param>
        /// <returns>Back to index site</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productModel = await _context.Products.FindAsync(id);
            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Cheks if the product exits in the database before going to action whit the request.
        /// </summary>
        /// <param name="id">id of the product choosen</param>
        /// <returns>returns the product id if it is in the database</returns>
        private bool ProductModelExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
