"""
"""
from ICDMConnector import ICDMConnector
import json, sys

def getInput():
    policy_uid = input("Enter the Policy UID from the Policy List (getPolicies): ")
    if (len(policy_uid) == 0):
      print("Policy UID cannot be empty..")
      getInput()
    version = input("Enter the Policy Version from the Policy List (getPolicies): ")
    if (len(version) == 0):
      print("Version cannot be empty..")
      getInput()
    if ((len(policy_uid) != 0) and (len(version) != 0)):
      runQuery(policy_uid, version)

def runQuery(policy_uid, version):

   s = ICDMConnector()
   url = "/v1/policies/" + policy_uid + "/versions/" + version
   m = "GET"
   data = {}

   resp = s.callAPI(m,url,data)

   if (len(resp) > 0):
      #print(resp)
      print("Raw: " + str(resp))
      print("----")
      s.enumList(json.loads(json.dumps(resp)))

   else:
      print("Empty Result..")

if (len(sys.argv) < 2):
   getInput()
else:
   runQuery(sys.argv[1], sys.argv[2])
