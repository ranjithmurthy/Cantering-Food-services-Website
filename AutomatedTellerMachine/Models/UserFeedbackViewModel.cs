using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Glimpse.AspNet.Tab;

namespace AutomatedTellerMachine.Models
{

  
    public class UserFeedbackViewModel
    {

        [Key]
        public int UserFeedbackId;
        public int SurveyId { get; set; }
        public string Description { get; set; }

        [StringLength(120, ErrorMessage = "Comment cannot be longer than 120 characters.")]
        public string UserFeedbackText { get; set; }

        public Boolean IsOpen { get; set; }
        public List<UserAnswerViewModel> UserAnswerCollection { get; set; }

        [DataType(DataType.Date)]
        [DefaultValue("DateTime.MinValue")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DefaultValue("DateTime.MinValue")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }


        public  UserFeedbackViewModel()
        {
            UserAnswerCollection = new List<UserAnswerViewModel>();
        }

    }


    //formated 
    public class FeedbackViewModel
    {

        [Key]
        public int UserFeedbackId;
        public int SurveyId { get; set; }
        public string Description { get; set; }

        [StringLength(120, ErrorMessage = "Comment cannot be longer than 120 characters.")]
        public string UserFeedbackText { get; set; }

        public Boolean IsOpen { get; set; }
        public List<UserAnswerViewModel> UserAnswerCollection { get; set; }

     
        
        public FeedbackViewModel()
        {
            UserAnswerCollection = new List<UserAnswerViewModel>();
        }

    }



    public class UserAnswerViewModel
    {
        [Key]
        public string UserAnswerid { get; set; }
        public string AnswerText { get; set; }
        public string QuestionId { get; set; }
        public string Question { get; set; }

        public  UserAnswerViewModel()
        {
            
        }
    }

    public class QAModel
    {
        
        public string UserAnswerid { get; set; }
        public string Question { get; set; }
        public string QuestionId { get; set; }
        public string AnswerText { get; set; }


        public QAModel()
        {

        }

        //var QuestionDropDownlist = Request.Form.ToDictionary()
        //      .Where(x => x.Key.Contains("QuestionDropDownlist"))
        //      .Select(each => new
        //      {
        //          key = each.Key,
        //          AnswerText = each.Value,
        //          questionId = each.Key.Split(':').LastOrDefault()
        //      }); ;
    }
}