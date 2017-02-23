using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutomatedTellerMachine.Models
{

    public  class Survey
    {
        public Survey() { }


        public int SurveyId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Boolean IsOpen { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
    public  class Question
    {
        public Question() { }


        [Key]
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }


        //[ForeignKey("Survey")]
        //public int SurveyId { get; set; }
        
        public virtual Survey Survey { get; set; }

      
    }

    

    public  class Answer
    {

        [Key]
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        [ForeignKey("Survey")]
         public int SurveyId { get; set; }
        public virtual Survey Survey { get; set; }


         [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }


}