//===============================================================================================//
//
// Author      : Chyld Medford
// Contact     : chyld@cingulariti.net
// Date        : 2008-11-18
// File        : WebServiceTest.cs
// Description : Tests the BoxOffice WCF web service.
//
// Copyright   : Copyright (C) Cingulariti.  All rights reserved. 
// 
//===============================================================================================//

using System;
using Cingulariti.Blockbuster.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cingulariti.Blockbuster.ExternalTesting
{
    [TestClass]
    public class WebServiceTest
    {
        [TestMethod]
        public void TestBasicOperation()
        {
            BoxOfficeClient client = new BoxOfficeClient();
            BoxOfficeDto results = client.GetBoxOfficeResults(DateTime.Now.AddDays(-25));
            client.Close();
        }
    }
}

//===============================================================================================//
//
// Copyright   : Copyright (C) Cingulariti.  All rights reserved. 
// 
//===============================================================================================//
