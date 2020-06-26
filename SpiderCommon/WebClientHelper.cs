using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SpiderCommon
{
    public class WebClientHelper
    {

        public static string DownloadHtml(string url, Encoding encode)
        {
            string html = string.Empty;
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    html = webClient.DownloadString(url);
                }
                catch 
                {
                    return "";
                }
                
            
            }
            return html;
        }

        public static string DownloadHtml(string url, Encoding encode, string ssid)
        {
            string html = string.Empty;

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    webClient.Encoding = Encoding.UTF8;
                    //也可以向表头中添加一些其他东西
                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                    webClient.Headers.Add("Accept", "application/json, text/javascript, */*; q=0.01");
                    webClient.Headers.Add("Host", "");
                    webClient.Headers.Add("Origin", "");
                    webClient.Headers.Add("Referer", $"");
                    webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36");

                    html = webClient.DownloadString(url);
                }
                catch 
                {
                    return "";
                }
               

            }

            return html;
        }

        /// <summary>
        /// post 请求 表单方式
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string PostDownloadHtml(string url,string parameter,string ssid)
        {
            string html = string.Empty;
            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                //也可以向表头中添加一些其他东西
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                webClient.Headers.Add("Accept", "application/json, text/javascript, */*; q=0.01");
                webClient.Headers.Add("Host", "www.crpa.cn");
                webClient.Headers.Add("Origin", "9999999");
                webClient.Headers.Add("Referer", $"********");
                webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36");
                //string result = webClient.UploadString(url, "userName=admin&pwd=123456");
                html = webClient.UploadString(url,parameter);
            }
            return html;
        }


    }
}
