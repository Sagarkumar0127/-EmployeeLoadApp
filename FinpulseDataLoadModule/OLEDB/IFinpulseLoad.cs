using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinpulseDataLoadModule
{
    public interface IEmployeeLoadFile
    {
       string UserID { get; set; }
        void Load(string file);

        bool IsVerificationNeeded();
    }
}
