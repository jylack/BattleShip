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



    public class Player
    {
        string _name;
        Skills _skill;
        Ship[] _ships;
        Field _myField;
        ConsoleKeyInfo inputKey;

        //새로시작하거나 처음 시작할때 플레이어 세팅 초기화.
        public void InitPlayer()
        {
            _myField = new Field();

            _ships = new Ship[(int)ShipType.end];

            for (int i = 0; i < (int)ShipType.end; i++)
            {
                _ships[i] = new Ship((ShipType)i);
            }

            //Console.Write("사용하실 닉네임을 정해주세요 : ");
            //_name = Console.ReadLine();
        }
        public Ship[] Ships
        {
            get { return _ships; }
            set { _ships = value; }
        }

        public Field Field
        {
            get { return _myField; }
            set { _myField = value; }            
        }

        public string Name
        {
            get { return _name; }
        }

        //배들 위치배정
        public void PlaceShip()
        {

            //_ships[selectShipIndex].SetPointByIndex(0, new Point(0, 0));
            //_ships[selectShipIndex].SetPointByIndex(1, new Point(0, 1));
            //_ships[selectShipIndex].SetPointByIndex(2, new Point(0, 2));
            //_ships[selectShipIndex].SetPointByIndex(3, new Point(0, 3));
            //_ships[selectShipIndex].SetPointByIndex(4, new Point(0, 4));

            //왼쪽위부터 일렬로 하나씩 배정. 나중에 선택해서 하기전에 좌표지정해준거임.
            for (int i = 0; i < _ships.Length; i++)
            {
                for (int j = 0; j < _ships[i].Size; j++)
                {
                    _ships[i].SetPointByIndex(j, new Point(j, i));
                }
            }

        }

        //랜덤으로 배들 놓기
        public void PlaceRandomShips()
        {

        }


    }
}
