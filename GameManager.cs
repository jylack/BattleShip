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
        
        //게임 시작할건가?
        public bool isGamePlay = false;
        //게임 시작전 세팅이 다 끝났는가?
        bool isGameSetComplete = false;
        //배 선택중이냐?
        bool isSelecting = false;
        //게임 끝났는가?
        bool isGameOver = false;
        //현재 내가 움직일 배의 인덱스
        int selectShipIndex = 0;


        Player player = new Player();
        Player cpu = new Player();
        Field field = new Field();

        //UI 관련 클래스
        SelectInterface selectInterface = new SelectInterface();

        //우릐의 기준점. 플레이어가 조종할 좌표
        Point curSur = new Point(0, 0);
        
        ConsoleKeyInfo inputKey;

        //로-고
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

 
        //플레이어,cpu 생성 , 시작화면 생성
        public void InitGame()
        {
            LogoPrint();

            Console.WriteLine("게임을 시작 하시겠습니까?");
            Console.WriteLine("1.게임시작\t 2.게임 종료");

            //1과 2빼고 다른걸 눌렀을때 무한루프 올바른값 체킹용
            bool isGame = true;

            while (isGame)
            {
                inputKey = Console.ReadKey(true);

                switch (inputKey.Key)
                {
                    case ConsoleKey.D1:
                    //isGamePlay = true;
                    //break;
                    case ConsoleKey.NumPad1:
                        isGamePlay = true;
                        isGame = false;
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        isGamePlay = false;
                        isGame = false;
                        break;
                }
            }

            //게임 시작도 안할건데 메모리 할당 할필요없어서 초기화부분은 게임시작할시에만 작동
            if (isGamePlay)
            {
                player.InitPlayer();
                cpu.InitPlayer(10);
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
                    Field.PrintField(player);
                    selectInterface.ShipSelectingView();

                    //키 입력받아서 배 재배치
                    inputKey = Console.ReadKey(true);
                    ShipPointUpdate(inputKey);
                }
            }
            //세팅 완료 본격적인 게임 시작.
            else
            {
                string winerName = "";

                while (isGameOver == false)
                {
                    ShipsFight();

                    //비기기는 없다!
                    //럽샷이 나도 플레이어가 승리하게 접대플레이...!
                    //비김도 만들라면 둘다 true일때 비김으로 바꾸면 되긴한다. 
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
            
            bool isAttackedLocation = false;
            bool isInputTruePosX = false;
            bool isInputTruePosY = false;

            int posX = 0;
            int posY = 0;
            int addedLine = 0;
            
            //플레이어가 좌표 제대로 입력했는지 체크
            while (isAttackedLocation == false)
            {
                string strIntroduce = "공격하고 싶은 좌표를 입력해 주세요";
                addedLine = SelectInterface.PrintUnderField(player, cpu, addedLine, strIntroduce);

                string inputX = "x좌표 입력 : ";
                addedLine = SelectInterface.PrintUnderField(player, cpu, addedLine, inputX);

                // 여기 파싱 안되면 X가 0으로 입력됨 -> X파싱하고 Y파싱 둘다 검증해야할듯
                isInputTruePosX = int.TryParse(Console.ReadLine(), out posX);

                string inputY = "y좌표 입력 : ";
                addedLine = SelectInterface.PrintUnderField(player, cpu, addedLine, inputY);

                isInputTruePosY = int.TryParse(Console.ReadLine(), out posY);

                //x 와 y 둘다 제대로된 값을 넣었는지?
                if (isInputTruePosX && isInputTruePosY)
                {
                    isAttackedLocation = player.ShotMissile(posY, posX, cpu);
                }
                else
                {
                    string notValid = "잘못 입력하셨습니다 다시 입력해주세요.";
                    addedLine =+ SelectInterface.PrintUnderField(player, cpu, addedLine, notValid, ConsoleColor.Red);
                }

                addedLine++;
            }

            //좌표랜덤
            Random rndPosXY = new Random();

            //NPC가 좌표 제대로 입력했는지 체크
            isAttackedLocation = false;

            while (isAttackedLocation == false)
            {
                posX = rndPosXY.Next(player.Field.Sea.GetLength(0));
                Thread.Sleep(1);//이거 같은 랜덤 변수를 반복문안에서 여러번 쓸때 중간에 껴주면 중복값 안나옴
                posY = rndPosXY.Next(player.Field.Sea.GetLength(1));

                isAttackedLocation = cpu.ShotMissile(posX, posY, player);
            }

            //공격당한 로그 띄우기
            string attacked = $"[{cpu.Name}]가  [X : {posX}] [Y : {posY}] 를 공격했다!";
            SelectInterface.PrintUnderField(player, cpu, addedLine+4, attacked, ConsoleColor.Green);
            Thread.Sleep(1500);
        }

        //움직일 배 선택하는 방법을 써줄 인터페이스 그려주기
        public void InterFacePrint()
        {

            //필드 그려줌
            Field.PrintField(player);

            //Console.SetCursorPosition(selectInterface.Point.PosX, selectInterface.Point.PosY);            
            selectShipIndex = selectInterface.ShipSelectView(player);

            //디버그 코드
            //선택된 인덱스 제대로 들어왔나 확인
            //Console.SetCursorPosition(selectInterface.Point.PosX, selectInterface.Point.PosY + 10);
            //Console.Write("선택된 배의 인덱스값 : ");
            //Console.WriteLine(selectShipIndex);

            //지금 배 좌표 선택이 끝났는지 판별
            isSelecting = false;

            //선택된 배의 첫번째 인덱스 좌표 가져와서 움직일 커서 좌표로 지정
            curSur = new Point(player.Ships[selectShipIndex].Points[0].PosX, 
                player.Ships[selectShipIndex].Points[0].PosY);

        }

        /*
        ////특정좌표 덮어쓰고 지우기 메서드
        //public void EraserPrint(int x, int y)
        //{
        //    for (int i = 0; i < player.Field.Sea.GetLength(0) * 2; i++)
        //    {
        //        for (int j = 0; j < player.Field.Sea.GetLength(1) * 2; j++)
        //        {
        //            Console.SetCursorPosition(x + j, y + i);
        //            Console.Write(" ");
        //        }
        //    }
        //}
        */


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

            //현재 배대신 미리 좌표움직여서 충돌체크해줄 temp 배 생성
            int size = player.Ships[selectShipIndex].Size;
            Point[] temp = new Point[size];

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

                    //현재 선택된 배를 일단 돌려본다.
                    player.Ships[selectShipIndex].isHorizontal = !player.Ships[selectShipIndex].isHorizontal;
                    //좌표지정
                    player.ShipSetPoition(selectShipIndex, curSur);

                    
                    
                    //하나라도 배가 겹치면 배가있다 해줌
                    if (player.FindNonShip(player.Ships[selectShipIndex].Points, selectShipIndex))
                    {                       
                        isShipsCheck = true;
                    }

                    //지금 선택된 배의 끝자락 좌표를 가지고 있을 포인트이다.
                    Point point = player.Ships[selectShipIndex].Points[size-1];

                    //배를 돌려봤을때 끝에 좌표가 맵의 끝보다 크거나 같을때 돌리지못함
                    if (player.Field.Sea.GetLength(1) <= point.PosY  ||
                        player.Field.Sea.GetLength(0) <= point.PosX  )
                    {
                        isNonRotation = true;
                    }

                    //돌렸을때 배가 있었거나 맵의 끝이면 다시 원상복귀 해줌
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

                    //이동한곳에 배가있나? 하고 테스트
                    MoveShipCheck(curSur,temp);

                    //겹친게 있으면 커서값 원상복구
                    //이동한걸 다시 취소해야해서 역으로 증감
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
                        if (curSur.PosX + 1 < field.Sea.GetLength(0))
                        {
                            curSur.PosX += 1;
                        }
                    }
                    //현재 선택된 배가 세로일경우
                    else
                    {
                        if (curSur.PosX + 1 <= field.Sea.GetLength(0) - player.Ships[selectShipIndex].Size)
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
                        
                        if (curSur.PosY + 1 <= field.Sea.GetLength(1) - player.Ships[selectShipIndex].Size)
                        {
                            curSur.PosY += 1;

                        }
                    }
                    //현재 선택된 배가 세로일경우
                    else
                    {
                        if (curSur.PosY + 1 < field.Sea.GetLength(1))
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
            Console.Clear();
            //로고 생성후 프린트해주기.
            for (int i = 0; i < logo.Length; i++)
            {
                Console.WriteLine(logo[i]);
            }
        }
    }
}

