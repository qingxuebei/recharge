using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.details
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCallback)
            {
               
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            #region 价目表
            String pulsa_channel = txt_channel.Text.Trim();
            String url = "https://api.mobilepulsa.net/v1/legacy/index/"+txt_type.Text.Trim()+"/" + pulsa_channel;
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

            String ss = getResult(url, json);

            MyData.PlusaPriceList pulsa = new MyData.PlusaPriceList();
            pulsa = MyData.Utils.DeserializeJsonToObject<MyData.PlusaPriceList>(ss);

            BLL.PulsaProductBLL pulsaBLL = new BLL.PulsaProductBLL();



            List<Model.PulsaProduct> pulsaProductList = new List<Model.PulsaProduct>();
            foreach (var pulsa1 in pulsa.data)
            {
                Model.PulsaProduct pulsaProduct = new Model.PulsaProduct();
                pulsaProduct.cn_price = 0;
                pulsaProduct.cn_op = "";
                pulsaProduct.cn_status = 0;
                pulsaProduct.masaaktif = pulsa1.masaaktif;
                pulsaProduct.pulsa_code = pulsa1.pulsa_code;
                pulsaProduct.pulsa_nominal = pulsa1.pulsa_nominal;
                pulsaProduct.pulsa_op = pulsa1.pulsa_op;
                pulsaProduct.pulsa_price = pulsa1.pulsa_price;
                pulsaProduct.pulsa_type = pulsa1.pulsa_type;
                pulsaProduct.status = pulsa1.status;
                pulsaProduct.cn_quatity = "";
                pulsaProduct.pulsa_channel = pulsa_channel;
                pulsaProduct.cn_oldprice = decimal.Round(pulsa1.pulsa_price / 2000 + 2, 1);


                pulsaProductList.Add(pulsaProduct);
            }

            OleDbConnection conn = MyData.DataBase.Conn();
            conn.Open();
            OleDbTransaction tr = conn.BeginTransaction();
            try
            {
                pulsaBLL.Insert(pulsaProductList, tr);
                tr.Commit();
            }
            catch (Exception ex)
            {
                tr.Rollback();
            }
            conn.Close();
            Label1.Text = ss;
            #endregion
        }
    }
}