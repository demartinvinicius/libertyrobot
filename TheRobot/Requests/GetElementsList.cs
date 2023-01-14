﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRobot.Response;

namespace TheRobot.Requests;

public class GetElementsList : IRobotRequest
{
    public TimeSpan DelayBefore { get; set; }
    public TimeSpan DelayAfter { get; set; }
    public Action<IWebDriver> PreExecute { get; set; }
    public Action<IWebDriver> PostExecute { get; set; }
    public By By { get; set; }

    public RobotResponse Exec(IWebDriver driver)
    {
        List<IWebElement> elements;
        try
        {
            elements = driver.FindElements(By).ToList();
        }
        catch (Exception ex)
        {
            return new()
            {
                Status = RobotResponseStatus.ElementNotFound
            };
        }

        return new()
        {
            Status = RobotResponseStatus.ActionRealizedOk,
            WebElements = elements
        };
    }
}