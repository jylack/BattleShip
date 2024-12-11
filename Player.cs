using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    enum Skills
    {
        start, end
    }

    

    internal class Player
    {
        string _name;
        Skills _skill;
        Ship[] _ships;
        Field _field;

        //새로시작하거나 처음 시작할때 플레이어 세팅 초기화.
        public void InitPlayer()
        {
            _field = new Field();
            _ships = new Ship[(int)ShipType.end];

            for (int i = 0; i < (int)ShipType.end; i++)
            {
                _ships[i] = new Ship((ShipType)i);
            }

            Console.Write("사용하실 닉네임을 정해주세요 : ");
            _name = Console.ReadLine();
        }

        //왠지 필요없을거같아 일단 주석해둠.
        ////초기에 배들 배열에 배들넣기.?
        //public void InitShip()
        //{

        //}

        //배들 위치배정
        public void PlaceShip()
        {

        }

        //랜덤으로 배들 놓기
        public void PlaceRandomShips()
        {

        }


    }
}
