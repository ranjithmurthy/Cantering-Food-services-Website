using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutomatedTellerMachine.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Migrations;

namespace AutomatedTellerMachine.Controllers
{
    public class RestUserFeedBackController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();


        [HttpPost]
        public HttpResponseMessage SubmitUserFeedback(UserFeedbackViewModel userFeedback)
        {
            //if (!ModelState.IsValid)
            //    return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                var userId = User.Identity.GetUserId();
                var user = db.Users.Find(userId);

                ////var QuestionDropDownlist = Request.Form.ToDictionary()
                ////    .Where(x => x.Key.Contains("QuestionDropDownlist"))
                ////    .Select(each => new
                ////    {
                ////        key = each.Key,
                ////        AnswerText = each.Value,
                ////        questionId = each.Key.Split(':').LastOrDefault()
                ////    }); ;

                //foreach (var question in QuestionDropDownlist)
                //{
                //    var answer = new Answer()
                //    {
                //        SurveyId = userFeedback.SurveyId,
                //        QuestionId = Convert.ToInt16(question.questionId),
                //        AnswerText = question.AnswerText,
                //        User = user
                //    };
                //    db.Answers.AddOrUpdate(answer);
                //}

                var userfeebacktext = new UserFeedback
                {
                    UserFeedbackText = userFeedback.UserFeedbackText,
                    User = user,
                    SurveyId = userFeedback.SurveyId
                };

                db.UserFeedbacks.AddOrUpdate(userfeebacktext);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }



        // GET: api/RestUserFeedBack
        public IEnumerable<Survey> Get()
        {
            var showAllsurvery = db.Surveys.ToList();

            List<Survey> listOfSurveys = new List<Survey>();
            foreach (var SurveyVar in showAllsurvery)
            {
                listOfSurveys.Add(new Survey()
                {
                    SurveyId = SurveyVar.SurveyId,
                    Description = SurveyVar.Description,
                    EndDate = SurveyVar.EndDate,
                    StartDate = SurveyVar.StartDate
                }

                    );
            }
            return listOfSurveys;
        }

        // GET: api/RestUserFeedBack/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/RestUserFeedBack
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/RestUserFeedBack/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RestUserFeedBack/5
        public void Delete(int id)
        {
        }
    }
}
