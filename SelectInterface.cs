using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class SelectInterface
    {
        public Point interfacePoint = new Point();
        int width;
        int height;

        public SelectInterface()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("\t\t\t\t\t");
            interfacePoint = new Point(Console.CursorLeft, Console.CursorTop);
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public Point Point
        {
            get { return interfacePoint; }
            //set { interfacePoint = value; }
        }
        public void ShipSelectingView()
        {
            int textCount = 1;

            Console.SetCursorPosition(interfacePoint.PosX, interfacePoint.PosY + textCount);
            Console.Write("Space버튼 : 해당 위치 확정");
            textCount++;

            Console.SetCursorPosition(interfacePoint.PosX, interfacePoint.PosY + textCount);
            Console.Write("R버튼 : 해당 위치 좌측 맨위 기준으로 회전.");
            textCount++;
            textCount++;

            Console.SetCursorPosition(interfacePoint.PosX, interfacePoint.PosY + textCount);
            Console.Write("W S A D 버튼 : 선택 후 위치 조정.");
            textCount++;

            Console.SetCursorPosition(interfacePoint.PosX, interfacePoint.PosY + textCount);
            
            Console.Write("방향키 버튼 : 선택 후 위치 조정.");
            textCount++;
            textCount++;

            Console.SetCursorPosition(interfacePoint.PosX, interfacePoint.PosY + textCount);
            Console.Write("Enter버튼 : 모든 배 선택 완료후 게임 시작.");

        }

        public int ShipSelectView(Player ply)
        {
            int textCount = 1;

            Console.SetCursorPosition(interfacePoint.PosX, interfacePoint.PosY + textCount);
            Console.Write("어떤 배를 이동하시겠습니까?");
            textCount++;
            Console.SetCursorPosition(interfacePoint.PosX, interfacePoint.PosY + textCount);
            Console.Write($"1.{ply.Ships[0].Name}");
            textCount++;

            Console.SetCursorPosition(interfacePoint.PosX, interfacePoint.PosY + textCount);
            Console.Write($"2.{ply.Ships[1].Name}");
            textCount++;

            Console.SetCursorPosition(interfacePoint.PosX, interfacePoint.PosY + textCount);
            Console.Write($"3.{ply.Ships[2].Name}");
            textCount++;

            Console.SetCursorPosition(interfacePoint.PosX, interfacePoint.PosY + textCount);
            Console.Write($"4.{ply.Ships[3].Name}");
            textCount++;

            Console.SetCursorPosition(interfacePoint.PosX, interfacePoint.PosY + textCount);
            Console.Write($"5.{ply.Ships[4].Name}");
            textCount++;


            Console.SetCursorPosition(interfacePoint.PosX, interfacePoint.PosY + textCount);
            Console.Write("배를 선택해 주십시오");
            textCount++;

            Console.SetCursorPosition(interfacePoint.PosX, interfacePoint.PosY + textCount);
            Console.Write("숫자를 써주세요 : ");
            textCount++;



            bool isInput = false;
            //잘못된 입력일때 재입력요청.
            while (isInput == false)
            {


                isInput = int.TryParse(Console.ReadLine(), out var shipIndex);

                if (isInput && shipIndex > 0)
                {
                    //ply.SelectShipIndex = shipIndex;
                    return shipIndex - 1;
                }
                else
                {
                    Console.SetCursorPosition(interfacePoint.PosX, interfacePoint.PosY + textCount);
                    Console.Write("잘못된 입력입니다. 다시 입력해주세요 :");
                    textCount++;
                }
            }

            return -1;
            //Console.SetCursorPosition(interfacePoint.PosX, interfacePoint.PosY + textCount);
            //Console.Write("배를 선택해 주십시오");
        }

        // 둘 중에 큰 필드의 높이를 반환
        public static int GetBiggerFieldHeight(Player player, Player cpu)
        {
            int p1Height = player.Field.Sea.GetLength(0);
            int p2Height = cpu.Field.Sea.GetLength(0);
            // 필드 비교해서 더 큰 필드 아래에서 출력 할수 있도록, 각 플레이어 네임과 Y인덱스 추가하여 +2
            int fieldHeight = (p1Height > p2Height ? p1Height : p2Height) + 2;

            return fieldHeight;
        }        
        
        // 디폴트 흰색 색상으로 텍스트 출력
        public static int PrintUnderField(Player player, Player cpu, int addedLine, string text)
        {
            return PrintUnderField(player, cpu, addedLine, text, ConsoleColor.White);
        }
        
        // 필드 아래에 출력해주기
        public static int PrintUnderField(Player player, Player cpu, int addedLine, string text, ConsoleColor cc)
        {
            int fieldHeight = GetBiggerFieldHeight(player, cpu);
            int debugHeight = Console.WindowHeight;            
            // 출력부가 윈도우 크기보다 크면
            if (fieldHeight + addedLine > debugHeight -1)
            {
                ClearUnderField(player, cpu);
                addedLine = 0;
            }
            
            Console.SetCursorPosition(0, fieldHeight + addedLine);
            Console.ForegroundColor = cc;
            Console.Write(text);
            Console.ResetColor();

            // text 한줄 추가
            return addedLine + 1;
        }
        
        // DLC 에서 제공예정
        public static int PrintRightField(Player field, int addedLine, string text)
        {
            return 0;
        }

        // 전체 클리어, 처음부터 끝까지 클리어
        public static void ClearAll()
        {
            ClearAll(0, 0);
        }
        
        // 전체 클리어, 원하는 포지션 설정 가능
        public static void ClearAll(int startWidth, int startHeight)
        {
            int windowHeight = Console.WindowHeight;
            int windowWidth = Console.WindowWidth;
            Console.SetCursorPosition(startWidth,startHeight);
            
            for (int i = startHeight; i < windowHeight; i++)
            {
                for (int j = startWidth; j < windowWidth; j++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine("");
            }
        }
        
        public static void ClearUnderField(Player player, Player cpu)
        {
            int fieldHeight = GetBiggerFieldHeight(player, cpu);
            ClearAll(0, fieldHeight);
        }     
    }
}
