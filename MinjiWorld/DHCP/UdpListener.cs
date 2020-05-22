using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MinjiWorld.DHCP
{
    internal class UdpListener
    {

        #region Class Variables
        private int portToListenTo, portToSendTo = 0;
        private string rcvCardIP;
        private bool isListening;
        private UdpState s;
        #endregion

        #region Delegates
        public delegate void DataReceivedEventHandler(byte[] dData, IPEndPoint rIpEndPoint);
        public delegate void DataSentEventHandler();
        public delegate void ErrEventHandler(string msg);
        #endregion

        #region "Events"
        public event DataReceivedEventHandler Received;
        public event DataSentEventHandler Sent;
        #endregion

        // class constructors
        public UdpListener()
        {
            isListening = false;
        }

        // overrides pass the port to listen to/sendto and startup
        public UdpListener(int portListen, int portSent, string rcvCardIP)
        {
            try
            {
                isListening = false;
                this.portToListenTo = portListen;
                this.portToSendTo = portSent;
                this.rcvCardIP = rcvCardIP;
                StartListener();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{GetType().FullName}:{e.Message}");
            }
        }

        // function to send data as a byte stream to a remote socket
        // modified to work as a callback rather than a block
        public void SendData(string dest, byte[] Data)
        {

            try
            {
                s.u.BeginSend(Data, Data.Length, dest, portToSendTo, new AsyncCallback(OnDataSent), s);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{GetType().FullName}:{e.Message}");
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
                Sent?.Invoke();

            }
            catch (Exception e)
            {
                if (isListening)
                    Console.WriteLine($"{this.GetType().FullName}:{e.Message}");
            }
        }



        // function to start the listener call back every time something is received
        private void InitListenerCallBack()
        {
            try
            {
                // start teh receive call back method
                s.u.BeginReceive(OnDataReceived, s);
            }
            catch (Exception e)
            {
                if (isListening)
                    Console.WriteLine($"{GetType().FullName}:{e.Message}");
            }
        }


        // This is the call back function, which will be invoked when a client is connected
        public void OnDataReceived(IAsyncResult ar)
        {
            UdpClient u;
            IPEndPoint e;

            try
            {

                u = ((UdpState) ar.AsyncState).u;
                e = ((UdpState) ar.AsyncState).e;

                var receiveBytes = u.EndReceive(ar, ref e);
                //raise the event with the data received
                Received?.Invoke(receiveBytes, e);
            }
            catch (Exception ex)
            {
                if (isListening)
                    Console.WriteLine($"{GetType().FullName}:{ex.Message}");
            }
            finally
            {
                // recall the call back
                InitListenerCallBack();
            }

        }

        // function to start the listener 
        // if the the listener is active, destroy it and restart
        // shall mark the flag that the listener is active
        private void StartListener()
        {
            // byte[] receiveBytes; // array of bytes where we shall store the data received
            try
            {

                isListening = false;
                //resolve the net card ip address
                var ipAddress = IPAddress.Parse(rcvCardIP);
                //get the ipEndPoint
                var ipLocalEndPoint = new IPEndPoint(ipAddress, portToListenTo);
                // if the udpclient interface is active destroy
                s.u?.Close();
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
                    Console.WriteLine($"{GetType().FullName}:{e.Message}");
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
                s.u?.Close();
                s.u = null; 
                s.e = null;

            }
            catch (Exception e)
            {
                Console.WriteLine($"{GetType().FullName}:{e.Message}");
            }
        }

        ~UdpListener()
        {
            try
            {
                StopListener();
                s.u?.Close();
                s.u = null; s.e = null;
                rcvCardIP = null;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{GetType().FullName}:{e.Message}");
            }
        }

        //class that shall hold the reference of the call backs
        private struct UdpState
        {
            public IPEndPoint e; //define an end point
            public UdpClient u; //define a client
        }
    }
}
