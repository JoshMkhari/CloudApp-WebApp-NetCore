using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20104681CLDV6211POE.Models;
namespace _20104681CLDV6211POE.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeDAL employeeDal = new EmployeeDAL();
        public IActionResult Index()
        {
            //get all Employees
            List<EmployeeData> empList = new List<EmployeeData>();
            empList = employeeDal.GetAllEmployees().ToList();
            return View(empList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] EmployeeData objEmp)//once we click on create 
        {
            if (ModelState.IsValid)
            {
                employeeDal.AddEmployee(objEmp);
                return RedirectToAction("Index");
            }
            return View(objEmp);
        }

        //edit employees
        [HttpGet]
        public IActionResult Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmployeeData emp = employeeDal.GetEmployee(id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        //update employee
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(String? id, [Bind] EmployeeData objEmp)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                employeeDal.UpdateEmployees(objEmp);
                return RedirectToAction("Index");
            }
            return View(employeeDal);
        }

        //get delete view
        //perform the delete
        public IActionResult Delete(String? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            EmployeeData emp = employeeDal.GetEmployee(id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }
        
        public IActionResult AddToJob()
        {
            return RedirectToAction("Index", "JobCard", new { area = "" });
        }

    }
}
