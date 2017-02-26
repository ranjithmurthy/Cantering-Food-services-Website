using AutomatedTellerMachine.Models;
using System.Data.Entity.Migrations;

namespace AutomatedTellerMachine.Services
{
    public class SurveryManageService
    {
        private IApplicationDbContext db;

        public SurveryManageService(IApplicationDbContext dbContext)
        {
            db = dbContext;
        }

        public void CreateSurvery(Survey survery)
        {
            db.Surveys.AddOrUpdate(survery);
            db.SaveChanges();
        }

        public void UpdateBalance(int checkingAccountId)
        {
            // var checkingAccount = db.CheckingAccounts.Where(c => c.Id == checkingAccountId).First();
            // checkingAccount.Balance = db.Transactions.Where(c => c.CheckingAccountId == checkingAccountId).Sum(t => t.Amount);
            db.SaveChanges();
        }
    }
}