using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class PulsaApiController : ApiController
    {
        // GET: api/PulsaApi
        [HttpGet]
        public string Get()
        {
            Model.PulsaPerfixProductShow pulsaPerfixProductShow = new Model.PulsaPerfixProductShow();
            //获取所有手机号运营商
            BLL.PulsaPrefixBLL pulsaPrefixBLL = new BLL.PulsaPrefixBLL();
            pulsaPerfixProductShow.pulsaPerfixList = pulsaPrefixBLL.getList();

            //获取所有运营商套餐
            BLL.PulsaProductBLL pulsaProductBLL = new BLL.PulsaProductBLL();
            pulsaPerfixProductShow.pulsaProductShowList = pulsaProductBLL.getNormalShowList();

            return JsonConvert.SerializeObject(pulsaPerfixProductShow);
        }

        // GET: api/PulsaApi/5
        public string Get(int id)
        {
            return "";
        }

        // POST: api/PulsaApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/PulsaApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PulsaApi/5
        public void Delete(int id)
        {
        }
        public string Options()
        {
            return null; // HTTP 200 response with empty body
        }
    }
}
