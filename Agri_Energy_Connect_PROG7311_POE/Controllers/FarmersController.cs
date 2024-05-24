using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgriEnergyConnectApp.Models;
using System.Security.Cryptography; // For hashing passwords
using Microsoft.AspNetCore.Http; // For session management
using System.Text; // For encoding passwords

namespace AgriEnergyConnectApp.Controllers
{
    public class FarmersController : Controller
    {
        private readonly AgriEnergyConnectContext _context;
        AgriEnergyConnectContext db = new AgriEnergyConnectContext();

        public FarmersController(AgriEnergyConnectContext context)
        {
            _context = context;
        }

        // GET: Farmers
        // Retrieves all farmers from the database and renders them on the Index view
        public async Task<IActionResult> Index()
        {
            return View(await _context.Farmers.ToListAsync());
        }
        // GET: Products
        // Retrieves products for a specific farmer based on filtering criteria and renders them on the Products view
        public async Task<IActionResult> Products(string id,DateTime toDate,DateTime fromDate,string searchString)
        {
            // Passes data to the view for filtering and display
            ViewData["Farmer_ID"] = id;
            ViewData["toDate"] = toDate;
            ViewData["fromDate"] = fromDate;
            Helper.frmID = id;

            // Filters products based on search string if provided

            if (!String.IsNullOrEmpty(searchString))
            {   // Code to create filter function adapted from:
                //Author:Rick Anderson
                //Link:https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/search?view=aspnetcore-6.0
                // Filters database for searched product type
                var types = _context.Products.Where(p => p.Farmer_ID == id && p.Product_Type!.Contains(searchString));
                return View(await types.ToListAsync());
            }

            // Filters products based on date range if provided

            else if (toDate != default(DateTime) && fromDate != default(DateTime))
            {
                
              //Filters database to show products between 2 dates
                var type = _context.Products.Where(p => p.Farmer_ID == id && p.Product_Date >= fromDate && p.Product_Date <= toDate);
                return View(await type.ToListAsync());
            }
            else {

                // Displays all products for the specified farmer
                var AgriEnergyConnectContext = _context.Products.Where(p => p.Farmer_ID == id);
            return View(await AgriEnergyConnectContext.ToListAsync());

        }
        }

        // GET: Farmers/Create
        // Shows the form for creating a new farmer profile
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Products/Create
        // Handles form submission for creating a new farmer profile

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Product_ID,Product_Name,Product_Price,Product_Quantity,Product_Type,Product_Date,Farmer_ID")] Product product)
        {
            if (ModelState.IsValid)
            // Retrieves current user's ID from session for association with the product
            {
                product.Farmer_ID = HttpContext.Session.GetString("CurrentUser");
                _context.Add(@product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["Farmer_ID"] = new SelectList(_context.Farmers, "Farmer_ID", "Farmer_ID", product.Farmer_ID);
            return View(product);
        }

         // GET: Farmers/Edit/5
        // Shows the form for editing a product
        
        public async Task<IActionResult> Edit(int? id)
        {

            ViewData["Farmer_ID"] = id;
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            // ViewData["Farmer_ID"] = new SelectList(_context.Farmers, "Farmer_ID", "Farmer_ID", product.Farmer_ID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Product_ID,Product_Name,Product_Price,Product_Quantity,Product_Type,Product_Date,Farmer_ID")] Product product,String frm)
        {
            frm = Helper.frmID;
            if (id != product.Product_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    product.Farmer_ID = frm;
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
            //ViewData["Farmer_ID"] = new SelectList(_context.Farmers, "Farmer_ID", "Farmer_ID", product.Farmer_ID);
            return View(product);
        }
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Product_ID == id);
        }

        //Shows the Login form
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        //Recieves data from the login form
        [HttpPost]
        public ActionResult Login(Farmer f)
        {
            //Code below to hash Password 
            //Learnt and adapted from :
            //Author : Afzaal Ahmad Zeeshan
            //Link : https://www.c-sharpcorner.com/article/hashing-Passwords-in-net-core-with-tips/
            using (var sha256 = SHA256.Create())
            {

                HttpContext.Session.SetString("CurrentUser", f.Farmer_ID);
                //Variables declared to take in farmer ID and Password from login 
                string Farmer_ID = HttpContext.Session.GetString("CurrentUser");
                string pass = f.Password;


                // Hashes the password using SHA256
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));
                // Get hashed 
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                // Matches farmer credentials with records in the database for authentication
                Farmer user = db.Farmers.Where(us => us.Farmer_ID.Equals(f.Farmer_ID) && us.Password.Equals(hash)).FirstOrDefault();

                if (user != null)
                {
                    //The user can now enter and will be taken to the main menu page
                    return RedirectToAction("New", "Home");
                }
                else
                {

                    // Displays error message for invalid credentials
                    ViewBag.Login = "INVALID USERNAME OR Password";
                    return View();

                }
            }


        }


        //Shows the form
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //recieves data
        [HttpPost]
        public ActionResult Register(Farmer f)
        {
            HttpContext.Session.SetString("CurrentUser", f.Farmer_ID);
            string Farmer_ID = HttpContext.Session.GetString("CurrentUser");
            string pass = f.Password;
            string fname = f.FarmerName;
            using (var sha256 = SHA256.Create())
            {
                // Hashes the password using SHA256
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));
                // Get the hashed string version of the Password
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                // Creates a new farmer object to be stored in the database
                Farmer newF = new Farmer();
                newF.Farmer_ID = Farmer_ID;
                newF.Password = hash;
                newF.FarmerName = fname;
                // Checks if the user already exists in the database
                var UserAlreadyExists = db.Farmers.Any(x => x.Farmer_ID == newF.Farmer_ID);
                if (UserAlreadyExists)
                {

                    ViewBag.Fail = "USER WITH THIS ID NUMBER ALREADY EXISTS";
                }
                else
                {
                    // Adds the new farmer to the database
                    db.Farmers.Add(newF);
                    // Saves changes to the database using asynchronous method
                    db.SaveChangesAsync();

                    return RedirectToAction("Emp", "Home");
                }
            }
            return View();
        }

        // GET: Farmers1/Edit/5
        public async Task<IActionResult> Reset(string id)
        {
            id = HttpContext.Session.GetString("CurrentUser");
            if (id == null)
            {
                return NotFound();
            }

            var farmer = await _context.Farmers.FindAsync(id);
            if (farmer == null)
            {
                return NotFound();
            }
            return View(farmer);
        }

        // POST: Farmers1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reset(string id, [Bind("Farmer_ID,FarmerName,Password")] Farmer farmer)
        {      id = HttpContext.Session.GetString("CurrentUser");
            if (id != farmer.Farmer_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var sha256 = SHA256.Create())
                    {

                        HttpContext.Session.SetString("CurrentUser", farmer.Farmer_ID);
                        //Variables 
                        string Farmer_ID = HttpContext.Session.GetString("CurrentUser");
                        string pass = farmer.Password;


                        // Hash
                        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));
                        var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                        farmer.Password = hash;
                        _context.Update(farmer);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmerExists(farmer.Farmer_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("New", "Home"); 
            }
            return View(farmer);
        }
        private bool FarmerExists(string id)
        {
            return _context.Farmers.Any(e => e.Farmer_ID == id);
        }
    }
}
