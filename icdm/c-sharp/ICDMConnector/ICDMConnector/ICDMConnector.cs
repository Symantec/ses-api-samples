using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Net;
using System.Xml;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Util;
using System.Data;
using System.Threading;

namespace Symantec.ICDM
{

    public class ICResponse
    {
        public ICResponse(string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var jsonObject = serializer.Deserialize<dynamic>(json);
            Raw = json;
            Response = jsonObject;
            //ResultCode = (string)jsonObject["resultCode"];
        }

        public string Raw { get; set; }
        public Array Response { get; set; }
        //public string ResultCode { get; set; }
    }

    public class ICResult
    {
        public ICResult(string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var jsonObject = serializer.Deserialize<dynamic>(json);
            Raw = json;
            Result = jsonObject;
            //ResultCode = (string)jsonObject["resultCode"];
        }

        public string Raw { get; set; }
        public Dictionary<string, object> Result { get; set; }
        //public string ResultCode { get; set; }
    }


    public class ICDMConnector
    {

        public enum RequestMethods
        {
            GET,
            POST,
        }

        public enum Protocols
        {
            HTTP,
            HTTPS,
        }

        private string _ClientId;
        private string _ClientSecret;
        private string _APIHost;
        private string _CustomerId;
        private string _DomainId;
        private Protocols _Protocol = Protocols.HTTPS;
        private RequestMethods _RequestMethod;
        private string _URL;
        private string _Data;
        private Boolean _Download;
        private string _Directory;
        private string _AccessToken;

        private string result = null;
        private string JSONResult = null;
        private string responseValue = null;
        private string resultString = null;

        public string ClientId
        {
            get { return this._ClientId; }
            set { this._ClientId = value; }
        }

        public string ClientSecret
        {
            get { return this._ClientSecret; }
            set { this._ClientSecret = value; }
        }

        public string APIHost
        {
            get { return this._APIHost; }
            set { this._APIHost = value; }
        }

        public string CustomerId
        {
            get { return this._CustomerId; }
            set { this._CustomerId = value; }
        }

        public string DomainId
        {
            get { return this._DomainId; }
            set { this._DomainId = value; }
        }

        public string AccessToken
        {
            get { return this._AccessToken; }
            set { this._AccessToken = value; }
        }

        public Protocols Protocol
        {
            get { return this._Protocol; }
            set { this._Protocol = value; }
        }

        public RequestMethods RequestMethod
        {
            get { return this._RequestMethod; }
            set { this._RequestMethod = value; }
        }

        public string URL
        {
            get { return this._URL; }
            set { this._URL = value; }
        }

        public string Data
        {
            get { return this._Data; }
            set { this._Data = value; }
        }

        public Boolean Download
        {
            get { return this._Download; }
            set { this._Download = value; }
        }

        public string Directory
        {
            get { return this._Directory; }
            set { this._Directory = value; }
        }

        static ManualResetEvent allDone = new ManualResetEvent(false);


