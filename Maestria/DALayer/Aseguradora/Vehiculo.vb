﻿Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports BE = BEntities
Imports MEB = BEntities.Aseguradora
Imports BEB = BEntities.Base
Imports DAL = DALayer.Base
Imports System.Data.Common
Imports System.Net

Namespace Aseguradora

    <Serializable>
    Public Class Vehiculo
        Inherits DALEntity

#Region " Save Methods"

        ''' <summary>
        ''' 	Saves business information object of type InstanciaSala
        ''' </summary>
        ''' <param name="BEObj">Business object of type InstanciaSala </param>
        ''' <remarks>
        ''' </remarks>
        Public Sub Guardar(ByRef BEObj As MEB.Vehiculo)

            Dim strQuery As String = ""
            If BEObj.StatusType = BE.StatusType.Insert Then
                strQuery = "crm_aseguradora_vehiculo_insert"
            ElseIf BEObj.StatusType = BE.StatusType.Update Then
                strQuery = "crm_aseguradora_vehiculo_update"
            ElseIf BEObj.StatusType = BE.StatusType.Delete Then
                strQuery = "crm_aseguradora_vehiculo_update"
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
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@ClienteId", DbType.Int32, BEObj.ClienteId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@MarcaIdc", DbType.Int32, BEObj.MarcaIdc)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@ModeloIdc", DbType.Int32, BEObj.ModeloIdc)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@TipoVehiculoIdc", DbType.Int32, BEObj.TipoVehiculoIdc)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@OrigenIdc", DbType.Int32, BEObj.OrigenIdc)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Placa", DbType.String, BEObj.Placa)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Potencia", DbType.String, BEObj.Potencia)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, BEObj.UnidadNegocioId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEObj.PersonalId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalModificacionId", DbType.Int32, BEObj.PersonalModificacionId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaRegistro", DbType.DateTime, BEObj.FechaRegistro)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaModificacion", DbType.DateTime, BEObj.FechaModificacion)
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
                DALBitacora.Commit()
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
            Finally
                DALBitacora.CloseConnection()
                MyBase.DisposeCommand()
            End Try
        End Sub

        Public Sub Guardar(ByRef colBEObj As List(Of MEB.Vehiculo))

            Dim strQuery As String = ""
            For Each BEobj As MEB.Vehiculo In colBEObj
                If BEobj.StatusType = BE.StatusType.Insert Then
                    strQuery = "crm_base_pais_insert"
                ElseIf BEobj.StatusType = BE.StatusType.Update Then
                    strQuery = "crm_base_pais_update"
                ElseIf BEobj.StatusType = BE.StatusType.Delete Then
                    strQuery = "crm_base_pais_update"
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
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@ClienteId", DbType.Int32, BEobj.ClienteId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@MarcaIdc", DbType.Int32, BEobj.MarcaIdc)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@ModeloIdc", DbType.Int32, BEobj.ModeloIdc)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@TipoVehiculoIdc", DbType.Int32, BEobj.TipoVehiculoIdc)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@OrigenIdc", DbType.Int32, BEobj.OrigenIdc)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Placa", DbType.String, BEobj.Placa)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Potencia", DbType.String, BEobj.Potencia)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, BEobj.UnidadNegocioId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEobj.PersonalId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalModificacionId", DbType.Int32, BEobj.PersonalModificacionId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaRegistro", DbType.DateTime, BEobj.FechaRegistro)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaModificacion", DbType.DateTime, BEobj.FechaModificacion)
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
        ''' 	Metodo sobrecargardo que permite cargar la informacion de la base de datos al
        '''		Entidad de Negocio
        ''' </summary>
        ''' <param name="DR">El Datareader que contiene la informacion del objeto</param>
        ''' <returns>Un tipo Generico que herreda de BEGenerico.BEEntidad</returns>
        ''' <remarks>
        ''' 	Para el conjunto de Hijos se deben pasar los enumeradores de los objetos relacionados
        ''' </remarks>
        Protected Overrides Function LoadBE(Of T As BE.BEntity)(ByRef DR As IDataReader) As T
            Dim BEObject As New MEB.Vehiculo

            With DR
                BEObject.Id = .GetInt32(0)
                BEObject.ClienteId = .GetInt32(1)
                BEObject.MarcaIdc = .GetInt32(2)
                BEObject.ModeloIdc = .GetInt32(3)
                BEObject.TipoVehiculoIdc = .GetInt32(4)
                BEObject.OrigenIdc = .GetInt32(5)
                BEObject.Placa = .GetString(6)
                BEObject.Potencia = .GetString(7)
                BEObject.UnidadNegocioId = .GetInt32(8)
                BEObject.PersonalId = .GetInt32(9)
                BEObject.PersonalModificacionId = .GetInt32(10)
                BEObject.FechaRegistro = .GetDateTime(11)
                BEObject.FechaModificacion = .GetDateTime(12)
                BEObject.Estado = .GetInt16(13)
            End With

            Return CType(DirectCast(BEObject, BE.BEntity), T)
        End Function

        Friend Function ReturnChild(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of MEB.Vehiculo)
            Dim strQuery2 As String = "SELECT * from crm_aseguradora_vehiculo where Id IN " & MyBase.KeysArray(Keys)
            Try
                MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery2)
                Dim Coleccion As List(Of MEB.Vehiculo) = MyBase.SQLConvertidorIDataReaderListas(Of MEB.Vehiculo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Coleccion.Count > 0 Then
                    Me.LoadRelations(Coleccion, Relaciones)
                End If
                Return Coleccion
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function


        ''' <summary>
        ''' Carga las Relations de la Collection dada
        ''' </summary>
        ''' <param name="Collection">Lista de objetos para cargar las Relations</param>
        ''' <param name="Relations">Enumerador de Relations a cargar</param>
        ''' <remarks></remarks>
        Protected Sub LoadRelations(ByRef Collection As List(Of MEB.Vehiculo), ByVal ParamArray Relations() As [Enum])
            Dim DALClasificador As DAL.Clasificadores
            Dim DALCliente As Cliente
            Dim colMarca As List(Of BEB.Clasificadores) = Nothing
            Dim colModelo As List(Of BEB.Clasificadores) = Nothing
            Dim colTipoVehiculo As List(Of BEB.Clasificadores) = Nothing
            Dim colOrigen As List(Of BEB.Clasificadores) = Nothing
            Dim colCliente As List(Of MEB.Cliente) = Nothing
            Dim Keys As IEnumerable(Of Int32)

            For Each RelationEnum As [Enum] In Relations
                If RelationEnum.Equals(MEB.relVehiculo.Cliente) Then
                    DALCliente = New Cliente(True, CType(MyBase.DBFactory, Object))
                    Keys = (From BEObject In Collection Select BEObject.ClienteId).Distinct
                    colCliente = DALCliente.ReturnChild(Keys, Relations)
                End If
                If RelationEnum.Equals(MEB.relVehiculo.Marca) Then
                    DALClasificador = New DAL.Clasificadores(True, CType(MyBase.DBFactory, Object))
                    Keys = (From BEObject In Collection Select BEObject.MarcaIdc).Distinct
                    colMarca = DALClasificador.ReturnChild(Keys, Relations)
                End If
                If RelationEnum.Equals(MEB.relVehiculo.Modelo) Then
                    DALClasificador = New DAL.Clasificadores(True, CType(MyBase.DBFactory, Object))
                    Keys = (From BEObject In Collection Select BEObject.ModeloIdc).Distinct
                    colModelo = DALClasificador.ReturnChild(Keys, Relations)
                End If
                If RelationEnum.Equals(MEB.relVehiculo.TipoVehiculo) Then
                    DALClasificador = New DAL.Clasificadores(True, CType(MyBase.DBFactory, Object))
                    Keys = (From BEObject In Collection Select BEObject.TipoVehiculoIdc).Distinct
                    colTipoVehiculo = DALClasificador.ReturnChild(Keys, Relations)
                End If
                If RelationEnum.Equals(MEB.relVehiculo.Origen) Then
                    DALClasificador = New DAL.Clasificadores(True, CType(MyBase.DBFactory, Object))
                    Keys = (From BEObject In Collection Select BEObject.OrigenIdc).Distinct
                    colOrigen = DALClasificador.ReturnChild(Keys, Relations)
                End If
            Next

            If Relations.GetLength(0) > 0 Then
                For Each BEVehiculo In Collection
                    If colCliente IsNot Nothing Then
                        BEVehiculo.Cliente = (From BEObject In colCliente
                                              Where BEObject.Id = BEVehiculo.ClienteId
                                              Select BEObject).FirstOrDefault
                    End If
                    If colMarca IsNot Nothing Then
                        BEVehiculo.Marca = (From BEObject In colMarca
                                            Where BEObject.Id = BEVehiculo.MarcaIdc
                                            Select BEObject).FirstOrDefault
                    End If
                    If colModelo IsNot Nothing Then
                        BEVehiculo.Modelo = (From BEObject In colModelo
                                             Where BEObject.Id = BEVehiculo.ModeloIdc
                                             Select BEObject).FirstOrDefault
                    End If
                    If colTipoVehiculo IsNot Nothing Then
                        BEVehiculo.TipoVehiculo = (From BEObject In colTipoVehiculo
                                                   Where BEObject.Id = BEVehiculo.TipoVehiculoIdc
                                                   Select BEObject).FirstOrDefault
                    End If
                    If colOrigen IsNot Nothing Then
                        BEVehiculo.Origen = (From BEObject In colOrigen
                                             Where BEObject.Id = BEVehiculo.OrigenIdc
                                             Select BEObject).FirstOrDefault
                    End If
                Next
            End If
            DALClasificador = Nothing
            DALCliente = Nothing
        End Sub

        ''' <summary>
        ''' Load Relationship of an Object
        ''' </summary>
        ''' <param name="BEVehiculo">Given Object</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <remarks></remarks>
        Protected Sub LoadRelations(ByRef BEVehiculo As MEB.Vehiculo, ByVal ParamArray Relations() As [Enum])
            Dim DALClasificador As DAL.Clasificadores
            Dim DALCliente As Cliente
            Dim colMarca As List(Of BEB.Clasificadores) = Nothing
            Dim colModelo As List(Of BEB.Clasificadores) = Nothing
            Dim colTipoVehiculo As List(Of BEB.Clasificadores) = Nothing
            Dim colOrigen As List(Of BEB.Clasificadores) = Nothing
            Dim colCliente As List(Of MEB.Cliente) = Nothing

            For Each RelationEnum As [Enum] In Relations
                If RelationEnum.Equals(MEB.relVehiculo.Marca) Then
                    DALClasificador = New DAL.Clasificadores(True, CType(MyBase.DBFactory, Object))
                    BEVehiculo.Marca = DALClasificador.ReturnMaster(BEVehiculo.MarcaIdc, Relations)
                End If
                If RelationEnum.Equals(MEB.relVehiculo.Modelo) Then
                    DALClasificador = New DAL.Clasificadores(True, CType(MyBase.DBFactory, Object))
                    BEVehiculo.Modelo = DALClasificador.ReturnMaster(BEVehiculo.ModeloIdc, Relations)
                End If
                If RelationEnum.Equals(MEB.relVehiculo.TipoVehiculo) Then
                    DALClasificador = New DAL.Clasificadores(True, CType(MyBase.DBFactory, Object))
                    BEVehiculo.TipoVehiculo = DALClasificador.ReturnMaster(BEVehiculo.TipoVehiculoIdc, Relations)
                End If
                If RelationEnum.Equals(MEB.relVehiculo.Origen) Then
                    DALClasificador = New DAL.Clasificadores(True, CType(MyBase.DBFactory, Object))
                    BEVehiculo.Origen = DALClasificador.ReturnMaster(BEVehiculo.OrigenIdc, Relations)
                End If
                If RelationEnum.Equals(MEB.relVehiculo.Cliente) Then
                    DALCliente = New Cliente(True, CType(MyBase.DBFactory, Object))
                    BEVehiculo.Cliente = DALCliente.ReturnMaster(BEVehiculo.ClienteId, Relations)
                End If
                'Dim Keys() As Int32 = {BEInstanciaSala.Id}
                'If RelationEnum.Equals(MEM.relInstanciaSala.Sala) Then
                '    DALSala = New Sala(True, CType(MyBase.DBFactory, Object))
                '    BEInstanciaSala.ColeccionSala = DALSala.ReturnChildInstanciaSala(Keys, Relations)
                'End If
            Next
            DALClasificador = Nothing
            DALCliente = Nothing
        End Sub

#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Return an object Collection of InstanciaSala
        ''' </summary>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>A Collection of type InstanciaSala</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function List(ByVal UnidadNegocioId As Int32, ByVal CantidadRegistros As Int32, ByVal NumeroPagina As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of MEB.Vehiculo)
            Dim strQuery As String = "crm_aseguradora_vehiculo_listado"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                'MyBase.DBFactory.AddInParameter(MyBase.Command, "@Desde", DbType.DateTime, Desde)
                'MyBase.DBFactory.AddInParameter(MyBase.Command, "@Hasta", DbType.DateTime, Hasta)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, UnidadNegocioId)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CantidadRegistros", DbType.Int32, CantidadRegistros)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@NumeroPagina", DbType.Int32, NumeroPagina)
                Dim Collection As List(Of MEB.Vehiculo) = MyBase.SQLConvertidorIDataReaderListas(Of MEB.Vehiculo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    Me.LoadRelations(Collection, Relations)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function ListCount(ByVal UnidadNegocioId As Int32) As Int32
            Dim strQuery As String = "crm_aseguradora_vehiculo_listadocount"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, UnidadNegocioId)
                'MyBase.DBFactory.AddInParameter(MyBase.Command, "@Login", DbType.String, If(Login = "", "%%", String.Concat("%", Login, "%")))
                Return CInt(MyBase.DBFactory.ExecuteScalar(MyBase.Command))

                'Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function List(ByVal Filter As String, ByVal Order As String, ByVal ParamArray Relations() As [Enum]) As List(Of MEB.Vehiculo)
            Dim strQuery As String = "crm_base_pais_lista"
            Dim Collection As List(Of MEB.Vehiculo) = MyBase.SQLList(Of MEB.Vehiculo)(strQuery)

            If Collection.Count > 0 Then
                Me.LoadRelations(Collection, Relations)
            End If
            Return Collection
        End Function

        ''' <summary>
        ''' 	Return an object Collection of InstanciaSala
        ''' </summary>
        ''' <param name="IdPadre">Object Identifier</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>A Collection of type InstanciaSala</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function List(ByVal IdPadre As Long, ByVal ParamArray Relations() As [Enum]) As List(Of MEB.Vehiculo)
            Dim strQuery As String = "Select * FROM membership_msInstanciaSala WHERE IdPadre = " & IdPadre.ToString
            Dim Collection As List(Of MEB.Vehiculo) = MyBase.SQLList(Of MEB.Vehiculo)(strQuery)

            If Collection.Count > 0 Then
                Me.LoadRelations(Collection, Relations)
            End If
            Return Collection
        End Function
        Public Function ListByClienteId(ByVal ClienteId As Int32, ByVal UnidadNegocioId As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of MEB.Vehiculo)
            Dim strQuery As String = "crm_aseguradora_vehiculo_listByClienteId"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@ClienteId", DbType.Int32, ClienteId)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, UnidadNegocioId)
                Dim Collection As List(Of MEB.Vehiculo) = MyBase.SQLConvertidorIDataReaderListas(Of MEB.Vehiculo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    Me.LoadRelations(Collection, Relations)
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
        ''' 	Search an object of type InstanciaSala    	''' </summary>
        ''' <param name="Id">Object identifier InstanciaSala</param>
        ''' <param name="Relations">Relationship enumerator</param>
        ''' <returns>An object of type InstanciaSala</returns>
        ''' <remarks>
        ''' </remarks> 
        Public Function Search(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As MEB.Vehiculo
            Dim strQuery As String = "crm_aseguradora_vehiculo_search"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, Id)
                Dim Collection As MEB.Vehiculo = MyBase.SQLConvertidorIDataReader(Of MEB.Vehiculo)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection IsNot Nothing Then
                    Me.LoadRelations(Collection, Relations)
                End If
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


