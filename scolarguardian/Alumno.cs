using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scolarguardian
{
    class Alumno
    {
        public long ID { get; set; }
        public String nombres { get; set; }
        public String apellidos { get; set; }

        public byte[] huellas { get; set; }

        public Alumno() { }

        public Alumno(int id, String nombre, String apellido, byte[] huella) {
            this.ID = id;
            this.nombres = nombre;
            this.huellas = huella;
        }

    }
}
