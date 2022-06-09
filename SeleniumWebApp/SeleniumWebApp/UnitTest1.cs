using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SeleniumWebApp {
    [TestClass]
    public class UnitTest1{
        private static readonly string DriverDirectory = "C:\\Users\\Lobyl\\Documents\\3. Semester\\TestPractice\\WebDriver";
        private static IWebDriver _driver;

        [ClassInitialize]
        public static void Setup(TestContext context){
            _driver = new ChromeDriver(DriverDirectory);
        }

        [ClassCleanup]
        public static void TearDown(){
            _driver.Dispose();
        }

        [TestMethod]
        public void TestMethod1(){
            string url = "file:///C:\\Users\\Lobyl\\Documents\\3. Semester\\TestPractice\\MovieSetMedMikkel-master\\Frontend\\index.html";
            //string url = "https://winddatatobilp.azurewebsites.net/";

            _driver.Navigate().GoToUrl(url);
            string title = _driver.Title;
            Assert.AreEqual("Movies", title);

            IWebElement inputElement = _driver.FindElement(By.Id("addNameInput"));
            inputElement.SendKeys("TestMovie");

            inputElement = _driver.FindElement(By.Id("addLengthInput"));
            inputElement.SendKeys("50");


            inputElement = _driver.FindElement(By.Id("addCountryInput"));
            inputElement.SendKeys("Denmark");

            IWebElement buttonElement = _driver.FindElement(By.Id("addButton"));
            buttonElement.Click();

            IWebElement outputElement = _driver.FindElement(By.Id("outputField"));
            string text = outputElement.Text;

            Assert.AreEqual("{ \"id\": 0, \"name\": \"TestMovie\", \"lengthInMinutes\": \"50\", \"country\": \"Denmark\" }", text);



        }
    }
}
