## How to install Banyan Unity into your Unity game.
These are the steps needed to run on the machine running your Unity game.

### Step 1: Install Python 3
Go to the [Python 3 Install Page](https://www.python.org/downloads/release/python-354/) and scroll down to the bottom of the page. Choose your install type, and install. Make sure to add Python to the PATH! 

### Step 2: Install Banyan
To install Banyan on your machine, just open a command shell in Windows by pressing the Windows button and typing **cmd**. Since Python 3 comes with Pip, use pip to install Banyan. Just type in the command shell: `pip install python-banyan`

### Step 3: Download and import Banyan Unity to your Unity game

1. Download and import Banyan Unity into your game.
2. Open up the BanyanUnity Demo Scene.
3. Copy over the Sender and Listener Game Objects, and also the Cube into your own scene.

### Step 4: Pull the other components from GitHub

1. Go to the [Banyan Unity GitHub Page](https://github.com/NoahMoscovici/banyanunity) and clone the repository to your machine.
2. The scripts you will be needing to run UnityBanyan: **unitylistener.py**, **unitygateway.py**, **test_unity_sender_cube.py**. If you want extra documentation on how those scripts work go [here](https://github.com/NoahMoscovici/banyanunity/blob/master/README.md)

### Step 5: Test it!

1. Run unitygateway.py
2. Run unitylistener.py
3. Run your Unity game
4. Run test_unity_sender_cube.py
If the cube starts flashing colors, you did they other steps correctly!

### Step 4: Integrate it in to your own project

1. In your scene, copy the MessageProcessor script in the cube over to an object in your scene that will be reacting to the messages received. 
2. Change DoAction() in the MessageProcessor code to do whatever you would like the reaction of the new message coming in to be.
3. Change the test_unity_sender_cube.py code to send your specific messages.

*Please email me at noah.moscovici@gmail.com if you have any questions*
