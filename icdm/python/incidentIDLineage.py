"""
"""
from ICDMConnector import ICDMConnector
import json, sys

def getInput():
    incident_id = input("Enter the Incident ID: ") 
    if (len(incident_id) == 0):
      print("Incident ID cannot be empty..")
      getInput()
    else:
      runQuery(incident_id)

def runQuery(incident_id): 

   s = ICDMConnector()
   url = "/v1/incidents/" + incident_id + "/lineage"
   m = "GET"
   data = {}

   resp = s.callAPI(m,url,data)

   if (len(resp) > 0):
      print("Raw: " + str(resp))
   else:
      print("Empty Result..")

if (len(sys.argv) == 1):
   getInput()
else:
   runQuery(sys.argv[1])
