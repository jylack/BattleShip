using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    internal class GameManager
    {

        public bool isGamePlay = false;
        bool isGameSetComplete = false;
        bool isSelecting = false;   

        Player player = new Player();
        Player cpu = new Player();
        Field field = new Field();

        SelectInterface selectInterface = new SelectInterface();

        Point curSur = new Point(0, 0);

        ConsoleKeyInfo inputKey;

        string[] logo = { "--------------------------------------------------------------------------------------------------",
"--------------------------------------------------------------------------------------------------" ,
"--=%%%%%%%%*--=#%%%%%*+%%%%%%@%*%%%%%%%%#%%%@=---*@%%%%@#=+%%%%%%%*=#%%%%+%@%%+#%%%#+%%%%%%%#+----" ,
"--=%@%@#-%%%*-+%%%%%%#++#%%%%#*+*#%%%%#+*%%%%=---*@%%%**++%%%@*%%%%*#%%%%+%%%%+#%%@#+%%%@*%%%%*---" ,
"--=%%%@--%%%*-*%%%#%%%+-+%%%%*---+%%%%+-+%%%%=---*@%%%---+%%%@-####+#%%%%+%%%%=#%%%#+%%%@=%%%%*---" ,
"--=#@%@%%@%%=-*%%-+-%%*-=%%%%*---+%%%%+-+%%%%=---*%%%%%%*+%%%%%%%+--#%%%%%%%%%=%%%%#+%%%@=%%%%+---" ,
"--=#%%%%%%%#-=#%%-=-%%#==%%%%*---+%%%%+-+%%%%=---*%%%%%#+--*%%%%%%%+#%%%%%%%%%=%%%%#+%%%%%%%%#=---" ,
"--=*%%-#-%%%#+#%%%#%%%%+=#%%%*---=#%%#+-+#%%%=---*%%%#---=====-%%%%+#%%%#+%%%#=%%%%**%%%%##*=-----" ,
"---*%%-#-%%%#+#%%%**%%#+=####*---=####+-=####=---+####---+####-####+*####+%%%#=#%%%**%%%#---------" ,
"=--+%%%%%%%#*+####=+###*=*###+---=*##*+==*######++######*+*#######*+*###*+###*+####**#%%#==-------" ,
"===+*******=-=***+==+**+=+++++---=++++===+++++++==+++++++==+++++++==++++++++++=+**+=+***+=====----" ,
"---=--=--------==-----=====-=--=====================-===---===-=========-==----=====-----==-------" };

        int selectShipIndex = 0;


        //플레이어,cpu 생성 , 시작화면 생성
        public void InitGame()
        {
            LogoPrint();

            Console.WriteLine("게임을 시작 하시겠습니까?");
            Console.WriteLine("1.게임시작\t 2.게임 종료");

            inputKey = Console.ReadKey(true);

            switch (inputKey.Key)
            {
                case ConsoleKey.D1:
                    isGamePlay = true;
                    break;
                case ConsoleKey.NumPad1:
                    isGamePlay = true;
                    break;
                default:
                    isGamePlay = false;
                    break;
            }

            if (isGamePlay)
            {
             
                player.InitPlayer();
                cpu.InitPlayer();
                cpu.Name = "CPU";
                cpu.PlaceRandomShips();
                Console.Clear();
                Console.Write("플레이어의 이름을 입력해 주세요 : ");
                player.Name = Console.ReadLine();
            }

        }

        //턴 반복되는동안 수행 여기서 좌표입력받고 cpu가 공격하고 실행함.
        public void UpdateGame()
        {
            //플레이어 세팅 완료 전까진 이거만 작동.
            if (isGameSetComplete == false)
            {
                InterFacePrint();

                //배 배치중...
                while (isSelecting == false)
                {
                    //키 입력받아서 배 재배치
                    inputKey = Console.ReadKey(true);
                    ShipPointUpdate(inputKey);

                    Field.PrintField(player);
                    selectInterface.ShipSelectingView();
                }
            }
            //세팅 완료 본격적인 게임 시작.
            else
            {
                //게임 시작시 cpu player 둘다 그려줌
                Field.PrintField(player,cpu);

                int textCount = 1;


                Point interfacePoint = new Point(Console.CursorLeft, Console.CursorTop);
                Console.SetCursorPosition(interfacePoint.PosX, interfacePoint.PosY + textCount);
                Console.WriteLine("공격하고 싶은 좌표를 입력해 주세요");
                Console.ReadLine();
            }

        }

        //움직일 배 선택하는 방법을 써줄 인터페이스 그려주기
        public void InterFacePrint()
        {
            
            //필드 그려줌
            Field.PrintField(player);

            //Console.SetCursorPosition(selectInterface.Point.PosX, selectInterface.Point.PosY);            
            selectShipIndex = selectInterface.ShipSelectView(player);

            
            Console.SetCursorPosition(selectInterface.Point.PosX, selectInterface.Point.PosY+10);
            Console.WriteLine(selectShipIndex);
            isSelecting = false;

            //선택된 배의 첫번째 인덱스 좌표 가져와서 움직일 커서 좌표로 지정
            curSur = new Point(player.Ships[selectShipIndex].Points[0].PosX, player.Ships[selectShipIndex].Points[0].PosY);

        }


        //특정좌표 덮어쓰고 지우기 메서드
        public void EraserPrint(int x, int y)
        {
            for (int i = 0; i < player.Field.Sea.GetLength(0)*2; i++)
            {
                for (int j = 0; j < player.Field.Sea.GetLength(1)*2; j++)
                {
                    Console.SetCursorPosition(x+j, y+i);
                    Console.Write(" ");
                }
            }


        }



        //나중에 씬 인터페이스 만들어서 
        //인터페이스 메서드로 키입력 메서드 만든다음
        //씬마다 키입력값이 달라지게 하면 플레이어의 스위치를 일일이 호출하거나
        //할필요없이 매니저에서 자동으로 해당씬의 키입력값을 가져올듯?
        public void ShipPointUpdate(ConsoleKeyInfo key)
        {

            switch (key.Key)
            {
                //플레이어 배 배치 완료
                case ConsoleKey.Enter:
                    isGameSetComplete = true;
                    isSelecting = true;
                    
                    break;

                //선택된 배 원하는 좌표에 지정 완료
                case ConsoleKey.Spacebar:
                    isSelecting = true;
                    
                    break;

                case ConsoleKey.R:

                    player.Ships[selectShipIndex].isHorizontal = !player.Ships[selectShipIndex].isHorizontal;
                    break;

                case ConsoleKey.A:

                    if (curSur.PosY - 1 >= 0)
                    {
                        curSur.PosY -= 1;
                    }
                    break;

                case ConsoleKey.S:

                    if (curSur.PosX + 1 <= field.Sea.GetLength(1) - player.Ships[selectShipIndex].Size)
                    {
                        curSur.PosX += 1;
                    }
                    break;
                case ConsoleKey.D:
                    if (curSur.PosY + 1 < field.Sea.GetLength(0))
                    {
                        curSur.PosY += 1;
                    }
                    break;
                case ConsoleKey.W:
                    if (curSur.PosX - 1 >= 0)
                    {
                        curSur.PosX -= 1;
                    }

                    break;
                default:
                    break;
            }


            //쉽 사이즈 만큼이동
            //쉽사이즈 만큼 기준점을 기준으로 더 해줌
            for (int i = 0; i < player.Ships[selectShipIndex].Size; i++)
            {
                if (player.Ships[selectShipIndex].isHorizontal)
                {
                    player.Ships[selectShipIndex].SetPointByIndex(i, new Point(curSur.PosX, curSur.PosY + i));
                }
                else
                {
                    player.Ships[selectShipIndex].SetPointByIndex(i, new Point(curSur.PosX + i, curSur.PosY));
                }

            }

            //가로세로 디버그 
            //Console.WriteLine(player.Ships[selectShipIndex].IsHorizontal());
            //Console.WriteLine(player.Ships[selectShipIndex].IsVertical());
            //좌표 디버그
            //Console.WriteLine($"x{curSur.PosX} : y{curSur.PosY}");


        }

        //게임도중 조건이되어 끝났을떄 게임 승패 표기
        //isGamePlay 이 false 일때 호출 
        public void EndGame()
        {
            Console.WriteLine("EndGame");
        }

        public void LogoPrint()
        {
            //로고 생성후 프린트해주기.
            for (int i = 0; i < logo.Length; i++)
            {
                Console.WriteLine(logo[i]);
            }
        }
    }
}

