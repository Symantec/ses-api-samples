"""
"""
from ICDMConnector import ICDMConnector
import json, sys

s = ICDMConnector()

def getInput():
   group_id = input("Enter the Group ID from the Device Groups List (getDeviceGroups): ") 
   if (len(group_id) == 0):
      print("Group ID cannot be empty..")
      getInput()
   else:
      runQuery(group_id)

def runQuery(group_id):

   s = ICDMConnector()
   url = "/v1/device-groups/" + group_id + "/devices"
   m = "GET"
   data = {}

   resp = s.callAPI(m,url,data)

   if (len(resp) > 0):
      print("Total: " + str(resp["total"]))
      print("Raw: " + str(resp))
      print("----")

      for r in resp["devices"]:
         print("ID: " + str(r["id"]))
         print("Name: " + str(r["name"]))
         print("-----")

if (len(sys.argv) == 1):
   getInput()
else:
   runQuery(sys.argv[1])
