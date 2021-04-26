"""
"""
from ICDMConnector import ICDMConnector
import json, sys

def getInput():
    device_ids = input("Enter the Device ID or a comma seperated device list from the Device Group List (getDeviceGroup): ") 
    if (len(device_ids) == 0):
      print("Device ID cannot be empty..")
      getInput()
    org_unit_ids = input("Enter the Organization ID or a comma seperated Org list from the Device Group List (getDeviceGroup): ")
    if (len(org_unit_ids) == 0):
      print("Organization ID cannot be empty..")
      getInput()
    else:
      runQuery(device_ids, org_unit_ids)

def runQuery(device_ids, org_unit_ids): 

   s = ICDMConnector()
   url = "/v1/commands/scans/quick"
   m = "POST"
   data = "{\"device_ids\": [\"" + device_ids + "\" ], \"org_unit_ids\": [\"" + org_unit_ids + "\" ], \"is_recursive\": false}"

   resp = s.callAPI(m,url,data)
   if (len(resp) > 0):
      if "message" in resp:
         print(str(resp["message"]))
      else:
         s.enumList(resp)
   else:
      print("Empty Result..")

if (len(sys.argv) != 3):
   getInput()
else:
   runQuery(sys.argv[1], sys.argv[2])
