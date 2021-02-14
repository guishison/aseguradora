
Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports BE = BEntities
Imports DAL = DALayer.Seguridad
Imports SEC = BEntities.Seguridad

Namespace Seguridad
    <Serializable>
    Public Class PersonalPassword
        Inherits BCEntity


#Region " List Methods "

        Public Function Listar(ByVal PersonalId As Int32, ByVal CantidadRegistrosPassword As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.PersonalPassword)
            Dim BECollection As List(Of SEC.PersonalPassword) = New List(Of SEC.PersonalPassword)
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.PersonalPassword
                    BECollection = DALObject.Listar(PersonalId, CantidadRegistrosPassword, Relations)
                End Using
                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function VerificarPasswordAnteriores(ByVal Password As String, ByVal PersonalId As Int32, ByVal CantidadRegistrosPassword As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of SEC.PersonalPassword)
            Dim BECollection As List(Of SEC.PersonalPassword) = New List(Of SEC.PersonalPassword)
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.PersonalPassword
                    BECollection = DALObject.VerificarPasswordAnteriores(Password, PersonalId, CantidadRegistrosPassword, Relations)
                End Using
                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function


#End Region

#Region " Save Methods "

        ''' <summary>
        ''' 	Saves data object of type 
        ''' </summary>
        ''' <param name="BEObj">Object type Profile</param>    	
        ''' <remarks>
        ''' </remarks>
        Public Sub Guardar(ByRef BEObj As SEC.PersonalPassword)
            Dim DALObject As DAL.PersonalPassword = Nothing

            Me.ErrorCollection.Clear()

            Try
                DALObject = New DAL.PersonalPassword
                DALObject.OpenConnection()
                DALObject.Guardar(BEObj)
                DALObject.Commit()
            Catch ex As Exception
                DALObject.Rollback()
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            Finally
                DALObject.CloseConnection()
            End Try

        End Sub


#End Region

#Region " Constructors "
        ''' <summary>
        '''  Default Constructors 
        ''' </summary>
        ''' <remarks>
        '''	</remarks>
        Public Sub New()
            MyBase.New()
        End Sub

#End Region

    End Class

End Namespace