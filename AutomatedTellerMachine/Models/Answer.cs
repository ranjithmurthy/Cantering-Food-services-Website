using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedTellerMachine.Models
{
    public class Answer
    {

        public int AnswerId { get; set; }
        public int Value { get; set; }
        public int QuestionId { get; set; }
        public int SurveyId { get; set; }

        //  public virtual Question Question { get; set; }
        //  public virtual Survey Survey { get; set; }


    }
}