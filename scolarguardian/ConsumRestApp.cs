using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace scolarguardian
{
    class ConsumRestApp
    {
        private string url = "http://" + GlobalVar.WebHost+"/";

        public dynamic Post(string uri, string json, string autorizacion = null)
            {
                try
                {
                    var client = new RestClient(url+uri);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/json");
                    request.AddParameter("application/json", json, ParameterType.RequestBody);

                    if (autorizacion != null)
                    {
                        request.AddHeader("Authorization", autorizacion);
                    }

                    IRestResponse response = client.Execute(request);

                    dynamic datos = JsonConvert.DeserializeObject(response.Content);

                

                    return datos;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return null;
                }
            }


            public dynamic Get(string uri)
            {
                HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(url+uri);
                myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
                //myWebRequest.CookieContainer = myCookie;
                myWebRequest.Credentials = CredentialCache.DefaultCredentials;
                myWebRequest.Proxy = null;
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
                Stream myStream = myHttpWebResponse.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myStream);
                //Leemos los datos
                string Datos = HttpUtility.HtmlDecode(myStreamReader.ReadToEnd());

                dynamic data = JsonConvert.DeserializeObject(Datos);

                return data;
            }
        
    }
}
