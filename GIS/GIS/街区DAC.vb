Imports System.IO
Imports System.Text
Imports Dictionaries

Public Class 街区DAC

    Private Function F(ByVal field As String) As String
        If field(0) = """" Then field = field.Substring(1)
        Dim last As Integer = field.Length - 1
        If field(last) = """" Then field = field.Substring(0, last)
        Return field
    End Function

    Public Function Read(ByVal ParamArray files As String()) As List(Of 街区)
        Dim list As New List(Of 街区)
        For Each file As String In files
            Using reader As New StreamReader(file, Encoding.GetEncoding("Shift_JIS"))
                While True
                    Dim line As String = reader.ReadLine
                    If line Is Nothing Then Exit While
                    Dim fields As String() = line.Split(","c)
                    If F(fields(0)) = "都道府県名" Then Continue While
                    Dim g As New 街区
                    g.都道府県名 = F(fields(0))
                    g.市区町村名 = F(fields(1))
                    g.大字・町丁目名 = F(fields(2))
                    g.街区符号・地番 = F(fields(3))
                    g.座標系番号 = Integer.Parse(F(fields(4)))
                    g.Ｘ座標 = Double.Parse(F(fields(5)))
                    g.Ｙ座標 = Double.Parse(F(fields(6)))
                    g.緯度 = Double.Parse(F(fields(7)))
                    g.経度 = Double.Parse(F(fields(8)))
                    g.住居表示フラグ = Integer.Parse(F(fields(9)))
                    g.代表フラグ = Integer.Parse(F(fields(10)))
                    g.更新前履歴フラグ = Integer.Parse(F(fields(11)))
                    g.更新後履歴フラグ = Integer.Parse(F(fields(12)))
                    list.Add(g)
                End While
            End Using
        Next
        Return list
    End Function

    Public Function ReadDict(ByVal 置換辞書 As String, ByVal ParamArray files As String()) As WordDictionary(Of 街区Word)
        Dim list As List(Of 街区) = Read(files)
        Dim dict As New NormalizedDictionary(Of 街区Word)(New TrieDictionary(Of 街区Word)(New 街区Word.Acc))
        Dim reader As New CsvReader(置換辞書)
        Try
            dict.AddReplacement(reader)
        Finally
            reader.Close()
        End Try
        For Each g As 街区 In list
            If g.代表フラグ = 1 Then
                dict.Add(New 街区Word(g, g.都道府県名 & g.市区町村名 & g.大字・町丁目名 & g.街区符号・地番, "0"))
                dict.Add(New 街区Word(g, g.市区町村名 & g.大字・町丁目名 & g.街区符号・地番, "1"))
            End If
        Next
        Return dict
    End Function

End Class
