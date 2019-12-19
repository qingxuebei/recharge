using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data;
using Model;
using System.Reflection;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.IO;

namespace MyData
{
    public class Utils
    {
        #region 将字符串转换为数组
        public static string[] GetStrArray(string str)
        {
            return str.Split(new char[',']);
        }
        #endregion

        #region 将数组转换为字符串
        public static string GetArrayStr(List<string> list, string speater)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i]);
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(speater);
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 删除最后结尾的一个逗号
        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        public static string DelLastComma(string str)
        {
            return str.Substring(0, str.LastIndexOf(","));
        }
        #endregion

        #region 删除最后结尾的指定字符后的字符
        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar)
        {
            if (str.LastIndexOf(strchar) >= 0)
                return str.Substring(0, str.LastIndexOf(strchar));
            else
                return str;
        }
        #endregion

        #region 生成指定长度的字符串
        /// <summary>
        /// 生成指定长度的字符串,即生成strLong个str字符串
        /// </summary>
        /// <param name="strLong">生成的长度</param>
        /// <param name="str">以str生成字符串</param>
        /// <returns></returns>
        public static string StringOfChar(int strLong, string str)
        {
            string ReturnStr = "";
            for (int i = 0; i < strLong; i++)
            {
                ReturnStr += str;
            }

            return ReturnStr;
        }
        #endregion

        #region 生成日期随机码
        /// <summary>
        /// 生成日期随机码
        /// </summary>
        /// <returns></returns>
        public static string GetRamCode()
        {
            lock (new object())
            {
                Random ran = new Random();
                return DateTime.Now.ToString("yyyyMMddHHmmssffff") + ran.Next(1000, 9999).ToString();
            }
        }
        #endregion

        #region 截取字符长度
        /// <summary>
        /// 截取字符长度
        /// </summary>
        /// <param name="inputString">字符</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string CutString(string inputString, int len)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                {
                    tempLen += 2;
                }
                else
                {
                    tempLen += 1;
                }

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > len)
                    break;
            }
            //如果截过则加上半个省略号 
            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (mybyte.Length > len)
                tempString += "…";
            return tempString;
        }
        #endregion

        #region 清除HTML标记
        public static string DropHTML(string Htmlstring)
        {
            //删除脚本  
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML  
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
        #endregion

        #region 清除HTML标记且返回相应的长度
        public static string DropHTML(string Htmlstring, int strLen)
        {
            return CutString(DropHTML(Htmlstring), strLen);
        }
        #endregion

        #region TXT代码转换成HTML格式
        /// <summary>
        /// 字符串字符处理
        /// </summary>
        /// <param name="chr">等待处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        /// //把TXT代码转换成HTML格式
        public static String ToHtml(string Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("&", "&amp;");
            sb.Replace("<", "&lt;");
            sb.Replace(">", "&gt;");
            sb.Replace("\r\n", "<br />");
            sb.Replace("\n", "<br />");
            sb.Replace("\t", " ");
            //sb.Replace(" ", "&nbsp;");
            return sb.ToString();
        }
        #endregion

        #region HTML代码转换成TXT格式
        /// <summary>
        /// 字符串字符处理
        /// </summary>
        /// <param name="chr">等待处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        /// //把HTML代码转换成TXT格式
        public static String ToTxt(String Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("&nbsp;", " ");
            sb.Replace("<br>", "\r\n");
            sb.Replace("<br>", "\n");
            sb.Replace("<br />", "\n");
            sb.Replace("<br />", "\r\n");
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            sb.Replace("&amp;", "&");
            return sb.ToString();
        }
        #endregion

        #region 检查危险字符
        /// <summary>
        /// 检查危险字符
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string Filter(string sInput)
        {
            if (sInput == null || sInput == "")
                return null;
            string sInput1 = sInput.ToLower();
            string output = sInput;
            string pattern = @"*|and|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(|'";
            if (Regex.Match(sInput1, Regex.Escape(pattern), RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
            {
                throw new Exception("字符串中含有非法字符!");
            }
            else
            {
                output = output.Replace("'", "''");
            }
            return output;
        }

        /// 过滤SQL字符。
        /// </summary>
        /// <param name="str">要过滤SQL字符的字符串。</param>
        /// <returns>已过滤掉SQL字符的字符串。</returns>
        public static string ReplaceSQLChar(string str)
        {
            if (str == String.Empty)
                return String.Empty;
            str = str.Replace("'", "‘");
            str = str.Replace(";", "；");
            str = str.Replace(",", ",");
            str = str.Replace("?", "?");
            str = str.Replace("<", "＜");
            str = str.Replace(">", "＞");
            str = str.Replace("(", "(");
            str = str.Replace(")", ")");
            str = str.Replace("@", "＠");
            str = str.Replace("=", "＝");
            str = str.Replace("+", "＋");
            str = str.Replace("*", "＊");
            str = str.Replace("&", "＆");
            str = str.Replace("#", "＃");
            str = str.Replace("%", "％");
            str = str.Replace("$", "￥");

            return str;
        }
        #endregion

        #region 检查过滤设定的危险字符
        /// <summary> 
        /// 检查过滤设定的危险字符
        /// </summary> 
        /// <param name="InText">要过滤的字符串 </param> 
        /// <returns>如果参数存在不安全字符，则返回true </returns> 
        public static bool SqlFilter(string word, string InText)
        {
            if (InText == null)
                return false;
            foreach (string i in word.Split('|'))
            {
                if ((InText.ToLower().IndexOf(i + " ") > -1) || (InText.ToLower().IndexOf(" " + i) > -1))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 过滤特殊字符
        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string Htmls(string Input)
        {
            if (Input != string.Empty && Input != null)
            {
                string ihtml = Input.ToLower();
                ihtml = ihtml.Replace("<script", "&lt;script");
                ihtml = ihtml.Replace("script>", "script&gt;");
                ihtml = ihtml.Replace("<%", "&lt;%");
                ihtml = ihtml.Replace("%>", "%&gt;");
                ihtml = ihtml.Replace("<$", "&lt;$");
                ihtml = ihtml.Replace("$>", "$&gt;");
                return ihtml;
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        #region 获得配置文件节点XML文件的绝对路径
        public static string GetXmlMapPath(string xmlName)
        {
            return GetMapPath(ConfigurationManager.AppSettings[xmlName].ToString());
        }
        #endregion

        #region 获得当前绝对路径
        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
        #endregion

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string key, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();

            return "";
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName, string key)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
                return HttpContext.Current.Request.Cookies[strName][key].ToString();

            return "";
        }


        #region EasyUI
        public static string EasuuiComboxJson(DataTable ds)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(ds);
        }
        public static string EasyuiDataGridJson(DataTable ds)
        {
            int num = ds.Rows.Count;
            return "{ \"total\":" + num.ToString() + ",\"rows\":" + Newtonsoft.Json.JsonConvert.SerializeObject(ds) + "}";
        }
        public static string EasyuiDataGridJsonForList<T>(List<T> list)
        {
            int num = list.Count;
            return "{ \"total\":" + num.ToString() + ",\"rows\":" + Newtonsoft.Json.JsonConvert.SerializeObject(list) + "}";
        }
        public static string EasyuiDataGridJson(DataTable dt, int total)
        {
            return "{ \"total\":" + total.ToString() + ",\"rows\":" + Newtonsoft.Json.JsonConvert.SerializeObject(dt) + "}";
        }
        public static string EasyuiDataGridFooter(DataTable ds, string total, string footer)
        {
            return "{ \"total\":" + total + ",\"rows\":" + Newtonsoft.Json.JsonConvert.SerializeObject(ds) + ",\"footer\":[" + footer.Replace("'", "\"") + "]}";
        }

        #endregion

        public static DataTable PageDataTable(DataTable dt, int rows, int page)
        {
            dt.Columns.Add("ZTS");
            dt.Columns.Add("TS");
            dt.Columns.Add("ZYS");
            dt.Columns.Add("DQY");
            int zts = 0, zys = 1;
            if (dt.Rows.Count > 0)
            {
                zts = dt.Rows.Count;
                zys = zts % rows == 0 ? zts / rows : zts / rows + 1;
                int qtiaoshu = (page - 1) * rows;

                for (int i = 0; i < qtiaoshu; i++)
                {
                    dt.Rows.RemoveAt(0);
                }

                for (;;)
                {
                    if (dt.Rows.Count <= rows)
                        break;
                    dt.Rows.RemoveAt(rows);
                }
                dt.Rows[0]["ZTS"] = zts.ToString();
                dt.Rows[0]["TS"] = rows.ToString();
                dt.Rows[0]["ZYS"] = zys.ToString();
                dt.Rows[0]["DQY"] = page.ToString();
            }
            return dt;
        }
        public static DateTime DateTime_Week(DateTime dTime, DayOfWeek dWeek)
        {
            int i, j;
            switch (dTime.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    i = 5;
                    break;
                case DayOfWeek.Monday:
                    i = 1;
                    break;
                case DayOfWeek.Saturday:
                    i = 6;
                    break;
                case DayOfWeek.Sunday:
                    i = 7;
                    break;
                case DayOfWeek.Thursday:
                    i = 4;
                    break;
                case DayOfWeek.Tuesday:
                    i = 2;
                    break;
                case DayOfWeek.Wednesday:
                    i = 3;
                    break;
                default:
                    i = 0;
                    break;
            }
            switch (dWeek)
            {
                case DayOfWeek.Friday:
                    j = 5;
                    break;
                case DayOfWeek.Monday:
                    j = 1;
                    break;
                case DayOfWeek.Saturday:
                    j = 6;
                    break;
                case DayOfWeek.Sunday:
                    j = 7;
                    break;
                case DayOfWeek.Thursday:
                    j = 4;
                    break;
                case DayOfWeek.Tuesday:
                    j = 2;
                    break;
                case DayOfWeek.Wednesday:
                    j = 3;
                    break;
                default:
                    j = 0;
                    break;
            }
            return dTime.AddDays(j - i);
        }

        /// <summary>
        /// 合并结构相同DataTable
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static DataTable UnDataTable(DataTable dt1, DataTable dt2)
        {
            object[] obj = new object[dt1.Columns.Count];
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                dt2.Rows[i].ItemArray.CopyTo(obj, 0);
                dt1.Rows.Add(obj);
            }
            return dt1;
        }
        /// <summary>
        /// 合并不同表结构(表数据必须为升序排列)
        /// </summary>
        /// <param name="LikeColumnCount">相同列数（必须在前）</param>
        /// <param name="listdt">数据表list</param>
        /// <returns></returns>
        public static DataTable UnDataNotTable(int LikeColumnCount, List<DataTable> listdt)
        {
            DataTable dtAll = listdt[0].Copy();//合并后的总表，先设置为第一个表
            for (int i = 1; i < listdt.Count; i++)
            {
                DataTable dtl = listdt[i];//被合并表
                string str = DataTableColumnStr(dtAll);//获取所有字段名的字符串
                for (int j = LikeColumnCount; j < dtl.Columns.Count; j++)
                {//增加列--判断列是否存在，如果不存在再增加
                    if (str.IndexOf("," + dtl.Columns[j].ColumnName + ",") < 0)
                    {
                        dtAll.Columns.Add(dtl.Columns[j].ColumnName, dtl.Columns[j].DataType);
                    }
                }

                DataTable dtLs = dtAll.Clone();//临时表//复制表结构
                int jj = 0;//主表索引
                if (dtAll.Rows.Count > 0)
                {
                    for (int t = 0; t < dtl.Rows.Count; t++, jj++)
                    {
                        DataRow dr = dtAll.Rows[jj];
                        DataRow dr1 = dtl.Rows[t];
                        DataRow drNew = dtLs.NewRow();
                        int big = 0;

                        for (int k = 0; k < LikeColumnCount; k++)
                        {//判断相同列的值是否都相同
                            big = String.Compare(dr[k].ToString().Replace("长", "厂"), dr1[k].ToString().Replace("长", "厂"));
                            if (big != 0)//如果有不相同，停止判断
                            {
                                break;
                            }
                        }
                        if (big == 0)//值相同
                        {
                            for (int f = 0; f < dtAll.Columns.Count; f++)
                            {//复制总表当前行数据
                                drNew[dtAll.Columns[f].ColumnName] = dr[f];
                            }
                            for (int f = 0; f < dtl.Columns.Count; f++)
                            {//复制被合并表当前行数据
                                drNew[dtl.Columns[f].ColumnName] = dr1[f];
                            }
                        }
                        else if (big < 0)//总表的值小
                        {
                            for (int f = 0; f < dtAll.Columns.Count; f++)
                            {//复制总表当前行数据
                                drNew[dtAll.Columns[f].ColumnName] = dr[f];
                            }
                            t--;//保持被合并表索引不变
                        }
                        else if (big > 0)
                        {
                            for (int f = 0; f < dtl.Columns.Count; f++)
                            {//复制被合并表当前行数据
                                drNew[dtl.Columns[f].ColumnName] = dr1[f];
                            }
                            jj--;//保持总表索引不变
                        }
                        dtLs.Rows.Add(drNew);

                        if (jj == dtAll.Rows.Count - 1)//如果总表数据比较完毕
                        {
                            for (int f = t + 1; f < dtl.Rows.Count; f++)
                            {//复制被合并表余下所有数据
                                drNew = dtLs.NewRow();
                                for (int l = 0; l < dtl.Columns.Count; l++)
                                {
                                    drNew[dtl.Columns[l].ColumnName] = dtl.Rows[f][l];
                                }
                                dtLs.Rows.Add(drNew);
                            }
                        }
                    }
                }
                else
                {
                    for (int f = 0; f < dtl.Rows.Count; f++)
                    {//复制被合并表余下所有数据
                        DataRow drNew = dtLs.NewRow();
                        for (int l = 0; l < dtl.Columns.Count; l++)
                        {
                            drNew[dtl.Columns[l].ColumnName] = dtl.Rows[f][l];
                        }
                        dtLs.Rows.Add(drNew);
                    }
                }
                if (jj < dtAll.Rows.Count)//被合并表完毕，判断总表数据是否也完毕
                {
                    for (int f = jj; f < dtAll.Rows.Count; f++)
                    {//复制总表余下所有数据
                        DataRow drNew = dtLs.NewRow();
                        for (int l = 0; l < dtAll.Columns.Count; l++)
                        {
                            drNew[dtAll.Columns[l].ColumnName] = dtAll.Rows[f][l];
                        }
                        dtLs.Rows.Add(drNew);
                    }
                }
                dtAll = dtLs.Copy();//复制表

            }
            return dtAll;
        }
        private static string DataTableColumnStr(DataTable dt)
        {
            string str = ",";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                str += dt.Columns[i].ColumnName + ",";
            }
            return str;
        }
        public static string SumDataTableFooter(int LikeColumnCount, DataTable dt)
        {
            string footer = "{'" + dt.Columns[LikeColumnCount - 1].ColumnName + "':'合计'";
            for (int i = LikeColumnCount; i < dt.Columns.Count; i++)
            {
                double hj = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    hj += double.Parse(dt.Rows[j][i].ToString().Trim() == "" ? "0" : dt.Rows[j][i].ToString());
                }
                footer += ",'" + dt.Columns[i].ColumnName + "':'" + hj.ToString() + "'";
            }
            footer += "}";
            return footer;
        }
        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns></returns>
        public static string getNowTimeStamp() { return ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString(); }

        public static Int32 getYearMonth()
        {
            DateTime date = DateTime.Now;
            String month = date.Month.ToString();
            if (month.Length < 2)
            {
                month = "0" + month;
            }
            return Convert.ToInt32(date.Year + month);
        }

        /// <summary>
        /// DataTable to List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IList<T> ConvertToList<T>(DataTable dt) where T : new()
        {

            IList<T> ts = new List<T>();// 定义集合
            Type type = typeof(T); // 获得此模型的类型
            string tempName = "";
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                PropertyInfo[] propertys = t.GetType().GetProperties();// 获得此模型的公共属性
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;
                    if (dt.Columns.Contains(tempName))
                    {
                        if (!pi.CanWrite) continue;
                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
        /// <summary>  
        /// 通过DataRow 填充实体  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="dr"></param>  
        /// <returns></returns>  
        public static T GetModelByDataRow<T>(System.Data.DataRow dr) where T : new()
        {
            T model = new T();
            foreach (PropertyInfo pInfo in model.GetType().GetProperties())
            {
                object val = getValueByColumnName(dr, pInfo.Name);
                pInfo.SetValue(model, Convert.ChangeType(val, pInfo.PropertyType), null);
            }
            return model;
        }

        //返回DataRow 中对应的列的值。  
        public static object getValueByColumnName(System.Data.DataRow dr, string columnName)
        {
            if (dr.Table.Columns.IndexOf(columnName) >= 0)
            {
                if (dr[columnName] == DBNull.Value)
                    return null;
                return dr[columnName];
            }
            return null;
        }
        /// <summary>
        /// 获得当前月的第一天的日期
        /// </summary>
        /// <returns></returns>
        public static DateTime getMonthFirstDay()
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            return d1;
        }
        /// <summary>
        /// 获得当前月上一个月的年月（例：201907）
        /// </summary>
        /// <returns></returns>
        public static int getLastYearMonth()
        {
            DateTime date = DateTime.Now.AddMonths(-1);
            String month = date.Month.ToString();
            if (month.Length < 2)
            {
                month = "0" + month;
            }
            return Convert.ToInt32(date.Year + month);
        }

        /// <summary>
        /// 获得MD5Hash
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o)
        {
            string json = JsonConvert.SerializeObject(o);
            return json;
        }
        
        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                StringReader sr = new StringReader(json);
                object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
                
                T t = o as T;
                return t;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }
    }
}
