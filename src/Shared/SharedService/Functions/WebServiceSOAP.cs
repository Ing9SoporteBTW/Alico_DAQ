using System;
using System.Collections.Generic;
using System.Net;
using System.Data;

namespace SharedService.Functions
{
    public class WebServiceSOAP
    {
        #region "Constructor"

        public WebServiceSOAP(string url, string methodName, string _xmlns, string _SOAPAction)
        {
            Url = url;
            MethodName = methodName;
            xmlns = _xmlns;
            SOAPAction = _SOAPAction;
        }

        #endregion

        #region "Propiedades"

        public string Url { get; set; }
        public string MethodName { get; set; }
        public string xmlns { get; set; }
        public string SOAPAction { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Dictionary<string, object> Params = new Dictionary<string, object>();
        //public XDocument ResultXML;        
        public string ResultString;

        #endregion

        /// <summary>
        /// Invokes service
        /// </summary>
        public void Invoke()
        {
            string soapStr = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                             "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">" +
                             "<soap:Body><{0} xmlns=\"" + xmlns + "\">{1}</{0}></soap:Body>" +
                             "</soap:Envelope>";

            //string user = "admin";
            //string pwd = "admin";

            string auth = string.Format("{0}", Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(string.Format("{0}:{1}", UserName, Password))));

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Url);
            req.PreAuthenticate = true;
            req.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
            req.Timeout = 1800000;//30 Minutos
                                  //req.ReadWriteTimeout = 120000;
                                  //req.ServicePoint.ConnectionLeaseTimeout = 120000;
                                  //req.ServicePoint.MaxIdleTime = 120000;

            req.Headers.Add("SOAPAction", "\"" + SOAPAction + "\"");
            req.Headers.Add(HttpRequestHeader.Authorization, auth);

            req.ContentType = "text/xml;charset=\"utf-8\"";
            req.Accept = "text/xml";
            req.Method = "POST";


            using (Stream stm = req.GetRequestStream())
            {
                string postValues = "";
                foreach (var param in Params)
                {
                    postValues += string.Format("<{0}>{1}</{0}>", param.Key, param.Value.ToString().Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&apos;").Replace("\"", "&quot;"));
                }

                soapStr = string.Format(soapStr, MethodName, postValues);

                using (StreamWriter stmw = new StreamWriter(stm))
                {
                    stmw.Write(soapStr);
                }
            }

            using (StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream()))
            {
                string result = responseReader.ReadToEnd();
                ResultString = result;//GetResponseValues(result);
            }
        }

        private string GetResponseValues(string XmlResponse)
        {
            string RespuestaDIAN = string.Empty;

            try
            {
                string StarChar = "<s:Envelope";
                string EndChar = "</s:Envelope>";

                int StarPosition = XmlResponse.IndexOf(StarChar);
                int EndPosition = XmlResponse.IndexOf(EndChar) + EndChar.Length;

                string XmlResponseFiltrado = XmlResponse.Substring(StarPosition, (EndPosition - StarPosition));

                DataSet DsResponse = new DataSet();
                DsResponse.ReadXml(new StringReader(XmlResponseFiltrado));

                //RespuestaDIAN += "Version:" + DsResponse.Tables["EnvioFacturaElectronicaRespuesta"].Rows[0]["Version"].ToString() + "\n" +
                //                 "ReceivedDateTime:" + DsResponse.Tables["EnvioFacturaElectronicaRespuesta"].Rows[0]["ReceivedDateTime"].ToString() + "\n" +
                //                 "ResponseDateTime:" + DsResponse.Tables["EnvioFacturaElectronicaRespuesta"].Rows[0]["ResponseDateTime"].ToString() + "\n" +
                //                 "Response Code:" + DsResponse.Tables["EnvioFacturaElectronicaRespuesta"].Rows[0]["Response"].ToString() + "\n" +
                //                 "Descripción:" + DsResponse.Tables["EnvioFacturaElectronicaRespuesta"].Rows[0]["Comments"].ToString() + "\n";
                //CodigoResponseDIAN = DsResponse.Tables["EnvioFacturaElectronicaRespuesta"].Rows[0]["Response"].ToString();

                return XmlResponseFiltrado;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

    }
}
