"""
"""
from ICDMConnector import ICDMConnector
import json, sys

def getInput():
    group_id = input("Enter the Group ID from the Device Groups List (getDeviceGroups): ") 
    if (len(group_id) == 0):
      print("Group ID cannot be empty..")
      getInput()
    else:
      runQuery(group_id)

def runQuery(group_id): 

   s = ICDMConnector()
   url = "/v1/device-groups/" + group_id + "/policies"
   m = "GET"
   data = {}

   resp = s.callAPI(m,url,data)

   if (len(resp) > 0):
       print("Raw: " + json.dumps(resp, indent=6))
       print("----")
       print("Total: " + str(resp["total"]) + "\n\n")
       # print("Name\t\t\t\tVersion\tPolicy Type\t\t\tID\n\n")
       s.enumList(json.loads(json.dumps(resp)))
       
       #for group in resp["policies"]:
       #   print("Name: " + str(group["name"]))
       #   print("Version: " + str(group["policy_version"]))
       #   if "policy_type" in group:
       #      print(str("Policy Type: " + group["policy_type"]))
       #   print("Policy UID: " + str(group["policy_uid"]))
       #   print("\n")
   else:
      print("Empty Result..")

if (len(sys.argv) == 1):
   getInput()
else:
   runQuery(sys.argv[1])
