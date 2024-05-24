using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgriEnergyConnectApp.Models;
using Microsoft.AspNetCore.Http;

namespace AgriEnergyConnectApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AgriEnergyConnectContext _context;
       
        public ProductsController(AgriEnergyConnectContext context)
        {
            _context = context; // Initializing the database context
        }

        // GET: Products        
        // Action method to display products based on various filters

        public async Task<IActionResult> Index(String FarmerLogin, String searchString, DateTime toDate, DateTime fromDate)
        {
            FarmerLogin = HttpContext.Session.GetString("CurrentUser");
            // Setting view data for date filters

            ViewData["toDate"] = toDate;
            ViewData["fromDate"] = fromDate;


            // Handling different search criteria to filter products

            if (!String.IsNullOrEmpty(searchString))
                {
                // Code to create filter function adapted from:
                //Author:Rick Anderson
                //Link:https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/search?view=aspnetcore-6.0
                // Filters database for searched product type
                var types = _context.Products.Where(x => x.Farmer_ID == FarmerLogin && x.Product_Type!.Contains(searchString));
                    return View(await types.ToListAsync());
                }
                else if (toDate != default(DateTime) && fromDate != default(DateTime))
                {
                    //Filters database for products between date range
                    var type = _context.Products.Where(x => x.Farmer_ID == FarmerLogin && x.Product_Date >= fromDate && x.Product_Date <= toDate);
                    return View(await type.ToListAsync());
                }
                else
                {
                   //Displays list of user specific products
                    var AgriEnergyConnectContext = _context.Products.Where(x => x.Farmer_ID == FarmerLogin);
                    return View(await AgriEnergyConnectContext.ToListAsync());
                }

            }


        // GET: Products/Details/5
        // Action method to display details of a specific product
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Farmer)
                .FirstOrDefaultAsync(m => m.Product_ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        // Action method to create a new product

        public IActionResult Create()
        {
           // ViewData["Farmer_ID"] = new SelectList(_context.Farmers, "Farmer_ID", "Farmer_ID");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Product_ID,Product_Name,Product_Price,Product_Quantity,Product_Type,Product_Date,Farmer_ID")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Farmer_ID = HttpContext.Session.GetString("CurrentUser");
                _context.Add(@product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["Farmer_ID"] = new SelectList(_context.Farmers, "Farmer_ID", "Farmer_ID", product.Farmer_ID);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id,String FarmerLogin)
        {
           
            FarmerLogin = HttpContext.Session.GetString("CurrentUser");
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            product.Farmer_ID = FarmerLogin;
           // ViewData["Farmer_ID"] = new SelectList(_context.Farmers, "Farmer_ID", "Farmer_ID", product.Farmer_ID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Product_ID,Product_Name,Product_Price,Product_Quantity,Product_Type,Product_Date,Farmer_ID")] Product product)
        {
           String FarmerLogin = HttpContext.Session.GetString("CurrentUser");
            product.Farmer_ID = FarmerLogin;
            if (id != product.Product_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Product_ID))
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
           // ViewData["Farmer_ID"] = new SelectList(_context.Farmers, "Farmer_ID", "Farmer_ID", product.Farmer_ID);
            return View(product);
        }

        // GET: Products/Delete/5
        // Retrieving the product to be deleted

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Farmer)
                .FirstOrDefaultAsync(m => m.Product_ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // Action method to delete a product
        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        // Confirming and deleting the specified product

        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // Method to check if a product exists
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Product_ID == id);
        }

      
    }
    
}
