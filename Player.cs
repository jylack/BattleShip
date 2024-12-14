using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
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

        int _selectShipIndex;
        
        public int SelectShipIndex
        {
            get { return _selectShipIndex; }
            set { _selectShipIndex = value; }
        }

        //새로시작하거나 처음 시작할때 플레이어 세팅 초기화.
        public void InitPlayer()
        {
            _myField = new Field();

            _ships = new Ship[(int)ShipType.end];

            for (int i = 0; i < (int)ShipType.end; i++)
            {
                _ships[i] = new Ship((ShipType)i);
            }

            //배들 기본 위치배정 왼쪽 위로 정렬
            PlaceShip();
        }
        public Ship[] Ships
        {
            get { return _ships; }
            set { _ships = value; }
        }

        /* 프로퍼티 아래 두개 불필요하면 나중에 삭제 */
        public Field Field
        {
            get { return _myField; }
            set { _myField = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /* 여기까지 */

        //배들 기본 위치배정
        public void PlaceShip()
        {

            //왼쪽위부터 일렬로 하나씩 배정. 나중에 선택해서 하기전에 좌표지정해준거임.
            for (int i = 0; i < _ships.Length; i++)
            {
                for (int j = 0; j < _ships[i].Size; j++)
                {
                    _ships[i].SetPointByIndex(j, new Point(j, i));
                }
            }

        }

        //전체멥에서 해당위치에 배가있슴?
        public bool FindShip(int x, int y)
        {
            //모든 배의 블럭갯수
            int maxShipBlock = 0;

            for (int i = 0; i < _ships.Length; i++)
            {
                for (int j = 0; j < _ships[i].Size; j++)
                {
                    maxShipBlock++;
                }
            }

            //모든 배의 좌표값 모음
            Point[] allPoints = new Point[maxShipBlock];


            int index = 0;
            for (int i = 0; i < _ships.Length; i++)
            {
                for (int j = 0; j < _ships[i].Size; j++)
                {
                    allPoints[index] = _ships[i].Points[j];
                    index++;
                }
            }
            Point temp = new Point(x, y);

            for (int i = 0; i < maxShipBlock; i++)
            {
                //모든 좌표중에 현재 들어온 값이 있는가? 겹치는게 있으면 true
                if (allPoints[i].PosX == temp.PosX &&
                    allPoints[i].PosY == temp.PosY)
                {
                    return true;
                }
            }

            //겹치는게 없다!
            return false;
        }

        //랜덤으로 배들 놓기
        public void PlaceRandomShips()
        {

            //PlaceShip();

            Random rnd = new Random();

            Random rndPointX = new Random();
            Random rndPointY = new Random();


            for (int i = 0; i < _ships.Length; i++)
            {
                //random.Next(2): 0에서 1까지의 정수 중 하나를 무작위로 생성합니다.
                //Next(2)는 0과 1 중 하나를 반환합니다.
                //random.Next(2) == 0; 무작위로 생성된 값이 0인지 확인하여, 0이면 true, 1이면 false를 반환합니다.
                //true 면 가로 flase이면 세로
                _ships[i].isHorizontal = rnd.Next(2) == 0;
                
                //현재 선택된 배 크기만큼 좌표배열 생성
                Point[] point = new Point[Ships[i].Size];

                
                if (_ships[i].isHorizontal)// 현재 배는 가로
                {
                    
                    point[0].PosX = rndPointX.Next(_myField.Sea.GetLength(0));
                    point[0].PosY = rndPointY.Next(_myField.Sea.GetLength(1) - _ships[i].Size); //필드 최대길이에서 배 사이즈만큼 이미 뻈음.

                    if (FindShip(point[0].PosX, point[0].PosY) == false) // 지정된 좌표에 배가 없다면
                    {
                        for (int j = 1; j < _ships[i].Size; j++)
                        {
                            point[j].PosX = point[0].PosX;
                            point[j].PosY = point[0].PosY+j;
                        }
                    }

                }
                else //세로
                {
                    point[0].PosX = rndPointX.Next(_myField.Sea.GetLength(0) - _ships[i].Size);//필드 최대길이에서 배 사이즈만큼 이미 뻈음.
                    point[0].PosY = rndPointY.Next(_myField.Sea.GetLength(1) );

                    if (FindShip(point[0].PosX, point[0].PosY) == false) // 지정된 좌표에 배가 없다면
                    {
                        for (int j = 1; j < _ships[i].Size; j++)
                        {
                            point[j].PosX = point[0].PosX + j;
                            point[j].PosY = point[0].PosY;
                        }
                    }
                }

                int count = 0;
                int index = 0;

                //현재 배 사이즈 만큼 어디 들갈수있나 체킹
                while (index < point.Length) 
                {
                    //세번다 배가 없으면 고고
                    if(FindShip(point[index].PosX,point[index].PosY) == false)
                    {
                        count++;
                    }
                    index++;
                }

                //현재 배가없는 좌표탐색횟수가 배사이즈만큼 완료 했을때.
                //좌표 지정
                if (count == _ships[i].Size)
                {
                    _ships[i].Points = point;
                }


            }
        }


    }
}
