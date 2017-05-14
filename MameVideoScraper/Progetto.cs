using System;
using System.Net;
using System.Windows.Forms;

namespace MameVideoScraper
{
    class Progetto
    {
        public static void downloadFile(string url, string dest)
        {
            using (var client = new WebClient())
            {
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                client.DownloadFile(url, dest);
            }
        }
    }
}
