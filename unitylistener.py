#!/usr/bin/env python3

import json
import socket
import logging
import msgpack_numpy as m
import argparse
import signal
import sys
import traceback
import time
import serial
import zmq
from python_banyan.banyan_base import BanyanBase

BANYAN_IP="172.16.70.1"

class listener(BanyanBase):
    """
    This class subscribes to all messages on the back plane and prints out both topic and payload.
    """

    def __init__(self, back_plane_ip_address=BANYAN_IP,
                 process_name=None, com_port='None', baud_rate=115200, log=False, quiet=False, loop_time="0.1"):
        """
        This is constructor for the Monitor class
        :param back_plane_ip_address: IP address of the currently running backplane
        :param subscriber_port: subscriber port number - matches that of backplane
        :param publisher_port: publisher port number - matches that of backplane
        """

        # initialize the base class
        super().__init__(back_plane_ip_address,  process_name=process_name, numpy=True)


        m.patch()

        self.receive_loop()


    def receive_loop(self):
        """
        Listening for messages on port 5001 from Unity.
        """
        host = "127.0.0.1"
        port = 5001

        while True:
            try:
                mySocket = socket.socket()
                mySocket.bind((host,port))

                mySocket.listen(1)
                conn, addr = mySocket.accept()
                print ("Connection from: " + str(addr))

                while True:
                    buffer = ""
                    data = conn.recv(2048)
                    buffer = data.decode(encoding="utf-8", errors="strict")
                    if len(data) > 0:
                        message = json.loads(buffer)
                        topic = "receive_unity_message"
                        self.publish_payload(message, topic)
                    else:
                        mySocket.close()
                        break

            except KeyboardInterrupt:
                self.clean_up()

            except:
                print(sys.exc_info()[0])


    def clean_up(self):
        """
        Clean up before exiting - override if additional cleanup is necessary

        :return:
        """

        self.publisher.close()
        self.subscriber.close()
        self.context.term()
        sys.exit(0)

def unity_listener():
    parser = argparse.ArgumentParser()
    parser.add_argument("-b", dest="back_plane_ip_address", default="None",
                         help="None or IP address used by Back Plane")
    parser.add_argument("-n", dest="process_name", default="Unity Sender",
                         help="Set process name in banner")
    parser.add_argument("-t", dest="loop_time", default=".1",
                         help="Event Loop Timer in seconds")

    args = parser.parse_args()
    kw_options = {}

    if args.back_plane_ip_address != "None":
        kw_options["back_plane_ip_address"] = args.back_plane_ip_address

    kw_options["process_name"] = args.process_name
    kw_options["loop_time"] = float(args.loop_time)

    my_listener = listener(**kw_options)

    # signal handler function called when Control-C occurs
    def signal_handler(sig, frame):
        print('Control-C detected. See you soon.')

        my_listener.clean_up()
        sys.exit(0)

    # listen for SIGINT
    signal.signal(signal.SIGINT, signal_handler)
    signal.signal(signal.SIGTERM, signal_handler)


if __name__ == '__main__':
    unity_listener()
