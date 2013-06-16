Imports System.Text

''' <summary>
''' 用語および検索対象文字列に対して正規化を行うWordDictionaryの実装です。
''' このクラスは辞書のエンジン自身は持っておらず、正規化のみを行います。
''' 辞書のエンジンはコンストラクタの引数で与える必要があります。
''' </summary>
''' <typeparam name="T">用語の型を指定します。</typeparam>
Public Class NormalizedDictionary(Of T)
    Inherits WordDictionaryBase(Of T)

    ''' <summary>
    ''' 置換パターンを保持するための内部クラスです。
    ''' </summary>
    Public Class Rep

        Public Src As String
        Public Dst As String

        Public Sub New(ByVal src As String, ByVal dst As String)
            Me.Src = src
            Me.Dst = dst
        End Sub

        Public Overrides Function ToString() As String
            Return "Rep(" + Src + "->" + Dst + ")"
        End Function

    End Class

    ''' <summary>
    ''' 正規化を行うWordAccessorのラッパーです。
    ''' </summary>
    ''' <typeparam name="W">用語を格納するクラスを指定します。</typeparam>
    Private Class NormWordAccessor(Of W)
        Implements WordAccessor(Of W)

        Private _dict As NormalizedDictionary(Of W)
        Private _origin As WordAccessor(Of W)

        Public Sub New(ByVal dict As NormalizedDictionary(Of W), ByVal origin As WordAccessor(Of W))
            Me._dict = dict
            Me._origin = origin
        End Sub

        Public ReadOnly Property Name(ByVal word As W) As String Implements WordAccessor(Of W).Name
            Get
                Return _dict.Normalize(_origin.Name(word), Nothing)
            End Get
        End Property

        Public ReadOnly Property Type(ByVal word As W) As String Implements WordAccessor(Of W).Type
            Get
                Return _origin.Type(word)
            End Get
        End Property
    End Class

    Private reps As New Dictionary(Of Char, List(Of Rep))

    Private dict As WordDictionary(Of T)

    Private initialized As Boolean = False

    Public Sub New(ByVal dict As WordDictionary(Of T))
        MyBase.New(Nothing)
        Dim newAccessor As New NormWordAccessor(Of T)(Me, dict.WordAccessor)
        Me.dict = dict
        Me.dict.WordAccessor = newAccessor
        Me.WordAccessor = newAccessor
    End Sub

    ''' <summary>
    ''' 置換パターンの中に循環参照がないことを確認します。
    ''' 循環参照がある場合はInvalidOperationExceptionをスローします。
    ''' </summary>
    ''' <param name="r">置換パターンを指定します。</param>
    ''' <param name="history">置換の履歴を指定します。</param>
    Private Sub CheckCyclic(ByVal r As Rep, ByVal history As List(Of String))
        If history.Contains(r.Src) Then
            Dim sb As New StringBuilder
            For Each e As String In history
                sb.Append(e).Append("->")
            Next
            sb.Append(r.Src)
            Throw New InvalidOperationException(String.Format( _
                "置換パターン{0}は循環参照しています({1})", _
                r, sb.ToString))
        End If
    End Sub

    ''' <summary>
    ''' 置換辞書の多重置換関係を解決します。
    ''' 例えば置換パターン
    ''' Ａ→ＢおよびＢ→Ｃがあった場合、前者の置換パターンをＡ→Ｃに変更します。
    ''' また置換パターンの循環参照をチェックします。
    ''' 循環参照がある場合はInvalidOperationExceptionをスローします。
    ''' </summary>
    ''' <param name="rep"></param>
    ''' <param name="dst"></param>
    ''' <param name="history"></param>
    Private Sub Initialize(ByVal rep As Rep, ByVal dst As StringBuilder, ByVal history As List(Of String))
        dst.Length = 0
        dst.Append(rep.Dst)
        Dim changed As Boolean = False
        history.Clear()
        history.Add(rep.Src)
        Dim i As Integer = 0
        While i < dst.Length
            Dim repled As Boolean = False
            Dim found As List(Of Rep) = Nothing
            If reps.TryGetValue(dst.Chars(i), found) Then
                For Each r As Rep In found
                    If Not dst.ToString.Substring(i).StartsWith(r.Src) Then Continue For
                    CheckCyclic(r, history)
                    history.Add(r.Src)
                    dst.Remove(i, r.Src.Length)
                    dst.Insert(i, r.Dst)
                    changed = True
                    repled = True
                    Exit For
                Next
            End If
            If Not repled Then
                i += 1
                history.Clear()
                history.Add(rep.Src)
            End If
        End While
        If changed Then
            rep.Dst = dst.ToString
        End If
    End Sub

    Private Sub Initialize()
        If initialized Then Return
        initialized = True
        Dim dst As New StringBuilder
        Dim history As New List(Of String)
        For Each list As List(Of Rep) In reps.Values
            For Each rep As Rep In list
                Initialize(rep, dst, history)
            Next
        Next
    End Sub

    ''' <summary>
    ''' 置換パターンを登録します。
    ''' </summary>
    ''' <param name="src">置換前の文字列を指定します。１文字以上でなければなりません。</param>
    ''' <param name="dst">置換後の文字列を指定します。空文字列でもかまいません。</param>
    ''' <remarks></remarks>
    Public Sub AddReplacement(ByVal src As String, ByVal dst As String)
        initialized = False
        If src Is Nothing OrElse src.Length < 1 Then
            Throw New ArgumentException("src")
        End If
        If dst Is Nothing Then
            Throw New ArgumentException("dst")
        End If
        Dim rep As New Rep(src, dst)
        Dim key As Char = src.Chars(0)
        Dim list As List(Of Rep) = Nothing
        If Not reps.TryGetValue(key, list) Then
            list = New List(Of Rep)
            reps(key) = list
        End If
        For i As Integer = 0 To list.Count - 1
            If src.Length > list(i).Src.Length Then
                list.Insert(i, rep)
                Return
            End If
        Next
        list.Add(rep)
    End Sub

    ''' <summary>
    ''' 置換パターンをCSVReaderから読み込みます。
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <remarks></remarks>
    Public Sub AddReplacement(ByVal reader As CsvReader)
        While reader.Read
            If reader.Count < 2 Then Continue While
            If reader(0).Trim.StartsWith("#") Then Continue While
            AddReplacement(reader(0), reader(1))
        End While
    End Sub

    ''' <summary>
    ''' 置換前後の文字位置の対応表を更新します。
    ''' </summary>
    ''' <param name="positions">対応表を指定します。
    ''' この引数がNothingの場合は何もしません。
    ''' </param>
    Private Sub SetPos(ByVal positions As List(Of Integer), ByVal index As Integer, _
                       ByVal len As Integer, ByVal element As Integer)
        If positions Is Nothing Then Return
        ' index番目まで格納できるように対応表を拡張します。
        Dim size As Integer = index + len
        While positions.Count <= size
            positions.Add(-1)
        End While
        For i As Integer = index To size - 1
            positions(i) = element
        Next
    End Sub

    ''' <summary>
    ''' 文字列を置換します。
    ''' 引数positionsにNothing以外の値を指定すると
    ''' 置換前後の文字位置の対応表を格納します。
    ''' 置換後文字列における文字位置iに対応する正規化前文字列の位置がjの場合、
    ''' positions(i) == j
    ''' となります。
    ''' </summary>
    ''' <param name="s">
    ''' 正規化する文字列を指定します。
    ''' </param>
    ''' <param name="positions">
    ''' 正規化前後の位置対応表を格納する空のリストを指定します。
    ''' 位置対応表が必要ない場合はNothingを指定します。
    ''' positionsは必要に応じてサイズが拡張されます。
    ''' 呼出後positionsの長さは「 正規化後文字列.Length() + 1」となります。</param> 
    ''' <returns>
    ''' 正規化された文字列を返します。
    ''' </returns>
    Private Function Normalize(ByVal s As String, ByVal positions As List(Of Integer)) As String
        If s Is Nothing Then
            Throw New ArgumentException("s")
        End If
        Dim r As New StringBuilder
        Dim i As Integer = 0
        Dim size As Integer = s.Length
        While i < size
            Dim ch As Char = s.Chars(i)
            Dim list As List(Of Rep) = Nothing
            If reps.TryGetValue(ch, list) Then
                For Each e As Rep In list
                    If s.Substring(i).StartsWith(e.Src) Then
                        Dim len As Integer = e.Src.Length
                        SetPos(positions, r.Length, len, i)
                        r.Append(e.Dst)
                        i += len
                        GoTo L
                    End If
                Next
            End If
            SetPos(positions, r.Length, 1, i)
            r.Append(ch)
            i += 1
