using FinpulseDataLoadModule.OLEDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FinpulseDataLoadModule.Data
{
    public class FPData : BaseData
    {

        public void UpdateFileDateTimeStap(String fileName, String packageName, String userID, DateTime date)
        {
            try
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@FileName", fileName));
                param.Add(new SqlParameter("@PackageName", packageName));
                param.Add(new SqlParameter("@UserId", userID));
                param.Add(new SqlParameter("@timestamp", date));
                ExecuteSQLSP("SPROC_UpdateDataLoadTracker_test", param);
            }
            catch (Exception)
            {
                
                throw;
            }
        }


      

        public void UpdateEmp(string statement)
        {
            ExecuteSqlStatement(statement);

        }




        public void DeleteEmployeeData(string EmpLoadName)
        {
            try
            {
                string spName = "sp_Before_EmployeeLoad";
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@LoadName", EmpLoadName));
                ExecuteSQLSP(spName, param);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public void DeleteRevProdData(string csvYearMonth)
        {
            try
            {
                string spName = string.Format("delete from  [BEFinPulseData] where [YearMonth] in({0}) ", csvYearMonth);
                List<SqlParameter> param = new List<SqlParameter>();
                ExecuteSQLStatement(spName, param);
            }
            catch (Exception)
            {
                
                throw;
            }
        }


        public void UpdateAfterLoad(string loadName)
        {
            try
            {
                string spName = "sp_Updates_AfterLoad_EmployeeLoad";
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@LoadName", loadName));
                ExecuteSQLSP(spName, param);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateAfterLoadRevProd(int yearMonth)
        {

            try
            {
                string spName = "SP_finpulseData_ETLUpdates";
                List<SqlParameter> param = new List<SqlParameter>();

                param.Add(new SqlParameter("@YearMonth", yearMonth.ToString()));
                ExecuteSQLSP(spName, param);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void BE_Backup_Before_Update(int yearMonth)
        {

            try
            {
                string spName = "EAS_spBEFinpulseActuals_BE_Backup_PreLoad";
                List<SqlParameter> param = new List<SqlParameter>();

                param.Add(new SqlParameter("@YearMonth", yearMonth.ToString()));
                ExecuteSQLSP(spName, param);
            }
            catch (Exception)
            {

                throw;
            }
        }



        public void uploadFin(DataTable dt, string loadName)
        {

            try
            {
                Dictionary<string, string> dictMapping = new Dictionary<string, string>(); // LoadName,SP Name
              
                dictMapping.Add(FPLoadNames.AlconEmployeeLoad, "sp_InsertEmp_EmployeeLoad");
                dictMapping.Add(FPLoadNames.ManPower_Emp, "sp_Insert_Manpower_EmployeeLoad");
                dictMapping.Add(FPLoadNames.AlconAllocations, "sp_InsertAllocation_EmployeeLoad");
                dictMapping.Add(FPLoadNames.BufferAgeing, "sp_insert_buffer_EmployeeLoad");
                dictMapping.Add(FPLoadNames.BenchAgeing, "sp_insert_bench_EmployeeLoad");
                List<SqlParameter> param = new List<SqlParameter>();
                string spName = dictMapping[loadName];
                param.Add(new SqlParameter("@tblFin", dt));
                ExecuteSQLSP(spName, param);
            }
            catch (Exception)
            {
                
                throw;
            }

        }


    }
}