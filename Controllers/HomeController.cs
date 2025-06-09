using System;
using System.Configuration;
using System.Web.Mvc;
using Myisolve.Models;

namespace Myisolve.Controllers
{
    public class HomeController : Controller
    {
        private readonly SqlDataAccess sqlconnection;

        public HomeController()
        {
            string connStr = ConfigurationManager.ConnectionStrings["dataconnection"].ConnectionString;
            sqlconnection = new SqlDataAccess(connStr);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginCheck(string username, string password)
        {
            if ((username == "admin" || username == "User1" || username == "User2") && password == "admin123")
            {
                Session["Username"] = username;
                return RedirectToAction("Employee");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password";
                return View("Login");
            }
        }

        [HttpGet]
        public ActionResult Employee()
        {
            var viewModel = new EmployeeViewModel
            {
                NewEmployee = new properties(),
                EmployeeList = sqlconnection.GetAllEmployees()
            };

            if (TempData["PopupMessage"] != null)
            {
                ViewBag.PopupMessage = TempData["PopupMessage"].ToString();
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Employee(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Optional: Generate employee code here if needed
                if (model.NewEmployee.EmployeeCode == "Auto")
                {
                    model.NewEmployee.EmployeeCode = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
                }

                sqlconnection.InsertEmployee(
                    model.NewEmployee.BranchName,
                    model.NewEmployee.EmployeeName,
                    model.NewEmployee.EmployeeCode,
                    model.NewEmployee.DepartmentName,
                    model.NewEmployee.DateOfJoin ?? DateTime.Now,
                    model.NewEmployee.Email,
                    model.NewEmployee.Gender,
                    model.NewEmployee.Role,
                    model.NewEmployee.MobileNumber1,
                    model.NewEmployee.Status
                );

                TempData["PopupMessage"] = "Employee added successfully!";
                return RedirectToAction("Employee");
            }

            // If model is invalid, refill list
            model.EmployeeList = sqlconnection.GetAllEmployees();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return RedirectToAction("Employee");

            var employee = sqlconnection.GetEmployeeByCode(id);
            if (employee == null)
                return RedirectToAction("Employee");

            return View("Edit", employee); // ✅ this matches your actual view file
        }


        [HttpPost]
        public ActionResult EditEmployee(properties model)
        {
            sqlconnection.UpdateEmployee(
                model.BranchName,
                model.EmployeeName,
                model.EmployeeCode,
                model.DepartmentName,
                model.DateOfJoin ?? DateTime.Now,
                model.Email,
                model.Gender,
                model.Role,
                model.MobileNumber1,
                model.Status
            );

            TempData["PopupMessage"] = "Employee updated successfully!";
            return RedirectToAction("Employee", model);
        }

        [HttpPost]
        public ActionResult DeleteEmployee(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                sqlconnection.DeleteEmployee(id);
                TempData["PopupMessage"] = "Employee deleted successfully!";
            }

            return RedirectToAction("Employee");
        }



    }
}
