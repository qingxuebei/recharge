using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AllTypesDal
    {
        public List<Model.AllTypes> getListByQuery(string query)
        {
            string where = " where 1=1 ";
            String sql = @"SELECT [ID]
                                  ,[Types]
                                  ,[Channel]
                                  ,[OperatorsId]
                                  ,[OperatorsName]
                              FROM [recharge].[dbo].[AllTypes] with(nolock)";
            where += query;
            sql += where;
            return MyData.DataBase.Base_list<Model.AllTypes>(sql);
        }
    }
}
