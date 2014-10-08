using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BingBackgroundChanger
{
    class Program
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(UInt32 uiAction, UInt32 uiParam, String pvParam, UInt32 fWinIni);
        private static UInt32 SPI_SETDESKWALLPAPER = 20;
        private static UInt32 SPIF_UPDATEINIFILE = 0x1;

        public static string filePath;

        public static string imageFileName;

        public static string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

        static void Main(string[] args)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("http://www.bing.com/HPImageArchive.aspx?format=xml&idx=0&n=1&mkt=en-US");

            XmlElement root = doc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("url");
            for (int i = 0; i < elemList.Count; i++)
            {
                Console.WriteLine("http://www.bing.com" + elemList[i].InnerXml);
                filePath = "http://www.bing.com" + elemList[i].InnerXml;
            }

            System.Console.WriteLine("background fetched");

            //Console.ReadKey(true);

            string localFilename = @"C:\\Users\\" + Environment.UserName + "\\Documents\\currentbackground.jpg";

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(filePath, localFilename);
            }

            imageFileName = localFilename;

            Program b = new Program();
            b.changeBackground();
        }

        public void changeBackground()
        {
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, "C:\\Users\\" + Environment.UserName + "\\Documents\\currentbackground.jpg", SPIF_UPDATEINIFILE);
        }

    }
}