using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MIRI.Helpers
{
    public class Downloader
    {
        private static readonly Lazy<Downloader> downloader = new Lazy<Downloader>(() => new Downloader());

        public static Downloader Instance
        {
            get
            {
                return downloader.Value;
            }
        }

        private Downloader()
        {
            _webClient = new WebClient();

            UserAgentUpdate += Downloader_UserAgentUpdate;
        }

        string _userAgent;
        WebClient _webClient;

        public string UserAgent
        {
            get
            {
                return _userAgent;
            }
            set
            {
                _userAgent = value;
                UserAgentUpdate(this, UserAgent);                
            }
        }

        public byte[] UploadValues(Uri uri, System.Collections.Specialized.NameValueCollection parameters)
        {
            byte[] response = null;
            int tries = 0;

            while (tries++ < 5)
            {
                try
                {
                    while (_webClient.IsBusy)
                    {
                        System.Threading.Thread.Sleep(300);
                    }

                    _webClient.Headers[HttpRequestHeader.Referer] = uri.ToString();
                    _webClient.Headers[HttpRequestHeader.Host] = uri.Host;
                    response = _webClient.UploadValues(uri, parameters);

                    break;
                }
                catch (WebException)
                {
                    // Log exception and retry.

                }
            }

            return response;
        }

        public string DownloadString(Uri uri)
        {
            string downloadedString = string.Empty;
            int tries = 0;

            while (tries++ < 5)
            {
                try
                {
                    while (_webClient.IsBusy)
                    {
                        System.Threading.Thread.Sleep(300);
                    }

                    downloadedString = _webClient.DownloadString(uri);

                    break;
                }
                catch (WebException)
                {
                    // Log exception and retry.

                }
            }

            return downloadedString;
        }

        void Downloader_UserAgentUpdate(object sender, string e)
        {
            _webClient.Headers[HttpRequestHeader.UserAgent] = _userAgent;
        }

        event EventHandler<string> UserAgentUpdate;

    }
}
