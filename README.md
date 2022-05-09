# API code samples for demonstrating Symantec ICDm and SEPM RESTful API calls

Refer to Symantec ICDm and SEP API documentation at: https://apidocs.securitycloud.symantec.com 

There are currently two directories containing the ICDM API samples located on GitHub. One directory contains Python API samples and the other is a Windows C# project. 
There is also a 'sepm' directory containing SEPM on-prem API examples. 

* main
  * icdm
    * python
    * c-sharp
  * sepm

All projects utilize a centralized Controller class to handle the oAuth authentication, REST API formatting and submission of the API query. The Controller class reads the authentication credentials from a configuration file located within each project (ICDM.conf for ICDm Python API examples, App.Config for Windows C# ICDm API examples and SEPM.conf for SEPM API examples). 

NOTE: The configuration file should be used to store API credentials for testing, training or demonstration purposes ONLY. Production deployments of these samples should be modified to utilize the secure credential storage facilities running within your organization.    

ICDm API Instructions 
----------------------
Before you get started, you need to enroll the Symantec ICDm console by logging into; 

https://sep.securitycloud.symantec.com

Click on the Integrations icon and select Enrollment, Follow the instructions on the page in order to enroll the ICDm console. After you have completed the enrollment.

Create the API access keys by selecting the Integrations icon and selecting Client Applications. Click the add button in order to create a new API Client Application. 

Copy following Client API and Secret keys as well as your ICDM Customer and Domain ID information and add them to the ICDm configuration file. (ICDM.conf for Python and App.Config for Windows C#)

    Client ID: [copy from integrations page]
    Client Secret Key: [copy from integrations page]
    Customer ID: [copy from integrations page]
    Domain ID: [copy from integrations page]


SEPM API Instructions
---------------------

Open the SEPM.conf file and edit the following values;

    username={SEPM_USERNAME}
    password={SEPM_USER_PASSWORD}
    domain=Default
    apihost={SEPM_IP_ADDRESS}:8446

Once the configuration file is saved, ensure that you have the necessary requirements installed (below) and you are ready to start using these samples.

### Requirements for these API examples

#### Python
* Python 3.x
* Python ‘Requests’ library

If you do not have the Python Requests library installed, perform the following;
1. curl "https://bootstrap.pypa.io/get-pip.py" -o "get-pip.py"
1. python3 get-pip.py 
1. pip3 install requests

#### Windows C#
* .NET 4.5 Runtime
