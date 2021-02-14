Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports BE = BEntities
Imports SEC = BEntities.Seguridad
Imports BEB = BEntities.Base
Imports DALayer.Base
Imports System.Data.Common
Imports BEntities.Seguridad
Imports System.Net
Imports System.Security.Cryptography

Namespace Seguridad
    ''' -----------------------------------------------------------------------------
    ''' Project   : Code Generator
    ''' NameSpace : Security
    ''' Class     : User
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''    This data access component saves business object information of type User
    '''    for the service Security.
    ''' </summary>
    ''' <remarks>
    '''    Data access layer for the service Security
    ''' </remarks>
    ''' <history>
    '''   [Code]   9/21/2015 6:49:09 PM Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    <Serializable>
    Public Class Personal
        Inherits DALEntity

#Region " Save Methods"

        ''' <summary>
        ''' 	Saves business information object of type User
        ''' </summary>
        ''' <param name="BEObj">Business object of type User </param>
        ''' <remarks>
        ''' </remarks>
        Public Sub Guardar(ByRef BEObj As SEC.Personal)
            Dim strQuery As String = ""
            If BEObj.StatusType = BE.StatusType.Insert Then
                strQuery = "crm_seguridad_personal_insert"
            ElseIf BEObj.StatusType = BE.StatusType.Update Then
                'strQuery = "UPDATE security_msuser SET " &
                '"UserId = @UserId,PositionId = @PositionId, PerfilId = @PerfilId, SupervisorId = @SupervisorId,PlaceIdc = @PlaceIdc, Name = @Name,Email = @Email,Login = @Login,Password = " & "SHA(" & "@Password" & ")" & ",Hidden = @Hidden, Fee= @Fee,Registry = @Registry " &
                '"WHERE Id = @Id"
                strQuery = "crm_seguridad_personal_update"
            ElseIf BEObj.StatusType = BE.StatusType.Delete Then
                'strQuery = "UPDATE security_msuser SET UserId = @UserId WHERE Id = @Id;DELETE FROM security_msuser WHERE Id = @Id"
                strQuery = "crm_seguridad_personal_delete"
            End If

            Dim DALBitacora = New DALayer.Bitacora.BitacoraGeneral()
            DALBitacora.OpenConnection()
            Dim beBitacora As BEntities.Bitacora.BitacoraGeneral = New BEntities.Bitacora.BitacoraGeneral
            With beBitacora
                .Id = 0
                .Procedimiento = strQuery
                .Accion = If(BEObj.StatusType = BEntities.StatusType.Insert, "INSERT", If(BEObj.StatusType = BEntities.StatusType.Update, "UPDATE", "DELETE"))
                .PersonalId = BEObj.PersonalId
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
                    If (BEObj.StatusType <> BE.StatusType.Delete) Then
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEObj.PersonalId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@SupervisorId", DbType.Int32, BEObj.SupervisorId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@CargoId", DbType.Int32, BEObj.CargoId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@MontoComision", DbType.Decimal, BEObj.MontoComision)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEObj.Nombre)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Rut", DbType.String, BEObj.Rut)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Correo", DbType.String, BEObj.Correo)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Login", DbType.String, BEObj.Login)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Password", DbType.String, BEObj.Password)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, BEObj.UnidadNegocioId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@DepartamentoId", DbType.Int32, BEObj.DepartamentoId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Telefono", DbType.String, BEObj.Telefono)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@DependenciaIdc", DbType.Int32, BEObj.DependenciaIdc)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaReg", DbType.DateTime, BEObj.FechaReg)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaActualizacion", DbType.DateTime, BEObj.FechaActualizacion)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, BEObj.Estado)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalEstado", DbType.Int32, BEObj.PersonalEstado)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaBloqueo", DbType.DateTime, BEObj.FechaBloqueo)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@IntentosFallidos", DbType.Int32, BEObj.IntentosFallidos)
                    End If

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
        Public Sub Guardar2(ByRef BEObj As SEC.Personal)
            Dim strQuery As String = ""
            If BEObj.StatusType = BE.StatusType.Insert Then
                strQuery = "crm_seguridad_personal_insert"
            ElseIf BEObj.StatusType = BE.StatusType.Update Then
                strQuery = "UPDATE crm_seguridad_personal SET " &
                "IntentosFallidos = @IntentosFallidos,FechaBloqueo = @FechaBloqueo " &
                "WHERE Id = @Id"
                'strQuery = "crm_seguridad_personal_update"
            ElseIf BEObj.StatusType = BE.StatusType.Delete Then
                'strQuery = "UPDATE security_msuser SET UserId = @UserId WHERE Id = @Id;DELETE FROM security_msuser WHERE Id = @Id"
                'strQuery = "crm_seguridad_personal_delete"
            End If

            Dim DALBitacora = New DALayer.Bitacora.BitacoraGeneral()
            DALBitacora.OpenConnection()
            Dim beBitacora As BEntities.Bitacora.BitacoraGeneral = New BEntities.Bitacora.BitacoraGeneral
            With beBitacora
                .Id = 0
                .Procedimiento = strQuery
                .Accion = If(BEObj.StatusType = BEntities.StatusType.Insert, "INSERT", If(BEObj.StatusType = BEntities.StatusType.Update, "UPDATE", "DELETE"))
                .PersonalId = BEObj.PersonalId
                .StatusType = BEntities.StatusType.Insert
                .FechaReg = Now
                .TipoTransaccionIdc = BEntities.TipoTransaccionBitacora.Exitosa
            End With

            Try

                If BEObj.StatusType <> BE.StatusType.NoAction Then
                    MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery)
                    If (BEObj.StatusType <> BE.StatusType.Insert) Then
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEObj.Id)
                    End If
                    If (BEObj.StatusType <> BE.StatusType.Delete) Then
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEObj.PersonalId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@SupervisorId", DbType.Int32, BEObj.SupervisorId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@CargoId", DbType.Int32, BEObj.CargoId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@MontoComision", DbType.Decimal, BEObj.MontoComision)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEObj.Nombre)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Rut", DbType.String, BEObj.Rut)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Correo", DbType.String, BEObj.Correo)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Login", DbType.String, BEObj.Login)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Password", DbType.String, BEObj.Password)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, BEObj.UnidadNegocioId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@DepartamentoId", DbType.Int32, BEObj.DepartamentoId)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Telefono", DbType.String, BEObj.Telefono)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@DependenciaIdc", DbType.Int32, BEObj.DependenciaIdc)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaReg", DbType.DateTime, BEObj.FechaReg)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaActualizacion", DbType.DateTime, BEObj.FechaActualizacion)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, BEObj.Estado)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalEstado", DbType.Int32, BEObj.PersonalEstado)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaBloqueo", DbType.DateTime, BEObj.FechaBloqueo)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@IntentosFallidos", DbType.Int32, BEObj.IntentosFallidos)
                    End If

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
        Public Sub Guardar(ByRef ListBEObj As List(Of SEC.Personal))

            Dim strQuery As String = ""
            For Each BEObj In ListBEObj

                If BEObj.StatusType = BE.StatusType.Insert Then
                strQuery = "crm_seguridad_personal_insert"
            ElseIf BEObj.StatusType = BE.StatusType.Update Then
                'strQuery = "UPDATE security_msuser SET " &
                '"UserId = @UserId,PositionId = @PositionId, PerfilId = @PerfilId, SupervisorId = @SupervisorId,PlaceIdc = @PlaceIdc, Name = @Name,Email = @Email,Login = @Login,Password = " & "SHA(" & "@Password" & ")" & ",Hidden = @Hidden, Fee= @Fee,Registry = @Registry " &
                '"WHERE Id = @Id"
                strQuery = "crm_seguridad_personal_update"
            ElseIf BEObj.StatusType = BE.StatusType.Delete Then
                'strQuery = "UPDATE security_msuser SET UserId = @UserId WHERE Id = @Id;DELETE FROM security_msuser WHERE Id = @Id"
                strQuery = "crm_seguridad_personal_delete"
            End If

            Dim DALBitacora = New DALayer.Bitacora.BitacoraGeneral()
            DALBitacora.OpenConnection()
            Dim beBitacora As BEntities.Bitacora.BitacoraGeneral = New BEntities.Bitacora.BitacoraGeneral
            With beBitacora
                .Id = 0
                .Procedimiento = strQuery
                .Accion = If(BEObj.StatusType = BEntities.StatusType.Insert, "INSERT", If(BEObj.StatusType = BEntities.StatusType.Update, "UPDATE", "DELETE"))
                .PersonalId = BEObj.PersonalId
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
                        If (BEObj.StatusType <> BE.StatusType.Delete) Then
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEObj.PersonalId)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@SupervisorId", DbType.Int32, BEObj.SupervisorId)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@CargoId", DbType.Int32, BEObj.CargoId)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@MontoComision", DbType.Decimal, BEObj.MontoComision)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, BEObj.Nombre)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Rut", DbType.String, BEObj.Rut)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Correo", DbType.String, BEObj.Correo)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Login", DbType.String, BEObj.Login)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Password", DbType.String, BEObj.Password)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Int32, BEObj.UnidadNegocioId)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@DepartamentoId", DbType.Int32, BEObj.DepartamentoId)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Telefono", DbType.String, BEObj.Telefono)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@DependenciaIdc", DbType.Int32, BEObj.DependenciaIdc)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaReg", DbType.DateTime, BEObj.FechaReg)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaActualizacion", DbType.DateTime, BEObj.FechaActualizacion)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, BEObj.Estado)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalEstado", DbType.Int32, BEObj.PersonalEstado)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaBloqueo", DbType.DateTime, BEObj.FechaBloqueo)
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@IntentosFallidos", DbType.Int32, BEObj.IntentosFallidos)
                        End If

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
            Next
        End Sub
        ''Public Sub Guardar2(ByRef BEObj As SEC.Personal)
        ''    Dim strQuery As String = ""
        ''    If BEObj.StatusType = BE.StatusType.Insert Then
        ''        strQuery = "INSERT INTO security_msuser " &
        ''        "VALUES(@Id,@UserId,@PositionId,@PerfilId,@SupervisorId,@PlaceIdc,@Name,@Email,@Login,@Password,@Hidden,@Fee, @Registry); SELECT @@IDENTITY"
        ''    ElseIf BEObj.StatusType = BE.StatusType.Update Then
        ''        strQuery = "UPDATE security_msuser SET " &
        ''        "UserId = @UserId,PositionId = @PositionId, PerfilId = @PerfilId, SupervisorId = @SupervisorId,PlaceIdc = @PlaceIdc, Name = @Name,Email = @Email,Login = @Login,Password = @Password,Hidden = @Hidden, Fee= @Fee,Registry = @Registry " &
        ''        "WHERE Id = @Id"
        ''    ElseIf BEObj.StatusType = BE.StatusType.Delete Then
        ''        strQuery = "UPDATE security_msuser SET UserId = @UserId WHERE Id = @Id;DELETE FROM security_msuser WHERE Id = @Id"
        ''    End If

        ''    Try

        ''        If BEObj.StatusType <> BE.StatusType.NoAction Then
        ''            MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery)
        ''            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int64, BEObj.Id)
        ''            MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int64, BEObj.PersonalId)
        ''            MyBase.DBFactory.AddInParameter(MyBase.Command, "@CargoId", DbType.Int64, BEObj.CargoId)
        ''            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.Int64, BEObj.Nombre)
        ''            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Correo", DbType.String, BEObj.Correo)
        ''            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Login", DbType.String, BEObj.Login)
        ''            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Password", DbType.String, BEObj.Password)
        ''            MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioId", DbType.Boolean, BEObj.UnidadNegocioId)
        ''            MyBase.DBFactory.AddInParameter(MyBase.Command, "@FechaReg", DbType.Decimal, BEObj.FechaReg)
        ''            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.DateTime, BEObj.Estado)

        ''            If BEObj.StatusType = BE.StatusType.Insert Then
        ''                BEObj.Id = CInt(MyBase.DBFactory.ExecuteScalar(MyBase.Command, MyBase.Transaction))
        ''            Else
        ''                MyBase.DBFactory.ExecuteNonQuery(MyBase.Command, MyBase.Transaction)
        ''            End If
        ''        End If

        ''    Catch ex As Exception
        ''        MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
        ''    Finally
        ''        MyBase.DisposeCommand()
        ''    End Try
        ''End Sub


#End Region

#Region " Methods "

        ''' <summary>
        ''' 	For use on data access layer at assembly level, return an  User type object
        ''' </summary>
        ''' <param name="Id">Object Identifier User</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>An object of type User</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function ReturnMaster(ByVal Id As Int32, ByVal ParamArray Relaciones() As [Enum]) As SEC.Personal
            Return Buscar(Id, Relaciones)
        End Function

        ''' <summary>
        ''' 	Devuelve un objeto User de tipo uno a uno con otro objeto
        ''' </summary>
        ''' <param name="Keys">Los identificadores de los objetos relacionados a User</param>
        ''' <param name="Relaciones">Enumerador de Relaciones a retorar</param>
        ''' <returns>Un Objeto de tipo User</returns>
        ''' <remarks>
        ''' </remarks>
        ''' 
        Friend Function ReturnChild(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            'Dim llaves As String = MyBase.KeysArray(Keys).ToString.Replace("(", "(',")
            'llaves = llaves.Replace(")", ",')")
            'Dim strQuery As String = "crm_seguridad_personal_buscarpersonales"
            Dim strQuery2 As String = "SELECT * from crm_seguridad_personal where Id IN " & MyBase.KeysArray(Keys)
            Try
                MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery2)
                'MyBase.DBFactory.AddInParameter(MyBase.Command, "@llaves", DbType.String, llaves)
                Dim Coleccion As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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

        Friend Function ReturnChild2(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "SELECT u.* FROM security_msuser u inner join membership_mscontact m on m.OpcId = u.Id WHERE m.OpcId = u.Id and m.Id IN " & MyBase.KeysArray(Keys)
            Dim Collection As List(Of SEC.Personal) = Nothing
            Collection = MyBase.SQLList(Of SEC.Personal)(strQuery)
            If Collection.Count > 0 Then
                Me.CargarRelaciones(Collection, Relaciones)
            End If
            Return Collection
        End Function




        ''' <summary>
        ''' 	Devuelve un objeto User de tipo uno a uno con otro objeto
        ''' </summary>
        ''' <param name="Keys">Los identificadores de los objetos relacionados a User</param>
        ''' <param name="Relaciones">Enumerador de Relaciones a retorar</param>
        ''' <returns>Un Objeto de tipo User</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function ReturnChildSupervised(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "SELECT * FROM security_msuser WHERE SupervisorId IN " & MyBase.KeysArray(Keys)
            Dim Collection As List(Of SEC.Personal) = Nothing
            Collection = MyBase.SQLList(Of SEC.Personal)(strQuery)
            If Collection.Count > 0 Then
                Me.CargarRelaciones(Collection, Relaciones)
            End If
            Return Collection
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
            Dim BEObject As New SEC.Personal

            With DR
                BEObject.Id = .GetInt32(0)
                If Not .IsDBNull(1) Then
                    BEObject.PersonalId = .GetInt32(1)
                End If
                If Not .IsDBNull(2) Then
                    BEObject.SupervisorId = .GetInt32(2)
                End If
                If Not .IsDBNull(3) Then
                    BEObject.CargoId = .GetInt32(3)
                End If
                If Not .IsDBNull(4) Then
                    BEObject.MontoComision = .GetDecimal(4)
                End If
                If Not .IsDBNull(5) Then
                    BEObject.Nombre = .GetString(5)
                End If
                If Not .IsDBNull(6) Then
                    BEObject.Rut = .GetString(6)
                End If
                If Not .IsDBNull(7) Then
                    BEObject.Correo = .GetString(7)
                End If
                If Not .IsDBNull(8) Then
                    BEObject.Login = .GetString(8)
                End If
                If Not .IsDBNull(9) Then
                    BEObject.Password = .GetString(9)
                End If
                If Not .IsDBNull(10) Then
                    BEObject.UnidadNegocioId = .GetInt32(10)
                End If
                If Not .IsDBNull(11) Then
                    BEObject.DepartamentoId = .GetInt32(11)
                End If
                If Not .IsDBNull(12) Then
                    BEObject.Telefono = .GetString(12)
                End If
                If Not .IsDBNull(13) Then
                    BEObject.DependenciaIdc = .GetInt32(13)
                End If
                If Not .IsDBNull(14) Then
                    BEObject.FechaReg = .GetDateTime(14)
                End If
                BEObject.FechaActualizacion = .GetDateTime(15)
                BEObject.Estado = .GetInt16(16)
                If Not .IsDBNull(17) Then
                    BEObject.PersonalEstado = .GetInt32(17)
                End If
                If Not .IsDBNull(18) Then
                    BEObject.FechaBloqueo = .GetDateTime(18)
                End If
                If Not .IsDBNull(19) Then
                    BEObject.IntentosFallidos = .GetInt32(19)
                End If
            End With

            Return CType(DirectCast(BEObject, BE.BEntity), T)
        End Function

        ''' <summary>
        ''' Carga las Relaciones de la Collection dada
        ''' </summary>
        ''' <param name="Collection">Lista de objetos para cargar las Relaciones</param>
        ''' <param name="Relaciones">Enumerador de Relaciones a cargar</param>
        ''' <remarks></remarks>
        Protected Sub CargarRelaciones(ByRef Collection As List(Of SEC.Personal), ByVal ParamArray Relaciones() As [Enum])
            'Dim DALPerfil As Perfil
            Dim colPerfil As List(Of SEC.Perfil) = Nothing
            'Dim DALPerfilPersonal As PerfilPersonal
            Dim colPerfilPersonal As List(Of SEC.PerfilPersonal) = Nothing

            'Dim DALPrivilegios As Privilegio
            Dim colPriviliegios As List(Of SEC.Privilegio) = Nothing
            'Dim DALCargo As Cargo
            Dim colCargo As List(Of BEB.Cargo) = Nothing
            'Dim DalUNSucursal As UNSucursal
            Dim colUNSucursal As List(Of BEB.UNSucursal) = Nothing
            'Dim DALPersonal As Personal
            Dim colUser As List(Of SEC.Personal) = Nothing
            Dim colSupervisor As List(Of SEC.Personal) = Nothing
            'Dim colSupervised As List(Of SEC.Personal) = Nothing
            Dim Keys As IEnumerable(Of Int32)

            For Each RelationEnum As [Enum] In Relaciones
                If RelationEnum.Equals(SEC.relPersonal.Personal) Then
                    Using DALPersonal = New Personal(True, CType(MyBase.DBFactory, Object))
                        Keys = (From BEObject In Collection Select BEObject.PersonalId).Distinct
                        colUser = DALPersonal.ReturnChild(Keys, Relaciones)
                    End Using
                End If
                Keys = From BEUser In Collection Select BEUser.Id
                'If RelationEnum.Equals(SEC.relPersonal.Privilegios) Then
                '    DALPrivilegios = New Privilegio(True, CType(MyBase.DBFactory, Object))
                '    Keys = (From BEObject In Collection Select BEObject.PersonalId).Distinct
                '    colPriviliegios = DALPrivilegios.BuscarByPersonal(Keys)
                'End If
                Keys = From BEUser In Collection Select BEUser.CargoId
                If RelationEnum.Equals(SEC.relPersonal.Cargo) Then
                    Using DALCargo = New Cargo(True, CType(MyBase.DBFactory, Object))
                        colCargo = DALCargo.ReturnChild(Keys, Relaciones)
                    End Using
                End If
                If RelationEnum.Equals(SEC.relPersonal.Supervisor) Then
                    Using DALPersonal = New Personal(True, CType(MyBase.DBFactory, Object))
                        Keys = (From BEObject In Collection Where BEObject.SupervisorId > 0 Select BEObject.SupervisorId).Distinct
                        colSupervisor = DALPersonal.ReturnChild(Keys, Relaciones)
                    End Using
                End If
                If RelationEnum.Equals(SEC.relPersonal.UNSucursal) Then
                    Using DalUNSucursal = New UNSucursal(True, CType(MyBase.DBFactory, Object))
                        Keys = (From BEObject In Collection Select BEObject.UnidadNegocioId).Distinct
                        colUNSucursal = DalUNSucursal.ReturnChild(Keys, Relaciones)
                    End Using
                End If
            Next

            If Relaciones.GetLength(0) > 0 Then
                For Each BEUser In Collection
                    If colUser IsNot Nothing Then
                        BEUser.Personal = (From BEObject In colUser
                                           Where BEObject.Id = BEUser.Id
                                           Select BEObject).FirstOrDefault
                    End If
                    If colPriviliegios IsNot Nothing Then
                        Using DALPrivilegios = New Privilegio(True, CType(MyBase.DBFactory, Object))
                            BEUser.CollectionPrivilegios = DALPrivilegios.BuscarByPersonal(BEUser.Id)
                        End Using
                    End If
                    If colCargo IsNot Nothing Then
                        BEUser.Cargo = (From BEObject In colCargo
                                        Where BEObject.Id = BEUser.CargoId
                                        Select BEObject).FirstOrDefault
                    End If
                    If colSupervisor IsNot Nothing Then
                        BEUser.Supervisor = (From BEObject In colSupervisor
                                             Where BEObject.Id = BEUser.SupervisorId
                                             Select BEObject).FirstOrDefault
                    End If
                    If colUNSucursal IsNot Nothing Then
                        BEUser.UNSucursal = (From BEObject In colUNSucursal
                                             Where BEObject.Id = BEUser.UnidadNegocioId
                                             Select BEObject).FirstOrDefault
                    End If
                Next
            End If

            'DALPerfil = Nothing
            'DALPersonal = Nothing
            'DALPerfilPersonal = Nothing
            'DalUNSucursal = Nothing
            'DALCargo = Nothing
        End Sub

        ''' <summary>
        ''' Load Relacioneship of an Object
        ''' </summary>
        ''' <param name="BEUser">Given Object</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <remarks></remarks>
        Protected Sub LoadRelaciones(ByRef BEUser As SEC.Personal, ByVal ParamArray Relaciones() As [Enum])
            'Dim DALPerfilPersonal As PerfilPersonal
            'Dim DALUNSucursal As UNSucursal

            For Each RelationEnum As [Enum] In Relaciones
                If RelationEnum.Equals(SEC.relPersonal.Personal) Then
                    Using DALUser = New Personal(True, CType(MyBase.DBFactory, Object))
                        BEUser.Personal = DALUser.ReturnMaster(BEUser.PersonalId, Relaciones)
                    End Using
                End If
                If RelationEnum.Equals(SEC.relPersonal.UNSucursal) Then
                    Using DALUNSucursal = New UNSucursal(True, CType(MyBase.DBFactory, Object))
                        BEUser.UNSucursal = DALUNSucursal.Search(BEUser.UnidadNegocioId, Relaciones)
                    End Using
                End If
            Next

            'DALPerfilPersonal = Nothing
            'DALUser = Nothing

        End Sub

        Protected Sub LoadRelaciones2(ByRef BEUser As SEC.Personal, ByVal ParamArray Relaciones() As [Enum])
            For Each RelationEnum As [Enum] In Relaciones
                If RelationEnum.Equals(SEC.relPersonal.Personal) Then
                    Using DALUser = New Personal(True, CType(MyBase.DBFactory, Object))
                        BEUser.Personal = DALUser.ReturnMaster(BEUser.PersonalId, Relaciones)
                    End Using
                End If
                If RelationEnum.Equals(SEC.relPersonal.Cargo) Then
                    Using DALCargo = New Cargo(True, CType(MyBase.DBFactory, Object))
                        BEUser.Cargo = DALCargo.ReturnMaster(BEUser.CargoId, Relaciones)
                    End Using
                End If
                If RelationEnum.Equals(SEC.relPersonal.Supervisor) Then
                    Using DALPersonal = New Personal(True, CType(MyBase.DBFactory, Object))
                        BEUser.Supervisor = DALPersonal.ReturnMaster(BEUser.SupervisorId, Relaciones)
                    End Using
                End If
            Next
        End Sub

#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Return an object Collection of User
        ''' </summary>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>A Collection of type User</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function Listar(ByVal Texto As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_perfilpersonal_listarlike"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Texto", DbType.String, Texto)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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
        Public Function Listar(ByVal Nombre As String, ByVal UnidadNegocioPadreId As Int32, ByVal Cargo As String, Login As String, PageIndex As Int32, PageSize As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_lista"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, If(Nombre = "", "%%", String.Concat("%", Nombre, "%")))
                'MyBase.DBFactory.AddInParameter(MyBase.Command, "@Perfil", DbType.String, If(Perfil = "-1", "%%", Perfil))
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Cargo", DbType.String, If(Cargo = "-1", "%%", Cargo))
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioPadreId", DbType.Int32, UnidadNegocioPadreId)
                'MyBase.DBFactory.AddInParameter(MyBase.Command, "@Login", DbType.String, If(Login = "", "%%", String.Concat("%", Login, "%")))
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@PageIndex", DbType.Int32, PageIndex)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@PageSize", DbType.Int32, PageSize)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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
        Public Function Listar(ByVal Nombre As String, ByVal Cargo As String, UnidadNegocioPadreId As Int32) As Int32
            Dim strQuery As String = "crm_seguridad_personal_listacount"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Nombre", DbType.String, If(Nombre = "", "%%", String.Concat("%", Nombre, "%")))
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Cargo", DbType.String, If(Cargo = "-1", "%%", Cargo))
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioPadreId", DbType.Int32, UnidadNegocioPadreId)
                'MyBase.DBFactory.AddInParameter(MyBase.Command, "@Login", DbType.String, If(Login = "", "%%", String.Concat("%", Login, "%")))
                Return CInt(MyBase.DBFactory.ExecuteScalar(MyBase.Command))

                'Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        ''' <summary>
        ''' 	Return an object Collection of User
        ''' </summary>
        ''' <param name="IdPosition">Object Identifier</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>A Collection of type User</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function Listar(ByVal IdPosition As Long, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "SELECT * FROM security_msuser WHERE PositionId = " & IdPosition.ToString & " Order By Name"
            Dim Collection As List(Of SEC.Personal) = MyBase.SQLList(Of SEC.Personal)(strQuery)
            If Collection.Count > 0 Then
                Me.CargarRelaciones(Collection, Relaciones)
            End If
            Return Collection
        End Function

        Public Function ListarPersonalPorCargo(ByVal CargoId As Int32, ByVal Estado As Int16, ByVal CantidadRegistro As Int32, ByVal NumeroPagina As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_listarporcargocobrador"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CargoId", DbType.Int32, CargoId)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, Estado)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CantidadRegistros", DbType.Int32, CantidadRegistro)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@NumeroPagina", DbType.Int32, NumeroPagina)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    CargarRelacionesCartera(Collection, Relations)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        Public Function ListarPersonalSearchNombrePorCargo(ByVal filtro As String, ByVal CargoId As Int32, ByVal Estado As Int16, ByVal CantidadRegistro As Int32, ByVal NumeroPagina As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_Search_listarporcargocobrador"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)

                MyBase.DBFactory.AddInParameter(MyBase.Command, "@text", DbType.String, filtro)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CargoId", DbType.Int32, CargoId)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, Estado)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CantidadRegistros", DbType.Int32, CantidadRegistro)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@NumeroPagina", DbType.Int32, NumeroPagina)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    CargarRelacionesCartera(Collection, Relations)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        Public Function ListarPersonalSearchNombrePorCargoCount(ByVal filtro As String, ByVal CargoId As Int32, ByVal Estado As Int16, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_Search_listarporcargocobrador_count"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)

                MyBase.DBFactory.AddInParameter(MyBase.Command, "@text", DbType.String, filtro)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CargoId", DbType.Int32, CargoId)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, Estado)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    CargarRelacionesCartera(Collection, Relations)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        Public Function ListarPersonalPorCargoCount(ByVal CargoId As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_listarporcargo"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CargoId", DbType.Int32, CargoId)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    CargarRelacionesCartera(Collection, Relations)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        Public Function ListarPersonalPorCargo(ByVal CargoId As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_listarporcargo"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CargoId", DbType.Int32, CargoId)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    CargarRelaciones(Collection, Relaciones)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        Public Function ListarPersonalPorCargoUN(ByVal CargoId As Int32, ByVal UnidadNegocioPadreId As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_listarporcargoun"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CargoId", DbType.Int32, CargoId)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioPadreId", DbType.Int32, UnidadNegocioPadreId)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    CargarRelaciones(Collection, Relaciones)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function ListarPersonalPorCargo2(ByVal CargoId As Int32, ByVal UnidadNegocioPadreId As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_listarporcargo2"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CargoId", DbType.Int32, CargoId)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioPadreId", DbType.Int32, UnidadNegocioPadreId)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    CargarRelaciones(Collection, Relaciones)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function ListarPersonalPorCargoxsala(ByVal CargoId As Int32, ByVal IdSAla As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_listarporcargoxsala"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CargoId", DbType.Int32, CargoId)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@IdSala", DbType.Int32, IdSAla)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    CargarRelaciones(Collection, Relaciones)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        Private Sub CargarRelacionesCartera(collection As List(Of SEC.Personal), relaciones() As [Enum])
            'Dim DALCartera As DALayer.Cobranzas.CarteraCobranza
            'Dim colCartera As List(Of BE.Cobranzas.CarteraCobranza) = Nothing
            'Dim Key As IEnumerable(Of Int32)
            'For Each RelacionEnum As [Enum] In relaciones
            '    Key = From BCobrador In collection Select BCobrador.Id
            '    If RelacionEnum.Equals(BE.Seguridad.relPersonal.CarteraCobranza) Then
            '        DALCartera = New DALayer.Cobranzas.CarteraCobranza(True, CType(MyBase.DBFactory, Object))
            '        colCartera = DALCartera.ReturnChild(Key, relaciones)
            '    End If
            'Next
            'If relaciones.GetLength(0) > 0 Then
            '    For Each BECobrador In collection
            '        If colCartera IsNot Nothing Then
            '            BECobrador.CollectionCartera = (From BEObject In colCartera
            '                                            Where BEObject.CobradorId = BECobrador.Id
            '                                            Select BEObject).ToList
            '        End If
            '    Next
            'End If

            'DALCartera = Nothing
        End Sub

        Public Function ListarPersonalOperadorTmk(ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_listarOperadorTmk"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Teleoperador", DbType.Int32, BEntities.CargoEnSistema.OPERADOR)
                'MyBase.DBFactory.AddInParameter(MyBase.Command, "@EjecutivoTeleMkt", DbType.Int32, BEntities.CargoEnSistema.Tesoreria)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function '
        Public Function ListarPersonalPorGrupoCargo(ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_listarporgrupodecargos"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function ListarPersonalPorGrupoCargoxsala(ByVal Idsala As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "[crm_seguridad_personal_listarporgrupodecargosxsala]"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, Idsala)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function ListarCompleto(ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_todos"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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
        Public Function ListarCompletoUN(ByVal UnidadNegocioPadreId As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_todos_UN"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioPadreId", DbType.Int32, UnidadNegocioPadreId)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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
        Public Function ListarCompletoConTransaccion(ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_todos"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command, MyBase.Transaction))
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
        Public Function ListarCloser(ByVal Idsala As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_listarporliscloser"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@IdSala", DbType.String, Idsala)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@GerenteComercial", DbType.Int32, BEntities.CargoEnSistema.GerenteComercial)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@GerenteSala", DbType.Int32, BEntities.CargoEnSistema.GerenteSala)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Close", DbType.Int32, BEntities.CargoEnSistema.CLOSER)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Linner", DbType.Int32, BEntities.CargoEnSistema.LINER)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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
        Public Function ListByDepartamento(ByVal Text As String, ByVal DepartamentoId As String, ByVal Estado As Int16, ByVal CantidadRegistro As Int32, ByVal NumeroPagina As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_listbydepartamento"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)

                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Text", DbType.String, Text)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@DepartamentoId", DbType.String, If(DepartamentoId = "-1", "%%", DepartamentoId))
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, Estado)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CantidadRegistros", DbType.Int32, CantidadRegistro)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@NumeroPagina", DbType.Int32, NumeroPagina)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    CargarRelacionesCartera(Collection, Relations)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function ListByDepartamentoCount(ByVal Text As String, ByVal DepartamentoId As String, ByVal Estado As Int16, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_listbydepartamentoCount"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Text", DbType.String, Text)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@DepartamentoId", DbType.String, If(DepartamentoId = "-1", "%%", DepartamentoId))
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, Estado)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection.Count > 0 Then
                    CargarRelacionesCartera(Collection, Relations)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function ListarNoAsignados(ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_listnoasignado"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                'If Collection.Count > 0 Then
                '    Me.CargarRelaciones(Collection, Relaciones)
                'End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function ListarNoAsignados2(text As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_listnoasignado2"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@text", DbType.String, text)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                'If Collection.Count > 0 Then
                '    Me.CargarRelaciones(Collection, Relaciones)
                'End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function ListarNoAsignados3(text As String, SalaId As Int32, ByVal UnidadNegocioPadreId As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_listnoasignado3"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@text", DbType.String, text)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@SalaId", DbType.Int32, SalaId)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioPadreId", DbType.Int32, UnidadNegocioPadreId)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                'If Collection.Count > 0 Then
                '    Me.CargarRelaciones(Collection, Relaciones)
                'End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function ListarNoAsignadosOperador(ByVal Text As String, ByVal CargoId As Int32, ByVal UnidadNegocioPadreId As Int32, ByVal Estado As Int16, ByVal CantidadRegistro As Int32, ByVal NumeroPagina As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_listnoasignadoOperador"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@text", DbType.String, Text)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CargoId", DbType.Int32, CargoId)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioPadreId", DbType.Int32, UnidadNegocioPadreId)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, Estado)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CantidadRegistros", DbType.Int32, CantidadRegistro)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@NumeroPagina", DbType.Int32, NumeroPagina)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function ListarNoAsignadosOperadorCount(ByVal Text As String, ByVal CargoId As Int32, ByVal UnidadNegocioPadreId As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "crm_seguridad_personal_listnoasignadoOperadorCount"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@text", DbType.String, Text)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CargoId", DbType.Int32, CargoId)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@UnidadNegocioPadreId", DbType.Int32, UnidadNegocioPadreId)
                Dim Collection As List(Of SEC.Personal) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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
        ''' 	Search an object of type User    	''' </summary>
        ''' <param name="Id">Object identifier User</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>An object of type User</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function Buscar(ByVal Id As Int32, ByVal ParamArray Relaciones() As [Enum]) As SEC.Personal
            Dim strQuery As String = "crm_seguridad_personal_buscarporid"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, Id)
                Dim Collection As SEC.Personal = MyBase.SQLConvertidorIDataReader(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection IsNot Nothing Then
                    Me.LoadRelaciones2(Collection, Relaciones)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function BuscarCobradorporClienteId(ByVal ClienteId As Int32, ByVal ParamArray Relaciones() As [Enum]) As SEC.Personal
            Dim strQuery As String = "crm_seguridad_personal_buscarcobradorporclienteid"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Cliente", DbType.Int32, ClienteId)
                Dim Collection As SEC.Personal = MyBase.SQLConvertidorIDataReader(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection IsNot Nothing Then
                    Me.LoadRelaciones2(Collection, Relaciones)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function


        Public Function Buscar2(ByVal Id As Long, ByVal ParamArray Relaciones() As [Enum]) As SEC.Personal
            Dim strQuery As String = "SELECT * FROM security_msuser WHERE Id = " & Id.ToString
            Dim BEUser As SEC.Personal = MyBase.SQLSearch(Of SEC.Personal)(strQuery)

            If BEUser IsNot Nothing Then
                Me.LoadRelaciones2(BEUser, Relaciones)
            End If
            Return BEUser
        End Function


        Public Function FindByProfileId(ByVal PerfilId As Long, ByVal ParamArray Relaciones() As [Enum]) As SEC.Personal
            Dim strQuery As String = "SELECT * FROM security_msuser WHERE ProfileId = " & PerfilId
            Dim BEUser As SEC.Personal = MyBase.SQLSearch(Of SEC.Personal)(strQuery)

            If BEUser IsNot Nothing Then
                Me.LoadRelaciones2(BEUser, Relaciones)
            End If
            Return BEUser
        End Function

        Public Function FindByPerfilId(ByVal PerfilId As Long, ByVal ParamArray Relaciones() As [Enum]) As SEC.Personal
            Dim strQuery As String = "SELECT * FROM security_msuser WHERE PerfilId = " & PerfilId
            Dim BEUser As SEC.Personal = MyBase.SQLSearch(Of SEC.Personal)(strQuery)

            If BEUser IsNot Nothing Then
                Me.LoadRelaciones2(BEUser, Relaciones)
            End If
            Return BEUser
        End Function

#End Region

#Region " Methods "
        Public Shadows Function Count() As Int32

            Dim strQuery As String = "crm_seguridad_personal_contador"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)

                Return CInt(MyBase.DBFactory.ExecuteScalar(MyBase.Command))
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try

        End Function

#End Region


#Region " List Methods "

        Public Function ListByProfile(ByVal IdProfile As Integer, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "SELECT * FROM security_msuser WHERE ProfileId = " & IdProfile & " AND Hidden = 0 Order By Name"
            Dim Collection As List(Of SEC.Personal) = MyBase.SQLList(Of SEC.Personal)(strQuery)

            If Collection.Count > 0 Then
                Me.CargarRelaciones(Collection, Relaciones)
            End If
            Return Collection
        End Function
        Public Function ListByProfileAll(ByVal IdProfile As Integer, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "SELECT * FROM security_msuser WHERE ProfileId = " & IdProfile & " Order By Name"
            Dim Collection As List(Of SEC.Personal) = MyBase.SQLList(Of SEC.Personal)(strQuery)

            If Collection.Count > 0 Then
                Me.CargarRelaciones(Collection, Relaciones)
            End If
            Return Collection
        End Function
        Public Function ListByProfileForSupervisor(ByVal IdSupervisor As Integer, ByVal IdProfile As Integer, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "SELECT * FROM security_msuser WHERE ProfileId = " & IdProfile & " AND SupervisorId = " & IdSupervisor & " AND Hidden = 0 Order By Name"
            Dim Collection As List(Of SEC.Personal) = MyBase.SQLList(Of SEC.Personal)(strQuery)

            If Collection.Count > 0 Then
                Me.CargarRelaciones(Collection, Relaciones)
            End If
            Return Collection
        End Function

        Public Function Listar(ByVal PositionId As Long, OrderBy As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "SELECT * FROM security_msuser WHERE PostionId = " & PositionId.ToString & " Order By " & OrderBy
            Dim Collection As List(Of SEC.Personal) = MyBase.SQLList(Of SEC.Personal)(strQuery)

            If Collection.Count > 0 Then
                Me.CargarRelaciones(Collection, Relaciones)
            End If
            Return Collection
        End Function


        Public Function PagedList(ByVal Order As String, PageIndex As Integer, PageSize As Integer, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Personal)
            Dim strQuery As String = "SELECT * FROM security_msuser  Order By " & Order
            Dim Collection As List(Of SEC.Personal) = MyBase.PaggingSQLList(Of SEC.Personal)(strQuery, PageIndex, PageSize)

            If Collection.Count > 0 Then
                Me.CargarRelaciones(Collection, Relaciones)
            End If
            Return Collection
        End Function
#End Region

#Region " Search Methods "
        ''' <summary>
        ''' 	Search an object of type User    	''' </summary>
        ''' <param name="Login">Object identifier User</param>
        ''' <param name="Password">Relacioneship enumerator</param>
        ''' <returns>An object of type User</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function ValidateUser(ByVal Login As String, ByVal Password As String) As SEC.Personal
            Dim strQuery As String = "SELECT * FROM crm_seguridad_personal WHERE Login = '" & Login.Trim & "' AND Password = HASHBYTES('SHA','" & Password.Trim & "') AND Estado = 1"
            Dim BEUser As SEC.Personal = MyBase.SQLSearch(Of SEC.Personal)(strQuery)

            If BEUser IsNot Nothing Then
                Me.LoadRelaciones(BEUser)
            End If
            Return BEUser
        End Function

        Public Function ValidarPersonal(ByVal Login As String, ByVal Password As String, ByVal Estado As Int32, IP As String, host As String) As SEC.Personal
            Dim strQuery As String = "crm_seguridad_personal_validarpersonal"
            Using DALBitacora = New DALayer.Bitacora.BitacoraGeneral()
                DALBitacora.OpenConnection()

                Dim beBitacora As BEntities.Bitacora.BitacoraGeneral = New BEntities.Bitacora.BitacoraGeneral
                Try
                    MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Login", DbType.String, Login)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Password", DbType.String, Password)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Estado", DbType.Int16, Estado)
                    Dim Collection As SEC.Personal = MyBase.SQLConvertidorIDataReader(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                    If Collection IsNot Nothing Then
                        Using DALObject As New DALayer.Base.UNSucursal
                            Collection.UNSucursal = DALObject.Search(Collection.UnidadNegocioId)
                        End Using
                        Dim strHostName As String = host
                        'Dim ipEntry As IPHostEntry = Dns.GetHostEntry(strHostName)
                        Dim IPLogin As String = IP

                        With beBitacora
                            '.Id = 0
                            .Procedimiento = strQuery
                            .Accion = "LOGIN"
                            .Campos = "Login=" & Collection.Login & "-.-Password=" & Collection.Password
                            .PersonalId = Collection.Id
                            .IpCliente = IPLogin
                            .MaquinaCliente = strHostName
                            .StatusType = BE.StatusType.Insert
                            .FechaReg = Now
                            .TipoTransaccionIdc = BE.TipoTransaccionBitacora.Exitosa
                            .TransaccionId = Collection.Id
                        End With
                        DALBitacora.Save(beBitacora)
                        DALBitacora.Commit()
                    Else
                        Dim strHostName As String = host
                        'Dim ipEntry As IPHostEntry = Dns.GetHostEntry(strHostName)
                        Dim IPLogin As String = IP

                        With beBitacora
                            '.Id = 0
                            .Procedimiento = strQuery
                            .Accion = "LOGIN"
                            .Campos = "Login=" & Login & "-.-Password=" & passwordEncryptSHA(Password)
                            .PersonalId = 0
                            .IpCliente = IPLogin
                            .MaquinaCliente = strHostName
                            .StatusType = BE.StatusType.Insert
                            .FechaReg = Now
                            .TipoTransaccionIdc = BE.TipoTransaccionBitacora.Fallida
                            .TransaccionId = 0
                        End With
                        DALBitacora.Save(beBitacora)
                        DALBitacora.Commit()
                    End If

                    Return Collection
                Catch ex As Exception
                    beBitacora.TipoTransaccionIdc = BE.TipoTransaccionBitacora.Fallida
                    DALBitacora.Save(beBitacora)
                    MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                    Return Nothing
                Finally
                    DALBitacora.CloseConnection()
                    MyBase.DisposeConnection()
                    MyBase.DisposeCommand()
                End Try
            End Using
        End Function
        Public Function ValidateUser(ByVal Login As String, ByVal Password As String, ByVal Estado As Int32) As SEC.Personal
            Dim strQuery As String = "crm_seguridad_personal_validarpersonal('" & Login.Trim & "', HASHBYTES('SHA','" & Password.Trim & "'), Estado = " & Estado & ")"
            Dim BEUser As SEC.Personal = MyBase.SQLSearch(Of SEC.Personal)(strQuery)

            If BEUser IsNot Nothing Then
                Me.LoadRelaciones(BEUser)
            End If
            Return BEUser
        End Function

        ''' <summary>
        ''' 	Search an object of type User    	''' </summary>
        ''' <param name="EmailNameLogin">Object identifier User</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>An object of type User</returns>
        ''' <remarks>
        ''' </remarks>  
        Public Function Buscar(ByVal Login As String, ByVal ParamArray Relaciones() As [Enum]) As SEC.Personal
            Dim strQuery As String = "crm_seguridad_personal_buscarporlogin"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Login", DbType.String, Login)
                Dim Collection As SEC.Personal = MyBase.SQLConvertidorIDataReader(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        Public Function passwordEncryptSHA(ByVal password As String) As String
            Dim sha As New SHA1CryptoServiceProvider ' declare sha as a new SHA1CryptoServiceProvider
            Dim bytesToHash() As Byte ' and here is a byte variable

            bytesToHash = System.Text.Encoding.ASCII.GetBytes(password) ' covert the password into ASCII code

            bytesToHash = sha.ComputeHash(bytesToHash) ' this is where the magic starts and the encryption begins

            Dim encPassword As String = ""

            For Each b As Byte In bytesToHash
                encPassword += b.ToString("x2")
            Next

            Return encPassword ' boom there goes the encrypted password!

        End Function

        Public Function BuscarRut(ByVal Rut As String, ByVal ParamArray Relaciones() As [Enum]) As SEC.Personal
            Dim strQuery As String = "crm_seguridad_personal_buscarporrut"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Rut", DbType.String, Rut)
                Dim Collection As SEC.Personal = MyBase.SQLConvertidorIDataReader(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function BuscarPorId(ByVal id As Int32, ByVal ParamArray Relaciones() As [Enum]) As SEC.Personal
            Dim strQuery As String = "crm_seguridad_personal_buscarporid"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, id)
                Dim Collection As SEC.Personal = MyBase.SQLConvertidorIDataReader(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Using DALPrivilegios As New Privilegio(True, CType(MyBase.DBFactory, Object))
                    Collection.CollectionPrivilegios = DALPrivilegios.BuscarByPersonal(Collection.Id)
                End Using
                If Collection IsNot Nothing Then
                    Me.LoadRelaciones(Collection, Relaciones)
                End If
                'Dim DALPrivilegios As Privilegio
                'DALPrivilegios = New Privilegio(True, CType(MyBase.DBFactory, Object))
                'Collection.CollectionPrivilegios = DALPrivilegios.BuscarByPersonal(Collection.Id)
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function BuscarPorIdCargo(ByVal idCargo As Int32, ByVal ParamArray Relaciones() As [Enum]) As SEC.Personal
            Dim strQuery As String = "crm_seguridad_personal_buscarporcargo"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@IdCargo", DbType.Int32, idCargo)
                Dim Collection As SEC.Personal = MyBase.SQLConvertidorIDataReader(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection IsNot Nothing Then
                    Me.LoadRelaciones2(Collection, Relaciones)
                End If
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function
        Public Function BuscarPorIdCargoYSala(ByVal idCargo As Int32, ByVal SalaId As Int32, ByVal ParamArray Relaciones() As [Enum]) As SEC.Personal
            Dim strQuery As String = "crm_seguridad_personal_buscarporcargoysala"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@IdCargo", DbType.Int32, idCargo)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@SalaId", DbType.Int32, SalaId)
                Dim Collection As SEC.Personal = MyBase.SQLConvertidorIDataReader(Of SEC.Personal)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                If Collection IsNot Nothing Then
                    Me.LoadRelaciones2(Collection, Relaciones)
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