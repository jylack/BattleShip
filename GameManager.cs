using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    internal class GameManager
    {
        Player player = new Player();
        Player cpu = new Player();
        Field field = new Field();
        Ship[] ships = new Ship[(int)ShipType.end];


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

        public bool isGamePlay = true;


        //플레이어,cpu 생성 , 시작화면 생성
        public void InitGame()
        {
            LogoPrint();


            player.InitPlayer();
            cpu.InitPlayer();
            //cpu.PlaceRandomShips();

            //배들 종류별로 하나씩 다넣음.
            for (int i = 0; i < ships.Length; i++)
            {
                ships[i] = new Ship((ShipType)i);
            }

            //생성된것 하나씩 다 넣음
            player.Ships = ships;
            cpu.Ships = ships;


        }

        //턴 반복되는동안 수행 여기서 좌표입력받고 cpu가 공격하고 실행함.
        public void UpdateGame()
        {
            player.PlaceShip();
            field.PrintField(player, cpu);
            Console.ReadLine();
        }

        public void ShipPointUpdate(ConsoleKeyInfo key)
        {
            int selectShipIndex = 0;

            Point curSur = new Point(0, 0);



            switch (key.Key)
            {
                case ConsoleKey.A:
                    curSur.PosY -= 1;
                    break;
                case ConsoleKey.S:
                    curSur.PosY += 1;

                    break;
                case ConsoleKey.D:
                    curSur.PosY += 1;

                    break;
                case ConsoleKey.W:
                    curSur.PosY -= 1;

                    break;
                default:
                    break;
            }

            Point selectPoint = new Point();
            selectPoint =
                new Point(player.Ships[selectShipIndex].Points[0].PosX + curSur.PosX,
                player.Ships[selectShipIndex].Points[0].PosY + curSur.PosY);


            //쉽 사이즈 만큼이동
            //쉽사이즈 만큼 기준점을 기준으로 더 해줌
            for (int i = 0; i < player.Ships[i].Size; i++)
            {                
                player.Ships[selectShipIndex].
                    SetPointByIndex(selectShipIndex, selectPoint);
            }


        }

        //게임도중 조건이되어 끝났을떄 게임 승패 표기
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


/*

 
 
 */