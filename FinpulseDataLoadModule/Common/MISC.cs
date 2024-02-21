using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinpulseDataLoadModule
{
    public enum FPLoadDDLEnum
    {
        ManPowerReport, PreproductionAgeingDetails, BenchAgeingDetails,
        EmployeeLoad,AllocationLoad
    }



    public class FPLoadNames
    {
      
        public const string AlconEmployeeLoad = "EmployeeReport";
        public const string AlconAllocations = "AllocationReport";
        public const string ManPower_Emp = "ManPowerReport";
        public const string BufferAgeing = "PreProductionReport";
        public const string BenchAgeing = "BenchReport";
    }


    public static class EnumExtension
    {
        public static T ToEnum<T>(this string value)
        {
            string finalVal = "";
            switch (value)
            {

                case "Employee Load":
                    finalVal = "EmployeeLoad";
                    break;
                case "Alcon Allocations to Activities":
                    finalVal = "AllocationLoad";
                    break;
                case "Bench Ageing Load":
                    finalVal = "BenchAgeingDetails";
                    break;
                case "Buffer Ageing Load":
                    finalVal = "PreproductionAgeingDetails";
                    break;
                case "Alcon Manpower Report":
                    finalVal = "ManPowerReport";
                    break;

                default:
                    finalVal = null;
                    break;
            }

            return (T)Enum.Parse(typeof(T), finalVal, true);
        }
    }
}