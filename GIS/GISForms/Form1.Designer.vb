<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.街区TreeView = New System.Windows.Forms.TreeView
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.選択ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.選択ＢToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.ATextBox = New System.Windows.Forms.TextBox
        Me.BTextBox = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.平面直角座標系距離Label = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.緯度経度距離Label = New System.Windows.Forms.Label
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        '街区TreeView
        '
        Me.街区TreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.街区TreeView.Location = New System.Drawing.Point(0, 0)
        Me.街区TreeView.Name = "街区TreeView"
        Me.街区TreeView.Size = New System.Drawing.Size(238, 414)
        Me.街区TreeView.TabIndex = 1
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.街区TreeView)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1008, 414)
        Me.SplitContainer1.SplitterDistance = 238
        Me.SplitContainer1.TabIndex = 2
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 21
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(766, 292)
        Me.DataGridView1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.緯度経度距離Label)
        Me.SplitContainer2.Panel1.Controls.Add(Me.平面直角座標系距離Label)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer2.Panel1.Controls.Add(Me.BTextBox)
        Me.SplitContainer2.Panel1.Controls.Add(Me.ATextBox)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.DataGridView1)
        Me.SplitContainer2.Size = New System.Drawing.Size(766, 414)
        Me.SplitContainer2.SplitterDistance = 118
        Me.SplitContainer2.TabIndex = 1
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.選択ToolStripMenuItem, Me.選択ＢToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(137, 48)
        '
        '選択ToolStripMenuItem
        '
        Me.選択ToolStripMenuItem.Name = "選択ToolStripMenuItem"
        Me.選択ToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.選択ToolStripMenuItem.Text = "Ａとして選択"
        '
        '選択ＢToolStripMenuItem
        '
        Me.選択ＢToolStripMenuItem.Name = "選択ＢToolStripMenuItem"
        Me.選択ＢToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.選択ＢToolStripMenuItem.Text = "Ｂとして選択"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(14, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Ａ"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(14, 12)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Ｂ"
        '
        'ATextBox
        '
        Me.ATextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ATextBox.Location = New System.Drawing.Point(38, 18)
        Me.ATextBox.Name = "ATextBox"
        Me.ATextBox.ReadOnly = True
        Me.ATextBox.Size = New System.Drawing.Size(703, 19)
        Me.ATextBox.TabIndex = 1
        '
        'BTextBox
        '
        Me.BTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BTextBox.Location = New System.Drawing.Point(38, 44)
        Me.BTextBox.Name = "BTextBox"
        Me.BTextBox.ReadOnly = True
        Me.BTextBox.Size = New System.Drawing.Size(703, 19)
        Me.BTextBox.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(146, 12)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "平面直角座標系による距離："
        '
        '平面直角座標系距離Label
        '
        Me.平面直角座標系距離Label.AutoSize = True
        Me.平面直角座標系距離Label.Location = New System.Drawing.Point(183, 76)
        Me.平面直角座標系距離Label.Name = "平面直角座標系距離Label"
        Me.平面直角座標系距離Label.Size = New System.Drawing.Size(47, 12)
        Me.平面直角座標系距離Label.TabIndex = 3
        Me.平面直角座標系距離Label.Text = "-------"
        Me.平面直角座標系距離Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(48, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(116, 12)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "緯度・経度による距離："
        '
        '緯度経度距離Label
        '
        Me.緯度経度距離Label.AutoSize = True
        Me.緯度経度距離Label.Location = New System.Drawing.Point(183, 97)
        Me.緯度経度距離Label.Name = "緯度経度距離Label"
        Me.緯度経度距離Label.Size = New System.Drawing.Size(47, 12)
        Me.緯度経度距離Label.TabIndex = 3
        Me.緯度経度距離Label.Text = "-------"
        Me.緯度経度距離Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1008, 414)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents 街区TreeView As System.Windows.Forms.TreeView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents BTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ATextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 選択ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 選択ＢToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 平面直角座標系距離Label As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents 緯度経度距離Label As System.Windows.Forms.Label

End Class
