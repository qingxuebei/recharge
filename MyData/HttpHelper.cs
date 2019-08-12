using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyData
{
    public class HttpHelper
    {
        public static string plusaUrlClass()
        {
            return ConfigurationManager.AppSettings["plusaUrl"];
        }
        public String getPulsaResult(String json)
        {
            string url = HttpHelper.plusaUrlClass();
            var request = (HttpWebRequest)WebRequest.Create(url);
            var postData = json;
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.Timeout = 30000;


            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            return responseFromServer;
        }
    }
}
