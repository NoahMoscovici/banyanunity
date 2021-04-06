# banyanunity

## Overview

Banyanunity is a package built upon the [python-banyan framework](https://mryslab.github.io/python_banyan/), allowing two-way communication between Unity's game environment and python applications. Using lightweight. asynchronous socket communication, Banyanunity allows you communicate quickly and effectively between the two environments as long as they are both a part of the banyan network.  

You can view the Unity Asset Store counterpart with its respective documentation [here](https://assetstore.unity.com/packages/tools/integration/banyan-unity-124623).

## Prerequisites
For this package you will need [Python 3](https://www.python.org/downloads/release/python-354/), [Python Banyan](https://mryslab.github.io/python_banyan/install/), and [Unity](https://store.unity.com/) all installed.

## Scripts Documentation

### unitygateway.py
unitygateway.py uses the Banyan framework to constantly listen for messages in the `receive_loop()`, then processing those messages in `incoming_message_processing()`. While being processed, if the topic is `send_to_unity`, the message will be forwarded to the listening C# Unity script by opening a socket on port 5000. 

### unitylistener.py
unitylistener.py listens on port 5001 for messages being sent by BanyanMessageSender.cs (that is running in the Unity game environment). Once unitylistener.py decodes the message, it forwards the message to the python-banyan backplane with the topic of `receive_unity_message`.

### test_unity_sender_cube.py
This script uses Banyan to send two messages with the topic of send_to_unity. Each topic starts in a python dictionary format, which is converted JSON before sent.

### BanyanMessageListener.cs, BanyanMessageSender.cs, MessageProcessor.cs, TcpConnectedClient.cs
These scripts were based on the TCP & UDP chat framework developed by HardlyDifficult. You can see an in depth guide on the specifics of how it works [here](https://www.youtube.com/watch?v=MW91_l2dnnU&ab_channel=HardlyDifficult). For the banyanunity specific documentation about the Unity C# scripts, there are extensive comments in the files that should explain how they work. If you wan't to quickly check out the files without loading them up on Unity, you can do so [here](https://github.com/NoahMoscovici/banyanunity/tree/master/unity%20components).

## How each component communicates with one another
![Image of BanyanUnity Chart](https://github.com/NoahMoscovici/unitybanyan/blob/master/BanyanUnity.png)
