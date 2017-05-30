using AutomatedTellerMachine.Models;
using AutomatedTellerMachine.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace AutomatedTellerMachine.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //var survey = new List<Survey>
            //{
            //new Survey
            //{
            //    Questions = questions,
            //    Answers = new List<Answer>(),
            //    SurveyId = 1001, IsOpen= true ,Description = "Bday PatyFeedback",StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(5)
            //}

            //};
            //survey.ForEach(s => context.Surverys.AddOrUpdate(s));

            //var SurveryQuestions = new List<Survey>();
            //questions.ForEach(s => context.Questions.AddOrUpdate(s));
            //context.SaveChanges();

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

            var userStore = new UserStore<ApplicationUser>(context);

            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(t => t.UserName == "admin@feedback.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@feedback.com",
                    Email = "admin@feedback.com"
                };

                userManager.Create(user, "$Password");

                // surveryService.CreateSurvery(survery);

                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole
                {
                    Name = "Admin"
                });
                context.SaveChanges();
                userManager.AddToRole(user.Id, "Admin");
            }

            if (context.Surveys.Count() < 1)
            {
                var surveryService = new SurveryManageService(context);

                var questions = new List<Question>
                {
                    new Question {QuestionText = "Pricing"},
                    new Question {QuestionText = "Quality of Service"},
                    new Question {QuestionText = "Cleanliness"},
                    new Question {QuestionText = "Order Accuracy"},
                    new Question {QuestionText = "Speed of Service"}
                };

                var survery = new Survey
                {
                    Description = "Demo",
                    EndDate = DateTime.Today.AddDays(10),
                    StartDate = DateTime.Today,
                    IsOpen = true,
                    Questions = questions
                };

                context.Surveys.AddOrUpdate(survery);

                context.SaveChanges();
            }
        }
    }
}