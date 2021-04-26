"""
/*******************************************************************************************************
*                                                                                                      *
*                                                                                                      *
*******************************************************************************************************/
"""
import os, sys
import json
import os.path

class ICDMConfig(object):
  def __init__(self):
    self.clientId = getConfig("clientId")
    self.clientSecret = getConfig("clientSecret")
    self.customerId = getConfig("customerId")
    self.domainId = getConfig("domainId")
    self.apihost = getConfig("apihost")

    @property
    def clientId(self):
      return self.clientId

    @property
    def clientSecret(self):
      return self.clientSecret

    @property
    def customerId(self):
      return self.customerId

    @property
    def domainId(self):
      return self.domainId

    @property
    def apihost(self):
      return self.apihost


def getConfig(attrib):
    cf = open('ICDM.conf','r')
    for line in cf:
      if not line.startswith("#"):
         parts = line.split('=')
         if len(parts) > 1:
           if parts[0] == attrib:
              name =  parts[0]
              value = parts[1].rstrip('\r\n')
              if len(value) == 0:
                 sys.exit("Please edit the ICDM.conf file in order to add the required oAuth credentials")
              return value
         else:
           sys.exit("Please edit the ICDM.conf file in order to add the required oAuth credentials")
    cf.close

