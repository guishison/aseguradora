Option Explicit On
Option Compare Text
Option Strict On
Imports BE = BEntities
Imports System.Data.Common
Imports System.Text
Imports Microsoft.Practices.EnterpriseLibrary
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.Data.Sql
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Logging
Imports System.Transactions
Imports System.Configuration

<Serializable()> Public MustInherit Class DALEntity
    Implements IDisposable

#Region " Enumerator "
    Protected Enum ErrorPolicy As Byte
        DALReplace = 1
        DALWrap = 2
        DALNew = 3
        DALThrow = 4
    End Enum
#End Region

#Region " Variables "
    Protected dpfFactory As DatabaseProviderFactory
    Protected sqlCommand As DbCommand = Nothing
    Protected sqlTransaction As DbTransaction = Nothing
    Protected sqlConection As DbConnection = Nothing
    Protected sqlDBFactory As Database = Nothing
    Protected bolUseDbConn As Boolean = False
    Protected strError As String = ""
#End Region

#Region " Mustoverride Methods "
    Protected MustOverride Function LoadBE(Of T As BE.BEntity)(ByRef DR As IDataReader) As T
#End Region

#Region " Properties "

    Public Property ErrorMessage() As String
        Get
            Return strError
        End Get
        Set(ByVal value As String)
            strError = value
        End Set
    End Property

    Protected Property Command() As DbCommand
        Get
            Return sqlCommand
        End Get
        Set(ByVal value As DbCommand)
            sqlCommand = value
        End Set
    End Property

    Public Property Connection() As DbConnection
        Get
            Return sqlConection
        End Get
        Set(ByVal value As DbConnection)
            sqlConection = value
        End Set
    End Property

    Public Property DBFactory() As Database
        Get
            Return sqlDBFactory
        End Get
        Set(ByVal value As Database)
            sqlDBFactory = value
        End Set
    End Property

    Public Property Transaction() As DbTransaction
        Get
            Return sqlTransaction
        End Get
        Set(ByVal value As DbTransaction)
            sqlTransaction = value
        End Set
    End Property

#End Region

#Region " Declaracion de Metodos Para Generar IDs"

    Protected Function GenID(ByVal strTableName As String, ByVal IdQuantity As Integer) As Long
        Dim lngGenId As Long = 0

        Dim ComandoGenId As DbCommand = DBFactory.GetStoredProcCommand("GeneratorId")
        With DBFactory
            .AddInParameter(ComandoGenId, "@Table", DbType.String, strTableName)
            .AddInParameter(ComandoGenId, "@CounterId", DbType.Int32, IdQuantity)
            .AddOutParameter(ComandoGenId, "@Id", DbType.Int32, 8)
            .ExecuteNonQuery(ComandoGenId)
            lngGenId = CType(.GetParameterValue(ComandoGenId, "@Id"), Long)
        End With

        Return lngGenId
    End Function

    Protected Function GenerateBusinessTransaction() As TransactionScope
        Dim tsOptions As TransactionOptions
        tsOptions.IsolationLevel = IsolationLevel.Serializable
        tsOptions.Timeout = New TimeSpan(0, 10, 0)
        Return New TransactionScope(TransactionScopeOption.Required, tsOptions)
    End Function

    Protected Function Count(Of T As BE.BEntity)(ByRef Coleccion As List(Of T), ByVal Estado As BE.StatusType) As Integer
        Dim intIdQuantity As Integer = 0
        For Each BEEntity As T In Coleccion
            If BEEntity.StatusType = Estado Then
                intIdQuantity += 1
            End If
        Next
        Return intIdQuantity
    End Function

#End Region

