using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomatedTellerMachine.Models;
using Microsoft.AspNet.Identity;

namespace AutomatedTellerMachine.Controllers
{
    public class SurveryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Survery

        public ActionResult Index()
        {

             var userId = User.Identity.GetUserId();
            var studentList = new List<SurveryQuestion>{
                            new SurveryQuestion() { ApplicationUserId =userId , SurveyQuestion = "Qverall Service Quailty"  } ,
                            new SurveryQuestion() { ApplicationUserId =userId , SurveyQuestion = "Cleanlines" } ,
                            new SurveryQuestion() { ApplicationUserId =userId , SurveyQuestion = "Order Accuracy" } ,
                            new SurveryQuestion() { ApplicationUserId =userId , SurveyQuestion = "Speed of Service" } ,
                            new SurveryQuestion() { ApplicationUserId =userId , SurveyQuestion = "Value" } ,

                        };

            return View(studentList);
           
        }

        
        // GET: Survery/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Survery/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Survery/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Survery/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Survery/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Survery/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Survery/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
