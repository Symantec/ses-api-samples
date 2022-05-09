"""
"""
from SEPMConnector import SEPMConnector
import json, sys

s = SEPMConnector()

def getInput():
    group_id = input("Enter the Group ID from the Device Groups List (getDeviceGroups): ")
    if (len(group_id) == 0):
      print("Group ID cannot be empty..")
      getInput()
    else:
      runQuery(group_id)

def runQuery(group_id):

 url = "/sepm/api/v1/groups/" + group_id
 m = "GET"
 data = {}

 resp = s.callAPI(m,url,data)

 print("Raw: " + json.dumps(resp, indent=6))

 if (len(resp) > 0):

   #for group in resp:
      print("ID: " + str(resp["id"]))
      print("Name: " + str(resp["name"]))
      print("Created: " + str(resp["created"]))
      print("Modified: " + str(resp["lastModified"]))
      print("-----")
 else:
      print("Empty Result..")
 
if (len(sys.argv) == 1):
   getInput()
else:
   runQuery(sys.argv[1])
