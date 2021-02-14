Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports BE = BEntities
Imports BAS = BEntities.Base
Imports SEC = BEntities.Seguridad
Imports DALayer.Seguridad
Imports System.Data.Common
Imports System.Net

Namespace Base
    ''' -----------------------------------------------------------------------------
    ''' Project   : Code Generator
    ''' NameSpace : Base
    ''' Class     : Cargo
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''    This data access component saves business object information of type Cargo
    '''    for the service Base.
    ''' </summary>
    ''' <remarks>
    '''    Data access layer for the service Base
    ''' </remarks>
    ''' <history>
    '''   [Code]   9/21/2015 6:47:15 PM Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    <Serializable> Partial Public Class Cargo
        Inherits DALEntity

#Region " Save Methods"

        ''' <summary>
        ''' 	Saves business information object of type Cargo
        ''' </summary>
        ''' <param name="BEObj">Business object of type Cargo </param>
        ''' <remarks>
        ''' </remarks>
        Public Sub Save(ByRef BEObj As BAS.Cargo)
            Dim strQuery As String = ""
            If BEObj.StatusType = BE.StatusType.Insert Then
                strQuery = "crm_base_cargo_insert"
            ElseIf BEObj.StatusType = BE.StatusType.Update Then
                strQuery = "crm_base_cargo_update"
            ElseIf BEObj.StatusType = BE.StatusType.Delete Then
                strQuery = "crm_base_cargo_update"
            End If

            Using DALBitacora = New DALayer.Bitacora.BitacoraGeneral()
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
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@CargoId", DbType.Int32, BEObj.CargoId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEObj.Nombre)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@OrgGrafica", DbType.Decimal, BEObj.OrgGrafica)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@ComisionBase", DbType.Decimal, BEObj.ComisionBase)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEObj.PersonalId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaReg", DbType.DateTime, BEObj.FechaReg)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaActualizacion", DbType.DateTime, BEObj.FechaActualizacion)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, BEObj.Estado)

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
                    MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Finally
                    DALBitacora.CloseConnection()
                    MyBase.DisposeCommand()
                End Try
            End Using
        End Sub

        ''' <summary>
    	''' 	Saves a collection business information object of type  Cargo
    	''' </summary>
    	''' <param name="colBEObj">Business object of type Cargo para Save</param>
    	''' <remarks>
    	''' </remarks>
        Public Sub Save(ByRef colBEObj As List(Of BAS.Cargo), ByVal IdMaster As Int32)

            Dim strQuery As String = ""

            For Each BEobj As BAS.Cargo In colBEObj
                If BEobj.StatusType = BE.StatusType.Insert Then
                    strQuery = "INSERT INTO base_msCargo " &
                    "VALUES(@Id,@CargoId,@Description,@OrganizationChart,@BaseFee); " &
                    "SELECT @@IDENTITY;"
                ElseIf BEobj.StatusType = BE.StatusType.Update Then
                    strQuery = "UPDATE base_msCargo SET " &
                    "CargoId = @CargoId,Description = @Description,OrganizationChart = @OrganizationChart, BaseFee = @BaseFee " &
                    "WHERE Id = @Id"
                ElseIf BEobj.StatusType = BE.StatusType.Delete Then
                    strQuery = "DELETE FROM base_msCargo WHERE Id = @Id"
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
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@CargoId", DbType.Int32, BEobj.CargoId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEobj.Nombre)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@OrgGrafica", DbType.Decimal, BEobj.OrgGrafica)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@ComisionBase", DbType.Decimal, BEobj.ComisionBase)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEobj.PersonalId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaReg", DbType.DateTime, BEobj.FechaReg)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaActualizacion", DbType.DateTime, BEobj.FechaActualizacion)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, BEobj.Estado)

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
        ''' 	For use on data access layer at assembly level, return an  Cargo type object
        ''' </summary>
        ''' <param name="Id">Object Identifier Cargo</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>An object of type Cargo</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function ReturnMaster(ByVal Id As Int32, ByVal ParamArray Relaciones() As [Enum]) As BAS.Cargo
            Return Search(Id, Relaciones)
        End Function

        ''' <summary>
        ''' 	Devuelve un objeto Cargo de tipo uno a uno con otro objeto
        ''' </summary>
        ''' <param name="Keys">Los identificadores de los objetos relacionados a Cargo</param>
        ''' <param name="Relaciones">Enumerador de Relaciones a retorar</param>
        ''' <returns>Un Objeto de tipo Cargo</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function ReturnChild(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.Cargo)
            'Dim llaves As String = MyBase.KeysArray(Keys).ToString.Replace("(", "(',")
            'llaves = llaves.Replace(")", ",')")
            'Dim strQuery As String = "crm_base_cargo_buscarcargos"

            Dim strQuery2 As String = "SELECT * from crm_base_cargo where Id IN " & MyBase.KeysArray(Keys)
            Try
                MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery2)
                'MyBase.DBFactory.AddInParameter(MyBase.Command, "@llaves", DbType.String, llaves)
                Dim Coleccion As List(Of BAS.Cargo) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.Cargo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Coleccion.Count > 0 Then
                    Me.CargarRelaciones(Coleccion, Relaciones)
                End If
                Return Coleccion
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

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
            Dim BEObject As New BAS.Cargo

            With DR
                BEObject.Id = .GetInt32(0)
                If Not .IsDBNull(1) Then
                    BEObject.CargoId = .GetInt32(1)
                End If
                BEObject.Nombre = .GetString(2)
                BEObject.OrgGrafica = .GetDecimal(3)
                BEObject.ComisionBase = .GetDecimal(4)
                BEObject.PersonalId = .GetInt32(5)
                BEObject.FechaReg = .GetDateTime(6)
                BEObject.FechaActualizacion = .GetDateTime(7)
                BEObject.Estado = .GetInt16(8)
            End With

            Return CType(DirectCast(BEObject, BE.BEntity), T)
        End Function

        ''' <summary>
        ''' Carga las Relaciones de la Collection dada
        ''' </summary>
        ''' <param name="Collection">Lista de objetos para cargar las Relaciones</param>
        ''' <param name="Relaciones">Enumerador de Relaciones a cargar</param>
        ''' <remarks></remarks>
        Protected Sub CargarRelaciones(ByRef Collection As List(Of BAS.Cargo), ByVal ParamArray Relaciones() As [Enum])
            Dim colCargo As List(Of BAS.Cargo) = Nothing
            Dim colPersonal As List(Of SEC.Personal) = Nothing
            Dim Keys As IEnumerable(Of Int32)

            For Each RelationEnum As [Enum] In Relaciones
                If RelationEnum.Equals(BAS.relCargo.Cargo) Then
                    Using DALCargo = New Cargo(True, CType(MyBase.DBFactory, Object))
                        Keys = (From BEObject In Collection Select BEObject.CargoId).Distinct
                        colCargo = DALCargo.ReturnChild(Keys, Relaciones)
                    End Using
                End If

                Keys = From BECargo In Collection Select BECargo.Id
                'If RelationEnum.Equals(BAS.relCargo.Cargo) Then
                '    DALCargo = New Cargo(True, CType(MyBase.DBFactory, Object))
                '    colCargo = DALCargo.ReturnChild(Keys, Relaciones)
                'End If
                If RelationEnum.Equals(BAS.relCargo.Personal) Then
                    Using DALPersonal = New Personal(True, CType(MyBase.DBFactory, Object))
                        colPersonal = DALPersonal.ReturnChild(Keys, Relaciones)
                    End Using
                End If

            Next

            If Relaciones.GetLength(0) > 0 Then
                For Each BECargo In Collection
                    If colCargo IsNot Nothing Then
                        BECargo.Cargo = (From BEObject In colCargo
                                         Where BEObject.Id = BECargo.CargoId
                                         Select BEObject).FirstOrDefault
                        If BECargo.Cargo Is Nothing Then
                            BECargo.Cargo = New BAS.Cargo
                        End If
                    End If
                    If colCargo IsNot Nothing Then
                        BECargo.ColeccionCargo = (From BEObject In colCargo
                                                  Where BEObject.CargoId = BECargo.Id
                                                  Select BEObject).ToList
                    End If
                    If colPersonal IsNot Nothing Then
                        BECargo.ColeccionPersonal = (From BEObject In colPersonal
                                                     Where BEObject.CargoId = BECargo.Id
                                                     Select BEObject).ToList
                    End If
                Next
            End If

        End Sub

        ''' <summary>
        ''' Load Relacioneship of an Object
        ''' </summary>
        ''' <param name="BECargo">Given Object</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <remarks></remarks>
        Protected Sub LoadRelaciones(ByRef BECargo As BAS.Cargo, ByVal ParamArray Relaciones() As [Enum])

            For Each RelationEnum As [Enum] In Relaciones
                If RelationEnum.Equals(BAS.relCargo.Cargo) Then
                    Using DALCargo = New Cargo(True, CType(MyBase.DBFactory, Object))
                        BECargo.Cargo = DALCargo.ReturnMaster(BECargo.CargoId, Relaciones)
                    End Using
                End If

                Dim Keys() As Int32 = {BECargo.Id}
                If RelationEnum.Equals(BAS.relCargo.Cargo) Then
                    Using DALCargo = New Cargo(True, CType(MyBase.DBFactory, Object))
                        BECargo.ColeccionCargo = DALCargo.ReturnChild(Keys, Relaciones)
                    End Using
                End If
                If RelationEnum.Equals(BAS.relCargo.Personal) Then
                    Using DALPersonal = New Personal(True, CType(MyBase.DBFactory, Object))
                        BECargo.ColeccionPersonal = DALPersonal.ReturnChild(Keys, Relaciones)
                    End Using
                End If

            Next

        End Sub

