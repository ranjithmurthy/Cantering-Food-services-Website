using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AutomatedTellerMachine.Models
{
    public class UserFeedbackViewModel
    {
        [Key] public int UserFeedbackId;

        public UserFeedbackViewModel()
        {
            UserAnswerCollection = new List<UserAnswerViewModel>();
        }

        public int SurveyId { get; set; }
        public string Description { get; set; }

        [StringLength(120, ErrorMessage = "Comment cannot be longer than 120 characters.")]
        public string UserFeedbackText { get; set; }

        public bool IsOpen { get; set; }
        public List<UserAnswerViewModel> UserAnswerCollection { get; set; }

        [DataType(DataType.Date)]
        [DefaultValue("DateTime.MinValue")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DefaultValue("DateTime.MinValue")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
    }

    //formated
    public class FeedbackViewModel
    {
        [Key] public int UserFeedbackId;

        public FeedbackViewModel()
        {
            UserAnswerCollection = new List<UserAnswerViewModel>();
        }

        public int SurveyId { get; set; }
        public string Description { get; set; }

        [StringLength(120, ErrorMessage = "Comment cannot be longer than 120 characters.")]
        public string UserFeedbackText { get; set; }

        public bool IsOpen { get; set; }
        public List<UserAnswerViewModel> UserAnswerCollection { get; set; }
    }

    public class UserAnswerViewModel
    {
        [Key]
        public string UserAnswerid { get; set; }

        public string AnswerText { get; set; }
        public string QuestionId { get; set; }
        public string Question { get; set; }
    }

    public class QAModel
    {
        public string UserAnswerid { get; set; }
        public string Question { get; set; }
        public string QuestionId { get; set; }
        public string AnswerText { get; set; }
        //          key = each.Key,
        //      {
        //      .Select(each => new
        //      .Where(x => x.Key.Contains("QuestionDropDownlist"))

        //var QuestionDropDownlist = Request.Form.ToDictionary()
        //          AnswerText = each.Value,
        //          questionId = each.Key.Split(':').LastOrDefault()
        //      }); ;
    }
}