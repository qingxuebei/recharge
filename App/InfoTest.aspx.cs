using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App
{
    public partial class InfoTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 查询余额
                //String url = "https://api.mobilepulsa.net/v1/legacy/index";
                //string username = "082310565157";
                //string password = "7315d43e48d40855731";
                //string signature = "";

                //using (MD5 md5Hash = MD5.Create())
                //{
                //    signature = GetMd5Hash(md5Hash, username + password + "bl");
                //}


                //string json = @"{
                //    ""commands"" : ""balance"",
                //    ""username"" : """ + username + @""",
                //    ""sign""     : """ + signature + @"""
                //    }";


                //Label1.Text = getResult(url, json);

                #endregion

                #region 价目表
                String url = "https://api.mobilepulsa.net/v1/legacy/index/data/telkomsel_paket_internet";
                string username = "082310565157";
                string password = "7315d43e48d40855731";
                string signature = "";

                using (MD5 md5Hash = MD5.Create())
                {
                    signature = GetMd5Hash(md5Hash, username + password + "pl");
                }

                string json = @"{
                    ""commands"" : ""pricelist"",
                    ""username"" : """ + username + @""",
                    ""sign""     : """ + signature + @""",
                    ""status""   : ""all""
                    }";

                Label1.Text = getResult(url, json);

                #endregion

                #region 充值
                //String url = "https://api.mobilepulsa.net/v1/legacy/index";
                //string username = "082310565157";
                //string password = "7315d43e48d40855731";
                //string signature = "";

                //using (MD5 md5Hash = MD5.Create())
                //{
                //    signature = GetMd5Hash(md5Hash, username + password + "order001");
                //}

                //string json = @"{
                //    ""commands"" : ""topup"",
                //    ""username"" : """ + username + @""",
                //    ""sign""     : """ + signature + @""",
                //    ""hp""   : ""082310565157"",
                //    ""ref_id"" : ""order001"",
                //    ""pulsa_code"":""htelkomsel50000TEL""

                //    }";

                //Label1.Text = getResult(url, json);
                #endregion
            }
        }
        public String getResult(String url, String json)
        {
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

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}