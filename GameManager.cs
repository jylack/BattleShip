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

        public bool isGamePlay = true;


        //플레이어,cpu 생성 , 시작화면 생성
        public void InitGame()
        {
            LogoPrint();


            player.InitPlayer();
            cpu.InitPlayer();
            cpu.PlaceRandomShips();


            player.PlaceShip();

        }

        //턴 반복되는동안 수행 여기서 좌표입력받고 cpu가 공격하고 실행함.
        public void UpdateGame()
        {
            Field.PrintField(player, cpu);
            inputKey = Console.ReadKey(true);
            ShipPointUpdate(inputKey);


        }

        public void ShipPointUpdate(ConsoleKeyInfo key)
        {
            int selectShipIndex = 0;


            switch (key.Key)
            {
                case ConsoleKey.R:
                    
                    
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
                    player.Ships[selectShipIndex].SetPointByIndex(i, new Point(curSur.PosX, curSur.PosY +i));
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