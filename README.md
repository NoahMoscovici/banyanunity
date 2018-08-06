# unitybanyan

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
This script receives messages sent by Unity, by listening to the topic of receive_unity_messages

### test_unity_sender_cube.py
Test_unity_sender_cube documentation

## Images for how all of the components work together

![Image of UnityBanyan Chart](https://github.com/NoahMoscovici/unitybanyan/blob/master/BanyanUnity.png)
