''' <summary>
''' 位置参照情報を格納するクラスです。
''' 詳細は以下を参照してください。
''' 
''' http://nlftp.mlit.go.jp/isj/index.html
''' </summary>
Public Class 街区

    Private _都道府県名 As String  ' 例：東京都
    Private _市区町村名 As String  ' 例：千代田区
    Private _大字・町丁目名 As String    ' 例：霞が関二丁目
    Private _街区符号・地番 As String    ' 例：1
    Private _座標系番号 As Integer  ' 平面直角座標系の座標系番号（1～19）例：9 詳細は http://www.gsi.go.jp/LAW/heimencho.html を参照してください。
    Private _Ｘ座標 As Double    ' 平面直角座標系の座標系原点からの距離メートル単位（小数第1位まで）（北方向プラス）例：-35925.9
    Private _Ｙ座標 As Double    ' 平面直角座標系の座標系原点からの距離メートル単位（小数第1位まで）（東方向プラス）例：-7446.2
    Private _緯度 As Double ' 十進経緯度（少数第6位まで）例：35.676154
    Private _経度 As Double ' 十進経緯度（少数第6位まで）例：139.751074
    Private _住居表示フラグ As Integer    ' 1:住居表示実施、0:住居表示未実施例：1
    Private _代表フラグ As Integer  ' 1:代表する、0:代表しない例:1
    Private _更新前履歴フラグ As Integer   ' 1：新規作成、2：名称変更、3：削除、0：変更なし
    Private _更新後履歴フラグ As Integer   ' 1：新規作成、2：名称変更、3：削除、0：変更なし

    Public Function 平面直角座標系距離(ByVal g As 街区) As Double
        If g Is Nothing Then Return Double.NaN
        If g.座標系番号 <> 座標系番号 Then Return Double.NaN
        Dim x As Double = g.Ｘ座標 - Ｘ座標
        Dim y As Double = g.Ｙ座標 - Ｙ座標
        Return Math.Sqrt(x * x + y * y)
    End Function

    Public Const BESSEL_A As Double = 6377397.155
    Public Const BESSEL_E2 As Double = 0.00667436061028297
    Public Const BESSEL_MNUM As Double = 6334832.10663254

    Public Const GRS80_A As Double = 6378137.0
    Public Const GRS80_E2 As Double = 0.00669438002301188
    Public Const GRS80_MNUM As Double = 6335439.32708317

    Public Const WGS84_A As Double = 6378137.0
    Public Const WGS84_E2 As Double = 0.00669437999019758
    Public Const WGS84_MNUM As Double = 6335439.32729246

    Public Const BESSEL As Integer = 0
    Public Const GRS80 As Integer = 1
    Public Const WGS84 As Integer = 2

    Private Shared Function deg2rad(ByVal deg As Double) As Double
        Return deg * Math.PI / 180.0
    End Function

    Private Shared Function calcDistHubeny(ByVal lat1 As Double, ByVal lng1 As Double, ByVal lat2 As Double, ByVal lng2 As Double, ByVal a As Double, ByVal e2 As Double, ByVal mnum As Double) As Double
        Dim my As Double = deg2rad((lat1 + lat2) / 2.0)
        Dim dy As Double = deg2rad(lat1 - lat2)
        Dim dx As Double = deg2rad(lng1 - lng2)
        Dim sin As Double = Math.Sin(my)
        Dim w As Double = Math.Sqrt(1.0 - e2 * sin * sin)
        Dim m As Double = mnum / (w * w * w)
        Dim n As Double = a / w
        Dim dym As Double = dy * m
        Dim dxncos As Double = dx * n * Math.Cos(my)
        Return Math.Sqrt(dym * dym + dxncos * dxncos)
    End Function

    ' 緯度および経度の対から距離を計算します。
    ' 単位はメートルです。
    ' 以下のページを参考にしています。
    '     ' 
    ' http://yamadarake.web.fc2.com/trdi/2009/report000001.html
    ' 
    ' @param lat1 地点１の緯度
    ' @param lng1 地点１の経度
    ' @param lat2 地点２の緯度
    ' @param lng2 地点２の経度
    ' @return
    Private Shared Function calcDistHubeny(ByVal lat1 As Double, ByVal lng1 As Double, ByVal lat2 As Double, ByVal lng2 As Double) As Double
        Return calcDistHubeny(lat1, lng1, lat2, lng2, GRS80_A, GRS80_E2, GRS80_MNUM)
    End Function

    Private Shared Function calcDistHubeny(ByVal lat1 As Double, ByVal lng1 As Double, ByVal lat2 As Double, ByVal lng2 As Double, ByVal type As Integer) As Double
        Select Case type
            Case BESSEL
                Return calcDistHubeny(lat1, lng1, lat2, lng2, BESSEL_A, BESSEL_E2, BESSEL_MNUM)
            Case WGS84
                Return calcDistHubeny(lat1, lng1, lat2, lng2, WGS84_A, WGS84_E2, WGS84_MNUM)
            Case Else
                Return calcDistHubeny(lat1, lng1, lat2, lng2, GRS80_A, GRS80_E2, GRS80_MNUM)
        End Select
    End Function

    Public Function 緯度経度距離(ByVal g As 街区) As Double
        If g Is Nothing Then Return Double.NaN
        ' 世界測地系で距離を求めます。
        Return calcDistHubeny(g.緯度, g.経度, 緯度, 経度, GRS80)
    End Function

    Public ReadOnly Property 住所() As String
        Get
            Return _都道府県名 & _市区町村名 & _大字・町丁目名 & _街区符号・地番
        End Get
    End Property

    Public Property 都道府県名() As String
        Get
            Return _都道府県名
        End Get
        Set(ByVal value As String)
            _都道府県名 = value
        End Set
    End Property

    Public Property 市区町村名() As String
        Get
            Return _市区町村名
        End Get
        Set(ByVal value As String)
            _市区町村名 = value
        End Set
    End Property

    Public Property 大字・町丁目名() As String
        Get
            Return _大字・町丁目名
        End Get
        Set(ByVal value As String)
            _大字・町丁目名 = value
        End Set
    End Property

    Public Property 街区符号・地番() As String
        Get
            Return _街区符号・地番
        End Get
        Set(ByVal value As String)
            _街区符号・地番 = value
        End Set
    End Property

    Public Property 座標系番号() As Integer
        Get
            Return _座標系番号
        End Get
        Set(ByVal value As Integer)
            _座標系番号 = value
        End Set
    End Property

    Public Property Ｘ座標() As Double
        Get
            Return _Ｘ座標
        End Get
        Set(ByVal value As Double)
            _Ｘ座標 = value
        End Set
    End Property

    Public Property Ｙ座標() As Double
        Get
            Return _Ｙ座標
        End Get
        Set(ByVal value As Double)
            _Ｙ座標 = value
        End Set
    End Property

    Public Property 緯度() As Double
        Get
            Return _緯度
        End Get
        Set(ByVal value As Double)
            _緯度 = value
        End Set
    End Property

    Public Property 経度() As Double
        Get
            Return _経度
        End Get
        Set(ByVal value As Double)
            _経度 = value
        End Set
    End Property

    Public Property 住居表示フラグ() As Integer
        Get
            Return _住居表示フラグ
        End Get
        Set(ByVal value As Integer)
            _住居表示フラグ = value
        End Set
    End Property

    Public Property 代表フラグ() As Integer
        Get
            Return _代表フラグ
        End Get
        Set(ByVal value As Integer)
            _代表フラグ = value
        End Set
    End Property

    Public Property 更新前履歴フラグ() As Integer
        Get
            Return _更新前履歴フラグ
        End Get
        Set(ByVal value As Integer)
            _更新前履歴フラグ = value
        End Set
    End Property

    Public Property 更新後履歴フラグ() As Integer
        Get
            Return _更新後履歴フラグ
        End Get
        Set(ByVal value As Integer)
            _更新後履歴フラグ = value
        End Set
    End Property


End Class
