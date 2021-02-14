Option Strict On
Option Compare Text
Option Explicit On
Option Infer On
Imports BE = BEntities
Imports BAS = BEntities.Base
Imports System.Data.Common
Imports System.Net

Namespace Base
    ''' -----------------------------------------------------------------------------
    ''' Project   : Code Generator
    ''' NameSpace : Base
    ''' Class     : Classifiers
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''    This data access component saves business object information of type Classifiers
    '''    for the service Base.
    ''' </summary>
    ''' <remarks>
    '''    Data access layer for the service Base
    ''' </remarks>
    ''' <history>
    '''   [Code]   9/21/2015 6:47:21 PM Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    <Serializable>
    Partial Public Class Clasificadores
        Inherits DALEntity

#Region " Save Methods"

        ''' <summary>
        ''' 	Saves business information object of type Classifiers
        ''' </summary>
        ''' <param name="BEObj">Business object of type Classifiers </param>
        ''' <remarks>
        ''' </remarks>
        Public Sub Save(ByRef BEObj As BAS.Clasificadores)
            Dim strQuery As String = ""

            If BEObj.StatusType = BE.StatusType.Insert Then
                strQuery = "crm_base_clasificador_insert"
            ElseIf BEObj.StatusType = BE.StatusType.Update Then
                strQuery = "crm_base_clasificador_update"
            ElseIf BEObj.StatusType = BE.StatusType.Delete Then
                strQuery = "crm_base_clasificador_update"
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

                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@TipoClasificadorId", DbType.Int32, BEObj.TipoClasificadorId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEObj.Nombre)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Value", DbType.String, BEObj.Valor)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEObj.PersonalId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaReg", DbType.DateTime, BEObj.FechaReg)
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
    	''' 	Saves a Colecciones business information object of type  Classifiers
    	''' </summary>
    	''' <param name="colBEObj">Business object of type Classifiers para Save</param>
    	''' <remarks>
    	''' </remarks>
        Public Sub Save(ByRef colBEObj As List(Of BAS.Clasificadores), ByVal IdMaster As Int32)

            Dim strQuery As String = ""
            Dim bolResult As Boolean = False
            For Each BEobj As BAS.Clasificadores In colBEObj
                If BEobj.StatusType = BE.StatusType.Insert Then
                    strQuery = "crm_base_clasificador_insert"
                ElseIf BEobj.StatusType = BE.StatusType.Update Then
                    strQuery = "crm_base_clasificador_update"
                ElseIf BEobj.StatusType = BE.StatusType.Delete Then
                    strQuery = "crm_base_clasificador_update"
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

                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@TipoClasificadorId", DbType.Int32, BEobj.TipoClasificadorId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEobj.Nombre)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Value", DbType.String, BEobj.Valor)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEobj.PersonalId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaReg", DbType.DateTime, BEobj.FechaReg)
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
        ''' 	For use on data access layer at assembly level, return an  Classifiers type object
        ''' </summary>
        ''' <param name="Id">Object Identifier Classifiers</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>An object of type Classifiers</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function ReturnMaster(ByVal Id As Int32, ByVal ParamArray Relaciones() As [Enum]) As BAS.Clasificadores
            Return Search(Id, Relaciones)
        End Function

        ''' <summary>
        ''' 	Devuelve un objeto Classifiers de tipo uno a uno con otro objeto
        ''' </summary>
        ''' <param name="Keys">Los identificadores de los objetos relacionados a Classifiers</param>
        ''' <param name="Relaciones">Enumerador de Relaciones a retorar</param>
        ''' <returns>Un Objeto de tipo Classifiers</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function ReturnChild(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.Clasificadores)
            Dim strQuery2 As String = "SELECT * from crm_base_clasificadores where Id IN " & MyBase.KeysArray(Keys)
            Try
                MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery2)
                Dim Coleccion As List(Of BAS.Clasificadores) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.Clasificadores)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Coleccion
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        Friend Function ReturnChildById(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of BEntities.Base.Clasificadores)
            'Dim llaves As String = MyBase.KeysArray(Keys).ToString.Replace("(", "(',")
            'llaves = llaves.Replace(")", ",')")
            'Dim strQuery As String = "crm_base_clasificadores_buscarclasificadores"
            Dim strQuery2 As String = "SELECT * from crm_base_clasificadores where Id IN " & MyBase.KeysArray(Keys)
            Try
                MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery2)
                'MyBase.DBFactory.AddInParameter(MyBase.Command, "@llaves", DbType.String, llaves)
                Dim Coleccion As List(Of BEntities.Base.Clasificadores) = MyBase.SQLConvertidorIDataReaderListas(Of BEntities.Base.Clasificadores)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Coleccion.Count > 0 Then
                    Me.LoadRelaciones(Coleccion, Relaciones)
                End If
                Return Coleccion
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        ''' <summary>
        ''' 	Devuelve un objeto Classifiers de tipo uno a uno con otro objeto 
        ''' </summary>
        ''' <param name="Keys">Los identificadores de los objetos relacionados a Classifiers</param>
        ''' <param name="Relaciones">Enumerador de Relaciones a retorar</param>
        ''' <returns>Un Objeto de tipo Classifiers</returns>  crm_base_clasificadores_buscarclasificadores
        ''' <remarks>
        ''' </remarks>
        Friend Function ReturnClassifierChild(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.Clasificadores)
            'Dim llaves As String = MyBase.KeysArray(Keys).ToString.Replace("(", "(',")
            'llaves = llaves.Replace(")", ",')")
            'Dim strQuery As String = "crm_base_clasificadores_buscarclasificadores"
            Dim strQuery2 As String = "SELECT * from crm_base_clasificadores where Id IN " & MyBase.KeysArray(Keys)
            Try
                MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery2)
                'MyBase.DBFactory.AddInParameter(MyBase.Command, "@llaves", DbType.String, llaves)
                Dim Coleccion As List(Of BAS.Clasificadores) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.Clasificadores)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Coleccion.Count > 0 Then
                    'Me.CargarRelaciones(Coleccion, Relaciones)
                End If
                Return Coleccion
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        Protected Overrides Function LoadBE(Of T As BE.BEntity)(ByRef DR As IDataReader) As T
            Dim BEObject As New BAS.Clasificadores

            With DR
                BEObject.Id = .GetInt32(0)
                BEObject.TipoClasificadorId = .GetInt32(1)
                BEObject.Nombre = .GetString(2)
                If Not .IsDBNull(3) Then
                    BEObject.Valor = .GetString(3)
                End If
                BEObject.PersonalId = .GetInt32(4)
                BEObject.FechaReg = .GetDateTime(5)
                BEObject.FechaActualizacion = .GetDateTime(6)
                BEObject.Estado = .GetInt16(7)
            End With
            Return CType(DirectCast(BEObject, BE.BEntity), T)
        End Function

        Protected Sub LoadRelaciones(ByRef Colecciones As List(Of BAS.Clasificadores), ByVal ParamArray Relaciones() As [Enum])
            Dim DALClasificadoresTipo As ClasificadoresTipo
            Dim colClasificadoresTipo As BAS.ClasificadoresTipo

            For Each RelationEnum As [Enum] In Relaciones
                If RelationEnum.Equals(BAS.relClasificadores.ClasificadoresTipo) Then
                    DALClasificadoresTipo = New ClasificadoresTipo(True, CType(MyBase.DBFactory, Object))
                    colClasificadoresTipo = DALClasificadoresTipo.ReturnMaster(Colecciones.ElementAt(0).TipoClasificadorId, Relaciones)
                    For Each BEClassifiers In Colecciones
                        BEClassifiers.ClasificadoresTipo = colClasificadoresTipo

                    Next
                End If
            Next

            DALClasificadoresTipo = Nothing
        End Sub

        ''' <summary>
        ''' Load Relacioneship of an Object
        ''' </summary>
        ''' <param name="BEClassifiers">Given Object</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <remarks></remarks>
        Protected Sub CargarRelaciones(ByRef BEClassifiers As BAS.Clasificadores, ByVal ParamArray Relaciones() As [Enum])
            Dim DALClassifierType As ClasificadoresTipo

            For Each RelationEnum As [Enum] In Relaciones
                If RelationEnum.Equals(BAS.relClasificadores.ClasificadoresTipo) Then
                    DALClassifierType = New ClasificadoresTipo(True, CType(MyBase.DBFactory, Object))
                    BEClassifiers.ClasificadoresTipo = DALClassifierType.ReturnMaster(BEClassifiers.TipoClasificadorId, Relaciones)
                End If
            Next

            DALClassifierType = Nothing
        End Sub

#End Region

#Region " List Methods "

        Public Function List(ByVal text As String, ByVal Estado As Int16, ByVal PageIndex As Int32, ByVal pageSize As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.Clasificadores)

            Dim strQuery As String = "crm_base_clasificadores_lista"

            MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
            MyBase.DBFactory.AddInParameter(MyBase.Command, "@text", DbType.String, text)
            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, Estado)
            MyBase.DBFactory.AddInParameter(MyBase.Command, "@PageIndex", DbType.Int32, PageIndex)
            MyBase.DBFactory.AddInParameter(MyBase.Command, "@PageSize", DbType.Int32, pageSize)

            Dim Collection As List(Of BAS.Clasificadores) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.Clasificadores)(MyBase.DBFactory.ExecuteReader(MyBase.Command))

            If Collection.Count > 0 Then
                For Each item In Collection
                    Me.CargarRelaciones(item, Relaciones)
                Next
            End If
            Return Collection
        End Function
        'Public Function List(ByVal IdClasifi) As List(Of BAS.Clasificadores)

        '    Dim strQuery As String = "crm_base_clasificadores_buscarid"
        '    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.String, Text)
        '    MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
        '    Dim Collection As List(Of BAS.Clasificadores) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.Clasificadores)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
        '    Return Collection
        'End Function
        'combo Region
        Public Function Listcb(ByVal ClassifierTypeId As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.Clasificadores)
            Dim strQuery As String = "SELECT psc.Id,psc.Description FROM base_psclassifiers psc WHERE psc.ClassifierTypeId = 15"
            Dim Colecciones As List(Of BAS.Clasificadores) = SQLListcbb(Of BAS.Clasificadores)(strQuery)

            If Colecciones.Count > 0 Then
                Me.LoadRelaciones(Colecciones, Relaciones)
            End If
            Return Colecciones
        End Function

        ''' <summary>
        ''' 	Return an object Colecciones of Classifiers
        ''' </summary>
        ''' <param name="TipoClasificadorId">Object Identifier</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>A Colecciones of type Classifiers</returns>
        ''' <remarks>
        ''' </remarks> crm_base_clasificadores_listaportipo
        Public Function List(ByVal TipoClasificadorId As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.Clasificadores)
            Dim strQuery As String = "crm_base_clasificadores_listaportipo"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@TipoClasificadorId", DbType.Int32, TipoClasificadorId)
                Dim Collection As List(Of BAS.Clasificadores) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.Clasificadores)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex.InnerException, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        Public Function ListCombo(ByVal TipoClasificadorId As Integer, ByVal text As String, ByVal Pagesize As Int32, ByVal PageIndex As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of BAS.Clasificadores)
            Dim strQuery As String = "crm_base_clasificadores_listaportipocombo"
            MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
            MyBase.DBFactory.AddInParameter(MyBase.Command, "@TipoClasificadorId", DbType.Int32, TipoClasificadorId)
            MyBase.DBFactory.AddInParameter(MyBase.Command, "@text", DbType.String, text)
            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Pagesize", DbType.Int32, Pagesize)
            MyBase.DBFactory.AddInParameter(MyBase.Command, "@PageIndex", DbType.Int32, PageIndex)
            Dim Collection As List(Of BAS.Clasificadores) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.Clasificadores)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
            'If Collection.Count > 0 Then
            '    Me.LoadRelations(Collection, Relations)
            'End If
            Return Collection
        End Function
        Public Function ListComboCount(ByVal TipoClasificadorId As Integer, ByVal text As String) As Int32
            Dim strQuery As String = "crm_base_clasificadores_listaportipocombocount"
            MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
            MyBase.DBFactory.AddInParameter(MyBase.Command, "@TipoClasificadorId", DbType.Int32, TipoClasificadorId)
            MyBase.DBFactory.AddInParameter(MyBase.Command, "@text", DbType.String, text)
            Return CInt(MyBase.DBFactory.ExecuteScalar(MyBase.Command))
        End Function
        Public Function List() As List(Of BAS.Clasificadores)
            Dim strQuery As String = "crm_base_clasificadores_listaClasif"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                Dim Collection As List(Of BAS.Clasificadores) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.Clasificadores)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex.InnerException, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function ListClasificadore(ByVal TipoClasificadorId As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.Clasificadores)
            Dim strQuery As String = "crm_base_clasificadores_listaportipo"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@TipoClasificadorId", DbType.Int32, TipoClasificadorId)
                Dim Collection As List(Of BAS.Clasificadores) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.Clasificadores)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    Me.LoadRelaciones(Collection, Relaciones)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex.InnerException, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        Public Function Count(ByVal text As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.Clasificadores)

            Dim strQuery As String = "crm_base_clasificadores_count"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@text", DbType.String, text)
                Dim Collection As List(Of BAS.Clasificadores) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.Clasificadores)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    For Each item In Collection
                        Me.CargarRelaciones(item, Relaciones)
                    Next
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
        ''' 	Search an object of type Classifiers    	''' </summary>
        ''' <param name="Id">Object identifier Classifiers</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>An object of type Classifiers</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function Search(ByVal Id As Int32, ByVal ParamArray Relaciones() As [Enum]) As BAS.Clasificadores
            Dim strQuery As String = "crm_base_clasificadores_buscarid"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, Id)
                Dim Collection As BAS.Clasificadores = MyBase.SQLConvertidorIDataReader(Of BAS.Clasificadores)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex.InnerException, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function Buscar(ByVal Id As Int32, ByVal ParamArray Relaciones() As [Enum]) As BAS.Clasificadores
            Dim strQuery As String = "crm_base_clasificadores_buscarid"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, Id)
                Dim Collection As BAS.Clasificadores = MyBase.SQLConvertidorIDataReader(Of BAS.Clasificadores)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function BuscarPorValor(ByVal valor As String, ByVal ParamArray Relaciones() As [Enum]) As BAS.Clasificadores
            Dim strQuery As String = "crm_base_clasificadores_buscarporvalor"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@valor", DbType.String, valor)
                Dim Collection As BAS.Clasificadores = MyBase.SQLConvertidorIDataReader(Of BAS.Clasificadores)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        Public Function Search(ByVal Id As Int32) As BAS.Clasificadores
            Dim strQuery As String = "crm_base_clasificadores_buscarid"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, Id)
                Dim ObjClasificador As BAS.Clasificadores = MyBase.SQLConvertidorIDataReader(Of BAS.Clasificadores)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return ObjClasificador
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
        Friend Sub New(ByVal UseDBCon As Boolean, ByRef BD As Object, ByVal Transaction As DbTransaction)
            MyBase.New(UseDBCon, BD, Transaction)
        End Sub

#End Region

    End Class

End Namespace