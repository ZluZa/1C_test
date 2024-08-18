
    public class EnemiesFactory : Factory<Enemy>
    {
        public Enemy CreateEnemy(EnemyFactoryData data)
        {
            return (Enemy) GetNewInstance().Init(data);
        }
    }
