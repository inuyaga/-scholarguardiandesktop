using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scolarguardian
{
    class AlumnoComand
    {
        public static int Update(Alumno alumno) {

            int retorno = 0;
            String TexFormat = string.Format("UPDATE ctrl_escolar_alumno SET al_huella=@huella WHERE id='{0}'", alumno.ID);
            ConexcionDb conex = new ConexcionDb();
            MySqlCommand comando = new MySqlCommand(TexFormat, conex.ObtenerConexion());
            comando.Parameters.Add("@huella", MySqlDbType.Blob).Value = alumno.huellas;
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static Alumno GetAlumnoID(long ID)
        {
            MySqlDataReader consultar;
            String TexFormat = "SELECT * FROM ctrl_escolar_alumno WHERE id = @id_alumno";
            ConexcionDb conex = new ConexcionDb();
            MySqlConnection Conexion = conex.ObtenerConexion();
            MySqlCommand comando = new MySqlCommand(TexFormat, Conexion);
            comando.Parameters.Add("@id_alumno", MySqlDbType.Int64).Value = ID;

            consultar = comando.ExecuteReader();
            Alumno new_alumno = new Alumno();
            while (consultar.Read())
            {
                new_alumno.ID = (int)consultar["id"];
                new_alumno.nombres = (string)consultar["al_nombres"];
                new_alumno.apellidos = (string)consultar["al_apellidos"];
            }
            Conexion.Close();


            return new_alumno;
        }

        public  MySqlDataReader Resultado() {
            MySqlDataReader consultar;
            String TexFormat = "SELECT * FROM ctrl_escolar_alumno WHERE al_huella IS NOT NULL";
            ConexcionDb conex = new ConexcionDb();
            MySqlConnection Conexion = conex.ObtenerConexion();
            MySqlCommand comando = new MySqlCommand(TexFormat, Conexion);
            consultar = comando.ExecuteReader();
            
            return consultar;
        }

    }
}
