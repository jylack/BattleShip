namespace BattleShip
{
    public class Field
    {
        private bool[,] _sea;

        public bool[,] Sea
        {
            get;
            set;
        }

        // 암것도 없으면 10x10 필드로 생성
        public Field()
        {
            _sea = new bool[10, 10];
        }
        
        // 맵 생성
        public Field(int size)
        {
            _sea = new bool[size, size];
        }
    }
}