#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Return an object Collection of Cargo
        ''' </summary>
        ''' <param name="Order">Object order property column </param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>A Collection of type Cargo</returns>
        ''' <remarks>
        ''' </remarks>
        '''
        Public Function List(ByVal buscar As String, ByVal estado As Int16, ByVal CantidadRegistros As Int32, ByVal NumeroPagina As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.Cargo)
            Dim strQuery As String = "crm_base_cargo_listalike"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@buscar", DbType.String, buscar)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, estado)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CantidadRegistros", DbType.String, CantidadRegistros)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@NumeroPagina", DbType.String, NumeroPagina)
                Dim Coleccion As List(Of BAS.Cargo) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.Cargo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Coleccion.Count > 0 Then
                    Me.CargarRelaciones(Coleccion, Relaciones)
                End If
                Return Coleccion
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        'Public Function List(ByVal Order As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.Cargo)
        '    Dim strQuery As String = "SELECT * FROM base_msCargo ORDER By " & Order
        '    Dim Collection As List(Of BAS.Cargo) = MyBase.SQLList(Of BAS.Cargo)(strQuery)

        '    If Collection.Count > 0 Then
        '        Me.CargarRelaciones(Collection, Relaciones)
        '    End If
        '    Return Collection
        'End Function


        Public Function List(ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.Cargo)
            Dim strQuery As String = "crm_base_cargo_lista"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                Dim Collection As List(Of BAS.Cargo) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.Cargo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))

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

        Public Function Count(ByVal text As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.Cargo)

            Dim strQuery As String = "crm_base_cargo_count"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@buscar", DbType.String, text)
                Dim Collection As List(Of BAS.Cargo) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.Cargo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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
        Public Function ListAutorizados(ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.Cargo)
            Dim strQuery As String = "crm_base_cargo_listaautorizados"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                Dim Collection As List(Of BAS.Cargo) = MyBase.SQLConvertidorIDataReaderListas(Of BAS.Cargo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))

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
        ''' <summary>
        ''' 	Return an object Collection of Cargo
        ''' </summary>
        ''' <param name="IdPadre">Object Identifier</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>A Collection of type Cargo</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function List(ByVal IdPadre As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.Cargo)
            Dim strQuery As String = "SELECT * FROM base_msCargo WHERE IdPadre = " & IdPadre.ToString
            Dim Collection As List(Of BAS.Cargo) = MyBase.SQLList(Of BAS.Cargo)(strQuery)

            If Collection.Count > 0 Then
                Me.CargarRelaciones(Collection, Relaciones)
            End If
            Return Collection
        End Function

#End Region

#Region " Search Methods "

        ''' <summary>
        ''' 	Search an object of type Cargo    	''' </summary>
        ''' <param name="Id">Object identifier Cargo</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>An object of type Cargo</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function Search(ByVal Id As Int32, ByVal ParamArray Relaciones() As [Enum]) As BAS.Cargo
            Dim strQuery As String = "crm_base_cargo_buscarporid"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.String, Id)
                Dim Coleccion As BAS.Cargo = MyBase.SQLConvertidorIDataReader(Of BAS.Cargo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Coleccion
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function SearchPorComentario(ByVal IdPersona As Int32, ByVal ParamArray Relaciones() As [Enum]) As BAS.Cargo
            Dim strQuery As String = "crm_base_cargo_porcomentario"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@IdPersona", DbType.Int32, IdPersona)
                Dim Coleccion As BAS.Cargo = MyBase.SQLConvertidorIDataReader(Of BAS.Cargo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Coleccion
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