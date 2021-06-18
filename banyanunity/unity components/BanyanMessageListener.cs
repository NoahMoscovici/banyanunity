/*

 Copyright (c) 2021 Noah Moscovici, Palace Games. All right reserved.

 This program is free software; you can redistribute it and/or
 modify it under the terms of the GNU AFFERO GENERAL PUBLIC LICENSE
 Version 3 as published by the Free Software Foundation; either
 or (at your option) any later version.
 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 General Public License for more details.

 You should have received a copy of the GNU AFFERO GENERAL PUBLIC LICENSE
 along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA

*/
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Net;

namespace Banyan
{
    //summary
    // This script listens to Banyan and decodes the message, to 
    // forward off to the MessageProcessor on an object, to do the action.
    //summary

    public class BanyanMessageListener : MonoBehaviour
    {
        public static BanyanMessageListener instance;
        public int port;
        public IPAddress serverIp;
        List<TcpConnectedClient> clientList = new List<TcpConnectedClient>();
        public static string messageFromBanyan;

        TcpListener listener;

        #region Unity Events
        public void Awake()
        {
            instance = this;

            if (serverIp == null)
            { // Server: start listening for connections
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                listener.BeginAcceptTcpClient(OnServerConnect, null);
            }
            else
            { // Client: try connecting to the server
                TcpClient client = new TcpClient();
                TcpConnectedClient connectedClient = new TcpConnectedClient(client);
                clientList.Add(connectedClient);
                client.BeginConnect(serverIp, port, (ar) => connectedClient.EndConnect(ar), null);
            }
        }

        protected void OnApplicationQuit()
        {
            listener.Stop();
            for (int i = 0; i < clientList.Count; i++)
            {
                clientList[i].Close();
            }
        }

        protected void Update()
        {
            string singleMessage = "";

            if (!string.IsNullOrEmpty(messageFromBanyan))
            {
                messageFromBanyan = messageFromBanyan.Replace("'", "\"");
                for (int i = 0; i < messageFromBanyan.Length; i++)
                {
                    singleMessage += messageFromBanyan[i];
                    if (messageFromBanyan[i].Equals('}'))
                    {
                        Debug.Log(singleMessage);
                        Message message = JsonUtility.FromJson<Message>(singleMessage);

                        try
                        {
                            GameObject theObject = GameObject.Find(message.target);
                            MessageProcessor messageProcessor = theObject.GetComponent<MessageProcessor>();
                            messageProcessor.DoAction(message.action, message.info, message.value, message.target);
                        }
                        catch (NullReferenceException e)
                        {
                            Debug.LogError(" Either the object: " + message.target + " you are referencing in your Banyan message does not exist or is not active in your Unity world, or there is no MessageProcessor script associated with the object: " + message.target + "!");
                        }


                        singleMessage = "";
                        }
                }

                messageFromBanyan = null;
            }
        }
        #endregion

        #region Async Events
        void OnServerConnect(IAsyncResult ar)
        {
            TcpClient TcpClient = listener.EndAcceptTcpClient(ar);
            clientList.Add(new TcpConnectedClient(TcpClient));

            listener.BeginAcceptTcpClient(OnServerConnect, null);
        }
        #endregion

        public void OnDisconnect(TcpConnectedClient client)
        {
            clientList.Remove(client);
        }

    }
    #region JSON Structure

    [System.Serializable]

    public class Message
    {
        public string info;
        public int value;
        public string action;
        public string target;
    }
    #endregion
}