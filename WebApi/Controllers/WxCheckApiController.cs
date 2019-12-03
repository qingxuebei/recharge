using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Security;

namespace WebApi.Controllers
{
    public class WxCheckApiController : ApiController
    {
        // GET: api/WxCheckApi
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/WxCheckApi
        [HttpGet]
        public string GetWx(string signature, string timestamp, string nonce, string echostr)
        {
            Model.Logs logs = new Model.Logs();
            BLL.LogsBLL logBLL = new BLL.LogsBLL();
            logs.LogText = "signature=" + signature + ",timestamp=" + timestamp + ",nonce" + nonce + ",echostr" + echostr;
            logBLL.Insert(logs);

            string token = "weixin";
            var arr = new[] { token, timestamp, nonce }.OrderBy(z => z).ToArray();
            var arrString = string.Join("", arr);
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));
            StringBuilder enText = new StringBuilder();
            foreach (var b in sha1Arr)
            {
                enText.AppendFormat("{0:x2}", b);
            }
            logs.LogText = "temp3=" + enText;
            logBLL = new BLL.LogsBLL();
            logBLL.Insert(logs);

            if (signature != enText.ToString())
            {
                return "参数错误";
            }
            return echostr;
            
        }

        // POST: api/WxCheckApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/WxCheckApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/WxCheckApi/5
        public void Delete(int id)
        {
        }
    }
}
