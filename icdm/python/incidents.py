"""
"""
from ICDMConnector import ICDMConnector
from datetime import datetime, timedelta
import json, sys

s = datetime.now() - timedelta(days=7)
e = datetime.now()
st = s.replace(hour=0, minute=0, second=0)
en = e.replace(hour=0, minute=0, second=0)
sdate = st.strftime('%Y-%m-%d')
edate = en.strftime('%Y-%m-%d')

def getInput():

      start_date = input("Enter the start date (Default: " + sdate + "): ") or sdate
      start_time = input("Enter the start time in 24 hour time (Default: 00:00:00): ") or "00:00:00"
      end_date = input("Enter the end date (Default: " + edate + "): ") or edate
      end_time = input("Enter the start time in 24 hour time (Default: 23:59:00): ") or "23:59:59"
      next = input("Enter the next row to retrieve (0 for first row) (0): ") or "0"
      limit = input("Enter the limit of results (100): ") or "100"
      query = input("Enter the query to search (state_id:1): ") or "state_id:1"

      runQuery(start_date, start_time, end_date, end_time, next, limit, query)

def runQuery(start_date, start_time, end_date, end_time, next, limit, query): 
   # .230+0000
   s = ICDMConnector()
   url = "/v1/incidents"
   m = "POST"
   data = "{ \"start_date\": \"" + start_date + "T" + start_time + ".000+0000\",  \"end_date\": \"" + end_date + "T" + end_time + ".000+0000\",  \"next\": " + str(next) + ", \"limit\": " + str(limit) + ",  \"include_events\": true, \"query\": \"" + query + "\" }"

   print(data)
   resp = s.callAPI(m,url,data)
   if (len(resp) > 0):
      if "message" in resp:
         print(str(resp["message"]))
      else:
         # print(resp)
         s.enumList(resp)
   else:
      print("Empty Result..")

# if (len(sys.argv) != 7):
getInput()
# else:
# runQuery(sys.argv[1], sys.argv[2], sys.argv[3], sys.argv[4], sys.argv[5], sys.argv[6], sys.argv[7])

