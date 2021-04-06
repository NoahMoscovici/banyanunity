using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

//summary
// This sends a message back to Banyan after being called by another script
//summary

public class BanyanMessageSender : MonoBehaviour
{

    Int32 port;
    NetworkStream stream;
    string TextToSend = "";

    private TcpClient socketConnection;
    private Thread clientReceiveThread;

    // Use this for initialization
    void Start()
    {
        Int32 port = 5001;
        socketConnection = new TcpClient("127.0.0.1", port);
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if(!TextToSend.Equals(""))
            {
                if (socketConnection == null)
                {
                    return;
                }
                try
                {
                    // Get a stream object for writing. 			
                    NetworkStream stream = socketConnection.GetStream();
                    if (stream.CanWrite)
                    {
                        if (!TextToSend.Equals(""))
                        {
                            // Convert string message to byte array.                 
                            byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(TextToSend);
                            // Write byte array to socketConnection stream.                 
                            stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                            Debug.Log("Client sent this message: " + TextToSend + " - should be received by server");
                            TextToSend = "";

                        }
                    }
                }
                catch (SocketException socketException)
                {
                    Debug.Log("Socket exception: " + socketException);
                }
            }
        }
        catch (ArgumentNullException e)
        {
            Debug.LogError(String.Format("ArgumentNullException: {0}", e));
        }
        catch (SocketException e)
        {
            Debug.LogError(String.Format("SocketException: {0}", e));
        }
        catch (Exception e)
        {
            Debug.LogError(String.Format("Exception: {0}", e));
        }
    }

    public void SendMessageToBanyan(string sender, string message)
    {
        TextToSend += "{\"sender\":\"" + sender + "\",\"message\":\"" + message + "\"}";
        Debug.Log("Set TextToSend to: " + TextToSend);
    }

    protected void OnApplicationQuit()
    {
        socketConnection.Close();
    }
}
