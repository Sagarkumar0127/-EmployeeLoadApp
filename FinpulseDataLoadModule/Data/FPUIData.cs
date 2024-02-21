using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinpulseDataLoadModule.Data
{
    public class FPUIData : BaseData
    {

        public List<KeyValuePair<string, string>> GetUploadItems()
        {
            try
            {
                List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();
                List<SqlParameter> param = new List<SqlParameter>();
                DataTable dt = GetDataTableFromQuery("select   DataLoad , ExcelName from [Data_Load] where id in (14,15,16,17,18)", param);
                dt.Rows.Cast<DataRow>().ToList().ForEach(row => lst.Add(new KeyValuePair<string, string>(row["DataLoad"] + "", row["ExcelName"] + "")));
                return lst;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

    }
}
