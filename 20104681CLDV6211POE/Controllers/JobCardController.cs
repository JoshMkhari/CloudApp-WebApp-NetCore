using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

using _20104681CLDV6211POE.Models;

namespace _20104681CLDV6211POE.Controllers
{
    
    public class JobCardController : Controller
    {
        JobCardDAL jobDal = new JobCardDAL();
        JobCardDAL jobsDal = new JobCardDAL();

        
        public IActionResult Index()
        {
            List<JobCardData> jobCardList = new List<JobCardData>();
            jobCardList = jobDal.GetAllJobCards().ToList();
            return View(jobCardList);
        }

        //edit employees
        [HttpGet]
        public IActionResult Edit(string? id)
        {
            List<JobCardData> jobCardList = new List<JobCardData>();
            if (id == null)
            {
                return NotFound();
            }
            string rate = jobDal.Rate(id);
            switch(rate)
            {
                case "1":
                    jobCardList = jobDal.GetInvoiceFloorBoarding(id,rate).ToList();
                    break;
                default:
                    jobCardList = jobDal.GetSemi(id,rate).ToList();
                break;
            }
           
            while (jobCardList.Count>1)
            {
                jobCardList.RemoveAt(0);
            }
            if (jobCardList == null)
            {
                return NotFound();
            }
            return View(jobCardList);
        }
        [HttpGet]
        public IActionResult Delete(string? id)
        {
            //List<JobCardData> jobCardList = new List<JobCardData>();
            if (id == null)
            {
                return NotFound();
            }
            jobDal.CheckJobCard(id);
            jobDal.DeleteJobCard(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Employees(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<JobCardData> jobCardList = new List<JobCardData>();
            JobCardDAL.JobsID= id;
            jobCardList = jobDal.GetAllEmployeesT(id).ToList();
            return View(jobCardList);
        }
        [HttpGet]
        public IActionResult AddToJob(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<JobCardData> jobCardList = new List<JobCardData>();
            jobsDal.AddEmployeeToJob(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            //List<JobCardData> jobCardList = new List<JobCardData>();

            return View();
        }

        public IActionResult ExistingCustomer()
        {
            //List<JobCardData> jobCardList = new List<JobCardData>();

            return RedirectToAction("Index", "Customer", new { area = "" });
        }

        public IActionResult ForNewCustomer()
        {
            //List<JobCardData> jobCardList = new List<JobCardData>();

            return RedirectToAction("Create", "Customer", new { area = "" });
        }

        
    }


}
