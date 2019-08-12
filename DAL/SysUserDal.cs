using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SysUserDal
    {
        public int getCount(String username, string password)
        {
            return MyData.DataBase.Base_count("SysUser", " Username='" + username + "' and Password='" + password + "' and State=1");
        }
        public bool editPwd(String username, string password)
        {
            return MyData.DataBase.Base_cmd("update SysUser set Password='" + password + "' where Username='" + username + "'");
        }
    }
}
