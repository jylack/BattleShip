namespace BattleShip
{
    // 종류 추가 예정
    public enum ShipType
    {
        Submarine,None
    }
    
    public struct Point
    {
        private int x;
        private int y;
        private bool isHit;
    }
    
    public class Ship
    {
        private ShipType _type;
        private string _name;
        private int _size; // 배사이즈 나중에 필요없으면 뺄듯
        private Point _point;
        private bool _isAlive; // 배 가라앉았는지 플래그 필요없음 뺌

        public ShipType Type
        {
            get { return _type;}
            set { _type = value; }
        }

        public string Name
        {
            get;
            set;
        }

        public int Size
        {
            get;
            set;
        }

        public Point Point
        {
            get { return _point;  }
            set { _point = value; }
        }

        public bool IsAlive
        {
            get { return _isAlive;}
            set { _isAlive = value; }
        }
    }
}