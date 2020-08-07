<%@ WebService Language="VB"  Class="LogConsulta" %>
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.Script.Serialization
Imports CommonClass
Imports commonfunction
' Per consentire la chiamata di questo servizio Web dallo script utilizzando ASP.NET AJAX, rimuovere il commento dalla riga seguente.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")>
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Public Class LogConsulta
    Inherits System.Web.Services.WebService

    Dim lg As New logfunction
    <WebMethod()>
    Public Sub GetLogCadastro(nometabela As String, chavetabela As String, showimporttxt As Integer)
        Try
            Dim l() As loginfo = lg.GetLogCadastro(nometabela, chavetabela, showimporttxt)
            Dim js As New JavaScriptSerializer
            js.MaxJsonLength = Int32.MaxValue
            Context.Response.Write(js.Serialize(l))
        Catch ex As Exception
            lg.fbs.AddErr("#" & ex.Message)
            Context.Response.Write(lg.fbs.LogErr)
        End Try
    End Sub
    <WebMethod()>
    Public Sub GetLogEventos(nomeevento As String, chaveevento As String)
        Try
            Dim l() As logeventos = lg.GetLogEventos(nomeevento, chaveevento)
            Dim js As New JavaScriptSerializer
            js.MaxJsonLength = Int32.MaxValue
            Context.Response.Write(js.Serialize(l))
        Catch ex As Exception
            lg.fbs.AddErr("#" & ex.Message)
            Context.Response.Write(lg.fbs.LogErr)
        End Try
    End Sub
End Class