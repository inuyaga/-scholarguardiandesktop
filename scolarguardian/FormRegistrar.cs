using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scolarguardian
{
    public partial class FormRegistrar : Form
    {

        private DPFP.Template Template;
        //private UsuariosDBEntities contexto;
        public FormRegistrar()
        {
            InitializeComponent();
        }

        private void btn_registrar_Click(object sender, EventArgs e)
        {
            CapturarHuella captura = new CapturarHuella();
            captura.OnTemplate += this.OnTemplate;
            captura.ShowDialog();
        }

        private void OnTemplate(DPFP.Template template)
        {
            this.Invoke(new Function(delegate ()
            {
                Template = template;

                btn_agregar.Enabled = (Template != null);
                btn_registrar.Enabled = (Template == null);
                if (Template != null)
                {
                    MessageBox.Show("The fingerprint template is ready for fingerprint verification.", "Fingerprint Enrollment");
                    txt_huella.Text = "Huella capturada correctamente";
                    
                }
                else
                {
                    MessageBox.Show("The fingerprint template is not valid. Repeat fingerprint enrollment.", "Fingerprint Enrollment");
                }
            }));
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            byte[] streamHuella = Template.Bytes;
            Alumno alumno = new Alumno();
            alumno.ID = Int32.Parse(txt_id.Text);
            alumno.huellas = streamHuella;

            int retorno=AlumnoComand.Update(alumno);
            if (retorno > 0)
            {
                MessageBox.Show("Actualizacion Correcta");
                btn_registrar.Enabled = false;
                btn_agregar.Enabled = false;
                txt_id.Text = "";
                txt_nombre.Text = "";
                txt_huella.Text = "";
                //Console.WriteLine(streamHuella);

            }
            else {
                MessageBox.Show("Error");
            }
        }

        private void txt_id_Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    long id_alumo = long.Parse(txt_id.Text);
                    AlumnoComand cmd_query = new AlumnoComand();
                    Alumno al_consultado = AlumnoComand.GetAlumnoID(id_alumo);
                    string texto_al= al_consultado.nombres + " " + al_consultado.apellidos;
                    txt_nombre.Text = texto_al;
                    if (!String.IsNullOrEmpty(texto_al)) {
                        btn_registrar.Enabled = true;
                    }
                }
                catch (FormatException error_formst)
                {
                    MessageBox.Show(error_formst.Message);
                }
            }
        }
    }
}