#Region " Declaracion de Metodos "

    Public Sub OpenConnection()
        sqlConection = DBFactory.CreateConnection()
        sqlConection.Open()
        sqlTransaction = sqlConection.BeginTransaction
    End Sub
    Public Sub OpenConnectionSinBegin()
        sqlConection = DBFactory.CreateConnection()
        sqlConection.Open()
    End Sub


    Public Sub CloseConnection()
        Me.DisposeConnection()
    End Sub

    Public Sub Commit()
        If sqlTransaction IsNot Nothing Then
            sqlTransaction.Commit()
        End If
    End Sub

    Public Sub Rollback()
        If sqlTransaction IsNot Nothing Then
            sqlTransaction.Rollback()
        End If
    End Sub

    Protected Function GenericList(ByVal strQuery As String) As IDataReader
        Dim Reader As IDataReader = Nothing
        Try
            Dim CommandList As DbCommand = DBFactory.GetSqlStringCommand(strQuery)

            Reader = DBFactory.ExecuteReader(CommandList)
        Catch Err As Exception
            Me.ErrorHandler(Err, ErrorPolicy.DALWrap)
        End Try
        Return Reader
    End Function

    Protected Function GenericListCommandList(CommandList As DbCommand) As IDataReader
        Dim Reader As IDataReader = Nothing
        Try
            Reader = DBFactory.ExecuteReader(CommandList)
        Catch Err As Exception
            Me.ErrorHandler(Err, ErrorPolicy.DALWrap)
        End Try
        Return Reader
    End Function

    Protected Function DataTableList(ByVal strQuery As String) As DataTable
        Dim dtTableName As New DataTable
        Dim CommandList As DbCommand = DBFactory.GetSqlStringCommand(strQuery)

        Try
            dtTableName = DBFactory.ExecuteDataSet(CommandList).Tables(0)
        Catch Err As Exception
            Me.ErrorHandler(Err, ErrorPolicy.DALWrap)
        End Try
        Return dtTableName
    End Function

    Protected Function DataSetList(ByVal strQuery As String) As DataSet
        Dim dsDatos As DataSet = Nothing
        Dim CommandList As DbCommand = DBFactory.GetSqlStringCommand(strQuery)
        Try
            dsDatos = DBFactory.ExecuteDataSet(CommandList)
        Catch Err As Exception
            Me.ErrorHandler(Err, ErrorPolicy.DALWrap)
        End Try
        Return dsDatos
    End Function

    Protected Function RecordExists(ByVal strQuery As String) As Boolean
        Dim Reader As IDataReader = GenericList(strQuery)
        Dim m_BooOk As Boolean = False
        If Reader IsNot Nothing Then
            With Reader
                If .Read Then
                    m_BooOk = True
                End If
            End With
        End If
        Return (m_BooOk)
    End Function

    Protected Function SQLList(Of T As BE.BEntity)(ByVal strQuery As String, ByVal ParamArray Relations() As [Enum]) As List(Of T)
        Dim objCollection As New List(Of T)
        Dim dtrReader As IDataReader = Nothing
        Try
            dtrReader = GenericList(strQuery)
            With dtrReader
                Do While .Read()
                    objCollection.Add(Me.LoadBE(Of T)(dtrReader))
                Loop
            End With
        Catch ex As Exception
            Me.ErrorHandler(ex, ErrorPolicy.DALWrap)
        Finally
            DisposeReader(dtrReader)
            DisposeConnection()
        End Try
        Return objCollection
    End Function
    'combo region
    Protected Function SQLListcbb(Of T As BE.BEntity)(ByVal strQuery As String, ByVal ParamArray Relations() As [Enum]) As List(Of T)
        Dim objCollection As New List(Of T)
        Dim dtrReader As IDataReader = Nothing
        Try
            dtrReader = GenericList(strQuery)
            With dtrReader
                Do While .Read()
                    'errorrr
                    objCollection.Add(Me.LoadBE(Of T)(dtrReader))
                Loop
            End With
        Catch ex As Exception
            Me.ErrorHandler(ex, ErrorPolicy.DALWrap)
        Finally
            DisposeReader(dtrReader)
            DisposeConnection()
        End Try
        Return objCollection
    End Function

    Protected Function SQLListParameter(Of T As BE.BEntity)(ByVal strQuery As String) As List(Of T)
        Dim objCollection As New List(Of T)
        Dim dtrReader As IDataReader = Nothing
        Try
            dtrReader = GenericList(strQuery)
            With dtrReader
                Do While .Read()
                    objCollection.Add(Me.LoadBE(Of T)(dtrReader))
                Loop
            End With
        Catch ex As Exception
            Me.ErrorHandler(ex, ErrorPolicy.DALWrap)
        Finally
            DisposeReader(dtrReader)
            DisposeConnection()
        End Try
        Return objCollection
    End Function

    Protected Function PaggingSQLList(Of T As BE.BEntity)(ByVal strQuery As String, ByVal Index As Integer, ByVal PageSize As Integer) As List(Of T)
        Dim objCollection As New List(Of T)
        Dim dtrReader As IDataReader = Nothing
        Dim strPager As String = " LIMIT {0},{1};"

        Try

            strPager = String.Format(strPager, (Index * PageSize), PageSize)
            strQuery &= strPager

            dtrReader = GenericList(strQuery)
            With dtrReader
                Do While .Read()
                    objCollection.Add(Me.LoadBE(Of T)(dtrReader))
                Loop
            End With
        Catch ex As Exception
            Me.ErrorHandler(ex, ErrorPolicy.DALWrap)
        Finally
            DisposeReader(dtrReader)
        End Try
        Return objCollection
    End Function

    Protected Function BuscarDatoPaginas(ByVal Orden As String, ByVal Campo As String, ByVal Dato As String, ByVal TableName As String) As Long
        Dim strQuery As String = ""
        strQuery = "Select TOP 1 TableNamePaginada.Indice From (SELECT *,row_number() over (Order by " & Orden & ") as Indice  " & _
                       " FROM " & TableName & " ) as TableNamePaginada " & _
                       " WHERE cast( TableNamePaginada." & Campo & " as varchar(100)) like '" & Dato.Trim & "'"
        Return CInt(Me.Value(strQuery))
    End Function

    Private Function AgregarIndicePaginacion(ByVal strQuery As String, ByVal OrdenarPor As String, ByVal Indice As Long, ByVal MaxRecords As Integer) As String
        Dim strNewQuery As New StringBuilder
        strNewQuery.Append("Select * From (")
        strNewQuery.Append(strQuery.Insert(strQuery.IndexOf("FROM") - 1, ",row_number() over (Order by " & OrdenarPor & ") as Indice "))
        strNewQuery.Append(") as TableNamePaginada Where Indice Between ")
        strNewQuery.Append(CStr(Indice + 1))
        strNewQuery.Append(" And ")
        strNewQuery.Append(CStr(Indice + MaxRecords))
        Return strNewQuery.ToString
    End Function

    Protected Function SQLConvertidorIDataReader(Of T As {BE.BEntity, New})(ByVal datareader2 As IDataReader) As T
        Dim m_ADR As IDataReader = Nothing
        Dim BEEntidad As T = Nothing
        Try
            m_ADR = datareader2
            With m_ADR
                If .Read Then
                    BEEntidad = Me.LoadBE(Of T)(m_ADR)
                End If
            End With
        Catch ex As Exception
            Me.ErrorHandler(ex, ErrorPolicy.DALWrap)
        Finally
            DisposeReader(m_ADR)
            DisposeReader(datareader2)
            DisposeConnection()
        End Try
        Return BEEntidad
    End Function

    Protected Function SQLConvertidorIDataReaderListas(Of T As {BE.BEntity, New})(ByVal datareader2 As IDataReader) As List(Of T)
        Dim m_ADR As IDataReader = Nothing
        Dim objCollection As New List(Of T)
        Dim dtrReader As IDataReader = Nothing
        Try
            m_ADR = datareader2
            With m_ADR
                Do While .Read()
                    objCollection.Add(Me.LoadBE(Of T)(m_ADR))
                Loop
            End With
        Catch ex As Exception
            Me.ErrorHandler(ex, ErrorPolicy.DALWrap)
        Finally
            DisposeReader(m_ADR)
            DisposeConnection()
        End Try
        Return objCollection
    End Function

    Protected Function SQLSearch(Of T As {BE.BEntity, New})(ByVal strQuery As String) As T
        Dim m_ADR As IDataReader = Nothing
        Dim BEEntidad As T = Nothing
        Try
            m_ADR = GenericList(strQuery)
            With m_ADR
                If .Read Then
                    BEEntidad = Me.LoadBE(Of T)(m_ADR)
                End If
            End With
        Catch ex As Exception
            Me.ErrorHandler(ex, ErrorPolicy.DALWrap)
        Finally
            DisposeReader(m_ADR)
            DisposeConnection()
        End Try
        Return BEEntidad
    End Function

    Protected Function Value(ByVal strQuery As String) As Object
        Dim objResult As Object = Nothing
        Dim cmdCommand As DbCommand = DBFactory.GetSqlStringCommand(strQuery)
        cmdCommand.CommandText = strQuery
        Try
            objResult = DBFactory.ExecuteScalar(cmdCommand)
        Catch Err As Exception
            Me.ErrorHandler(Err, ErrorPolicy.DALWrap)
        End Try
        Return objResult
    End Function

    Protected Sub DisposeReader(ByRef dtrReader As IDataReader)
        With dtrReader
            Try
                If dtrReader IsNot Nothing Then
                    If Not .IsClosed Then
                        .Close()
                    End If
                    .Dispose()
                End If
            Catch ex As Exception
                Me.ErrorHandler(ex, ErrorPolicy.DALWrap)
            End Try
            dtrReader = Nothing
        End With
    End Sub

    Protected Sub DisposeConnection()
        With Me.Connection
            Try
                If Me.Connection IsNot Nothing Then
                    If .State <> ConnectionState.Closed Then
                        .Close()
                    End If
                    .Dispose()
                End If
            Catch ex As Exception
                Me.ErrorHandler(ex, ErrorPolicy.DALWrap)
            End Try
            Me.Connection = Nothing
        End With
    End Sub

    Protected Sub DisposeCommand()
        If Command() IsNot Nothing Then
            Command.Dispose()
        End If
        Command = Nothing
    End Sub

    Protected Sub ErrorHandler(ByVal exnException As Exception, ByVal Policy As ErrorPolicy)
        Dim rethrow As Boolean = ExceptionPolicy.HandleException(exnException, Policy.ToString)
        If (rethrow) Then
            Throw exnException
        End If
    End Sub

    Protected Function RecordCount(ByVal strQuery As String) As Integer
        Dim strCadena As New StringBuilder
        strCadena.Append("SELECT COUNT(*) FROM ")
        strCadena.Append(strQuery)
        strCadena.Append(" AS TABLA where TABLA.Estado = 1")
        Return CInt(Me.Value(strCadena.ToString))
    End Function

    Protected Function ObjectList(ByVal SqlStr As String) As List(Of List(Of Object))
        Dim m_Col As New List(Of List(Of Object))
        Dim m_ADR As IDataReader = Nothing
        Dim lstObjeto As List(Of Object)

        Try
            m_ADR = GenericList(SqlStr)
            With m_ADR
                Do While .Read
                    lstObjeto = New List(Of Object)
                    For intCounter As Integer = 0 To m_ADR.FieldCount - 1
                        lstObjeto.Add(m_ADR.GetValue(intCounter))
                    Next
                    m_Col.Add(lstObjeto)
                Loop
            End With
        Catch ex As Exception
            Me.ErrorHandler(ex, ErrorPolicy.DALWrap)
        Finally
            DisposeReader(m_ADR)
        End Try
        Return m_Col
    End Function

