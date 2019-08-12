using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PulsaPrefixDal
    {
        public List<Model.PulsaPrefix> getList()
        {
            string sql = @"SELECT [Code]
                          ,[OperatorID]
                          ,[OperatorName]
                          ,[DataChannel]
                          ,[PulsaChannel]
                          ,[LogoUrl]
                      FROM [recharge].[dbo].[PulsaPrefix]";
            return MyData.DataBase.Base_list<Model.PulsaPrefix>(sql);
        }
    }
}
