using FinpulseDataLoadModule.OLEDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinpulseDataLoadModule
{
    public class BufferAgeing : BaseEmployeeLoad
    {
        public BufferAgeing( ) 
        {
            currentFileEnum = FPLoadDDLEnum.PreproductionAgeingDetails;
            LoadName = FPLoadNames.BufferAgeing;
            SheetNameStartsWith = "PreproductionAgeingDetails";
        }
    }
}