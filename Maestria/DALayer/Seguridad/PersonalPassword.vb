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
    Public Class PersonalPassword
        Inherits DALEntity

#Region " Save Methods"

        ''' <summary>
        ''' 	Saves business information object of type Profile
        ''' </summary>
        ''' <param name="BEObj">Business object of type Profile </param>
        ''' <remarks>
        ''' </remarks>
        Public Sub Guardar(ByRef BEObj As SEC.PersonalPassword)
            Dim strQuery As String = ""
            If BEObj.StatusType = BE.StatusType.Insert Then
                'strQuery = "INSERT INTO crm_seguridad_personalpassword " &
                '    "VALUES(HASHBYTES('sha',@Password),@PersonalId); SELECT @@IDENTITY"
                strQuery = "crm_seguridad_personalpassword_insert"
            ElseIf BEObj.StatusType = BE.StatusType.Update Then
                strQuery = "UPDATE crm_seguridad_personalpassword SET " &
                    "Password = @Password,PersonalId = @PersonalId" &
                    "WHERE Id = @Id"
            ElseIf BEObj.StatusType = BE.StatusType.Delete Then
                strQuery = "UPDATE crm_seguridad_personalpassword SET " &
                    "Password = @Password,PersonalId = @PersonalId" &
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
                    MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                    If BEObj.StatusType <> BE.StatusType.Insert Then
                        MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, BEObj.Id)
                    End If
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@Password", DbType.String, BEObj.Password)
                    MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, BEObj.PersonalId)

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


#End Region

#Region " Methods "

        Friend Function ReturnChild(ByVal Keys As IEnumerable(Of Int32), ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.PersonalPassword)
            Dim strQuery As String = "SELECT * FROM crm_seguridad_personalpassword WHERE Id IN " & MyBase.KeysArray(Keys)
            Dim Coleccion As List(Of SEC.PersonalPassword) = Nothing
            Coleccion = MyBase.SQLList(Of SEC.PersonalPassword)(strQuery)
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
            Dim BEObject As New SEC.PersonalPassword

            With DR
                BEObject.Id = .GetInt32(0)
                BEObject.Password = .GetString(1)
                BEObject.PersonalId = .GetInt32(2)
            End With

            Return CType(DirectCast(BEObject, BE.BEntity), T)
        End Function

        ''' <summary>
        ''' Carga las Relaciones de la Coleccion dada
        ''' </summary>
        ''' <param name="Coleccion">Lista de objetos para cargar las Relaciones</param>
        ''' <param name="Relaciones">Enumerador de Relaciones a cargar</param>
        ''' <remarks></remarks>
        Protected Sub CargarRelaciones(ByRef Coleccion As List(Of SEC.PersonalPassword), ByVal ParamArray Relaciones() As [Enum])
            Dim DALFormulario As Formulario
            Dim colFormulario As List(Of SEC.Formulario) = Nothing
            Dim DALPersonal As Personal
            Dim colPersonal As List(Of SEC.Personal) = Nothing
            Dim Keys As IEnumerable(Of Int32) = Nothing

            For Each RelationEnum As [Enum] In Relaciones
                'If RelationEnum.Equals(SEC.relPerfil.Personal) Then
                '    DALPersonal = New Personal(True, CType(MyBase.DBFactory, Object))
                '    Keys = (From BEObject In Coleccion Select BEObject.PersonalId).Distinct
                '    colPersonal = DALPersonal.ReturnChild(Keys, Relaciones)
                'End If

                'Keys = From BEProfile In Coleccion Select BEProfile.Id
                'If RelationEnum.Equals(SEC.relPerfil.Formulario) Then
                '    DALFormulario = New Formulario(True, CType(MyBase.DBFactory, Object))
                '    colFormulario = DALFormulario.ReturnChildOption(Keys, Relaciones)
                'End If

            Next

            If Relaciones.GetLength(0) > 0 Then
                'For Each BEProfile In Coleccion
                '    If colPersonal IsNot Nothing Then
                '        BEProfile.Personal = (From BEObject In colPersonal
                '                              Where BEObject.Id = BEProfile.PersonalId
                '                              Select BEObject).FirstOrDefault
                '    End If
                '    If colFormulario IsNot Nothing Then
                '        Dim Relacioneship As List(Of BE.Relation)

                '        Relacioneship = Me.ReturnOption(Keys)
                '        BEProfile.ColeccionFormulario = (From BEObject In colFormulario Join Key In Relacioneship On Key.FirstKey Equals BEObject.Id
                '                                         Where Key.SecondKey = BEProfile.Id
                '                                         Select BEObject).ToList
                '    End If
                'Next
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
        Protected Sub CargarRelaciones(ByRef BEProfile As SEC.PersonalPassword, ByVal ParamArray Relaciones() As [Enum])
            Dim DALFormulario As Formulario
            Dim DALPersonal As Personal

            For Each RelationEnum As [Enum] In Relaciones
                'If RelationEnum.Equals(SEC.relPerfil.Personal) Then
                '    DALPersonal = New Personal(True, CType(MyBase.DBFactory, Object))
                '    BEProfile.Personal = DALPersonal.ReturnMaster(BEProfile.PersonalId, Relaciones)
                'End If

                'Dim Keys() As Int32 = {BEProfile.Id}
                'If RelationEnum.Equals(SEC.relPerfil.Formulario) Then
                '    DALFormulario = New Formulario(True, CType(MyBase.DBFactory, Object))
                '    BEProfile.ColeccionFormulario = DALFormulario.ReturnChildOption(Keys, Relaciones)

                'End If
            Next

            DALFormulario = Nothing
            DALPersonal = Nothing
        End Sub

#End Region

#Region " List Methods "

        Public Function Listar(ByVal PersonalId As Int32, ByVal CantidadRegistrosPassword As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.PersonalPassword)
            Dim strQuery As String = "SELECT TOP " & CantidadRegistrosPassword & " * FROM crm_seguridad_personalpassword where PersonalId = " & PersonalId
            Dim Coleccion As List(Of SEC.PersonalPassword) = Nothing
            Coleccion = MyBase.SQLList(Of SEC.PersonalPassword)(strQuery)
            If Coleccion.Count > 0 Then
                Me.CargarRelaciones(Coleccion, Relaciones)
            End If
            Return Coleccion
        End Function
        Public Function VerificarPasswordAnteriores(ByVal Password As String, ByVal PersonalId As Int32, ByVal CantidadRegistrosPassword As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.PersonalPassword)
            Dim strQuery As String = "crm_seguridad_personalpassword_validarpassword"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Password", DbType.String, Password)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@PersonalId", DbType.Int32, PersonalId)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@CantidadRegistrosPassword", DbType.Int32, CantidadRegistrosPassword)
                Dim Collection As List(Of SEC.PersonalPassword) = MyBase.SQLConvertidorIDataReaderListas(Of SEC.PersonalPassword)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
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