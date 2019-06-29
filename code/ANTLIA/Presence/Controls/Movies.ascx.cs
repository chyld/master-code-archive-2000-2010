//===============================================================================================//
//
// Author      : Chyld Medford
// Contact     : chyld@cingulariti.net
// Date        : 2008-11-26
// File        : Movies.ascx.cs
// Description : Movie control.
//
// Copyright   : Copyright (C) Cingulariti.  All rights reserved. 
// 
//===============================================================================================//

using System;
using System.Linq;
using System.Web.UI.DataVisualization.Charting;
using Presence.ServiceReference;

namespace Cingulariti.ANTLIA.Presence.Controls
{
    public partial class Movies : System.Web.UI.UserControl
    {
        protected void Page_Load(Object sender, EventArgs e)
        {
            try
            {
                Compose();
            }
            catch
            {
                // catch all errors
            }
        }

        private void Compose()
        {
            BoxOfficeClient c = new BoxOfficeClient();

            if (!IsPostBack)
            {
                Int32 dayCounter = 0;

                while (true)
                {
                    BoxOfficeDto boxoffice = c.GetBoxOfficeResults(DateTime.Now.AddDays(-1 * dayCounter));

                    Int32 badMovies = (from movie in boxoffice.Movies
                                       where movie.TotalGross == 0
                                       select movie).Count();

                    if ((badMovies > 5) || (boxoffice.Movies.Count() != 10))
                        dayCounter++;
                    else
                        break;
                }

                sliderextender.Minimum = 0;
                sliderextender.Maximum = (DateTime.Now - DateTime.Parse("01/04/2002")).Days;
                textboxSlider.Text = (sliderextender.Maximum - dayCounter).ToString();
            }

            chartTotalGross.AntiAliasing = AntiAliasingStyles.All;
            BoxOfficeDto dto = c.GetBoxOfficeResults(DateTime.Now.AddDays(Double.Parse(textboxSlider.Text) - sliderextender.Maximum));

            var movies = (from m in dto.Movies
                          select new
                          {
                              m.Title,
                              m.TotalGross,
                              m.Theaters,
                              m.Average,
                              m.DailyGross,
                              PercentChange = m.PercentChange / 100
                          }).ToList();

            chartTotalGross.Series["seriesTotalGross"].Points.DataBindXY(movies, "Title", movies, "TotalGross");
            chartTheaters.Series["seriesTheaters"].Points.DataBindXY(movies, "Title", movies, "Theaters");
            chartAverage.Series["seriesAverage"].Points.DataBindXY(movies, "Title", movies, "Average");
            chartCombo.Series["seriesDailyGross"].Points.DataBindXY(movies, "Title", movies, "DailyGross");
            chartCombo.Series["seriesPercentChange"].Points.DataBindXY(movies, "Title", movies, "PercentChange");

            labelDate.Text = dto.ResultsDate.ToString("D");

            c.Close();
        }
    }
}

//===============================================================================================//
//
// Copyright   : Copyright (C) Cingulariti.  All rights reserved. 
// 
//===============================================================================================//
