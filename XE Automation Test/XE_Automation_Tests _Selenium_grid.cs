using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

public class SeleniumTestsChromeSeleniumGrid
{
    private IWebDriver driver;
    [SetUp]
    public void Setup()
    {
       //var options = new FirefoxOptions();
        var options = new ChromeOptions();
       // var options = new InternetExplorerOptions();
        //options.AddArguments("--headless", "--window-size=1920,1080");
        driver = new RemoteWebDriver(new Uri(
            "http://localhost:4444/wd/hub"), options);

        //driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
    }

    [Test]
    [Parallelizable]
    public void Test_XECom_EurBgnCourse_ByKeyboard()
    {
        driver.Navigate().GoToUrl("https://xe.com");

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        // Choose amount "1"
        driver.FindElement(By.CssSelector("input#amount")).Click();
        driver.FindElement(By.CssSelector("input#amount")).SendKeys("1");

        // Choose source currency "EUR"
        driver.SwitchTo().ActiveElement().SendKeys(Keys.Tab);
        driver.SwitchTo().ActiveElement().SendKeys("eur");
        driver.SwitchTo().ActiveElement().SendKeys(Keys.Enter);

        // Choose destination currency "BGN"
        driver.SwitchTo().ActiveElement().SendKeys(Keys.Tab);
        driver.SwitchTo().ActiveElement().SendKeys(Keys.Tab);
        driver.SwitchTo().ActiveElement().SendKeys("bgn");
        driver.SwitchTo().ActiveElement().SendKeys(Keys.Enter);

        // Click the [Submit] button
        driver.SwitchTo().ActiveElement().SendKeys(Keys.Tab);
        driver.SwitchTo().ActiveElement().SendKeys(Keys.Enter);

        // Assert the conversion rate is correct
        var rateFound = driver.FindElement(
            By.XPath("//*[contains(.,'1 EUR = 1.95583 BGN')]"));
        Assert.True(rateFound != null);
    }

    [Test]
    [Parallelizable]
    public void Test_XECom_EurBgnCourse_ByMouse()
    {
        driver.Navigate().GoToUrl("https://xe.com");

        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        // Choose amount "1"
        driver.FindElement(By.XPath("(//input[@type='text'])[1]")).Click();
        driver.FindElement(By.XPath("(//input[@type='text'])[1]")).SendKeys("1");

        // Choose source currency "EUR"
        driver.FindElement(By.XPath("(//input[@type='text'])[2]")).Click();
        driver.FindElement(By.XPath("(//input[@type='text'])[2]")).SendKeys("eur");
        driver.FindElement(By.XPath("//*[(contains(@class,'converterform-dropdown__option') or contains(@class,'ListboxOption')) and contains(.,'EUR')]")).Click();

        // Choose destination currency "BGN"
        driver.FindElement(By.XPath("(//input[@type='text'])[3]")).Click();
        driver.FindElement(By.XPath("(//input[@type='text'])[3]")).SendKeys("bgn");
        driver.FindElement(By.XPath("//*[(contains(@class,'converterform-dropdown__option') or contains(@class,'ListboxOption')) and contains(.,'BGN')]")).Click();

        // Click the [Submit] button
        driver.FindElement(By.XPath("//button[@type='submit'] | //a[contains(@class,'BaseButton')]")).Click();

        // Assert the conversion rate is correct
        var rateFound = driver.FindElement(
            By.XPath("//*[contains(.,'1 EUR = 1.95583 BGN')]"));
        Assert.True(rateFound != null);
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}