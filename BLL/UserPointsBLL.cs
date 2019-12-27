using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UserPointsBLL
    {
        public DataTable GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DAL.UserPointsDal userPointsDal = new DAL.UserPointsDal();
            return userPointsDal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        public int GetRecordCount(string strWhere)
        {
            DAL.UserPointsDal userPointsDal = new DAL.UserPointsDal();
            return userPointsDal.GetRecordCount(strWhere);
        }
    }
}
