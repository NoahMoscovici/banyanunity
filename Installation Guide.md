## How to install Banyan Unity into your Unity game.

### Installs

Python 3 install
Go to the Python 3 Install Page and scroll down to the bottom of the page. Choose your install type, and install. Make sure to add Python to the PATH!

Banyan install
To install Banyan on your machine, just open a command shell in Windows by pressing the Windows button and typing cmd. Since Python 3 comes with Pip, use pip to install Banyan. Just type in the command shell: pip install python-banyan

### Setup

1. Download and import Banyan Unity into your game.
2. Open up the BanyanUnity Demo Scene.
3. Copy over the Sender, Listener, and Cube into your own scene. You shouldn't ever have to edit the Sender or Listener code, only the MessageProcessor code on the Cube. 
4. Copy over the MessageProcessor script from the Cube to your own Object that will be reacting to the messages being received.
5. Make sure both the unitygateway.py and the unitylistener.py are both running. Then run your game.
6. Finally run the test_unity_sender_cube.py
