using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Microsoft.Win32;


namespace SmallDhcpServer
{
    class UDPListener

    {
        #region Class Variables
        private Int32 portToListenTo, portToSendTo = 0;
        private string rcvCardIP;
        private bool isListening;
        private UdpState s;
        #endregion

        #region Delegates
        public delegate void DataRcvdEventHandler(byte[] dData, IPEndPoint rIpEndPoint);
       
        public delegate void ErrEventHandler(string msg);
        #endregion

        #region "Events"
        public event DataRcvdEventHandler Reveived;
        #endregion
        // class constructors
        public UDPListener()
        {
            isListening = false;
        }
        // overrides pass the port to listen to/sendto and startup
        public UDPListener(Int32 portListen, Int32 PortSent, string rcvCardIP)
        {
            try
            {
                isListening = false;
                this.portToListenTo = portListen;
                this.portToSendTo = PortSent;
                this.rcvCardIP = rcvCardIP;
                StartListener();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("{0}:{1}", this.GetType().FullName, e.Message));
            }
        }

        // function to send data as a byte stream to a remote socket
        // modified to work as a callback rather than a block
        public void SendData(byte[] Data)
        {

            try
            {
                s.u.BeginSend(Data, Data.Length, "255.255.255.255", portToSendTo, new AsyncCallback(OnDataSent), s);
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("{0}:{1}", this.GetType().FullName, e.Message));
            }
        }

        // This is the call back function, which will be invoked when a client is connected
        public void OnDataSent(IAsyncResult ar)
        {
            try
            {
                // get the data
                UdpClient ii = ((UdpState)ar.AsyncState).u;
                // stop the send call back
                ii.EndSend(ar); 
            }
            catch (Exception e)
            {
                if (isListening)
                    Console.WriteLine(string.Format("{0}:{1}", this.GetType().FullName, e.Message));
            }
        }



        // function to start the listener call back everytime something is recieved
        private void InitListenerCallBack()
        {
            try
            {
                // start teh recieve call back method
                s.u.BeginReceive(new AsyncCallback(OnDataRecieved), s);
            }
            catch (Exception e)
            {
                if (isListening)
                    Console.WriteLine(string.Format("{0}:{1}", this.GetType().FullName, e.Message));
            }
        }


        // This is the call back function, which will be invoked when a client is connected
        public void OnDataRecieved(IAsyncResult ar)
        {
            Byte[] receiveBytes;
            UdpClient u;
            IPEndPoint e;

            try
            {

                u = ((UdpState) ar.AsyncState).u;
                e = ((UdpState) ar.AsyncState).e;

                receiveBytes = u.EndReceive(ar, ref e);
                //raise the event with the data recieved
                Reveived(receiveBytes, e);
            }
            catch (Exception e)
            {
                if (isListening)
                    Console.WriteLine(string.Format("{0}:{1}", this.GetType().FullName, e.Message));
            }
            finally
            {
                // recall the call back
                InitListenerCallBack();
            }

        }

        // function to start the listener 
        // if the the listner is active, destroy it and restart
        // shall mark the flag that the listner is active
        private void StartListener()
        {
            // byte[] receiveBytes; // array of bytes where we shall store the data recieved
            IPAddress ipAddress;
            IPEndPoint ipLocalEndPoint;
            try
            {

                isListening = false;
                //resolve the net card ip address
                ipAddress = IPAddress.Parse(rcvCardIP);
                //get the ipEndPoint
                ipLocalEndPoint = new IPEndPoint(ipAddress, portToListenTo);
                // if the udpclient interface is active destroy
                if (s.u != null) s.u.Close();
                //re initialise the udp client

                s = new UdpState
                {
                    e = ipLocalEndPoint,
                    u = new UdpClient(ipLocalEndPoint)
                };
                // set to start listening
                isListening = true; 
                // wait for data
                InitListenerCallBack();
            }
            catch (Exception e)
            {
                if (isListening)
                    Console.WriteLine(string.Format("{0}:{1}", this.GetType().FullName, e.Message));
                throw e;
            }
            finally
            {
                if (s.u == null)
                {
                    Thread.Sleep(1000);
                    StartListener();
                }
            }
        }



        //stop the listener thread
        public void StopListener()
        {
            try
            {
                isListening = false;
                if (s.u != null) s.u.Close();
                s.u = null; 
                s.e = null;

            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("{0}:{1}", this.GetType().FullName, e.Message));
            }
        }

        ~UDPListener()
        {
            try
            {
                StopListener();
                if (s.u != null) s.u.Close();
                s.u = null; s.e = null;
                rcvCardIP = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("{0}:{1}", this.GetType().FullName, e.Message));
            }
        }

        //class that shall hold the reference of the call backs
        struct UdpState
        {
            public IPEndPoint e; //define an end point
            public UdpClient u; //define a client
        }
    }
}
