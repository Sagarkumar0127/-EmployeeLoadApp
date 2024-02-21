using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
 
using Excel = Microsoft.Office.Interop.Excel;
using VBIDE = Microsoft.Vbe.Interop;

namespace FinpulseDataLoadModule.Data
{
    public class ActVsBdgMailer
    {
        public ActVsBdgMailer()
        {
        }

        public static void GenerateMailer( )
        {
            string path = ConfigurationManager.AppSettings["ExcelOperations"];
            string item = ConfigurationManager.AppSettings["EmailCSV"];
            //item = "karthik_mahalingam01";
            string[] strArrays = item.Split(new char[] { ',' });
            DataSet ds = ActVsBdgMailer.GetData();
            string dateTimeStamp = ActVsBdgMailer.GetDateTimeStamp();
            string attachment = ActVsBdgMailer.GenerateAndSaveFile(path);


             //strArrays.ToList<string>().ForEach((string email) =>
            //{
                string hTML = ActVsBdgMailer.GetHTML(ds, "GLN", dateTimeStamp);
                string subject = string.Concat("Budget / Actuals : Sales Support, Solutions, Practice Sales as on (", dateTimeStamp, ")");
                ActVsBdgMailer.SendMail("", subject, hTML , attachment);
            //});
     
        }

        private static DataTable GetAttachmentData_CS()
        {
            string item = ConfigurationManager.ConnectionStrings["FPLoadConnectionString"].ConnectionString;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand("SP_PracticeSales_HC_Summary_ClientServices", new SqlConnection(item)));
            DataTable ds = new DataTable();
            
