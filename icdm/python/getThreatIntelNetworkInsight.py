"""
"""
from ICDMConnector import ICDMConnector
import json, sys

s = ICDMConnector()

def getInput():
   sha256 = input("Enter the Network Address or a comma separated list of IPs: ") 
   if (len(Ips) == 0):
      print("Network Address cannot be empty..")
      getInput()
   else:
      runQuery(IPs)

def runQuery(IPs):

   s = ICDMConnector()
   url = "/v1/threat-intel/insight/network/" + IPs
   m = "GET"
   data = ""

   resp = s.callAPI(m,url,data)
   if (len(resp) > 0):
      if "message" in resp:
         print(str(resp["message"]))
      else:
         s.enumList(resp)
   else:
      print("Empty Result..")

if (len(sys.argv) == 1):
   getInput()
else:
   runQuery(sys.argv[1])
