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
    public class MarketDal
    {
        public bool Insert(Model.Market market)
        {
            String sql = @"INSERT INTO [dbo].[Market]
                           ([ID]
                          ,[IconUrl]
                          ,[Name]
                          ,[SmsUrl]
                          ,[MaxMoney]
                          ,[Tenure]
                          ,[Rate]
                          ,[ApprovlTime]
                          ,[Disbursement]
                          ,[SortId]
                          ,[CreateTime]
                          ,[CreatePerson]
                          ,[UpdateTime]
                          ,[UpdatePerson]
                          ,[State])
                     VALUES ('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}',{9},'{10}','{11}','{12}','{13}',{14});";
            sql = String.Format(sql, market.ID, market.IconUrl, market.Name, market.SmsUrl, market.MaxMoney,
                market.Tenure, market.Rate, market.ApprovlTime,market.Disbursement,
                market.SortId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), market.CreatePerson,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), market.UpdatePerson, market.State);

            MyData.DataBase.Base_cmd(sql);
            return true;
        }

        public bool Update(Model.Market market)
        {
            String sql = @"update Market set IconUrl='{0}',Name='{1}',SmsUrl='{2}',MaxMoney={3},
                                            Tenure='{4}',Rate='{5}',ApprovlTime='{6}',Disbursement='{7}',
                                            SortId={8},UpdateTime='{9}',UpdatePerson='{10}',State={11}
                                            where ID='{12}'";
            sql = String.Format(sql, market.IconUrl, market.Name, market.SmsUrl, market.MaxMoney,
                market.Tenure, market.Rate, market.ApprovlTime, market.Disbursement,market.SortId,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), market.UpdatePerson,market.State,market.ID);
            return DataBase.Base_cmd(sql);

        }

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
            strSql.Append(")AS Row, T.*  from Market T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DataBase.Base_dt(strSql.ToString());
        }

        public int GetRecordCount(string strWhere)
        {
            return DataBase.Base_count("Market", strWhere);
        }
        public List<Model.Market> GetList()
        {
            String sql = "select * from Market where State=1 order by SortId desc;";
            List<Model.Market> marketList = new List<Model.Market>();
            marketList = DataBase.Base_list<Model.Market>(sql);
            return marketList;
        }
    }
}
