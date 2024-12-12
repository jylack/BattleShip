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
            
            // TestCode
            Field field = new Field();
            field.PrintField(null, null);
        }
    }
}
