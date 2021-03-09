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
    Public Class Privilegio
        Inherits DALEntity

#Region " Save Methods"

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
        Friend Function ReturnMaster(ByVal Id As Long, ByVal ParamArray Relaciones() As [Enum]) As SEC.Privilegio
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
        Friend Function ReturnChild(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Privilegio)
            Dim strQuery As String = "select * from crm_seguridad_privilegio WHERE Id IN " & MyBase.KeysArray(Keys)
            Dim Coleccion As List(Of SEC.Privilegio) = Nothing
            Coleccion = MyBase.SQLList(Of SEC.Privilegio)(strQuery)
            If Coleccion.Count > 0 Then
                Me.CargarRelaciones(Coleccion, Relaciones)
            End If
            Return Coleccion
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
            Dim BEObject As New SEC.Privilegio

            With DR
                BEObject.Id = .GetInt32(0)
                If Not .IsDBNull(1) Then
                    BEObject.PersonalIdL = .GetInt32(1)
                End If
                If Not .IsDBNull(2) Then
                    BEObject.FechaReg = .GetDateTime(2)
                End If
                If Not .IsDBNull(3) Then
                    BEObject.PersonalId = .GetInt32(3)
                End If
                If Not .IsDBNull(4) Then
                    BEObject.FormularioId = .GetInt32(4)
                End If
                If Not .IsDBNull(5) Then
                    BEObject.Permiso = .GetString(5)
                End If
                If Not .IsDBNull(6) Then
                    BEObject.TiempoDias = .GetInt32(6)
                End If
                If Not .IsDBNull(7) Then
                    BEObject.UnidadNegocioId = .GetInt32(7)
                End If
                If Not .IsDBNull(8) Then
                    BEObject.Estado = .GetInt16(8)
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
        Protected Sub CargarRelaciones(ByRef Coleccion As List(Of SEC.Privilegio), ByVal ParamArray Relaciones() As [Enum])
            Dim DALFormulario As Formulario
            Dim colFormulario As List(Of SEC.Formulario) = Nothing
            Dim Keys As IEnumerable(Of Int32)

            For Each RelationEnum As [Enum] In Relaciones

                Keys = From BEModulo In Coleccion Select BEModulo.Id
                If RelationEnum.Equals(SEC.relModulo.Formulario) Then
                    DALFormulario = New Formulario(True, CType(MyBase.DBFactory, Object))
                    colFormulario = DALFormulario.ReturnChild(Keys, Relaciones)
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
        Protected Sub CargarRelaciones(ByRef BEModulo As SEC.Privilegio, ByVal ParamArray Relaciones() As [Enum])
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
        Public Function Listar(ByVal Order As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Privilegio)
            Dim strQuery As String = "SELECT * FROM security_msmodule ORDER By " & Order
            Dim Coleccion As List(Of SEC.Privilegio) = MyBase.SQLList(Of SEC.Privilegio)(strQuery)

            If Coleccion.Count > 0 Then
                Me.CargarRelaciones(Coleccion, Relaciones)
            End If
            Return Coleccion
        End Function

        ''' <summary>
        ''' 	Return an object Coleccion of Module
        ''' </summary>
        ''' <param name="IdPadre">Object Identifier</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>A Coleccion of type Module</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function ListaPorPersonal(ByVal PersonalId As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Privilegio)

            Dim strQuery As String = "crm_seguridad_privilegio_listaporpersonal"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.String, PersonalId)
                Dim Collection As List(Of SEC.Privilegio) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Privilegio)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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
        ''' 	Search an object of type Module    	''' </summary>
        ''' <param name="Id">Object identifier Module</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>An object of type Module</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function Buscar(ByVal Id As Long, ByVal ParamArray Relaciones() As [Enum]) As SEC.Privilegio
            Dim strQuery As String = "SELECT * FROM security_msmodule WHERE Id = " & Id.ToString
            Dim BEModulo As SEC.Privilegio = MyBase.SQLSearch(Of SEC.Privilegio)(strQuery)

            If BEModulo IsNot Nothing Then
                Me.CargarRelaciones(BEModulo, Relaciones)
            End If
            Return BEModulo
        End Function
        Public Function BuscarPrivilegioPorPersonal(ByVal PersonalId As Int32, ByVal FormularioId As Int32, ByVal UnidadNegocioId As Int32, ByVal ParamArray Relaciones() As [Enum]) As SEC.Privilegio
            Dim strQuery As String = "crm_seguridad_privilegio_buscarporpersonal"

            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.String, PersonalId)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@FormularioId", DbType.String, FormularioId)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.String, UnidadNegocioId)
                Dim Collection As SEC.Privilegio = MyBase.SQLConvertidorIDataReader(Of SEC.Privilegio)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection IsNot Nothing Then
                    Return Collection
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        Public Function BuscarByPersonal(ByVal Id As Int32) As List(Of SEC.Privilegio)
            Dim strQuery As String = "select * from crm_seguridad_privilegio WHERE PersonalId = " & Id & " and Estado = 1"
            Dim BEModulo As List(Of SEC.Privilegio) = MyBase.SQLList(Of SEC.Privilegio)(strQuery)

            Return BEModulo
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




        Public Sub Guardar(ByRef colBEObj As List(Of SEC.Privilegio))
            Dim strQuery As String = ""


            For Each BEobj As SEC.Privilegio In colBEObj

                Dim DALBitacora = New DALayer.Bitacora.BitacoraGeneral()
                DALBitacora.OpenConnection()
                Try
                    If BEobj.StatusType = BE.StatusType.Insert Then
                        strQuery = "[crm_seguridad_privilegio_insert]"
                    ElseIf BEobj.StatusType = BE.StatusType.Update Then
                        'strQuery = "UPDATE security_msuser SET " &
                        '"UserId = @UserId,PositionId = @PositionId, PerfilId = @PerfilId, SupervisorId = @SupervisorId,PlaceIdc = @PlaceIdc, Name = @Name,Email = @Email,Login = @Login,Password = " & "SHA(" & "@Password" & ")" & ",Hidden = @Hidden, Fee= @Fee,Registry = @Registry " &
                        '"WHERE Id = @Id"
                        strQuery = "[crm_seguridad_privilegio_update]"
                    ElseIf BEobj.StatusType = BE.StatusType.Delete Then
                        'strQuery = "UPDATE security_msuser SET UserId = @UserId WHERE Id = @Id;DELETE FROM security_msuser WHERE Id = @Id"
                        strQuery = "[crm_seguridad_privilegio_delete]"
                    End If


                    Dim beBitacora As BEntities.Bitacora.BitacoraGeneral = New BEntities.Bitacora.BitacoraGeneral
                    With beBitacora
                        .Id = 0
                        .Procedimiento = strQuery
                        .Accion = If(BEobj.StatusType = BE.StatusType.Insert, "INSERT", If(BEobj.StatusType = BE.StatusType.Update, "UPDATE", "DELETE"))
                        .PersonalId = BEobj.PersonalIdL
                        .StatusType = BE.StatusType.Insert
                        .FechaReg = Now
                        .TipoTransaccionIdc = BE.TipoTransaccionBitacora.Exitosa
                    End With

                    If BEobj.StatusType <> BE.StatusType.NoAction Then
                        MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                        If (BEobj.StatusType <> BE.StatusType.Insert) Then
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEobj.Id)
                        End If
                        If (BEobj.StatusType <> BE.StatusType.Delete) Then
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalIdL", DbType.Int32, BEobj.PersonalIdL)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaReg", DbType.DateTime, BEobj.FechaReg)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEobj.PersonalId)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@FormularioId", DbType.Int32, BEobj.FormularioId)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Permiso", DbType.String, BEobj.Permiso)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@TiempoDias", DbType.Int32, BEobj.TiempoDias)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, BEobj.UnidadNegocioId)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, BEobj.Estado)
                        End If
                        'If BEobj.StatusType = BE.StatusType.Insert Then
                        '    BEobj.Id = CInt(MyBase.DBFactory.ExecuteScalar(MyBase.Command, MyBase.Transaction))
                        'Else
                        '    MyBase.DBFactory.ExecuteNonQuery(MyBase.Command, MyBase.Transaction)
                        'End If


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
                    MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Finally
                    DALBitacora.CloseConnection()
                    MyBase.DisposeCommand()
            End Try
            Next
        End Sub



        Public Sub Guardar(ByRef BEObj As SEC.Privilegio)

            Dim strQuery As String = ""
            If BEObj.StatusType = BE.StatusType.Insert Then
                strQuery = "[crm_seguridad_privilegio_insert]"
            ElseIf BEObj.StatusType = BE.StatusType.Update Then
                'strQuery = "UPDATE security_msuser SET " &
                '"UserId = @UserId,PositionId = @PositionId, PerfilId = @PerfilId, SupervisorId = @SupervisorId,PlaceIdc = @PlaceIdc, Name = @Name,Email = @Email,Login = @Login,Password = " & "SHA(" & "@Password" & ")" & ",Hidden = @Hidden, Fee= @Fee,Registry = @Registry " &
                '"WHERE Id = @Id"
                strQuery = "[crm_seguridad_privilegio_update]"
            ElseIf BEObj.StatusType = BE.StatusType.Delete Then
                'strQuery = "UPDATE security_msuser SET UserId = @UserId WHERE Id = @Id;DELETE FROM security_msuser WHERE Id = @Id"
                strQuery = "[crm_seguridad_privilegio_delete]"
            End If

            Try

                If BEObj.StatusType <> BE.StatusType.NoAction Then
                    MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                    If (BEObj.StatusType <> BE.StatusType.Insert) Then
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEObj.Id)
                    End If
                    If (BEObj.StatusType <> BE.StatusType.Delete) Then
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalIdL", DbType.Int32, BEObj.PersonalIdL)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaReg", DbType.DateTime, BEObj.FechaReg)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEObj.PersonalId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FormularioId", DbType.Int32, BEObj.FormularioId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Permiso", DbType.String, BEObj.Permiso)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@TiempoDias", DbType.Int32, BEObj.TiempoDias)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, BEObj.UnidadNegocioId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, BEObj.Estado)
                    End If
                    If BEObj.StatusType = BE.StatusType.Insert Then
                        BEObj.Id = CInt(MyBase.DBFactory.ExecuteScalar(MyBase.Command, MyBase.Transaction))
                    Else
                        MyBase.DBFactory.ExecuteNonQuery(MyBase.Command, MyBase.Transaction)
                    End If
                End If

            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
            Finally
                MyBase.DisposeCommand()
            End Try
        End Sub

#End Region

    End Class

End Namespace