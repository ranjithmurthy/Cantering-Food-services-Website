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
        IDbSet<UserFeedback> UserFeedbacks { get; set; }

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

            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<Question> Questions { get; set; }
        public IDbSet<Answer> Answers { get; set; }
        public IDbSet<Survey> Surveys { get; set; }
        public IDbSet<UserFeedback> UserFeedbacks { get; set; }
    }

    public class FakeApplicationDbContext : IApplicationDbContext
    {
        public IDbSet<Question> Questions { get; set; }
        public IDbSet<Answer> Answers { get; set; }
        public IDbSet<Survey> Surveys { get; set; }
        public IDbSet<UserFeedback> UserFeedbacks { get; set; }

        public int SaveChanges()
        {
            return 0;
        }
    }
}