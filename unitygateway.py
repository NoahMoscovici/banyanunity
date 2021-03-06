#!/usr/bin/env python3
import socket
import umsgpack
import msgpack_numpy as m
import msgpack
import numpy
import argparse
import signal
import sys
import time
import serial
import zmq
import traceback
from python_banyan.banyan_base import BanyanBase

BANYAN_IP="172.16.71.1"

class gateway(BanyanBase):
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

        # Set the subscriber topic that will be used to listen for messages to be relayed to Unity
        self.set_subscriber_topic('send_to_unity')

        self.receive_loop()


    def receive_loop(self):
        """
        Listening for messages to be relayed to Unity.
        """
        while True:
            try:
                data = self.subscriber.recv_multipart(zmq.NOBLOCK)
                if self.numpy:
                    payload = msgpack.unpackb(data[1], object_hook=m.decode)
                    self.incoming_message_processing(data[0].decode(), payload)
                else:
                    self.incoming_message_processing(data[0].decode(), umsgpack.unpackb(data[1]))
            except zmq.error.Again:
                try:
                    time.sleep(0.01)
                except serial.SerialException:
                    continue
                except UnicodeDecodeError:
                    continue
                except KeyboardInterrupt:
                    self.clean_up()


    def incoming_message_processing(self, topic, payload):
        """
        For each incoming message, define the host and port, and send it off to Unity using sockets
        :param topic:
        :param payload:
        :return:
        """
        try:
            # Check to see if the message received is a message we will send to Unity
            if topic == 'send_to_unity':
                # Define constants used to send to Unity
                host = '127.0.0.1'
                port = 5000

                # Create socket
                mySocket = socket.socket()

                mySocket.connect((host,port))
                message = str(payload)

                mySocket.send(message.encode())
                mySocket.close()

        except ConnectionRefusedError:
            print("Your Unity receiver is not listening on: " + host + ", port: " + str(port))
            print("Could not send message: " + str(payload))

        except KeyboardInterrupt:
            self.clean_up()

        except:
            exc_type, exc_value, exc_traceback = sys.exc_info()
            print("Error occured")
            print(str(repr(traceback.format_exception(exc_type, exc_value, exc_traceback))))


    def clean_up(self):
        """
        Clean up before exiting - override if additional cleanup is necessary

        :return:
        """

        self.publisher.close()
        self.subscriber.close()
        self.context.term()
        sys.exit(0)


def unity_gateway():
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

    my_gateway = gateway(**kw_options)

    # signal handler function called when Control-C occurs
    def signal_handler(sig, frame):
        print('Control-C detected. See you soon.')

        my_gateway.clean_up()
        sys.exit(0)

    # listen for SIGINT
    signal.signal(signal.SIGINT, signal_handler)
    signal.signal(signal.SIGTERM, signal_handler)


if __name__ == '__main__':
    unity_gateway()
