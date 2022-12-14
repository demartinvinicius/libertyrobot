using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRobot.Response;
using static System.Net.Mime.MediaTypeNames;

namespace TheRobot.Requests;

public class SelectBy2Clicks : IRobotRequest
{
    public TimeSpan DelayBefore { get; set; }
    public TimeSpan DelayAfter { get; set; }
    public Action<IWebDriver> PreExecute { get; set; }
    public Action<IWebDriver> PostExecute { get; set; }
    public By By1 { get; set; }
    public By By2 { get; set; }
    public TimeSpan DelayBetweenClicks { get; set; }

    public RobotResponse Exec(IWebDriver driver)
    {
        try
        {
            IWebElement firstClickElement = new WebDriverWait(driver, TimeSpan.FromSeconds(2)).Until(x => x.FindElement(By1));

            var actions = new Actions(driver);
            actions.ScrollToElement(firstClickElement);
            actions.ScrollByAmount(0, 100);
            actions.Perform();
            System.Threading.Thread.Sleep(DelayBetweenClicks);

            firstClickElement.Click();

            System.Threading.Thread.Sleep(DelayBetweenClicks);
            IWebElement secondClickElement = new WebDriverWait(driver, TimeSpan.FromSeconds(2)).Until(x => x.FindElement(By2));
            secondClickElement.Click();

            //Console.WriteLine(String.Join('\n', option.Select(x => x.GetAttribute("outerText"))));

            //selectObject.SelectByText(selectObject.Options.First(x => x.Text.Contains(Text)).Text);
        }
        catch (OpenQA.Selenium.WebDriverTimeoutException ex)
        {
            return new()
            {
                Status = RobotResponseStatus.ElementNotFound
            };
        }
        return new()
        {
            Status = RobotResponseStatus.ActionRealizedOk
        };
    }
}