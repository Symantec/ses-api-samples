"""
/*******************************************************************************************************
*                                                                                                      *
*                                                                                                      *
*******************************************************************************************************/
"""
from SEPMConfig import SEPMConfig
import requests
from requests.packages.urllib3.exceptions import InsecureRequestWarning
import os
import json
import base64
import os.path
from base64 import b64encode
import ssl

requests.packages.urllib3.disable_warnings(InsecureRequestWarning)

try:
    _create_unverified_https_context = ssl._create_unverified_context 
except AttributeError:
    # Legacy Python that doesn't verify HTTPS certificates by default
    pass
else:
    # Handle target environment that doesn't support HTTPS verification
    ssl._create_default_https_context = _create_unverified_https_context

class SEPMConnector:
    def __init__(self):

        c = SEPMConfig()

        self.username = c.username
        self.password = c.password
        self.domain = c.domain
        self.apihost = c.apihost

        tokenUrl = "https://" + self.apihost + "/sepm/api/v1/identity/authenticate"
        post = json.dumps({"username": self.username, "password":  self.password, "domain": self.domain})
        files = []

        self.s = requests.Session()
        self.s.headers.update({ 'Accept': 'application/json' })
        self.s.headers.update({ 'Content-Type': 'application/json' })

        f = self.s.post(tokenUrl, data=post, json=post, verify=False)

        #print('Response: ' + f.text)
        r = json.loads(f.text)
        self.accessToken = r['token']

    def callAPI(self, method, url, data, download=False):

        s2 = requests.Session()
        self.s = s2

        #print("username: " + self.username)
        #print("password: " + self.password)
        #print("apihost: " + self.apihost)
        #print("accessToken: " + self.accessToken)
        #print("domain: " + self.domain)

        if url[0:1] == '/':
            url = url[1:]

        baseUrl = "https://" + self.apihost + "/" + url
        #self.s.headers.update({ 'Accept': 'application/json' })
        self.s.headers.update({ 'Content-Type': 'application/json' })
        self.s.headers.update({ 'Authorization': 'Bearer ' + self.accessToken })

        return self._request(method, baseUrl, data, download)

    def _request(self, method, url, data, download=False):

        post = {}
        files = {}
        basestring = (str,bytes)

        #if len(data) != 0:
            # for k,v in data.items():
            #    if isinstance(v, basestring) and os.path.isfile(v):
            #        files[k] = open(v, "rb")
            #    else:
            #        post[k] = json.dumps(v)
            #if method == "POST":
                # f = self.s.post(url, data=data, files=files, allow_redirects=True)
                #f = self.s.post(url, data=data, verify=False)
        #else:
            # print("Sending GET Request for " + url)
            #f = self.s.get(url, verify=False)

        req = requests.Request(method,  url, data=data)
        prepped = self.s.prepare_request(req)
        f = self.s.send(prepped, verify=False)

        #If download download to correct area
        if download != False:
            if download == 'memory':
                #return f
                return f.text
            else:
                chunk_size = 1000
                dfile = open(download, 'wb')
                for chunk in f.iter_content(chunk_size):
                  dfile.write(chunk)
                  filesize = os.path.getsize(download)
                return {'download_file':download, 'filesize':filesize}
        else: #Else return the data
            resp = f.text
            if len(resp) > 0:
               return json.loads(resp)
            else:
               # print(self.s.headers)
               # print(f.status_code)
               return f.text               

    def enumList(self, d):
       for key,value in d.items():
           if type(value) == type(dict()):
              self.enumList(value)
           elif type(value) == type(list()):
              for val in value:
                 if type(val) == type(str()):
                    pass
                 elif type(val) == type(list()):
                    pass
                 else:
                    self.enumList(val)
           else:
              print (str(key)+': '+str(value))

