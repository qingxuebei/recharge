using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SysUserBLL
    {
        public int getCount(string username, string password)
        {
            return new DAL.SysUserDal().getCount(username, password);
        }
        public bool editPwd(String username, string password)
        {
            return new DAL.SysUserDal().editPwd(username, password);
        }
    }
}
