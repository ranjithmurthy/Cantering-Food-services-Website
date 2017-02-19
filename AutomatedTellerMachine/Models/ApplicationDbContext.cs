using AutomatedTellerMachine.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AutomatedTellerMachine.Models
{
    public interface IApplicationDbContext
    {
      //  IDbSet<CheckingAccount> CheckingAccounts { get; set; }
      //  IDbSet<Transaction> Transactions { get; set; }

       //  IDbSet<OldSurveryQuestion> OldSurveryQuestions { get; set; }

         IDbSet<Survey> Surverys { get; set; }

         IDbSet<Question> Questions { get; set; }
         IDbSet<OfferedAnswer> OfferedAnswers { get; set; }
      //  IDbSet<SurveryQuestionAnswer> SurveryQuestionAnswer { get; set; }
        



         IDbSet<Answer> Answers { get; set; }

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

        public IDbSet<Survey> Surverys { get; set; }

        public IDbSet<Question> Questions { get; set; }
        public IDbSet<OfferedAnswer> OfferedAnswers { get; set; }

        //  public IDbSet<SurveryQuestionAnswer> SurveryQuestionAnswer { get; set; }
     //   public System.Data.Entity.DbSet<AutomatedTellerMachine.Models.ApplicationUser> ApplicationUsers { get; set; }

        public IDbSet<Answer> Answers { get; set; }

     
    }

    public class FakeApplicationDbContext : IApplicationDbContext
    {
        //  public IDbSet<CheckingAccount> CheckingAccounts { get; set; }
        //  public IDbSet<Transaction> Transactions { get; set; }

        public IDbSet<Survey> Surverys { get; set; }

        public IDbSet<Question> Questions { get; set; }
       public IDbSet<OfferedAnswer> OfferedAnswers { get; set; }
       // public IDbSet<SurveryQuestionAnswer> SurveryQuestionAnswer { get; set; }

         public IDbSet<Answer> Answers { get; set; }
        public int SaveChanges()
        {
            return 0;
        }
    }
}