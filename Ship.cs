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

        // x,y 로 쉽게 세팅할수 있도록 선언
        public Point(int x, int y)
        {
            _x = x;
            _y = y;
            _isHit = false;
        }
        
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
    
    /* 기능은 안다를거 같긴 한데, 시간나면 Ship 부모로 만드는걸로.. */
    public class Ship
    {
        private ShipType _type;
        private string _name;
        private int _size; // 배사이즈 나중에 필요없으면 뺄듯
        private Point[] _points;
        private bool _isAlive; // 배 가라앉았는지 플래그 필요없음 뺌
        private bool _isHorizontal =false; //가로형 인가? false이면 세로
        public ShipType Type
        {
            get { return _type;}
            set { _type = value; }
        }


        public bool isHorizontal
        {
            get { return _isHorizontal; }
            set { _isHorizontal = value; }
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
                    break;
                case ShipType.BattleShip:
                    Name = "전함";
                    Size = 4;
                    break;
                case ShipType.Cruiser:
                    Name = "순양함";
                    Size = 3;
                    break;
                case ShipType.Submarine:
                    Name = "잠수함";
                    Size = 3;
                    break;
                case ShipType.Destroyer:
                    Name = "경비정";
                    Size = 2;
                    break;
            }
            IsAlive = true;                            
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

            // 아직 살아있는데 다 맞았다?
            if (isHitAll && IsAlive)
            {
                DoSink();
            }
   
            return isHitSomeWhere;
        }
        
        // Points는 Property로, 개별 포인트는 메서드로
        public void SetPointByIndex(int index, Point point)
        {
            Points[index] = point;
        }
        
        // x,y를 받아서 내 배의 좌표가 있는 인덱스를 반환 확인
        public int FindPointByInt(int x, int y)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                if (Points[i].PosX == x && Points[i].PosY == y)
                {
                    return i;
                }                
            }
            return -1;
        }
        
        // 가라 앉았는지 출력해주는 판단. 콘솔로 가라 앉았습니다 출력, isAlive false 로 설정
        private void DoSink()
        {
            IsAlive = false;
            Console.WriteLine("배가 가라앉았습니다 꼬르륵.....");
        }

        // 1칸 짜리 배는 false로 반환, 0번째 지점과 1번째 지점의 X값이 같으면 가로형
        public bool IsHorizontal()
        {
            if (Points.Length < 2)
            {
                return false;
            }

            int x1 = Points[0].PosX;
            int x2 = Points[1].PosX;

            return x1 == x2;
        }

        // 1칸 짜리 배는 false로 반환, 0번째 지점과 1번째 지점의 Y값이 같으면 세로형
        public bool IsVertical()
        {
            if (Points.Length < 2)
            {
                return false;
            }

            int y1 = Points[0].PosY;
            int y2 = Points[1].PosY;

            return y1 == y2;
        }
    }
}