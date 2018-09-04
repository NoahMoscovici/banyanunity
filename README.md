# banyanunity

## Overview

This package allows two-way communication with your Unity project and the outside world. Using sockets, you can flawlessly receive and send messages with any machine running on a Banyan network. This will allow your game to not only be affected by real-world actions but also trigger them via Arduinos or Raspberry Pis.

For more information see this [extended guide](https://github.com/NoahMoscovici/banyanunity/blob/master/Unity%20Asset%20Store%20Description.md).

## Prerequisites

### Python 3 install
 Go to the [Python 3 Install Page](https://www.python.org/downloads/release/python-354/) and scroll down to the bottom of the page. Choose your install type, and install. Make sure to add Python to the PATH! 

### Banyan install
To install Banyan on your machine, just open a command shell in Windows by pressing the Windows button and typing **cmd**. Since Python 3 comes with Pip, use pip to install Banyan. Just type in the command shell: `pip install python-banyan`

### Unity install
Go to the [Unity Store Page](https://store.unity.com/) and choose the type of Unity you will use. Follow Unity's instructions on how to install the version of Unity you selected.

## Script documentation

### Unitygateway.py
This script uses Banyan to constantly listen to messages with the topic of send_to_unity, and forward those messages to Unity. It sends the message to Unity by opening a socket encoding the message, then after sending the message it will close the socket.

### Unitylistener.py
This script receives messages sent by Unity, by listening to the port 5001 on a socket. Once Unitylistener.py decodes the message, it sends the message to the backplane witht he topic of receive_unity_message.

### test_unity_sender_cube.py
This script uses Banyan to send two messages with the topic of send_to_unity. Each topic is in a dictionary format, which is eventually comverted to JSON once sent.

## Images for how all of the components work together

![Image of BanyanUnity Chart](https://github.com/NoahMoscovici/unitybanyan/blob/master/BanyanUnity.png)
