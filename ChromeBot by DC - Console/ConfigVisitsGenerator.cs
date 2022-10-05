using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace ChromeBot_by_DC___Console
{
    public class ConfigVisitsGenerator
    {
        public string UrlListPath { get; set; }
        public string ProxyListPath { get; set; }
        public int NThreads { get; set; } = 0;
        public int NVisits { get; set; } = 0;
        public bool IsBackground { get; set; } = false;
        
        public static void SampleJsonPrinter()
        {
            string json = Environment.CurrentDirectory + "\\ConfigVisitsGenerator SAMPLE.json";
            File.Create(json).Close();
            File.AppendAllText(json,
                "{" + Environment.NewLine
                + "\"UrlListPath\": \"\"," + Environment.NewLine                
                + "\"ProxyListPath\": \"\", // this param can be empty" + Environment.NewLine
                + "\"NThreads\": 1," + Environment.NewLine
                + "\"NVisits\": 5," + Environment.NewLine
                + "\"IsBackground\": false," + Environment.NewLine
                + "}"
                + "// Don't forget to rename this file in 'ConfigVisitsGenerator.json' if you want to use it"
                );
        }
        public static ConfigVisitsGenerator ReadJson(string json)
        {
            ConfigVisitsGenerator configurazione = new ConfigVisitsGenerator();
            try
            {
                Console.WriteLine(json);

                using (var webClient = new WebClient())
                {
                    string rawJSON = webClient.DownloadString(json);
                    configurazione = JsonConvert.DeserializeObject<ConfigVisitsGenerator>(rawJSON);
                }
                Console.WriteLine("Json is correct. Example Url List Path: " + configurazione.UrlListPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERR " + ex.Message);
            }
            return configurazione;
        }
    }
}
