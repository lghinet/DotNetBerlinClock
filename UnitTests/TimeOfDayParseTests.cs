using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BerlinClock.UnitTests
{
    [TestClass]
    public class TimeOfDayParseTests
    {
        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void Parse_shoud_throw_overflow()
        {
            TimeOfDay.ParseExact("12:44:99");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Parse_shoud_throw_format_exception()
        {
            TimeOfDay.ParseExact("12:44::cc");
        }

        [TestMethod]
        public void Parse_ok()
        {
            Assert.AreEqual(new TimeOfDay(12, 44, 12), TimeOfDay.ParseExact("12:44:12"));
        }
    }
}
