using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Componets.BL.ServiceReferenceQuotes;
using NUnit.Framework;

namespace Componets.BL.Test
{
    [TestFixture]
    public class ServiceTest
    {
        [Test]
        public void ServiceOne()
        {
            var serviceClient = new DelayedStockQuoteSoapClient("DelayedStockQuoteSoap");
            var google = serviceClient.GetQuickQuote("GOOG", "0");
            
        }
    }
}