L:
        End While
        SetPos(positions, r.Length, 1, s.Length)
        'Console.WriteLine("置換前:{0}", s)
        'Console.WriteLine("置換後:{0}", r)
        Return r.ToString
    End Function

    ''' <summary>
    ''' 辞書に用語を追加します。
    ''' 置換辞書の初期化を行い、元となる辞書に処理を委譲します。
    ''' </summary>
    Public Overrides Sub Add(ByVal word As T)
        Initialize()
        dict.Add(word)
    End Sub

    ''' <summary>
    ''' 文字列をコード化します。
    ''' 置換辞書により文字列を置換後にコード化を行います。
    ''' コード化結果の文字位置は文字列置換前の位置を返します。
    ''' </summary>
    Public Overrides Function Encode(ByVal s As String, ByVal ParamArray types() As String) As System.Collections.Generic.List(Of WordFound(Of T))
        Initialize()
        Dim positions As New List(Of Integer)
        s = Normalize(s, positions)
        Dim founds As List(Of WordFound(Of T)) = dict.Encode(s, types)
        Dim r As New List(Of WordFound(Of T))
        For Each e As WordFound(Of T) In founds
            Dim start As Integer = positions(e.Position)
            Dim ends As Integer = positions(e.Position + e.Length)
            r.Add(New WordFound(Of T)(e.Word, start, ends - start))
        Next
        Return r
    End Function

    ''' <summary>
    ''' Findの実装です。
    ''' 元になる辞書に処理を委譲します。
    ''' </summary>
    Public Overrides Function Find(ByVal s As String, ByVal max As Integer, ByVal ParamArray types() As String) As System.Collections.Generic.List(Of T)
        Initialize()
        s = Normalize(s, Nothing)
        Return dict.Find(s, max, types)
    End Function

    ''' <summary>
    ''' FindStartsWithの実装です。
    ''' 元になる辞書に処理を委譲します。
    ''' </summary>
    Public Overrides Function FindStartsWith(ByVal s As String, ByVal max As Integer, ByVal ParamArray types() As String) As System.Collections.Generic.List(Of T)
        Initialize()
        s = Normalize(s, Nothing)
        Return dict.FindStartsWith(s, max, types)
    End Function

    ''' <summary>
    ''' ToListの実装です。
    ''' 元になる辞書に処理を委譲します。
    ''' </summary>
    Public Overrides Function ToList() As System.Collections.Generic.List(Of T)
        Initialize()
        Return dict.ToList
    End Function

    ''' <summary>
    ''' 置換辞書をリストに出力します。
    ''' </summary>
    Public Function ReplacementToList() As List(Of Rep)
        Dim r As New List(Of Rep)
        For Each list As List(Of Rep) In reps.Values
            For Each Rep As Rep In list
                r.Add(Rep)
            Next
        Next
        Return r
    End Function

End Class
