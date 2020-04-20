using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scolarguardian
{
    public partial class FomrIdentificar : CaptureForm
    {
        private DPFP.Template Template;
        private DPFP.Verification.Verification Verificator;


        public void Verify(DPFP.Template template)
        {
            Template = template;
            ShowDialog();
        }

        protected override void Init()
        {
            base.Init();
            base.Text = "Verificación de Huella Digital";
            Verificator = new DPFP.Verification.Verification();     // Create a fingerprint template verificator
            UpdateStatus(0);
        }

        private void UpdateStatus(int FAR)
        {
            // Show "False accept rate" value
            SetStatus(String.Format("False Accept Rate (FAR) = {0}", FAR));
        }


        protected override void Process(DPFP.Sample Sample)
        {
            base.Process(Sample);

            // Process the sample and create a feature set for the enrollment purpose.
            DPFP.FeatureSet features = ExtractFeatures(Sample, DPFP.Processing.DataPurpose.Verification);

            // Check quality of the sample and start verification if it's good
            // TODO: move to a separate task
            if (features != null)
            {

     

                // Compare the feature set with our template
                DPFP.Verification.Verification.Result result = new DPFP.Verification.Verification.Result();

                DPFP.Template template = new DPFP.Template();
                Stream stream;

                AlumnoComand al = new AlumnoComand();
                MySqlDataReader alumnos = al.Resultado();
                ConsumRestApp res_api = new ConsumRestApp();

                while (alumnos.Read())
                {
                    byte[] data=(byte[])alumnos["al_huella"];
                    stream = new MemoryStream(data);
                    template = new DPFP.Template(stream);

                    Verificator.Verify(features, template, ref result);
                    UpdateStatus(result.FARAchieved);
                    if (result.Verified)
                    {
                        MakeReport("Alumno Verificado. " + alumnos["al_nombres"] + " "+ alumnos["al_apellidos"]);
                        // string resultado = post.AsistenciaCreate(alumnos["id"].ToString());
                        AsistenciaObj asistencia = new AsistenciaObj {
                            asis_user = alumnos["id"].ToString(),
                           
                        };
                        string json = JsonConvert.SerializeObject(asistencia);
                        dynamic respuesta = res_api.Post("ctr/asist/", json);
                        if (respuesta != null)
                        {
                            //dynamic asisten = JsonConvert.DeserializeObject(respuesta.ToString());
                           
                            foreach (var item in respuesta) {
                                if (item.Name.ToString() == "asis_tipo_evento") {
                                    switch (item.Value.ToString())
                                    {
                                        case "1":
                                            MakeReport("ENTRADA");
                                            break;
                                        case "2":
                                            MakeReport("SALIDA");
                                            break;
                                    }
                                }

                                if (item.Name.ToString() == "asis_tipo_tiempo")
                                {
                                    switch (item.Value.ToString())
                                    {
                                        case "1":
                                            MakeReport("A tiempo");
                                            break;
                                        case "2":
                                            MakeReport("Tolerancia");
                                            break;
                                        case "3":
                                            MakeReport("Fuera de rango de horario");
                                            break;
                                        case "4":
                                            MakeReport("Antes de tiempo");
                                            break;
                                    }
                                }
                            }
                        }
                        else {
                            MakeReport("No se obtuvo respuesta del servidor...");
                        }
                        
                        break;
                    }
                }

                alumnos.Close();
                
                


                /*
                foreach (var emp in contexto.Empleadoes)
                {
                    stream = new MemoryStream(emp.Huella);
                    template = new DPFP.Template(stream);

                    Verificator.Verify(features, template, ref result);
                    UpdateStatus(result.FARAchieved);
                    if (result.Verified)
                    {
                        MakeReport("The fingerprint was VERIFIED. " + emp.Nombre);
                        break;
                    }
                }
                */


            }
        }
        public FomrIdentificar()
        {
            InitializeComponent();
        }
    }
}
