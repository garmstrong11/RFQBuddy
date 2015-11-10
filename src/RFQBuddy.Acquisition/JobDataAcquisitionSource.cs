namespace RFQBuddy.Acquisition
{
  using System;
  using System.Linq;
  using System.Xml.Linq;
  using OpenQA.Selenium;
  using OpenQA.Selenium.Firefox;
  using OpenQA.Selenium.Support.UI;
  using RFQBuddy.Acquisition.Contracts;

  public class JobDataAcquisitionSource : IJobDataAcquisitionSource
  {
    private readonly FirefoxDriver _driver;
    private readonly ISettingsService _settings;
    private string _handle1 = string.Empty;

    public JobDataAcquisitionSource(ISettingsService settings)
    {
      _settings = settings;
      _driver = new FirefoxDriver();
    }

    public XElement AcquireJobData(string url)
    {
      _driver.Navigate().GoToUrl(url);
      try {
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        var submit = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("vendor_submit")));

        var uName = _driver.FindElementById("login_username");
        var pword = _driver.FindElementById("login_password");
        //var submit = _driver.FindElementById("vendor_submit");

        uName.SendKeys(_settings.ReconUsername); // "ivieteam@integracolor.com"
        pword.SendKeys(_settings.ReconPassword); // "steve"
        submit.Click();

        wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("job_l_print")));

        _handle1 = _driver.WindowHandles.First();
        var printButton = _driver.FindElementById("job_l_print");
        printButton.Click();

        var rfqButton = _driver.FindElementById("print_rfq");

        rfqButton.Click();

        wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        wait.Until(d => d.WindowHandles.Count > 1);

        var handle2 = _driver.WindowHandles.First(h => h != _handle1);

        _driver.SwitchTo().Window(handle2);

        wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        wait.Until(d => d.PageSource.Contains("div class=\"content\""));

        return XElement.Parse(_driver.PageSource);
      }
      catch (WebDriverTimeoutException wex) {
        throw new AcquisitionFailedException(wex.Message, wex.InnerException);
      }
      finally {
        _driver.Close();
        _driver.SwitchTo().Window(_handle1);
        _driver.Close();
      }
    }
  }
}