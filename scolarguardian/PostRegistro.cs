using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.IO;

namespace scolarguardian
{
    class PostRegistro
    {
        private string url = "http://"+GlobalVar.WebHost+ "/ctr/asist/";
        public string AsistenciaCreate(string id) {
            string resultante = "";

            WebRequest oResquest = WebRequest.Create(url + "ctr/asist/");
            oResquest.Method = "post";
            oResquest.ContentType = "application/json;charset=utf-8";

            using (var osw = new StreamWriter(oResquest.GetRequestStream()))
            {
                string json = "{asis_user:1, asis_tipo_evento:1, asis_tipo_tiempo:1}";
                osw.Write(json);
                osw.Flush();
                osw.Close();
            }

            WebResponse oResponse = oResquest.GetResponse();
            using (var oSR = new StreamReader(oResponse.GetResponseStream())) {
                resultante = oSR.ReadToEnd().Trim();
            }

            return resultante;
        }
    }
}
