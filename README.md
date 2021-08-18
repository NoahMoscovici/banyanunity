# Banyan Unity

## Overview

Banyan Unity is a package built upon the [python-banyan framework](https://mryslab.github.io/python_banyan/), allowing two-way communication between Unity's game environment and python applications using lightweight asynchronous socket communication. You can view the installation guide [here](https://github.com/NoahMoscovici/banyanunity/blob/master/Installation%20Guide.md).

You can view the Unity Asset Store counterpart with its respective documentation [here](https://assetstore.unity.com/packages/tools/integration/banyan-unity-124623).

## Prerequisites
For this package you will need [Python 3](https://www.python.org/downloads/release/python-354/), [Python Banyan](https://mryslab.github.io/python_banyan/install/), and [Unity](https://store.unity.com/) all installed. A general understanding of [Alan Yorinks' Python Banyan](https://mryslab.github.io/python_banyan/) is highly recommended.

## Overview on Banyan Unity architecture
![banyanunity diagram](https://github.com/NoahMoscovici/unitybanyan/blob/master/banyanunity.png)

Below is a brief explanation on the purpose of each component. Both the Python and C# components of Banyan Unity are located in this [GitHub repository](https://github.com/NoahMoscovici/banyanunity/tree/master/banyanunity).

### TcpConnectedClient.cs
The framework used for the Unity C# programs were originally based on the TCP & UDP chat framework developed by HardlyDifficult. You can see a more in-depth guide on this implemented structure [here](https://www.youtube.com/watch?v=MW91_l2dnnU&ab_channel=HardlyDifficult).

### test_unity_sender_cube.py
This script acts as a demonostration of how one might use Python Banyan to send messages through the Backplane. Each topic starts in a python dictionary format, which is converted JSON before sent.

### unitygateway.py
unitygateway.py uses the Python Banyan framework to constantly listen for messages in the `receive_loop()`, then processing those messages in `incoming_message_processing()`. While being processed, if the incoming topic is `send_to_unity`, the message will be forwarded to BanyanMessageListener.cs in the Unity environment (through port 5000).

### BanyanMessageListener.cs
BanyanMessageListener.cs listens to unitygateway.py on port 5000. Once receiving a message, the script will decode it and forward the payload off to MessageProcessor.cs on a Unity object.

### MessageProcessor.cs
MessageProcessor.cs will perform an action (in the example, turn the cube a certain color) based on the method `DoAction()` after being called by BanyanMessageListener.cs. The script will then use BanyanMessageSender.cs to send a message back to the Python Banyan environment.

### BanyanMessageSender.cs
BanyanMessageSender.cs sends a message to unitylistener.py on port 5001 after being called from MessageProcessor.cs. 

### unitylistener.py
unitylistener.py listens on port 5001 for messages being sent by BanyanMessageSender.cs in the Unity environment. Once receiving a message, unitylistener.py decodes the message and forwards it to the Python Banyan Backplane with the topic of `receive_unity_message`.

---
*For further help on installation or other inquiries, feel free to contact me at noah.moscovici@gmail.com*
