'------------------------------------------------------------------------------
' <auto-generated>
'     Este código fue generado por una herramienta.
'     Versión de runtime:4.0.30319.42000
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace Resources
    
    'StronglyTypedResourceBuilder generó automáticamente esta clase
    'a través de una herramienta como ResGen o Visual Studio.
    'Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    'con la opción /str o recompile el proyecto de Visual Studio.
    '''<summary>
    '''  Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "16.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Class RadProgressArea
        
        Private Shared resourceMan As Global.System.Resources.ResourceManager
        
        Private Shared resourceCulture As Global.System.Globalization.CultureInfo
        
        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>  _
        Friend Sub New()
            MyBase.New
        End Sub
        
        '''<summary>
        '''  Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Resources.RadProgressArea", Global.System.Reflection.[Assembly].Load("App_GlobalResources"))
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Invalida la propiedad CurrentUICulture del subproceso actual para todas las
        '''  búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Shared Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Busca una cadena traducida similar a Cancel.
        '''</summary>
        Friend Shared ReadOnly Property Cancel() As String
            Get
                Return ResourceManager.GetString("Cancel", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Busca una cadena traducida similar a Uploading file: .
        '''</summary>
        Friend Shared ReadOnly Property CurrentFileName() As String
            Get
                Return ResourceManager.GetString("CurrentFileName", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Busca una cadena traducida similar a Elapsed time: .
        '''</summary>
        Friend Shared ReadOnly Property ElapsedTime() As String
            Get
                Return ResourceManager.GetString("ElapsedTime", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Busca una cadena traducida similar a Estimated time: .
        '''</summary>
        Friend Shared ReadOnly Property EstimatedTime() As String
            Get
                Return ResourceManager.GetString("EstimatedTime", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Busca una cadena traducida similar a Please do not remove this key..
        '''</summary>
        Friend Shared ReadOnly Property ReservedResource() As String
            Get
                Return ResourceManager.GetString("ReservedResource", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Busca una cadena traducida similar a Total .
        '''</summary>
        Friend Shared ReadOnly Property Total() As String
            Get
                Return ResourceManager.GetString("Total", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Busca una cadena traducida similar a Total files: .
        '''</summary>
        Friend Shared ReadOnly Property TotalFiles() As String
            Get
                Return ResourceManager.GetString("TotalFiles", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Busca una cadena traducida similar a Speed: .
        '''</summary>
        Friend Shared ReadOnly Property TransferSpeed() As String
            Get
                Return ResourceManager.GetString("TransferSpeed", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Busca una cadena traducida similar a Uploaded.
        '''</summary>
        Friend Shared ReadOnly Property Uploaded() As String
            Get
                Return ResourceManager.GetString("Uploaded", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Busca una cadena traducida similar a Uploaded files: .
        '''</summary>
        Friend Shared ReadOnly Property UploadedFiles() As String
            Get
                Return ResourceManager.GetString("UploadedFiles", resourceCulture)
            End Get
        End Property
    End Class
End Namespace
