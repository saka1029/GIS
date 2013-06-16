GIS
===

国土交通省が公開する位置参照情報を使用してふたつの住所の間の距離を求めます。

●ビルド
ビルドして実行するためにはMicrosoft Visual Studio 2008が必要です。

●ディレクトリ構成
GIS/Dictionaries       住所を検索するための辞書のプロジェクトです。
GIS/GIS                街区レベル位置参照情報を処理するためのプロジェクトです。
GIS/GISForms           GIS/DictionariesおよびGIS/GISを使用して対話的に距離を求めるためのプロジェクトです。
GIS/Data               このプログラムで使用するデータが格納されています。
GIS/Data/13_2011.csv   東京都の街区レベル位置参照情報です。
GIS/Data/norm.csv      住所を正規化するための置換辞書です。

●参考URL
国土交通省位置参照情報ダウンロードサービス： http://nlftp.mlit.go.jp/isj/index.html
二地点の緯度・経度からその距離を計算する： http://yamadarake.jp/trdi/report000001.html
Google Maps APIを使用して距離を計算するサイト： http://www.benricho.org/map_straightdistance/
平面直角座標系（平成十四年国土交通省告示第九号）： http://www.gsi.go.jp/LAW/heimencho.html
