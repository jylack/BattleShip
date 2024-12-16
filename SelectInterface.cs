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

            Console.SetCursorPosition(interfacePoint.PosX, interfacePoint.PosY + textCount);
            Console.Write("Enter버튼 : 모든 배 선택 완료후 게임 시작.");
            textCount++;
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
    }
}
