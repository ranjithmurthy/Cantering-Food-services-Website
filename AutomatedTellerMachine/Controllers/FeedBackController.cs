using System;
using AutomatedTellerMachine.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using Glimpse.Core.Extensions;

namespace AutomatedTellerMachine.Controllers
{
    public class FeedBackController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private List<Survey> showAllsurvery = new List<Survey>();

        // GET: FeedBack
        public ActionResult Index()
        {
            showAllsurvery = db.Surveys.ToList();

            return View(showAllsurvery);
        }

        // GET: FeedBack/Details/5
        public ActionResult Details(int id)
        {
            var singleSurvey = db.Surveys.Find(id);

            return View(singleSurvey);
        }

        // GET: FeedBack/Select/5
        public ActionResult Select(int id)
        {
         
            showAllsurvery = db.Surveys.ToList();
            var survery = showAllsurvery.Where(x => x.SurveyId == id).FirstOrDefault();



            var userFeedback = new UserFeedbackViewModel()
            {
                SurveyId = survery.SurveyId,
                StartDate = survery.StartDate,
                EndDate = survery.EndDate,
                Description = survery.Description,

                UserAnswerCollection = survery.Questions.ToList().ConvertAll(x => new UserAnswerViewModel()
                {
                    Question = x.QuestionText,
                    UserAnswerid = x.QuestionId.ToString()
                })

            };

           




            return View(userFeedback);
        }

        // POST: FeedBack/Select/
        [HttpPost]
        public ActionResult Select(UserFeedbackViewModel userFeedback)
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            var QuestionDropDownlist2 = Request.Form.ToDictionary();

            var QuestionDropDownlist = Request.Form.ToDictionary()
                .Where(x => x.Key.Contains("QuestionDropDownlist"))
                .Select(each => new
              {
                  key= each.Key,
                  AnswerText = each.Value,
                  questionId = each.Key.Split(':').LastOrDefault()
              }); ;




            foreach (var question in QuestionDropDownlist)
            {
                var answer = new Answer()
                {
                    SurveyId =userFeedback.SurveyId ,
                    QuestionId = Convert.ToInt16(question.questionId),
                    AnswerText = question.AnswerText,
                    User = user
                };
                db.Answers.AddOrUpdate(answer);
                db.SaveChanges();
            }


          

            return RedirectToAction("Index");
        }

        // GET: FeedBack/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FeedBack/Create
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

        // GET: FeedBack/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FeedBack/Edit/5
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

        // GET: FeedBack/Delete/5
        public ActionResult Delete(int id)
        {

            return View();
        }

        // POST: FeedBack/Delete/5
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