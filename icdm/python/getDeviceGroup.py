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
   url = "/v1/device-groups/" + group_id
   m = "GET"
   data = {}

   resp = s.callAPI(m,url,data)

   if (len(resp) > 0):
      #print("Raw: " + str(resp))
      print("----")
      print("ID: " + str(resp["id"]))
      print("Name: " + str(resp["name"]))
      if str(resp["name"]) != "Default":
         print("Parent ID: " + str(resp["parent_id"]))
         if ("description") in str(resp):
            print("Description: " + str(resp["description"]))
      print("Created: " + str(resp["created"]))
      print("Modified: " + str(resp["modified"]))
   else:
      print("Empty Result..")

if (len(sys.argv) == 1):
   getInput()
else:
   runQuery(sys.argv[1])
