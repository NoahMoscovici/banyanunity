## Installation and usage of Banyan Unity
This document will be an in depth guide on installing and using the unitybanyan specific components. It is recommended that you first read [Alan Yorink's guide to Python Banyan](https://mryslab.github.io/python_banyan/users_guide/), as it will provide an explanation as to the benefits of using a Python Banyan Backplane and reasoning behind the formatting of messages. It is also recommended that you are familiar with the general structure of how each piece communicates with one another inside and between environments *(see [here](https://github.com/NoahMoscovici/banyanunity/blob/master/BanyanUnity.png))* 

### Step 1: Installing Python 3, pySerial, Python Banyan
a) Installing Python. As Banyan Unity is built upon the Python Banyan framework, we will first need to install Python. You can do so at the [Python 3 Installation Page](https://www.python.org/downloads/). During installation, you will want to check the option that adds Python to the PATH.

b) Installing pySerial. Using Pip (which should be preinstalled when you install Python), run: `pip install pyserial` to install pyserial. However, if you prefer using a different installation method, you can view [pySerial's installation documentation](https://pyserial.readthedocs.io/en/latest/pyserial.html). In short, pySerial provides backends for access to the serial port which is going to be used by Python Banyan. For more information on pySerial and its use, you can view [pySerial's documentation](https://pythonhosted.org/pyserial/).

c) Installing Python Banyan. Once again using Pip, you can run `pip install python-banyan` to install Python Banyan. If you want to use a different installation method, then visit the [Python Banyan Installation Guide](https://mryslab.github.io/python_banyan/install/).

### Step 2: Installing and configuring Banyan Unity
Now that we have installed the dependencies for Banyan Unity, we clone the repository and manually copy over the files in the [python components folder](https://github.com/NoahMoscovici/banyanunity/tree/master/python%20components) of this repository. Out of the three files in the folder (unitylistener.py, unitygateway.py, and test_unity_sender_cube.py) only the unitylistener.py and unitygateway.py are needed to complete the Banyan Unity structure. test_unity_sender_cube.py exists as an example for sending messages through the Python Banyan framework to our Unity game. 

However, these three files need to be configured in order to work on the Python Banyan framework that you just installed on your machine. Right now, in all three files there exists the line `BANYAN_IP="10.111.0.3"` which defines the Backplane IP address. You will need to change this line to the IP of the machine that the backplane will be running on. Keep in mind that unitylistener.py and unitygateway.py will both be running on the same machine that is running the Unity game, however not necessarily the same machine that the Backplane is being run from. If you are unsure as to the correct IP that your Python Banyan Backplane is running on, you can check the output that will print to the console after starting the Backplane: 
```
******************************************
Banyan BackPlane IP address: 10.111.0.3
Subscriber Port = 43125
Publisher  Port = 43124
******************************************
```
*If you are at all confused on the purpose of the Backplane or how to manually start it, refer back to [Alan Yorink's documentation on his Python Banyan](https://mryslab.github.io/python_banyan/users_guide/).*

Copy the IP address from the first line (in this case `10.111.0.3`) and replace the line in unitylistener.py, unitygateway.py, and test_unity_sender_cube.py. Now, the gateway, listener, and sending script will be able to access the Backplane.

### Step 3: Importing Banyan Unity to the Unity environment

Now that we have successfully installed and configured Banyan Unity outside of the Unity environment, we must now install and configure the Unity counterpart. Visit the Unity [Asset Store page for Banyan Unity](https://assetstore.unity.com/packages/tools/integration/banyan-unity-124623) and add the asset to your Unity Project. Inside, are both the necessary C# scripts and the Demo Scene associated with test_unity_sender_cube.py that will show a simple example of how one might use Banyan Unity. 

### Step 5: Testing the Demo Scene, and integrating it into your own project

First, make sure that a machine on the network is running the Python Banyan Backplane. On the machine running the Unity game, start unitygateway.py and unitylistener.py either manually or as services. Then, start the Unity scene and run test_unity_sender_cube.py. If the cube starts changing colors, then the Banyan Unity connection is successful.

To integrate Banyan Unity into your own Unity project, copy MessageProcessor.cs and assign it to the object that will react to incoming messages. Then, copy over the Sender and Listener objects, which will manage the incoming messages on port 5000 and outgoing ones on port 5001. Inside MessageProcessor.cs, you can change the contents of DoAction() to what you wish to do with the incoming message.

---
*For further help on installation or other inquiries, feel free to contact me at noah.moscovici@gmail.com*
