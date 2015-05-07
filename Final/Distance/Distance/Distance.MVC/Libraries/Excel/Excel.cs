using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;

namespace EpplusExcel
{
    public class ExcelExport
    {
        public ExcelHeaderList headerList;
        public ExcelDetailDataList detailList;
        public ExcelSummaryList summaryList;

        public ExcelExport()
        {
            //constructor
            headerList = new ExcelHeaderList();
            detailList = new ExcelDetailDataList();
            summaryList = new ExcelSummaryList();
        }

        public void GenerateExcel(string sheetName, string fileName) 
        {
            this.GenerateExcel(sheetName, fileName, null, null);
        }

        public void GenerateExcel(string sheetName, string fileName, string headerStartPosition)
        {
            this.GenerateExcel(sheetName, fileName, headerStartPosition, null);
        }

        public void GenerateExcel(string sheetName, string fileName, string headerStartPosition, string detailStartPosition)
        {
            sheetName = "Sheet1";
            headerStartPosition = null; //"B1";
            detailStartPosition = null; //"B4";
            //fileName = "excel1";
          
            ExcelPackage excelPackage = new ExcelPackage();
            ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets.Add(sheetName);

            if (headerStartPosition == null)
            {
                headerStartPosition = "A1";
            }

            //make header
            excelPackage = MakeHeader(excelPackage, headerList, sheetName, headerStartPosition);

            //find start detail position
            string detailStartColumnAlphabet = "";
            if (detailStartPosition == null)
            {
                ExcelCellAddress lastrow = excelPackage.Workbook.Worksheets[sheetName].Dimension.End;
                //number run at 48 to 57
                //A to Z is 65 to 90
                //a to z is 97 to 122               
                foreach (char c in headerStartPosition)
                {
                    if (Convert.ToInt32(c) > 57)
                    {
                        //this is alphabet
                        detailStartColumnAlphabet += c.ToString();
                    }
                    else
                    {
                        break;
                    }
                }

                detailStartPosition = detailStartColumnAlphabet + (lastrow.Row + 1).ToString();
            }

            //make detail
            excelPackage = MakeDetail(excelPackage, detailList, sheetName, detailStartPosition);
            //make summary
            excelPackage = MakeSummary(excelPackage, summaryList, sheetName, detailStartPosition);

            //export excel file
            HttpContext.Current.Response.BinaryWrite(excelPackage.GetAsByteArray());
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + ".xlsx");
            
        }

