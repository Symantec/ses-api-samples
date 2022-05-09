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

 url = "/sepm/api/v1/groups/" + group_id + "/computers"
 m = "GET"
 data = {}

 resp = s.callAPI(m,url,data)

 #print("Raw: " + json.dumps(resp, indent=6))

 if (len(resp) > 0):

   print("ID\t\t\t\t\t\tGroup\t\t\t\t\t\tComputer\t\t\tHardware Key" )
   print("--------------------------------------------------------------------------------------------------------------------------------------------")
   for content in resp["content"]:
      print(str(content["group"]["id"]) + "\t\t" + str(content["group"]["name"]) + "\t\t\t" + str(content["computerName"]) + "\t\t\t" + str(content["hardwareKey"]))
 else:
      print("Empty Result..")
 
if (len(sys.argv) == 1):
   getInput()
else:
   runQuery(sys.argv[1])
