"""
/*******************************************************************************************************
*                                                                                                      *
*                                                                                                      *
*******************************************************************************************************/
"""
from ICDMConfig import ICDMConfig
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

class ICDMConnector:
    def __init__(self):

        c = ICDMConfig()

        self.clientId = c.clientId
        self.clientSecret = c.clientSecret
        self.apihost = c.apihost
        self.customerId = c.customerId
        self.domainId = c.domainId

        tokenUrl = "https://" + self.apihost + "/v1/oauth2/tokens"
        post = []
        files = []

        self.s = requests.Session()
        e = (self.clientId + ':' + self.clientSecret)
        en = e.encode('utf-8')
        en64 = base64.urlsafe_b64encode(en)
        self.s.headers.update({ 'Accept': 'application/json' })
        self.s.headers.update({ 'Authorization': 'Basic ' + str(en64.decode()) })
        self.s.headers.update({ 'Content-Type': 'application/x-www-form-urlencoded' })
        self.s.headers.update({ 'Host': self.apihost })
        # print("Encoded Auth: " + str(en64.decode()))

        f = self.s.post(tokenUrl, data=post, files=files, verify=False)

        # print('Response: ' + f.text)
        r = json.loads(f.text)
        self.accessToken = r['access_token']

    def callAPI(self, method, url, data, download = False):

        s2 = requests.Session()
        self.s = s2

        # print("clientId: " + self.clientId)
        # print("clientSecret: " + self.clientSecret)
        # print("apihost: " + self.apihost)
        # print("accessToken: " + self.accessToken)
        # print("customerId: " + self.customerId)
        # print("domainId: " + self.domainId)

        if url[0:1] == '/':
            url = url[1:]

        baseUrl = "https://" + self.apihost + "/" + url
        self.s.headers.update({ 'Accept': 'application/json' })
        self.s.headers.update({ 'Content-Type': 'application/json;charset=UTF-8' })
        self.s.headers.update({ 'Accept-Encoding': '*' })
        self.s.headers.update({ 'Authorization': 'Bearer ' + self.accessToken })
        self.s.headers.update({ 'x-epmp-customer-id':  self.customerId })
        self.s.headers.update({ 'x-epmp-domain-id':  self.domainId })

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
            #if ((method == "POST") or (method == "PUT")):
            # f = self.s.post(url, data=data, files=files, allow_redirects=True)
            #f = self.s.post(url, data=data, allow_redirects=True)
        #else:
            # print("Sending GET Request for " + url)
            #f = self.s.get(url, allow_redirects=True)

        req = requests.Request(method,  url, data=data)
        prepped = self.s.prepare_request(req)
        f = self.s.send(prepped, allow_redirects=True)
        
        #If download download to correct area
        if download != False:
            if download == 'memory':
                #return f
                return json.loads(f.text)
            else:
                chunk_size = 1000
                dfile = open(download, 'wb')
                for chunk in f.iter_content(chunk_size):
                  dfile.write(chunk)
                  filesize = os.path.getsize(download)
                return {'download_file':download, 'filesize':filesize}
        else: #Else return the data
            resp = json.loads(f.text)
            if len(resp) > 0:
               return resp
            else:
               # print(self.s.headers)
               # print(f.status_code)
               return json.loads('{"result": "none" }')

### Enumerate JSON Response Object
    def enumList(self, d):
       #if (type(d) == type(str())):
       #   dd = json.loads(d)
        if type(d) == type(dict()):
            for key,value in d.items():
                #print(str(value) + " - "  + str(type(value)))
                if type(value) == type(dict()):
                    self.enumList(value)
                elif type(value) == type(list()):
                    for val in value:
                        if type(val) == type(str()):
                            print(str(val))
                        if type(val) == type(int()):
                            print(str(val))
                        elif type(val) == type(list()):
                            print(str(val))
                        else:
                            self.enumList(val)
                else:
                    print (str(key)+': '+str(value))
        elif type(d) == type(list()):
            for val in d:
                print(str(val))

