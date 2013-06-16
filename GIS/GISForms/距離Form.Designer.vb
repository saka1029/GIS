<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class 距離Form
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.住所AComboBox = New System.Windows.Forms.ComboBox
        Me.住所BComboBox = New System.Windows.Forms.ComboBox
        Me.LogTextBox = New System.Windows.Forms.TextBox
        Me.ResultTextBox = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "住所A"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 12)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "住所B"
        '
        '住所AComboBox
        '
        Me.住所AComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.住所AComboBox.FormattingEnabled = True
        Me.住所AComboBox.Items.AddRange(New Object() {"", "東京都千代田区神田錦町2-3", "千代田区神田錦町２ー３", "千代田区神田錦町二丁目三番地", "東京都千代田区神田錦町２-３"})
        Me.住所AComboBox.Location = New System.Drawing.Point(64, 6)
        Me.住所AComboBox.Name = "住所AComboBox"
        Me.住所AComboBox.Size = New System.Drawing.Size(547, 20)
        Me.住所AComboBox.TabIndex = 1
        '
        '住所BComboBox
        '
        Me.住所BComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.住所BComboBox.FormattingEnabled = True
        Me.住所BComboBox.Items.AddRange(New Object() {"", "東京都江戸川区西葛西3-22-36", "東京都目黒区上目黒2-9-3", "目黒区上目黒二丁目9番3号", "東京都目黒区上目黒二丁目9番地3号", "東京都中央区八重洲1-2-16", "大阪市中央区今橋4-2-1", "東京都文京区白山5-16-6 日土地原町ビル新館", "文京区白山五丁目一六番地６", "東京都小笠原村父島１－２"})
        Me.住所BComboBox.Location = New System.Drawing.Point(64, 33)
        Me.住所BComboBox.Name = "住所BComboBox"
        Me.住所BComboBox.Size = New System.Drawing.Size(547, 20)
        Me.住所BComboBox.TabIndex = 2
        '
        'LogTextBox
        '
        Me.LogTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LogTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.LogTextBox.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.LogTextBox.Location = New System.Drawing.Point(14, 126)
        Me.LogTextBox.Multiline = True
        Me.LogTextBox.Name = "LogTextBox"
        Me.LogTextBox.ReadOnly = True
        Me.LogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.LogTextBox.Size = New System.Drawing.Size(597, 269)
        Me.LogTextBox.TabIndex = 3
        '
        'ResultTextBox
        '
        Me.ResultTextBox.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.0!)
        Me.ResultTextBox.ForeColor = System.Drawing.Color.Red
        Me.ResultTextBox.Location = New System.Drawing.Point(66, 62)
        Me.ResultTextBox.Multiline = True
        Me.ResultTextBox.Name = "ResultTextBox"
        Me.ResultTextBox.ReadOnly = True
        Me.ResultTextBox.Size = New System.Drawing.Size(545, 58)
        Me.ResultTextBox.TabIndex = 5
        '
        '距離Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(629, 407)
        Me.Controls.Add(Me.ResultTextBox)
        Me.Controls.Add(Me.LogTextBox)
        Me.Controls.Add(Me.住所BComboBox)
        Me.Controls.Add(Me.住所AComboBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "距離Form"
        Me.Text = "ふたつの住所から距離を求める"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents 住所AComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents 住所BComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents LogTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ResultTextBox As System.Windows.Forms.TextBox
End Class
