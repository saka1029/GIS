''' <summary>
''' トライ木によるWordDictionaryの実装です。
''' （トライ木の詳細については「http://ja.wikipedia.org/wiki/トライ木」
''' を参照してください）
''' サブツリーの検索は二分探索で行います。
''' </summary>
Public Class TrieDictionary(Of T)
    Inherits WordDictionaryBase(Of T)

    ''' <summary>
    ''' トライ木のノードです。
    ''' </summary>
    ''' <typeparam name="W">格納する用語の型を指定します。</typeparam>
    Private Class Node(Of W)

        ' 用語の一部である文字を格納します。
        Friend Key As Char

        ' メモリを節約するため追加時点でリストを作成します。
        Friend Words As List(Of W) = Nothing

        ' メモリを節約するため追加時点でリストを作成します。
        Friend Children As List(Of Node(Of W)) = Nothing

        Sub New(ByVal key As Char)
            Me.Key = key
        End Sub

    End Class

    ''' <summary>
    ''' ルートノードです。
    ''' </summary>
    Private Root As Node(Of T) = New Node(Of T)(ChrW(0))

    ''' <summary>
    ''' 空の辞書を作成するコンストラクタです。
    ''' </summary>
    ''' <param name="WordAccessor">用語へのアクセサを指定します。</param>
    Public Sub New(ByVal WordAccessor As WordAccessor(Of T))
        MyBase.new(WordAccessor)
    End Sub

    ''' <summary>
    ''' 指定されたノードの子ノードの中から指定した文字のノードを検索します。
    ''' 検索は二分探索によって行います。
    ''' このため子ノードは文字の上昇順に並んでいる必要があります。
    ''' </summary>
    ''' <param name="node">検索する親ノードを指定します。</param>
    ''' <param name="ch">検索する文字を指定します。</param>
    ''' <returns>見つかった子ノードを返します。
    ''' 該当する子ノードが見つからなかった場合はNothingを返します。</returns>
    Private Overloads Function Find(ByVal node As Node(Of T), ByVal ch As Char) As Node(Of T)
        If node.Children Is Nothing Then Return Nothing
        Dim i As Integer = 0
        Dim j As Integer = node.Children.Count - 1
        While i <= j
            Dim m As Integer = (i + j) \ 2
            Dim e As Node(Of T) = node.Children(m)
            Dim ekey As Char = e.Key
            If ch = ekey Then
                Return e
            ElseIf ch < ekey Then
                j = m - 1
            Else
                i = m + 1
            End If
        End While
        Return Nothing
    End Function

    ''' <summary>
    ''' 指定されたノードに子ノードを追加します。
    ''' 子ノードが文字の上昇順に並ぶように追加します。
    ''' </summary>
    ''' <param name="node">追加先の親ノードを指定します。</param>
    ''' <param name="child">追加する子ノードを指定します。</param>
    Private Overloads Sub Add(ByVal node As Node(Of T), ByVal child As Node(Of T))
        If node.Children Is Nothing Then
            node.Children = New List(Of Node(Of T))
        End If
        For i As Integer = node.Children.Count - 1 To 0 Step -1
            Dim e As Node(Of T) = node.Children(i)
            If child.Key >= e.Key Then
                node.Children.Insert(i + 1, child)
                Return
            End If
        Next
        node.Children.Insert(0, child)
    End Sub

    ''' <summary>
    ''' ノードに用語を追加します。
    ''' </summary>
    ''' <param name="node">追加先のノードを指定します。</param>
    ''' <param name="word">追加する用語を指定します。</param>
    Private Overloads Sub Add(ByVal node As Node(Of T), ByVal word As T)
        If node.Words Is Nothing Then
            node.Words = New List(Of T)
        End If
        node.Words.Add(word)
    End Sub

    ''' <summary>
    ''' 用語を辞書に追加します。
    ''' </summary>
    ''' <param name="word">追加する用語を指定します。</param>
    Public Overrides Sub Add(ByVal word As T)
        Dim n As String = Name(word)
        If n Is Nothing OrElse n.Length < 1 Then
            Throw New ArgumentException("空文字列は用語として登録できません")
        End If
        Dim last As Node(Of T) = Root
        For i As Integer = 0 To n.Length - 1
            Dim ch As Char = n.Chars(i)
            Dim child As Node(Of T) = Find(last, ch)
            If child Is Nothing Then
                child = New Node(Of T)(ch)
                Add(last, child)
            End If
            last = child
        Next
        Add(last, word)
    End Sub

    ''' <summary>
    ''' 文字列中に出現するすべての用語を検索します。
    ''' </summary>
    ''' <param name="s">検索対象文字列を指定します。</param>
    ''' <param name="types">検索する用語の種類を指定します。</param>
    ''' <returns>検索した結果の用語と出現位置および長さをリストで返します。
    ''' １件も見つからなかった場合は空のリストを返します。</returns>
    ''' <remarks></remarks>
    Public Overrides Function Encode(ByVal s As String, _
                                     ByVal ParamArray types() As String) As List(Of WordFound(Of T))
        If s Is Nothing Then Throw New ArgumentException("s")
        Dim list As New List(Of WordFound(Of T))
        If s = "" Then Return list
        For pos As Integer = 0 To s.Length - 1
            Dim last As Node(Of T) = Root
            For i As Integer = pos To s.Length - 1
                Dim ch As Char = s.Chars(i)
                last = Find(last, ch)
                If last Is Nothing Then Exit For
                If Not last.Words Is Nothing Then
                    For Each word As T In last.Words
                        If InType(word, types) Then
                            list.Add(New WordFound(Of T)(word, pos, i - pos + 1))
                        End If
                    Next
                End If
            Next
        Next
        Return list
    End Function

    ''' <summary>
    ''' 指定した文字列と同じ用語を検索します。
    ''' </summary>
    ''' <param name="s">検索文字列を指定します。</param>
    ''' <param name="max">検索結果の最大件数を指定します。
    ''' ゼロ以下の値を指定した場合は全件検索します。</param>
    ''' <param name="types">検索する用語の種類を指定します。</param>
    ''' <returns>検索した結果の用語をリストで返します。
    ''' １件も見つからなかった場合は空のリストを返します。</returns>
    Public Overrides Function Find(ByVal s As String, ByVal max As Integer, _
                                   ByVal ParamArray types() As String) As List(Of T)
        If s Is Nothing Then Throw New ArgumentException("s")
        Dim list As New List(Of T)
        Dim last As Node(Of T) = Root
        For i As Integer = 0 To s.Length - 1
            last = Find(last, s.Chars(i))
            If last Is Nothing Then Return list
        Next
        If Not last.Words Is Nothing Then
            For Each word As T In last.Words
                If InType(word, types) Then
                    If max > 0 AndAlso list.Count >= max Then Exit For
                    list.Add(word)
                End If
            Next
        End If
        Return list
    End Function

    ''' <summary>
    ''' 指定したノードおよびそのすべての子孫ノードに格納された用語をリストに格納します。
    ''' </summary>
    ''' <param name="node">起点となるノードを指定します。</param>
    ''' <param name="list">結果を格納するリストを指定します。</param>
    ''' <param name="max">格納する最大件数を指定します。</param>
    ''' <param name="types">対象となる用語の種類を指定します。</param>
    Private Sub AllDescendant(ByVal node As Node(Of T), ByVal list As List(Of T), ByVal max As Integer, _
                              ByVal ParamArray types As String())
        If Not node.Words Is Nothing Then
            For Each word As T In node.Words
                If InType(word, types) Then
                    If max > 0 AndAlso list.Count >= max Then Return
                    list.Add(word)
                End If
            Next
        End If
        If Not node.Children Is Nothing Then
            For Each child As Node(Of T) In node.Children
                AllDescendant(child, list, max, types)
            Next
        End If
    End Sub

    ''' <summary>
    ''' 指定した文字列で始まる用語を検索します。
    ''' </summary>
    ''' <param name="s">検索文字列を指定します。</param>
    ''' <param name="max">検索結果の最大件数を指定します。
    ''' ゼロ以下の値を指定した場合は全件検索します。</param>
    ''' <param name="types">検索する用語の種類を指定します。</param>
    ''' <returns>検索した結果の用語をリストで返します。
    ''' １件も見つからなかった場合は空のリストを返します。</returns>
    Public Overrides Function FindStartsWith(ByVal s As String, ByVal max As Integer, _
                                             ByVal ParamArray types() As String) As List(Of T)
        If s Is Nothing Then Throw New ArgumentException("s")
        Dim list As New List(Of T)
        Dim last As Node(Of T) = Root
        For i As Integer = 0 To s.Length - 1
            last = Find(last, s.Chars(i))
            If last Is Nothing Then Return list
        Next
        AllDescendant(last, list, max, types)
        Return list
    End Function

    ''' <summary>
    ''' 辞書に格納されているすべての用語をリストとして返します。
    ''' </summary>
    Public Overrides Function ToList() As System.Collections.Generic.List(Of T)
        Dim result As New List(Of T)
        AllDescendant(Root, result, -1)
        Return result
    End Function

End Class
