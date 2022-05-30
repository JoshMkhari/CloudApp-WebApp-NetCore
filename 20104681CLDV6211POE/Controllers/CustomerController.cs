using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20104681CLDV6211POE.Models;

namespace _20104681CLDV6211POE.Controllers
{
    public class CustomerController : Controller
    {
        CustomerDAL customerDals = new CustomerDAL();
        bool JCnum = false;
        String JCid;

        public IActionResult Index()
        {
            //get all Employees
            List<CustomerData> custList = new List<CustomerData>();
            custList = customerDals.GetAllCustomer().ToList();
            return View(custList);
        }

        //edit customer
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CustomerData cus = customerDals.GetCustomer(id);
            if (cus == null)
            {
                return NotFound();
            }
            return View(cus);
        }


        //update cusotmer
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(String? id, [Bind] CustomerData obCust)
        {
            if (id == null)
            {
                return NotFound();
            }

                customerDals.UpdateCustomer(obCust);
                return RedirectToAction("Index");

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] CustomerData obCust)//once we click on create 
        {
            customerDals.AddCustomer(obCust);
                return RedirectToAction("JobCard");
        }

        //get delete view
        //perform the delete
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CustomerData cus = customerDals.GetCustomer(id);
            if (cus == null)
            {
                return NotFound();
            }
            return View(cus);
        }

        //perform the delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public IActionResult DeleteEmp(String? id)
        {
            customerDals.DeleteCustomer(id);

            return RedirectToAction("Index");
        }


        public IActionResult JobCard(String? id)
        {
            if (id != null)
            {
                JCnum = true;
                JCid = id;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult JobCard([Bind] CustomerData obCust)
        {

            switch (obCust.JobType)
            {
                case "FloorBoarding":
                    CustomerDAL.RateID = 1;
                    customerDals.AddJobCard(obCust, JCid, JCnum);
                    return RedirectToAction(obCust.JobType);
                case "Semi":
                    customerDals.AddJobCard(obCust, JCid, JCnum);
                    CustomerDAL.RateID = 2;
                    return RedirectToAction("Conversion");
                case "Full":
                    customerDals.AddJobCard(obCust, JCid, JCnum);
                    CustomerDAL.RateID = 3;
                    return RedirectToAction("Conversion");
                default:
                    return RedirectToAction("JobCard","Customer");
            }
            
        }
        //JobDetailsSF
        public IActionResult Conversion()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Conversion([Bind] CustomerData obCust)
        {

            customerDals.AddMaterials(obCust, CustomerDAL.RateID); //floor boarding is 1
            customerDals.AddJobEquiptmentMaterials();
            customerDals.AddEquiptment(obCust);
            return RedirectToAction("Index", "JobCard", new { area = "" });

        }
        public IActionResult FloorBoarding()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FloorBoarding([Bind] CustomerData obCust)
        {

            customerDals.AddMaterials(obCust,CustomerDAL.RateID); //floor boarding is 1
            customerDals.AddJobEquiptmentMaterials();
            return RedirectToAction("Index","JobCard",new { area = "" });

        }


    }
}
