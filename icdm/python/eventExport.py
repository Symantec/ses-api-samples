"""
"""
import requests

from ICDMConnector import ICDMConnector
import json, sys

def getInput():
    stream_guid = input("Enter the Stream GUID: ")
    if (len(stream_guid) == 0):
      print("Stream GUID cannot be empty..")
      getInput()

    channel_id = input("Enter the Channel ID: ")
    if (len(channel_id) == 0):
      print("Channel ID cannot be empty..")
      getInput()

    next = input("Enter the next pointer (optional): ") or None
    connection_timeout = input("Enter the connection timeout (min) (Default 1):") or "1"

    runQuery(stream_guid, channel_id, next, connection_timeout)

def runQuery(stream_guid, channel_id, next, connection_timeout):

   s = ICDMConnector()

   url = "https://%s/v1/event-export/stream/%s/%s?connectionTimeout=%s" %(s.apihost,stream_guid,channel_id, connection_timeout)

   session = requests.session()
   header = {}
   header["Authorization"] = "Bearer " + s.accessToken
   header['Accept-Encoding'] = "gzip"
   header["Accept"] = "application/x-ndjson"
   header["Content-Type"] = "application/json"

   payload = {"next": next}

   response = session.post(url,
                            headers=header,
                            json=payload, stream=True)

   if response.status_code == 200:
       for chunk in response.iter_lines(chunk_size=64000, decode_unicode=True):
           # Each response chunk is a json containing event array and a next pointer
           # Refer api doc from exact response fomrat or uncomment below line to dump complete response json
           # print (stream_response)
           stream_response = json.loads(chunk)
           for event in stream_response["events"]:
               # Iterate through event list from response and print each event
                print (event)
           # Print next pointer from response chunk
           print ("Next pointer: %s" %stream_response["next"])
   else:
       print ("Request Failed:")
       s.enumList(response.json())

if (len(sys.argv) == 1):
   getInput()
else:
   runQuery(sys.argv[1])
