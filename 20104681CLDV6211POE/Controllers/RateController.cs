using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20104681CLDV6211POE.Models;

namespace _20104681CLDV6211POE.Controllers
{
    public class RateController : Controller
    {
        RateDAL rateDall = new RateDAL();
        public IActionResult Index()
        {
            //get all Employees
            List<RateData> rateList = new List<RateData>();
            rateList = rateDall.GetRates().ToList();
            return View(rateList);
        }

        //edit employees
        [HttpGet]
        public IActionResult Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            RateData ra = rateDall.GetRate(id);
            if (ra == null)
            {
                return NotFound();
            }
            return View(ra);
        }

        //update employee
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(String? id, [Bind] RateData objEmp)
        {
            if (id == null)
            {
                return NotFound();
            }
                rateDall.UpdateRate(objEmp,id);
                return RedirectToAction("Index");

        }


    }
}
