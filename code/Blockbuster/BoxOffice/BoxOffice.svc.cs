//===============================================================================================//
//
// Author      : Chyld Medford
// Contact     : chyld@cingulariti.net
// Date        : 2008-11-18
// File        : BoxOffice.svc.cs
// Description : Returns top 10 movies and related statistics for a given date.
//
// Copyright   : Copyright (C) Cingulariti.  All rights reserved. 
// 
//===============================================================================================//

using System;
using System.Collections.Generic;
using System.Configuration;
using Cingulariti.Blockbuster.Business;

namespace Cingulariti.Blockbuster.BoxOffice
{
    public class BoxOffice : IBoxOffice
    {
        public BoxOfficeDto GetBoxOfficeResults(DateTime p_ResultsDate)
        {
            String baseUrl = ConfigurationManager.AppSettings["baseUrl"];
            String basePath = AppDomain.CurrentDomain.BaseDirectory;
            String xslFile = ConfigurationManager.AppSettings["xslFile"];

            BoxOfficeDto payload = new BoxOfficeDto() { ResultsDate = p_ResultsDate, Movies = new List<MovieDto>() };

            try
            {
                Manager.GetBoxOfficeResults(baseUrl, p_ResultsDate, basePath, xslFile, payload);
            }
            catch
            {
                // catching all faults
            }

            return payload;
        }
    }
}

//===============================================================================================//
//
// Copyright   : Copyright (C) Cingulariti.  All rights reserved. 
// 
//===============================================================================================//
