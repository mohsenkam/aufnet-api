using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Shared.utils;
using NUnit.Framework;

namespace Aufnet.Backend.UnitTests.ApiServiceShared.utils
{
    [TestFixture]
    public class UtilsTest
    {
        [Test]
        public void GenerateTrackingIdTest() 
        {
            var dateTime = new DateTime(2017,05,27,18,37,23,176);
            //_mappings = { "q", "F", "E", "P", "C", "G", "O", "a", "Z", "R" }
            //"MMddHHmmssfff"
            var expected = "qGEaFZPaEPFaO";
            Assert.AreEqual(expected, UtilityMethods.GenerateTrackingId(dateTime));


            dateTime = new DateTime(2017, 05, 27, 18, 37, 23, 070);
            expected = "qGEaFZPaEPqaq";
            Assert.AreEqual(expected, UtilityMethods.GenerateTrackingId(dateTime));

            expected = "qGEaFZPaEPqa";
            Assert.AreNotEqual(expected, UtilityMethods.GenerateTrackingId(dateTime));
            
        }
    }
}
