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
    Public Class ParametroLogin
        Inherits DALEntity

#Region " Save Methods"

        ''' <summary>
        ''' 	Saves business information object of type Profile
        ''' </summary>
        ''' <param name="BEObj">Business object of type Profile </param>
        ''' <remarks>
        ''' </remarks>
        Public Sub Guardar(ByRef BEObj As SEC.ParametroLogin)
            Dim strQuery As String = ""
            If BEObj.StatusType = BE.StatusType.Insert Then
                strQuery = "INSERT INTO crm_seguridad_parametrologin " &
                    "VALUES(@LongitudMinima,@LongitudMaxima,@NivelPassword,@Expiracion,@CantidadIntentosFallidos,@TiempoBloqueo,@ComprobacionesPasswordAnteriores,@PersonalId
           ,@PersonalModificacionId,@FechaRegistro,@FechaModificacion,@Estado); SELECT @@IDENTITY"
            ElseIf BEObj.StatusType = BE.StatusType.Update Then
                strQuery = "UPDATE crm_seguridad_parametrologin SET " &
                    "LongitudMinima=@LongitudMinima,LongitudMaxima=@LongitudMaxima,NivelPassword=@NivelPassword,Expiracion=@Expiracion,CantidadIntentosFallidos=@CantidadIntentosFallidos,
                    TiempoBloqueo=@TiempoBloqueo,ComprobacionesPasswordAnteriores=@ComprobacionesPasswordAnteriores,PersonalId=@PersonalId
           ,PersonalModificacionId=@PersonalModificacionId,FechaRegistro=@FechaRegistro,FechaModificacion=@FechaModificacion,Estado=@Estado " &
                    "WHERE Id = @Id"
            ElseIf BEObj.StatusType = BE.StatusType.Delete Then
                strQuery = "UPDATE crm_seguridad_parametrologin SET " &
                    "LongitudMinima=@LongitudMinima,LongitudMaxima=@LongitudMaxima,NivelPassword=@NivelPassword,Expiracion=@Expiracion,CantidadIntentosFallidos=@CantidadIntentosFallidos,
                    TiempoBloqueo=@TiempoBloqueo,ComprobacionesPasswordAnteriores=@ComprobacionesPasswordAnteriores,PersonalId=@PersonalId
           ,PersonalModificacionId=@PersonalModificacionId,FechaRegistro=@FechaRegistro,FechaModificacion=@FechaModificacion,Estado=@Estado " &
                    "WHERE Id = @Id"
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
                    If BEObj.StatusType <> BE.StatusType.Insert Then
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEObj.Id)
                    End If
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@LongitudMinima", DbType.Int32, BEObj.LongitudMinima)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@LongitudMaxima", DbType.Int32, BEObj.LongitudMaxima)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@NivelPassword", DbType.Int32, BEObj.NivelPassword)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Expiracion", DbType.Int32, BEObj.Expiracion)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@CantidadIntentosFallidos", DbType.Int32, BEObj.CantidadIntentosFallidos)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@TiempoBloqueo", DbType.Int32, BEObj.TiempoBloqueo)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@ComprobacionesPasswordAnteriores", DbType.Int32, BEObj.ComprobacionesPasswordAnteriores)
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

        Public Sub Guardar(ByRef BEObj2 As List(Of SEC.ParametroLogin))
            Dim strQuery As String = ""

            For Each BEObj In BEObj2
                Dim DALBitacora = New DALayer.Bitacora.BitacoraGeneral()
                DALBitacora.OpenConnection()

                If BEObj.StatusType = BE.StatusType.Insert Then
                        strQuery = "INSERT INTO crm_seguridad_parametrologin " &
                "VALUES(@LongitudMinima,@LongitudMaxima,@NivelPassword,@Expiracion,@CantidadIntentosFallidos,@TiempoBloqueo,@ComprobacionesPasswordAnteriores,@PersonalId
           ,@PersonalModificacionId,@FechaRegistro,@FechaModificacion,@Estado); SELECT @@IDENTITY"
                    ElseIf BEObj.StatusType = BE.StatusType.Update Then
                        strQuery = "UPDATE crm_seguridad_parametrologin SET " &
                "LongitudMinima=@LongitudMinima,LongitudMaxima=@LongitudMaxima,NivelPassword=@NivelPassword,Expiracion=@Expiracion,CantidadIntentosFallidos=@CantidadIntentosFallidos,
                    TiempoBloqueo=@TiempoBloqueo,ComprobacionesPasswordAnteriores=@ComprobacionesPasswordAnteriores,PersonalId=@PersonalId
           ,PersonalModificacionId=@PersonalModificacionId,FechaRegistro=@FechaRegistro,FechaModificacion=@FechaModificacion,Estado=@Estado " &
                "WHERE Id = @Id"
                    ElseIf BEObj.StatusType = BE.StatusType.Delete Then
                        strQuery = "UPDATE crm_seguridad_parametrologin SET " &
                "LongitudMinima=@LongitudMinima,LongitudMaxima=@LongitudMaxima,NivelPassword=@NivelPassword,Expiracion=@Expiracion,CantidadIntentosFallidos=@CantidadIntentosFallidos,
                    TiempoBloqueo=@TiempoBloqueo,ComprobacionesPasswordAnteriores=@ComprobacionesPasswordAnteriores,PersonalId=@PersonalId
           ,PersonalModificacionId=@PersonalModificacionId,FechaRegistro=@FechaRegistro,FechaModificacion=@FechaModificacion,Estado=@Estado " &
                "WHERE Id = @Id"
                    End If
                Dim beBitacora As BEntities.Bitacora.BitacoraGeneral = New BEntities.Bitacora.BitacoraGeneral
                Try
                    With beBitacora
                        .Id = 0
                        .Procedimiento = strQuery
                        .Accion = If(BEObj.StatusType = BEntities.StatusType.Insert, "INSERT", If(BEObj.StatusType = BEntities.StatusType.Update, "UPDATE", "DELETE"))
                        .PersonalId = BEObj.PersonalId
                        .StatusType = BEntities.StatusType.Insert
                        .FechaReg = Now
                        .TipoTransaccionIdc = BEntities.TipoTransaccionBitacora.Exitosa
                    End With

                    If BEObj.StatusType <> BE.StatusType.NoAction Then
                        MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery)
                        If BEObj.StatusType <> BE.StatusType.Insert Then
                            MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEObj.Id)
                        End If
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@LongitudMinima", DbType.Int32, BEObj.LongitudMinima)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@LongitudMaxima", DbType.Int32, BEObj.LongitudMaxima)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@NivelPassword", DbType.Int32, BEObj.NivelPassword)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Expiracion", DbType.Int32, BEObj.Expiracion)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@CantidadIntentosFallidos", DbType.Int32, BEObj.CantidadIntentosFallidos)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@TiempoBloqueo", DbType.Int32, BEObj.TiempoBloqueo)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@ComprobacionesPasswordAnteriores", DbType.Int32, BEObj.ComprobacionesPasswordAnteriores)
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

