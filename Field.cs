using System;

namespace BattleShip
{
    public class Field
    {
        private bool[,] _sea;

        // 한번 초기화하면 받을 수 없도록
        public bool[,] Sea
        {
            get { return _sea;  }
        }

        // 암것도 없으면 10x10 필드로 생성, false로 초기화
        public Field()
        {
            _sea = new bool[10, 10];
        }
        
        // 맵 생성
        public Field(int size)
        {
            _sea = new bool[size, size];
        }
        
        /*
         * 사이즈보다 큰곳에 미사일을 쏜다면, false 반환
         * 이미 쏜곳에 미사일을 또 쐈다면 false 반환
         * 쏠수 있는 곳에 (Field의 값이 false) 쏜다면 true
         */
        public bool TakeMissile(int x, int y)
        {
            if (IsOverField(x,y))
            {
                Console.WriteLine("필드의 범위를 넘어섰습니다");
                return false;
            }

            // 이미 쏜곳에 쐈다
            if (Sea[x, y])
            {
                Console.WriteLine("이미 미사일을 쏜 곳입니다");
                return false;
            }

            // 필드가 미사일을 맞음
            Sea[x, y] = true;

            return true;
        }

        /* 필드 넘어섰는지 확인, 다른데서 안쓰면 private 으로 전환 예정 */
        public bool IsOverField(int x, int y)
        {
            int xFieldSize = Sea.GetLength(0) - 1;
            int yFieldSize = Sea.GetLength(1) - 1;   
            
            if (xFieldSize < x || yFieldSize < y)
            {
                return true;
            }
            
            return false;
        }

        public void PrintField(Player player, Player cpu)
        {
            Console.WriteLine("∼ ◀ □ ■ ▶ ∼ ∼ ∼");  
            Console.WriteLine("∼ △ ∼ ∼ ∼ ∼ ♨ ∼");  
            Console.WriteLine("∼ □ ∼ ∼ ∼ ∼ ∼ ∼");  
            Console.WriteLine("∼ □ ∼ ∼ ∼ ∼ ∼ ∼");  
            Console.WriteLine("∼ ▼ ∼ ∼ ∼ ∼ ∼ ∼ ");
        }
    }
}