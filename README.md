GIS
===

国土交通省が公開する位置参照情報を使用してふたつの住所の間の距離を求めます。<br>
このプログラムは『街区レベル位置参照情報　国土交通省』を使用して作成しています。<br>
<br>
●ビルド<br>
ビルドして実行するためにはMicrosoft Visual Studio 2008が必要です。<br>
<br>
●ディレクトリ構成<br>
GIS/GIS.sln            Visual Studioのソリューションファイルです。<br>
GIS/Dictionaries       住所を検索するための辞書のプロジェクトです。<br>
GIS/GIS                街区レベル位置参照情報を処理するためのプロジェクトです。<br>
GIS/GISForms           GIS/DictionariesおよびGIS/GISを使用して対話的に距離を求めるためのサンプルプロジェクトです。<br>
GIS/Data               このプログラムで使用するデータが格納されています。<br>
GIS/Data/13_2011.csv   東京都の街区レベル位置参照情報です。<br>
GIS/Data/norm.csv      住所を正規化するための置換辞書です。<br>
<br>
●サンプルプログラムの説明<br>
ビルドして実行するとフォームが表示されます。<br>
住所Aおよび住所Bにそれぞれ住所を入力するとその間の距離が表示されます。<br>
東京都の住所のみ処理可能です。<br>
平面直角座標系による距離と緯度経度による距離が表示されます。<br>
前者は「平面直角座標系の座標系番号（1～19）」が同じ住所間の距離のみ求めることができます。<br>
（「平面直角座標系」については国土交通省のホームページに説明があります）<br>
このため同じ東京都でも神田錦町と小笠原村父島の間の距離を求めることはできません。<br>
緯度経度による距離にはこの制限はありません。<br>
●精度の評価<br>
Google Maps APIを使用して距離を計算するサイトでサンプルプログラムと同様のことができるので比較することができます。<br>
<br>
●処理可能住所の拡張と制限<br>
国土交通省位置参照情報ダウンロードサービスで他府県のデータをダウンロードし、GIS/Dataディレクトリに格納します。<br>
GIS/GISForms/app.configにダウンロードしたファイル名を追加します。<br>
例) &lt;add key="街区CSVファイル" value="..\\..\Data\13_2011.csv,..\\..\Data\14_2011.csv" /&gt;<br>
ただしこのデータ自体が都市計画区域内に限られているため、それ以外の住所については処理できません。<br>
Wikipediaによれば<br>
「都市計画区域は、国土の25.7%を占めているに過ぎないが、91.6%の人が住んでいる。」<br>
とのことです。<br>
<br>
●参考URL<br>
国土交通省位置参照情報ダウンロードサービス： http://nlftp.mlit.go.jp/isj/index.html<br>
二地点の緯度・経度からその距離を計算する： http://yamadarake.jp/trdi/report000001.html<br>
Google Maps APIを使用して距離を計算するサイト： http://www.benricho.org/map_straightdistance/<br>
平面直角座標系（平成十四年国土交通省告示第九号）： http://www.gsi.go.jp/LAW/heimencho.html<br>
Wikipedia都市計画区域： http://ja.wikipedia.org/wiki/%E9%83%BD%E5%B8%82%E8%A8%88%E7%94%BB%E5%8C%BA%E5%9F%9F
