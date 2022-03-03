namespace CoreNetworking
{
    public class Player
    {
        public int playerID { get; }
        public string playerName { get; }

        public Player(int id, string name)
        {
            playerID = id;
            playerName = name;
        }
    }
}