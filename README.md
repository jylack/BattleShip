# BattleShip
 
![배틀쉽](battleShip.png)

# 변경사항!!!

## Ship.cs
* 배의 isSink 삭제
  * private DoSink로 변경, isHit 할때 호출해서 배 가라앉히고 침몰 텍스트 프린트. (한번만 호출)
* 배의 포인터 인덱스로 세팅하는 메서드 추가
  * SetPointByIndex 추가

---

## Player.cs 
* internal -> public 으로 변경



## 건의 / 제안
* 있나유..?

## 해야할것들
* 필드 만들기
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