#End Region

#Region " Special Methods "

    ''' <summary>
    ''' Concatena las llaves para una consulta en bloque 
    ''' </summary>
    ''' <param name="Keys"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function KeysArray(ByVal Keys As IEnumerable(Of Int32)) As String
        Dim strIndex As New StringBuilder
        If Keys.Count > 0 Then
            strIndex.Append("(")
            For Each Llave In Keys
                strIndex.Append(Llave.ToString & ",")
            Next
            Return strIndex.Replace(",", ")", strIndex.Length - 1, 1).ToString
        Else
            Return "(0)"
        End If
    End Function

    ''' <summary>
    ''' Concatena las llaves para una consulta en bloque 
    ''' </summary>
    ''' <param name="Keys"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function KeysArray(ByVal Keys As IEnumerable(Of String)) As String
        Dim strIndex As New StringBuilder
        If Keys.Count > 0 Then
            strIndex.Append("(")
            For Each Llave In Keys
                strIndex.Append("'" & Llave.ToString & "',")
            Next
            Return strIndex.Replace(",", ")", strIndex.Length - 1, 1).ToString
        Else
            Return "('')"
        End If
    End Function

#End Region

#Region "List Filters"

    'Protected Function LisFilterFields(ByVal ParamArray lstFilterFields() As stField) As String

    '    Dim strFilter As String = String.Empty
    '    Dim lstSentences As New List(Of String)

    '    If UBound(lstFilterFields) >= 0 Then
    '        For intPos As Integer = 0 To UBound(lstFilterFields)
    '            Dim strSentence As String = ""

    '            If lstFilterFields(intPos).LogicalOperator = Code.Helper.Utils.Common.LogicalOperators.None Then
    '                Select Case lstFilterFields(intPos).Operator
    '                    Case Helper.Operators.Equal
    '                        ' strSentence = " {0} = '{1}' "
    '                        strSentence = " {0} in ({1}) "
    '                    Case Helper.Operators.Different
    '                        strSentence = " {0} <> '{1}' "
    '                    Case Helper.Operators.HigherOrEqualThan
    '                        strSentence = " {0} >= '{1}' "
    '                    Case Helper.Operators.HigherThan
    '                        strSentence = " {0} > '{1}' "
    '                    Case Helper.Operators.Likes
    '                        strSentence = " {0} LIKE '%{1}%' "
    '                    Case Helper.Operators.LowerOrEqualThan
    '                        strSentence = " {0} <= '{1}' "
    '                    Case Helper.Operators.LowerThan
    '                        strSentence = " {0} < '{1}' "
    '                    Case Helper.Operators.NotLikes
    '                        strSentence = " {0} NOT LIKE '%{1}%' "
    '                    Case Helper.Operators.Between
    '                        strSentence = " {0} BETWEEN '{1}' AND '{2}' "
    '                    Case Helper.Operators.In
    '                        strSentence = " {0} in ({1}) "
    '                    Case Helper.Operators.Null
    '                        strSentence = "{0} is {1}"
    '                    Case Else
    '                        strSentence = " {0} in ({1}) "
    '                End Select

    '                If lstFilterFields(intPos).FieldValue.GetType.Name = "DateTime" Then
    '                    If lstFilterFields(intPos).Operator = Helper.Operators.Between Then
    '                        strSentence = String.Format(strSentence, "{0}", "{1:yyyy/MM/dd HH:mm:ss}", "{2:yyyy/MM/dd HH:mm:ss}")
    '                    Else
    '                        strSentence = String.Format(strSentence, "{0}", "{1:yyyy/MM/dd HH:mm:ss}")
    '                    End If
    '                End If

    '                If lstFilterFields(intPos).Operator = Helper.Operators.Between Then
    '                    If lstFilterFields(intPos).Alias IsNot Nothing Then
    '                        strSentence = String.Format(strSentence, lstFilterFields(intPos).Alias & "." & lstFilterFields(intPos).FieldName, lstFilterFields(intPos).FieldValue, lstFilterFields(intPos).FieldValue2)
    '                    Else
    '                        strSentence = String.Format(strSentence, lstFilterFields(intPos).FieldName, lstFilterFields(intPos).FieldValue, lstFilterFields(intPos).FieldValue2)
    '                    End If
    '                Else
    '                    If lstFilterFields(intPos).Alias IsNot Nothing Then
    '                        strSentence = String.Format(strSentence, lstFilterFields(intPos).Alias & "." & lstFilterFields(intPos).FieldName, lstFilterFields(intPos).FieldValue)
    '                    Else
    '                        strSentence = String.Format(strSentence, lstFilterFields(intPos).FieldName, lstFilterFields(intPos).FieldValue)
    '                    End If
    '                End If

    '                lstSentences.Add(strSentence)

    '            Else
    '                strSentence = " ( {0} {1} {2} ) "
    '                Dim strOperating1, strOperating2 As String

    '                If lstSentences.Count > 1 Then
    '                    strOperating2 = lstSentences(lstSentences.Count - 1)
    '                    lstSentences.RemoveAt(lstSentences.Count - 1)
    '                    strOperating1 = lstSentences(lstSentences.Count - 1)
    '                    lstSentences.RemoveAt(lstSentences.Count - 1)

    '                    Select Case lstFilterFields(intPos).LogicalOperator
    '                        Case Code.Helper.Utils.Common.LogicalOperators.And
    '                            strSentence = String.Format(strSentence, strOperating1, "AND", strOperating2)
    '                        Case Code.Helper.Utils.Common.LogicalOperators.OR
    '                            strSentence = String.Format(strSentence, strOperating1, "OR", strOperating2)
    '                    End Select

    '                    lstSentences.Add(strSentence)
    '                Else
    '                    lstSentences.Clear()
    '                    lstSentences.Add(" 1 = 1 ")
    '                    Exit For
    '                End If
    '            End If

    '        Next

    '        strFilter = lstSentences(0)
    '    End If


    '    Return strFilter

    'End Function
