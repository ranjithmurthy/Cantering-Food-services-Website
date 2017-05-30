using AutomatedTellerMachine.Models;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;

namespace AutomatedTellerMachine.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: DashBoard
        public ActionResult Index()
        {
            var listItems = new List<SelectListItem>();

            var questions = db.Questions.ToList();

            foreach (var question in questions)
                listItems.Add(
                    new SelectListItem
                    {
                        Text = question.QuestionText,
                        Value = question.QuestionText
                    });

            ViewBag.ListItems = listItems;

            var chart = SurveryBarChart();
            var donutchart = SentimentStackBarChart();

            var ChartCollection = new ChartsModel();

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
            var listItems = new List<SelectListItem>();

            var questions = db.Questions.ToList();

            foreach (var question in questions)
                listItems.Add(
                    new SelectListItem
                    {
                        Text = question.QuestionText,
                        Value = question.QuestionText
                    });

            ViewBag.ListItems = listItems;

            var name = Request.Form["listItems"];
            var ChartCollection = new ChartsModel();

            var chart = SurveryBarChart(name);
            var donutchart = SentimentStackBarChart();
            ChartCollection.Chart1 = chart;
            ChartCollection.Chart2 = donutchart;
            //  this.ViewBag.DonutchartModel = Donutchart;
            return View(ChartCollection);
        }

        public Highcharts SurveryBarChart(string kpi = "Speed of Service")
        {
            var chart = new Highcharts("chart");

            try
            {
                //GetlistOfAnswers
                var listofAnswers =
                    db.Answers.Where(k => k.Question.QuestionText == kpi).Select(x => x.AnswerText).ToList().Distinct();

                var distictSurverys = db.Surveys.Select(x => x).ToList();

                var questiontext =
                    db.Answers.Select(x => x.AnswerText).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToArray();

                var chartdata = new DataTable();

                chartdata.Columns.Add("Surverys", typeof(string));
                foreach (var listofAnswer in listofAnswers)
                    chartdata.Columns.Add(listofAnswer, typeof(double));

                foreach (var survery in distictSurverys)
                {
                    var series = chartdata.NewRow();

                    series["Surverys"] = survery.Description;

                    var allansersin =
                        db.Answers.Where(s => s.SurveyId == survery.SurveyId).Where(k => k.Question.QuestionText == kpi)
                            .Select(x => x.AnswerText)
                            .Distinct()
                            .Where(x => !string.IsNullOrEmpty(x))
                            .ToList();

                    var getQuestionID = db.Questions.Single(x => x.QuestionText == kpi).QuestionId;

                    var totolAnswersCount = db.Answers.Where(s => s.SurveyId == survery.SurveyId).
                        Where(q => q.Question.QuestionId == getQuestionID).Count();

                    foreach (var answer in allansersin)
                    {
                        var answers =
                            db.Answers.Where(s => s.SurveyId == survery.SurveyId)
                                .Where(q => q.Question.QuestionId == getQuestionID)
                                .Where(x => x.AnswerText == answer)
                                .Count();

                        try
                        {
                            series[answer] = answers / (double) totolAnswersCount * 100;
                        }
                        catch (DivideByZeroException e)
                        {
                        }
                    }

                    chartdata.Rows.Add(series);
                }

                var listSeries = new List<Series>();

                foreach (DataRow seriesDataRow in chartdata.Rows)
                {
                    var surveryName = seriesDataRow["Surverys"];
                    var data = seriesDataRow.ItemArray.Skip(1).ToArray();
                    listSeries.Add(new Series {Name = surveryName.ToString(), Data = new Data(data)});
                }

                chart = new Highcharts("chart")
                    .InitChart(new Chart {DefaultSeriesType = ChartTypes.Bar})
                    .SetTitle(new Title {Text = kpi + "|Surveys"})
                    .SetSubtitle(new Subtitle {Text = "Source: Feedbacks"})
                    .SetXAxis(new XAxis
                    {
                        Categories = questiontext,
                        Title = new XAxisTitle {Text = string.Empty}
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
                    .SetTooltip(new Tooltip
                    {
                        Formatter = "function() { return ''+ this.series.name +': '+ this.y +' counts'; }"
                    })
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
                    .SetCredits(new Credits {Enabled = false})
                    .SetSeries(listSeries.ToArray());

                return chart;
            }
            catch (Exception exp)
            {
                return chart;
            }
        }

        public Highcharts SentimentStackBarChart()
        {
            var distictSurverys = db.Surveys.Select(x => x).ToList(); //columnName

            var chartdata = new DataTable();

            chartdata.Columns.Add("Dimension");

            foreach (var distictSurvery in distictSurverys)
                chartdata.Columns.Add(distictSurvery.Description, typeof(int));

            var DimensionList = new List<string>
            {
                "Positive",
                "Negative"
            };

            foreach (var dimension in DimensionList)
            {
                var dataRow = chartdata.NewRow();

                dataRow["Dimension"] = dimension;

                foreach (var distictSurvery in distictSurverys)
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

                chartdata.Rows.Add(dataRow);
            }

            var questiontext =
                db.Answers.Select(x => x.AnswerText).Where(x => !string.IsNullOrEmpty(x)).Distinct().ToArray();

            var listSeries = new List<Series>();

            var i = 0;
            foreach (DataRow row in chartdata.Rows)
            {
                var allvalues = row.ItemArray.Skip(1).ToArray();

                i++;
                listSeries.Add(new Series
                {
                    Name = row["Dimension"].ToString(),
                    Data = new Data(allvalues),
                    Color = row["Dimension"].ToString() == "Positive" ? Color.LawnGreen : Color.Red
                });
            }

            var Categories = distictSurverys.Select(x => x.Description).ToArray();

            var chart = new Highcharts("chartStackBar")
                .InitChart(new Chart {DefaultSeriesType = ChartTypes.Column})
                .SetTitle(new Title {Text = "Customer Feedback Sentiment"})
                .SetXAxis(new XAxis {Categories = Categories})
                .SetYAxis(new YAxis {Min = 0, Title = new YAxisTitle {Text = "Total Participated Customers"}})
                .SetTooltip(new Tooltip
                {
                    Formatter =
                        "function() { return ''+ this.series.name +': '+ this.y +' ('+ Math.round(this.percentage) +'%)'; }"
                })
                .SetPlotOptions(new PlotOptions {Column = new PlotOptionsColumn {Stacking = Stackings.Percent}})
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