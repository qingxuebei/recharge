using MyData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PulsaProductDal
    {
        public void Insert(List<Model.PulsaProduct> pulsaProductList, OleDbTransaction tr)
        {
            foreach (Model.PulsaProduct pulsaProduct in pulsaProductList)
            {
                String sql = @"INSERT INTO [dbo].[PulsaProduct]
                           ([pulsa_code]
                           ,[pulsa_op]
                           ,[pulsa_nominal]
                           ,[pulsa_price]
                           ,[pulsa_type]
                           ,[masaaktif]
                           ,[status]
                           ,[cn_op]
                           ,[cn_status]
                           ,[cn_price]
                           ,[create_time]
                           ,[update_time],cn_quatity,pulsa_channel,cn_oldprice)
                     VALUES ('{0}','{1}','{2}',{3},'{4}','{5}','{6}','{7}',{8},{9},'{10}','{11}','{12}','{13}',{14});";
                sql = String.Format(sql, pulsaProduct.pulsa_code, pulsaProduct.pulsa_op, pulsaProduct.pulsa_nominal, pulsaProduct.pulsa_price,
                    pulsaProduct.pulsa_type, pulsaProduct.masaaktif, pulsaProduct.status, pulsaProduct.cn_op, pulsaProduct.cn_status,
                    pulsaProduct.cn_price, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"),
                    pulsaProduct.cn_quatity, pulsaProduct.pulsa_channel, pulsaProduct.cn_oldprice);

                MyData.DataBase.Base_cmd(sql, tr);
            }
        }

        public List<Model.PulsaProduct> getListByQuery(string query)
        {
            string where = " where 1=1 ";
            String sql = @"SELECT [pulsa_code]
                              ,[pulsa_op]
                              ,[pulsa_nominal]
                              ,[pulsa_price]
                              ,[pulsa_type]
                              ,[masaaktif]
                              ,[status]
                              ,[cn_quatity]
                              ,[cn_op]
                              ,[cn_status]
                              ,[cn_price]
                              ,[create_time]
                              ,[update_time]
                              ,[pulsa_channel]
                              ,[cn_oldprice]
                          FROM [recharge].[dbo].[PulsaProduct] with(nolock)";
            where += query;
            sql += where;
            return MyData.DataBase.Base_list<Model.PulsaProduct>(sql);
        }

        public List<Model.PulsaProductShow> getShowListByQuery(string query)
        {
            string where = " where 1=1 ";
            String sql = @"SELECT [pulsa_code]
                              ,[pulsa_op]
                              ,[pulsa_nominal]
                              ,[pulsa_type]
                              ,[masaaktif]
                              ,[status]
                              ,[cn_quatity]
                              ,[cn_op]
                              ,[cn_status]
                              ,[cn_price]
                              ,[pulsa_channel]
                              ,[cn_oldprice]
                          FROM [recharge].[dbo].[PulsaProduct] with(nolock)";
            where += query;
            sql += where;
            return MyData.DataBase.Base_list<Model.PulsaProductShow>(sql);
        }

        public Model.PulsaProduct getFirstById(string pulsaCode)
        {
            String sql = @"SELECT [pulsa_code]
                              ,[pulsa_op]
                              ,[pulsa_nominal]
                              ,[pulsa_price]
                              ,[pulsa_type]
                              ,[masaaktif]
                              ,[status]
                              ,[cn_quatity]
                              ,[cn_op]
                              ,[cn_status]
                              ,[cn_price]
                              ,[create_time]
                              ,[update_time]
                              ,[pulsa_channel]
                              ,[cn_oldprice]
                          FROM [recharge].[dbo].[PulsaProduct] with(nolock) where pulsa_code='" + pulsaCode + "'";

            return MyData.DataBase.Base_getFirst<Model.PulsaProduct>(sql);
        }

        /// <summary>
        /// 分页获取整个列表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="startIndex">起始记录</param>
        /// <param name="endIndex">截止记录</param>
        /// <returns></returns>
        public DataTable GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.SortId desc");
            }
            strSql.Append(")AS Row, T.*  from PulsaProduct T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DataBase.Base_dt(strSql.ToString());
        }

        /// <summary>
        /// 获取条数
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public int GetRecordCount(string strWhere)
        {
            return DataBase.Base_count("PulsaProduct", strWhere);
        }
    }
}
