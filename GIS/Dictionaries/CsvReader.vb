Imports System.IO
Imports System.Text

''' <summary>
''' CSV�`���̃t�@�C����ǂݍ��݂܂��B
''' �ȉ���RFC�Œ�`���ꂽ�`���̃t�@�C����ǂݍ��ނ��Ƃ��ł��܂��B
''' RFC4180�uCommon Format and MIME Type for Comma-Separated Values (CSV) �v
''' �uFiles (CSV�t�@�C���̈�ʓI�����A�����MIME�^�C�v) �v
''' �ȉ��Ɏg�p��������܂��B
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

    ' �P�s���̃f�[�^��ێ����܂��B
    Private fields As New ArrayList
    Private reader As TextReader
    Private prevCh As Integer = 0

    ''' <summary>
    ''' �ێ����Ă���P�s���f�[�^�̍��ڐ����Q�Ƃ܂��͐ݒ肵�܂��B
    ''' ���݂̒l���������Ȓl��ݒ肷��Ɨ]���ȃf�[�^���ڂ��폜����܂��B
    ''' ���݂̒l�����傫�Ȓl��ݒ肷��ƃf�[�^���ڂƂ��ċ󕶎���("")��
    ''' �K�v�Ȃ����ǉ�����܂��B
    ''' </summary>
    ''' <value>�V�������ڐ����w�肵�܂��B</value>
    ''' <returns>���݂̍��ڐ���Ԃ��܂��B</returns>
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
    ''' �ǂݍ��񂾃f�[�^���Q�Ƃ܂��͕ύX����f�t�H���g�̃v���p�e�B�ł��B
    ''' </summary>
    ''' <param name="index">�s���̍��ڔԍ����w�肵�܂��B�擪�̓[���ł��B</param>
    ''' <value>�ύX����l���w�肵�܂��B</value>
    ''' <returns>�擾�����l��Ԃ��܂��B</returns>
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
    ''' �t�@�C��������уG���R�[�f�B���O���w�肵�ăI�u�W�F�N�g���쐬���܂��B
    ''' </summary>
    ''' <param name="filename">�ǂݍ���CSV�t�@�C�����w�肵�܂��B</param>
    Public Sub New(ByVal filename As String, ByVal encoding As Encoding)
        Me.reader = New StreamReader(filename, encoding)
    End Sub

    Public Sub New(ByVal filename As String)
        Me.New(filename, DEFAULT_ENCODING)
    End Sub

    ' ''' <summary>
    ' ''' �t�@�C�������w�肵�ăI�u�W�F�N�g���쐬���܂��B
    ' ''' �G���R�[�f�B���O��Shift_JIS�ƂȂ�܂��B
    ' ''' </summary>
    ' ''' <param name="filename">�ǂݍ���CSV�t�@�C�����w�肵�܂��B</param>
    'Public Sub New(ByVal filename As String)
    '    Me.reader = New StreamReader(filename, DEFAULT_ENCODING)
    'End Sub

    ''' <summary>
    ''' TextReader���w�肵�ăI�u�W�F�N�g���쐬���܂��B
    ''' </summary>
    ''' <param name="reader">�ǂݍ���TextReader���w�肵�܂��B</param>
    Public Sub New(ByVal reader As TextReader)
        Me.reader = reader
    End Sub

    ''' <summary>
    ''' �t�@�C������܂��B
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Close()
        reader.Close()
    End Sub

    ''' <summary>
    ''' �P�s���̃f�[�^��ǂݍ��݂܂��B
    ''' �ǂݍ��񂾓��e�͂��̃I�u�W�F�N�g�̒��ɕێ����܂��B
    ''' ���������o�����߂ɂ̓f�t�H���g�v���p�e�B
    ''' �uItem(index As Integer)�v���g�p���܂��B
    ''' </summary>
    ''' <returns>�s��ǂݍ��񂾂Ƃ���True���A
    ''' �t�@�C���̏I���ɒB�����Ƃ���False��Ԃ��܂��B
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
                        ' '\r'�̌��'\n'���X�L�b�v����B
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
    ''' ���݂̍s�̓��e��TextWriter�ɏo�͂��܂��B
    ''' ���ڂ̋�؂蕶���̓J���}�ł��B
    ''' �e���ڂ͓�d���p���ň͂�ŏo�͂��܂��B
    ''' ���ڂ̒l�Ƃ��ē�d���p�����g���܂ޏꍇ�͂ӂ��A�����ďo�͂��܂��B
    ''' </summary>
    ''' <param name="writer">�o�͐���w�肵�܂��B</param>
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
