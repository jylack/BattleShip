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

        // 1. 바다는 bool sea로 본다.
        // 2. 플레이어의 배가 있으면 배를 프린트한다.
        public void PrintField(Player player, Player cpu)
        {
            Console.WriteLine("바다를 출력할게요~");

            // 플레이어 필드
            for (int i = 0; i < Sea.GetLength(0); i++)
            {
                for (int j = 0; j < Sea.GetLength(1); j++)
                {
                    // 해당 좌표에 배가 있을때
                    if (IsShipOnTarget(player, i, j))
                    {
                        PrintShip(player, i, j);
                    }
                    else
                    {
                        PrintSea(Sea[i, j]);
                    }
                    Console.Write(" ");
                }
                Console.WriteLine("");
            }

        }

        // private bool IsSea(Player somePlayer, int x, int y)
        // {
        //     bool[,];
        // }
        
        /* 해당 x,y 값에 플레이어 배가 있는지 확인 */
        private bool IsShipOnTarget(Player somePlayer, int x, int y)
        {
            if (somePlayer.TestShips != null)
            {
                foreach (Ship s in somePlayer.TestShips)
                {
                    if (s.FindPointByInt(x, y) >= 0)
                    {
                        return true;
                    }
                }
            }
            

            return false;

            //원본코드
            //foreach (Ship s in somePlayer.TestShips)
            //{
            //    if (s.FindPointByInt(x, y) >= 0)
            //    {
            //        return true;
            //    }
            //}

            //return false;
        }

        // 바다에 쐈을 경우 프린트
        private void PrintSea(bool isSeaHit)
        {
            if (isSeaHit)
            {
                Console.Write("♨");
            }
            else
            {
                Console.Write("∼");
            }            
        }

        // 배의 상태 프린트
        private void PrintShip(Player player, int x, int y)
        {
            foreach(Ship s in player.TestShips)
            {
                int i = s.FindPointByInt(x, y);
                if (i == -1)
                {
                    continue;
                }
                bool isHead = (i == 0);
                bool isTail = (i == s.Points.Length - 1);
                bool isBody = (!isHead && !isTail);
                
                if (isHead && s.IsHorizontal() && s.Points[i].IsHit)
                {
                    Console.Write("◀");
                }

                if (isHead && s.IsHorizontal() && s.Points[i].IsHit == false)
                {
                    Console.Write("◁");
                }

                if (isTail && s.IsHorizontal() && s.Points[i].IsHit)
                {
                    Console.Write("▶");   
                }

                if (isTail && s.IsHorizontal() && s.Points[i].IsHit == false)
                {
                    Console.Write("▷");
                }

                if (isBody && s.Points[i].IsHit)
                {
                    Console.Write("■");
                }
                
                if (isBody && s.Points[i].IsHit == false)
                {
                    Console.Write("□");
                }
                
                if (isHead && s.IsVertical() && s.Points[i].IsHit)
                {
                    Console.Write("▲");
                }

                if (isHead && s.IsVertical() && s.Points[i].IsHit == false)
                {
                    Console.Write("△");
                }

                if (isTail && s.IsVertical() && s.Points[i].IsHit)
                {
                    Console.Write("▼");   
                }

                if (isTail && s.IsVertical() && s.Points[i].IsHit == false)
                {
                    Console.Write("▽");
                }                
                
            }
        }
    }
}