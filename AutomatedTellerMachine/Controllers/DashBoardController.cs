using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutomatedTellerMachine.Models;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Samples.Models;
using Point = DotNet.Highcharts.Options.Point;

namespace AutomatedTellerMachine.Controllers
{
    public class DashBoardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: DashBoard
        public ActionResult Index()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();


            var questions = db.Questions.ToList();

            foreach (var question in questions)
            {
                listItems.Add(
                    new SelectListItem()
                    {

                        Text = question.QuestionText,
                        Value = question.QuestionText

                    });

            }


            this.ViewBag.ListItems = listItems;

            var chart = SurveryBarChart();
            var donutchart = SentimentStackBarChart();

            ChartsModel ChartCollection = new ChartsModel();

            ChartCollection.Option = listItems.LastOrDefault().Text;
            ChartCollection.Chart1 = null;
            ChartCollection.Chart2 = donutchart;
            //  this.ViewBag.DonutchartModel = Donutchart; 
            return View(ChartCollection);
        }

        // POST: DashBoard/Index/5
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();


            var questions = db.Questions.ToList();

            foreach (var question in questions)
            {
                listItems.Add(
                    new SelectListItem()
                    {

                        Text = question.QuestionText,
                        Value = question.QuestionText

                    });

            }


            this.ViewBag.ListItems = listItems;



            string name = Request.Form["listItems"];
            ChartsModel ChartCollection = new ChartsModel();

            var chart = SurveryBarChart(name);
            var donutchart = SentimentStackBarChart();
            ChartCollection.Chart1 = chart;
            ChartCollection.Chart2 = donutchart;
            //  this.ViewBag.DonutchartModel = Donutchart; 
            return View(ChartCollection);
        }

        public Highcharts SurveryBarChart(string KPI = "Speed of Service")
        {


            var listofAnswers = new List<string>()
            {
                "Excellent", "Good","Average","Poor","Fair"
            };

            var distictSurverys = db.Surveys.Select(x => x).ToList();

            var questiontext = db.Answers.Select(x => x.AnswerText).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToArray();

            DataTable chartdata = new DataTable();

            chartdata.Columns.Add("Surverys", typeof(string));
            foreach (string listofAnswer in listofAnswers)
            {
                chartdata.Columns.Add(listofAnswer, typeof(double));
            }



            foreach (var survery in distictSurverys)
            {
                DataRow series = chartdata.NewRow();

                series["Surverys"] = survery.Description;

                var allansersin =
                    db.Answers.Where(s => s.SurveyId == survery.SurveyId)
                        .Select(x => x.AnswerText)
                        .Distinct()
                        .Where(x => !string.IsNullOrEmpty(x))
                        .ToList();



                var getQuestionID = db.Questions.Single(x => x.QuestionText == KPI).QuestionId;

                var totolAnswersCount = db.Answers.Where(s => s.SurveyId == survery.SurveyId).
                    Where(q => q.Question.QuestionId == getQuestionID).Count();

                foreach (string answer in allansersin)
                {


                    var answers =
                        db.Answers.Where(s => s.SurveyId == survery.SurveyId)
                            .Where(q => q.Question.QuestionId == getQuestionID).Where(x => x.AnswerText == answer).Count();

                    try
                    {
                        series[answer] = ((double)answers / (double)totolAnswersCount) * 100;
                    }
                    catch (DivideByZeroException e)
                    {
                        // series[answer] = 0;

                    }

                }

                chartdata.Rows.Add(series);

            }



            List<Series> listSeries = new List<Series>();


            foreach (DataRow seriesDataRow in chartdata.Rows)
            {
                var surveryName = seriesDataRow["Surverys"];
                var data = seriesDataRow.ItemArray.Skip(1).ToArray();
                listSeries.Add(new Series { Name = surveryName.ToString(), Data = new Data(data) });
            }


            Highcharts chart = new Highcharts("chart")
             .InitChart(new Chart { DefaultSeriesType = ChartTypes.Bar })
             .SetTitle(new Title { Text = KPI+"|Surveys" })
             .SetSubtitle(new Subtitle { Text = "Source: Feedbacks" })
             .SetXAxis(new XAxis
             {
                 Categories = questiontext,
                 Title = new XAxisTitle { Text = string.Empty }
             })
             .SetYAxis(new YAxis
             {

                 Max = 100,
                 Min = 0,
                 Title = new YAxisTitle
                 {
                     Text = "Total percent Users Feedbacks",
                     
                     Align = AxisTitleAligns.High
                 }
                 

                 
             })
             .SetTooltip(new Tooltip { Formatter = "function() { return ''+ this.series.name +': '+ this.y +' counts'; }" })

                //.SetPlotOptions(new PlotOptions
                //{
                //    Column = new PlotOptionsColumn { Stacking = Stackings.Percent }

              //  .SetPlotOptions(new PlotOptions { Column = new PlotOptionsColumn { Stacking = Stackings.Percent } })

             .SetPlotOptions(new PlotOptions
             {

                 Bar = new PlotOptionsBar
                 {
                     DataLabels = new PlotOptionsBarDataLabels
                     {
                         Enabled = true,
                       
                         Formatter = "function() { return this.y +'%'; }",
                         Style = "fontWeight: 'bold'"
                     }
                 }
             })
             .SetLegend(new Legend
             {
                 Layout = Layouts.Vertical,
                 Align = HorizontalAligns.Right,
                 VerticalAlign = VerticalAligns.Top,
                 X = -100,
                 Y = 100,
                 Floating = true,
                 BorderWidth = 1,
                 BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                 Shadow = true
             })
             .SetCredits(new Credits { Enabled = false })
             .SetSeries(listSeries.ToArray());

            return chart;

        }

        public Highcharts SentimentStackBarChart()
        {

            //Highcharts chart = new Highcharts("chart")
            //   .InitChart(new Chart { PlotShadow = false })
            //   .SetTitle(new Title { Text = "Browser market shares at a specific website, 2010" })
            //   .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }" })
            //   .SetPlotOptions(new PlotOptions
            //   {
            //       Pie = new PlotOptionsPie
            //       {
            //           AllowPointSelect = true,
            //           Cursor = Cursors.Pointer,
            //           DataLabels = new PlotOptionsPieDataLabels
            //           {
            //               Color = ColorTranslator.FromHtml("#000000"),
            //               ConnectorColor = ColorTranslator.FromHtml("#000000"),
            //               Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }"
            //           }
            //       }
            //   })
            //   .SetSeries(new Series
            //   {
            //       Type = ChartTypes.Pie,
            //       Name = "Browser share",
            //       Data = new Data(new object[]
            //       {
            //            new object[] { "Firefox", 45.0 },
            //            new object[] { "IE", 26.8 },
            //            new Point
            //            {
            //                Name = "Chrome",
            //                Y = 12.8,
            //                Sliced = true,
            //                Selected = true
            //            },
            //            new object[] { "Safari", 8.5 },
            //            new object[] { "Opera", 6.2 },
            //            new object[] { "Others", 0.7 }
            //       })
            //   });


            var distictSurverys = db.Surveys.Select(x => x).ToList(); //columnName

            DataTable chartdata = new DataTable();

            chartdata.Columns.Add("Dimension");

            foreach (var distictSurvery in distictSurverys)
            {
                chartdata.Columns.Add(distictSurvery.Description, typeof(int));
            }


            List<string> DimensionList = new List<string>()
            {
                "Positive",
                "Negative"
            };

            foreach (var dimension in DimensionList)
            {
                DataRow dataRow = chartdata.NewRow();

                dataRow["Dimension"] = dimension;

                foreach (var distictSurvery in distictSurverys)
                {
                    try
                    {
                        var positiveCount =
                            db.UserFeedbacks.Where(x => x.SurveyId == distictSurvery.SurveyId)
                                .Where(x => x.Sentiment == dimension)
                                .Count();


                        var Count =
                            db.UserFeedbacks.Where(x => x.SurveyId == distictSurvery.SurveyId)
                                .Count();



                        dataRow[distictSurvery.Description] = positiveCount;
                    }

                    catch (Exception exception)
                    {
                        dataRow[distictSurvery.Description] = 0;
                    }

                }


                chartdata.Rows.Add(dataRow);
            }



            var questiontext = db.Answers.Select(x => x.AnswerText).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToArray();

            List<Series> listSeries = new List<Series>();

            int i = 0;
            foreach (DataRow row in chartdata.Rows)
            {
                var allvalues = row.ItemArray.Skip(1).ToArray();


                i++;
                listSeries.Add(new Series()
                {
                    Name = row["Dimension"].ToString(),

                    Data = new Data(allvalues),

                    Color = row["Dimension"].ToString() == "Positive" ? Color.LawnGreen : Color.Red

                });

            }


            var Categories = distictSurverys.Select(x => x.Description).ToArray();


            Highcharts chart = new Highcharts("chartStackBar")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Column })
                .SetTitle(new Title { Text = "Customer Feedback Sentiment" })
                .SetXAxis(new XAxis { Categories = Categories })
                .SetYAxis(new YAxis { Min = 0, Title = new YAxisTitle { Text = "Total Participated Customers" } })
                .SetTooltip(new Tooltip { Formatter = "function() { return ''+ this.series.name +': '+ this.y +' ('+ Math.round(this.percentage) +'%)'; }" })
                .SetPlotOptions(new PlotOptions { Column = new PlotOptionsColumn { Stacking = Stackings.Percent } })

                .SetSeries(listSeries.ToArray());

            return chart;
        }



        // GET: DashBoard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DashBoard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashBoard/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DashBoard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DashBoard/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DashBoard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DashBoard/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