            sqlDataAdapter.Fill(ds);
            return ds;
        }
        private static DataTable GetAttachmentData_NCS()
        {
            string item = ConfigurationManager.ConnectionStrings["FPLoadConnectionString"].ConnectionString;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand("SP_PracticeSales_HC_Summary_Non_ClientServices", new SqlConnection(item)));
            DataTable ds = new DataTable();
            sqlDataAdapter.Fill(ds);
            return ds;
        }

        public static string GenerateAndSaveFile( string path)
        {
            
            DataTable dtCS = GetAttachmentData_CS();
            DataTable dtNCS = GetAttachmentData_NCS();


           // string folder = "D:\\Temp Work\\FinpulseDataLoadAsp\\FinpulseDataLoadAsp\\ExcelOperations\\";
            var myDir = new DirectoryInfo(path);


            string filename = "PracticeSales_HC_Summary" +    "_" + DateTime.Now.ToString("ddMMMyyyy_HHmm") + "IST.xlsx";
            String downloadFileTempPath = Path.Combine(myDir.FullName, filename);
            String downloadFile = downloadFileTempPath;

            //if (myDir.GetFiles().SingleOrDefault(k => k.Name == filename) != null)
            //{
            //    System.IO.File.Delete(downloadFile);
            //}

            Microsoft.Office.Interop.Excel.Application oExcel = null;
            Microsoft.Office.Interop.Excel.Workbook oBook = default(Microsoft.Office.Interop.Excel.Workbook);
            Microsoft.Office.Interop.Excel.Sheets ws = null;


            Excel.Worksheet sheet_CS = null;
            Excel.Worksheet sheet_NCS = null;
             
            VBIDE.VBComponent oModule;
            String sCode;
            Object oMissing = System.Reflection.Missing.Value;

            //instance of excel
            oExcel = new Microsoft.Office.Interop.Excel.Application();
            string templatePath = myDir.FullName + "\\Template\\" + "PracticeSales_HC_Summary_Template.xlsx" + "";
            oBook = oExcel.Workbooks.
                Open(templatePath, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            ws = oBook.Sheets;

            //File.WriteAllText(Server.MapPath("aa.txt"), templatePath);


            sheet_CS = ws.Item["Client Services"];
            sheet_NCS = ws.Item["Non-Client Services"];
            //sheet_TrendData = ws.Item["Trend_Data"];

            //sheet_RPP.Visible = Excel.XlSheetVisibility.xlSheetHidden;

            FillExcelSheet(dtCS, sheet_CS);
            FillExcelSheet(dtNCS, sheet_NCS);
           RefreshPivots(ws);

            //oModule = oBook.VBProject.VBComponents.Add(VBIDE.vbext_ComponentType.vbext_ct_StdModule);
            //sCode = "sub NSO_Pipeline()\r\n" +
            // System.IO.File.ReadAllText(myDir.FullName + "\\NSO_Pipeline.txt") + "\nend sub";

            //oModule.CodeModule.AddFromString(sCode);

            //oExcel.GetType().InvokeMember("Run",
            //                System.Reflection.BindingFlags.Default |
            //                System.Reflection.BindingFlags.InvokeMethod,
            //                null, oExcel, new string[] { "NSO_Pipeline" });


            //oBook.Activate();
            //oBook.Permission.Enabled = true;
            //oBook.Permission.RemoveAll();
            //string strExpiryDate = DateTime.Now.AddDays(60).Date.ToString();
            //DateTime dtTempDate = Convert.ToDateTime(strExpiryDate);
            //DateTime dtExpireDate = new DateTime(dtTempDate.Year, dtTempDate.Month, dtTempDate.Day);
            //UserPermission userper = oBook.Permission.Add("Everyone", MsoPermission.msoPermissionChange);
            //userper.ExpirationDate = dtExpireDate;

            oExcel.DisplayAlerts = false;
            oBook.SaveAs(downloadFile);


            oBook.Close(false, templatePath, null);


            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(sheet_CS);
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(sheet_NCS);
            sheet_CS = null;
            sheet_NCS = null;


            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(ws);
            ws = null;
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(oBook);
            oBook = null;

            oExcel.Quit();
            oExcel = null;

            GC.Collect();

            GC.WaitForPendingFinalizers();


            GC.Collect();
            GC.WaitForPendingFinalizers();
            return downloadFile;
        }


        private static DataSet GetData()
        {
            string item = ConfigurationManager.ConnectionStrings["FPLoadConnectionString"].ConnectionString;
            var cmd = new SqlCommand("sp_EmpLoad_BudgetMailerBody", new SqlConnection(item));
            cmd.CommandTimeout = int.MaxValue;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sqlDataAdapter.Fill(ds);
            return ds;
        }

        private static string GetDateTimeStamp()
        {
            string item = ConfigurationManager.ConnectionStrings["FPLoadConnectionString"].ConnectionString;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand(" select distinct asofdate from emp  ", new SqlConnection(item)));
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            DateTime dateTime = (DateTime)dataTable.Rows[0][0];
            return dateTime.ToString("dd-MMM-yyyy");
        }

        //static string GetPath([CallerFilePath]string fileName = null)
        static string GetPath( )
        {
            string path = ConfigurationManager.AppSettings["MailTemplatePath"];
            return path; 
           // return fileName;
        }

        private static string GetHTML(DataSet ds, string to, string asonDate)
        {
            // string currDir = Path.GetDirectoryName(GetPath());
            string currDir =  GetPath();
            string fileMailTemplate = Path.Combine(currDir, "mailTemplate.html");
            string htmlTemplate = File.ReadAllText(fileMailTemplate);

            string table1 = GetTableHtml(ds.Tables[0]);
            string table2 = GetTableHtml(ds.Tables[1]);
            string table3 = GetTableHtml(ds.Tables[2]);



            string final = htmlTemplate.Replace("{----}",to).Replace("{++++}", asonDate)
                .Replace("{1111}", table1).Replace("{2222}", table2).Replace("{3333}", table3);
            return final;
        }

        private static string GetTableHtml(DataTable dt)
        {


            StringBuilder stringBuilder = new StringBuilder();
            string tdFormat = "<td>{0}</td>";
            string tdRedFormat = "<td style='background-color:#fce4d6'>{0}</td>";
            string tdSLIndendFormat = "<td style='text-align:left;padding-left:25px'>{0}</td>";
            string tdSLFormat = "<td style='text-align:left'>{0}</td>";
            string tdGreyBold = "<td style='background-color: #e7e6e6;font-weight:bold; '>{0}</td>";
            string tdGreyBoldPercent = "<td style='background-color: #e7e6e6;font-weight:bold; '>{0} %</td>";
            string tdGreyBoldLA = "<td style='text-align:left;background-color: #e7e6e6;font-weight:bold; '>{0}</td>";

            bool flag = true;
            foreach (DataRow row in dt.Rows)
            {
                stringBuilder.AppendLine("<tr class='Medium0' > ");
                foreach (DataColumn column in dt.Columns)
                {
                    string cellFormat = tdFormat;
                    string value = string.Concat(row[column]);

                    if (new string[] { "PercentOverallH", "PercentOverallSL" }.Contains(column.ColumnName))
                    {
                        double _val = Convert.ToDouble(value);
                        value = string.Format("{0:0.0}", _val);
                    }

                    //string[] colsToRed = new string[] { "EmpPu_Diff_Total", "ProjPu_Diff_Total" };
                    string[] colsToRed = new string[] {  "EmpPu_Diff_Total", "ProjPu_Diff_Total" ,"TotalSLGap","OnsiteSLGap","OffshoreSLGap",
"TotalHCGap","OnsiteHCGap","OffshoreHCGap"  ,"TotalGap", "OnsiteGap", "OffshoreGap" ,
"Gap Total",   "Gap Onsite" , "Gap Offshore"
                    };
                    //string[] colsPercentage = new string[] { "TotalSLGap", "OnsiteSLGap", "OffshoreSLGap", "TotalHCGap", "OnsiteHCGap", "OffshoreHCGap",
                    //    "TotalGap", "OnsiteGap", "OffshoreGap"};
                    string[] colsPercentage = new string[] { };

                    if (colsToRed.Contains(column.ColumnName))
                        if(value != "")
                        if (Convert.ToDouble(value) > 0)
                            cellFormat = tdRedFormat;

                    if (colsPercentage.Contains(column.ColumnName))
                        cellFormat = tdGreyBoldPercent;


                    if (column.ColumnName == "SL")
                        cellFormat = value == "EAS" ? tdSLFormat : tdSLIndendFormat;

                    if (flag)
                        if (column.ColumnName == "SL")
                            cellFormat = value == "EAS" ? tdGreyBoldLA : tdGreyBold;
                        else
                        {
                            if (colsPercentage.Contains(column.ColumnName))
                                cellFormat = tdGreyBoldPercent;
                            else
                                cellFormat = tdGreyBold;
                        }

                    stringBuilder.AppendFormat(cellFormat, value);
                }
                flag = false;
                stringBuilder.AppendLine("</tr> ");
            }
            return stringBuilder.ToString();
        }

        private static void SendMail(string To, string subject, string body, string attachment)
        {
            MailMessage mailMessage = new MailMessage();
            try
            {
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = body;
                 
              mailMessage.Attachments.Add(new Attachment(attachment));

                mailMessage.To.Clear();
                mailMessage.CC.Clear();
                mailMessage.Bcc.Clear();

                string[] ToList = ConfigurationManager.AppSettings["ToList"].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                string[] CCList = ConfigurationManager.AppSettings["CCList"].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                string[] BCCList = ConfigurationManager.AppSettings["BCCList"].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string email in ToList)
                    mailMessage.To.Add(new MailAddress(email + "@infosys.com"));
                foreach (string email in CCList)
                    mailMessage.CC.Add(new MailAddress(email + "@infosys.com"));
                foreach (string email in BCCList)
                    mailMessage.Bcc.Add(new MailAddress(email + "@infosys.com"));


                SmtpClient smtpClient = new SmtpClient()
                {
                    Host = ConfigurationManager.AppSettings["Host"],
                    EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"])
                };
                NetworkCredential networkCredential = new NetworkCredential()
                {
                    UserName = ConfigurationManager.AppSettings["UserName"],
                    Password = ConfigurationManager.AppSettings["Password"]
                };
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = networkCredential;
                smtpClient.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                try
                {
                    smtpClient.Send(mailMessage);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Mail Sent Successfully to {0}", To);
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(exception.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            finally
            {
                if (mailMessage != null)
                {
                    ((IDisposable)mailMessage).Dispose();
                }
            }
        }

        public static void RefreshPivots(Microsoft.Office.Interop.Excel.Sheets excelsheets)
        {

            foreach (Microsoft.Office.Interop.Excel.Worksheet pivotSheet in excelsheets)
            {
                Microsoft.Office.Interop.Excel.PivotTables pivotTables = (Microsoft.Office.Interop.Excel.PivotTables)pivotSheet.PivotTables();
                int pivotTablesCount = pivotTables.Count;
                if (pivotTablesCount > 0)
                {
                    for (int i = 1; i <= pivotTablesCount; i++)
                    {
                        Microsoft.Office.Interop.Excel.PivotTable pivotTable = pivotTables.Item(i);
                        pivotTable.RefreshTable();
                        //CollapseFields(pivotSheet, pivotTable);
                    }
                }
            }
        }
 

        public static void FillExcelSheet(DataTable dt, Microsoft.Office.Interop.Excel.Worksheet excel)
        {

            try
            {
                // Copy the DataTable to an object array
                object[,] rawData = new object[dt.Rows.Count, dt.Columns.Count];

                // Copy the column names to the first row of the object array
                //for (int col = 0; col < dt.Columns.Count; col++)
                //{
                //    rawData[0, col] = dt.Columns[col].ColumnName;
                //}

                // Copy the values to the object array
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        // rawData[row + 1, col] = dt.Rows[row].ItemArray[col];
                        rawData[row, col] = dt.Rows[row][col];
                    }
                }

                Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)excel.Cells[2, 1];
                Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)excel.Cells[dt.Rows.Count + 1, dt.Columns.Count];
                Microsoft.Office.Interop.Excel.Range range_excel = excel.get_Range(c1, c2);

                //Fill Array in Excel
                range_excel.Value2 = rawData;
                range_excel.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                range_excel.Interior.Pattern = Microsoft.Office.Interop.Excel.XlPattern.xlPatternSolid;
                range_excel.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);


                ReleaseObject(range_excel);
                ReleaseObject(c2);
                ReleaseObject(c1);
                ReleaseObject(excel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void ReleaseObject(object o)
        {
            try
            {
                if (o != null)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            }
            catch (Exception) { }
            finally { o = null; }
        }
    }
}