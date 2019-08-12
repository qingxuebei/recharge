using System;
using System.Data;
using System.Web;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Diagnostics;
using System.Reflection;
using Model;

namespace MyData
{
    public static class ExcelHelper
    {

        #region  DataTable导出到Excel
        /// <summary>
        /// DataTable导出到Excel(方法一)
        /// </summary>
        /// <param name="pData">DataTable</param>
        /// <param name="pFileName">导出文件名</param>
        /// <param name="pHeader">导出标题以|分割</param>
        public static void DataTableExcel(System.Data.DataTable pData, string pFileName, string pHeader)
        {
            System.Web.UI.WebControls.DataGrid dgExport = null;
            // 当前对话 
            System.Web.HttpContext curContext = System.Web.HttpContext.Current;
            // IO用于导出并返回excel文件 
            System.IO.StringWriter strWriter = null;
            System.Web.UI.HtmlTextWriter htmlWriter = null;
            if (pData != null)
            {
                string UserAgent = curContext.Request.ServerVariables["http_user_agent"].ToLower();
                if (UserAgent.IndexOf("firefox") == -1)//火狐浏览器
                    pFileName = HttpUtility.UrlEncode(pFileName, System.Text.Encoding.UTF8);
                curContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + pFileName + ".xls");
                curContext.Response.ContentType = "application/octet-stream";
                strWriter = new System.IO.StringWriter();
                htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);
                // 为了解决dgData中可能进行了分页的情况，需要重新定义一个无分页的DataGrid 
                dgExport = new System.Web.UI.WebControls.DataGrid();
                dgExport.DataSource = pData.DefaultView;
                dgExport.AllowPaging = false;
                dgExport.ShowHeader = false;//去掉标题
                dgExport.DataBind();
                string[] arrHeader = pHeader.Split('|');
                string strHeader = "<table border=\"1\" style=\"background-color:Gray;font-weight:bold;\"><tr>";
                foreach (string j in arrHeader)
                {
                    strHeader += "<td>" + j.ToString() + "</td>";
                }
                strHeader += "</tr></table>";
                // 返回客户端 
                dgExport.RenderControl(htmlWriter);
                string strMeta = "<meta http-equiv=\"content-type\" content=\"application/ms-excel; charset=UTF-8\"/>";
                curContext.Response.Write(strMeta + strHeader + strWriter.ToString());
                curContext.Response.End();
            }
        }
        public static string dao_name()
        {
            return "数据导出.xls";
        }
        /// <summary>
        /// DataTable导出到Excel(方法二)
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="filepath">导出文件路径</param>
        public static void Excel_daochu(DataTable dt, string filepath)
        {
            try
            {
                FileStream file = new FileStream(filepath + "导出模板.xls", FileMode.Open, FileAccess.Read);

                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                hssfworkbook.SetSheetName(0, "数据");
                ISheet sheet1 = hssfworkbook.GetSheet("数据");
                InsertRows(sheet1, 1, dt.Rows.Count, hssfworkbook, dt.Columns.Count);


                //列标题
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sheet1.GetRow(0).GetCell(i).SetCellValue(dt.Columns[i].ColumnName);
                }
                //写入数据
                for (int j = 1; j <= dt.Rows.Count; j++)
                {
                    //dt.Columns[0].ColumnName;
                    //IRow row1 = sheet1.GetRow(j);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sheet1.GetRow(j).GetCell(i).SetCellValue(dt.Rows[j - 1][i].ToString());
                    }
                }

                sheet1.ForceFormulaRecalculation = true;
                file = new FileStream(filepath + dao_name(), FileMode.Create);
                hssfworkbook.Write(file);
                file.Close();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public static void Excel_daochu(DataTable dt, string filepath, bool isCg)//常规
        {
            try
            {
                string wj = "";
                if (isCg) wj = "常规";
                FileStream file = new FileStream(filepath + "导出模板" + wj + ".xls", FileMode.Open, FileAccess.Read);

                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                hssfworkbook.SetSheetName(0, "数据");
                ISheet sheet1 = hssfworkbook.GetSheet("数据");
                InsertRows(sheet1, 1, dt.Rows.Count, hssfworkbook, dt.Columns.Count);


                //列标题
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sheet1.GetRow(0).GetCell(i).SetCellValue(dt.Columns[i].ColumnName);
                }
                //写入数据
                for (int j = 1; j <= dt.Rows.Count; j++)
                {
                    //dt.Columns[0].ColumnName;
                    //IRow row1 = sheet1.GetRow(j);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sheet1.GetRow(j).GetCell(i).SetCellValue(dt.Rows[j - 1][i].ToString());
                    }
                }

                sheet1.ForceFormulaRecalculation = true;
                file = new FileStream(filepath + dao_name(), FileMode.Create);
                hssfworkbook.Write(file);
                file.Close();
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// DataTable导出到Excel重载(方法二)
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="filepath">导出文件路径</param>
        /// <param name="btname">标题</param>
        public static void Excel_daochu(DataTable dt, string filepath, string btname)
        {
            try
            {
                FileStream file = new FileStream(filepath + "导出模板.xls", FileMode.Open, FileAccess.Read);

                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                hssfworkbook.SetSheetName(0, "数据");
                ISheet sheet1 = hssfworkbook.GetSheet("数据");
                InsertRows(sheet1, 1, dt.Rows.Count, hssfworkbook, dt.Columns.Count);
                int columnrow = 0, datarow = 1;
                if (!String.IsNullOrEmpty(btname))
                {
                    IRow row = sheet1.CreateRow(0);
                    //在行中：建立单元格，参数为列号，从0计
                    ICell cell = row.CreateCell(0);
                    //设置单元格的高度
                    row.Height = 30 * 20;
                    //设置单元格的宽度
                    sheet1.SetColumnWidth(0, 30 * 256);
                    //合并单元格
                    sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, dt.Columns.Count - 1));

                    //设置单元格内容
                    cell.SetCellValue(btname);
                    ICellStyle style = hssfworkbook.CreateCellStyle();
                    //设置单元格的样式：水平对齐居中
                    style.Alignment = HorizontalAlignment.CENTER;
                    //新建一个字体样式对象
                    IFont font = hssfworkbook.CreateFont();
                    //设置字体加粗样式
                    font.Boldweight = short.MaxValue;
                    //使用SetFont方法将字体样式添加到单元格样式中 
                    style.SetFont(font);
                    //将新的样式赋给单元格
                    cell.CellStyle = style;
                    //列标题行从1开始写入
                    columnrow = 1;
                    //数据从2开始写入
                    datarow = 2;
                }

                //列标题
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sheet1.GetRow(columnrow).GetCell(i).SetCellValue(dt.Columns[i].ColumnName);

                }
                //写入数据
                for (int j = 1; j <= dt.Rows.Count; j++)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sheet1.GetRow(datarow).GetCell(i).SetCellValue(dt.Rows[j - 1][i].ToString());
                    }
                    datarow++;
                }
                //for (int q = 0; q < dt.Columns.Count; q++)
                //{
                //     sheet1.AutoSizeColumn(q);
                //}
                sheet1.ForceFormulaRecalculation = true;
                file = new FileStream(filepath + dao_name(), FileMode.Create);
                hssfworkbook.Write(file);
                file.Close();
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// DataTable导出到Excel(方法二)
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="filepath">导出文件路径</param>
        /// <param name="filepath">导出文件名</param>
        public static void Excel_daochu(string filepath, DataTable dt, string filename)
        {
            try
            {
                FileStream file = new FileStream(filepath + "导出模板.xls", FileMode.Open, FileAccess.Read);

                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                hssfworkbook.SetSheetName(0, "数据");
                ISheet sheet1 = hssfworkbook.GetSheet("数据");
                InsertRows(sheet1, 1, dt.Rows.Count, hssfworkbook, dt.Columns.Count);


                //列标题
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sheet1.GetRow(0).GetCell(i).SetCellValue(dt.Columns[i].ColumnName);
                }
                //写入数据
                for (int j = 1; j <= dt.Rows.Count; j++)
                {
                    //dt.Columns[0].ColumnName;
                    //IRow row1 = sheet1.GetRow(j);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sheet1.GetRow(j).GetCell(i).SetCellValue(dt.Rows[j - 1][i].ToString());
                    }
                }

                sheet1.ForceFormulaRecalculation = true;
                if (String.IsNullOrEmpty(filename))
                    filename = dao_name();
                file = new FileStream(filepath + filename, FileMode.Create);
                hssfworkbook.Write(file);
                file.Close();
            }
            catch (Exception)
            {

                throw;
            }

        }


        /// <summary>
        /// DataTable导出到Excel重载(方法二)
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="filepath">导出文件路径</param>
        /// <param name="btname">标题</param>
        /// <param name="filename">导出文件名称</param>
        public static void Excel_daochu(DataTable dt, string filepath, string btname, string filename)
        {
            try
            {
                FileStream file = new FileStream(filepath + "导出模板.xls", FileMode.Open, FileAccess.Read);

                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                hssfworkbook.SetSheetName(0, "数据");
                ISheet sheet1 = hssfworkbook.GetSheet("数据");
                InsertRows(sheet1, 1, dt.Rows.Count, hssfworkbook, dt.Columns.Count);
                int columnrow = 0, datarow = 1;
                if (!String.IsNullOrEmpty(btname))
                {
                    IRow row = sheet1.CreateRow(0);
                    //在行中：建立单元格，参数为列号，从0计
                    ICell cell = row.CreateCell(0);
                    //设置单元格的高度
                    row.Height = 30 * 20;
                    //设置单元格的宽度
                    sheet1.SetColumnWidth(0, 30 * 256);
                    //合并单元格
                    sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, dt.Columns.Count - 1));

                    //设置单元格内容
                    cell.SetCellValue(btname);
                    ICellStyle style = hssfworkbook.CreateCellStyle();
                    //设置单元格的样式：水平对齐居中
                    style.Alignment = HorizontalAlignment.CENTER;
                    //新建一个字体样式对象
                    IFont font = hssfworkbook.CreateFont();
                    //设置字体加粗样式
                    font.Boldweight = short.MaxValue;
                    //使用SetFont方法将字体样式添加到单元格样式中 
                    style.SetFont(font);
                    //将新的样式赋给单元格
                    cell.CellStyle = style;
                    //列标题行从1开始写入
                    columnrow = 1;
                    //数据从2开始写入
                    datarow = 2;
                }

                //列标题
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sheet1.GetRow(columnrow).GetCell(i).SetCellValue(dt.Columns[i].ColumnName);

                }
                //写入数据
                for (int j = 1; j <= dt.Rows.Count; j++)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sheet1.GetRow(datarow).GetCell(i).SetCellValue(dt.Rows[j - 1][i].ToString());
                    }
                    datarow++;
                }
                //for (int q = 0; q < dt.Columns.Count; q++)
                //{
                //     sheet1.AutoSizeColumn(q);
                //}
                sheet1.ForceFormulaRecalculation = true;
                if (String.IsNullOrEmpty(filename))
                    filename = dao_name();
                file = new FileStream(filepath + filename, FileMode.Create);
                hssfworkbook.Write(file);

                file.Close();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public static void ExcSF_daochu(List<DataTable> Ldt, string filepath, string filename, int rowDataCount)
        {
            try
            {
                FileStream file = new FileStream(filepath + "导出模板.xls", FileMode.Open, FileAccess.Read);

                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                hssfworkbook.SetSheetName(0, "数据");
                ISheet sheet1 = hssfworkbook.GetSheet("数据");
                InsertRows(sheet1, 1, rowDataCount, hssfworkbook, 8);
                int datarow = 0;

                foreach (DataTable item in Ldt)
                {
                    //IRow row = sheet1.CreateRow(datarow);
                    ////在行中：建立单元格，参数为列号，从0计
                    //ICell cell = row.CreateCell(0);
                    ////设置单元格的高度
                    //row.Height = 30 * 20;
                    //设置单元格的宽度
                    // sheet1.SetColumnWidth(0, 30 * 256);
                    //合并单元格
                    IRow row = sheet1.GetRow(datarow);
                    ICell cell = row.GetCell(0);
                    row.Height = 30 * 20;
                    sheet1.SetColumnWidth(0, 30 * 270);
                    sheet1.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(datarow, datarow, 0, item.Columns.Count - 6));
                    string btname = "--收款单位：" + item.Rows[0]["Payee"].ToString() + "\r\n--付款单位：" + item.Rows[0]["付款单位"].ToString() + @"\n" + "--付款时间：" + item.Rows[0]["ChargingTime"].ToString() + Environment.NewLine + "--付款金额：" + item.Rows[0]["PaidIn"].ToString() + "";
                    //设置单元格内容
                    cell.SetCellValue(btname);
                    ICellStyle style = hssfworkbook.CreateCellStyle();
                    //设置单元格的样式：水平对齐居中
                    style.Alignment = HorizontalAlignment.CENTER;
                    //新建一个字体样式对象
                    IFont font = hssfworkbook.CreateFont();
                    //设置字体加粗样式
                    font.Boldweight = short.MaxValue;
                    //使用SetFont方法将字体样式添加到单元格样式中 
                    style.SetFont(font);
                    //将新的样式赋给单元格
                    cell.CellStyle = style;
                    //数据从2开始写入
                    datarow++;


                    item.Columns.Remove("ChargingRecordsID");
                    item.Columns.Remove("PaidIn");
                    item.Columns.Remove("Payee");
                    item.Columns.Remove("ChargingTime");
                    item.Columns.Remove("付款单位");
                    item.Columns.Remove("送检单位");
                    item.Columns.Remove("生产单位");
                    item.Columns.Remove("类型");
                    //列标题
                    for (int i = 0; i < item.Columns.Count; i++)
                    {
                        sheet1.GetRow(datarow).GetCell(i).SetCellValue(item.Columns[i].ColumnName);

                    }
                    datarow++;
                    //写入数据
                    for (int j = 1; j <= item.Rows.Count; j++)
                    {
                        for (int i = 0; i < item.Columns.Count; i++)
                        {
                            sheet1.GetRow(datarow).GetCell(i).SetCellValue(item.Rows[j - 1][i].ToString());
                        }
                        datarow++;
                    }
                }


                //for (int q = 0; q < dt.Columns.Count; q++)
                //{
                //     sheet1.AutoSizeColumn(q);
                //}
                sheet1.ForceFormulaRecalculation = true;
                if (String.IsNullOrEmpty(filename))
                    filename = dao_name();
                file = new FileStream(filepath + filename, FileMode.Create);
                hssfworkbook.Write(file);

                file.Close();
            }
            catch (Exception)
            {

                throw;
            }

        }

        /////
        ///// 导出Word
        /////
        ///// 标题
        ///// 导出的数据DataTable
        ///// 是否显示列名
        //public static void OutPutWordDT(string strTitle, DataTable dt, bool isColname, string fname)
        //{
        //    //string FileURL = fpath + DateTime.Now.ToShortDateString() + ".doc";//为将创建的文件设置路径,创建文件(路径+文件名)
        //    if (File.Exists(fname)) System.IO.File.Delete(fname); // 判断文件名是否已存在
        //    object filename = fname; //文件保存路径

        //    Object Nothing = System.Reflection.Missing.Value;
        //    Application oword = new ApplicationClass();
        //    Document odoc = oword.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
        //    odoc.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
        //    try
        //    {
        //        Table otable = odoc.Tables.Add(oword.Selection.Range, dt.Rows.Count + 3, dt.Columns.Count - 2, ref Nothing, ref Nothing);
        //        otable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;//设置对其方式
        //        otable.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;//设置表格边框样式
        //        otable.Cell(1, 1).Merge(otable.Cell(1, dt.Columns.Count - 2)); //合并单元格
        //        otable.Cell(1, 1).Range.Text = strTitle;
        //        otable.Cell(1, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
        //        otable.Cell(1, 1).Range.Font.Bold = 1;
        //        otable.Cell(1, 1).Range.Font.Size = 20;
        //        if (isColname)
        //        {
        //            int intCol = 0;
        //            for (int ii = 0; ii < dt.Columns.Count - 2; ii++)
        //            {
        //                intCol += 1;
        //                otable.Cell(2, intCol).Range.Text = dt.Columns[ii].ColumnName;
        //                otable.Cell(2, intCol).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
        //                otable.Cell(2, intCol).Range.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;//设置单元格样式
        //                //if (intCol == 8)
        //                //{
        //                //    otable.Cell(2, intCol).Width = 100;
        //                //}
        //            }
        //        }
        //        int intRow = 2;
        //        for (int ii = 0; ii < dt.Rows.Count; ii++)
        //        {
        //            intRow += 1;
        //            int intcol = 0;
        //            for (int jj = 0; jj < dt.Columns.Count - 2; jj++)
        //            {
        //                intcol += 1;
        //                otable.Cell(intRow, intcol).Range.Text = dt.Rows[ii][jj].ToString();
        //                otable.Cell(intRow, intcol).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
        //                otable.Cell(intRow, intcol).Range.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;//设置单元格样式
        //            }
        //        }

        //        intRow += 1;
        //        otable.Cell(intRow, 1).Merge(otable.Cell(intRow, 4)); //合并单元格
        //        otable.Cell(intRow, 1).Range.Text = "合计";
        //        otable.Cell(intRow, 1).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
        //        otable.Cell(intRow, 1).Range.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;//设置单元格样式

        //        otable.Cell(intRow, 2).Range.Text = dt.Compute("Sum(数量)", "").ToString();
        //        otable.Cell(intRow, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
        //        otable.Cell(intRow, 2).Range.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;//设置单元格样式

        //        otable.Cell(intRow, 3).Merge(otable.Cell(intRow, 5)); //合并单元格
        //        otable.Cell(intRow, 3).Range.Text = (double.Parse(dt.Compute("Sum(检定费)", "").ToString()) + double.Parse(dt.Compute("Sum(修配费)", "").ToString())).ToString(); ;
        //        otable.Cell(intRow, 3).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
        //        otable.Cell(intRow, 3).Range.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;//设置单元格样式

        //        //oword.Visible = true;
        //        odoc.SaveAs(ref filename, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing);
        //    }
        //    catch (Exception) { }
        //    finally
        //    {
        //        //odoc.Close(ref Nothing, ref Nothing, ref Nothing);
        //        //oword.Quit(ref Nothing, ref Nothing, ref Nothing);
        //    }
        //}

        static void InsertRows(ISheet targetSheet, int fromRowIndex, int rowCount, HSSFWorkbook hssfworkbook, int columnCount)
        {
            IRow xxx = targetSheet.GetRow(fromRowIndex);
            for (int rowIndex = fromRowIndex; rowIndex <= fromRowIndex + rowCount; rowIndex++)
            {
                IRow rowInsert = targetSheet.CreateRow(rowIndex);
                //rowInsert.RowStyle = xxx.RowStyle;
                //rowInsert.Height = xxx.Height;
                for (int colIndex = 0; colIndex < columnCount; colIndex++)
                {
                    ICell cellInsert = rowInsert.CreateCell(colIndex);
                    // cellInsert.CellStyle = xxx.GetCell(colIndex).CellStyle;
                }
            }
        }
        #endregion


        public static System.Data.DataTable GetDataByExcel(string FileName, string FileSheet)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            //System.Data.OleDb.OleDbConnection ocon = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";");
            System.Data.OleDb.OleDbConnection ocon = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";");


            System.Data.OleDb.OleDbDataAdapter oda = new System.Data.OleDb.OleDbDataAdapter("select * from [" + FileSheet + "$]", ocon);
            oda.Fill(dt);
            if (ocon.State == ConnectionState.Open)
            {
                ocon.Close();
            }
            ocon.Dispose();
            return dt;
        }

        /// <summary>
        /// 导出收入明细
        /// </summary>
        //public static void Excel_IncomeMX(String yearMonth, List<Income> list, string openFilePath, String saveFilePath)
        //{
        //    try
        //    {
        //        FileStream file = new FileStream(openFilePath, FileMode.Open, FileAccess.Read);
        //        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);

        //        hssfworkbook.SetSheetName(0, yearMonth);
        //        ISheet sheet1 = hssfworkbook.GetSheet(yearMonth);
        //        InsertRows2(sheet1, 1, list.Count);

        //        //写入数据
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            IRow row = sheet1.GetRow(1 + i);
        //            row.GetCell(0).SetCellValue(list[i].YearMonth);
        //            row.GetCell(1).SetCellValue(list[i].AgentId);
        //            row.GetCell(2).SetCellValue(list[i].AgentName);
        //            row.GetCell(3).SetCellValue(list[i].CareerStatus);
        //            row.GetCell(4).SetCellValue(list[i].Rank);
        //            row.GetCell(5).SetCellValue((double)list[i].SalesServiceMoney);
        //            row.GetCell(6).SetCellValue((double)list[i].PersonalServiceMoney);
        //            row.GetCell(7).SetCellValue((double)list[i].MarketServiceMoney);
        //            row.GetCell(8).SetCellValue((double)list[i].RegionServiceMoney);
        //            row.GetCell(9).SetCellValue((double)list[i].RegionServiceYum);
        //            row.GetCell(10).SetCellValue((double)list[i].IncomeMoney);
        //        }

        //        sheet1.ForceFormulaRecalculation = true;
        //        file = new FileStream(saveFilePath, FileMode.Create);
        //        hssfworkbook.Write(file);
        //        file.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}
        ///// <summary>
        ///// 导出会员概要
        ///// </summary>
        //public static void Excel_HuiyuanGY(String yearMonth, List<Income> list, string openFilePath, String saveFilePath)
        //{
        //    try
        //    {
        //        FileStream file = new FileStream(openFilePath, FileMode.Open, FileAccess.Read);
        //        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);

        //        hssfworkbook.SetSheetName(0, yearMonth);
        //        ISheet sheet1 = hssfworkbook.GetSheet(yearMonth);
        //        InsertRows2(sheet1, 1, list.Count);

        //        //写入数据
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            IRow row = sheet1.GetRow(1 + i);
        //            row.GetCell(0).SetCellValue(list[i].YearMonth);
        //            row.GetCell(1).SetCellValue(list[i].AgentId);
        //            row.GetCell(2).SetCellValue(list[i].AgentName);
        //            row.GetCell(3).SetCellValue(list[i].CareerStatus);
        //            row.GetCell(4).SetCellValue(list[i].Rank);
        //            row.GetCell(5).SetCellValue(list[i].RefereeId);
        //            row.GetCell(6).SetCellValue(list[i].RefereeName);
        //            row.GetCell(7).SetCellValue(list[i].AgencyId);
        //            row.GetCell(8).SetCellValue(list[i].AgencyName);
        //        }

        //        sheet1.ForceFormulaRecalculation = true;
        //        file = new FileStream(saveFilePath, FileMode.Create);
        //        hssfworkbook.Write(file);
        //        file.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}

        /// <summary>
        /// 导出产品汇总
        /// </summary>
        //public static void Excel_ChanpinHZ(String yearMonth, List<OrdersDetail> list, string openFilePath, String saveFilePath)
        //{
        //    try
        //    {
        //        FileStream file = new FileStream(openFilePath, FileMode.Open, FileAccess.Read);
        //        HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);

        //        hssfworkbook.SetSheetName(0, yearMonth);
        //        ISheet sheet1 = hssfworkbook.GetSheet(yearMonth);
        //        InsertRows2(sheet1, 1, list.Count);

        //        //写入数据
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            IRow row = sheet1.GetRow(1 + i);
        //            row.GetCell(0).SetCellValue(list[i].ProductName);
        //            row.GetCell(1).SetCellValue((double)list[i].UnitPrice);
        //            row.GetCell(2).SetCellValue(list[i].Num);
        //            row.GetCell(3).SetCellValue((double)list[i].Price);
        //        }

        //        sheet1.ForceFormulaRecalculation = true;
        //        file = new FileStream(saveFilePath, FileMode.Create);
        //        hssfworkbook.Write(file);
        //        file.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        /// <summary>
        /// 导出订单汇总
        /// </summary>
        public static void Excel_DingdanHZ(String yearMonth, DataTable dt, string openFilePath, String saveFilePath)
        {
            try
            {
                FileStream file = new FileStream(openFilePath, FileMode.Open, FileAccess.Read);
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);

                hssfworkbook.SetSheetName(0, yearMonth);
                ISheet sheet1 = hssfworkbook.GetSheet(yearMonth);
                InsertRows2(sheet1, 1, dt.Rows.Count);

                //写入数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet1.GetRow(1 + i);
                    row.GetCell(0).SetCellValue(dt.Rows[i]["AgentId"].ToString());
                    row.GetCell(1).SetCellValue(dt.Rows[i]["AgentName"].ToString());
                    row.GetCell(2).SetCellValue(dt.Rows[i]["CareerStatus"].ToString());
                    row.GetCell(3).SetCellValue(dt.Rows[i]["Rank"].ToString());
                    var state = dt.Rows[i]["State"].ToString();
                    row.GetCell(4).SetCellValue(state == "0" ? "已删除" : (state == "1" ? "正常" : (state == "-1" ? "新添加" : "")));
                    row.GetCell(5).SetCellValue(dt.Rows[i]["CountOrders"].ToString());
                    row.GetCell(6).SetCellValue(dt.Rows[i]["Price"].ToString());
                }

                sheet1.ForceFormulaRecalculation = true;
                file = new FileStream(saveFilePath, FileMode.Create);
                hssfworkbook.Write(file);
                file.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        static void InsertRows2(ISheet targetSheet, int fromRowIndex, int rowCount)
        {
            if (rowCount != 0)
            {
                targetSheet.ShiftRows(fromRowIndex + 1, targetSheet.LastRowNum, rowCount, true, false);
                IRow rowSource = targetSheet.GetRow(fromRowIndex);
                ICellStyle rowstyle = rowSource.RowStyle;

                for (int rowIndex = fromRowIndex; rowIndex <= fromRowIndex + rowCount; rowIndex++)
                {
                    IRow rowInsert = targetSheet.CreateRow(rowIndex);
                    //rowInsert.RowStyle = rowstyle;
                    //rowInsert.Height = rowSource.Height;
                    for (int colIndex = 0; colIndex < rowSource.LastCellNum; colIndex++)
                    {
                        ICell cellSource = rowSource.GetCell(colIndex);
                        ICell cellInsert = rowInsert.CreateCell(colIndex);
                        if (cellSource != null)
                        {
                            cellInsert.CellStyle = cellSource.CellStyle;
                        }
                    }
                }
            }
        }

    }
}
