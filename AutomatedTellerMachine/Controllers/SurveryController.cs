using AutomatedTellerMachine.Models;
using AutomatedTellerMachine.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AutomatedTellerMachine.Controllers
{
    public class SurveryController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Survery
        public ActionResult Index()
        {
            return View();
        }

        // GET: Survery/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [Authorize(Roles = "Admin, Worker")]
        // GET: Survery/Create
        public ActionResult Create()
        {
            var defaultSurveyData = new SurveryViewModel();

            defaultSurveyData.Survery = new Survey();

            var questionsforPage =
                db.Questions.ToList().ConvertAll(x => new QuestionModel(x.QuestionId, x.QuestionText, false));

            ViewBag.DatabaseListofQuestions = questionsforPage;

            return View(defaultSurveyData.Survery);
        }

        // POST: Survery/Create
        [HttpPost]
        public ActionResult Create(Survey surveyItem)
        {
            try
            {
                var quesitonsSelected = new List<int>();
                var ch = Request.Form.GetValues("questionList");

                foreach (var id in ch)
                {
                    int result;
                    if (int.TryParse(id, out result))
                        quesitonsSelected.Add(result);
                }

                var questionsUserSelected =
                    db.Questions.ToList().Where(x => quesitonsSelected.Any(t => t == x.QuestionId));

                surveyItem.Questions = questionsUserSelected.ToList();

                if (ModelState.IsValid)
                {
                    db.Surveys.Add(surveyItem);

                    db.SaveChanges();
                }

                return RedirectToAction("Thankyou", "FeedBack");
            }
            catch (Exception e)
            {
                return View(surveyItem);
            }
        }

        // POST: Survery/CreateQuestion
        [HttpPost]
        public ActionResult CreateQuestion(Question newQuestion)
        {
            var data = newQuestion.QuestionText;
            db.Questions.Add(newQuestion);
            db.SaveChanges();

            return RedirectToAction("Create");
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