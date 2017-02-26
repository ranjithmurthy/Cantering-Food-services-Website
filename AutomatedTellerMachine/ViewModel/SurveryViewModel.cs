using AutomatedTellerMachine.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AutomatedTellerMachine.ViewModel
{
    public class SurveryViewModel
    {
        public int ID
        {


            get { return Survery.SurveyId; }
        }

        public Survey Survery { get; set; }
        public List<QuestionModel> DatabaseListofQuestions { get; set; }




        //public SurveryViewModel(IList<Question> InDatabase)
        //{
        //    Survery = new Survey();
        //    DatabaseListofQuestions = InDatabase.ToList();
        //}

        //public SurveryViewModel()
        //{
        //    Survery = new Survey();
        //    DatabaseListofQuestions = new List<Question>();
        //}
    }

    public class QuestionModel
    {
     
        public int Id
        {
            get;
            set;
        }
        public string Question
        {
            get;
            set;
        }
        public bool Checked
        {
            get;
            set;
        }

        public QuestionModel(int id, string question, bool @checked)
        {
            Id = id;
            Question = question;
            Checked = @checked;
        }
    }
}