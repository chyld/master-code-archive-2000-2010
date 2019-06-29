using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Testing.ServiceReference;

namespace Cingulariti.ANDROMEDA.Testing
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestBoxOfficeWebService()
        {
            BoxOfficeClient client = new BoxOfficeClient();
            BoxOfficeDto dto = client.GetBoxOfficeResults(DateTime.Now.AddDays(-10));
            client.Close();
        }
    }
}
