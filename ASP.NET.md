### ASP.NET

------

- Microsoft의 웹 개발 Framework. 초기에는 주로 WebForms를 사용하였으나 MVC가 나온 이후 MVC를 주로 사용



### MVC(Model - View - Controller)

------

- 폴더

  - Model, View, Controller는 MVC Framework에서 지정한 폴더 밑에 생성해야 한다.

- Controller

  - 사용자 입력/요청을 받아들여 Model에서 필요한 데이터를 가져와 View를 생성하여 결과를 돌려줌

  - 사용자의 웹 Request를 받아들여 처리하는 첫 관문. 웹 Request에서 필요한 파라미터나 POST 데이터를 사용하여 Models에서 데이터를 가져오거나 저장 가능, 필요한 결과를 사용자에게 보내는 역할.

  - 일반적으로 HTML 결과를 리턴. 데이터를 View에 전달하여 렌더링된 HTML을 생성

  - 사용자가 생성한 컨트롤러는 System.Web.Mvc.Controller 클래스로부터 상속되어 Framework에서 제공하는 다양한 기능들을 사용

  - 클래스 안의 각 메서드는 하나의 웹 페이지에 해당, 각 웹 페이지는 /{컨트롤러명}/{메서드명}으로 접근

    - { ~ }Controller에서 Controller를 빼고 사용
    - 메서드명은 ActionResult(혹은 그 파생클래스)를 리턴하는 public 메서드에 대한 이름

  - MVC는 이와 같이 개발자가 일일이 모든 것을 Configuration하게 하지 않고 프레임워크가 정한 관례를 그냥 따르도록 하고 있는데, 이를 "Convention over Configuration"이라 한다.

  - Action 메서드

    - 외부 요청에 반응하여 결과를 리턴하는 메서드

    - 웹 Request를 받아들여 어떤 처리를 한 후 출력물인 ActionResult 객체를 리턴

    - Controller에서 View로 데이터를 전달하기 위해서는 **ViewBag**, **ViewData** 혹은 **Model객체**를 넘기는 방법을 사용(ViewBag은 dynamic 타입이므로 임의의 속성을 생성해서 지정 가능)

    - HTML을 리턴하는 것 이외에 파일, 문자열, JSON, 자바스크립트 등을 리턴 가능, 다른 URL로 리디렉트 가능

    - ActionResult 클래스는 실제 추상클래스로서 아래와 같은 파생 클래스들을 같는다.

      | **ActionResult 파생클래스**                         | **설명**                        | **Controller상의 메서드**    |
      | :-------------------------------------------------- | :------------------------------ | :--------------------------- |
      | ViewResult PartialViewResult                        | HTML을 리턴                     | View() PartialView()         |
      | EmptyResult                                         | 빈 결과                         |                              |
      | ContentResult                                       | 문자열을 리턴                   | Content()                    |
      | FileContentResult, FilePathResult, FileStreamResult | 파일을 리턴                     | File()                       |
      | JavaScriptResult                                    | 자바스크립트 리턴               | JavaScript()                 |
      | JsonResult                                          | JSON 리턴                       | Json()                       |
      | RedirectResult RedirectToRouteResult                | 새 URL 혹은 Action으로 Redirect | Redirect() RedirectToRoute() |
      | HttpUnauthorizedResult                              | HTTP 403 리턴                   |                              |

  - 파일 다운로드 및 업로드

    - 파일을 다운로드하기 위해서는 ActionResult 타입들 중 FileResult 객체를 리턴.
      1. 웹서버 상의 파일 절대 경로를 구한다
      2. 해당 파일을 바이트 배열로 읽는다
      3. Controller.File() 메서드의 첫 번째 파라미터에 전달
    - 웹 서버에서 파일을 업로드 받기 위해서는 기본적으로 HttpContext.REquest.Files의 내용을 서버 상에 저장하며 됨

- Model
  - 데이터 액세스, 비즈니스 로직, 유틸리티 클래스등을 가짐
  - 외부 데이터 소스를 액세스하기 위한 ADO.NET 클래스들이나 Entity Framework 클래스 등이 Model에 놓여진다.
  - /Models 폴더에 일반적으로 .cs 파일로 저장, /Models 안에 서브폴더를 만들어 클래스들을 그룹으로 묶을 수도 있음.
  - 모델 클래스는 Controller에 의해 사용

