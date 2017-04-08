using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using AutomatedTellerMachine.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;

namespace AutomatedTellerMachine.Controllers
{
    public class RestUserFeedBackController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();




        // POST: api/RestUserFeedBack/SubmitFeedback
        [System.Web.Http.Route("api/RestUserFeedBack/SubmitFeedback")]
        [System.Web.Http.HttpPost]

        public async Task<HttpResponseMessage> SubmitFeedback([FromBody] FeedbackViewModel  fromFeedBackForm)
        {
            
          //  var surveryObject = JsonConvert.DeserializeObject<UserFeedbackViewModel>(data);
            try
            {
                var userId=db.Users.Single(x => x.Email == "admin@feedback.com").Id;
               // var userId = "b0a1f84c-b935-4b61-9bdc-7c46681cc9d5";

                ApplicationUser  user = db.Users.Find(userId);
                
                foreach (var question in fromFeedBackForm.UserAnswerCollection)
                {
                    var answer = new Answer()
                    {
                        SurveyId = fromFeedBackForm.SurveyId,
                        QuestionId = Convert.ToInt16(question.QuestionId),
                        AnswerText = question.AnswerText,
                        User = user
                    };
                    db.Answers.AddOrUpdate(answer);
                }

                string sentiment;
                if (!string.IsNullOrEmpty(fromFeedBackForm.UserFeedbackText))
                {
                 sentiment = DragonClassifier.DragonApiClass.GetSentiment(fromFeedBackForm.UserFeedbackText);
                }
                else
                {
                    sentiment = "Neutral";
                }
              
                var userfeeback = new UserFeedback
                {
                    UserFeedbackText = fromFeedBackForm.UserFeedbackText,
                    User = user,
                    SurveyId = fromFeedBackForm.SurveyId,
                    Sentiment = sentiment

                };

                db.UserFeedbacks.AddOrUpdate(userfeeback);
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
                });
            }
            return listOfSurveys;
        }

        // GET: api/RestUserFeedBack/5
        public UserFeedbackViewModel Get(int id)
        {
            var survery = db.Surveys.Find(id);

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


            return userFeedback;
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