        public string GetAccessToken()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Uri uri = new Uri(this.Protocol + "://" + this.APIHost + "/v1/oauth2/tokens");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri) as HttpWebRequest;

            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(this.ClientId + ":" + this.ClientSecret));

            request.Headers.Add("Authorization", "Basic " + encoded);
            request.Accept = "application/json";
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Host = this.APIHost;

            WebResponse response = null;
            response = request.GetResponse();
            Stream rs = response.GetResponseStream();
            StreamReader reader = new StreamReader(rs);

            JSONResult = reader.ReadToEnd();

            rs.Close();
            reader.Close();

            ICResult scr = new ICResult(JSONResult);
            this.AccessToken = scr.Result["access_token"].ToString();
            return this.AccessToken;
        }

        public string CallAPI(string method, string url, String Data, bool download=false)
        {
            var baseUrl = "https://" + this.APIHost + url;
            return Request(method, baseUrl, Data, download);
        }


        public string Request(string method, string url, string Data, bool download=false)
        {
            try
            {
                var encoding = new UTF8Encoding();
                var bytes = Encoding.UTF8.GetBytes(Data);

                this.AccessToken = GetAccessToken();

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                Uri uri = new Uri(url);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri) as HttpWebRequest;

                request.Headers.Add("Authorization", "Bearer " + this.AccessToken);
                request.Headers.Add("x-epmp-customer-id", this.CustomerId);
                request.Headers.Add("x-epmp-domain-id", this.DomainId);

                request.Method = method;
                request.Accept = "application/json";
                request.ContentType = "application/json";
                request.AllowAutoRedirect = true;
                request.Host = this.APIHost;
                                
                if (Data.Length > 0)
                {
                    StreamWriter requestWriter = new StreamWriter(request.GetRequestStream());
                    requestWriter.Write(Data);
                    requestWriter.Close();

                    /*
                    // Byte Array Post
                    byte[] byteArray = Encoding.UTF8.GetBytes(Data); 
                    request.ContentLength = byteArray.Length;
                    request.ContentType = "application/x-www-form-urlencoded";
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                    
                    // Serialized Key/Value Pair Post 
                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    Dictionary<string, object> kv = ser.Deserialize<Dictionary<string, object>>(Data);
                    requestValue += BuildDataPost(kv, method.ToString());
                     
                    // File Post
                    if (download == true)
                    {
                        requestValue += BuildFilePost(kv);
                        request.ContentType = "application/x-www-form-urlencoded";
                        request.ContentLength = requestValue.Length;
                    }
                    */
                }
                               
                WebResponse response = null;
                response = request.GetResponse();
                Stream rs = response.GetResponseStream();
                StreamReader reader = new StreamReader(rs);
                JSONResult = reader.ReadToEnd();

                ICResult scr = new ICResult(JSONResult);
                result = scr.Raw;

                rs.Close();
                reader.Close();
            }
            catch (Exception ex)
            {
                result += "\r\n" + "ERROR: \r\n" + method + "\r\n" + url.ToString() + "\r\n\r\n" + ex.Message.ToString() + "\r\n\r\n" + ex.StackTrace.ToString() + "\r\n\r\nResponse: " + responseValue;
            }
            return result;  
        }

        
        #region BuildDataPost

        public String BuildDataPost(Dictionary<string, object> dict, string Method)
        {
            string DataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n{2}";

            try
            {
                string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                string boundaryString = "\r\n--" + boundary + "\r\n";

                //string hostName = Dns.GetHostName(); 
                //string APIHost = Dns.GetHostEntry(hostName).AddressList[0].ToString();

                resultString += "Content-Length: " + dict.ToString().Length + "\r\n";
                resultString += "Accept: *.*" + "\r\n";
                resultString += "Host: " + this.APIHost + "\r\n";
                resultString += "Content-Type: multipart/form-data; boundary=" + boundary + "\r\n\r\n";
                /*
                if ((Method == "GET") && (dict.Count > 0))
                {
                    resultString += string.Format(DataTemplate, "_method", Method, boundaryString);
                }
                */
                foreach (string strKey in dict.Keys)
                {
                    if (dict[strKey].ToString().Length > 0)
                    {
                        object o = dict[strKey];
                        if (o is Dictionary<string, object>)
                        {
                            BuildDataPost((Dictionary<string, object>)o, Method);
                        }
                        else if (o is ArrayList)
                        {
                            foreach (object oChild in ((ArrayList)o))
                            {
                                //resultString += boundaryString;

                                if (oChild is string)
                                {
                                    resultString += string.Format(DataTemplate, strKey, (string)oChild, boundaryString);
                                }
                                else if (oChild is Dictionary<string, object>)
                                {
                                    BuildDataPost((Dictionary<string, object>)oChild, Method);
                                }
                            }
                            //resultString += boundaryString;
                        }
                        else
                        {
                            resultString += string.Format(DataTemplate, strKey, o.ToString(), boundaryString);
                        }
                    }
                }



            }

            catch (Exception ex)
            {
                resultString += ex.Message + "\r\n\r\n" + ex.StackTrace;
            }

            return resultString;
        }
        #endregion

        #region BuildFilePost

        public String BuildFilePost(System.Collections.Generic.Dictionary<string, object> dict)
        {
            string strOutput = "";
            string contentType = "";

            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            string boundaryString = "\r\n--" + boundary + "\r\n";
            string FileTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";

            foreach (string strKey in dict.Keys)
            {
                strOutput = "";
                object o = dict[strKey];
                if (o is Dictionary<string, object>)
                {
                    BuildFilePost((Dictionary<string, object>)o);
                }
                else if (o is ArrayList)
                {
                    foreach (object oChild in ((ArrayList)o))
                    {
                        resultString += boundaryString;

                        if (oChild is string)
                        {
                            strOutput = ((string)oChild);
                            switch (strKey)
                            {
                                case "license":
                                    break;
                                case "file":
                                    break;
                                case "binary":
                                    if (File.Exists(strOutput))
                                    {
                                        FileAttributes fa = File.GetAttributes(strOutput);

                                        string FileItem = string.Format(FileTemplate, strKey, strOutput, contentType);
                                        resultString += FileItem;

                                        FileStream fileStream = new FileStream(strOutput, FileMode.Open, FileAccess.Read);
                                        byte[] buffer = new byte[4096];
                                        int bytesRead = 0;
                                        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                                        {
                                            resultString += buffer;
                                        }
                                        fileStream.Close();

                                        resultString += boundaryString;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (oChild is Dictionary<string, object>)
                        {
                            BuildFilePost((Dictionary<string, object>)oChild);
                        }
                    }


                }
                else
                {
                    strOutput = o.ToString();
                    switch (strKey)
                    {
                        case "license":
                            break;
                        case "file":
                            break;
                        case "binary":
                            resultString += boundaryString;

                            FileStream fileStream = new FileStream(strOutput, FileMode.Open, FileAccess.Read);
                            byte[] buffer = new byte[4096];
                            int bytesRead = 0;
                            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                resultString += buffer;
                            }
                            fileStream.Close();
                            resultString += boundaryString;

                            break;
                        default:
                            break;
                    }

                }
            }
            return resultString;
        }
        #endregion
    }

    
}




