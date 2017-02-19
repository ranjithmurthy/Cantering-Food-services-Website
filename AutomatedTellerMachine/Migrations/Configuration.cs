using System.Collections.Generic;
using AutomatedTellerMachine.Models;

namespace AutomatedTellerMachine.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AutomatedTellerMachine.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AutomatedTellerMachine.Models.ApplicationDbContext context)
        {
            var questions = new List<Question>
            {
            new Question{ QuestionId = 1,QuestionText= "Quailty" },
            new Question{ QuestionId =2,QuestionText= "Cleanliness"},
            new Question{ QuestionId = 3,QuestionText= "Order Accuracy"},
            new Question{ QuestionId = 4,QuestionText= "Speed of Service"},

            };
            var survey = new List<Survey>
            {
            new Survey
            {
                Questions = questions,
                Answers = new List<Answer>(),
                SurveyId = 1001, IsOpen= true ,Description = "Bday PatyFeedback",StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(5)
            }


            };
            survey.ForEach(s => context.Surverys.AddOrUpdate(s));



            var SurveryQuestions = new List<Survey>();
            questions.ForEach(s => context.Questions.AddOrUpdate(s));
            context.SaveChanges();

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
