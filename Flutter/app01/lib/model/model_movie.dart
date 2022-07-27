class Movie {
  // 영화의 구성요소 - 영화 데이터를 쉽게 관리하기 위함
  final String title;
  final String keyword;
  final String poster;
  final bool like;

  Movie.fromMap(Map<String, dynamic> map)
    : title = map['title'],
      keyword = map['keyword'],
      poster = map['poster'],
      like = map['like'];

  @override
  String toString() => "Movie<$title:$keyword>";
}