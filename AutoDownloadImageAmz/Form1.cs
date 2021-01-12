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

        volatile int __downloaded = 0;

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
                __downloaded = 0;
                bRunning = true;

                Parallel.ForEach(links, new ParallelOptions() { MaxDegreeOfParallelism = (int)numericUpDownThread.Value }, (item, loopState) =>
                {
                    if (!bRunning || __downloaded >= maxImage)
                        loopState.Break();

                    DownloadImgae(1, item, maxImage);
                    Thread.Sleep(200);


                    __checked = __checked + 1;
                    SetLabelStatus("Status: Running -> " + links.Count + " | " + "Checked =>>" + __checked + " / " + links.Count);
                });

                SetLabelStatus("Status: Imported -> " + links.Count + " | " + "Checked =>>" + links.Count + " / " + links.Count);
                SetbtnStart(true);
                MessageBox.Show("Completed !", "Done");
            });

            t.IsBackground = true;
            t.Start();


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

        private void DownloadImgae(int index, string link, int maxImage)
        {
            try
            {
                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                //chromeDriverService.HideCommandPromptWindow = true;

                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--start-maximized"); 

                try
                {
                    ChromeDriver chromeDriver = new ChromeDriver(chromeDriverService, options);
                    chromeDriver.Navigate().GoToUrl(link);

                    if (!bRunning || __downloaded >= maxImage)
                        return;

                    // get all item in a page
                    // Get all link
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

                    // Download all image in a item
                    foreach (string sLinkItem in lsLinkItem)
                    {
                        chromeDriver.Navigate().GoToUrl(sLinkItem);
                        // click to download hight 
                        IWebElement iWebImage = chromeDriver.FindElement(By.XPath("//li[contains(@class,'image item itemNo0 maintain-height')]//span//span//div//img"));

                        // get image path
                        string path = iWebImage.GetAttribute("src");

                        // download and save file
                        if (path != null && path.Length != 0)
                        {
                            // detetect file name
                            string sFileName;
                            //IWebElement iWebTitle = chromeDriver.FindElement(By.Id("productTitle")).Text;
                            string filename = chromeDriver.FindElement(By.Id("productTitle")).Text;
                            sFileName =  this.textBoxDownloadFoler.Text + "\\" + filename + Path.GetExtension(path);
                            
                            DownloadImgaeByLink(chromeDriver, path, sFileName);
                        }
                    }    

                    // Next Page======================
                    try
                    {
                        DownloadImgae(index++, link, maxImage);
                    }
                    catch { }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Open Chrome Browser\nError: " + ex.Message, "Error");
                }
            }
            catch { }
        }

        private void DownloadImgaeByLink(ChromeDriver chrome, string link, string sFilePath)
        {
            
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(link), sFilePath);
            }
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
