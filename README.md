# BattleShip
 
![배틀쉽](battleShip.png)

## 클래스 다이어그램 지원하는지 확인
```mermaid
classDiagram

note for Player "플레이어와 필드/n배열 플레이어의 동작들 정의"
note for SelectInterface "출력 관련한\n유틸리티 클래스"
note for GameManager "게임 초기화\n및 게임 진행"
note for Field "필드, bool 2차원 배열\n필드의 동작 정의"
note for Ship "배, 배의 좌표값\n배의 피격 판정 등 동작 정의"

Player --o GameManager
Field --o Player
Ship --o Player
ShipType --o Ship
Point --o Ship

class SelectInterface{
Point InterfacePoint
int Width
int Height
ShipSelectingView()
PrintUnderField()
ClearField()
PrintUnderField()
}

class GameManager {
Player player
Player Cpu
InitGame()
UpdateGame()
EndGame()
}

class Player {
string name
Field myField
Ships [] myShips
PlaceShip()
FindShip()
PlaceRandomShips()
ShotMissile()
}


class Field {
bool[,] Sea
TakeMissle()
PrintField()
PrintShip()
PrintSea()
}

class Ship {
string name
int size
Point points
bool isAlive
ShipType type
}

class Point {
int a
int b
bool isHit
compareHit()
}

class ShipType{
    <<enumeration>>
    Carrier
    BattleShip
    Cruiser
    Submarine
    Destroyer
    End
}
```
# 코드 기능 정리

## Field.cs
### 멤버변수
```cs
private bool [,] _sea
public bool [,] Sea {get;}
```
### 메소드
```cs
/*
 * 사이즈보다 큰곳에 미사일을 쏜다면, false 반환
 * 이미 쏜곳에 미사일을 또 쐈다면 false 반환
 * 쏠수 있는 곳에 (Field의 값이 false) 쏜다면 true
 */        
public bool TakeMissile(int x, int y)
    
/* 필드 넘어섰는지 확인 */
public bool IsOverField(int x, int y)

/* 플레이어의 필드 출력하는 메서드, 1인용, 2인용*/ 
public static void PrintField(Player player)
public static void PrintField(Player player, Player cpu)

/* 실제 필드 출력 메서드*/
// needToSpace 인자값이 0이면, 왼쪽부터 아니면 쭉 그려줌
public void PrintField(Player somePlayer, int needToSpace, bool hideShip)

/* 내부 필드 그리기용 메서드들 */
private void PrintXIndex(Player player, int needToSpace, int height)
private void PrintYIndex(int loop, int needToSpace)
/* 해당 x,y 값에 플레이어 배가 있는지 확인 */
private bool IsShipOnTarget(Player somePlayer, int x, int y)
// 바다에 쐈을 경우 프린트
private void PrintSea(bool isSeaHit)
// 배의 상태 프린트
private void PrintShip(Player player, int x, int y, bool hideShip)
```

## Ship.cs
### 멤버변수
```cs
public enum ShipType
private Point[] _points;
private string _name;
private int _size;
private bool _isAlive;

// 프로퍼티
public ShipType Type {get;set;}
public string Name {get;set;}
public int Size {get;set;}
public Point[] Points {get;set;}
public bool IsAlive {get;set;}
```
### 메소드
``` csharp
// 내 배의 Point를 모두 검사하면서 맞았는지 확인
// 다맞았을때는 DoSink를 실행하면서 isAlive를 false로 바꿔줌 
public bool IsHit(Point point)

// Points는 Property로, 개별 포인트는 메서드로
public void SetPointByIndex(int index, Point point)

// x,y를 받아서 내 배의 좌표가 있는 인덱스를 반환 확인
public int FindPointByInt(int x, int y)

// 가라 앉았는지 출력해주는 판단. 콘솔로 가라 앉았습니다 출력, isAlive false 로 설정
private void DoSink()

// 1칸 짜리 배는 false로 반환, 0번째 지점과 1번째 지점의 X값이 같으면 가로형
public bool IsHorizontal()

// 1칸 짜리 배는 false로 반환, 0번째 지점과 1번째 지점의 Y값이 같으면 세로형
public bool IsVertical()
```
---
# 변경사항!!!

## Ship.cs
* 배의 isSink 삭제
  * private DoSink로 변경, isHit 할때 호출해서 배 가라앉히고 침몰 텍스트 프린트. (한번만 호출)
* 배의 포인터 인덱스로 세팅하는 메서드 추가
  * SetPointByIndex 추가
* FindPointByInt 추가
  * x,y 의 값을 받아서 포인터가 있는지 확인 -> bool 리턴에서 인덱스 리턴으로 변경
---
## Field.cs
* 필드 크기를 동일하게 -> 각각 다르게 설정할수 있도록 변경
  * 출력부 변경, 한줄씩 그려주는 방식에서 한 필드씩 그리는 방식으로 변경
* 필드 클리어 변경 -> Console.Clear가 윈 10에서 번쩍번쩍함
  * 스페이스로 덮어씌워서 클리어
  * 필드 아래 부분만 클리어 + 출력 하는것도 수정
---

## Player.cs 
* internal -> public 으로 변경

## 건의 / 제안
* 있나유..?
* 12/12
* Field 클래스 내부에서 PrintField 메서드가 배가 아에 없는 상황에서 터져 버립니다.
* 아무래도 저 조건문 부분 수정해야 할듯합니다. 일단 주석처리후 임의로 수정하겠습니다.
* 수정한 코드랑 이전 코드랑 어떻게 합칠지는 내일 상의해보고 싶습니다.

## 해야할것들
* ~~필드 만들기~~ 완료
  * 필드 프린트, 배모양 가로 세로인지 판별 메서드
  * 가로일때는 ◁ ◀ □ ■ ▶ ▷
  * 세로일때는 △ ▲ □ ■ ▼ ▽
  * 바다 표시는 ∼, 맞았으면 ♨ / 완성형으로 가자면..?
    * // 데모
  Console.WriteLine("∼ ◀ □ ■ ▶ ∼ ∼ ∼");
  Console.WriteLine("∼ △ ∼ ∼ ∼ ∼ ♨ ∼");
  Console.WriteLine("∼ □ ∼ ∼ ∼ ∼ ∼ ∼");
  Console.WriteLine("∼ □ ∼ ∼ ∼ ∼ ∼ ∼");
  Console.WriteLine("∼ ▼ ∼ ∼ ∼ ∼ ∼ ∼ ");
  * 이런 느낌적인 느낌으로..

### DLC로 업데이트
* 배를 상속으로 받아서 구현 
* 스킬 구현 -> 배에 넣을지? 플레이어에 넣을지?