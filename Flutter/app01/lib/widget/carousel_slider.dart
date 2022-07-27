import 'package:carousel_slider/carousel_slider.dart';
import 'package:flutter/material.dart';
import '../model/model_movie.dart';

class CarouselImage extends StatefulWidget {
  final List<Movie> movies;
  CarouselImage({required this.movies});
  _CarouselImageState createState() => _CarouselImageState();
}
class _CarouselImageState extends State<CarouselImage>{
  List<Movie>? movies;
  List<Widget>? images;
  List<String>? keywords;
  List<bool>? likes;
  int _currentPage = 0;
  String? _currentKeyword;

  @override
  void initState() {
    super.initState();
    movies = widget.movies;
    images = movies?.map((m) => Image.asset('./images/' + m.poster)).toList();
    keywords = movies?.map((m) => m.keyword).toList();
    likes = movies?.map((m) => m.like).toList();
    _currentKeyword = keywords![0];
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      child: Column( // 세로
        children: <Widget>[
          Container( // Topbar 아래 생성할수있도록 패딩값 적용
            padding: EdgeInsets.all(20),
          ),
          CarouselSlider(
            items: images,
            options: CarouselOptions(
              onPageChanged: (index, reason){
              setState(() {
                _currentPage = index;
                _currentKeyword = keywords![_currentPage];
              });
            })
          ),
          Container(
            padding: EdgeInsets.fromLTRB(0, 10, 0, 3),
            child: Text(
              _currentKeyword!,
              style: TextStyle(fontSize: 11),
            ),
          ),
          Container(
            child: Row( // 가로
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: <Widget>[
                Container(
                  child: Column(
                    children: <Widget>[
                      likes![_currentPage]
                      ? IconButton(onPressed: () {}, icon: Icon(Icons.check)) // likes = true
                      : IconButton(onPressed: () {}, icon: Icon(Icons.add)),  // likes = false
                      Text('내가 찜한 콘텐츠', style: TextStyle(fontSize: 11),)
                ],),),
                Container(
                  padding: EdgeInsets.only(right:10),
                  child: TextButton(
                      style: ButtonStyle(
                          backgroundColor: MaterialStateProperty.all<Color>(Colors.white)
                      ),
                      onPressed: () { },
                      child: Row(
                        children: const <Widget>[
                          Icon(
                            Icons.play_arrow,
                            color: Colors.black,
                          ),
                          Padding(padding: EdgeInsets.all(3)),
                          Text('재생', style: TextStyle(color: Colors.black),
                          ),
                        ],
                      )
                  ),
                ),
                Container(
                  padding: EdgeInsets.only(right:10),
                  child: Column(
                    children: <Widget>[
                      IconButton(
                          onPressed: (){},
                          icon: Icon(Icons.info)
                      ),
                      Text('정보', style: TextStyle(fontSize: 11),
                      )
                    ],
                  ),
                ),
              ],
            ),
          ),
          Container(
            child: Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: makeIndicator(likes!, _currentPage),
            ),
          )
        ],
      ),
    );
  }
}

List<Widget> makeIndicator(List list, int _currentPage){
  List<Widget> results = [];
  // home에 존재하는 dummy data 수만큼 반복문 진행
  for(var i = 0; i < list.length; i++){
    results.add(Container(
      width: 8, height: 8,
      margin: EdgeInsets.symmetric(vertical: 10.0,horizontal: 2.0),
      decoration: BoxDecoration(
        shape: BoxShape.circle,
        color: _currentPage == i // 현재 보여지고 있는 포스터에 대한 indicator on
          ? Color.fromRGBO(255, 255, 255, 0.9)
          : Color.fromRGBO(255, 255, 255, 0.4)
      ),
    ));
  }

  return results;
}