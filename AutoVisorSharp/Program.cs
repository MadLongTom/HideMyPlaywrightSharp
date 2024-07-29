// See https://aka.ms/new-console-template for more information
using AutoVisorSharp;
using Microsoft.Playwright;

Console.WriteLine("Hello, World!");

using var playwright = await Playwright.CreateAsync();
await using var browser = await playwright.Chromium.LaunchAsync(new() { 
    Channel = "msedge",
    Headless = false,
    Args = [
                "--no-sandbox",
                //"--allow-running-insecure-content",
                "--ignore-certificate-errors",
                "--disable-single-click-autofill",
                "--disable-autofill-keyboard-accessory-view[8]",
                "--disable-full-form-autofill-ios",
                "--incognito",
                "--log-level=3",
                "--disable-dev-shm-usage",
                "--disable-blink-features=AutomationControlled",
                "--mute-audio",
                //"--headless"
        ]
});
var context = await browser.NewContextAsync();
var page = await browser.NewPageAsync();
await page.AddInitScriptAsync(script:Consts.Stealth);
page.Response += async (sender, e) =>
{
    var response = await e.TextAsync();
    Console.WriteLine(e.Url + Environment.NewLine + response);
};
await page.GotoAsync("https://bot.sannysoft.com/");
await Task.Delay(int.MaxValue);
