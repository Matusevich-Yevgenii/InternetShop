using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InternetShop.WindowsFolder
{
    public class HandlerServer2 : Handler
    {
        public override void HandleRequest(int request, int userId, DateTime date, string city, string address, string telephone)
        {
            if (request == 2)
            {
                byte[] bytes = new byte[1024];
                IPHostEntry ipHost = Dns.GetHostEntry("localhost");
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11001);

                Socket sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                sender.Connect(ipEndPoint);

                string message = "New order: " + userId + " " + date + " " + city + " " + address +
                                 " " + telephone;

                byte[] msg = Encoding.UTF8.GetBytes(message);

                int bytesSent = sender.Send(msg);

                int bytesRec = sender.Receive(bytes);

                if (bytesRec == 1)
                {
                    MessageBox.Show("Order is accepted");
                }
                else
                {
                    MessageBox.Show("Order isn't accepted");
                }

                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            else if (Successor != null)
            {
                Successor.HandleRequest(request, userId, date, city, address, telephone);
            }
        }
    }
}
