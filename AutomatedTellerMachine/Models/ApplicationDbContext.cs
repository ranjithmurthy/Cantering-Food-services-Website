using AutomatedTellerMachine.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace AutomatedTellerMachine.Models
{
    public interface IApplicationDbContext
    {
        IDbSet<Question> Questions { get; set; }
        IDbSet<Answer> Answers { get; set; }
        IDbSet<Survey> Surveys { get; set; }

        int SaveChanges();
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());

            ////// Configure StudentId as PK for StudentAddress
            ////modelBuilder.Entity<Survey>()
            ////    .HasKey(e => e.SurveyId);

            ////modelBuilder.Entity<Question>()
            ////.HasKey(e => e.QuestionId);

            //// Configure StudentId as FK for StudentAddress
            //modelBuilder.Entity<Answer>()
            //    .HasRequired(s => s.Question);

            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<Question> Questions { get; set; }
        public IDbSet<Answer> Answers { get; set; }
        public IDbSet<Survey> Surveys { get; set; }
    }

    public class FakeApplicationDbContext : IApplicationDbContext
    {
        public IDbSet<Question> Questions { get; set; }
        public IDbSet<Answer> Answers { get; set; }
        public IDbSet<Survey> Surveys { get; set; }

        public int SaveChanges()
        {
            return 0;
        }
    }
}