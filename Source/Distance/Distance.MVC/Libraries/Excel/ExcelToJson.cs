using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace DXTms
{
    public static class ExcelToJson
    {

        public static string Execute(HttpPostedFileBase file)
        {
            string result = ExcelToDataTable(file.InputStream);

            return result;
        }

        public static string Execute(Stream filestream)
        {            
            string result = ExcelToDataTable(filestream);

            return result;
        }

        private static string ExcelToDataTable(Stream filestream)
        {
            DataTable tblRawData = new DataTable();

            // Open and read the XlSX file.
            using (var package = new ExcelPackage(filestream))
            {
                // Get the work book in the file
                ExcelWorkbook workBook = package.Workbook;
                if (workBook != null)
                {
                    if (workBook.Worksheets.Count > 0)
                    {                        
                        ExcelWorksheet sheet = workBook.Worksheets.First();                        
                        //add columns                        
                        foreach (var headerRowCell in sheet.Cells[1, 1, 1, sheet.Dimension.End.Column])
                        {
                            tblRawData.Columns.Add(headerRowCell.Text);

                        }
                        //add row data (start from 2nd row)           
                        for (int i = 2; i <= sheet.Dimension.End.Row; i++)
                        {
                            DataRow datarow = tblRawData.NewRow();
                            int count_null = 0;
                            int count_col = 0;

                            for (int j = 1; j <= sheet.Dimension.End.Column; j++)
                            {
                                string data = sheet.Cells[i, j].Value == null ? "" : sheet.Cells[i, j].Text;

                                datarow[j - 1] = data;

                                if (data == "")
                                {
                                    count_null++;
                                }
                                count_col++;
                            }

                            //check empty row then ignore empty row
                            if (count_col > count_null)
                            {
                                tblRawData.Rows.Add(datarow);
                            }
                        }

                        

                       
                    }
                }
            }


            //HttpContext.Current.Response.Write("<table border='1'>");
            //for (int i = 0; i < tblRawData.Rows.Count; i++)
            //{
            //    HttpContext.Current.Response.Write("<tr>");

            //    for (int j = 0; j < tblRawData.Columns.Count; j++)
            //    {
            //        HttpContext.Current.Response.Write("<td>");
            //        HttpContext.Current.Response.Write(tblRawData.Rows[i][j]);
            //        HttpContext.Current.Response.Write("</td>");
            //    }
            //    HttpContext.Current.Response.Write("</tr>");
            //}
            //HttpContext.Current.Response.Write("</table>");

            int row_count = tblRawData.Rows.Count;
            int col_count = tblRawData.Columns.Count;

            string header = "{";
            for (int i = 0; i < tblRawData.Columns.Count; i++)
            {
                header += "\"" + i + "\":\"" + tblRawData.Columns[i].ToString() + "\"";
                if ((i + 1) != tblRawData.Columns.Count)
                {
                    header += ",";
                }
            }
            header+="}";

            string result = "{\"total_row\":\"" + row_count + "\",\"total_column\":\"" + col_count + "\",\"data\":";
            result += DataTableToJson(tblRawData);
            result += ",\"header\":";
            result += header;
            result += "}";

            return result; 
        }

        private static string DataTableToJson(DataTable dt)
        {           
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row = null;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                int index = 0;
                foreach (DataColumn col in dt.Columns)
                {
                    //row.Add(col.ColumnName.Replace(" ", "_"), dr[col].ToString().Trim());
                    row.Add("data"+index.ToString(), dr[col].ToString().Trim());
                    index++;
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
    }
}