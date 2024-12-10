namespace BattleShip
{

    // 나무위키 1990년판 참고
    public enum ShipType
    {
        // 항공모함, 전함, 순양함, 잠수함, 구축함
        Carrier,BattleShip,Cruiser,Submarine,Destroyer
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

        // 없으면 기본 잠수함
        public Ship()
        {
            Type = ShipType.Submarine;
            Name = "잠수함";
            Size = 3;
            Point = new Point();
            IsAlive = true;
        }
        public Ship(ShipType type)
        {
            Type = type;
            switch (type)
            {
                case ShipType.Carrier:
                    Name = "항공모함";
                    Size = 5;
                    Point = new Point();
                    IsAlive = true;                    
                    break;
                case ShipType.BattleShip:
                    Name = "전함";
                    Size = 4;
                    Point = new Point();
                    IsAlive = true;                            
                    break;
                case ShipType.Cruiser:
                    Name = "순양함";
                    Size = 3;
                    Point = new Point();
                    IsAlive = true;
                    break;
                case ShipType.Submarine:
                    Name = "잠수함";
                    Size = 3;
                    Point = new Point();
                    IsAlive = true;                    
                    break;
                case ShipType.Destroyer:
                    Name = "경비정";
                    Size = 2;
                    Point = new Point();
                    IsAlive = true;                    
                    break;
            }
        }

        public Ship(ShipType shipType, string name, int size, Point point, bool isAlive)
        {
            Type = shipType;
            Name = name;
            Size = size;
            Point = point;
            IsAlive = isAlive;
        }
    }
}