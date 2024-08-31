using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCMS_DataAccess
{
    public class clsDataAccessSettings
    {
        public static string ConnectionString = "Server=.;Database=HCMS;User Id=sa;Password=sa123456";

        public static bool CheckIsNullOrEmpty(string Value)
        {
            return string.IsNullOrWhiteSpace(Value) || string.IsNullOrEmpty(Value);
        }

    }
}
