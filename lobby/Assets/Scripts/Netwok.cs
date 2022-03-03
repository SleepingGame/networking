using System.Net;
using System.Net.Sockets;
using UnityEngine;
using CoreNetworking;

public class Netwok : MonoBehaviour
{
    Socket socket;
    BasePlacket basePacket;

    void Start()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Debug.Log("Connecting to server");
        socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000));
        Debug.Log("Connected!");

        while (true)
        {
            if (socket.Available > 0)
            {
                byte[] buffer = new byte[socket.Available];
                socket.Receive(buffer);

                basePacket = new BasePlacket();
                basePacket.Deserialize(buffer);

                switch (basePacket.Type)
                {
                    case BasePlacket.PacketType.None:
                        break;

                    case BasePlacket.PacketType.Transform:
                        break;

                    case BasePlacket.PacketType.Instantiate:
                        {
                            InstantiatePacket instantiate = new InstantiatePacket();
                            instantiate.Deserialize(buffer);

                            Debug.Log(instantiate.PrefabName);
                            Debug.Log(instantiate.Player.playerID);
                            Debug.Log(instantiate.Player.playerName);

                            Debug.Log(instantiate.Position);
                            Debug.Log(instantiate.Rotation);
                            //InstantiateFromResources(instantiate.PrefabName, instantiate.Position, instantiate.Rotation);
                            break;
                        }
                }
                break;
            }
        }
    }

    GameObject InstantiateFromResources(string Prefabname, Vector3 position, Quaternion rotation)
    {
        return Instantiate(Resources.Load($"/Prefabs/{Prefabname}"), position, rotation) as GameObject;
    }
}