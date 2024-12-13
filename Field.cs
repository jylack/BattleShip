using System;

namespace BattleShip
{
    enum CharIndex
    {
        A = 10, B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z
    }
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
            _sea = new bool[15, 15];
        }
        
        // 맵 생성, 0-9 A-Z 까지 더이상 표기 힘드니까 36까지 받음
        public Field(int size)
        {
            if (size < 10 || size > 36)
            {
                Console.WriteLine("10 미만 36 초과의 크기는 안됩니다");
                _sea = new bool[10, 10];
            }
            else
            {
                _sea = new bool[size, size];                
            }
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

        public void PrintFieldName(Player player, Player cpu)
        {
            // y열의 좌표 길이 +1 (+ 띄어쓰기)
            int cpuNamePos = player.Field.Sea.GetLength(1);
            Console.Write(player.Name);
            // y열의 갯수 체크해서 가운데에 이름 출력해주기
            Console.SetCursorPosition(cpuNamePos, Console.CursorTop);
            Console.Write("\t"+cpu.Name);
            Console.WriteLine();
        }

        // 1. 바다는 bool sea로 본다.
        // 2. 플레이어의 배가 있으면 배를 프린트한다.
        public void PrintField(Player player, Player cpu)
        {
            Console.WriteLine();
            bool[,] playerSea = player.Field.Sea;
            bool[,] opponentSea = cpu.Field.Sea;

            // 필드의 이름 출력
            PrintFieldName(player, cpu);

            
            for (int i = 0; i < playerSea.GetLength(0); i++)
            {
                PrintYIndex(i);
                // 플레이어 필드
                for (int j = 0; j < playerSea.GetLength(1); j++)
                {
                    // 해당 좌표에 배가 있을때
                    if (IsShipOnTarget(player, i, j))
                    {
                        PrintShip(player, i, j);
                    }
                    else
                    {
                        PrintSea(playerSea[i, j]);
                    }
                    Console.Write("");
                }
                
                Console.Write("\t");
                PrintYIndex(i);
                
                // CPU 필드
                for (int j = 0; j < opponentSea.GetLength(1); j++)
                {
                    // 해당 좌표에 배가 있을때
                    if (IsShipOnTarget(cpu, i, j))
                    {
                        PrintShip(cpu, i, j);
                    }
                    else
                    {
                        PrintSea(opponentSea[i, j]);
                    }
                    Console.Write("");
                }                
                Console.WriteLine("");
                
                // X인덱스 출력
                if (i == playerSea.GetLength(0) - 1)
                {
                    printXIndex(player, cpu);
                }
            }
        }

        // private bool IsSea(Player somePlayer, int x, int y)
        // {
        //     bool[,];
        // }

        private void printXIndex(Player player, Player cpu)
        {
            // 0번 깨져서 어쩔수없다
            string indexStr = "ⓞ①②③④⑤⑥⑦⑧⑨ⒶⒷⒸⒹⒺⒻⒼⒽⒾⓏⓀⓁⓂⓃⓄⓅⓆⓇⓈⓉⓊⓋⓌⓍⓎⓏ";
            string playerXIndex = indexStr.Substring(0, player.Field.Sea.GetLength(1));
            string cpuXIndex = indexStr.Substring(0, cpu.Field.Sea.GetLength(1));
            Console.WriteLine("/" + playerXIndex + "\t/" + cpuXIndex);
        }
        private void PrintYIndex(int loop)
        {
            if (loop < 10)
            {
                Console.Write(loop);                
            }
            else
            {
                Console.Write((CharIndex)loop);
            }
        }
        /* 해당 x,y 값에 플레이어 배가 있는지 확인 */
        private bool IsShipOnTarget(Player somePlayer, int x, int y)
        {
            if (somePlayer.Ships == null)
            {
                Console.WriteLine("플레이어에게 배가 초기화 되지 않았습니다");
                return false;
            }
            
            foreach (Ship s in somePlayer.Ships)
            {
                if (s?.FindPointByInt(x, y) >= 0)
                {
                    return true;
                }
            }            
            return false;
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
            foreach(Ship s in player.Ships)
            {
                int i = s.FindPointByInt(x, y);
                if (i == -1) { continue; }
                
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