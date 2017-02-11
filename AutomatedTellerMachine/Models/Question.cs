using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedTellerMachine.Models
{
   

    public class Question
    {
        public int Id { get; set; }
        public string SurveyQuestion { get; set; }
        public int SurveyId { get; set; }
        public ICollection<Answer> Answers { get; set; }
     //   public virtual Survey Survey { get; set; }
    }



   
}