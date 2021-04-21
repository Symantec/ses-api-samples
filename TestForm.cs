using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Symantec;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Xml;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.Web.Util;
using System.Threading;
using System.ServiceModel;


namespace Symantec.ICDM
{
    public partial class TestForm : Form
    {

        string rs = "";
        static ManualResetEvent allDone = new ManualResetEvent(false);            

        public TestForm()
        {
            InitializeComponent();            
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            TestSelection.Items.Add("AccessToken");
            TestSelection.Items.Add("getDeviceGroups");
            TestSelection.Items.Add("getDeviceGroup");
            TestSelection.Items.Add("getDeviceGroupDevices");
            TestSelection.Items.Add("getDeviceDetail");
            TestSelection.Items.Add("sendDeviceQuarantine");
            TestSelection.Items.Add("sendDeviceUnQuarantine");
            TestSelection.Items.Add("sendDeviceRestart");
            TestSelection.Items.Add("sendDeviceScanCustom");
            TestSelection.Items.Add("sendDeviceScanFull");
            TestSelection.Items.Add("sendDeviceScanQuick");
            TestSelection.Items.Add("sendDeviceUpdateContent");
            TestSelection.Items.Add("getThreatIntelFileInsight");
            TestSelection.Items.Add("getThreatIntelFileProcessChain");
            TestSelection.Items.Add("getThreatIntelFileFileProtection");
            TestSelection.Items.Add("getThreatIntelFileFileRelated");
            TestSelection.Items.Add("getThreatIntelNetworkInsight");
            TestSelection.Items.Add("getThreatIntelNetworkProtection");
            TestSelection.Items.Add("getThreatIntelNetworkRelated");
            TestSelection.Items.Add("getIncidents");
            TestSelection.Items.Remove("");

            TestSelection.SelectedIndex = 0;
            url.Text = "/v1/oauth2/tokens";
            method.Text = "POST";
            json.Text = "";

            TestSelection.SelectedIndexChanged += new System.EventHandler(this.TestSelection_SelectedIndexChanged);
            groupId.TextChanged += new System.EventHandler(this.groupId_TextChanged);
            deviceId.TextChanged += new System.EventHandler(this.deviceId_TextChanged);
            threatIntel.TextChanged += new System.EventHandler(this.threatIntel_TextChanged);

        }

