using System.IO;

namespace CoreNetworking
{
    public class BasePlacket
    {
        public MemoryStream memoryStream;
        public BinaryWriter binaryWriter;
        public BinaryReader binaryReader;

        public enum PacketType
        {
            None = -1,

            Transform,
            Instantiate,
            Destroy,
            Rotate,
            Choose
        }

        public PacketType Type { get; private set; }
        public Player Player { get; private set; }

        public BasePlacket()
        {
            Type = PacketType.None;
            Player = null;
        }

        public BasePlacket(PacketType type, Player player)
        {
            Type = type;
            Player = player;
        }

        protected void BeginSerialization()
        {
            memoryStream = new MemoryStream();
            binaryWriter = new BinaryWriter(memoryStream);

            binaryWriter.Write((int)Type);
            binaryWriter.Write(Player.playerID);
            binaryWriter.Write(Player.playerName);
        }

        protected void BeginDeserialiation(byte[] buffer)
        {
            memoryStream = new MemoryStream(buffer);
            binaryReader = new BinaryReader(memoryStream);

            Type = (PacketType)binaryReader.ReadInt32();
            Player = new Player(binaryReader.ReadInt32(), binaryReader.ReadString());
        }

        public void Deserialize(byte[] buffer)
        {
            memoryStream = new MemoryStream(buffer);
            binaryReader = new BinaryReader(memoryStream);

            Type = (PacketType)binaryReader.ReadInt32();
            Player = new Player(binaryReader.ReadInt32(), binaryReader.ReadString());
        }
    }
}