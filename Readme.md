# (C# 코딩)  File Compare Tool


## 개요
- C# 프로그래밍 학습
- 1줄 소개: 두 폴더 내의 파일 리스트를 가져와서 서로 비교하고, 파일 복사 기능을 구현하는 프로그램
- 사용한 플랫폼:
- C#, .NET Windows Forms, Visual Studio, GitHub
- 사용한 컨트롤:
- Label, TextBox, ListBox, Button
- 사용한 기술과 구현한 기능:
- Visual Studio를 이용하여 UI 디자인
- string 클래스를 이용한 사용자 입력 데이터 처리
- DateTime 클래스를 이용한 현재시간 정보 구하기


## 실행 화면 (과제1)

<img width="1149" height="807" alt="image" src="https://github.com/user-attachments/assets/d875c09d-b2fb-4bea-918d-8beb61234187" />

- 구현한 내용 (위 그림 참조)
-UI 구성 :
Label: "File Compare v1.0" 등 앱의 타이틀 표시
TextBox: 선택된 양쪽 폴더의 경로를 표시 (txtLeftDir, txtRightDir)
Button: 폴더 선택을 위한 버튼 2개 및 파일 이동용 버튼(>>> , <<<) 배치
ListView: 양쪽 폴더의 파일 목록을 보여주기 위한 리스트 뷰 2개 배치 (Details 뷰 설정)

-FolderBrowserDialog 활용 : '폴더선택' 버튼 클릭 시 윈도우 표준 폴더 선택창을 띄워 경로 획득

-기본 목록 표시 : 선택된 경로의 파일 이름, 크기, 수정일을 리스트뷰에 출력하는 기초 기능 구현



## 실행 화면 (과제2)

<img width="1152" height="803" alt="image" src="https://github.com/user-attachments/assets/d6c49f13-4795-487d-901d-18184b2b05b7" />

- 구현한 내용 (위 그림 참조)
-폴더 선택 및 비교 : FolderBrowserDialog를 통해 선택된 양쪽 폴더의 파일을 실시간으로 비교하여 리스트업

-색상 구분 표시 (OwnerDraw 기능 활용) 
동일 파일 : 양쪽 폴더에 이름과 수정 시간이 같은 파일은 검은색으로 표시
다른 파일 : 이름은 같으나 수정 시간이 더 최신인 파일은 빨간색(New), 오래된 파일은 **회색(Old)**으로 표시
단독 파일 : 상대방 폴더에 존재하지 않는 파일은 보라색으로 표시
이벤트 핸들링 : DrawItem, DrawSubItem 이벤트를 수동으로 연결하여 리스트뷰의 텍스트 색상을 동적으로 변경


## 실행 화면 (과제3)
## 실행 화면 (과제4)
