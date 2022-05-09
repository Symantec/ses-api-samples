"""
/*******************************************************************************************************
*                                                                                                      *
*                                                                                                      *
*******************************************************************************************************/
"""
import os, sys
import json
import os.path

class SEPMConfig(object):
  def __init__(self):
    self.username = getConfig("username")
    self.password = getConfig("password")
    self.domain = getConfig("domain")
    self.apihost = getConfig("apihost")

    @property
    def username(self):
      return self.username

    @property
    def password(self):
      return self.password

    @property
    def domain(self):
      return self.domain

    @property
    def apihost(self):
      return self.apihost


def getConfig(attrib):
    cf = open('SEPM.conf','r')
    for line in cf:
      if not line.startswith("#"):
         parts = line.split('=')
         if len(parts) > 1:
           if parts[0] == attrib:
              name =  parts[0]
              value = parts[1].rstrip('\r\n')
              if len(value) == 0:
                 sys.exit("Please edit the SEPM.conf file in order to add the required oAuth credentials")
              return value
         else:
           sys.exit("Please edit the SEPM.conf file in order to add the required oAuth credentials")
    cf.close

