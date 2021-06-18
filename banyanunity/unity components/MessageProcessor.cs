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
using UnityEngine;

//summary
// This script does an action based on the message forwarded by BanyanMessageListener (sent by Banyan).
// This script should be included on any object that will be doing an action, based on a message coming from Banyan
//summary

public class MessageProcessor : MonoBehaviour
{

    public void DoAction(string action, string info, int value, string target)
    {
        // Based on the Banyan message, we will turn the cube object this script is attached to, either red or blue
        ColorCube(action, info, value, target);
    }



    public void ColorCube(string action, string info, int value, string target)
    {
        // Simple action taken based on the message sent

        if(action.Equals("color"))
        {
            if (info.Equals("blue"))
            {
                GetComponent<Renderer>().material.color = new Color(0, 0, 255);
            }

            if (info.Equals("red"))
            {
                GetComponent<Renderer>().material.color = new Color(255, 0, 0);
            }

            // Below is an example of how to send a message back to Banyan.
            // You can put the following code in any script

            // Find the Object Sender
            GameObject theObject = GameObject.Find("Sender");
            // Find the script attachted to that Object
            BanyanMessageSender messageSender = theObject.GetComponent<BanyanMessageSender>();

            // Call this function to send your message back to Banyan
            messageSender.SendMessageToBanyan(target, "The cube turned " + info);
            Debug.Log("Finished sending message: " + info);
        }
    }
}