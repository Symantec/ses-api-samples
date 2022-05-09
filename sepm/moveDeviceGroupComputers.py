"""
"""
from SEPMConnector import SEPMConnector
import json, sys

s = SEPMConnector()

def getInput():
    from_group_id = input("Enter the Group ID to move the Computers FROM (getDeviceGroups.py): ")
    if (len(from_group_id) == 0):
      print("FROM Group ID cannot be empty..")
      getInput()
    to_group_id = input("Enter the Group ID to move the Computers TO (getDeviceGroups.py): ")
    if (len(to_group_id) == 0):
      print("TO Group ID cannot be empty..")
      getInput()
    else:
      runQuery(from_group_id, to_group_id)

def runQuery(from_group_id, to_group_id):

 url = "/sepm/api/v1/groups/" + from_group_id + "/computers"
 m = "GET"
 data = {}

 resp = s.callAPI(m,url,data)

 #print("Raw: " + json.dumps(resp, indent=6))

 if (len(resp) > 0):
   count=1
   numberOfElements = resp["numberOfElements"]
   
   for content in resp["content"]:
      if (numberOfElements == count):
        move_json = [{ "group" : { "id" : to_group_id }, "hardwareKey" : str(content["hardwareKey"]) }]
      else:
        move_json = [{ "group" : { "id" : to_group_id }, "hardwareKey" : str(content["hardwareKey"]) },]
      count=count+1
 else:
      print("Empty Result..")
      
 print(json.dumps(move_json, indent=2))
 s2 = SEPMConnector()
 move_resp = s2.callAPI("PATCH","/sepm/api/v1/computers",json.dumps(move_json))
 print("Move Result: " + json.dumps(move_resp, indent=6))
  
if (len(sys.argv) == 1):
   getInput()
else:
   runQuery(sys.argv[1])
