using FinpulseDataLoadModule.OLEDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinpulseDataLoadModule
{
    public class ManPowerFile : BaseEmployeeLoad
    {
        public ManPowerFile( ) 
        {
            currentFileEnum = FPLoadDDLEnum.ManPowerReport;
            LoadName = FPLoadNames.ManPower_Emp;
            SheetNameStartsWith = "ManPowerReport";
        }
    }
}