Imports GIS
Imports System.Configuration.ConfigurationManager
Imports System.Windows.Forms

Public Class Form1

    Private list As List(Of 街区)

    Public Shared Sub Main(ByVal args As String())
        Dim dac As New 街区DAC
        Dim form As New Form1
        form.list = dac.Read(args(0))
        Application.Run(form)
    End Sub

    Private Function Add(ByVal parent As TreeNode, ByVal name As String) As TreeNode
        Dim child As TreeNode
        If parent.Nodes.ContainsKey(name) Then
            child = parent.Nodes.Find(name, False)(0)
        Else
            child = New TreeNode(name)
            parent.Nodes.Add(child)
        End If
        Return child
    End Function

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dac As New 街区DAC
        list = dac.Read(AppSettings("街区CSVファイル").Split(","c))
        Dim root As New TreeNode
        Dim i As Integer = 0
        Dim size As Integer = list.Count
        While i < size
            Dim kenn As String = list(i).都道府県名
            Dim ken As New TreeNode(kenn)
            root.Nodes.Add(ken)
            While i < size AndAlso kenn = list(i).都道府県名
                Dim shin As String = list(i).市区町村名
                Dim shi As New TreeNode(shin)
                ken.Nodes.Add(shi)
                While i < size AndAlso kenn = list(i).都道府県名 AndAlso shin = list(i).市区町村名
                    Dim tyon As String = list(i).大字・町丁目名
                    Dim tyo As New TreeNode(tyon)
                    shi.Nodes.Add(tyo)
                    While i < size AndAlso kenn = list(i).都道府県名 AndAlso shin = list(i).市区町村名 AndAlso tyon = list(i).大字・町丁目名
                        Dim bann As String = list(i).街区符号・地番
                        Dim ban As New TreeNode(bann)
                        ban.Tag = New List(Of 街区)
                        tyo.Nodes.Add(ban)
                        While i < size AndAlso kenn = list(i).都道府県名 AndAlso shin = list(i).市区町村名 AndAlso tyon = list(i).大字・町丁目名 AndAlso bann = list(i).街区符号・地番
                            CType(ban.Tag, List(Of 街区)).Add(list(i))
                            i += 1
                        End While
                    End While
                End While
            End While
        End While
        For Each g As TreeNode In root.Nodes
            街区TreeView.Nodes.Add(g)
        Next
        UpdateResult()
    End Sub

    Private Sub 街区TreeView_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles 街区TreeView.AfterSelect
        DataGridView1.DataSource = Nothing
        Dim list As List(Of 街区) = CType(e.Node.Tag, List(Of 街区))
        If Not list Is Nothing Then
            DataGridView1.DataSource = list
        End If
    End Sub

    Private A As 街区
    Private B As 街区

    Private Function Format(ByVal a As 街区) As String
        If a Is Nothing Then Return "(選択してください)"
        Return String.Format("{0}{1}{2}{3}  {4}({5},{6})  ({7},{8})", a.都道府県名, a.市区町村名, a.大字・町丁目名, a.街区符号・地番, a.座標系番号, a.Ｘ座標, a.Ｙ座標, a.緯度, a.経度)
    End Function

    Private Sub UpdateResult()
        ATextBox.Text = Format(A)
        BTextBox.Text = Format(B)
        If A Is Nothing OrElse B Is Nothing Then Return
        平面直角座標系距離Label.Text = String.Format("{0:#,##0}m", A.平面直角座標系距離(B))
        緯度経度距離Label.Text = String.Format("{0:#,##0}m", A.緯度経度距離(B))
    End Sub

    Private Sub 選択ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 選択ToolStripMenuItem.Click
        If DataGridView1.SelectedRows.Count <= 0 Then Return
        A = CType(DataGridView1.SelectedRows(0).DataBoundItem, 街区)
        UpdateResult()
    End Sub

    Private Sub 選択ＢToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 選択ＢToolStripMenuItem.Click
        If DataGridView1.SelectedRows.Count <= 0 Then Return
        B = CType(DataGridView1.SelectedRows(0).DataBoundItem, 街区)
        UpdateResult()
    End Sub
End Class
