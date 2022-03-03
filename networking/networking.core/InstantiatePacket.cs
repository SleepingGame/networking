using UnityEngine;

namespace CoreNetworking
{
    public class InstantiatePacket : BasePlacket
    {
        public string PrefabName { get; set; }
        public Vector3 Position { get; private set; }
        public Quaternion Rotation { get; private set; }

        public InstantiatePacket() : base(PacketType.Instantiate, null)
        {
            PrefabName = "";
            Position = new Vector3(0, 0, 0);
            Rotation = Quaternion.identity;
        }

        public InstantiatePacket(string prefabName, Vector3 positoin, Quaternion rotation, Player player) : base(PacketType.Instantiate, player)
        {
            PrefabName = prefabName;
            Position = positoin;
            Rotation = rotation;
        }

        public byte[] Serialize()
        {
            base.BeginSerialization();

            binaryWriter.Write(PrefabName);

            binaryWriter.Write(Position.x);
            binaryWriter.Write(Position.y);
            binaryWriter.Write(Position.z);

            binaryWriter.Write(Rotation.x);
            binaryWriter.Write(Rotation.y);
            binaryWriter.Write(Rotation.z);
            binaryWriter.Write(Rotation.w);

            return memoryStream.GetBuffer();
        }

        public new void Deserialize(byte[] buffer)
        {
            base.BeginDeserialiation(buffer);
            PrefabName = binaryReader.ReadString();

            Position = new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
            Rotation = new Quaternion(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
        }
    }
}