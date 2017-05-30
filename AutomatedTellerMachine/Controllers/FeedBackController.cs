using AutomatedTellerMachine.Models;
using DragonClassifier;
using Glimpse.Core.Extensions;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;

namespace AutomatedTellerMachine.Controllers
{
    [Authorize]
    public class FeedBackController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
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
            var survery = showAllsurvery
                .FirstOrDefault(x => x.SurveyId == id);

            var userFeedback = new UserFeedbackViewModel
            {
                SurveyId = survery.SurveyId,
                StartDate = survery.StartDate,
                EndDate = survery.EndDate,
                Description = survery.Description,
                UserAnswerCollection = survery.Questions.ToList().ConvertAll(x => new UserAnswerViewModel
                {
                    Question = x.QuestionText,
                    UserAnswerid = x.QuestionId.ToString()
                })
            };

            ViewBag.UserFeedbackData = userFeedback;

            return View(userFeedback);
        }

        // POST: FeedBack/Select/
        [HttpPost]
        public ActionResult Select(UserFeedbackViewModel userFeedback)
        {
            var surveryObject = JsonConvert.SerializeObject(userFeedback);

            try
            {
                var userId = User.Identity.GetUserId();
                var user = db.Users.Find(userId);
                var questionDropDownlist = Request.Form.ToDictionary()
                    .Where(x => x.Key.Contains("QuestionDropDownlist"))
                    .Select(each => new
                    {
                        key = each.Key,
                        AnswerText = each.Value,
                        questionId = each.Key.Split(':').LastOrDefault()
                    });
                ;

                foreach (var question in questionDropDownlist)
                {
                    var answer = new Answer
                    {
                        SurveyId = userFeedback.SurveyId,
                        QuestionId = Convert.ToInt16(question.questionId),
                        AnswerText = question.AnswerText,
                        User = user
                    };
                    db.Answers.AddOrUpdate(answer);
                }

                string sentiment;
                if (!string.IsNullOrEmpty(userFeedback.UserFeedbackText))
                    sentiment = DragonApiClass.GetSentiment(userFeedback.UserFeedbackText);
                else
                    sentiment = "Neutral";

                var userfeeback = new UserFeedback
                {
                    UserFeedbackText = userFeedback.UserFeedbackText,
                    User = user,
                    SurveyId = userFeedback.SurveyId,
                    Sentiment = sentiment
                };

                db.UserFeedbacks.AddOrUpdate(userfeeback);
                db.SaveChanges();

                return View("ThankYou");
            }
            catch (Exception exp)
            {
                return RedirectToAction("Index");
            }
        }

        // GET: FeedBack/Thankyou
        public ActionResult Thankyou()
        {
            return View("ThankYou");
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