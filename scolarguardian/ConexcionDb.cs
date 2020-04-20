using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scolarguardian
{
    class ConexcionDb
    {
        public MySqlConnection conexion { get; set; }    

        public MySqlConnection ObtenerConexion()
        {
            this.conexion = new MySqlConnection("Server="+ GlobalVar.server + ";Port="+ GlobalVar.puerto + ";Database="+ GlobalVar.databasename + ";Uid="+ GlobalVar.user + ";Pwd="+ GlobalVar.pass + ";");
            this.conexion.Open();
            return this.conexion;
        }
    }
}
