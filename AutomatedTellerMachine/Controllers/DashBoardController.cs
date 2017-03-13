using System;
using System.Collections.Generic;
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

namespace AutomatedTellerMachine.Controllers
{
    public class DashBoardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        
        // GET: DashBoard
        public ActionResult Index()
        {

            // new Series { Name = "Year 1800", Data = new Data(new object[] { 107, 31, 635, 203, 2 }) },

          //  var distictSurverys = db.Surveys.Select(x => x).ToList();

            var distictSurverys = db.Surveys.Select(x=>x).ToList();

            var questiontext = db.Answers.Select(x => x.AnswerText).Distinct().ToArray();

            List<Series> listSeries = new List<Series>();
            foreach (var survery in distictSurverys)
            {
                var answertext = db.Answers.Where(s=>s.SurveyId==survery.SurveyId).Select(x => x.AnswerText).Distinct().Select(x => new
                {
                    KPI = x,
                    Count = db.Answers.Where(s => s.AnswerText == x).Count()

                });
                
                var data = answertext.Select(x => x.Count).ToArray().Cast<object>().ToArray();

                listSeries.Add(new Series { Name = survery.Description.ToString(), Data = new Data(data) } );
            }


          

           



            Highcharts chart = new Highcharts("chart")
              .InitChart(new Chart { DefaultSeriesType = ChartTypes.Bar })
              .SetTitle(new Title { Text = "|Survery Demo" })
              .SetSubtitle(new Subtitle { Text = "Source: Feedbacks" })
              .SetXAxis(new XAxis
              {
                  Categories = questiontext,
                  Title = new XAxisTitle { Text = string.Empty }
              })
              .SetYAxis(new YAxis
              {
                  Min = 0,
                  Title = new YAxisTitle
                  {
                      Text = "Population (millions)",
                      Align = AxisTitleAligns.High
                  }
              })
              .SetTooltip(new Tooltip { Formatter = "function() { return ''+ this.series.name +': '+ this.y +' counts'; }" })
              .SetPlotOptions(new PlotOptions
              {
                  Bar = new PlotOptionsBar
                  {
                      DataLabels = new PlotOptionsBarDataLabels { Enabled = true }
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

            return View(chart);
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
