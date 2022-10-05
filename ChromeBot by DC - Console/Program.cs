using System;
using System.IO;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using Newtonsoft.Json;
using System.Net;


namespace ChromeBot_by_DC___Console
{
    class Program
    {
        static string chromedriver_path = Environment.CurrentDirectory;
        static string configJson_path = Environment.CurrentDirectory + "\\ConfigVisitsGenerator.json";
        static List<string> urlList = new List<string>();
        static List<string> proxyList = new List<string>();
        static int visit = 0;
        static void Main(string[] args)
        {
            if(File.Exists(configJson_path))
            {
                //Deserialize Json Config File to have configurations available in code
                ConfigVisitsGenerator config = ConfigVisitsGenerator.ReadJson(configJson_path);
                //Add all lines in Url-Proxy files to List available in code
                if(config.UrlListPath != string.Empty && config.UrlListPath != null) urlList.AddRange(File.ReadAllLines(config.UrlListPath)); 
                else Console.WriteLine("Check Config file! Some value is missing. A Sample.json was printed."); ConfigVisitsGenerator.SampleJsonPrinter();
                if (config.ProxyListPath != string.Empty && config.ProxyListPath != null) proxyList.AddRange(File.ReadAllLines(config.ProxyListPath));                 
                //Check prerequisites to start
                if (File.Exists(chromedriver_path + "\\chromedriver.exe"))
                {
                    if (urlList.Count > 0) //don't execute if UrlList is not loaded yet
                    {
                        try
                        {
                            visit = 0;
                            ChromeOptions options = new ChromeOptions();
                            /*
                             * You can add every options argument you need. More info in Selenium documents.
                             * some examples here:
                            options.AddArgument("--proxy-server=" + string_proxy);
                            options.AddArgument("--silent");
                            options.AddArgument("--log-level=3");
                            options.AddArgument("--lang=en");
                            options.AddArgument("--disable-logging");
                            options.AddArgument("--no-sandbox");
                            options.AddArgument("--disable-infobars");
                            options.AddArgument("--ignore-certificate-errors");
                            options.AddArgument("--disable-dev-shm-usage");
                            options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
                            */
                            if (config.NThreads > 0)
                            {
                                for (int i = 0; i < config.NThreads; i++)
                                {
                                    //select a different random URL from List every thread (can be only one too)
                                    Random random = new Random();
                                    int r = random.Next(0, urlList.Count - 1);
                                    string url = urlList[r];

                                    //select a different random PROXY from List every thread (if exist)
                                    string proxy = string.Empty;
                                    if (proxyList.Count > 0)
                                    {
                                        r = random.Next(0, proxyList.Count - 1);
                                        proxy = proxyList[r];
                                    }

                                    //Run each thread with different configuration                    
                                    Thread thread = new Thread(() => OpenUrl(options, chromedriver_path, url, proxy))
                                    {
                                        IsBackground = config.IsBackground
                                    };
                                    thread.Start();
                                    //System.Threading.Thread.Sleep(2 * 1000);               
                                }
                            }
                            else Console.WriteLine("Check Config file! Some value is missing. A Sample.json was printed."); ConfigVisitsGenerator.SampleJsonPrinter();
                        }
                        catch (Exception ex) { File.AppendAllText("errorLog.txt", DateTime.Now + " | " + ex.Message); }
                    }
                    else Console.WriteLine("You need to upload at least one URL");
                }
                else Console.WriteLine("Chromedriver.exe is missing in the executable file same folder.\nMake sure you have installed latest version for your browser.\nIf you need you can download at: https://chromedriver.chromium.org/downloads");
                void OpenUrl(ChromeOptions options, string chromedriver_path, string url, string proxy)
                {
                    if (proxy != string.Empty) options.AddArgument("--proxy-server=" + proxy);
                    try
                    {
                        if (config.NVisits > 0)
                        {
                            while (visit < config.NVisits)
                            {
                                ++visit;
                                IWebDriver Driver = new ChromeDriver(chromedriver_path, options);
                                Driver.Navigate().GoToUrl(url);
                                Driver.Dispose();
                            }
                        }
                        else Console.WriteLine("Check Config file! Some value is missing. A Sample.json was printed."); ConfigVisitsGenerator.SampleJsonPrinter();
                    }
                    catch { }
                }
            }
            else Console.WriteLine("Config file is missing! A Sample.json was printed."); ConfigVisitsGenerator.SampleJsonPrinter();
        }
    }    
}