       private ExcelPackage MakeHeader(ExcelPackage excelPackage, ExcelHeaderList tableHeaderList, string sheetName, string headerStartPosition)
        {
            var workSheet = excelPackage.Workbook.Worksheets[sheetName];

            Dictionary<string, int> startPositionColumnRow = new Dictionary<string, int>();
            startPositionColumnRow = GetPositionColumnRowNumberFromAlphabet(headerStartPosition);
            
            int startColumnNum = Convert.ToInt32(startPositionColumnRow["column"]);
            int startRowNum = Convert.ToInt32(startPositionColumnRow["row"]);

            foreach (ExcelHeaderList.HeaderColumn head in tableHeaderList.Columns)
            {
                dynamic fromRow = head.FromRow - 1 + startRowNum;
                dynamic fromCol = head.FromColumn - 1 + startColumnNum;
                dynamic toRow = head.ToRow - 1 + startRowNum;
                dynamic toCol = head.ToColumn - 1 + startColumnNum;
                dynamic title = head.Title;
                dynamic cellWidth = head.ColumnWidth;
                dynamic shinkToFit = head.ShinkToFit;
                dynamic wrapText = head.WarpText;
                dynamic cellFormat = head.CellFormat;
                dynamic cellBackgroundColor = head.BlackgroundColor;
                dynamic fontBold = head.FontBold;
                dynamic fontColor = head.FontColor;
                dynamic fontItalic = head.FontItalic;
                dynamic fontUnderLine = head.FontUnderLine;
                dynamic fontSize = head.FontSize;
                dynamic fontName = head.FontName;
                dynamic verticalAlign = head.VerticalAlign;
                dynamic horizontalAlign = head.HorizontalAlign;
                dynamic topBorder = head.BorderTop;
                dynamic rightBorder = head.BorderRight;
                dynamic bottomBorder = head.BorderBottom;
                dynamic leftBorder = head.BorderLeft;

                workSheet.Cells[fromRow, fromCol, toRow, toCol].Merge = true;
                
                workSheet.Column(fromCol).Width = cellWidth;
                
                workSheet.Cells[fromRow, fromCol, toRow, toCol].Value = title;
                workSheet.Cells[fromRow, fromCol, toRow, toCol].Style.ShrinkToFit = shinkToFit;
                workSheet.Cells[fromRow, fromCol, toRow, toCol].Style.WrapText = wrapText;
                workSheet.Cells[fromRow, fromCol, toRow, toCol].Style.Numberformat.Format = cellFormat; //"@"; //"dd-MM-yyyy"; //@"#,##0.00฿";
                
                workSheet.Cells[fromRow, fromCol, toRow, toCol].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[fromRow, fromCol, toRow, toCol].Style.Fill.BackgroundColor.SetColor(cellBackgroundColor);
                
                workSheet.Cells[fromRow, fromCol, toRow, toCol].Style.Font.Bold = fontBold;
                workSheet.Cells[fromRow, fromCol, toRow, toCol].Style.Font.Color.SetColor(fontColor);
                workSheet.Cells[fromRow, fromCol, toRow, toCol].Style.Font.Italic = fontItalic;
                workSheet.Cells[fromRow, fromCol, toRow, toCol].Style.Font.UnderLine = fontUnderLine;
                workSheet.Cells[fromRow, fromCol, toRow, toCol].Style.Font.Size = fontSize;
                workSheet.Cells[fromRow, fromCol, toRow, toCol].Style.Font.Name = fontName;
                workSheet.Cells[fromRow, fromCol, toRow, toCol].Style.VerticalAlignment = verticalAlign;
                workSheet.Cells[fromRow, fromCol, toRow, toCol].Style.HorizontalAlignment = horizontalAlign;
                workSheet.Cells[fromRow, fromCol, toRow, toCol].Style.Border.Top.Style = topBorder;
                workSheet.Cells[fromRow, fromCol, toRow, toCol].Style.Border.Right.Style = rightBorder;
                workSheet.Cells[fromRow, fromCol, toRow, toCol].Style.Border.Bottom.Style = bottomBorder;
                workSheet.Cells[fromRow, fromCol, toRow, toCol].Style.Border.Left.Style = leftBorder;               
            }

            return excelPackage;
        }

