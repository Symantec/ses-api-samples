"""
"""
from ICDMConnector import ICDMConnector
import json, sys

s = ICDMConnector()

def runQuery():

   url = "/v1/device-groups?limit=10&offset=0"
   m = "GET"
   data = {}

   resp = s.callAPI(m,url,data)

   if (len(resp) > 0):
      print("Total Groups: " + str(resp["total"]))

      for g in resp["device_groups"]:
         print("\r\n\r\nGroup: " + str(g["name"]))

         url = "/v1/device-groups/" + str(g["id"]) + "/devices"
         m = "GET"
         data = {}

         resp1 = s.callAPI(m,url,data)

         if (len(resp1) > 0):
            print("Total Devices: " + str(resp1["total"]))

            for d in resp1["devices"]:
               print("Device: " + str(d["name"]))
               print("-------" + str(d["name"])  + " Details--------------------")

               url = "/v1/devices/" + str(d["id"])
               m = "GET"
               data = {}

               resp2 = s.callAPI(m,url,data)
               s.enumList(resp2)
runQuery()
