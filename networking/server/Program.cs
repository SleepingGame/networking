using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace CoreNetworking
{
    public class Program
    {
        static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(new IPEndPoint(IPAddress.Any, 3000));

            Console.WriteLine("server is listening");
            socket.Listen();

            Socket client = socket.Accept();
            Console.WriteLine("recieved something");

            Player player = new Player(0, "1 hit 1000 times");

            client.Send(new InstantiatePacket("NotCube", new Vector3(1, 2, 3), Quaternion.identity, player).Serialize());

            /*
            while (true)
            {
                if (client.Available > 0)
                {
                    byte[] buffer = new byte[client.Available];
                    client.Receive(buffer);
                    Console.WriteLine(Encoding.ASCII.GetString(buffer));
                    break;
                }
            }
            */

        }
    }
}