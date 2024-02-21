using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinpulseDataLoadModule.OLEDB;

namespace FinpulseDataLoadModule.OLEDB
{
    public class Bench : BaseEmployeeLoad
    {
        public Bench()
        {
            SheetNameStartsWith = "BenchAgeingDetails";
            currentFileEnum = FPLoadDDLEnum.BenchAgeingDetails;
            LoadName = FPLoadNames.BenchAgeing;
        }
    }
}
