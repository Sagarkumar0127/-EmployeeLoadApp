using FinpulseDataLoadModule.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinpulseDataLoadModule.OLEDB
{
    public class EmployeeLoad: BaseEmployeeLoad
    {

        public EmployeeLoad()
        {
            SheetNameStartsWith = "EmployeeReport";
            currentFileEnum = FPLoadDDLEnum.EmployeeLoad;
            LoadName = FPLoadNames.AlconEmployeeLoad;
        }

        protected override void UpdateAfterLoad()
        {
            base.UpdateAfterLoad();
           // ActVsBdgMailer.GenerateMailer();

        }


    }
}
