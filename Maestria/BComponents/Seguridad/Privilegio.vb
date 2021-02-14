
Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports BE = BEntities
Imports DAL = DALayer.Seguridad
Imports SEC = BEntities.Seguridad

Namespace Seguridad

    <Serializable>
    Public Class Privilegio
        Inherits BCEntity

#Region " Search Methods "

        ''' <summary>
        ''' 	Search for business objects of type Module
        ''' </summary>
        ''' <param name="Id">Object identifier Module</param>
        ''' <param name="Relaciones">Relacioneship enumetators</param>
        ''' <returns>An object of type Module</returns>
        ''' <remarks>
        ''' 	To get Relacioneship objects, suply Relacioneship enumetators
        ''' </remarks>
        ''' 


        Public Sub Guardar(ByRef BEObj As SEC.Privilegio)
            Dim DALObject As DAL.Privilegio = Nothing
            Me.ErrorCollection.Clear()

            If Me.Validar(BEObj) Then
                Try
                    DALObject = New DAL.Privilegio
                    DALObject.OpenConnection()
                    DALObject.Guardar(BEObj)
                    DALObject.Commit()
                Catch ex As Exception
                    DALObject.Rollback()
                    MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Finally
                    DALObject.CloseConnection()
                End Try
            Else
                MyBase.ErrorHandler(New BCException(Me.ErrorCollection), ErrorPolicy.BCNew)
            End If
        End Sub
        Public Sub Guardar(ByRef BEObj As List(Of SEC.Privilegio))
            Dim DALObject As DAL.Privilegio = Nothing
            Me.ErrorCollection.Clear()

            'If Me.Validar(BEObj) Then
            Try
                DALObject = New DAL.Privilegio
                DALObject.OpenConnection()
                DALObject.Guardar(BEObj)
                DALObject.Commit()
            Catch ex As Exception
                DALObject.Rollback()
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            Finally
                DALObject.CloseConnection()
            End Try
            'Else
            'MyBase.ErrorHandler(New BCException(Me.ErrorCollection), ErrorPolicy.BCNew)
            'End If
        End Sub
        Public Function Buscar(ByVal Id As Long, ByVal ParamArray Relaciones() As [Enum]) As SEC.Privilegio
            Dim BEObject As SEC.Privilegio = Nothing

            Try
                Using DALObject As New DAL.Privilegio
                    BEObject = DALObject.Buscar(Id, Relaciones)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function BuscarPrivilegioPorPersonal(ByVal PersonalId As Int32, ByVal FormularioId As Int32, ByVal UnidadNegocioId As Int32, ByVal ParamArray Relaciones() As [Enum]) As SEC.Privilegio
            Dim BEObject As SEC.Privilegio = Nothing
            Try
                Using DALObject As New DAL.Privilegio
                    BEObject = DALObject.BuscarPrivilegioPorPersonal(PersonalId, FormularioId, UnidadNegocioId, Relaciones)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function

#End Region

#Region " List Methods "

        ''' <summary>
        ''' 	Search for collection business objects of type Module
        ''' </summary>
        ''' <param name="Order">Property column to specify collection order</param>
        ''' <param name="Relaciones">Relacioneship enumerator</param>
        ''' <returns>Object collection of type Module</returns>
        ''' <remarks>
        ''' 	To get Relacioneship objects, suply Relacioneship enumetators
        ''' </remarks>
        Public Function Listar(ByVal Order As String, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Privilegio)
            Try
                Dim BECollection As List(Of SEC.Privilegio)

                Using DALObject As New DAL.Privilegio
                    BECollection = DALObject.Listar(Order, Relaciones)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListaPorPersonal(ByVal PersonalId As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of SEC.Privilegio)
            Try
                Dim BECollection As List(Of SEC.Privilegio)

                Using DALObject As New DAL.Privilegio
                    BECollection = DALObject.ListaPorPersonal(PersonalId, Relaciones)
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
        ''' 	Validates a business object before save it 
        ''' </summary>
        ''' <param name="BEObj">Object Type Module</param>
        ''' <returns>True: if object were validated</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function Validar(ByRef BEObj As SEC.Privilegio) As Boolean
            Dim bolOk As Boolean = True

            If BEObj.StatusType <> BE.StatusType.NoAction Then
                If BEObj.StatusType <> BE.StatusType.Insert Then
                    If BEObj.Id = 0 Then
                        MyBase.ErrorCollection.Add("No se ha proporcionado el Identificador")
                        bolOk = False
                    End If
                End If
            End If

            Return bolOk
        End Function

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