#End Region

#Region " Constructors "

    Protected Sub New()
        Try
            dpfFactory = New DatabaseProviderFactory()
            sqlDBFactory = dpfFactory.CreateDefault()

            'Dim ConectionString As String = System.Configuration.ConfigurationManager.AppSettings.Get("Business")

            'sqlDBFactory = New SqlDatabase(ConectionString)

        Catch ex As Exception
            Me.ErrorHandler(ex, ErrorPolicy.DALWrap)
        End Try

    End Sub

    Protected Sub New(ByVal UseDBCon As Boolean, ByRef BD As Object)
        If UseDBCon Then
            bolUseDbConn = True
            DBFactory = CType(BD, Database)
        Else
            DBFactory = DatabaseFactory.CreateDatabase()
        End If
    End Sub

    Protected Sub New(ByVal strConnection As String, Optional fromSecurity As Boolean = True)
        Try

            dpfFactory = New DatabaseProviderFactory()
            sqlDBFactory = dpfFactory.Create(strConnection)
            ''AVA_SECSystem_ConnectionString()
            'If fromSecurity Then
            '    'sqlDBFactory = New SqlDatabase(ConfigurationManager.AppSettings("AVA_SECSystem_ConnectionString"))
            '    sqlDBFactory = New SqlDatabase(ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
            'Else
            '    sqlDBFactory = New SqlDatabase(strConnection)
            'End If

            'sqlDBFactory = New SqlDatabase("Data Source=localhost;Initial Catalog=AVA_PMSystem_BG;Persist Security Info=True;User ID=sa;Password=Code")

            'sqlDBFactory = DatabaseFactory.CreateDatabase("Negocio")
        Catch ex As Exception
            Me.ErrorHandler(ex, ErrorPolicy.DALWrap)
        End Try
    End Sub

    Protected Sub New(ByVal UseDBCon As Boolean, ByRef BD As Object, ByRef Transaction As DbTransaction)
        DBFactory = DirectCast(BD, Database)
        sqlTransaction = Transaction
    End Sub

    Public Sub New(DataBase As Object)
        sqlDBFactory = DirectCast(DataBase, Database)
    End Sub

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                If sqlConection IsNot Nothing AndAlso sqlConection.State <> ConnectionState.Closed Then
                    Me.DisposeConnection()
                End If
            End If
        End If

        ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
        ' TODO: set large fields to null.
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class