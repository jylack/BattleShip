using System;
using System.Xml;

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
        
        // 배에 전달하는 인자 추가
        public bool TakeMissile(int x, int y, Ship[] targetShips)
        {
            Point missilePoint = new Point();
            missilePoint.PosX = x;
            missilePoint.PosY = y;

            // 빈 필드에 쐈다면 true, 이미 쏜 곳이나 범위 넘어서 쐈다면 (비정상적) false
            bool isFieldHit = TakeMissile(x, y);
            
            if (isFieldHit == false)
            {
                return false;
            };
            
            //ship 중에 맞은 포인트 있나 체크
            foreach (Ship s in targetShips)
            {
                s.IsHit(missilePoint);
            }
            
            return true;
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

        public void PrintFieldName(Player player, int needToSpace)
        {
            Console.SetCursorPosition(needToSpace, 0);
            // 띄어쓸 공간 필요하면, 탭추가
            if (needToSpace > 0)
            {
                AddTabToField();
            }
            Console.Write(player.Name);
            Console.WriteLine();
        }

        // 기존 코드 리팩토링
        public static void PrintField(Player player, Player cpu)
        {
            SelectInterface.ClearAll();
            int needToSpace = player.Field.Sea.GetLength(1)*2 + 1;
            
            player.Field.PrintField(player, 0, false);
            cpu.Field.PrintField(cpu, needToSpace, true);
            // 디버깅용, cpu 배 보여줄려면 이걸로
            // cpu.Field.PrintField(cpu, needToSpace, false);
            Console.WriteLine("");
        }
        //위에거 오버로드 플레이어만 그리기
        public static void PrintField(Player player)
        {
            SelectInterface.ClearAll();
            player.Field.PrintField(player, 0, false);
            Console.WriteLine("");
        }

        // 깔끔버전!?
        // needToSpace 인자값이 0이면, 왼쪽부터 아니면 쭉 그려줌
        public void PrintField(Player somePlayer, int needToSpace, bool hideShip)
        {
            // 편하게 변수선언
            bool[,] playerSea = somePlayer.Field.Sea;
            PrintFieldName(somePlayer, needToSpace);
            
            for (int i = 0; i < playerSea.GetLength(0); i++)
            {
                // 0보다 크면 뭔가 띄워서 다른 필드 출력하는것
                Console.SetCursorPosition(needToSpace, i+1);
                PrintYIndex(i, needToSpace);
                
                for (int j = 0; j < playerSea.GetLength(1); j++)
                {
                    // 해당 좌표에 배가 있을때
                    if (IsShipOnTarget(somePlayer, i, j))
                    {
                        PrintShip(somePlayer, i, j, hideShip);
                    }
                    else
                    {
                        PrintSea(playerSea[i, j]);
                    }
                }
                // X인덱스 출력
                if (i == playerSea.GetLength(0) - 1)
                {
                    PrintXIndex(somePlayer, needToSpace, i);
                }
            }            
        }
        
        private void PrintXIndex(Player player, int needToSpace, int height)
        {
            // 0번 깨져서 어쩔수없다
            string indexStr = "ⓞ①②③④⑤⑥⑦⑧⑨ⒶⒷⒸⒹⒺⒻⒼⒽⒾⒿⓀⓁⓂⓃⓄⓅⓆⓇⓈⓉⓊⓋⓌⓍⓎⓏ";
            string playerXIndex = indexStr.Substring(0, player.Field.Sea.GetLength(1));
            Console.SetCursorPosition(needToSpace, height+2);
            if (needToSpace > 0)
            {
                AddTabToField();
            }            
            Console.Write("/");

            for (int i = 0; i < playerXIndex.Length; i++)
            {
                Console.Write(playerXIndex.ToCharArray()[i]);
                if (IsWin11() || IsUnix())
                {
                    Console.Write(" ");
                }
            }
        }
        
        // 기본 탭 설정
        private void AddTabToField()
        {
            // 기본 (윈도우 10 cmd라 가정)
            int tabSize = 2;
            AddTabToField(tabSize);
        }

        private void AddTabToField(int tabSize)
        {
            for (int i = 0; i < tabSize; i++)
            {
                Console.Write("\t");
            }            
        }

        private void PrintYIndex(int loop, int needToSpace)
        {
            if (needToSpace > 0)
            {
                AddTabToField();
            }            
            
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("♨");
                Console.ResetColor();
            }
            else
            {
                Console.Write("∼");
            }
            
            if (IsWin11() || IsUnix())
            {
                Console.Write(" ");
            }
        }

        // 배의 상태 프린트, 이중포문 버전
        private void PrintShip(Player player, int x, int y, bool hideShip)
        {
            foreach(Ship s in player.Ships)
            {
                int i = s.FindPointByInt(x, y);
                if (i == -1) { continue; }

                // 쉽 숨기고 안맞은곳 출력 -> 바다 프린트하고 돌아가버리기
                if (hideShip && !s.Points[i].IsHit)
                {
                    Console.Write("∼");
                    if (IsWin11() || IsUnix())
                    {
                        Console.Write(" ");
                    }                    
                    continue;
                }
                
                bool isHead = (i == 0);
                bool isTail = (i == s.Points.Length - 1);
                bool isBody = (!isHead && !isTail);
                
                if (s.Points[i].IsHit)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                
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
                
                if (s.Points[i].IsHit)
                {
                    Console.ResetColor();
                }
                
                if (IsWin11() || IsUnix())
                {
                    Console.Write(" ");
                }
            }
        }
        
        // 윈도 11인지 판별하는 메서드
        // https://stackoverflow.com/questions/2819934/detect-windows-version-in-net
        public static bool IsWin11()
        {
            OperatingSystem os = Environment.OSVersion;

            if (os.Platform != PlatformID.Win32NT)
            {
                return false;
            }

            // 22000 버전 부터 윈도우 11 이라고함
            if (os.Version.Build < 22000)
            {
                return false;
            }

            return true;
        }

        public static bool IsUnix()
        {
            return Environment.OSVersion.Platform == PlatformID.Unix;
        }
    }
}