using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinpulseDataLoadModule.OLEDB
{
    class Allocation: BaseEmployeeLoad
    {

        public Allocation()
        {
            SheetNameStartsWith = "DataSheet";
            currentFileEnum = FPLoadDDLEnum.AllocationLoad;
            LoadName = FPLoadNames.AlconAllocations;

        }
    }
}
