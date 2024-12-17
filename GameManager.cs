using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShip
{
    internal class GameManager
    {

        public bool isGamePlay = false;

        bool isGameSetComplete = false;

        bool isSelecting = false;

        bool isGameOver = false;


        Player player = new Player();
        Player cpu = new Player();
        Field field = new Field();

        SelectInterface selectInterface = new SelectInterface();

        Point curSur = new Point(0, 0);

        ConsoleKeyInfo inputKey;

        string[] logo = {   "--------------------------------------------------------------------------------------------------" ,
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
                //isGamePlay = true;
                //break;
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
                string winerName = "";

                while (isGameOver == false)
                {
                    ShipsFight();

                    if (player.IsAllHitShips())
                    {
                        winerName = player.Name;
                        isGameOver = true;
                    }

                    else if (cpu.IsAllHitShips())
                    {
                        winerName = cpu.Name;
                        isGameOver = true;
                    }

                }

                isGamePlay = false;

                Console.Clear();

                Console.WriteLine($"{winerName}가 이겼습니다!");
            }

        }



        public void ShipsFight()
        {
            //게임 시작시 cpu player 둘다 그려줌
            Field.PrintField(player, cpu);

            int textCount = 1;

            //인터페이스 좌표 초기화
            Point interfacePoint = new Point(Console.CursorLeft, Console.CursorTop);
            Console.SetCursorPosition(interfacePoint.PosX, interfacePoint.PosY + textCount);
            textCount++;

            bool isAttackedLocation = false;
            bool isInputTest = false;

            int posX = 0;
            int posY = 0;

            //플레이어가 좌표 제대로 입력했는지 체크
            while (isAttackedLocation == false)
            {
                Console.WriteLine("공격하고 싶은 좌표를 입력해 주세요");

                Console.Write("x좌표 입력 : ");
                isInputTest = int.TryParse(Console.ReadLine(), out posX);


                Console.WriteLine();

                Console.Write("y좌표 입력 : ");
                isInputTest = int.TryParse(Console.ReadLine(), out posY);

                if (isInputTest)
                {
                    isAttackedLocation = player.ShotMissile(posY, posX, cpu);
                }
                else
                {
                    Console.WriteLine("잘못 입력하셨습니다 다시 입력해주세요.");
                }

            }

            Random rndPosXY = new Random();

            isAttackedLocation = false;

            //NPC가 좌표 제대로 입력했는지 체크
            while (isAttackedLocation == false)
            {
                posX = rndPosXY.Next(player.Field.Sea.GetLength(0));
                posY = rndPosXY.Next(player.Field.Sea.GetLength(1));

                isAttackedLocation = cpu.ShotMissile(posX, posY, player);
            }

            //공격당한 로그 띄우기
            textCount++;
            textCount++;
            textCount++;
            textCount++;
            Console.SetCursorPosition(interfacePoint.PosX, interfacePoint.PosY + textCount);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[{cpu.Name}]가  [X : {posX}] [Y : {posY}] 를 공격했다!");
            Console.ResetColor();

            Thread.Sleep(1500);


        }

        //움직일 배 선택하는 방법을 써줄 인터페이스 그려주기
        public void InterFacePrint()
        {

            //필드 그려줌
            Field.PrintField(player);

            //Console.SetCursorPosition(selectInterface.Point.PosX, selectInterface.Point.PosY);            
            selectShipIndex = selectInterface.ShipSelectView(player);


            Console.SetCursorPosition(selectInterface.Point.PosX, selectInterface.Point.PosY + 10);
            Console.WriteLine(selectShipIndex);
            isSelecting = false;

            //선택된 배의 첫번째 인덱스 좌표 가져와서 움직일 커서 좌표로 지정
            curSur = new Point(player.Ships[selectShipIndex].Points[0].PosX, player.Ships[selectShipIndex].Points[0].PosY);

        }


        //특정좌표 덮어쓰고 지우기 메서드
        public void EraserPrint(int x, int y)
        {
            for (int i = 0; i < player.Field.Sea.GetLength(0) * 2; i++)
            {
                for (int j = 0; j < player.Field.Sea.GetLength(1) * 2; j++)
                {
                    Console.SetCursorPosition(x + j, y + i);
                    Console.Write(" ");
                }
            }


        }

        //템프값에 현재 선택된 배의 좌표들을 넘겨줌
        public void MoveShipCheck(Point cursur , Point[] temp)
        {

            for (int i = 0; i < player.Ships[selectShipIndex].Size; i++)
            {
                if (player.Ships[selectShipIndex].isHorizontal)
                {
                    temp[i] = new Point(curSur.PosX, curSur.PosY + i);
                }
                else
                {
                    temp[i] = new Point(curSur.PosX + i, curSur.PosY);
                }
            }
        }

        //나중에 씬 인터페이스 만들어서 
        //인터페이스 메서드로 키입력 메서드 만든다음
        //씬마다 키입력값이 달라지게 하면 플레이어의 스위치를 일일이 호출하거나
        //할필요없이 매니저에서 자동으로 해당씬의 키입력값을 가져올듯?
        public void ShipPointUpdate(ConsoleKeyInfo key)
        {
            //배들 있는지 체크 
            bool isShipsCheck = false;
            //배를 돌릴수 있는지 체크
            bool isNonRotation = false;

            Point[] temp = new Point[player.Ships[selectShipIndex].Size];
            int size = player.Ships[selectShipIndex].Size;

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

                //회전시킬꺼야~
                case ConsoleKey.R:


                    player.Ships[selectShipIndex].isHorizontal = !player.Ships[selectShipIndex].isHorizontal;
                    
                    player.ShipSetPoition(selectShipIndex, curSur);

                    
                    
                    //하나라도 배가 겹치면 배가있다 해줌
                    if (player.FindNonShip(player.Ships[selectShipIndex].Points, selectShipIndex))
                    {                       
                        isShipsCheck = true;
                    }

                    //지금 선택된 배의 끝자락 좌표를 가지고 있을 포인트이다.
                    Point point = player.Ships[selectShipIndex].Points[size-1];

                    //배를 돌려봤을때 끝에 좌표가 맵의 끝보다 크거나 같을때 돌리지못함
                    if (player.Field.Sea.GetLength(0) <= point.PosY  ||
                        player.Field.Sea.GetLength(1) <= point.PosX  )
                    {
                        isNonRotation = true;
                    }

                    //돌렸을때 배가 있었으면 다시 원상복귀 해줌
                    if (isShipsCheck || isNonRotation)
                    {
                        player.Ships[selectShipIndex].isHorizontal = !player.Ships[selectShipIndex].isHorizontal;
                    }
                    //배가 없었으면 테스트하느라 바꿧던거 그대로 내려감


                    break;

                //방향키 좌표이동
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:

                    
                    if (curSur.PosY - 1 >= 0)
                    {
                        curSur.PosY -= 1;
                    }


                    MoveShipCheck(curSur,temp);

                    //겹친게 있으면 커서값 원상복구
                    //이동한걸 다시 취소해야해서
                    if (player.FindNonShip(temp, selectShipIndex))
                    {
                        curSur.PosY += 1;
                    }
                    

                    break;

                case ConsoleKey.S:
                case ConsoleKey.DownArrow:

                    //현재 선택된 배가 가로일경우
                    if (player.Ships[selectShipIndex].isHorizontal)
                    {
                        if (curSur.PosX + 1 < field.Sea.GetLength(1))
                        {
                            curSur.PosX += 1;
                        }
                    }
                    //현재 선택된 배가 세로일경우
                    else
                    {
                        if (curSur.PosX + 1 <= field.Sea.GetLength(1) - player.Ships[selectShipIndex].Size)
                        {
                            curSur.PosX += 1;
                        }
                    }
                    

                    MoveShipCheck(curSur, temp);


                    //겹친게 있으면 커서값 원상복구
                    //이동한걸 다시 취소해야해서
                    if (player.FindNonShip(temp, selectShipIndex))
                    {
                        curSur.PosX -= 1;
                    }
                    break;

                case ConsoleKey.D:
                case ConsoleKey.RightArrow:

                    //현재 선택된 배가 가로일경우
                    if (player.Ships[selectShipIndex].isHorizontal)
                    {
                        
                        if (curSur.PosY + 1 <= field.Sea.GetLength(0) - player.Ships[selectShipIndex].Size)
                        {
                            curSur.PosY += 1;

                        }
                    }
                    //현재 선택된 배가 세로일경우
                    else
                    {
                        if (curSur.PosY + 1 < field.Sea.GetLength(0))
                        {
                            curSur.PosY += 1;
                        }
                    }

                    MoveShipCheck(curSur, temp);

                    //겹친게 있으면 커서값 원상복구
                    //이동한걸 다시 취소해야해서
                    if (player.FindNonShip(temp, selectShipIndex))
                    {
                        curSur.PosY -= 1;
                    }


                    break;

                case ConsoleKey.W:
                case ConsoleKey.UpArrow:

                    if (curSur.PosX - 1 >= 0)
                    {
                        curSur.PosX -= 1;
                    }

                    MoveShipCheck(curSur, temp);



                    //겹친게 있으면 커서값 원상복구
                    //이동한걸 다시 취소해야해서
                    if (player.FindNonShip(temp, selectShipIndex))
                    {
                        curSur.PosX += 1;

                    }


                    break;

                default:

                    break;
            }


            //좌표확정
            player.ShipSetPoition(selectShipIndex, curSur);

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

