﻿using MyData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrdersDal
    {
        public bool Insert(Model.Orders orders)
        {
            String sql = @"INSERT INTO [dbo].[Orders]
                           ([orderId]
                           ,[pulsa_code]
                           ,[cn_pulsatype]
                           ,[masaaktif]
                           ,[cn_quatity]
                           ,[cn_op]
                           ,[cn_price]
                           ,[cn_oldprice]
                           ,[OperatorName])
                     VALUES ('{0}','{1}','{2}','{3}',{4},'{5}',{6},{7},'{8}');";
            sql = String.Format(sql, orders.OrderId, orders.PulsaCode, orders.CnPulsatype, orders.Masaaktif, orders.CnQuatity,
                orders.CnOp, orders.CnPrice, orders.CnOldprice, orders.OperatorName);

            return MyData.DataBase.Base_cmd(sql);

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
                strSql.Append("order by T.CreateTime desc");
            }
            strSql.Append(")AS Row, T.*  from Orders T ");
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
            return DataBase.Base_count("Orders", strWhere);
        }
    }
}
