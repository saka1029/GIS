Imports System.IO
Imports System.Text

''' <summary>
''' CSV形式のファイルを読み込みます。
''' 以下のRFCで定義された形式のファイルを読み込むことができます。
''' RFC4180「Common Format and MIME Type for Comma-Separated Values (CSV) 」
''' 「Files (CSVファイルの一般的書式、およびMIMEタイプ) 」
''' 以下に使用例を示します。
''' Dim reader As New CsvReader("test.csv")
''' Try
'''     While reader.Read
'''         Console.WriteLine("field1={0}, field2={1}", reader(0), reader(1))
'''     End While
''' Finally
'''     reader.Close()
''' End Try
''' </summary>
''' <remarks></remarks>
Public Class CsvReader

    Private Const CR As Integer = &HD
    Private Const LF As Integer = &HA
    Private Const COMMA As Integer = AscW(","c)
    Private Const QUOTE As Integer = AscW(""""c)

    Public Shared ReadOnly DEFAULT_ENCODING As Encoding = Encoding.GetEncoding("Shift_JIS")

    ' １行分のデータを保持します。
    Private fields As New ArrayList
    Private reader As TextReader
    Private prevCh As Integer = 0

    ''' <summary>
    ''' 保持している１行分データの項目数を参照または設定します。
    ''' 現在の値よりも小さな値を設定すると余分なデータ項目が削除されます。
    ''' 現在の値よりも大きな値を設定するとデータ項目として空文字列("")が
    ''' 必要なだけ追加されます。
    ''' </summary>
    ''' <value>新しい項目数を指定します。</value>
    ''' <returns>現在の項目数を返します。</returns>
    Public Property Count() As Integer
        Get
            Return fields.Count
        End Get
        Set(ByVal Value As Integer)
            While fields.Count > Value
                fields.RemoveAt(fields.Count - 1)
            End While
            While fields.Count < Value
                fields.Add("")
            End While
        End Set
    End Property

    ' 
    ''' <summary>
    ''' 読み込んだデータを参照または変更するデフォルトのプロパティです。
    ''' </summary>
    ''' <param name="index">行内の項目番号を指定します。先頭はゼロです。</param>
    ''' <value>変更する値を指定します。</value>
    ''' <returns>取得した値を返します。</returns>
    Default Public Property Item(ByVal index As Integer) As String
        Get
            Return CStr(fields(index))
        End Get
        Set(ByVal Value As String)
            While fields.Count <= index
                fields.Add("")
            End While
            fields(index) = Value
        End Set
    End Property

    ''' <summary>
    ''' ファイル名およびエンコーディングを指定してオブジェクトを作成します。
    ''' </summary>
    ''' <param name="filename">読み込むCSVファイルを指定します。</param>
    Public Sub New(ByVal filename As String, ByVal encoding As Encoding)
        Me.reader = New StreamReader(filename, encoding)
    End Sub

    Public Sub New(ByVal filename As String)
        Me.New(filename, DEFAULT_ENCODING)
    End Sub

    ' ''' <summary>
    ' ''' ファイル名を指定してオブジェクトを作成します。
    ' ''' エンコーディングはShift_JISとなります。
    ' ''' </summary>
    ' ''' <param name="filename">読み込むCSVファイルを指定します。</param>
    'Public Sub New(ByVal filename As String)
    '    Me.reader = New StreamReader(filename, DEFAULT_ENCODING)
    'End Sub

    ''' <summary>
    ''' TextReaderを指定してオブジェクトを作成します。
    ''' </summary>
    ''' <param name="reader">読み込むTextReaderを指定します。</param>
    Public Sub New(ByVal reader As TextReader)
        Me.reader = reader
    End Sub

    ''' <summary>
    ''' ファイルを閉じます。
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Close()
        reader.Close()
    End Sub

    ''' <summary>
    ''' １行分のデータを読み込みます。
    ''' 読み込んだ内容はこのオブジェクトの中に保持します。
    ''' これらを取り出すためにはデフォルトプロパティ
    ''' 「Item(index As Integer)」を使用します。
    ''' </summary>
    ''' <returns>行を読み込んだときはTrueを、
    ''' ファイルの終わりに達したときはFalseを返します。
    ''' </returns>
    Public Function Read() As Boolean
        If prevCh = -1 Then Return False
        fields.Clear()
        Dim sb As New StringBuilder
        Dim inQuote As Boolean = False
        While True
            Dim ch As Integer = reader.Read
            Select Case ch
                Case -1 ' EOF
                    prevCh = ch
                    If fields.Count = 0 AndAlso sb.Length = 0 Then
                        Return False
                    End If
                    fields.Add(sb.ToString())
                    Return True
                Case CR
                    If inQuote Then
                        sb.Append(ChrW(ch))
                    Else
                        fields.Add(sb.ToString())
                        sb.Length = 0
                        prevCh = ch
                        Return True
                    End If
                Case LF
                    If inQuote Then
                        sb.Append(ChrW(ch))
                    ElseIf prevCh = CR Then
                        ' '\r'の後の'\n'をスキップする。
                    Else
                        fields.Add(sb.ToString())
                        sb.Length = 0
                        prevCh = ch
                        Return True
                    End If
                Case COMMA
                    If inQuote Then
                        sb.Append(ChrW(ch))
                    Else
                        fields.Add(sb.ToString())
                        sb.Length = 0
                    End If
                Case QUOTE
                    If Not inQuote AndAlso prevCh = QUOTE Then
                        sb.Append(ChrW(ch))
                    End If
                    inQuote = Not inQuote
                Case Else
                    sb.Append(ChrW(ch))
            End Select
            prevCh = ch
        End While
    End Function

    Public Function Enquote(ByVal s As String) As String
        Return """" & s.Replace("""", """""") & """"
    End Function

    ''' <summary>
    ''' 現在の行の内容をTextWriterに出力します。
    ''' 項目の区切り文字はカンマです。
    ''' 各項目は二重引用符で囲んで出力します。
    ''' 項目の値として二重引用符自身を含む場合はふたつ連続して出力します。
    ''' </summary>
    ''' <param name="writer">出力先を指定します。</param>
    Public Sub WriteTo(ByVal writer As TextWriter)
        Dim sep As String = ""
        For i As Integer = 0 To fields.Count - 1
            writer.Write(sep)
            writer.Write(Enquote(CStr(fields(i))))
            sep = ","
        Next
        writer.WriteLine()
    End Sub

End Class
