using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public string Get(string signature, string timestamp, string nonce, string echostr)
        {
            Model.Logs logs = new Model.Logs();
            BLL.LogsBLL logBLL = new BLL.LogsBLL();
            
            logs.LogText = "signature=" + signature + ",timestamp=" + timestamp + ",nonce" + nonce + ",echostr" + echostr;

            logBLL.Insert(logs);

            string token = "qingxuebei";
            string[] temp1 = { token, timestamp, nonce };
            //字典序排列
            Array.Sort(temp1);
            string temp2 = string.Join("", temp1);

            string temp3 = FormsAuthentication.HashPasswordForStoringInConfigFile(temp2, "SHA1");

            logs.LogText = "temp3=" + temp3;
            logBLL = new BLL.LogsBLL();
            logBLL.Insert(logs);
            if (temp3.ToLower().Equals(signature))
            {
                return echostr;
            }

            return "";
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
