
Option Strict On
Option Compare Text
Option Explicit On
Option Infer On

Imports BAS = BEntities.Base
Imports BE = BEntities
Imports DAL = DALayer.Base

Namespace Base
    ''' -----------------------------------------------------------------------------
    ''' Project   : 
    ''' NameSpace : Base
    ''' Class     : Classifiers
    ''' Service  :  Base
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''    This business component validates business rules for business component Classifiers 
    '''    for the services Base
    ''' </summary>
    ''' <remarks>
    '''    Business component for service Base
    ''' </remarks>
    ''' <history>
    '''   []   9/21/2015 6:47:38 PM Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    <Serializable>
    Partial Public Class Clasificadores
        Inherits BCEntity

#Region " Search Methods "

        ''' <summary>
        ''' 	Search for business objects of type Classifiers
        ''' </summary>
        ''' <param name="Id">Object identifier Classifiers</param>
        ''' <param name="Relations">relationship enumetators</param>
        ''' <returns>An object of type Classifiers</returns>
        ''' <remarks>
        ''' 	To get relationship objects, suply relationship enumetators
        ''' </remarks>
        Public Function Search(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As BAS.Clasificadores
            Dim BEObject As BAS.Clasificadores = Nothing

            Try
                Using DALObject As New DAL.Clasificadores
                    BEObject = DALObject.Search(Id, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function Buscar(ByVal Id As Int32, ByVal ParamArray Relations() As [Enum]) As BAS.Clasificadores
            Dim BEObject As BAS.Clasificadores = Nothing
            'Dim DALObject As DAL.Clasificadores = Nothing
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.Clasificadores
                    BEObject = DALObject.Buscar(Id, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function
        Public Function BuscarPorValor(ByVal valor As String, ByVal ParamArray Relations() As [Enum]) As BAS.Clasificadores
            Dim BEObject As BAS.Clasificadores = Nothing
            'Dim DALObject As DAL.Clasificadores = Nothing
            Me.ErrorCollection.Clear()
            Try
                Using DALObject = New DAL.Clasificadores
                    BEObject = DALObject.BuscarPorValor(valor, Relations)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function

        Public Function Search(ByVal Id As Int32) As BAS.Clasificadores
            Dim BEObject As BAS.Clasificadores = Nothing

            Try
                Using DALObject As New DAL.Clasificadores
                    BEObject = DALObject.Search(Id)
                End Using
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            End Try
            Return BEObject
        End Function

#End Region

#Region " List Methods "

        Public Function List(ByVal text As String, ByVal Estado As Int16, ByVal PageIndex As Int32, ByVal pageSize As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of BAS.Clasificadores)
            Try
                Dim BECollection As List(Of BAS.Clasificadores)

                Using DALObject As New DAL.Clasificadores
                    BECollection = DALObject.List(text, Estado, PageIndex, pageSize, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        Public Function List(ByVal TipoClasificadorId As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of BAS.Clasificadores)
            Try
                Dim BECollection As List(Of BAS.Clasificadores)

                Using DALObject As New DAL.Clasificadores
                    BECollection = DALObject.List(TipoClasificadorId, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex.InnerException, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        Public Function ListCombo(ByVal TipoClasificadorId As Int32, ByVal text As String, ByVal PageSize As Int32, ByVal PageIndex As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of BAS.Clasificadores)
            Try
                Dim Collection As List(Of BAS.Clasificadores)
                Using DALObject As New DAL.Clasificadores
                    Collection = DALObject.ListCombo(TipoClasificadorId, text, PageSize, PageIndex, Relations)
                End Using
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListComboCount(ByVal TipoClasificadorId As Integer, ByVal text As String) As Int32
            Try
                Dim Collection As Int32
                Using DALObject As New DAL.Clasificadores
                    Collection = DALObject.ListComboCount(TipoClasificadorId, text)
                End Using
                Return Collection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        Public Function List() As List(Of BAS.Clasificadores)
            Try
                Dim BECollection As List(Of BAS.Clasificadores)

                Using DALObject As New DAL.Clasificadores
                    BECollection = DALObject.List()
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex.InnerException, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListClasificadores(ByVal TipoClasificadorId As Int32, ByVal ParamArray Relaciones() As [Enum]) As List(Of BAS.Clasificadores)
            Try
                Dim BECollection As List(Of BAS.Clasificadores)

                Using DALObject As New DAL.Clasificadores
                    BECollection = DALObject.ListClasificadore(TipoClasificadorId, Relaciones)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex.InnerException, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function
        Public Function ListCB(ByVal ClassifierTypeId As Int32, ByVal ParamArray Relations() As [Enum]) As List(Of BAS.Clasificadores)
            Try
                Dim BECollection As List(Of BAS.Clasificadores)

                Using DALObject As New DAL.Clasificadores
                    '*************************************************************************
                    BECollection = DALObject.Listcb(ClassifierTypeId, Relations)
                End Using

                Return BECollection
            Catch ex As Exception
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
                Return Nothing
            End Try
        End Function

        Public Function Count(ByVal text As String, ByVal ParamArray Relations() As [Enum]) As List(Of BAS.Clasificadores)
            Try
                Dim BECollection As List(Of BAS.Clasificadores)

                Using DALObject As New DAL.Clasificadores
                    BECollection = DALObject.Count(text, Relations)
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
        ''' <param name="BEObj">Object type Classifiers</param>    	
        ''' <remarks>
        ''' </remarks>
        Public Sub Save(ByRef BEObj As BAS.Clasificadores)
            Dim DALObject As DAL.Clasificadores = Nothing

            Me.ErrorCollection.Clear()

            If Me.Validate(BEObj) Then
                Try
                    DALObject = New DAL.Clasificadores
                    DALObject.OpenConnection()
                    DALObject.Save(BEObj)
                    DALObject.Commit()
                    'para Guardar Varios Objetos hay que utilizar los constructores adicionales de la clase que inicia la conexion 
                    'y se pasa los objetos transaccion db y conexion al constructor de la dal de los hijos  

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


        ''' <summary>
        ''' 	Saves data object of type Classifier
        ''' </summary>
        ''' <param name="colBEObj">Object type Classifier</param>
        ''' <remarks>
        ''' </remarks>
        Friend Sub Save(ByRef colBEObj As List(Of BAS.Clasificadores), ByVal IdMaster As Int32)
            Dim DALObject As New DAL.Clasificadores
            Try
                DALObject = New DAL.Clasificadores
                DALObject.OpenConnection()
                DALObject.Save(colBEObj, IdMaster)
                DALObject.Commit()
            Catch ex As Exception
                DALObject.Rollback()
                MyBase.ErrorHandler(ex, ErrorPolicy.BCWrap)
            Finally
                DALObject.CloseConnection()
            End Try
        End Sub

        ''' <summary>
        ''' 	Validates a business object before save it 
        ''' </summary>
        ''' <param name="BEObj">Object Type Classifiers</param>
        ''' <returns>True: if object were validated</returns>
        ''' <remarks>
        ''' </remarks>
        Friend Function Validate(ByRef BEObj As BAS.Clasificadores) As Boolean
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