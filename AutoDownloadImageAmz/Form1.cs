using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoDownloadImageAmz
{
    public partial class Form1 : Form
    {
        volatile bool bRunning = false;
        volatile int __checked = 0;
        volatile int __currentLnk = 1;

        public Form1()
        {
            InitializeComponent();

            textBoxLink.Text = "link.txt";
            textBoxDownloadFoler.Text = "Download_Img";
            
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            string link = textBoxLink.Text.Trim();
            string downloadFoler = textBoxDownloadFoler.Text.Trim();
            int maxImage = (int)numericUpDownMaxImage.Value; 

            if ("".Equals(textBoxLink.Text.Trim()))
            {
                MessageBox.Show("Error -> Link is invalid!");
                return;
            }

            List<string> links = GetLinksFromFile(link);

            if ("".Equals(textBoxDownloadFoler.Text.Trim()))
            {
                MessageBox.Show("Error -> downloadFoler is invalid!");
                return;
            }


            Thread t = new Thread(() =>
            {
                SetbtnStart(true);
                __checked = 0;
                bRunning = true;

                Parallel.ForEach(links, new ParallelOptions() { MaxDegreeOfParallelism = (int)numericUpDownThread.Value }, (item, loopState) =>
                {

                    int __downloaded = 0;
                    if (!bRunning || __downloaded >= maxImage)
                        loopState.Break();

                    var chromeDriver = StartProGram();
                    WebClient client = new WebClient();

                    string sFolder = this.textBoxDownloadFoler.Text + "\\Link_" + (__currentLnk++).ToString();
                    Directory.CreateDirectory(sFolder);

                    DownloadImgae(1, chromeDriver, client, item, maxImage, __downloaded, sFolder);
                    Thread.Sleep(200);

                    __checked = __checked + 1;
                    SetLabelStatus("Status: Running -> " + links.Count + " | " + "Checked =>>" + __checked + " / " + links.Count);
                });

                SetLabelStatus("Status: Imported -> " + links.Count + " | " + "Checked =>>" + links.Count + " / " + links.Count);
                SetbtnStart(true);
                MessageBox.Show("Completed !", "Done");
            })
            {
                IsBackground = true
            };
            t.Start();


        }

        ChromeDriver StartProGram()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArguments("--disable-notifications");
            options.AddArguments("disable-infobars");
            options.AddExcludedArgument("enable-automation");

            ChromeDriver chromeDriver = new ChromeDriver(chromeDriverService, options);
            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            return chromeDriver;
        }

        IWebElement FindElementByXpath(ChromeDriver driver, String sXpath, int timmeout = 5)
        {
            IWebElement element = null;

            try
            {
                element = driver.FindElement(By.XPath(sXpath));
            }
            catch (Exception e)
            {
                Console.WriteLine("Canot find element {0}", sXpath);
                element = null;
            }

            return element;
        }

        private void DownloadImgae(int index, ChromeDriver chromeDriver, WebClient client, string link, int maxImage, int __downloaded, string folder)
        {
            int i = 0;
            IWebElement iWebEle;
            try
            {
                try
                {
                    chromeDriver.Navigate().GoToUrl(link);

                    if (!bRunning || __downloaded >= maxImage)
                    {
                        chromeDriver.Quit();
                        return;
                    }

                    List<string> lsLinkItem = new List<string>();
                    IList<IWebElement> lsItem = chromeDriver.FindElements(By.XPath("//div//h2//a"));
                    foreach (IWebElement item in lsItem)
                    {
                        string sLink = item.GetAttribute("href");
                        if (sLink != null && sLink.Length != 0)
                        {
                            lsLinkItem.Add(sLink);
                        }
                    }

                    foreach (string sLinkItem in lsLinkItem)
                    {
                        chromeDriver.Navigate().GoToUrl(sLinkItem);
                        iWebEle = chromeDriver.FindElement(By.XPath("//li[contains(@class,'image item itemNo0 maintain-height')]//span//span//div//img"));
                        string path = iWebEle.GetAttribute("src");

                        if (path != null && path.Length != 0)
                        {
                            string sFileName;
                            string filename = chromeDriver.FindElement(By.Id("productTitle")).Text;
                            sFileName = folder + "\\" + filename + Path.GetExtension(path);
                            
                            DownloadImgaeByLink(chromeDriver, client, path, sFileName);
                            __downloaded++;

                            if (!bRunning || __downloaded >= maxImage)
                            {
                                chromeDriver.Quit();
                                return;
                            }
                        }
                    }    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Open Chrome Browser\nError: " + ex.Message, "Error");
                }

                try
                {
                    chromeDriver.Navigate().GoToUrl(link);
                    iWebEle = chromeDriver.FindElement(By.XPath("//li//a[contains(text(),'Next')]"));
                    string sNextPage = iWebEle.GetAttribute("href");
                    DownloadImgae(index++, chromeDriver, client, sNextPage, maxImage, __downloaded, folder);
                }
                catch { }
            }
            catch {
            }

            chromeDriver.Quit();
        }

        private void DownloadImgaeByLink(ChromeDriver chrome, WebClient client, string link, string sFilePath)
        {
             client.DownloadFileAsync(new Uri(link), sFilePath);
        }

        private void DownloadImgaeFor1To400(ChromeDriver chromeDriver, string link, int index)
        {
            chromeDriver.Navigate().GoToUrl(link + "page=" + index);
        }

        private List<string> GetLinksFromFile(string link)
        {
            List<string> links = new List<string>();

            try
            {
                string sLink = System.IO.Directory.GetCurrentDirectory() + "\\" + link.Trim();
                var fileText = File.ReadAllText(sLink);

                if (!fileText.Trim().Equals(""))
                {
                    links = fileText.Trim().Split('\n').ToList<string>();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error read Info file Link: " + ex.Message, "Error");
            }

            return links;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            bRunning = false;
        }


        delegate void SetTextCallback(string text);
        private void SetLabelStatus(string text)
        {
            if (this.labelStatus.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetLabelStatus);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelStatus.Text = text;
            }
        }

        delegate void SetBoolCallback(bool b);
        private void SetbtnStart(bool b)
        {
            if (this.buttonStart.InvokeRequired)
            {
                SetBoolCallback d = new SetBoolCallback(SetbtnStart);
                this.Invoke(d, new object[] { b });
            }
            else
            {
                this.buttonStart.Enabled = b;
            }
        }

        private void textBoxDownloadFoler_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
