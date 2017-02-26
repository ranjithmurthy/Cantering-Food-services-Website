using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutomatedTellerMachine.Models
{
    public class Survey
    {
        public Survey()
        {
            this.Questions = new HashSet<Question>();
            StartDate = DateTime.Today;
            EndDate = DateTime.Today.AddDays(9);
        }

        public int SurveyId { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DefaultValue("DateTime.MinValue")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate
        {
            get;

            set;
        }

        [DefaultValue("DateTime.MinValue")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public Boolean IsOpen { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }

    public class Question
    {
        public Question()
        {
            this.Surveys = new HashSet<Survey>();
        }

        [Key]
        public int QuestionId { get; set; }

        public string QuestionText { get; set; }

        //[ForeignKey("Survey")]
        //public int SurveyId { get; set; }

        public virtual ICollection<Survey> Surveys { get; set; }
    }

    public class Answer
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