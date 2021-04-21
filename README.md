# API code samples for demonstrating Symantec ICDm RESTful API

Refer to Symantec ICDm API documentation at: https://apidocs.securitycloud.symantec.com 

There are currently two branches containing the ICDM API samples located on GitHub. One branch contains Python API samples and the other is a Windows C# project;

* main
  * icdm
    * [Python](../Python)
    * [C-Sharp](../C-Sharp)

Both projects utilize a centralized ICDMController class to handle the oAuth authentication, REST API formatting and submission of the API query. The ICDMController class reads the authentication credentials from a configuration file located within each project (ICDM.conf for Python and App.Config for Windows C#). 

NOTE: The configuration file should be used to store API credentials for testing, training or demonstration purposes ONLY. Production deployments of these samples should be modified to utilize the secure credential storage facilities running within your organization.    

Before you get started you need to enroll the Symantec ICDM console by logging into; 

https://sep.securitycloud.symantec.com

Click on the Integrations icon and select Enrollment, Follow the instructions on the page in order to enroll the ICDm console. After you have completed the enrollment.

Create the API access keys by selecting the Integrations icon and selecting Client Applications. Click the add button in order to create a new API Client Application. 

Copy following Client API and Secret keys as well as your ICDM Customer and Domain ID information and add them to the ICDm configuration file. (ICDM.conf for Python and App.Config for Windows C#)

    Client ID: [copy from integrations page]
    Client Secret Key: [copy from integrations page]
    Customer ID: [copy from integrations page]
    Domain ID: [copy from integrations page]

Once this is complete, you are ready to start using these samples.


### Requirements

#### Python
* Python 3.x
* Python ‘Requests’ library

If you do not have the Python Requests library installed, perform the following;
1. curl "https://bootstrap.pypa.io/get-pip.py" -o "get-pip.py"
1. python get-pip.py (or python3 get-pip.py)
1. pip3 install requests

#### Windows C#
* .NET 4.5 Runtime
