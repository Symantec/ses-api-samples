"""
"""
from ICDMConnector import ICDMConnector
import json, sys

def getInput():
    device_id = input("Enter the Device ID from the Device Group List (getDeviceGroup): ") 
    if (len(device_id) == 0):
      print("Device ID cannot be empty..")
      getInput()
    else:
      runQuery(device_id)

def runQuery(device_id): 

   s = ICDMConnector()
   url = "/v1/devices/" + device_id
   m = "GET"
   data = {}

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
