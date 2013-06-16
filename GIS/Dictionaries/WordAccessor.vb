''' <summary>
''' WordDictionaryから用語(T)にアクセスするためのインタフェースです。
''' WordDictionaryが保持する用語(T)は任意のクラスで表現できますが、
''' このインタフェースを使用して内部のデータにアクセスします。
''' 用語(T)自身がインタフェースを実装するように強制してもよいわけですが、
''' Tの実体の型として型付きデータセットのDataRowのように、後からインタフェースを
''' 実装しにくい場合もあることから、このように外部のアクセサを通して
''' アクセスできるような設計にしています。
''' </summary>
''' <typeparam name="T">用語を格納するオブジェクトの型を指定します。</typeparam>
Public Interface WordAccessor(Of T)

    ''' <summary>
    ''' 用語の名称を取得します。
    ''' </summary>
    ''' <param name="word">用語を格納するオブジェクトを指定します。</param>
    ''' <returns>用語の文字列表現を返します。</returns>
    ReadOnly Property Type(ByVal word As T) As String

    ''' <summary>
    ''' 用語の種類を取得します。
    ''' </summary>
    ''' <param name="word">用語を格納するオブジェクトを指定します。</param>
    ''' <returns>用語の種類を返します。</returns>
    ReadOnly Property Name(ByVal word As T) As String

End Interface
