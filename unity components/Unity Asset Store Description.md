#### What does this package do?

This package allows two-way communication with your Unity project and the outside world. 
Using sockets, you can flawlessly receive and send messages with any machine running on a Banyan network. 
This will allow your game to not only be affected by real-world actions but also trigger them via Arduinos or Raspberry Pis. 

#### What is Banyan?
Banyan is a messaging framework that allows asynchronous communication with devices running Python or other computer languages. 
Banyan was developed by Alan Yorinks and is available [here](https://mryslab.github.io/python_banyan/).

#### What is in this package?
This Unity package includes a TCPClient, BanyanMessageSender, BanyanMessageListener, and MessageProcessor that can be attached to any object in Unity. 
You will only need to alter the MessageProcessor with your own logic for actions based on what messages you received. 
The demo scene included in this package will turn a cube in the scene blue and red based on incoming messages and will send a message back out after changing color.

#### How do I connect this to Banyan?
To connect this to Banyan, you will need Banyan installed on your machine, as well as the unitygateway.py and unitylistener.py running on your machine. 
You can find those scripts [here](https://github.com/NoahMoscovici/unitybanyan).
The design pattern for the TCPClient is based on the chat client created by [Hardly Difficult](https://www.youtube.com/watch?v=MW91_l2dnnU).
