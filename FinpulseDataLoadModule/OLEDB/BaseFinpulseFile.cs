
using FinpulseDataLoadModule.Common;
using FinpulseDataLoadModule.Data;
using FinpulseDataLoadModule.OLEDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace FinpulseDataLoadModule
{
    public abstract class BaseEmployeeLoad : IEmployeeLoadFile 
    {
        protected FPData objFPData = new FPData();
        protected FPOLEDB objOLEDB;
        protected string SheetNameStartsWith = "Sheet";        
        protected string[] _sheetNames;
       protected FPLoadDDLEnum currentFileEnum;
        protected string FileNameWithExt;
        protected string LoadName;
        public string UserID { get; set; }
       

        public BaseEmployeeLoad()
        {
             
        }
         

      public  void Load( string file)
        {
            Init(file);
            DeleteBeforeLoad();
            LoadData();
            UpdateAfterLoad();
            UpdateTimeStamp();

        }

        public bool IsVerificationNeeded()
        {
            return false;
        }

        void Init(  string file)
        {

            try
            {
                FileNameWithExt = Path.GetFileName(file);
                objOLEDB = new FPOLEDB(file, SheetNameStartsWith);
                _sheetNames = objOLEDB.GetSheetNames();
            }
            catch (Exception)
            {
                 objOLEDB.Dispose();
                throw;
            }
        }

        protected virtual void UpdateTimeStamp()
        {

            try
            {
                objFPData.UpdateFileDateTimeStap(currentFileEnum.ToString(), FileNameWithExt, UserID, DateTime.Now);
                objOLEDB.Dispose();
            }
            catch (Exception)
            {
                objOLEDB.Dispose(); 
                throw;
            }
        }

        protected virtual void LoadData()
        {
            try
            {
                foreach (string sheetName in _sheetNames)
                {
                    if (sheetName.StartsWith(SheetNameStartsWith))
                    {
                        DataTable dt = objOLEDB.GetDataTableFromSheet(sheetName);

                        int dummy;

                        dt.Columns.OfType<DataColumn>().Where(k =>
                        {
                            return int.TryParse(k.ColumnName.Replace("F", ""), out dummy);
                        }).Select(k => k.ColumnName).ToList().ForEach(col => dt.Columns.Remove(col));

                        Factory.RemoveEmptyRows(dt);

                        objFPData.uploadFin(dt, LoadName);
                      if (LoadName==FPLoadNames.AlconEmployeeLoad)
                        {
                          //  ActVsBdgMailer.GenerateMailer();
                        }
                            
                    }
                }
            }
            catch (Exception)
            {

                objOLEDB.Dispose();

                throw;
            }
        }


        protected virtual void DeleteBeforeLoad()
        {
            objFPData.DeleteEmployeeData(LoadName);
        }
        protected virtual void UpdateAfterLoad()
        {
            objFPData.UpdateAfterLoad(LoadName);
        } 

        
    }
}