import 'package:app01/screen/home_screen.dart';
import 'package:app01/widget/bottom_bar.dart';
import 'package:flutter/material.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatefulWidget {
  _MyAppState createState() => _MyAppState();
}

class _MyAppState extends State<MyApp> {
  TabController? controller;

  @override
  Widget build(BuildContext context){
    return MaterialApp(
      title: 'SingFlix',
      theme: ThemeData(
        brightness: Brightness.dark,
        primaryColor: Colors.black,
        accentColor: Colors.white
      ),
      home: DefaultTabController(
        length: 4,
        child: Scaffold(
          body: TabBarView(
            physics: NeverScrollableScrollPhysics(),
            children: <Widget>[
              // 외부에 있는 dart 파일 import
              HomeScreen(),
              Container(child: Center(child: Text('search'),),),
              Container(child: Center(child: Text('save'),),),
              Container(child: Center(child: Text('more'),),)
            ],
          ),
          bottomNavigationBar: Bottom(),
        ),
      ),
    );
  }
}