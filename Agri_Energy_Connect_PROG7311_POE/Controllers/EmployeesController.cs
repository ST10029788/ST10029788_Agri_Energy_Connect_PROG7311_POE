using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AgriEnergyConnectApp.Models;
using System.Security.Cryptography; // For hashing passwords
using System.Text; // For encoding passwords
using Microsoft.AspNetCore.Http; // For session management

namespace AgriEnergyConnectApp.Controllers
{
    public class EmployeesController : Controller

    {
        private readonly AgriEnergyConnectContext _context;
        AgriEnergyConnectContext db = new AgriEnergyConnectContext();

        public EmployeesController(AgriEnergyConnectContext context)
        {
            _context = context;
        }

        // GET: Employees
        // Retrieves all employees from the database and renders them on the Index view

        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        //Shows the Login form
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        //Recieves data from the login form
        [HttpPost]

        // Receives data from the login form and handles user authentication
        public ActionResult Login(Employee e)
        {
            //Code to hash Password 
            //Adapted from:
            //Author : Afzaal Ahmad Zeeshan
            //Link : https://www.c-sharpcorner.com/article/hashing-Passwords-in-net-core-with-tips/
            using (var sha256 = SHA256.Create())
            {
                // Store the current user's Employee_ID in session for tracking
                HttpContext.Session.SetString("CurrentUser", e.Employee_ID);
                //Variables declared to take in employee id and Password from login 
                string Employee_ID = HttpContext.Session.GetString("CurrentUser");
                string pass = e.Password;


                // Hash the password using SHA256
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));
                // Get the hashed string version of the password
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                // Match the employee credentials with the records in the database for authentication
                Employee user = db.Employees.Where(us => us.Employee_ID.Equals(e.Employee_ID) && us.Password.Equals(pass)).FirstOrDefault();

                if (user != null)
                {
                    // If authentication successful, redirect to the main menu page for employees
                    return RedirectToAction("Emp", "Home");
                }
                else
                {

                    // If credentials are invalid, display error message and stay on the login page
                    ViewBag.Login = "INVALID USERNAME OR Password";
                    return View();

                }
            }


        }

        // Checks if an employee exists based on their ID
        private bool EmployeeExists(string id)
        {
            return _context.Employees.Any(e => e.Employee_ID == id);
        }
    }
}
