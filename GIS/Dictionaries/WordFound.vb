''' <summary>
''' WordDictionaryで検索した結果の用語を格納するためのクラスです。
''' </summary>
''' <typeparam name="T">用語を格納するオブジェクトの型を指定します。</typeparam>
Public Class WordFound(Of T)

    Private _word As T

    ''' <summary>
    ''' 見つかった用語を取得します。
    ''' </summary>
    Public ReadOnly Property Word() As T
        Get
            Return _word
        End Get
    End Property

    Private _position As Integer

    ''' <summary>
    ''' 見つかった用語の位置を取得します。
    ''' </summary>
    Public ReadOnly Property Position() As Integer
        Get
            Return _position
        End Get
    End Property

    Private _length As Integer

    ''' <summary>
    ''' 見つかった用語の長さを取得します。
    ''' </summary>
    Public ReadOnly Property Length() As Integer
        Get
            Return _length
        End Get
    End Property

    Public Sub New(ByVal word As T, ByVal position As Integer, ByVal length As Integer)
        Me._word = word
        Me._position = position
        Me._length = length
    End Sub

    Class CompPositionLengthDesc
        Implements IComparer(Of WordFound(Of T))

        Public Function Compare(ByVal x As WordFound(Of T), ByVal y As WordFound(Of T)) As Integer _
                Implements IComparer(Of WordFound(Of T)).Compare
            Dim c As Integer
            c = x._position - y._position   ' Positionの昇順
            If c <> 0 Then Return c
            Return y._length - x._length    ' Lengthの降順
        End Function

    End Class

    Public Shared ReadOnly COMP_POSITION_LENGTH_DESC As New CompPositionLengthDesc

End Class
