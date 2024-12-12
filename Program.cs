using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.InitGame();

            while (gameManager.isGamePlay)
            {
                gameManager.UpdateGame();                
            }

            gameManager.EndGame();

            //// Test Code
            ////
            //Field field = new Field();
            //Player player = new Player();
            //Player cpu = new Player();
            
            //// 임의의 배 2개만 세팅
            //// Cpu 는 하나만...
            //Ship carrier = new Ship(ShipType.Carrier);
            //Ship destroyer = new Ship(ShipType.Destroyer);
            
            //carrier.SetPointByIndex(0, new Point(0,0));
            //carrier.SetPointByIndex(1, new Point(0,1));
            //carrier.SetPointByIndex(2, new Point(0,2));
            //carrier.SetPointByIndex(3, new Point(0,3));
            //carrier.SetPointByIndex(4, new Point(0,4));

            //destroyer.SetPointByIndex(0, new Point(7,5));
            //destroyer.SetPointByIndex(1, new Point(8,5));
            //player.TestShips = new [] { carrier, destroyer };

            //// 가로세로 잘 작동하는지
            //// Console.WriteLine(carrier.IsVertical()); // return false
            //// Console.WriteLine(destroyer.IsVertical()); // return true
            
            //Ship battleShip = new Ship(ShipType.BattleShip);
            //battleShip.SetPointByIndex(0, new Point(1,1));
            //battleShip.SetPointByIndex(1, new Point(2,1));
            //battleShip.SetPointByIndex(2, new Point(3,1));
            //battleShip.SetPointByIndex(3, new Point(4,1));

            //// Console.WriteLine(battleShip.IsVertical()); // return true            
            
            //field.PrintField(player, cpu);
            ////
            ////
        }
    }
}
