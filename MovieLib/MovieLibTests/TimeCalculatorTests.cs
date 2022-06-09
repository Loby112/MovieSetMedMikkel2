using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLib.Tests {
    [TestClass()]
    public class TimeCalculatorTests {
        [TestMethod()]
        public void ConvertMinutesToSecondsTest() {
            Movie testMovie = new Movie(){Country = "Denmark", Id = 1, LengthInMinutes = 100, Name = "Star Wars"};

            int seconds = TimeCalculator.ConvertMinutesToSeconds(testMovie.LengthInMinutes);

            Assert.AreEqual(6000, seconds);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => TimeCalculator.ConvertMinutesToSeconds(0));
        }
    }
}

namespace MovieLibTests {
    internal class TimeCalculatorTests {
    }
}
