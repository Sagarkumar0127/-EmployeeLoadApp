using FinpulseDataLoadModule.OLEDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinpulseDataLoadModule.Common
{
   public  class Factory
    {

       public   static IEmployeeLoadFile GetFPLoadObject(FPLoadDDLEnum fpFile)
       {
           IEmployeeLoadFile obj;
           switch (fpFile)
           {
    
                case FPLoadDDLEnum.EmployeeLoad:
                    obj = new EmployeeLoad();
                    break;
                case FPLoadDDLEnum.AllocationLoad:
                    obj = new Allocation();
                    break;
                case FPLoadDDLEnum.ManPowerReport:
                    obj = new ManPowerFile();
                    break;
                case FPLoadDDLEnum.PreproductionAgeingDetails:
                    obj = new BufferAgeing();
                    break;
                case FPLoadDDLEnum.BenchAgeingDetails:
                    obj = new Bench();
                    break;

                default:
                   obj = null;
                   break;
           }
           return obj;

       }

        public static void RemoveEmptyRows(DataTable dt)
        {
            for (int i = dt.Rows.Count; i >= 1; i--)
            {
                int index = i - 1;
                DataRow row = dt.Rows[index];
                bool isRowEmpty = IsRowEmptyRow(row);
                if (isRowEmpty)
                    dt.Rows[index].Delete();
            }
            dt.AcceptChanges();
        }

        public static bool IsRowEmptyRow(DataRow row)
        {
            bool isAllEmpty = row.ItemArray.Select(k => (k + "").Trim()).All(k => string.IsNullOrEmpty(k));
            if (isAllEmpty)
                return true;
            else
                return row.ItemArray.Length == row.ItemArray.Select(k => (k + "").Trim()).Count(k => k.Length == 0) + 1;
        }

    }
}
