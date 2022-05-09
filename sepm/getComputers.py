"""
"""
from SEPMConnector import SEPMConnector
import json

s = SEPMConnector()

url = "/sepm/api/v1/computers"
m = "GET"
data = {}

resp = s.callAPI(m,url,data)

#print("Raw: " + json.dumps(resp, indent=6))

if (len(resp) > 0):
   #s.enumList(resp)
   print("ID\t\t\t\t\t\tGroup\t\t\t\t\t\tComputer\t\t\tHardware Key" )
   print("--------------------------------------------------------------------------------------------------------------------------------------------")
   for content in resp["content"]:
      print(str(content["group"]["id"]) + "\t\t" + str(content["group"]["name"]) + "\t\t\t" + str(content["computerName"]) + "\t\t\t" + str(content["hardwareKey"]))
