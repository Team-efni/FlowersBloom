<a href = "https://github.com/Team-efni/FlowersBloom">
  <img src="https://github.com/Team-efni/FlowersBloom/assets/69100145/f86bb4a4-6c66-41a9-a980-0fbb3a25e4f8" align="right" height="90" />
</a>

# 꽃이 피는 날
> 빠른 판단과 순발력을 이용하여 일정 수의 몬스터를 쓰러뜨리는 스테이지형 캐주얼 모바일 게임

<img src="https://img.shields.io/badge/Unity-000000?style=flat&logo=unity&logoColor=white"/>  <img src="https://img.shields.io/badge/PlayerPrefs-007AAC?style=flat">  <img src="https://img.shields.io/badge/-C%23-512BD4?style=flat&logo=C%23&logoColor=white">


## 📢 프로젝트 소개
- 오른쪽으로 이동하며 마주치는 몬스터를, 노트를 이용해 쓰러뜨리는 모바일 게임
- 몬스터와 마주치면 상단의 노트가 등장하며 이를 순서에 맞게 터치하거나 홀드하여 물리치며 진행한다.
- 시간 내에 노트를 입력하지 못해 몬스터와 충돌하면 컷씬 화면이 등장한다.
- 컷씬 화면에서는 줄어드는 원에 맞추어 노트를 터치해야 한다.

<details>
<summary>
  
### 꽃이 피는 날 화면 보기
</summary>

* `타이틀 화면`
  
  <img src = "https://github.com/Team-efni/FlowersBloom/assets/69100145/8265ae7a-c6ed-4233-b1ad-f877563c85cf" width="450" height="200"/>
  <img src = "https://github.com/Team-efni/FlowersBloom/assets/69100145/7b740660-0766-4b11-9af3-1a97a22939f1" width="450" height="200"/>

* `캐릭터 선택 화면`

  <img src = "https://github.com/Team-efni/FlowersBloom/assets/69100145/1be1608c-b6df-41a8-b0bf-3e77245d9103" width="450" height="200"/>
  <img src = "https://github.com/Team-efni/FlowersBloom/assets/69100145/dacf71e8-4965-4dda-bf75-364f5ec5df2f" width="450" height="200"/>

* `월드 맵 선택 화면`

  <img src = "https://github.com/Team-efni/FlowersBloom/assets/69100145/162ddc07-6db6-4d0b-bc00-ebb147168664" width="450" height="200"/>
  <img src = "https://github.com/Team-efni/FlowersBloom/assets/69100145/fd496f6f-9cb0-4e2b-b0cc-cb9494c6b4f6" width="450" height="200"/>

* `맵 선택 화면`

  <img src = "https://github.com/Team-efni/FlowersBloom/assets/69100145/5359dcbb-e756-4dba-9954-1a479830512f" width="450" height="200"/>

  <img src = "https://github.com/Team-efni/FlowersBloom/assets/69100145/9aa4ac7f-26d8-4195-8ad5-13326a4db82a" width="450" height="200"/>


* `플레이 화면`

  <img src = "https://github.com/Team-efni/FlowersBloom/assets/69100145/f3bb71e1-f927-43af-905d-955a1072b657" width="450" height="200"/>
  <img src = "https://github.com/Team-efni/FlowersBloom/assets/69100145/4c026ce1-02e7-4dd8-9ea3-2b2c9a3c9552" width="450" height="200"/>

  

* `플레이 화면 - 스토리`

  <img src = "https://github.com/Team-efni/FlowersBloom/assets/69100145/469471bd-76cf-467b-81d2-4971cf1697f4" width="450" height="200"/>
  <img src = "https://github.com/Team-efni/FlowersBloom/assets/69100145/bfbb14dc-ecda-4588-9a6c-ac4be7d7cfdb" width="450" height="200"/>



* `컷씬 화면`
  
  <img src = "https://github.com/Team-efni/FlowersBloom/assets/69100145/2b9aad2f-3ba1-4cea-bebb-e9bb32904f4b" width="450" height="200"/>
  <img src = "https://github.com/Team-efni/FlowersBloom/assets/69100145/5c23e8f8-a61f-40e6-a1b7-7f8476d34c18" width="450" height="200"/>

  

* `게임 클리어 및 게임 오버 화면`

  <img src = "https://github.com/Team-efni/FlowersBloom/assets/69100145/6b5171fc-53e7-4b49-adb9-920984a18a3d" width="450" height="200"/>
  <img src = "https://github.com/Team-efni/FlowersBloom/assets/69100145/7d9c24f8-ba83-4aec-8299-65d3814881ff" width="450" height="200"/>
</details>

## 🛠 핵심 기능
### 메뉴 화면
- 선택한 캐릭터에 따라 배경과 UI 등 변화
- 화면 슬라이드를 통해 원하는 맵으로 이동
  
### 플레이 화면
- 2D 백그라운드 스크롤링을 통한 게임 진행
- 좌/우에 있는 4개의 노트를 터치 및 홀드하여 몬스터 제거
- 스토리 진행을 통해 새로운 맵과 난이도 개방

### 컷씬 화면
- 플레이 중 제한 시간 내 노트 입력에 실패할 때 진행되는 화면
- 원이 줄어드는 타이밍에 맞추어 노트를 터치
- 타이밍을 맞추지 못하거나 컷씬 노트 터치에 실패할 시 기회 포인트 차감


## 💡 서비스 구성도
<img src = "https://github.com/Team-efni/FlowersBloom/assets/69100145/e8bc2f51-3f9e-4962-b841-77542e403e38" height="400"/>

## 👩‍💻 Developer
- <b>기획</b> : <a href = "https://github.com/Siiieon">김시언</a>
- <b>그래픽</b> : 김도형, 이유리
- <b>프로그래밍</b> : <a href = "https://github.com/yehang218">김석희</a>, <a href = "https://github.com/TheHyperPay">김시훈</a>, <a href = "https://github.com/ksb3458">김수빈</a>
