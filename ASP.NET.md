### ASP.NET

------

- Microsoft의 웹 개발 Framework. 초기에는 주로 WebForms를 사용하였으나 MVC가 나온 이후 MVC를 주로 사용



### MVC(Model - View - Controller)

------

- Model
  - 데이터 엑세스, 비지니스 로직, 유틸리티 클래스등을 가짐
- Controller
  - 사용자 입력/요청을 받아들여 Model에서 필요한 데이터를 가져와 View를 생성하여 결과를 돌려줌
  - 사용자의 웹 Request를 받아들여 처리하는 첫 관문. 웹 Request에서 필요한 파라미터나 POST 데이터를 사용하여 Models에서 데이터를 가져오거나 저장 가능, 필요한 결과를 사용자에게 보내는 역할.
  - 일반적으로 HTML 결과를 리턴. 데이터를 View에 전달하여 렌더링된 HTML을 생성
- View
  - 데이터를 HTML태그와 결합하여 표현하는 역할



