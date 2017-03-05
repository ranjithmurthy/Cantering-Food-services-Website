using AutomatedTellerMachine.Models;
using AutomatedTellerMachine.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AutomatedTellerMachine.Controllers
{
    public class SurveryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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

        // GET: Survery/Create
        public ActionResult Create()
        {
            var defaultSurveyData = new SurveryViewModel();

            defaultSurveyData.Survery = new Survey();

            List<QuestionModel> questionsforPage = db.Questions.ToList().ConvertAll(x => new QuestionModel(x.QuestionId, x.QuestionText, false));

            this.ViewBag.DatabaseListofQuestions = questionsforPage;
            //db.Questions.ToList()

            //  defaultSurveyData.Survery.Description = "Default";

            return View(defaultSurveyData.Survery);
        }

        // POST: Survery/Create
        [HttpPost]
        public ActionResult Create(Survey surveyItem)
        {
            List<int> quesitonsSelected = new List<int>();
            var ch = Request.Form.GetValues("questionList");

            foreach (var id in ch)
            {
                int result;
                if (int.TryParse(id, out result))
                {
                    quesitonsSelected.Add(result);
                }
               
            }
            // return View();

            var questionsUserSelected = db.Questions.ToList().Where(x => quesitonsSelected.Any(t => t == x.QuestionId));

            surveyItem.Questions = questionsUserSelected.ToList();

            if (ModelState.IsValid)
            {
                db.Surveys.Add(surveyItem);

                db.SaveChanges();

                return RedirectToAction("Create");
            }
            else
            {
                return View(surveyItem);
            }
        }

        //// GET: Survery/CreateQuestion
        //public ActionResult CreateQuestion()
        //{
        //    Question q = new Question();
        //    return View(q);
        //}

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