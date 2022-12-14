using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRobot.Response;

namespace TheRobot.Requests;

public class ClickRequest : IRobotRequest
{
    public TimeSpan DelayBefore { get; set; }
    public TimeSpan DelayAfter { get; set; }
    public By? By { get; set; }
    public Action<IWebDriver>? PreExecute { get; set; }
    public Action<IWebDriver>? PostExecute { get; set; }
    public TimeSpan? Timeout { get; set; }
    public RobotResponse Exec(IWebDriver driver)
    {
        if (By == null)
        {
            throw new ArgumentNullException("By", "You must specify the element to click");
        }
        if (Timeout == null)
        {
            Timeout = TimeSpan.FromSeconds(2);
        }
        var wait = new WebDriverWait(driver, (TimeSpan) Timeout);
        try
        {
            wait.Until(e => e.FindElement(By)).Click();
        }
        catch (WebDriverTimeoutException)
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
