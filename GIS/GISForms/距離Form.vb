Imports System.Configuration.ConfigurationManager
Imports Dictionaries
Imports GIS

Public Class 距離Form

    Private dict As WordDictionary(Of 街区Word)

    Private Sub Log(ByVal format As String, ByVal ParamArray args As Object())
        LogTextBox.Text &= String.Format(format, args) & vbCrLf
        LogTextBox.SelectionStart = LogTextBox.TextLength
        LogTextBox.ScrollToCaret()
    End Sub

    Private A As 街区
    Private B As 街区

    Private Function Find(ByVal s As String) As 街区
        If s Is Nothing OrElse s = "" Then Return Nothing
        Log("Enocde: {0}", s)
        Dim found As List(Of WordFound(Of 街区Word)) = dict.Encode(s)
        If found.Count <= 0 Then
            Log("  みつかりません")
            Return Nothing
        Else
            Dim max As Integer = -1
            Dim best As 街区 = Nothing
            Dim bestWord As WordFound(Of 街区Word) = Nothing
            For Each w As WordFound(Of 街区Word) In found
                If w.Length > max Then
                    max = w.Length
                    best = w.Word.街区
                    bestWord = w
                End If
            Next
            For Each w As WordFound(Of 街区Word) In found
                Dim g As 街区 = w.Word.街区
                Log("  {0}用語:{1}  街区:{2}{3}{4}{5}", _
                    If(w Is bestWord, "*", " "), w.Word.Name, g.都道府県名, g.市区町村名, g.大字・町丁目名, g.街区符号・地番)
            Next
            Return best
        End If
    End Function

    Private Sub UpdateResult()
        ResultTextBox.Text = ""
        If A Is Nothing OrElse B Is Nothing Then Return
        ResultTextBox.Text = String.Format("{0} ({1}, {2}){3}{4} ({5}, {6}){7}平面直角座標系距離={8}m{9}緯度経度距離={10}m", _
            A.住所, A.緯度, A.経度, vbCrLf, _
            B.住所, B.緯度, B.経度, vbCrLf, _
            A.平面直角座標系距離(B), vbCrLf, _
            A.緯度経度距離(B))
    End Sub

    Private Sub 住所AComboBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 住所AComboBox.TextChanged
        A = Find(住所AComboBox.Text)
        UpdateResult()
    End Sub

    Private Sub 住所BComboBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 住所BComboBox.TextChanged
        B = Find(住所BComboBox.Text)
        UpdateResult()
    End Sub

    Private Sub 距離Form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dac As New 街区DAC
        dict = dac.ReadDict(AppSettings("置換辞書"), AppSettings("街区CSVファイル").Split(","c))
    End Sub

End Class