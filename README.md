# ChromeBot Console
Automate some operations using Chrome Browser.

Visits Generator can generate traffic into a website (or list of websites) with proxy use option available.
This Version not required user control and can be scheduled using Windows Task Scheduler.
- Added option to run in background all chrome and chromedriver windows.
- Added config file to set options before running program and schedule (UrlListPath, ProxyListPath(not required), NThreads, Nvisits, IsBackground)
- MultiThreads 

This is a Console Application Version of the software.
This version was developed using .NetCore 3.1 (Windows,Linux,MacOS runnable)

I asked ChatGPT to explain my code to a customer:
"Sure, this program is a tool that allows you to generate visits to a website using the Chrome web browser. The program reads a configuration file, which tells it where to find the list of URLs you want to visit and the list of proxies to use. It then uses those lists to automatically open and navigate to the URLs using the Chrome browser. The program also allows you to specify how many concurrent visits you want to make at once, so you can simulate a high amount of traffic on the website. Additionally, this program is written in C#, which is a popular programming language used to develop Windows applications. This tool can be utilized for different purposes such as website testing, website optimization and research, or even for marketing purposes."
