using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scolarguardian
{
    /*private string server = "201.163.99.83";
       private string puerto = "3307";
       private string databasename = "scolarguardian";
       private string user = "externo";
       private string pass = "0102261218";*/
    class GlobalVar
    {
        public static string server { get; set; } = "201.163.99.85";

        public static string WebHost { get; set; } = "scolarguardian.sytes.net";
        public static string puerto { get; set; } = "3306";
        public static string databasename { get; set; } = "scolarguardian";
        public static string user { get; set; } = "externo";
        public static string pass { get; set; } = "0102261218";
    }
}
