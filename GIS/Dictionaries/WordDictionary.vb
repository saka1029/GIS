''' <summary>
''' 用語の辞書です。
''' 文字列の中から辞書に登録された用語を検索することができます。
''' </summary>
''' <typeparam name="T">用語を格納するオブジェクトの型を指定します。</typeparam>
''' <remarks></remarks>
Public Interface WordDictionary(Of T)

    ''' <summary>
    ''' この辞書が使用するWordAccessorを取得または設定します。
    ''' </summary>
    Property WordAccessor() As WordAccessor(Of T)

    ''' <summary>
    ''' 用語を辞書に追加します。
    ''' </summary>
    ''' <param name="word">追加する用語を指定します。</param>
    Sub Add(ByVal word As T)

    ''' <summary>
    ''' 指定した文字列に含まれる用語を検索します。
    ''' </summary>
    ''' <param name="s">検索対象文字列を指定します。</param>
    ''' <param name="types">検索対象となる用語の種類を指定します。
    ''' 何も指定しない場合はすべての種類が検索対象となります。</param>
    ''' <returns>見つかった用語と位置および長さをリストで返します。</returns>
    Function Encode(ByVal s As String, ByVal ParamArray types As String()) As List(Of WordFound(Of T))

    ''' <summary>
    ''' 指定した文字列に一致する用語を検索します。
    ''' </summary>
    ''' <param name="s">検索文字列を指定します。</param>
    ''' <param name="max">結果として返す最大件数を指定します。
    ''' ゼロ以下の値を指定した場合はすべての結果を返します。
    ''' </param>
    ''' <param name="types">検索対象とする用語の種類を指定します。
    ''' 何も指定しない場合はすべての種類が検索対象となります。</param>
    ''' <returns>見つかった用語のリストを返します。
    ''' １件も見つからない場合は空のリストを返します。</returns>
    Function Find(ByVal s As String, ByVal max As Integer, ByVal ParamArray types As String()) As List(Of T)
    'List<T> find(String s, int max, String... types);

    ''' <summary>
    ''' 指定した文字列で始まる用語を検索します。
    ''' </summary>
    ''' <param name="s">検索文字列を指定します。</param>
    ''' <param name="max">結果として返す最大件数を指定します。
    ''' ゼロ以下の値を指定した場合はすべての結果を返します。
    ''' </param>
    ''' <param name="types">検索対象とする用語の種類を指定します。
    ''' 何も指定しない場合はすべての種類が検索対象となります。</param>
    ''' <returns>見つかった用語のリストを返します。
    ''' １件も見つからない場合は空のリストを返します。</returns>
    Function FindStartsWith(ByVal s As String, ByVal max As Integer, ByVal ParamArray types As String()) As List(Of T)

    ''' <summary>
    ''' 辞書に格納されているすべての用語をリストとして返します。
    ''' </summary>
    ''' <returns>すべての用語のリストを返します。
    ''' １件も見つからな場合は空のリストを返します。</returns>
    Function ToList() As List(Of T)

End Interface
