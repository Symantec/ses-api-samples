"""
"""
from SEPMConnector import SEPMConnector
import json

s = SEPMConnector()

url = "/sepm/api/v1/groups"
m = "GET"
data = {}

resp = s.callAPI(m,url,data)

print("Raw: " + str(resp))

if (len(resp) > 0):

   for group in resp["content"]:
      print("ID: " + str(group["id"]))
      print("Name: " + str(group["name"]))
      print("Created: " + str(group["created"]))
      print("Modified: " + str(group["lastModified"]))
      print("-----")
