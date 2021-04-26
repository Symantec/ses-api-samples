"""
"""
from ICDMConnector import ICDMConnector
import json, sys

def getInput():
    device_ids = input("Enter the Device ID or a comma seperated device list from the Device Group List (getDeviceGroup): ") 
    if (len(device_ids) == 0):
      print("Device ID cannot be empty..")
      getInput()
    else:
      runQuery(device_ids)

def runQuery(device_ids): 

   s = ICDMConnector()
   url = "/v1/commands/restart"
   m = "POST"
   payload = "\"payload\": { \"prompt_type\": \"prompt\", \"schedule_type\": \"later\", \"reason_type\": \"remediation\", \"message\": \"This is a restart test\"  }"
   data = "{\"device_ids\": [\"" + device_ids + "\" ] " + payload + " }"

   print(data)
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
