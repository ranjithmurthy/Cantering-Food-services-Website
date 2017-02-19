using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutomatedTellerMachine.Models
{
    public class Survey
    {
        public Survey()
        {
            Questions = new HashSet<Question>();
            Answers = new HashSet<Answer>();
        }

      // [Key]
        public int SurveyId { get; set; }
        public string Description  { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsOpen { get; set; }
        
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }


    public class Question
    {
        public Question()
        {
            Surverys = new HashSet<Survey>();
            Answers = new HashSet<Answer>();
        }


      //  [Key]
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }

        public virtual  ICollection<Survey> Surverys { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

    }

    public class OfferedAnswer
    {
        [Key]
        public int OfferedAnswerId { get; set; }
        public string AnswerText { get; set; }
        
    }

    //public class SurveryQuestionAnswer
    //{

    //    public  Question SurveryQuestions { get; set; }
    //    public  Survey Surverys { get; set; }
    //    public virtual ICollection<OfferedAnswer> OfferedAnswers { get; set; }

    //}

    //public class SurveryQuestion
    //{
    //  //  [Key]
    //  //  public int Id { get; set; }

    //    public virtual ICollection<Survey> Surveys { get; set; }

    //    public virtual ICollection<Question> Questions { get; set; }

    //}

    //public class SurveryQuestionAnswer
    //{
    //     public int SurveryQuestionAnswerId { get; set; }
    //    public virtual ICollection<SurveryQuestion> SurveryQuestions { get; set; }


    //    public virtual ICollection<OfferedAnswer> OfferedAnswers { get; set; }

    //    //[Key, ForeignKey("SurveyId")]
    //    //public int SurveyId { get; set; }
    //    //public virtual Survey Survey { get; set; }

    //    //[Key, ForeignKey("QuestionId")]
    //    //public int QuestionId { get; set; }
    //    //public virtual Question Question { get; set; }

    //    //[Key, ForeignKey("OfferedAnswerId")]
    //    //public int OfferedAnswerId { get; set; }

    //    //public virtual OfferedAnswer OfferedAnswer { get; set; }

    //}



    public class Answer
    {
      
 
         public int AnswerId { get; set; }


        public virtual ApplicationUser User { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }



        public string OtherText { get; set; }


        public virtual Survey Survey { get; set; }
        public virtual Question Question { get; set; }

        public virtual OfferedAnswer OfferedAnswer { get; set; }





    }
}