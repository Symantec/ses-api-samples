"""
"""
from ICDMConnector import ICDMConnector
import json, sys

s = ICDMConnector()

def getInput():
   sha256 = input("Enter the SHA-256 hash or a comma separated list of hashes: ") 
   if (len(sha256) == 0):
      print("Hash value cannot be empty..")
      getInput()
   else:
      runQuery(sha256)

def runQuery(sha256):

   s = ICDMConnector()
   url = "/v1/threat-intel/insight/file/" + sha256
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
