using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Terminal
{
    class Program
    {
      
        static void Main(string[] args)
        {
            TcpListener serversocket = new TcpListener(8080);
            int requestcount = 0;
            TcpClient clientsocket = default(TcpClient);
            serversocket.Start();
            Console.WriteLine(">> Server Started");
            clientsocket = serversocket.AcceptTcpClient();
            Console.WriteLine(">> Accept Connection from Client");
            requestcount = 0;
            while((true))
            {
                try
                {
                    requestcount += 1;
                    NetworkStream networkstream = clientsocket.GetStream();
                    byte[] bytesFrom = new byte[100025];
                    networkstream.Read(bytesFrom, 0, (int)clientsocket.ReceiveBufferSize);
                    string dataFC = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    Console.WriteLine(">> Data From Client - " + dataFC);
                    string serverresponse = "Last Message From Client" + dataFC;
                    byte[] sendBytes = Encoding.ASCII.GetBytes(serverresponse);
                    networkstream.Write(sendBytes, 0, sendBytes.Length);
                    networkstream.Flush();
                    Console.WriteLine(" >> "+serverresponse);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            clientsocket.Close();
            serversocket.Stop();
            Console.WriteLine(" >> Exit ");
            Console.ReadLine();
        }
    }
}
