using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BerlinClock.UnitTests
{
    [TestClass]
    public class BerlinTimeConverterTests
    {
        private readonly BerlinTimeConverter _converter = new BerlinTimeConverter();

        [TestMethod]
        public void Convert_14_hours()
        {
           Assert.AreEqual("RROO\r\nRRRR",  _converter.HandleHours(14));
        }

        [TestMethod]
        public void Convert_4_hours()
        {
            Assert.AreEqual("OOOO\r\nRRRR", _converter.HandleHours(4));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void HandleHours_shoud_throw()
        {
            _converter.HandleHours(99);
        }

        [TestMethod]
        public void Convert_44_minutes()
        {
            Assert.AreEqual("YYRYYRYYOOO\r\nYYYY", _converter.HandleMinutes(44));
        }

    }
}
