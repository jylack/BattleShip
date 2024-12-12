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
        Field _field;
        ConsoleKeyInfo inputKey;

        //새로시작하거나 처음 시작할때 플레이어 세팅 초기화.
        public void InitPlayer()
        {
            _field = new Field();
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

        /* To-Do : 쉽 정보 테스트 용으로 세팅, 불필요시 삭제 필요*/
        public Ship[] TestShips
        {
            get;
            set;
        }

        //왠지 필요없을거같아 일단 주석해둠.
        ////초기에 배들 배열에 배들넣기.?
        //public void InitShip()
        //{

        //}

        //배들 위치배정
        public void PlaceShip()
        {
            int selectShipIndex = 0;

            Point curSur = new Point(0, 0);

            while (true)
            {
                switch (inputKey.Key)
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


                while (_ships.Length > selectShipIndex)
                {
                    _ships[selectShipIndex].SetPointByIndex(_ships[selectShipIndex].Size, new Point(curSur.PosX, curSur.PosY));

                }

            }
        }

        //랜덤으로 배들 놓기
        public void PlaceRandomShips()
        {

        }


    }
}
