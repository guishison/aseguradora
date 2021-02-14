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
    Public Class PersonalSupervisor
        Inherits DALEntity

#Region " Save Methods"

        ''' <summary>
        ''' 	Saves business information object of type Profile
        ''' </summary>
        ''' <param name="BEObj">Business object of type Profile </param>
        ''' <remarks>
        ''' </remarks>
        Public Sub Guardar(ByRef BEObj As SEC.PersonalSupervisor)

            Dim strQuery As String = ""
            If BEObj.StatusType = BE.StatusType.Insert Then
                strQuery = "crm_seguridad_personalsupervisor_insert"
            ElseIf BEObj.StatusType = BE.StatusType.Update Then
                strQuery = "UPDATE crm_seguridad_personalsupervisor SET " &
                    "PersonalId = @PersonalId,SupervisorId = @SupervisorId,FechaActualizacion = @FechaActualizacion,estado = @estado " &
                        "WHERE Id = @Id"
            ElseIf BEObj.StatusType = BE.StatusType.Delete Then
                strQuery = "DELETE FROM crm_seguridad_personalsupervisor WHERE Id = @Id"
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
                    'MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery)
                    If (BEObj.StatusType <> BE.StatusType.Insert) Then
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEObj.Id)
                    End If
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEObj.PersonalId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@SupervisorId", DbType.Int32, BEObj.SupervisorId)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalIdL", DbType.Int32, BEObj.PersonalIdL)
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

        ''' <summary>
        ''' Saves Option Relacioneship n:n
        ''' </summary>
        ''' <param name="ColeccionFormulario"></param>
        ''' <param name="IdMaster"></param>
        ''' <remarks></remarks>
        Public Sub GuardarFormulario(ByRef ColeccionFormulario As List(Of SEC.Formulario), ByVal IdMaster As Long)
            Dim strQuery As String = "DELETE FROM security_psprofileoption WHERE ProfileId = " & IdMaster.ToString
            Try

                MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery)
                MyBase.DBFactory.ExecuteNonQuery(MyBase.Command, MyBase.Transaction)

                For Each BEObj In ColeccionFormulario
                    If BEObj.StatusType <> BE.StatusType.NoAction And BEObj.StatusType <> BE.StatusType.Delete Then
                        strQuery = "INSERT INTO security_psprofileoption Values(@OptionId,@ProfileId)"
                        MyBase.Command = MyBase.DBFactory.GetSqlStringCommand(strQuery)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@ProfileId", DbType.Int32, IdMaster)
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@OptionId", DbType.Int32, BEObj.Id)
                        MyBase.DBFactory.ExecuteNonQuery(MyBase.Command, MyBase.Transaction)
                    End If
                Next
            Catch ex As Exception
                Throw ex
            Finally
                MyBase.DisposeCommand()
            End Try
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
        Friend Function ReturnMaster(ByVal Id As Long, ByVal ParamArray Relaciones() As [Enum]) As SEC.PersonalSupervisor
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
        Friend Function ReturnChild(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.PersonalSupervisor)
            Dim strQuery As String = "SELECT * FROM crm_seguridad_personalsupervisor WHERE PersonalId IN " & MyBase.KeysArray(Keys)
            Dim Coleccion As List(Of SEC.PersonalSupervisor) = Nothing
            Coleccion = MyBase.SQLList(Of SEC.PersonalSupervisor)(strQuery)
            If Coleccion.Count > 0 Then
                Me.CargarRelaciones(Coleccion, Relaciones)
            End If
            Return Coleccion
        End Function


        Friend Function ReturnChildPerfilId(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.PersonalSupervisor)
            Dim llaves As String = MyBase.KeysArray(Keys).ToString.Replace("(", "(',")
            llaves = llaves.Replace(")", ",')")
            Dim strQuery As String = "crm_seguridad_perfilpersonal_buscarperfiles"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@llaves", DbType.String, llaves)
                Dim Coleccion As List(Of SEC.PersonalSupervisor) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.PersonalSupervisor)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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

        Friend Function ReturnChild3(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.PersonalSupervisor)
            Dim strQuery As String = "SELECT * FROM security_msprofile WHERE Id IN (SELECT ProfileId FROM security_msuser where Id in " & MyBase.KeysArray(Keys) & ")"
            Dim Coleccion As List(Of SEC.PersonalSupervisor) = Nothing
            Coleccion = MyBase.SQLList(Of SEC.PersonalSupervisor)(strQuery)
            If Coleccion.Count > 0 Then
                Me.CargarRelaciones(Coleccion, Relaciones)
            End If
            Return Coleccion
        End Function


        Friend Function ReturnChildById(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.PersonalSupervisor)
            Dim strQuery As String = "SELECT * FROM security_msprofile WHERE Id IN " & MyBase.KeysArray(Keys)
            Dim Coleccion As List(Of SEC.PersonalSupervisor) = Nothing
            Coleccion = MyBase.SQLList(Of SEC.PersonalSupervisor)(strQuery)
            If Coleccion.Count > 0 Then
                Me.CargarRelaciones(Coleccion, Relaciones)
            End If
            Return Coleccion
        End Function

        'modificado porque estaba mal asi estaba antes 
        Friend Function ReturnChild2(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As SEC.PersonalSupervisor
            Dim strQuery As String = "SELECT * FROM security_msprofile WHERE Id IN " & MyBase.KeysArray(Keys)
            Dim BEProfile As SEC.PersonalSupervisor = Nothing
            BEProfile = MyBase.SQLSearch(Of SEC.PersonalSupervisor)(strQuery)
            If BEProfile IsNot Nothing Then
                Me.CargarRelaciones(BEProfile, Relaciones)
            End If
            Return BEProfile
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
            Dim BEObject As New SEC.PersonalSupervisor

            With DR
                BEObject.Id = .GetInt32(0)
                BEObject.PersonalId = .GetInt32(1)
                BEObject.SupervisorId = .GetInt32(2)
                BEObject.PersonalIdL = .GetInt32(3)
                BEObject.FechaReg = .GetDateTime(4)
                BEObject.FechaActualizacion = .GetDateTime(5)
                BEObject.Estado = .GetInt16(6)

            End With

            Return CType(DirectCast(BEObject, BE.BEntity), T)
        End Function

        ''' <summary>
        ''' Carga las Relaciones de la Coleccion dada
        ''' </summary>
        ''' <param name="Coleccion">Lista de objetos para cargar las Relaciones</param>
        ''' <param name="Relaciones">Enumerador de Relaciones a cargar</param>
        ''' <remarks></remarks>
        Protected Sub CargarRelaciones(ByRef Coleccion As List(Of SEC.PersonalSupervisor), ByVal ParamArray Relaciones() As [Enum])
            Dim DALFormulario As Formulario
            Dim colFormulario As List(Of SEC.Formulario) = Nothing
            Dim DALPersonal As Personal
            Dim colPersonal As List(Of SEC.Personal) = Nothing
            Dim Keys As IEnumerable(Of Int32) = Nothing

            For Each RelationEnum As [Enum] In Relaciones
                If RelationEnum.Equals(SEC.relPerfil.Personal) Then
                    DALPersonal = New Personal(True, CType(MyBase.DBFactory, Object))
                    Keys = (From BEObject In Coleccion Select BEObject.PersonalId).Distinct
                    colPersonal = DALPersonal.ReturnChild(Keys, Relaciones)
                End If


            Next

            If Relaciones.GetLength(0) > 0 Then
                For Each BEProfile In Coleccion
                    If colPersonal IsNot Nothing Then
                        BEProfile.Personal = (From BEObject In colPersonal
                                              Where BEObject.Id = BEProfile.PersonalId
                                              Select BEObject).FirstOrDefault
                    End If

                Next
            End If

            DALFormulario = Nothing
            DALPersonal = Nothing
        End Sub

        ''' <summary>
        ''' Load Relacioneship of an Object
        ''' </summary>
        ''' <param name="BEProfile">Given Object</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <remarks></remarks>
        Protected Sub CargarRelaciones(ByRef BEProfile As SEC.PersonalSupervisor, ByVal ParamArray Relaciones() As [Enum])
            Dim DALFormulario As Formulario
            Dim DALPersonal As Personal

            For Each RelationEnum As [Enum] In Relaciones
                If RelationEnum.Equals(SEC.relPerfil.Personal) Then
                    DALPersonal = New Personal(True, CType(MyBase.DBFactory, Object))
                    BEProfile.Personal = DALPersonal.ReturnMaster(BEProfile.PersonalId, Relaciones)
                End If

            Next

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
        Public Function Listar(ByVal Order As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.PersonalSupervisor)
            Dim strQuery As String = "SELECT * FROM security_msprofile ORDER By " & Order
            Dim Coleccion As List(Of SEC.PersonalSupervisor) = MyBase.SQLList(Of SEC.PersonalSupervisor)(strQuery)

            If Coleccion.Count > 0 Then
                Me.CargarRelaciones(Coleccion, Relaciones)
            End If
            Return Coleccion
        End Function


        Public Function Listar(ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.PersonalSupervisor)
            Dim strQuery As String = "crm_seguridad_personalsupervisor_listar"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                Dim Collection As List(Of SEC.PersonalSupervisor) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.PersonalSupervisor)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

        Public Function PagedFilterList(PageIndex As Integer, PageSize As Integer, OrderBy As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.PersonalSupervisor)
            Dim strQuery As String = "SELECT * FROM security_msprofile Order By " & OrderBy
            Dim Coleccion As List(Of SEC.PersonalSupervisor) = MyBase.PaggingSQLList(Of SEC.PersonalSupervisor)(strQuery, PageIndex, PageSize)

            If Coleccion.Count > 0 Then
                Me.CargarRelaciones(Coleccion, Relaciones)
            End If
            Return Coleccion
        End Function

        Public Shadows Function Count() As Long
            Dim strQuery As String = "SELECT COUNT(*) FROM security_msprofile"

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
        Public Function Listar(ByVal IdPadre As Long, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.PersonalSupervisor)
            Dim strQuery As String = "SELECT * FROM security_msprofile WHERE IdPadre = " & IdPadre.ToString
            Dim Coleccion As List(Of SEC.PersonalSupervisor) = MyBase.SQLList(Of SEC.PersonalSupervisor)(strQuery)

            If Coleccion.Count > 0 Then
                Me.CargarRelaciones(Coleccion, Relaciones)
            End If
            Return Coleccion
        End Function

        ''' <summary>
        ''' Return an Array of keys from table ProfileOption based on the project key
        ''' </summary>
        ''' <param name="Keys"></param>
        ''' <param name="Relaciones"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Function ReturnOption(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of BE.Relation)
            Dim strQuery As String = "SELECT IT.* FROM security_psprofileoption IT INNER JOIN " &
                                         "security_msprofile A ON A.Id = IT.PersonalSupervisorId WHERE IT.PersonalSupervisorId in " & MyBase.KeysArray(Keys)
            Dim RelacioneshipKeys As New List(Of BE.Relation)
            Dim drReader As IDataReader = Nothing
            Dim BERelacioneship As New BE.Relation

            Try
                drReader = MyBase.GenericList(strQuery)
                With drReader
                    Do While .Read()
                        BERelacioneship = New BE.Relation
                        BERelacioneship.FirstKey = drReader.GetInt64(0)
                        BERelacioneship.SecondKey = drReader.GetInt64(1)
                        RelacioneshipKeys.Add(BERelacioneship)
                    Loop
                End With
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
            Finally
                DisposeReader(drReader)
            End Try
            Return RelacioneshipKeys
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
        Public Function Buscar(ByVal Id As Long, ByVal ParamArray Relaciones() As [Enum]) As SEC.PersonalSupervisor
            Dim strQuery As String = "SELECT * FROM crm_seguridad_personalsupervisor WHERE Id = " & Id.ToString
            Dim BEProfile As SEC.PersonalSupervisor = MyBase.SQLSearch(Of SEC.PersonalSupervisor)(strQuery)

            If BEProfile IsNot Nothing Then
                Me.CargarRelaciones(BEProfile, Relaciones)
            End If
            Return BEProfile
        End Function

        Public Function BuscarPorPersonal(ByVal Id As Long, ByVal ParamArray Relaciones() As [Enum]) As SEC.PersonalSupervisor
            Dim strQuery As String = "SELECT * FROM crm_seguridad_personalsupervisor WHERE PersonalId = " & Id.ToString
            Dim BEProfile As SEC.PersonalSupervisor = MyBase.SQLSearch(Of SEC.PersonalSupervisor)(strQuery)

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
        'Friend Sub New(ByVal UseDBCon As Boolean, ByRef BD As Object, ByVal Transaction As DbTransaction)
        '    MyBase.New(UseDBCon, BD, Transaction)
        'End Sub

        Public Sub New(ByVal UseDBCon As Boolean, ByRef BD As Object, ByVal Transaction As DbTransaction)
            MyBase.New(UseDBCon, BD, Transaction)
        End Sub


#End Region

    End Class

End Namespace