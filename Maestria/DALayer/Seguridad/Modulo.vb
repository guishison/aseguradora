Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports BE = BEntities
Imports SEC = BEntities.Seguridad
Imports System.Data.Common
Imports System.Net

Namespace Seguridad
    <Serializable>
    Public Class Modulo
        Inherits DALEntity

#Region " Save Methods"
        Public Sub Save(ByRef BEObj As SEC.Modulo)
            Dim strQuery As String = ""
            If BEObj.StatusType = BE.StatusType.Insert Then
                strQuery = "crm_inventario_hotel_insert"
            ElseIf BEObj.StatusType = BE.StatusType.Update Then
                strQuery = "crm_inventario_hotel_update"
            ElseIf BEObj.StatusType = BE.StatusType.Delete Then
                strQuery = "crm_inventario_hotel_update"
            End If

            Dim DALBitacora = New DALayer.Bitacora.BitacoraGeneral()
            DALBitacora.OpenConnection()
            Dim beBitacora As BEntities.Bitacora.BitacoraGeneral = New BEntities.Bitacora.BitacoraGeneral
            With beBitacora
                .Id = 0
                .Procedimiento = strQuery
                .Accion = If(BEObj.StatusType = BEntities.StatusType.Insert, "INSERT", If(BEObj.StatusType = BEntities.StatusType.Update, "UPDATE", "DELETE"))
                .PersonalId = 0
                .StatusType = BEntities.StatusType.Insert
                .FechaReg = Now
                .TipoTransaccionIdc = BEntities.TipoTransaccionBitacora.Exitosa
            End With

            Try
                If BEObj.StatusType <> BE.StatusType.NoAction Then
                    MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                    If (BEObj.StatusType <> BE.StatusType.Insert) Then
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEObj.Id)
                    End If

                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEObj.Nombre)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Icono", DbType.String, BEObj.Icono)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Orden", DbType.Int32, BEObj.Orden)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, BEObj.Estado)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@SubModuloId", DbType.Int32, BEObj.SubModuloId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, BEObj.UnidadNegocioId)


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
                    If BEObj.StatusType = BEntities.StatusType.Insert Then
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
                beBitacora.TipoTransaccionIdc = BEntities.TipoTransaccionBitacora.Fallida
                DALBitacora.Save(beBitacora)
                DALBitacora.Commit()
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
            Finally
                DALBitacora.CloseConnection()
                MyBase.DisposeCommand()
            End Try
        End Sub

        Public Sub Save(ByRef colBEObj As List(Of SEC.Modulo))
            Dim strQuery As String = ""

            For Each BEobj As SEC.Modulo In colBEObj
                If BEobj.StatusType = BE.StatusType.Insert Then
                    strQuery = "crm_inventario_hotel_insert"
                ElseIf BEobj.StatusType = BE.StatusType.Update Then
                    strQuery = "crm_inventario_hotel_update"
                ElseIf BEobj.StatusType = BE.StatusType.Delete Then
                    strQuery = "crm_inventario_hotel_update"
                End If

                Dim DALBitacora = New DALayer.Bitacora.BitacoraGeneral()
                DALBitacora.OpenConnection()
                Dim beBitacora As BEntities.Bitacora.BitacoraGeneral = New BEntities.Bitacora.BitacoraGeneral
                With beBitacora
                    .Id = 0
                    .Procedimiento = strQuery
                    .Accion = If(BEobj.StatusType = BEntities.StatusType.Insert, "INSERT", If(BEobj.StatusType = BEntities.StatusType.Update, "UPDATE", "DELETE"))
                    .PersonalId = 0
                    .StatusType = BEntities.StatusType.Insert
                    .FechaReg = Now
                    .TipoTransaccionIdc = BEntities.TipoTransaccionBitacora.Exitosa
                End With

                Try
                    If BEobj.StatusType <> BE.StatusType.NoAction Then
                        MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                        If (BEobj.StatusType <> BE.StatusType.Insert) Then
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEobj.Id)
                        End If

                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEobj.Nombre)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Icono", DbType.String, BEobj.Icono)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Orden", DbType.Int32, BEobj.Orden)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, BEobj.Estado)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@SubModuloId", DbType.Int32, BEobj.SubModuloId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, BEobj.UnidadNegocioId)

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
                        If BEobj.StatusType = BEntities.StatusType.Insert Then
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
                    beBitacora.TipoTransaccionIdc = BEntities.TipoTransaccionBitacora.Fallida
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
        ''' 	For use on data access layer at assembly level, return an  Module type object
        ''' </summary>
        ''' <param name="Id">Object Identifier Module</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>An object of type Module</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function ReturnMaster(ByVal Id As Long, ByVal ParamArray Relaciones() As [Enum]) As SEC.Modulo
            Return Buscar(Id, Relaciones)
        End Function

        ''' <summary>
        ''' 	Devuelve un objeto Module de tipo uno a uno con otro objeto
        ''' </summary>
        ''' <param name="Keys">Los identificadores de los objetos relacionados a Module</param>
        ''' <param name="Relaciones">Enumerador de Relaciones a retorar</param>
        ''' <returns>Un Objeto de tipo Module</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function ReturnChild(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Modulo)
            Dim llaves As String = MyBase.KeysArray(Keys).ToString.Replace("(", "(',")
            llaves = llaves.Replace(")", ",')")
            Dim strQuery As String = "crm_seguridad_modulo_buscarmodulos"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@llaves", DbType.String, llaves)
                Dim Coleccion As List(Of SEC.Modulo) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Modulo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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
            Dim BEObject As New SEC.Modulo

            With DR
                BEObject.Id = .GetInt32(0)
                If Not .IsDBNull(1) Then
                    BEObject.Nombre = .GetString(1)
                End If
                If Not .IsDBNull(2) Then
                    BEObject.Icono = .GetString(2)
                End If
                If Not .IsDBNull(3) Then
                    BEObject.Orden = .GetInt32(3)
                End If
                If Not .IsDBNull(4) Then
                    BEObject.Estado = .GetInt16(4)
                End If
                If Not .IsDBNull(5) Then
                    BEObject.SubModuloId = .GetInt32(5)
                End If
                If Not .IsDBNull(6) Then
                    BEObject.UnidadNegocioId = .GetInt32(6)
                End If
            End With

            Return CType(DirectCast(BEObject, BE.BEntity), T)
        End Function

        ''' <summary>
        ''' Carga las Relaciones de la Coleccion dada
        ''' </summary>
        ''' <param name="Coleccion">Lista de objetos para cargar las Relaciones</param>
        ''' <param name="Relaciones">Enumerador de Relaciones a cargar</param>
        ''' <remarks></remarks>
        Protected Sub CargarRelaciones(ByVal HotelId As Int32, ByRef Coleccion As List(Of SEC.Modulo), ByVal ParamArray Relaciones() As [Enum])
            Dim DALFormulario As Formulario
            Dim colFormulario As List(Of SEC.Formulario) = Nothing
            Dim Keys As IEnumerable(Of Int32)

            For Each RelationEnum As [Enum] In Relaciones

                Keys = From BEModulo In Coleccion Select BEModulo.Id
                If RelationEnum.Equals(SEC.relModulo.Formulario) Then
                    DALFormulario = New Formulario(True, CType(MyBase.DBFactory, Object))
                    colFormulario = DALFormulario.ReturnChild(Keys, HotelId, Relaciones)
                End If

            Next

            If Relaciones.GetLength(0) > 0 Then
                For Each BEModulo In Coleccion
                    If colFormulario IsNot Nothing Then
                        BEModulo.ColeccionFormulario = (From BEObject In colFormulario
                                                        Where BEObject.ModuloId = BEModulo.Id
                                                        Select BEObject).ToList
                    End If
                Next
            End If

            DALFormulario = Nothing
        End Sub
        Protected Sub CargarRelaciones(ByRef Coleccion As List(Of SEC.Modulo), ByVal ParamArray Relaciones() As [Enum])
            Dim DALFormulario As Formulario
            Dim colFormulario As List(Of SEC.Formulario) = Nothing
            Dim Keys As IEnumerable(Of Int32)

            For Each RelationEnum As [Enum] In Relaciones

                Keys = From BEModulo In Coleccion Select BEModulo.Id
                If RelationEnum.Equals(SEC.relModulo.Formulario) Then
                    DALFormulario = New Formulario(True, CType(MyBase.DBFactory, Object))
                    colFormulario = DALFormulario.ReturnChildByModuloId(Keys, Relaciones)
                End If

            Next

            If Relaciones.GetLength(0) > 0 Then
                For Each BEModulo In Coleccion
                    If colFormulario IsNot Nothing Then
                        BEModulo.ColeccionFormulario = (From BEObject In colFormulario
                                                        Where BEObject.ModuloId = BEModulo.Id
                                                        Select BEObject).ToList
                    End If
                Next
            End If

            DALFormulario = Nothing
        End Sub

        ''' <summary>
        ''' Load Relacioneship of an Object
        ''' </summary>
        ''' <param name="BEModulo">Given Object</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <remarks></remarks>
        Protected Sub CargarRelaciones(ByRef BEModulo As SEC.Modulo, ByVal ParamArray Relaciones() As [Enum])
            Dim DALFormulario As Formulario

            For Each RelationEnum As [Enum] In Relaciones

                Dim Keys() As Int32 = {BEModulo.Id}
                If RelationEnum.Equals(SEC.relModulo.Formulario) Then
                    DALFormulario = New Formulario(True, CType(MyBase.DBFactory, Object))
                    BEModulo.ColeccionFormulario = DALFormulario.ReturnChild(Keys, Relaciones)
                End If

            Next

            DALFormulario = Nothing
        End Sub

#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Return an object Coleccion of Module
        ''' </summary>
        ''' <param name="Order">Object order property column </param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>A Coleccion of type Module</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function ListarporHotel(ByVal Order As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Modulo)

            Dim strQuery As String = "crm_seguridad_modulo_listar"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                Dim Collection As List(Of SEC.Modulo) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Modulo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    Me.CargarRelaciones(Order, Collection, Relaciones)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function ListaUN(ByVal UnidadNegocioPadreId As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Modulo)

            Dim strQuery As String = "crm_seguridad_modulo_listar_UN"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioPadreId", DbType.Int32, UnidadNegocioPadreId)
                Dim Collection As List(Of SEC.Modulo) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Modulo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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
        Public Function ListaPadresSinHijos(ByVal PersonalId As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Modulo)

            Dim strQuery As String = "crm_seguridad_modulo_buscarpadressinhijos"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, PersonalId)
                Dim Collection As List(Of SEC.Modulo) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Modulo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    Me.CargarRelaciones(PersonalId, Collection, Relaciones)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        ''' <summary>
        ''' 	Return an object Coleccion of Module
        ''' </summary>
        ''' <param name="IdPadre">Object Identifier</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>A Coleccion of type Module</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function Listar(ByVal IdPadre As Long, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Modulo)
            Dim strQuery As String = "SELECT * FROM security_msmodule WHERE IdPadre = " & IdPadre.ToString
            Dim Coleccion As List(Of SEC.Modulo) = MyBase.SQLList(Of SEC.Modulo)(strQuery)

            If Coleccion.Count > 0 Then
                Me.CargarRelaciones(Coleccion, Relaciones)
            End If
            Return Coleccion
        End Function

#End Region

#Region " Search Methods "

        ''' <summary>
        ''' 	Search an object of type Module    	''' </summary>
        ''' <param name="Id">Object identifier Module</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>An object of type Module</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function Buscar(ByVal Id As Long, ByVal ParamArray Relaciones() As [Enum]) As SEC.Modulo
            Dim strQuery As String = "crm_seguridad_modulo_buscarporid"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.String, Id)
                Dim Collection As SEC.Modulo = MyBase.SQLConvertidorIDataReader(Of SEC.Modulo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))

                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function BuscarPorNombre(ByVal Nombre As String, ByVal ParamArray Relaciones() As [Enum]) As SEC.Modulo
            Dim strQuery As String = "crm_seguridad_modulo_buscarpornombre"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, Nombre)
                Dim Collection As SEC.Modulo = MyBase.SQLConvertidorIDataReader(Of SEC.Modulo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))

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
        Friend Sub New(ByVal UseDBCon As Boolean, ByRef BD As Object, ByVal Transaction As DbTransaction)
            MyBase.New(UseDBCon, BD, Transaction)
        End Sub

#End Region

    End Class

End Namespace