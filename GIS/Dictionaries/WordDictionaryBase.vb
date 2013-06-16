''' <summary>
''' 用語辞書WordDictionaryの実装を容易にするためのベースクラスです。
''' </summary>
''' <typeparam name="T">用語を格納するオブジェクトの型を指定します。</typeparam>
Public MustInherit Class WordDictionaryBase(Of T)
    Implements WordDictionary(Of T)

    Private _wordAccessor As WordAccessor(Of T)

    ''' <summary>
    ''' この辞書が使用するWordAccessorを取得または設定します。
    ''' </summary>
    Public Property WordAccessor() As WordAccessor(Of T) Implements WordDictionary(Of T).WordAccessor
        Get
            Return _wordAccessor
        End Get
        Set(ByVal value As WordAccessor(Of T))
            _wordAccessor = value
        End Set
    End Property

    ''' <summary>
    ''' コンストラクタです。
    ''' </summary>
    ''' <param name="wordAccessor">用語にアクセスするためのオブジェクトを指定します。</param>
    Protected Sub New(ByVal wordAccessor As WordAccessor(Of T))
        Me._wordAccessor = wordAccessor
    End Sub

    ''' <summary>
    ''' 用語の種類を取得するためのヘルパー関数です。
    ''' </summary>
    Protected ReadOnly Property Type(ByVal word As T) As String
        Get
            Return _wordAccessor.Type(word)
        End Get
    End Property

    ''' <summary>
    ''' 用語の文字列を取得するためのヘルパー関数です。
    ''' </summary>
    Protected ReadOnly Property Name(ByVal word As T) As String
        Get
            Return _wordAccessor.Name(word)
        End Get
    End Property

    ''' <summary>
    ''' 用語の種類が一致するかどうかを調べます。
    ''' </summary>
    ''' <param name="word">調べる用語を指定します。</param>
    ''' <param name="types">比較する種類を指定します。</param>
    ''' <returns>typesの中に指定した用語の種類と一致するものがあればTrueを、
    ''' それ以外の場合はFalseを返します。
    ''' 用語の種類をひとつも指定しなかった場合はTrueを返します。</returns>
    Public Function InType(ByVal word As T, ByVal ParamArray types As String()) As Boolean
        If types.Length <= 0 Then Return True
        Dim t As String = Type(word)
        For Each e As String In types
            If t = e Then
                Return True
            End If
        Next
        Return False
    End Function

#Region "インタフェースにある以下のメソッドはサブクラスで実装する必要があります"

    Public MustOverride Sub Add(ByVal word As T) Implements WordDictionary(Of T).Add

    Public MustOverride Function Encode(ByVal s As String, ByVal ParamArray types() As String) As List(Of WordFound(Of T)) Implements WordDictionary(Of T).Encode

    Public MustOverride Function Find(ByVal s As String, ByVal max As Integer, ByVal ParamArray types() As String) As List(Of T) Implements WordDictionary(Of T).Find

    Public MustOverride Function FindStartsWith(ByVal s As String, ByVal max As Integer, ByVal ParamArray types() As String) As System.Collections.Generic.List(Of T) Implements WordDictionary(Of T).FindStartsWith

    Public MustOverride Function ToList() As List(Of T) Implements WordDictionary(Of T).ToList

#End Region

End Class
