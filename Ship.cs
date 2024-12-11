using System;

namespace BattleShip
{
    // 나무위키 1990년판 참고
    public enum ShipType
    {
        // 항공모함, 전함, 순양함, 잠수함, 구축함
        Carrier,BattleShip,Cruiser,Submarine,Destroyer,end
    }
    
    public struct Point
    {
        private int _x;
        private int _y;
        private bool _isHit;

        public int PosX
        {
            get { return _x; }
            set { _x = value; }
        }

        public int PosY
        {
            get { return _y; }
            set { _y = value; }
        }

        public bool IsHit
        {
            get { return _isHit; }
            set { _isHit = value; }
        }

        
        // 두개 앤드 비교해서 맞았는지
        public bool CompareHit(Point point)
        {
            return PosX == point.PosX && PosY == point.PosY;
        }
    }
    
    public class Ship
    {
        private ShipType _type;
        private string _name;
        private int _size; // 배사이즈 나중에 필요없으면 뺄듯
        private Point[] _points;
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
            get { return _size; }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("사이즈는 음수가 될 수 없습니다");
                    _size = 1;
                }
                else
                {
                    _size = value;
                }
            }
        }

        public Point[] Points
        {
            get { return _points;  }
            set { _points = value; }
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
            Points = new Point[3];
            IsAlive = true;
        }
        public Ship(ShipType type)
        {
            switch (type)
            {
                case ShipType.Carrier:
                    Name = "항공모함";
                    Size = 5;
                    IsAlive = true;                    
                    break;
                case ShipType.BattleShip:
                    Name = "전함";
                    Size = 4;
                    IsAlive = true;                            
                    break;
                case ShipType.Cruiser:
                    Name = "순양함";
                    Size = 3;
                    IsAlive = true;
                    break;
                case ShipType.Submarine:
                    Name = "잠수함";
                    Size = 3;
                    IsAlive = true;                    
                    break;
                case ShipType.Destroyer:
                    Name = "경비정";
                    Size = 2;
                    IsAlive = true;                    
                    break;
            }
            Type = type;
            Points = new Point[Size];
        }

        public Ship(ShipType shipType, string name, int size, bool isAlive)
        {
            Type = shipType;
            Name = name;
            Size = size;
            Points = new Point[size];
            IsAlive = isAlive;
        }

        // 내 배의 Point를 모두 검사하면서 맞았는지 확인
        // 다맞았을때는 DoSink를 실행하면서 isAlive를 false로 바꿔줌 
        public bool IsHit(Point point)
        {
            bool isHitSomeWhere = false;
            bool isHitAll = true;
            for (int i = 0; i < Points.Length; i++)
            {
                if (Points[i].CompareHit(point))
                {
                    Points[i].IsHit = true;
                    isHitSomeWhere = true;
                }
                isHitAll = isHitAll && Points[i].IsHit;
            }

            if (isHitAll)
            {
                DoSink();
            }
   
            return isHitSomeWhere;
        }

        public void SetPoints(Point[] points)
        {
            Points = points;
        }
        
        // 가라 앉았는지 출력해주는 판단. 콘솔로 가라 앉았습니다 출력, isAlive false 로 설정
        private void DoSink()
        {
            IsAlive = false;
            Console.WriteLine("배가 가라앉았습니다 꼬르륵.....");
        }
    }
}