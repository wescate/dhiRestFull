using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Text;

namespace RESTTest
{
    [TestClass]
    public class UnitTestUsuario
    {
        [TestMethod]
        public void TestMethod1()
        {

            // Prueba de obtención de usuario vía HTTP GET
            HttpWebRequest req2 = (HttpWebRequest)WebRequest
                .Create("http://localhost:59173/api/WSentUsuario/1");
            req2.Method = "GET";
            HttpWebResponse res2 = (HttpWebResponse)req2.GetResponse();
            StreamReader reader2 = new StreamReader(res2.GetResponseStream());
            string usuarioJson2 = reader2.ReadToEnd();
            JavaScriptSerializer js2 = new JavaScriptSerializer();
            Usuario usuarioObtenido = js2.Deserialize<Usuario>(usuarioJson2);
            Assert.AreEqual("1", usuarioObtenido.DTOUsuario.codUsuario);
            Assert.AreEqual("pepito", usuarioObtenido.DTOUsuario.nombres);

            // Prueba de creación de usuario vía HTTP POST
            string postdata = "{\"codUsuario\":\"13\",\"nombres\":\"Pedro\"}"; //JSON
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            HttpWebRequest req = (HttpWebRequest)WebRequest
                .Create("http://localhost:59173/api/WSentUsuario");
            req.Method = "POST";
            req.ContentLength = data.Length;
            req.ContentType = "application/json";
            var reqStream = req.GetRequestStream();
            reqStream.Write(data, 0, data.Length);
            HttpWebResponse res = null;
            
            try
            {
                res = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(res.GetResponseStream());
                string usuarioJson = reader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                Usuario usuarioCreado = js.Deserialize<Usuario>(usuarioJson);
                Assert.AreEqual("13", usuarioCreado.DTOUsuario.codUsuario);
                Assert.AreEqual("Pedro", usuarioCreado.DTOUsuario.nombres);
            }
            catch (WebException e)
            {
                HttpStatusCode code = ((HttpWebResponse)e.Response).StatusCode;
                string message = ((HttpWebResponse)e.Response).StatusDescription;
                StreamReader reader = new StreamReader(e.Response.GetResponseStream());
                string error = reader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                string mensaje = js.Deserialize<string>(error);
                Assert.AreEqual("Usuario imposible", mensaje);
            }

            // Prueba de delete de usuario vía HTTP DELETE
            string postdata4 = "{\"Codigo\":\"2\"}"; //JSON
            byte[] data4 = Encoding.UTF8.GetBytes(postdata4);
            HttpWebRequest req4 = (HttpWebRequest)WebRequest
                .Create("http://localhost:59173/api/WSentUsuario");
            req4.Method = "DELETE";
            req4.ContentLength = data4.Length;
            req4.ContentType = "application/json";
            var reqStream4 = req4.GetRequestStream();
            reqStream4.Write(data4, 0, data4.Length);
            var res4 = (HttpWebResponse)req4.GetResponse();
            StreamReader reader4 = new StreamReader(res4.GetResponseStream());
            string usuarioJson4 = reader4.ReadToEnd();
            JavaScriptSerializer js4 = new JavaScriptSerializer();
            Usuario usuarioEliminado = js4.Deserialize<Usuario>(usuarioJson4);
            Assert.AreEqual("2", usuarioEliminado.DTOUsuario.codUsuario);
            
        }
    }
}
