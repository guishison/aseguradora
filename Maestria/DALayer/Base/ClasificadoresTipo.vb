Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports System
Imports BE = BEntities
Imports BAS = BEntities.Base
Imports System.Data.Common
Imports System.Net

Namespace Base
    <Serializable> Partial Public Class ClasificadoresTipo
        Inherits DALEntity

#Region " Save Methods"

        ''' <summary>
        ''' 	Saves business information object of type ClassifierType
        ''' </summary>
        ''' <param name="BEObj">Business object of type ClassifierType </param>
        ''' <remarks>
        ''' </remarks>
        Public Sub Save(ByRef BEObj As BAS.ClasificadoresTipo)

            Dim strQuery As String = ""

            If BEObj.StatusType = BE.StatusType.Insert Then
                strQuery = "crm_base_clasificadortipo_insert"
            ElseIf BEObj.StatusType = BE.StatusType.Update Then
                strQuery = "crm_base_clasificadortipo_update"
            ElseIf BEObj.StatusType = BE.StatusType.Delete Then
                strQuery = "crm_base_clasificadortipo_update"
            End If

            Dim DALBitacora = New DALayer.Bitacora.BitacoraGeneral()
            DALBitacora.OpenConnection()
            Dim beBitacora As BEntities.Bitacora.BitacoraGeneral = New BEntities.Bitacora.BitacoraGeneral
            With beBitacora
                .Id = 0
                .Procedimiento = strQuery
                .Accion = If(BEObj.StatusType = BE.StatusType.Insert, "INSERT", If(BEObj.StatusType = BE.StatusType.Update, "UPDATE", "DELETE"))
                .PersonalId = BEObj.PersonalId
                .StatusType = BE.StatusType.Insert
                .FechaReg = Now
                .TipoTransaccionIdc = BE.TipoTransaccionBitacora.Exitosa
            End With
            Try
                If BEObj.StatusType <> BE.StatusType.NoAction Then
                    MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                    If (BEObj.StatusType <> BE.StatusType.Insert) Then
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEObj.Id)
                    End If

                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEObj.Nombre)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEObj.PersonalId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Fechareg", DbType.DateTime, BEObj.Fechareg)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaActualizacion", DbType.DateTime, BEObj.FechaActualizacion)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int32, BEObj.Estado)

                    Dim hola As String = ""
                    For i As Int32 = 0 To MyBase.Command.Parameters.Count - 1
                        hola = hola + MyBase.Command.Parameters.Item(i).ParameterName + "=" + MyBase.Command.Parameters.Item(i).Value.ToString + "-.-"
                    Next
                    hola = hola.Replace("@", "")
                    Dim strHostName As String = Dns.GetHostName()
                    Dim ipEntry As IPHostEntry = Dns.GetHostEntry(strHostName)
                    Dim IPcita As String = ipEntry.AddressList(1).ToString
                    beBitacora.Campos = hola
                    beBitacora.IpCliente = IPcita
                    beBitacora.MaquinaCliente = strHostName
                    If BEObj.StatusType = BE.StatusType.Insert Then
                        BEObj.Id = CInt(MyBase.DBFactory.ExecuteScalar(MyBase.Command, MyBase.Transaction))
                        beBitacora.TransaccionId = BEObj.Id
                        DALBitacora.Save(beBitacora)
                        DALBitacora.Commit()
                    Else
                        MyBase.DBFactory.ExecuteNonQuery(MyBase.Command, MyBase.Transaction)
                        beBitacora.TransaccionId = BEObj.Id
                        DALBitacora.Save(beBitacora)
                        DALBitacora.Commit()
                    End If
                End If

            Catch ex As Exception
                beBitacora.TipoTransaccionIdc = BE.TipoTransaccionBitacora.Fallida
                DALBitacora.Save(beBitacora)
                DALBitacora.Commit()
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
            Finally
                DALBitacora.CloseConnection()
                MyBase.DisposeCommand()
            End Try
        End Sub

        ''' <summary>
    	''' 	Saves a Colecciones business information object of type  ClassifierType
    	''' </summary>
    	''' <param name="colBEObj">Business object of type ClassifierType para Save</param>
    	''' <remarks>
    	''' </remarks>
        Public Sub Save(ByRef colBEObj As List(Of BAS.ClasificadoresTipo), ByVal IdMaster As Int32)

            Dim strQuery As String = ""
            For Each BEobj As BAS.ClasificadoresTipo In colBEObj

                If BEobj.StatusType = BE.StatusType.Insert Then
                    strQuery = "crm_base_clasificadortipo_insert"
                ElseIf BEobj.StatusType = BE.StatusType.Update Then
                    strQuery = "crm_base_clasificadortipo_update"
                ElseIf BEobj.StatusType = BE.StatusType.Delete Then
                    strQuery = "crm_base_clasificadortipo_update"
                End If

                Dim DALBitacora = New DALayer.Bitacora.BitacoraGeneral()
                DALBitacora.OpenConnection()
                Dim beBitacora As BEntities.Bitacora.BitacoraGeneral = New BEntities.Bitacora.BitacoraGeneral
                With beBitacora
                    .Id = 0
                    .Procedimiento = strQuery
                    .Accion = If(BEobj.StatusType = BE.StatusType.Insert, "INSERT", If(BEobj.StatusType = BE.StatusType.Update, "UPDATE", "DELETE"))
                    .PersonalId = BEobj.PersonalId
                    .StatusType = BE.StatusType.Insert
                    .FechaReg = Now
                    .TipoTransaccionIdc = BE.TipoTransaccionBitacora.Exitosa

                End With

                Try
                    If BEobj.StatusType <> BE.StatusType.NoAction Then
                        MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                        If (BEobj.StatusType <> BE.StatusType.Insert) Then
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEobj.Id)
                        End If

                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEobj.Nombre)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEobj.PersonalId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Fechareg", DbType.DateTime, BEobj.Fechareg)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaActualizacion", DbType.DateTime, BEobj.FechaActualizacion)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int32, BEobj.Estado)

                        Dim hola As String = ""
                        For i As Int32 = 0 To MyBase.Command.Parameters.Count - 1
                            hola = hola + MyBase.Command.Parameters.Item(i).ParameterName + "=" + MyBase.Command.Parameters.Item(i).Value.ToString + "-.-"
                        Next
                        hola = hola.Replace("@", "")
                        Dim strHostName As String = Dns.GetHostName()
                        Dim ipEntry As IPHostEntry = Dns.GetHostEntry(strHostName)
                        Dim IPcita As String = ipEntry.AddressList(1).ToString
                        beBitacora.Campos = hola
                        beBitacora.IpCliente = IPcita
                        beBitacora.MaquinaCliente = strHostName
                        If BEobj.StatusType = BE.StatusType.Insert Then
                            BEobj.Id = CInt(MyBase.DBFactory.ExecuteScalar(MyBase.Command, MyBase.Transaction))
                            beBitacora.TransaccionId = BEobj.Id
                            DALBitacora.Save(beBitacora)
                            DALBitacora.Commit()
                        Else
                            MyBase.DBFactory.ExecuteNonQuery(MyBase.Command, MyBase.Transaction)
                            beBitacora.TransaccionId = BEobj.Id
                            DALBitacora.Save(beBitacora)
                            DALBitacora.Commit()
                        End If
                    End If

                Catch ex As Exception
                    beBitacora.TipoTransaccionIdc = BE.TipoTransaccionBitacora.Fallida
                    DALBitacora.Save(beBitacora)
                    DALBitacora.Commit()
                    MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Finally
                    DALBitacora.CloseConnection()
                    MyBase.DisposeCommand()
                End Try
            Next

        End Sub

