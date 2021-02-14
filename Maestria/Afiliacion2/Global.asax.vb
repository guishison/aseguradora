Imports Microsoft.Practices.EnterpriseLibrary.Common.Configuration
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports Microsoft.Practices.EnterpriseLibrary.Validation

Public Class Global_asax
    Inherits System.Web.HttpApplication
    'Dim sched As IScheduler = Nothing

    'void Application_AcquireRequestState(Object sender, EventArgs e)
    '{
    '    HttpContext context = HttpContext.Current;

    '    If (context.Session["myapplication.language"] != null)
    '    {
    '        Thread.CurrentThread.CurrentUICulture = New CultureInfo(context.Session["myapplication.language"].ToString().Trim());
    '        Thread.CurrentThread.CurrentCulture = New CultureInfo(context.Session["myapplication.language"].ToString().Trim());
    '    }
    '}

    'Sub Application_AcquireRequestState(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim context As HttpContext = HttpContext.Current
    '    If context.Session IsNot Nothing Then
    '        If Session("myapplication.language") IsNot Nothing Then
    '            Dim Culturin As String = Session("myapplication.language")
    '            Thread.CurrentThread.CurrentUICulture = New CultureInfo(Culturin)
    '            Thread.CurrentThread.CurrentCulture = New CultureInfo(Culturin)
    '        End If
    '    End If
    'End Sub

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
        Dim config As IConfigurationSource = ConfigurationSourceFactory.Create
        Dim factory As New ExceptionPolicyFactory(config)
        Dim LogFactory As New LogWriterFactory(config)
        Logger.SetLogWriter(LogFactory.Create)
        Dim exManager As ExceptionManager = factory.CreateManager
        ExceptionPolicy.SetExceptionManager(factory.CreateManager())
        ValidationFactory.SetDefaultConfigurationValidatorFactory(New SystemConfigurationSource(False))
    End Sub




End Class