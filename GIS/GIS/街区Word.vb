Imports Dictionaries

Public Class 街区Word

    Public Class Acc
        Implements WordAccessor(Of 街区Word)

        Public ReadOnly Property Name(ByVal word As 街区Word) As String Implements Dictionaries.WordAccessor(Of 街区Word).Name
            Get
                Return word.Name
            End Get
        End Property

        Public ReadOnly Property Type(ByVal word As 街区Word) As String Implements Dictionaries.WordAccessor(Of 街区Word).Type
            Get
                Return word.Type
            End Get
        End Property
    End Class

    Private g As 街区
    Private n As String
    Private t As String

    Public ReadOnly Property 街区() As 街区
        Get
            Return g
        End Get
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return n
        End Get
    End Property

    Public ReadOnly Property Type() As String
        Get
            Return t
        End Get
    End Property

    Public Sub New(ByVal g As 街区, ByVal name As String, ByVal type As String)
        Me.g = g
        Me.n = name
        Me.t = type
    End Sub

End Class

