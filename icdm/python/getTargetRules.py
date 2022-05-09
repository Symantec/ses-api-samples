"""
"""
from ICDMConnector import ICDMConnector
import json

s = ICDMConnector()

# url = "/v1/device-groups"
url = "/v1/policies/target-rules?limit=100&offset=0"
m = "GET"
data = {}

resp = s.callAPI(m,url,data)

print("Raw: " + str(resp))

if (len(resp) > 0):
   print("\n\nTotal: " + str(resp["total"]) + "\n\n")
   s.enumList(json.loads(json.dumps(resp)))
   #for group in resp["target_rules"]:
   #   print("Name: " + str(group["name"]))
   #   print("Enabled: " + str(group["enabled"]))
   #   print("Sort Order: " + str(group["sort_order"]))
   #   print("Description: " + str(group["description"]))
   #   print("Created: " + str(group["created"]))
   #   print("Modified: " + str(group["modified"]))
   #   print("-----")
