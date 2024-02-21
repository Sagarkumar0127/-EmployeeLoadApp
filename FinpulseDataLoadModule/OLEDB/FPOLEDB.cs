using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinpulseDataLoadModule.OLEDB
{
    public class FPOLEDB : IDisposable
    {
        private string SheetNameStartsWith;
        private OleDbConnection xlCon;
        public FPOLEDB(string filePath, string _SheetNameStartsWith)
        {
            try
            {
                xlCon = new OleDbConnection(this.GetConnectionstring(filePath));
                SheetNameStartsWith = _SheetNameStartsWith;
                xlCon.Open();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private string GetConnectionstring(string filePath)
        {
            string extension = Path.GetExtension(filePath);
            string conString = string.Empty;


            switch (extension)
            {
                case ".xls": //Excel 97-03 
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=yes;IMEX=1;\"";
                    break;
                case ".xlsx": //Excel 07 or higher                    
                    conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1;Connect Timeout=0;\"";
                    break;
            }
            return conString;


        }

        public string[] GetSheetNames()
        {

            try
            {
                string sheet1 = xlCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                DataTable dtSheetInfo = xlCon.GetSchema("Tables");
                List<string> lstsheetNames = new List<string>();
                Action<DataRow> actionToGetSheetName = (k) => { lstsheetNames.Add(k["TABLE_NAME"] + ""); };
                dtSheetInfo.Rows.OfType<DataRow>().ToList().ForEach(actionToGetSheetName);
                lstsheetNames = lstsheetNames.Where(k => !k.ToLower().Contains("filterdatabase")).ToList();
                return lstsheetNames.ToArray();
            }
            catch (Exception)
            {
                
                throw;
            }

        }

        public DataTable GetDataTableFromSheet(string sheetName)
        {
            try
            {
                DataTable dtExcel = new DataTable();
                string s = sheetName.Replace(@"'", string.Empty);
                s = s.Replace("$", "");
                using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT *  FROM [" + s + "$]", xlCon))
                {
                    oda.Fill(dtExcel);
                }
                return dtExcel;
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public void Dispose()
        {
            try
            {
                if (xlCon != null)
                    if (xlCon.State == ConnectionState.Open)
                        xlCon.Close();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
