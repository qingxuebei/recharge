using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LogsDal
    {
        public bool Insert(Model.Logs logs)
        {
            String sql = @"INSERT INTO [dbo].[Logs]
                           ([ID]
                          ,[LogText]
                          ,[CreateTime])
                     VALUES ('{0}','{1}','{2}');";
            sql = String.Format(sql, System.Guid.NewGuid().ToString(), logs.LogText, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            MyData.DataBase.Base_cmd(sql);
            return true;
        }
    }
}
