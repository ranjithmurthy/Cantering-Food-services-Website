using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Samples.Models;

namespace AutomatedTellerMachine.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: DashBoard
        public ActionResult Index()
        {
            Highcharts chart = new Highcharts("chart")
              .InitChart(new Chart { DefaultSeriesType = ChartTypes.Bar })
              .SetTitle(new Title { Text = "Historic World Population by Region" })
              .SetSubtitle(new Subtitle { Text = "Source: Wikipedia.org" })
              .SetXAxis(new XAxis
              {
                  Categories = new[] { "Africa", "America", "Asia", "Europe", "Oceania" },
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
              .SetTooltip(new Tooltip { Formatter = "function() { return ''+ this.series.name +': '+ this.y +' millions'; }" })
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
              .SetSeries(new[]
              {
                    new Series { Name = "Year 1800", Data = new Data(new object[] { 107, 31, 635, 203, 2 }) },
                    new Series { Name = "Year 1900", Data = new Data(new object[] { 133, 156, 947, 408, 6 }) },
                    new Series { Name = "Year 2008", Data = new Data(new object[] { 973, 914, 4054, 732, 34 }) }
              });

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
