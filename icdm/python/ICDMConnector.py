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
               return resp

    def enumList(self, d):
       for key,value in d.items():
           #print(value + " - " + type(value))
           if type(value) == type(dict()):
              self.enumList(value)
           elif type(value) == type(list()):
              for val in value:
                 if type(val) == type(str()):
                    pass
                 if type(val) == type(int()):
                    pass
                 elif type(val) == type(list()):
                    pass
                 else:
                    self.enumList(val)
           else:
              print (str(key)+': '+str(value))

    def enumList2(self, d):
       count=0
       for key,value in d.items():
         if type(value) == type(dict()):
            self.enumList2(value)
         elif type(value) == type(list()):
            for val in value:
               #print(val)
               #print(type(val))
               if type(val) == type(dict()):
                 self.enumList2(val)
               elif type(val) == type(str()):
                 #print(type(key))
                 #print(key)
                 #if count==0:
                 print(value)
                 #count=count+1
                 #pass
         else:
            print(str(key) + ': ' + str(value))