        static void Main()
        {
            Application.Run(new TestForm());            
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {

            try
            {
                ICDMConfig config = new ICDMConfig();
                ICDMConnector sc = new ICDMConnector();
                
                config.GetCredentials();

                string result = null;
                //string JSONResult = null;
                
                sc.Protocol = Symantec.ICDM.ICDMConnector.Protocols.HTTPS;

                sc.ClientId = config.ClientId;
                sc.ClientSecret = config.ClientSecret;
                sc.APIHost = config.APIHost;
                sc.CustomerId = config.CustomerId;
                sc.DomainId = config.DomainId;

                ClientId.Text = config.ClientId;
                ClientSecret.Text = config.ClientSecret;
                CustomerId.Text = config.CustomerId;
                DomainId.Text = config.DomainId;
                APIHost.Text = config.APIHost;
                sc.URL = url.Text;


                if (TestSelection.SelectedIndex == 0)
                {
                    sc.GetAccessToken();
                }
                else
                {
                    result += sc.CallAPI(method.Text, url.Text, json.Text, false).ToString();
                    Result.Text += result + "\r\n\r\n";

                    JavaScriptSerializer ser = new JavaScriptSerializer();
                    Dictionary<string, object> kv = ser.Deserialize<Dictionary<string, object>>(result);
                    rs = "";
                    Result.Text += "Parsed Result: \r\n\r\n" + EnumListItems(kv) + "\r\n";
                }
                
                accessToken.Text = sc.AccessToken;                
                //ICResult ic = new ICResult(result);
                //ic.Result["access_token"].ToString();

            }
            catch (Exception ex)
            {
                Result.Text += "Error: " + ex.Message + "\r\n\r\n" + ex.StackTrace;
            }

        }

        private void parseJSON()
        {
            url.Text = url.Text.Replace("{group_id}", groupId.Text);
            url.Text = url.Text.Replace("{device_id}", deviceId.Text);
            url.Text = url.Text.Replace("{threat_intel_data}", threatIntel.Text);

            json.Text = json.Text.Replace("{group_id}", groupId.Text);
            json.Text = json.Text.Replace("{device_id}", deviceId.Text);
            json.Text = json.Text.Replace("{threat_intel_data}", threatIntel.Text);
        }


        private string EnumListItems(System.Collections.Generic.Dictionary<string, object> dict)
        {
            foreach (string strKey in dict.Keys)
            {
                object o = dict[strKey];
                if (o is Dictionary<string, object>)
                {
                    EnumListItems((Dictionary<string, object>)o);
                }
                else if (o is ArrayList)
                {
                    foreach (object oChild in ((ArrayList)o))
                    {
                        if (oChild is string)
                        {
                            rs += strKey + ": " + (string)oChild + "\r\n";
                        }
                        else if (oChild is Dictionary<string, object>)
                        {
                            EnumListItems((Dictionary<string, object>)oChild);
                        }
                    }
                }
                else
                {
                    rs += strKey + ": " + o.ToString() + "\r\n";
                }
            }
            return rs;
        }

        #region Data Dictionary

        private string DisplayDictionary(System.Collections.Generic.Dictionary<string, object> dict)
        {
            int indentLevel = 0;
            indentLevel++;
            string tbOutput = "";

            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            string boundarybytes = "\r\n--" + boundary + "\r\n";
            string DataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";

            foreach (string strKey in dict.Keys)
            {
                string strOutput = ""; 

                object o = dict[strKey];
                if (o is Dictionary<string, object>)
                {
                    DisplayDictionary((Dictionary<string, object>)o);
                }
                else if (o is ArrayList)
                {
                    foreach (object oChild in ((ArrayList)o))
                    {
                        tbOutput += boundarybytes;

                        if (oChild is string)
                        {
                            tbOutput += string.Format(DataTemplate, strKey, (string)oChild);
                        }
                        else if (oChild is Dictionary<string, object>)
                        {
                            DisplayDictionary((Dictionary<string, object>)oChild);
                        }
                    }
                    tbOutput += boundarybytes;
                }
                else
                {
                    tbOutput += boundarybytes;
                    strOutput = o.ToString();
                    tbOutput += string.Format(DataTemplate, strKey, strOutput);
                    tbOutput += boundarybytes;
                }
            }

            indentLevel--;

            return tbOutput;
        }
        #endregion

        #region BuildDataPost
        //private Stream BuildDataPost(System.Collections.Generic.Dictionary<string, object> dict, Stream rs)
        private String BuildDataPost(System.Collections.Generic.Dictionary<string, object> dict, Stream rs)
        {
            string strOutput = "";
            string resultString = "";
            int byteLength = 0;

            string DataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";

            try
            {
                foreach (string strKey in dict.Keys)
                {
                    strOutput = "";
                    string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
                    string boundaryString = "\r\n--" + boundary + "\r\n";
                    byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

                    Result.Text += dict[strKey].ToString();
                    if (dict[strKey].ToString().Length > 0)
                    {
                        object o = dict[strKey];
                        if (o is Dictionary<string, object>)
                        {
                            BuildDataPost((Dictionary<string, object>)o, rs);
                        }
                        else if (o is ArrayList)
                        {
                            foreach (object oChild in ((ArrayList)o))
                            {
                                resultString += boundaryString;
                                rs.Write(boundarybytes, 0, boundarybytes.Length);

                                if (oChild is string)
                                {
                                    strOutput = ((string)oChild);
                                    string DataItem = string.Format(DataTemplate, strKey, (string)oChild);
                                    resultString += DataItem;

                                    rs.Write(boundarybytes, 0, boundarybytes.Length);
                                    byte[] DataBytes = System.Text.Encoding.UTF8.GetBytes(DataItem);
                                    rs.Write(DataBytes, 0, DataBytes.Length);

                                }
                                else if (oChild is Dictionary<string, object>)
                                {
                                    BuildDataPost((Dictionary<string, object>)oChild, rs);
                                }
                            }
                            rs.Write(boundarybytes, 0, boundarybytes.Length);
                        }
                        else
                        {
                            resultString += boundaryString;
                            //resultString += "\r\nHeader Offset: " + byteLength + "\r\n";
                            rs.Write(boundarybytes, 0, boundarybytes.Length);
                            byteLength += boundarybytes.Length;
                            strOutput = o.ToString();
                            string DataItem = string.Format(DataTemplate, strKey, o.ToString());
                            resultString += DataItem;

                            byte[] DataBytes = System.Text.Encoding.UTF8.GetBytes(DataItem);
                            //resultString += "\r\nData Offset: " + byteLength + "\r\n";
                            rs.Write(DataBytes, 0, DataBytes.Length);
                            byteLength += DataBytes.Length;
                            //resultString += "\r\nFooter Offset: " + byteLength + "\r\n";
                            rs.Write(boundarybytes, 0, boundarybytes.Length);
                            byteLength += boundarybytes.Length;
                            resultString += boundaryString;
                        }
                    }
                }

            }

            catch (Exception ex)
            {
                resultString += ex.Message + "\r\n\r\n" + ex.StackTrace;
            }

            //rs.Close();
            //return rs;
            return resultString;
        }
        #endregion

        #region BuildFilePost
        //private Stream BuildFilePost(System.Collections.Generic.Dictionary<string, object> dict, Stream rs)
        private String BuildFilePost(System.Collections.Generic.Dictionary<string, object> dict, Stream rs)
        {
            string strOutput = "";
            string resultString = "";
            string contentType = "";

            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            string boundaryString = "\r\n--" + boundary + "\r\n";
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            string FileTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";

            foreach (string strKey in dict.Keys)
            {
                strOutput = "";
                object o = dict[strKey];
                if (o is Dictionary<string, object>)
                {
                    BuildFilePost((Dictionary<string, object>)o, rs);
                }
                else if (o is ArrayList)
                {
                    foreach (object oChild in ((ArrayList)o))
                    {
                        resultString += boundaryString;
                        rs.Write(boundarybytes, 0, boundarybytes.Length);

                        if (oChild is string)
                        {
                            strOutput = ((string)oChild);
                            switch (strKey)
                            {
                                case "license":
                                case  "file":
                                case  "binary":
                                    if (File.Exists(strOutput))
                                    {
                                        FileAttributes fa = File.GetAttributes(strOutput);
                                        
                                        string FileItem = string.Format(FileTemplate, strKey, strOutput, contentType);
                                        resultString += FileItem;

                                        byte[] FileBytes = System.Text.Encoding.UTF8.GetBytes(FileItem);
                                        rs.Write(FileBytes, 0, FileBytes.Length);

                                        FileStream fileStream = new FileStream(strOutput, FileMode.Open, FileAccess.Read);
                                        byte[] buffer = new byte[4096];
                                        int bytesRead = 0;
                                        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                                        {
                                            rs.Write(buffer, 0, bytesRead);
                                            resultString += "[4096]";
                                        }
                                        fileStream.Close();

                                        resultString += boundaryString;
                                        rs.Write(boundarybytes, 0, boundarybytes.Length);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (oChild is Dictionary<string, object>)
                        {
                            BuildFilePost((Dictionary<string, object>)oChild, rs);
                        }
                    }


                }
                else
                {
                    strOutput = o.ToString();
                    switch (strKey)
                    {
                        case "license":
                        case "file":
                        case "binary":
                            resultString += boundaryString;
                            rs.Write(boundarybytes, 0, boundarybytes.Length);

                            string FileItem = string.Format(FileTemplate, strKey, strOutput, contentType);
                            byte[] FileBytes = System.Text.Encoding.UTF8.GetBytes(FileItem);
                            rs.Write(FileBytes, 0, FileBytes.Length);

                            FileStream fileStream = new FileStream(strOutput, FileMode.Open, FileAccess.Read);
                            byte[] buffer = new byte[4096];
                            int bytesRead = 0;
                            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                rs.Write(buffer, 0, bytesRead);
                                resultString += "[4096]";
                            }
                            fileStream.Close();

                            resultString += boundaryString;
                            rs.Write(boundarybytes, 0, boundarybytes.Length);

                            break;
                        default:
                            break;
                    }

                }
            }
            //rs.Close();
            //return rs;
            return resultString;
        }
        #endregion


        #region HttpUploadFile

        public static void HttpUploadFile(string url, string file, string paramName, string contentType, NameValueCollection nvc) {

        string resultString = "";

        resultString += string.Format("Uploading {0} to {1}", file, url);

        string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
        byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

        HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
        wr.ContentType = "multipart/form-data; boundary=" + boundary;
        wr.Method = "POST";
        wr.KeepAlive = true;
        wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

        Stream rs = wr.GetRequestStream();

        string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
        foreach (string key in nvc.Keys)
        {
            rs.Write(boundarybytes, 0, boundarybytes.Length);
            string formitem = string.Format(formdataTemplate, key, nvc[key]);
            byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
            rs.Write(formitembytes, 0, formitembytes.Length);
        }
        rs.Write(boundarybytes, 0, boundarybytes.Length);

        string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
        string header = string.Format(headerTemplate, paramName, file, contentType);
        byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
        rs.Write(headerbytes, 0, headerbytes.Length);

        FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
        byte[] buffer = new byte[4096];
        int bytesRead = 0;
        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0) {
            rs.Write(buffer, 0, bytesRead);
        }
        fileStream.Close();

        byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
        rs.Write(trailer, 0, trailer.Length);
        rs.Close();

        WebResponse wresp = null;
        try {
            wresp = wr.GetResponse();
            Stream stream2 = wresp.GetResponseStream();
            StreamReader reader2 = new StreamReader(stream2);
            resultString += string.Format("File uploaded, server response is: {0}", reader2.ReadToEnd());
        } catch(Exception ex) {
            resultString += string.Format("Error uploading file {1}", ex);
            if(wresp != null) {
                wresp.Close();
                wresp = null;
            }
        } finally {
            wr = null;
        }

       
        StreamReader reader3 = new StreamReader(rs);
        resultString = reader3.ReadToEnd();
    }
        #endregion

        
    public class ListItems
    {
        public string Name { set; get; }
        public string Value { set; get; }
    }

    private void method_TextChanged(object sender, EventArgs e)
    {

    }

    private void apiKey_TextChanged(object sender, EventArgs e)
    {

    }

    private void json_TextChanged(object sender, EventArgs e)
    {

    }

    private void groupId_TextChanged(object sender, EventArgs e)
    {
        parseJSON();
    }

    private void deviceId_TextChanged(object sender, EventArgs e)
    {
        parseJSON();
    }

    private void threatIntel_TextChanged(object sender, EventArgs e)
    {
        parseJSON();
    }

    private void label3_Click(object sender, EventArgs e)
    {
        
    }

    private void TestSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (TestSelection.SelectedItem.ToString())
        {
            case "AccessToken":
                url.Text = "/v1/oauth2/tokens";
                method.Text = "POST";
                json.Text = "";
                break;
            case "getDeviceGroups":
                url.Text = "/v1/device-groups?limit=10&offset=0";
                method.Text = "GET";
                json.Text = "";
                break;
            case "getDeviceGroup":
                url.Text = "/v1/device-groups/{group_id}";
                method.Text = "GET";
                json.Text = "";
                parseJSON();
                break;
            case "getDeviceGroupDevices":
                url.Text = "/v1/device-groups/{group_id}/devices";
                method.Text = "GET";
                json.Text = "";
                parseJSON();
                break;
            case "getDeviceDetail":
                url.Text = "/v1/devices/{device_id}";
                method.Text = "GET";
                json.Text = "";
                parseJSON();
                break;
            case "sendDeviceQuarantine":
                url.Text = "/v1/commands/contain";
                method.Text = "POST";
                json.Text = "{\"device_ids\": [\"{device_id}\" ], \"org_unit_ids\": [\"{group_id}\" ], \"is_recursive\": false}";
                parseJSON();
                break;
            case "sendDeviceUnQuarantine":
                url.Text = "/v1/commands/allow";
                method.Text = "POST";
                json.Text = "{\"device_ids\": [\"{device_id}\" ] }";
                parseJSON();
                break;
            case "sendDeviceRestart":
                url.Text = "/v1/commands/restart";
                method.Text = "POST";
                json.Text = "{\"device_ids\": [\"{device_id}\" ] \"payload\": { \"prompt_type\": \"prompt\", \"schedule_type\": \"later\", \"reason_type\": \"remediation\", \"message\": \"This is a restart test\"  } }";
                parseJSON();
                break;
            case "sendDeviceScanCustom":
                url.Text = "/v1/commands/scans/custom";
                method.Text = "POST";
                json.Text = "{\"device_ids\": [\"{device_id}\" ], \"org_unit_ids\": [\"{group_id}\" ], \"is_recursive\": false}";
                parseJSON();
                break;
            case "sendDeviceScanFull":
                url.Text = "/v1/commands/scans/full";
                method.Text = "POST";
                json.Text = "{\"device_ids\": [\"{device_id}\" ], \"org_unit_ids\": [\"{group_id}\" ], \"is_recursive\": false}";
                parseJSON();
                break;
            case "sendDeviceScanQuick":
                url.Text = "/v1/commands/scans/quick";
                method.Text = "POST";
                json.Text = "{\"device_ids\": [\"{device_id}\" ], \"org_unit_ids\": [\"{group_id}\" ], \"is_recursive\": false}";
                parseJSON();
                break;
            case "sendDeviceUpdateContent":
                url.Text = "/v1/commands/scans/update_content";
                method.Text = "POST";
                json.Text = "{\"device_ids\": [\"{device_id}\" ], \"org_unit_ids\": [\"{group_id}\" ], \"is_recursive\": false}";
                parseJSON();
                break;
            case "getThreatIntelFileInsight":
                url.Text = "/v1/threat-intel/insight/file/{threat_intel_data}";
                method.Text = "GET";
                json.Text = "";
                parseJSON();
                break;
            case "getThreatIntelFileProcessChain":
                url.Text = "/v1/threat-intel/processchain/file/{threat_intel_data}";
                method.Text = "GET";
                json.Text = "";
                parseJSON();
                break;
            case "getThreatIntelFileFileProtection":
                url.Text = "/v1/threat-intel/protection/file/{threat_intel_data}";
                method.Text = "GET";
                json.Text = "";
                parseJSON();
                break;
            case "getThreatIntelFileFileRelated":
                url.Text = "/v1/threat-intel/related/file/{threat_intel_data}";
                method.Text = "GET";
                json.Text = "";
                parseJSON();
                break;
            case "getThreatIntelNetworkInsight":
                url.Text = "/v1/threat-intel/insight/network/{threat_intel_data}";
                method.Text = "GET";
                json.Text = "";
                parseJSON();
                break;
            case "getThreatIntelNetworkProtection":
                url.Text = "/v1/threat-intel/protection/network/{threat_intel_data}";
                method.Text = "GET";
                json.Text = "";
                parseJSON();
                break;
            case "getThreatIntelNetworkRelated":
                url.Text = "/v1/threat-intel/related/network/{threat_intel_data}";
                method.Text = "GET";
                json.Text = "";
                parseJSON();
                break;
            case "getIncidents":
                url.Text = "/v1/incidents";
                method.Text = "POST";
                json.Text = "{ \"start_date\": \"2021-03-20T00:00:00.230+0000\",  \"end_date\": \"2021-03-23T23:59:00.230+0000\",  \"next\": 0, \"limit\": 1,  \"include_events\": true, \"query\": \"state_id: 1\" }";
                parseJSON();
                break;

            default:
                // code block  
                break;
        }
    }

   
 }

}