- View
  - 데이터를 HTML태그와 결합하여 표현하는 역할(View 파일을 랜더링하여 HTML 파일을 만들어 리턴)
  - 기본적으로 HTML, CSS로 UI를 구성하지만, Dynamic하게 HTML을 생성하기 위해  MVC에서 특별한 구문 및 MVC Helper 함수들을 제공
    - 특별한 구문은 View Engine을 통해 제공, MVC에서 가장 많이 쓰이는 View Engine은 Razor 엔진
    - 일반적으로 MVC View를 작성하기 위해서는 HTML과 Razor 문법을 함께 사용(MVC5에서는 Razor 뷰만 기본적으로 지원)
  - View 파일은 /Views/{컨트롤러명} 폴더에 놓이게 되는데, {컨트롤러명}은 Controller를 뺀 부분을 의미
    - View 파일의 이름은 Action 메서드의 이름과 동일해야함
  - View Layout
    - ASP.NET WebForms의 Master Page와 같이 기본 마스터 템플릿을 계층적으로 가질 수 있음. 이를 Layout이라 함.
    - 디폴트 레이아웃 파일은 _Layout.cshtml
    - 일반적으로 View에서 공통적으로 사용되는 파일은 /Views/Shared 폴더에 놓는다.
    - Layout 파일은 여러 계층을 가질 수 있음.
    - View에서 레이아웃을 별도로 지정하지 않으면, 디폴트 레이아웃인 /Views/Shared/_Layout.cshtml 을 사용
    - 다른 레이아웃을 지정하기 위해서는 View 안에 Layout 속성을 지정하면 된다.
  - Partial View
    - View의 특정 부분을 띄어내어 별도의 부분 View로 만들어 사용 가능
    - 여러 View안에 임의로 넣어서 사용 가능
  - Controller에서 View로 데이터 전달 방식
    - Controller의 View() 메서드의 파라미터로 Model 객체를 보내거나 ViewBag(dynamic) 혹은 ViewData(컬렉션)에 담아 데이터를 넘김
    - View에서 사용할 데이터 모델을 넘기기 위해서는 Controller.View(모델 객체) 메서드의 파라미터로 넘길 수 있다.
    - View는 이렇게 넘어온 모델 객체의 타입을 (아래 예제에서 같이) @model 과 함께 지정하고, View 본문에서 "Model.속성명" 과 같이 사용
    - ViewData는 Dictionary(해시테이블) 타입으로 Key에 Value를 지정해서 데이터를 전달
    - ViewBag은 dynamic 타입으로서 개발자 임의의 속성들을 지정해서 사용 가능
      - ViewBag은 내부적으로 ViewData 객체를 사용, 해시테이블 데이터를 dynamic 타입으로 보다 편리하게 제공하기 위한 Wrapper
  - View Razor
    - HTML 안에 C# (혹은 VB) 코드를 넣어 동적이 HTML을 생성할 수 있도록 함
    - 하나 이상의 C# 문장 혹은 문장 블럭을 표현하기 위해 @{...} 블럭을 사용.
    - 블럭 안의 C# 코드는 표준 C#에서와 마찬가지로 세미콜론으로 둔다.
    - 간단한 Inline 표현식은 @사인 뒹 ㅔ변수명 혹은 함수명을 쓴다.
    - 코멘트는 @* *@ 으로 둘러 싼다.
    - if 조건문은 @if 처럼 if 앞에만 @사인을 붙이면 되고, 블럭 안에 HTML 문을 그대로 사용하면 Razor 엔진이 알아서 렌더링 해줌.
    - switch 블럭도 처음 @switch 을 사용하고 해당 블럭을 C# 처럼 사용하고 중간에 HTML을 혼용해서 사용 가능
    - 반복문은 @for, @foreach, @while 처럼 앞에 @사인을 붙이고 블럭안에서 C#문과 HTML문을 혼용해서 사용하면 Razor 엔진이 알아서 렌더링.
  - @model 선언
    - Controller에서 데이타를 View에 전달하기 위해서는 ViewBag, ViewData, TempData을 사용하거나 Controller.View() 메서드에 모델 객체를 파라미터로 넘기는 방식을 사용. 모델 객체가 전달되었을 때, Razor에서는 "@model 모델타입" 과 같이 View 파일 상단에 어떤 모델 타입이 전달되었는지는 쓰게 됨. 이렇게 정의된 모델은 View 클래스의 "Model" 속성으로 해당 객체의 속성을 Strong Type 형태로 사용 가능.
    - View에서 클래스 타입을 지정할 때, 클래스 타입 앞에 네임스페이스를 함께 써야 함. 물론 모든 클래스 앞에 네임스페이스를 쓸 필요 없이 맨 앞에 @using 을 써서 미리 네임스페이스를 선언할 수도 있음.



### Entity Framework

------

- C#과 같은 객체 지향형 프로그래밍(OOP) 언어에서 DB를 쉽게 사용하기 위한 ORM(Object-Relational Mapping) 도구
- Entity Framework 모델
  - (1) Code First, (2) Model First, (3) Database First
  - Model First와 Database First 접근 모델은 Visual Studio의 Visual Model Designer (EDMX)를 통해 객체/테이블 매핑을 디자인하는 방식으로, Database First 접근 모델은 기존 DB로부터 테이블 구조들을 읽어 Visual Model을 구성하는 것, Model First는 기존 DB가 없을 때 직접 Visual Model Designer에 Entity 들을 하나씩 추가해 가면서 데이타 모델을 구성하는 방식, Model First / Database First 방식은 Visual Model Designer에 디자인한 것을 *.edmx 라는 파일에 저장
  - Code First 접근 방식은 Visual Model Designer / EDMX 를 사용하지 않고 데이타 모델을 C# 클래스로 직접 코딩하는 방식으로 향후 Entity Framework은 Code First 모델 만을 지원할 것이므로 (주: EFv6는 3 개 모두 지원하고 있음) 여기서는 Code First 방식 만을 설명
- Code First
  - C# 클래스로 테이블의 구조를 정의, 클래스의 속성을 테이블의 컬럼에 매핑
  - DB를 미리 설계하지 않고 C# 클래스들로 Domain Object들을 정의하고 프로그램 실행 시 DB가 없으면 자동으로 DB를 생성하는 방식(기업 환경에서는 DB팀이 별도로 있거나 정교한 DB 설계를 미리 하는 경우가 많음. 이런 경우 기존 DB 구조와 매핑하는 C# 클래스들을 정의하고 DB 생성 없이 사용)
