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
            // SeongchulTest();
        }
        public static void SeongchulTest() 
        {
            Player player = new Player();
            Field myField = new Field(10);
            player.Field = myField;
            player.Name = "내이름";
            player.Ships = new Ship[0];
            Player cpu = new Player();
            Field cpuField = new Field(30);
            cpu.Field = cpuField;
            cpu.Ships = new Ship[0];
            cpu.Name = "알파고";
            
            // 비어있는 바다 출력
            player.Field.PrintField(player, cpu);
            
            //// 임의의 배 2개만 세팅
            //// Cpu 는 하나만...
            Ship carrier = new Ship(ShipType.Carrier);
            Ship destroyer = new Ship(ShipType.Destroyer);
            
            carrier.SetPointByIndex(0, new Point(0,0));
            carrier.SetPointByIndex(1, new Point(0,1));
            carrier.SetPointByIndex(2, new Point(0,2));
            carrier.SetPointByIndex(3, new Point(0,3));
            carrier.SetPointByIndex(4, new Point(0,4));

            destroyer.SetPointByIndex(0, new Point(7,5));
            destroyer.SetPointByIndex(1, new Point(8,5));
            player.Ships = new [] { carrier, destroyer };
            
            Ship battleShip = new Ship(ShipType.BattleShip);
            battleShip.SetPointByIndex(0, new Point(1,1));
            battleShip.SetPointByIndex(1, new Point(2,1));
            battleShip.SetPointByIndex(2, new Point(3,1));
            battleShip.SetPointByIndex(3, new Point(4,1));
            cpu.Ships = new[] { battleShip };
            
            // 처음 세팅후 출력
            player.Field.PrintField(player, cpu);

            // 피격시 출력 테스트
            player.Field.Sea[0,9] = true;
            player.Field.Sea[1,9] = true;

            player.Ships[0].Points[4].IsHit = true;
            
            cpu.Field.Sea[9,0] = true;
            cpu.Field.Sea[9,1] = true;

            cpu.Ships[0].Points[0].IsHit = true;
            cpu.Ships[0].Points[1].IsHit = true;
            
            player.Field.PrintField(player, cpu);        
            
            // player.Field.PrintField(player, cpu)가 너무 보기 안좋다..
            // 좀더 예쁘게 짜보려면..?
            // 1. Field.PrintField(player, cpu)
            // 2. player.Field.PrintFieldWithOpponent(cpu)
            // 3. player.PrintField(), cpu.PrintField() 한번씩 호출
            // 3번은 근데 콘솔에서 옆으로 출력하기가 어렵기 때문에 위아래로 보여주는거 말곤 힘들듯
        }        
    }
}
