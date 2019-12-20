using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class WeChatUserBLL
    {
        public void Insert(Model.WeChatUser weChatUser)
        {
            DAL.WeChatUserDal weChatUserDal = new DAL.WeChatUserDal();
            weChatUserDal.Insert(weChatUser);
        }
        public DataTable GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DAL.WeChatUserDal weChatUserDal = new DAL.WeChatUserDal();
            return weChatUserDal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        public int GetRecordCount(string strWhere)
        {
            DAL.WeChatUserDal weChatUserDal = new DAL.WeChatUserDal();
            return weChatUserDal.GetRecordCount(strWhere);
        }
    }
}
