Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports BE = BEntities
Imports MEB = BEntities.Base
Imports DAL = DALayer.Base
Imports DALayer.Base
Imports System.Data.Common
Imports System.Net

Namespace Base

    <Serializable>
    Public Class PatronIdentificador
        Inherits DALEntity

        Protected Overrides Function LoadBE(Of T As BE.BEntity)(ByRef DR As IDataReader) As T
            Dim BEObject As New MEB.PatronIdentificador

            With DR
                BEObject.Id = .GetInt32(0)
                BEObject.PaisIdc = .GetInt32(1)
                BEObject.Denomninacion = .GetString(2)
                BEObject.Nomenclatura = .GetString(3)
                BEObject.ExpresionRegular = .GetString(4)
                BEObject.Longitud = .GetInt32(5)
                BEObject.PersonalId = .GetInt32(6)
                BEObject.FechaReg = .GetDateTime(7)
                BEObject.FechaActualizacion = .GetDateTime(8)
                BEObject.Estado = .GetInt16(9)
            End With

            Return CType(DirectCast(BEObject, BE.BEntity), T)
        End Function

        Public Function ListByCountry(ByVal Id As Int32) As List(Of MEB.PatronIdentificador)
            Dim strQuery As String = "crm_patronidentificador_listbycountryid"
            Try
                MyBase.Command = MyBase.DBFactory.GetStoredProcCommand(strQuery)
                MyBase.DBFactory.AddInParameter(MyBase.Command, "@Id", DbType.Int32, Id)
                Dim Coleccion As List(Of MEB.PatronIdentificador) = MyBase.SQLConvertidorIDataReaderListas(Of MEB.PatronIdentificador)(MyBase.DBFactory.ExecuteReader(MyBase.Command))
                'If Coleccion.Count > 0 Then
                '    Me.LoadRelations(Coleccion, Relations)
                'End If
                Return Coleccion
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.DALWrap)
                Return Nothing
            Finally
            End Try
        End Function

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
