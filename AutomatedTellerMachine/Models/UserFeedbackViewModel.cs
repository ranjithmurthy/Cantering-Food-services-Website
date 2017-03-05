using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomatedTellerMachine.Models
{
    public class UserFeedbackViewModel
    {

        [Key]
        public int UserFeedbackId;
        public int SurveyId { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DefaultValue("DateTime.MinValue")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DefaultValue("DateTime.MinValue")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public Boolean IsOpen { get; set; }
        public  IList<UserAnswerViewModel> UserAnswerCollection{ get; set; }

    }


    public class UserAnswerViewModel
    {
        [Key]
        public string UserAnswerid { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        //public UserAnswerViewModel( string _question, string _answer)
        //{
        //    UserAnswerid = _question;
        //    Answer = _answer;
        //}
    }
}