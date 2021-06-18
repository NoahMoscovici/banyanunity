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
using System.Net.Sockets;

namespace Banyan
{
    //summary
    // This handles the TCP Connection and Streams used to receive messages from Banyan.
    // Multiple threads are used for each Banyan client.
    //summary

    public class TcpConnectedClient
    {
        readonly TcpClient connection;

        readonly byte[] readBuffer = new byte[5000];

        NetworkStream stream
        {
            get
            {
                return connection.GetStream();
            }
        }

        public TcpConnectedClient(TcpClient tcpClient)
        {
            this.connection = tcpClient;
            this.connection.NoDelay = true;
            // Client is awaiting connection from Banyan
            stream.BeginRead(readBuffer, 0, readBuffer.Length, OnRead, null);
        }

        internal void Close()
        {
            connection.Close();
        }

        void OnRead(IAsyncResult ar)
        {
            // Read messages from Banyan until the socket is closed (message length = 0)
            int length = stream.EndRead(ar);
            if (length <= 0)
            { // Connection closed
                BanyanMessageListener.instance.OnDisconnect(this);
                return;
            }

            string newMessage = System.Text.Encoding.UTF8.GetString(readBuffer, 0, length);

            // Append the latest message from Banyan to the queue for processing
            BanyanMessageListener.messageFromBanyan += newMessage + Environment.NewLine;

            stream.BeginRead(readBuffer, 0, readBuffer.Length, OnRead, null);
        }

        internal void EndConnect(IAsyncResult ar)
        {
            connection.EndConnect(ar);

            stream.BeginRead(readBuffer, 0, readBuffer.Length, OnRead, null);
        }
    }
}