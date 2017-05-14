using System.Linq;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;
using System.Net;
using System.IO;
using System.Reflection;
using System;
using MameVideoScraper.Properties;

namespace MameVideoScraper
{
    public class MameVideoSnaps : IGameMenuItemPlugin
    {
        public static string getplataforma { get; set; }
        public static string getgame { get; set; }
        public static string progetto = "http://www.progettosnaps.net/videosnaps/mp4";
        public bool SupportsMultipleGames
        {
            get { return true; }
        }

        public string Caption
        {
            get { return "Mame Snaps Scraper"; }
        }

        public System.Drawing.Image IconImage
        {
            get { return Resources.mame; }
        }

        public bool ShowInLaunchBox
        {
            get { return true; }
        }

        public bool ShowInBigBox
        {
            get { return false; }
        }

        public static object Configs { get; private set; }

        public bool GetIsValidForGame(IGame selectedGame)
        {
            return !string.IsNullOrEmpty(selectedGame.ApplicationPath);
        }

        public bool GetIsValidForGames(IGame[] selectedGames)
        {
            return selectedGames.Any(g => !string.IsNullOrEmpty(g.Platform));
        }

        public void OnSelected(IGame selectedGame)
        {
            string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            getplataforma = selectedGame.Platform;
            getgame = selectedGame.Title;
            WebClient webClient = new WebClient();
            string fullpath = selectedGame.ApplicationPath;
            var romfile = Path.GetFileNameWithoutExtension(fullpath);
            string dest = Path.Combine(path, "videos", getplataforma);
            var client = new WebClient();
            string url = progetto + "/" + romfile.ToLower() + ".mp4";
            Progetto.downloadFile(url, dest + @"\" + romfile + ".mp4");
        }

        public void OnSelected(IGame[] selectedGames)
        {
            foreach (var item in selectedGames)
            {
                string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                getplataforma = item.Platform;
                getgame = item.Title;
                WebClient webClient = new WebClient();
                string fullpath = item.ApplicationPath;
                var romfile = Path.GetFileNameWithoutExtension(fullpath);
                string dest = Path.Combine(path, "videos", getplataforma);
                var client = new WebClient();
                string url = progetto + "/" + romfile.ToLower() + ".mp4";
                Progetto.downloadFile(url, dest + @"\" + romfile + ".mp4");
            }
            MessageBox.Show("Downloaded");
        }
    }
}