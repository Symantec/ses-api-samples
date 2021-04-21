using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Symantec.ICDM
{
    class ICDMConfig
    {

        private string _ClientId;
        private string _ClientSecret;
        private string _APIHost;
        private string _CustomerId;
        private string _DomainId;

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


        public string GetCredentials()
        {
            try
            {
                AppSettingsReader appsettingsreader = new AppSettingsReader();

                this.ClientId = (string)(new AppSettingsReader().GetValue("ClientId", typeof(string)));
                this.ClientSecret = (string)(new AppSettingsReader().GetValue("ClientSecret", typeof(string)));
                this.CustomerId = (string)(new AppSettingsReader().GetValue("CustomerId", typeof(string)));
                this.DomainId = (string)(new AppSettingsReader().GetValue("DomainId", typeof(string)));
                this.APIHost = (string)(new AppSettingsReader().GetValue("APIHost", typeof(string)));

                return "Result: Success";
            }
                catch (Exception ex)
            {
                return ex.Message;
            }

        }


    }
}
