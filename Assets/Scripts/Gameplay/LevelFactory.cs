
    public class LevelFactory : Factory<Level>
    {
        public Level CreateLevel(LevelData data)
        {
            return (Level) GetNewInstance().Init(data);
        }
    }