        private ExcelPackage MakeDetail(ExcelPackage excelPackage, ExcelDetailDataList tableDetailList, string sheetName, string detailStartPosition)
        {
            var workSheet = excelPackage.Workbook.Worksheets[sheetName];

            Dictionary<string, int> startPositionColumnRow = new Dictionary<string, int>();
            startPositionColumnRow = GetPositionColumnRowNumberFromAlphabet(detailStartPosition);

            int startColumnNum = Convert.ToInt32(startPositionColumnRow["column"]);
            int startRowNum = Convert.ToInt32(startPositionColumnRow["row"]);
           
            int i = 0;
            foreach (ExcelDetailDataList.DetailRow detail in tableDetailList.Rows)
            {              
                //column loop level
                var row = startRowNum + i;

                int j = 0;
                foreach (ExcelDetailDataList.DetailRow.DetailColumn cell in detail.Columns)
                {
                    var col = startColumnNum + j;

                    dynamic data = cell.Data;
                    dynamic shinkToFit = cell.ShinkToFit;
                    dynamic wrapText = cell.WarpText;
                    dynamic cellFormat = cell.CellFormat;
                    dynamic cellBackgroundColor = cell.BlackgroundColor;
                    dynamic fontBold = cell.FontBold;
                    dynamic fontColor = cell.FontColor;
                    dynamic fontItalic = cell.FontItalic;
                    dynamic fontUnderLine = cell.FontUnderLine;
                    dynamic fontSize = cell.FontSize;
                    dynamic fontName = cell.FontName;
                    dynamic verticalAlign = cell.VerticalAlign;
                    dynamic horizontalAlign = cell.HorizontalAlign;
                    dynamic topBorder = cell.BorderTop;
                    dynamic rightBorder = cell.BorderRight;
                    dynamic bottomBorder = cell.BorderBottom;
                    dynamic leftBorder = cell.BorderLeft;

                    workSheet.Cells[row, col].Value = data;
                    workSheet.Cells[row, col].Style.ShrinkToFit = shinkToFit;
                    workSheet.Cells[row, col].Style.WrapText = wrapText;
                    workSheet.Cells[row, col].Style.Numberformat.Format = cellFormat; //"@"; //"dd-MM-yyyy"; //@"#,##0.00฿";

                    workSheet.Cells[row, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    workSheet.Cells[row, col].Style.Fill.BackgroundColor.SetColor(cellBackgroundColor);

                    workSheet.Cells[row, col].Style.Font.Bold = fontBold;
                    workSheet.Cells[row, col].Style.Font.Color.SetColor(fontColor);
                    workSheet.Cells[row, col].Style.Font.Italic = fontItalic;
                    workSheet.Cells[row, col].Style.Font.UnderLine = fontUnderLine;
                    workSheet.Cells[row, col].Style.Font.Size = fontSize;
                    workSheet.Cells[row, col].Style.Font.Name = fontName;
                    workSheet.Cells[row, col].Style.VerticalAlignment = verticalAlign;
                    workSheet.Cells[row, col].Style.HorizontalAlignment = horizontalAlign;
                    workSheet.Cells[row, col].Style.Border.Top.Style = topBorder;
                    workSheet.Cells[row, col].Style.Border.Right.Style = rightBorder;
                    workSheet.Cells[row, col].Style.Border.Bottom.Style = bottomBorder;
                    workSheet.Cells[row, col].Style.Border.Left.Style = leftBorder;  
                    
                    j++;
                }
            
                i++;
            }

            return excelPackage;
        }

        private ExcelPackage MakeSummary(ExcelPackage excelPackage,ExcelSummaryList detailSummaryColumn,string sheetName,string detailStartPosition)
        {
            //make summary
            ExcelCellAddress totalLastrow = excelPackage.Workbook.Worksheets[sheetName].Dimension.End;
            Dictionary<string, int> detailStartPositionNum = GetPositionColumnRowNumberFromAlphabet(detailStartPosition); //["column"] = column , ["row"] = row                        

            if (detailSummaryColumn.TextSummary.ShowSummaryText)
            {
                //show summary text
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, detailSummaryColumn.TextSummary.SummaryTextColumnNumber].Value = detailSummaryColumn.TextSummary.SummaryText;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, detailSummaryColumn.TextSummary.SummaryTextColumnNumber].Style.Numberformat.Format = detailSummaryColumn.TextSummary.CellFormat;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, detailSummaryColumn.TextSummary.SummaryTextColumnNumber].Style.Fill.PatternType = ExcelFillStyle.Solid;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, detailSummaryColumn.TextSummary.SummaryTextColumnNumber].Style.Fill.BackgroundColor.SetColor(detailSummaryColumn.TextSummary.BlackgroundColor);
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, detailSummaryColumn.TextSummary.SummaryTextColumnNumber].Style.Font.Bold = detailSummaryColumn.TextSummary.FontBold;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, detailSummaryColumn.TextSummary.SummaryTextColumnNumber].Style.Font.Color.SetColor(detailSummaryColumn.TextSummary.FontColor);
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, detailSummaryColumn.TextSummary.SummaryTextColumnNumber].Style.Font.Italic = detailSummaryColumn.TextSummary.FontItalic;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, detailSummaryColumn.TextSummary.SummaryTextColumnNumber].Style.Font.UnderLine = detailSummaryColumn.TextSummary.FontUnderLine;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, detailSummaryColumn.TextSummary.SummaryTextColumnNumber].Style.Font.Size = detailSummaryColumn.TextSummary.FontSize;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, detailSummaryColumn.TextSummary.SummaryTextColumnNumber].Style.Font.Name = detailSummaryColumn.TextSummary.FontName;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, detailSummaryColumn.TextSummary.SummaryTextColumnNumber].Style.VerticalAlignment = detailSummaryColumn.TextSummary.VerticalAlign;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, detailSummaryColumn.TextSummary.SummaryTextColumnNumber].Style.HorizontalAlignment = detailSummaryColumn.TextSummary.HorizontalAlign;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, detailSummaryColumn.TextSummary.SummaryTextColumnNumber].Style.Border.Top.Style = detailSummaryColumn.TextSummary.BorderTop;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, detailSummaryColumn.TextSummary.SummaryTextColumnNumber].Style.Border.Right.Style = detailSummaryColumn.TextSummary.BorderRight;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, detailSummaryColumn.TextSummary.SummaryTextColumnNumber].Style.Border.Bottom.Style = detailSummaryColumn.TextSummary.BorderBottom;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, detailSummaryColumn.TextSummary.SummaryTextColumnNumber].Style.Border.Left.Style = detailSummaryColumn.TextSummary.BorderLeft;
            }

            foreach (ExcelSummaryList.SummaryColumn sumCol in detailSummaryColumn.Columns)
            {
                dynamic formulaText = sumCol.FormulaText;
                string detailSummaryColumnAlphabet = GetPositionAlphabetFromColumnRowNumber(1, sumCol.ColumnNumber)["column"];
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, sumCol.ColumnNumber].Formula = formulaText + "(" + detailSummaryColumnAlphabet + detailStartPositionNum["row"].ToString() + ":" + detailSummaryColumnAlphabet + totalLastrow.Row.ToString() + ")";
                dynamic cellFormat = sumCol.CellFormat;
                dynamic cellBackgroundColor = sumCol.BlackgroundColor;
                dynamic fontBold = sumCol.FontBold;
                dynamic fontItalic = sumCol.FontItalic;
                dynamic fontColor = sumCol.FontColor;
                dynamic fontUnderline = sumCol.FontUnderLine;
                dynamic fontSize = sumCol.FontSize;
                dynamic fontName = sumCol.FontName;
                dynamic verticalAlign = sumCol.VerticalAlign;
                dynamic horizontalAlign = sumCol.HorizontalAlign;
                dynamic topBorder = sumCol.BorderTop;
                dynamic rightBorder = sumCol.BorderRight;
                dynamic bottomBorder = sumCol.BorderBottom;
                dynamic leftBorder = sumCol.BorderLeft;

                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, sumCol.ColumnNumber].Style.Numberformat.Format = cellFormat;

                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, sumCol.ColumnNumber].Style.Fill.PatternType = ExcelFillStyle.Solid;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, sumCol.ColumnNumber].Style.Fill.BackgroundColor.SetColor(cellBackgroundColor);

                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, sumCol.ColumnNumber].Style.Font.Bold = fontBold;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, sumCol.ColumnNumber].Style.Font.Color.SetColor(fontColor);
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, sumCol.ColumnNumber].Style.Font.Italic = fontItalic;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, sumCol.ColumnNumber].Style.Font.UnderLine = fontUnderline;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, sumCol.ColumnNumber].Style.Font.Size = fontSize;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, sumCol.ColumnNumber].Style.Font.Name = fontName;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, sumCol.ColumnNumber].Style.VerticalAlignment = verticalAlign;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, sumCol.ColumnNumber].Style.HorizontalAlignment = horizontalAlign;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, sumCol.ColumnNumber].Style.Border.Top.Style = topBorder;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, sumCol.ColumnNumber].Style.Border.Right.Style = rightBorder;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, sumCol.ColumnNumber].Style.Border.Bottom.Style = bottomBorder;
                excelPackage.Workbook.Worksheets[sheetName].Cells[totalLastrow.Row + 1, sumCol.ColumnNumber].Style.Border.Left.Style = leftBorder;
            }

            return excelPackage;
        }

        private Dictionary<string, int> GetPositionColumnRowNumberFromAlphabet(string addressString)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            
            using (ExcelPackage xlsp = new ExcelPackage())
            {
                xlsp.Workbook.Worksheets.Add("sheet1");                
                int startRowNum = xlsp.Workbook.Worksheets["sheet1"].Cells[addressString].Start.Row;
                int startColumnNum = xlsp.Workbook.Worksheets["sheet1"].Cells[addressString].Start.Column;
                result.Add("column", startColumnNum);
                result.Add("row", startRowNum);
            }           

            return result;
        }

        private Dictionary<string, dynamic> GetPositionAlphabetFromColumnRowNumber(int rowNumber, int columnNumber)
        {
            string address ="";
            using (ExcelPackage xlsp = new ExcelPackage())
            {
                xlsp.Workbook.Worksheets.Add("sheet1");
                address = xlsp.Workbook.Worksheets["sheet1"].Cells[1, columnNumber].Address.ToString();
            }
            address = address.Substring(0,address.Length-1);
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            result.Add("column", address);
            result.Add("row", rowNumber);

            return result;
        }

        //class header,detail,summary
        public class ExcelHeaderList
        {
            public ExcelHeaderList()
            {
                //Contructor     
                _Column = new HeaderColumns();
            }

            public class HeaderColumns : List<HeaderColumn>
            {
                public void Add()
                {
                    this.Add(new HeaderColumn());
                }

            }
            private HeaderColumns _Column;

            public HeaderColumns Columns
            {
                get { return this._Column; }
            }

            public void NewColumn()
            {
                this._Column.Add();
            }
            public void NewColumn(int numberOfNewColumn)
            {
                for (int i = 0; i < numberOfNewColumn; i++)
                {
                    this._Column.Add();
                }
            }

            public class HeaderColumn : List<dynamic>
            {
                public HeaderColumn()
                {
                    //Contructor

                }

                private int _FromRow = 0;
                public int FromRow
                {
                    set { this._FromRow = value; }
                    get { return this._FromRow; }
                }

                private int _FromColumn = 0;
                public int FromColumn
                {
                    set { this._FromColumn = value; }
                    get { return this._FromColumn; }
                }

                private int _ToRow = 0;
                public int ToRow
                {
                    set { this._ToRow = value; }
                    get { return this._ToRow; }
                }

                private int _ToColumn = 0;
                public int ToColumn
                {
                    set { this._ToColumn = value; }
                    get { return this._ToColumn; }
                }

                private string _Title = "";
                public string Title
                {
                    set { this._Title = value; }
                    get { return this._Title; }
                }

                private int _ColumnWidth = 10;
                public int ColumnWidth
                {
                    set { this._ColumnWidth = value; }
                    get { return this._ColumnWidth; }
                }

                private bool _ShinkToFit = false;
                public bool ShinkToFit
                {
                    set { this._ShinkToFit = value; }
                    get { return this._ShinkToFit; }
                }

                private bool _WarpText = false;
                public bool WarpText
                {
                    set { this._WarpText = value; }
                    get { return this._WarpText; }
                }

                private string _CellFormat = "";
                public string CellFormat
                {
                    set { this._CellFormat = value; }
                    get { return this._CellFormat; }
                }

                private System.Drawing.Color _BlackgroundColor = System.Drawing.Color.White;
                public System.Drawing.Color BlackgroundColor
                {
                    set { this._BlackgroundColor = value; }
                    get { return this._BlackgroundColor; }
                }

                private bool _FontBold = true;
                public bool FontBold
                {
                    set { this._FontBold = value; }
                    get { return this._FontBold; }
                }

                private System.Drawing.Color _FontColor = System.Drawing.Color.Black;
                public System.Drawing.Color FontColor
                {
                    set { this._FontColor = value; }
                    get { return this._FontColor; }
                }

                private bool _FontItalic = false;
                public bool FontItalic
                {
                    set { this._FontItalic = value; }
                    get { return this._FontItalic; }
                }

                private bool _FontUnderLine = false;
                public bool FontUnderLine
                {
                    set { this._FontUnderLine = value; }
                    get { return this._FontUnderLine; }
                }

                private float _FontSize = 11;
                public float FontSize
                {
                    set { this._FontSize = value; }
                    get { return this._FontSize; }
                }

                private string _FontName = "calibli";
                public string FontName
                {
                    set { this._FontName = value; }
                    get { return this._FontName; }
                }

                private ExcelVerticalAlignment _VerticalAlign = ExcelVerticalAlignment.Center;
                public ExcelVerticalAlignment VerticalAlign
                {
                    set { this._VerticalAlign = value; }
                    get { return this._VerticalAlign; }
                }

                private ExcelHorizontalAlignment _HorizontalAlign = ExcelHorizontalAlignment.Center;
                public ExcelHorizontalAlignment HorizontalAlign
                {
                    set { this._HorizontalAlign = value; }
                    get { return this._HorizontalAlign; }
                }

                private ExcelBorderStyle _BorderTop = ExcelBorderStyle.Thin;
                public ExcelBorderStyle BorderTop
                {
                    set { this._BorderTop = value; }
                    get { return this._BorderTop; }
                }

                private ExcelBorderStyle _BorderRight = ExcelBorderStyle.Thin;
                public ExcelBorderStyle BorderRight
                {
                    set { this._BorderRight = value; }
                    get { return this._BorderRight; }
                }

                private ExcelBorderStyle _BorderBottom = ExcelBorderStyle.Thin;
                public ExcelBorderStyle BorderBottom
                {
                    set { this._BorderBottom = value; }
                    get { return this._BorderBottom; }
                }

                private ExcelBorderStyle _BorderLeft = ExcelBorderStyle.Thin;
                public ExcelBorderStyle BorderLeft
                {
                    set { this._BorderLeft = value; }
                    get { return this._BorderLeft; }
                }
            }

        }


        public class ExcelDetailDataList
        {
            public ExcelDetailDataList()
            {
                //Contructor     
                _Row = new DetailRows();
            }

            public class DetailRows : List<DetailRow>
            {
                public void Add()
                {
                    this.Add(new DetailRow());
                }

            }
            private DetailRows _Row;

            public DetailRows Rows
            {
                get { return this._Row; }
            }

            public void NewRow()
            {
                this._Row.Add();
            }

            public void NewRow(int NumnerOfNewRow)
            {
                for (int i = 0; i < NumnerOfNewRow; i++)
                {
                    this._Row.Add();
                }
            }

            public class DetailRow
            {
                public DetailRow()
                {
                    //Contructor
                    _Column = new DetailColumns();
                }

                public class DetailColumns : List<DetailColumn>
                {
                    public void Add()
                    {
                        this.Add(new DetailColumn());
                    }
                }

                private DetailColumns _Column;

                public DetailColumns Columns
                {
                    get { return this._Column; }
                }

                public void NewColumn()
                {
                    this._Column.Add();
                }

                public void NewColumn(int NumberOfNewColumn)
                {
                    for (int i = 0; i < NumberOfNewColumn; i++)
                    {
                        this._Column.Add();
                    }
                }

                public class DetailColumn : List<object>
                {
                    private dynamic _Data = "";
                    public dynamic Data
                    {
                        set { this._Data = value; }
                        get { return this._Data; }
                    }

                    private bool _ShinkToFit = false;
                    public bool ShinkToFit
                    {
                        set { this._ShinkToFit = value; }
                        get { return this._ShinkToFit; }
                    }

                    private bool _WarpText = false;
                    public bool WarpText
                    {
                        set { this._WarpText = value; }
                        get { return this._WarpText; }
                    }

                    private string _CellFormat = "";
                    public string CellFormat
                    {
                        set { this._CellFormat = value; }
                        get { return this._CellFormat; }
                    }

                    private System.Drawing.Color _BlackgroundColor = System.Drawing.Color.White;
                    public System.Drawing.Color BlackgroundColor
                    {
                        set { this._BlackgroundColor = value; }
                        get { return this._BlackgroundColor; }
                    }

                    private bool _FontBold = false;
                    public bool FontBold
                    {
                        set { this._FontBold = value; }
                        get { return this._FontBold; }
                    }

                    private System.Drawing.Color _FontColor = System.Drawing.Color.Black;
                    public System.Drawing.Color FontColor
                    {
                        set { this._FontColor = value; }
                        get { return this._FontColor; }
                    }

                    private bool _FontItalic = false;
                    public bool FontItalic
                    {
                        set { this._FontItalic = value; }
                        get { return this._FontItalic; }
                    }

                    private bool _FontUnderLine = false;
                    public bool FontUnderLine
                    {
                        set { this._FontUnderLine = value; }
                        get { return this._FontUnderLine; }
                    }

                    private float _FontSize = 11;
                    public float FontSize
                    {
                        set { this._FontSize = value; }
                        get { return this._FontSize; }
                    }

                    private string _FontName = "calibli";
                    public string FontName
                    {
                        set { this._FontName = value; }
                        get { return this._FontName; }
                    }

                    private ExcelVerticalAlignment _VerticalAlign = ExcelVerticalAlignment.Bottom;
                    public ExcelVerticalAlignment VerticalAlign
                    {
                        set { this._VerticalAlign = value; }
                        get { return this._VerticalAlign; }
                    }

                    private ExcelHorizontalAlignment _HorizontalAlign = ExcelHorizontalAlignment.General;
                    public ExcelHorizontalAlignment HorizontalAlign
                    {
                        set { this._HorizontalAlign = value; }
                        get { return this._HorizontalAlign; }
                    }

                    private ExcelBorderStyle _BorderTop = ExcelBorderStyle.Thin;
                    public ExcelBorderStyle BorderTop
                    {
                        set { this._BorderTop = value; }
                        get { return this._BorderTop; }
                    }

                    private ExcelBorderStyle _BorderRight = ExcelBorderStyle.Thin;
                    public ExcelBorderStyle BorderRight
                    {
                        set { this._BorderRight = value; }
                        get { return this._BorderRight; }
                    }

                    private ExcelBorderStyle _BorderBottom = ExcelBorderStyle.Thin;
                    public ExcelBorderStyle BorderBottom
                    {
                        set { this._BorderBottom = value; }
                        get { return this._BorderBottom; }
                    }

                    private ExcelBorderStyle _BorderLeft = ExcelBorderStyle.Thin;
                    public ExcelBorderStyle BorderLeft
                    {
                        set { this._BorderLeft = value; }
                        get { return this._BorderLeft; }
                    }

                }
            }

        }


        public class ExcelSummaryList
        {
            public ExcelSummaryList()
            {
                //Contructor     
                _Column = new SummaryColumns();
                _TextSummary = new SummaryString();
            }


            private SummaryString _TextSummary;
            public SummaryString TextSummary
            {
                get { return _TextSummary; }
            }

            public class SummaryString
            {
                public SummaryString()
                {
                    //Contructor
                }

                private bool _ShowSummaryText = false;
                public bool ShowSummaryText
                {
                    set { this._ShowSummaryText = value; }
                    get { return this._ShowSummaryText; }
                }

                private string _SummaryText = "Summary";
                public string SummaryText
                {
                    set { this._SummaryText = value; }
                    get { return this._SummaryText; }
                }

                private int __SummaryTextColumnNumber = 1;
                public int SummaryTextColumnNumber
                {
                    set { this.__SummaryTextColumnNumber = value; }
                    get { return this.__SummaryTextColumnNumber; }
                }

                private string _CellFormat = "";
                public string CellFormat
                {
                    set { this._CellFormat = value; }
                    get { return this._CellFormat; }
                }

                private System.Drawing.Color _BlackgroundColor = System.Drawing.Color.White;
                public System.Drawing.Color BlackgroundColor
                {
                    set { this._BlackgroundColor = value; }
                    get { return this._BlackgroundColor; }
                }

                private bool _FontBold = true;
                public bool FontBold
                {
                    set { this._FontBold = value; }
                    get { return this._FontBold; }
                }

                private System.Drawing.Color _FontColor = System.Drawing.Color.Black;
                public System.Drawing.Color FontColor
                {
                    set { this._FontColor = value; }
                    get { return this._FontColor; }
                }

                private bool _FontItalic = false;
                public bool FontItalic
                {
                    set { this._FontItalic = value; }
                    get { return this._FontItalic; }
                }

                private bool _FontUnderLine = false;
                public bool FontUnderLine
                {
                    set { this._FontUnderLine = value; }
                    get { return this._FontUnderLine; }
                }

                private float _FontSize = 11;
                public float FontSize
                {
                    set { this._FontSize = value; }
                    get { return this._FontSize; }
                }

                private string _FontName = "calibli";
                public string FontName
                {
                    set { this._FontName = value; }
                    get { return this._FontName; }
                }

                private ExcelVerticalAlignment _VerticalAlign = ExcelVerticalAlignment.Bottom;
                public ExcelVerticalAlignment VerticalAlign
                {
                    set { this._VerticalAlign = value; }
                    get { return this._VerticalAlign; }
                }

                private ExcelHorizontalAlignment _HorizontalAlign = ExcelHorizontalAlignment.Center;
                public ExcelHorizontalAlignment HorizontalAlign
                {
                    set { this._HorizontalAlign = value; }
                    get { return this._HorizontalAlign; }
                }

                private ExcelBorderStyle _BorderTop = ExcelBorderStyle.Thin;
                public ExcelBorderStyle BorderTop
                {
                    set { this._BorderTop = value; }
                    get { return this._BorderTop; }
                }

                private ExcelBorderStyle _BorderRight = ExcelBorderStyle.Thin;
                public ExcelBorderStyle BorderRight
                {
                    set { this._BorderRight = value; }
                    get { return this._BorderRight; }
                }

                private ExcelBorderStyle _BorderBottom = ExcelBorderStyle.Thin;
                public ExcelBorderStyle BorderBottom
                {
                    set { this._BorderBottom = value; }
                    get { return this._BorderBottom; }
                }

                private ExcelBorderStyle _BorderLeft = ExcelBorderStyle.Thin;
                public ExcelBorderStyle BorderLeft
                {
                    set { this._BorderLeft = value; }
                    get { return this._BorderLeft; }
                }
            }

            public class SummaryColumns : List<SummaryColumn>
            {
                public void Add()
                {
                    this.Add(new SummaryColumn());
                }

            }
            private SummaryColumns _Column;

            public SummaryColumns Columns
            {
                get { return this._Column; }
            }

            public void NewColumn()
            {
                this._Column.Add();
            }
            public void NewColumn(int numberOfNewColumn)
            {
                for (int i = 0; i < numberOfNewColumn; i++)
                {
                    this._Column.Add();
                }
            }

            public class SummaryColumn : List<dynamic>
            {
                public SummaryColumn()
                {
                    //Contructor

                }

                private int _ColumnNumber = 0;
                public int ColumnNumber
                {
                    set { this._ColumnNumber = value; }
                    get { return this._ColumnNumber; }
                }

                private string _CellFormat = @"#,#00.000";
                public string CellFormat
                {
                    set { this._CellFormat = value; }
                    get { return this._CellFormat; }
                }

                private System.Drawing.Color _BlackgroundColor = System.Drawing.Color.White;
                public System.Drawing.Color BlackgroundColor
                {
                    set { this._BlackgroundColor = value; }
                    get { return this._BlackgroundColor; }
                }

                private bool _FontBold = true;
                public bool FontBold
                {
                    set { this._FontBold = value; }
                    get { return this._FontBold; }
                }

                private System.Drawing.Color _FontColor = System.Drawing.Color.Black;
                public System.Drawing.Color FontColor
                {
                    set { this._FontColor = value; }
                    get { return this._FontColor; }
                }

                private bool _FontItalic = false;
                public bool FontItalic
                {
                    set { this._FontItalic = value; }
                    get { return this._FontItalic; }
                }

                private bool _FontUnderLine = true;
                public bool FontUnderLine
                {
                    set { this._FontUnderLine = value; }
                    get { return this._FontUnderLine; }
                }

                private float _FontSize = 11;
                public float FontSize
                {
                    set { this._FontSize = value; }
                    get { return this._FontSize; }
                }

                private string _FontName = "calibli";
                public string FontName
                {
                    set { this._FontName = value; }
                    get { return this._FontName; }
                }

                private ExcelVerticalAlignment _VerticalAlign = ExcelVerticalAlignment.Bottom;
                public ExcelVerticalAlignment VerticalAlign
                {
                    set { this._VerticalAlign = value; }
                    get { return this._VerticalAlign; }
                }

                private ExcelHorizontalAlignment _HorizontalAlign = ExcelHorizontalAlignment.General;
                public ExcelHorizontalAlignment HorizontalAlign
                {
                    set { this._HorizontalAlign = value; }
                    get { return this._HorizontalAlign; }
                }

                private ExcelBorderStyle _BorderTop = ExcelBorderStyle.Thin;
                public ExcelBorderStyle BorderTop
                {
                    set { this._BorderTop = value; }
                    get { return this._BorderTop; }
                }

                private ExcelBorderStyle _BorderRight = ExcelBorderStyle.Thin;
                public ExcelBorderStyle BorderRight
                {
                    set { this._BorderRight = value; }
                    get { return this._BorderRight; }
                }

                private ExcelBorderStyle _BorderBottom = ExcelBorderStyle.Thin;
                public ExcelBorderStyle BorderBottom
                {
                    set { this._BorderBottom = value; }
                    get { return this._BorderBottom; }
                }

                private ExcelBorderStyle _BorderLeft = ExcelBorderStyle.Thin;
                public ExcelBorderStyle BorderLeft
                {
                    set { this._BorderLeft = value; }
                    get { return this._BorderLeft; }
                }

                private string _FormulaText = "SUM";
                public string FormulaText
                {
                    set { this._FormulaText = value; }
                    get { return this._FormulaText; }
                }
            }

        }


    }

    public class ExcelImport
    {

    }

}