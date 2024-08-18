
    public class PlayerFactory : Factory<Player>
    {
        public Player CreatePlayer(PlayerData data)
        {
            return (Player) GetNewInstance().Init(data);
        }
    }