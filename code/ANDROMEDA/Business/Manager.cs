//===============================================================================================//
//
// Author      : Chyld Medford
// Contact     : chyld@cingulariti.net
// Date        : 2008-11-18
// File        : Manager.cs
// Description : Manages the BoxOffice service.
//
// Copyright   : Copyright (C) Cingulariti.  All rights reserved. 
// 
//===============================================================================================//

using System;
using System.IO;
using System.Xml.Linq;
using Cingulariti.ANDROMEDA.HtmlParser;

namespace Cingulariti.ANDROMEDA.Business
{
    public class Manager
    {
        public static void GetBoxOfficeResults(String p_BaseUrl, DateTime p_ResultsDate, String p_BasePath, String p_XslFile, BoxOfficeDto p_Payload)
        {
            String url = p_BaseUrl + p_ResultsDate.ToString("yyyy-MM-dd");
            String xml = p_BasePath + @"xml\" + Guid.NewGuid().ToString() + ".xml";
            String xsl = p_BasePath + @"xsl\" + p_XslFile;

            XDocument movieXml = new XDocument();

            try
            {
                Transform.HtmlToXml(url, xsl, xml);
                movieXml = XDocument.Load(xml);
            }
            finally
            {
                File.Delete(xml);
            }

            ParseMovieDataFromXmlFile(movieXml, p_Payload);
        }

        private static void ParseMovieDataFromXmlFile(XDocument p_MovieXml, BoxOfficeDto p_Payload)
        {
            foreach (var movie in p_MovieXml.Descendants("movie"))
            {
                MovieDto child = new MovieDto();

                Int32 tempInt32;
                Decimal tempDecimal;
                Double tempDouble;

                child.Title = movie.Element("title").Value;
                child.Studio = movie.Element("studio").Value;

                if (Int32.TryParse(movie.Element("rank").Value, out tempInt32))
                    child.Rank = tempInt32;

                if (Int32.TryParse(movie.Element("daynumber").Value, out tempInt32))
                    child.DayNumber = tempInt32;

                if (Int32.TryParse(movie.Element("theaters").Value, out tempInt32))
                    child.Theaters = tempInt32;

                if (Double.TryParse(movie.Element("percentchange").Value, out tempDouble))
                    child.PercentChange = tempDouble;

                if (Decimal.TryParse(movie.Element("dailygross").Value, out tempDecimal))
                    child.DailyGross = tempDecimal;

                if (Decimal.TryParse(movie.Element("average").Value, out tempDecimal))
                    child.Average = tempDecimal;

                if (Decimal.TryParse(movie.Element("totalgross").Value, out tempDecimal))
                    child.TotalGross = tempDecimal;

                p_Payload.Movies.Add(child);
            }
        }
    }
}

//===============================================================================================//
//
// Copyright   : Copyright (C) Cingulariti.  All rights reserved. 
// 
//===============================================================================================//
