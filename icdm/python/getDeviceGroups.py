"""
"""
from ICDMConnector import ICDMConnector
import json

s = ICDMConnector()

# url = "/v1/device-groups"
url = "/v1/device-groups?limit=10&offset=0"
m = "GET"
data = {}

resp = s.callAPI(m,url,data)

print("Raw: " + str(resp))

if (len(resp) > 0):
   print("Total: " + str(resp["total"]))

   for group in resp["device_groups"]:
      print("ID: " + str(group["id"]))
      print("Name: " + str(group["name"]))
      if str(group["name"]) != "Default":
         print("Parent ID: " + str(group["parent_id"]))
         print("Description: " + str(group["description"]))
      print("Created: " + str(group["created"]))
      print("Modified: " + str(group["modified"]))
      print("-----")
