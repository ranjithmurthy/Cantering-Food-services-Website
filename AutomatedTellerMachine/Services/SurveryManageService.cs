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


        //public void CreateCheckingAccount(string firstName, string lastName, string userId, decimal initialBalance)
        //{
            
        //    var checkingAccount = new CheckingAccount { FirstName = firstName, LastName = lastName, AccountNumber = accountNumber, Balance = initialBalance, ApplicationUserId = userId };
        //    db..Add(checkingAccount);
        //    db.SaveChanges();
        //}


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