#End Region

#Region " Methods "

        ''' <summary>
        ''' 	For use on data access layer at assembly level, return an  ClassifierType type object
        ''' </summary>
        ''' <param name="Id">Object Identifier ClassifierType</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>An object of type ClassifierType</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function ReturnMaster(ByVal Id As Int32, ByVal ParamArray Relaciones() As [Enum]) As BAS.ClasificadoresTipo
            Return Search(Id, Relaciones)
        End Function

        '''' <summary>
        '''' 	Devuelve un objeto ClassifierType de tipo uno a uno con otro objeto
        '''' </summary>
        '''' <param name="Keys">Los identificadores de los objetos relacionados a ClassifierType</param>
        '''' <param name="Relaciones">Enumerador de Relaciones a retorar</param>
        '''' <returns>Un Objeto de tipo ClassifierType</returns>
        '''' <remarks>
        '''' </remarks>
        'Friend Function ReturnChild(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.ClasificadoresTipo)
        '    Dim llaves As String = MyBase.KeysArray(Keys).ToString.Replace("(", "(',")
        '    llaves = llaves.Replace(")", ",')")
        '    Dim strQuery As String = "crm_inventario_hotel_buscarcategorias"
        '    Try
        '        MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
        '        MyBase.DBFactory.AddInParameter(MyBase.Command, "@llaves", DbType.String, llaves)
        '        Dim Coleccion As List(Of BAS.ClasificadoresTipo) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.ClasificadoresTipo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
        '        If Coleccion.Count > 0 Then
        '            Me.CargarRelaciones(Coleccion, Relaciones)
        '        End If
        '        Return Coleccion
        '    Catch ex As Exception
        '        MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
        '        Return Nothing
        '    Finally
        '    End Try
        'End Function

        ''' <summary>
        ''' 	Metodo sobrecargardo que permite cargar la informacion de la base de datos al
        '''		Entidad de Negocio
        ''' </summary>
        ''' <param name="DR">El Datareader que contiene la informacion del objeto</param>
        ''' <returns>Un tipo Generico que herreda de BEGenerico.BEEntidad</returns>
        ''' <remarks>
        ''' 	Para el conjunto de Hijos se deben pasar los enumeradores de los objetos relacionados
        ''' </remarks>
        Protected Overrides Function LoadBE(Of T As BE.BEntity)(ByRef DR As IDataReader) As T
            Dim BEObject As New BAS.ClasificadoresTipo

            With DR
                BEObject.Id = .GetInt32(0)
                BEObject.Nombre = .GetString(1)
                BEObject.PersonalId = .GetInt32(2)
                BEObject.Fechareg = .GetDateTime(3)
                BEObject.FechaActualizacion = .GetDateTime(4)
                BEObject.Estado = .GetInt16(5)
            End With

            Return CType(DirectCast(BEObject, BE.BEntity), T)
        End Function

        ''' <summary>
        ''' Carga las Relaciones de la Colecciones dada
        ''' </summary>
        ''' <param name="Colecciones">Lista de objetos para cargar las Relaciones</param>
        ''' <param name="Relaciones">Enumerador de Relaciones a cargar</param>
        ''' <remarks></remarks>
        Protected Sub CargarRelaciones(ByRef Colecciones As List(Of BAS.ClasificadoresTipo), ByVal ParamArray Relaciones() As [Enum])
            Dim DALClasificadores As Clasificadores
            Dim colClasificadores As List(Of BAS.Clasificadores) = Nothing
            Dim Keys As IEnumerable(Of Int32)

            For Each RelationEnum As [Enum] In Relaciones

                Keys = From BEClasificadorTipo In Colecciones Select BEClasificadorTipo.Id
                If RelationEnum.Equals(BAS.relClasificadoresTipo.Clasificadores) Then
                    DALClasificadores = New Clasificadores(True, CType(MyBase.DBFactory, Object))
                    colClasificadores = DALClasificadores.ReturnChild(Keys, Relaciones)
                End If

            Next

            If Relaciones.GetLength(0) > 0 Then
                For Each BEClassifierType In Colecciones
                    If colClasificadores IsNot Nothing Then
                        BEClassifierType.ColeccionClasificadores = (From BEObject In colClasificadores
                                                                    Where BEObject.TipoClasificadorId = BEClassifierType.Id
                                                                    Select BEObject).ToList
                    End If
                Next
            End If

            DALClasificadores = Nothing
        End Sub

        ''' <summary>
        ''' Load Relacioneship of an Object
        ''' </summary>
        ''' <param name="BEClassifierType">Given Object</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <remarks></remarks>
        Protected Sub LoadRelaciones(ByRef BEClassifierType As BAS.ClasificadoresTipo, ByVal ParamArray Relaciones() As [Enum])
            Dim DALClasificadores As Clasificadores

            For Each RelationEnum As [Enum] In Relaciones

                Dim Keys() As Int32 = {BEClassifierType.Id}
                If RelationEnum.Equals(BAS.relClasificadoresTipo.Clasificadores) Then
                    DALClasificadores = New Clasificadores(True, CType(MyBase.DBFactory, Object))
                    BEClassifierType.ColeccionClasificadores = DALClasificadores.ReturnChild(Keys, Relaciones)
                End If

            Next

            DALClasificadores = Nothing
        End Sub

#End Region

#Region " List Methods "

        Public Function List(ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.ClasificadoresTipo)

            Dim strQuery As String = "crm_base_clasificadorestipo_lista"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                Dim Collection As List(Of BAS.ClasificadoresTipo) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.ClasificadoresTipo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    Me.CargarRelaciones(Collection, Relaciones)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        Public Function List() As List(Of BAS.ClasificadoresTipo)

            Dim strQuery As String = "crm_base_clasificadorestipo_lista_all"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                Dim Collection As List(Of BAS.ClasificadoresTipo) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.ClasificadoresTipo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    Me.CargarRelaciones(Collection)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function Count(ByVal text As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.ClasificadoresTipo)

            Dim strQuery As String = "crm_base_clasificadorestipo_count"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@text", DbType.String, text)
                Dim Collection As List(Of BAS.ClasificadoresTipo) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.ClasificadoresTipo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    Me.CargarRelaciones(Collection, Relaciones)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try

            'Dim strQuery As String = "crm_base_clasificadorestipo"
            'Try
            '    MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery)
            '    Dim Collection As Int32 = MyBase.RecordCount(strQuery)

            '    Return Collection
            'Catch ex As Exception
            '    MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
            '    Return Nothing
            'Finally
            'End Try
        End Function

        Public Function List(ByVal text As String, ByVal Estado As Int16, ByVal PageIndex As Int32, ByVal pageSize As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.ClasificadoresTipo)

            Dim strQuery As String = "crm_base_clasificadorestipo_lista"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@text", DbType.String, text)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, Estado)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@PageIndex", DbType.Int32, PageIndex)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@PageSize", DbType.Int32, pageSize)
                Dim Collection As List(Of BAS.ClasificadoresTipo) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.ClasificadoresTipo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    Me.CargarRelaciones(Collection, Relaciones)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

#End Region

#Region " Search Methods "

        ''' <summary>
        ''' 	Search an object of type ClassifierType    	''' </summary>
        ''' <param name="Id">Object identifier ClassifierType</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>An object of type ClassifierType</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function Search(ByVal Id As Int32, ByVal ParamArray Relaciones() As [Enum]) As BAS.ClasificadoresTipo

            Dim strQuery As String = "crm_base_clasificadorestipo_buscar"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, Id)
                Dim Collection As BAS.ClasificadoresTipo = MyBase.SQLConvertidorIDataReader(Of BAS.ClasificadoresTipo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function SearchPorId(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As BAS.ClasificadoresTipo
            Dim strQuery As String = "crm_base_clasificadorestipo_buscar"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, Id)
                Dim Collection As BAS.ClasificadoresTipo = MyBase.SQLConvertidorIDataReader(Of BAS.ClasificadoresTipo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

#End Region

#Region " Constructors"

        ''' <summary>
        ''' El constructor por defecto que crear una instancia de la base de datos
        ''' utilizando el Factory Pattern
        ''' </summary>
        ''' <remarks>
        '''  La instancia de la Base de datos se pasa al constructor
        '''	</remarks>
        Public Sub New()
            MyBase.New("Negocio")
        End Sub

        ''' <summary>
        ''' El constructor por defecto que crear una instancia de la base de datos
        ''' utilizando el Factory Pattern
        ''' </summary>
        ''' <remarks>
        '''  La instancia de la Base de datos se pasa al constructor
        '''	</remarks>
        Public Sub New(ByVal StringConnection As String)
            MyBase.New(StringConnection)
        End Sub

        ''' <summary>
        ''' Constructor que crea la instancia del la base de datos utilizando
        ''' el Factory pattern
        ''' </summary>
        ''' <param name="UseDBCon">True para utilizar la Connection del padre</param>
        ''' <param name="BD">la instancia de la base de datos del padre</param>
        ''' <remarks></remarks>
        Friend Sub New(ByVal UseDBCon As Boolean, ByRef BD As Object)
            MyBase.New(UseDBCon, BD)
        End Sub

        ''' <summary>
        ''' Constructor que crea la instancia de base mas la Transaction
        ''' </summary>
        ''' <param name="UseDBCon"></param>
        ''' <param name="BD"></param>
        ''' <param name="Transaction"></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal UseDBCon As Boolean, ByRef BD As Object, ByVal Transaction As DbTransaction)
            MyBase.New(UseDBCon, BD, Transaction)
        End Sub

#End Region

    End Class

End Namespace