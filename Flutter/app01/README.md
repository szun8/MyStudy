# app01 - SingFlix
in 🐢 [Inflearn](https://www.inflearn.com/course/flutter-netflix-clone-app/dashboard) : Flutter + Firebase로 넷플릭스 UI 클론 코딩하기 [무작정 플러터]

---

<h3>session00. netflix UI 개발하기</h3>

- Stateless Widget vs Stateful Widget : 위젯 상호작용의 유무성
- Scaffold : 발판, 빈페이지 제공
- import 'package:flutter/material.dart';
- 이미지 적용 : pubspec.yaml에 `asset` 추가
- [className.fromMap(data structure)](https://pub.dev/documentation/dson/latest/dson/fromMap.html) : 생성자 생성 전에 데이터 구조에 맞게 먼저 초기화 하는 생성자 메서드   
📎 주로 DB에서 받아온 데이터를 미리 지정한 변수구조에 초기설정하는데 사용한다고 함
- [Carousel_Slider(ver ^4.1.1)](https://pub.dev/packages/carousel_slider) ➡️ version에 따라 `null safety` 지원 여부가 달라지므로 현 플러터 버전에 맞는 carousel slider를 pubspec에 적용시켜야 함
- [UI Layout](https://docs.flutter.dev/development/ui/layout) - Container속 widget들의 위치 정렬 (Widget Tree)    
    - Container속 하위 위젯 한 개 = `child` / 여러 개 = `children`
---
### Flutter 기능
- Flutter Inspector : <b>✔️simulator 실행 시</b>에 설정한 위젯들의 계층구조와 사이즈 뷰를 자세히 보여주는 기능