#End Region

#Region " Methods "

        ''' <summary>
        ''' 	For use on data access layer at assembly level, return an  Profile type object
        ''' </summary>
        ''' <param name="Id">Object Identifier Profile</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>An object of type Profile</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function ReturnMaster(ByVal Id As Long, ByVal ParamArray Relaciones() As [Enum]) As SEC.ParametroLogin
            Return Buscar(Id, Relaciones)
        End Function

        ''' <summary>
        ''' 	Devuelve un objeto Profile de tipo uno a uno con otro objeto
        ''' </summary>
        ''' <param name="Keys">Los identificadores de los objetos relacionados a Profile</param>
        ''' <param name="Relaciones">Enumerador de Relaciones a retorar</param>
        ''' <returns>Un Objeto de tipo Profile</returns>
        ''' <remarks>
        ''' </remarks>
        ''' 'aqui apuntaba antes no al de abajo
        Friend Function ReturnChild(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.ParametroLogin)
            Dim strQuery As String = "SELECT * FROM crm_seguridad_parametrologin WHERE UserId IN " & MyBase.KeysArray(Keys)
            Dim Coleccion As List(Of SEC.ParametroLogin) = Nothing
            Coleccion = MyBase.SQLList(Of SEC.ParametroLogin)(strQuery)
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
            Dim BEObject As New SEC.ParametroLogin

            With DR
                BEObject.Id = .GetInt32(0)
                BEObject.LongitudMinima = .GetInt32(1)
                BEObject.LongitudMaxima = .GetInt32(2)
                BEObject.NivelPassword = .GetInt32(3)
                BEObject.Expiracion = .GetInt32(4)
                BEObject.CantidadIntentosFallidos = .GetInt32(5)
                BEObject.TiempoBloqueo = .GetInt32(6)
                BEObject.ComprobacionesPasswordAnteriores = .GetInt32(7)
                BEObject.PersonalId = .GetInt32(8)
                BEObject.PersonalModificacionId = .GetInt32(9)
                BEObject.FechaRegistro = .GetDateTime(10)
                BEObject.FechaModificacion = .GetDateTime(11)
                BEObject.Estado = .GetInt16(12)
            End With

            Return CType(DirectCast(BEObject, BE.BEntity), T)
        End Function

        ''' <summary>
        ''' Carga las Relaciones de la Coleccion dada
        ''' </summary>
        ''' <param name="Coleccion">Lista de objetos para cargar las Relaciones</param>
        ''' <param name="Relaciones">Enumerador de Relaciones a cargar</param>
        ''' <remarks></remarks>
        Protected Sub CargarRelaciones(ByRef Coleccion As List(Of SEC.ParametroLogin), ByVal ParamArray Relaciones() As [Enum])
            Dim DALClasificadores As DALayer.Base.Clasificadores
            Dim colClasificadores As List(Of BE.Base.Clasificadores) = Nothing
            Dim DALPersonal As Personal
            Dim colPersonal As List(Of SEC.Personal) = Nothing
            Dim Keys As IEnumerable(Of Int32) = Nothing

            For Each RelationEnum As [Enum] In Relaciones
                '    If RelationEnum.Equals(SEC.relPerfil.Personal) Then
                '        DALPersonal = New Personal(True, CType(MyBase.DBFactory, Object))
                '        Keys = (From BEObject In Coleccion Select BEObject.PersonalId).Distinct
                '        colPersonal = DALPersonal.ReturnChild(Keys, Relaciones)
                '    End If

                '    Keys = From BEProfile In Coleccion Select BEProfile.Id
                If RelationEnum.Equals(SEC.relParametroLogin.NivelPassword) Then
                    DALClasificadores = New Base.Clasificadores(True, CType(MyBase.DBFactory, Object))
                    Keys = (From BEObject In Coleccion Select BEObject.NivelPassword).Distinct
                    colClasificadores = DALClasificadores.ReturnChild(Keys, Relaciones)
                End If

            Next

            If Relaciones.GetLength(0) > 0 Then
                For Each BEProfile In Coleccion

                    If colClasificadores IsNot Nothing Then

                        BEProfile.Nivel = (From BEObject In colClasificadores Where BEObject.Id = BEProfile.NivelPassword
                                           Select BEObject).FirstOrDefault
                    End If
                Next
            End If

            DALClasificadores = Nothing
            DALPersonal = Nothing
        End Sub

        ''' <summary>
        ''' Load Relacioneship of an Object
        ''' </summary>
        ''' <param name="BEProfile">Given Object</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <remarks></remarks>
        Protected Sub CargarRelaciones(ByRef BEProfile As SEC.ParametroLogin, ByVal ParamArray Relaciones() As [Enum])
            Dim DALFormulario As Formulario
            Dim DALPersonal As Personal

            'For Each RelationEnum As [Enum] In Relaciones
            '    If RelationEnum.Equals(SEC.relPerfil.Personal) Then
            '        DALPersonal = New Personal(True, CType(MyBase.DBFactory, Object))
            '        BEProfile.Personal = DALPersonal.ReturnMaster(BEProfile.PersonalId, Relaciones)
            '    End If

            '    Dim Keys() As Int32 = {BEProfile.Id}
            '    If RelationEnum.Equals(SEC.relPerfil.Formulario) Then
            '        DALFormulario = New Formulario(True, CType(MyBase.DBFactory, Object))
            '        BEProfile.ColeccionFormulario = DALFormulario.ReturnChildOption(Keys, Relaciones)

            '    End If
            'Next

            DALFormulario = Nothing
            DALPersonal = Nothing
        End Sub

#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Return an object Coleccion of Profile
        ''' </summary>
        ''' <param name="Order">Object order property column </param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>A Coleccion of type Profile</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function Listar(ByVal Order As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.ParametroLogin)
            Dim strQuery As String = "SELECT * FROM crm_seguridad_parametrologin where Estado = 1 ORDER By " & Order
            Dim Coleccion As List(Of SEC.ParametroLogin) = MyBase.SQLList(Of SEC.ParametroLogin)(strQuery)

            If Coleccion.Count > 0 Then
                Me.CargarRelaciones(Coleccion, Relaciones)
            End If
            Return Coleccion
        End Function


        Public Function Listar(ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.ParametroLogin)
            Dim strQuery As String = "SELECT * FROM crm_seguridad_parametrologin where Estado = 1"
            Dim Coleccion As List(Of SEC.ParametroLogin) = MyBase.SQLList(Of SEC.ParametroLogin)(strQuery)

            If Coleccion.Count > 0 Then
                Me.CargarRelaciones(Coleccion, Relaciones)
            End If
            Return Coleccion
            'Try
            '    MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
            '    Dim Collection As List(Of SEC.ParametroLogin) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.ParametroLogin)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
            '    Return Collection
            'Catch ex As Exception
            '    MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
            '    Return Nothing
            'Finally
            'End Try
        End Function
        Public Function ListarTodo(ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.ParametroLogin)
            Dim strQuery As String = "SELECT * FROM crm_seguridad_parametrologin order by Id desc"
            Dim Coleccion As List(Of SEC.ParametroLogin) = MyBase.SQLList(Of SEC.ParametroLogin)(strQuery)

            If Coleccion.Count > 0 Then
                Me.CargarRelaciones(Coleccion, Relaciones)
            End If
            Return Coleccion
            'Try
            '    MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
            '    Dim Collection As List(Of SEC.ParametroLogin) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.ParametroLogin)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
            '    Return Collection
            'Catch ex As Exception
            '    MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
            '    Return Nothing
            'Finally
            'End Try
        End Function

        Public Function PagedFilterList(PageIndex As Integer, PageSize As Integer, OrderBy As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.ParametroLogin)
            Dim strQuery As String = "SELECT * FROM crm_seguridad_parametrologin where Estado = 1 Order By " & OrderBy
            Dim Coleccion As List(Of SEC.ParametroLogin) = MyBase.PaggingSQLList(Of SEC.ParametroLogin)(strQuery, PageIndex, PageSize)

            If Coleccion.Count > 0 Then
                Me.CargarRelaciones(Coleccion, Relaciones)
            End If
            Return Coleccion
        End Function

        Public Shadows Function Count() As Long
            Dim strQuery As String = "SELECT COUNT(*) FROM crm_seguridad_parametrologin where Estado = 1"

            Return CLng(MyBase.Value(strQuery))
        End Function


        ''' <summary>
        ''' 	Return an object Coleccion of Profile
        ''' </summary>
        ''' <param name="IdPadre">Object Identifier</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>A Coleccion of type Profile</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function Listar(ByVal IdPadre As Long, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.ParametroLogin)
            Dim strQuery As String = "SELECT * FROM crm_seguridad_parametrologin WHERE IdPadre = " & IdPadre.ToString
            Dim Coleccion As List(Of SEC.ParametroLogin) = MyBase.SQLList(Of SEC.ParametroLogin)(strQuery)

            If Coleccion.Count > 0 Then
                Me.CargarRelaciones(Coleccion, Relaciones)
            End If
            Return Coleccion
        End Function

#End Region

#Region " Search Methods "

        ''' <summary>
        ''' 	Search an object of type Profile    	''' </summary>
        ''' <param name="Id">Object identifier Profile</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>An object of type Profile</returns>
        ''' <remarks>
        ''' </remarks>
        Public Function Buscar(ByVal Id As Long, ByVal ParamArray Relaciones() As [Enum]) As SEC.ParametroLogin
            Dim strQuery As String = "SELECT * FROM crm_seguridad_parametrologin WHERE Id = " & Id.ToString
            Dim BEProfile As SEC.ParametroLogin = MyBase.SQLSearch(Of SEC.ParametroLogin)(strQuery)

            If BEProfile IsNot Nothing Then
                Me.CargarRelaciones(BEProfile, Relaciones)
            End If
            Return BEProfile
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