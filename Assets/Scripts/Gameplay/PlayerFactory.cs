
    public class PlayerFactory : Factory<Player>
    {
        public Player CreateEnemy(PlayerData data)
        {
            return (Player) GetNewInstance().Init(data);
        }
    }