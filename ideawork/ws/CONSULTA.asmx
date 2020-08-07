<%@ WebService Language="VB"  Class="CadastroConsulta" %>
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
Public Class CadastroConsulta
    Inherits System.Web.Services.WebService
    Dim cf As New commonfunction
    <WebMethod()>
    Public Sub GetNewCheckList()
        Dim c As New checklist
        c.cab.cod = cf.GetUProssimoCheckList()
        Dim js As New JavaScriptSerializer
        js.MaxJsonLength = Int32.MaxValue
        Context.Response.Write(js.Serialize(c))
    End Sub
    <WebMethod()>
    Public Sub GetNewJob(usuario As String)
        Dim j As New Job
        j.cod = cf.GetProssimoJob()
        j.dataHoraCriacao = DateTime.Now
        j.usuarioCriacao = usuario
        j.status = 1
        j.codCheckList = 0
        Dim js As New JavaScriptSerializer
        js.MaxJsonLength = Int32.MaxValue
        Context.Response.Write(js.Serialize(j))
    End Sub
    <WebMethod()>
    Public Sub GetListacabchecklist()
        Try
            Dim l() As cabchecklist = cf.GetListacabchecklist()
            Dim js As New JavaScriptSerializer
            js.MaxJsonLength = Int32.MaxValue
            Context.Response.Write(js.Serialize(l))
        Catch ex As Exception
            cf.fbs.AddErr("#" & ex.Message)
            Context.Response.Write(cf.fbs.LogErr)
        End Try
    End Sub
    <WebMethod()>
    Public Sub GetcabchecklistByCod(cod As String)
        Try
            Dim l As cabchecklist = cf.GetcabchecklistByCod(cod)
            Dim js As New JavaScriptSerializer
            js.MaxJsonLength = Int32.MaxValue
            Context.Response.Write(js.Serialize(l))
        Catch ex As Exception
            cf.fbs.AddErr("#" & ex.Message)
            Context.Response.Write(cf.fbs.LogErr)
        End Try
    End Sub
    <WebMethod()>
    Public Sub GetChecklist(cod As String)
        Try
            Dim c As checklist = cf.GetChecklistByCod(cod)
            Dim js As New JavaScriptSerializer
            js.MaxJsonLength = Int32.MaxValue
            Context.Response.Write(js.Serialize(c))
        Catch ex As Exception
            cf.fbs.AddErr("#" & ex.Message)
            Context.Response.Write(cf.fbs.LogErr)
        End Try
    End Sub

    <WebMethod()>
    Public Sub GetListaJobs()
        Try
            Dim l() As Job = cf.GetListaJobs()
            Dim js As New JavaScriptSerializer
            js.MaxJsonLength = Int32.MaxValue
            Context.Response.Write(js.Serialize(l))
        Catch ex As Exception
            cf.fbs.AddErr("#" & ex.Message)
            Context.Response.Write(cf.fbs.LogErr)
        End Try
    End Sub
    <WebMethod()>
    Public Sub GetJobByCod(cod As String)
        Try
            Dim l As Job = cf.GetJobByCod(cod)
            Dim js As New JavaScriptSerializer
            js.MaxJsonLength = Int32.MaxValue
            Context.Response.Write(js.Serialize(l))
        Catch ex As Exception
            cf.fbs.AddErr("#" & ex.Message)
            Context.Response.Write(cf.fbs.LogErr)
        End Try
    End Sub
    <WebMethod()>
    Public Sub GetListaClientes()
        Try
            Dim l() As Clientes = cf.GetListaClientes()
            Dim js As New JavaScriptSerializer
            js.MaxJsonLength = Int32.MaxValue
            Context.Response.Write(js.Serialize(l))
        Catch ex As Exception
            cf.fbs.AddErr("#" & ex.Message)
            Context.Response.Write(cf.fbs.LogErr)
        End Try
    End Sub
    <WebMethod()>
    Public Sub GetClienteByCod(cod As String)
        Try
            Dim l As Clientes = cf.GetClienteByCod(cod)
            Dim js As New JavaScriptSerializer
            js.MaxJsonLength = Int32.MaxValue
            Context.Response.Write(js.Serialize(l))
        Catch ex As Exception
            cf.fbs.AddErr("#" & ex.Message)
            Context.Response.Write(cf.fbs.LogErr)
        End Try
    End Sub
    <WebMethod()>
    Public Sub GetListaUsuarios()
        Try
            Dim l() As usuarios = cf.GetListaUsuarios()
            Dim js As New JavaScriptSerializer
            js.MaxJsonLength = Int32.MaxValue
            Context.Response.Write(js.Serialize(l))
        Catch ex As Exception
            cf.fbs.AddErr("#" & ex.Message)
            Context.Response.Write(cf.fbs.LogErr)
        End Try
    End Sub
    <WebMethod()>
    Public Sub getusuariobycod(cod As String)
        Try
            Dim l As usuarios = cf.GetUsuarioByCod(cod)
            Dim js As New JavaScriptSerializer
            js.MaxJsonLength = Int32.MaxValue
            Context.Response.Write(js.Serialize(l))
        Catch ex As Exception
            cf.fbs.AddErr("#" & ex.Message)
            Context.Response.Write(cf.fbs.LogErr)
        End Try
    End Sub

    <WebMethod()>
    Public Sub GetCabCarByCodJson(cod As String)
        Try
            Dim c As CarCab = cf.GetCabCarByCod(cod)
            Dim js As New JavaScriptSerializer
            js.MaxJsonLength = Int32.MaxValue
            Context.Response.Write(js.Serialize(c))
        Catch ex As Exception
            cf.fbs.AddErr("#" & ex.Message)
            Context.Response.Write(cf.fbs.LogErr)
        End Try
    End Sub

    <WebMethod()>
    Public Sub GetCarCabJson(tipo As String)
        Try
            Dim c() As CarCab = cf.GetCarCab(tipo)
            Dim js As New JavaScriptSerializer
            js.MaxJsonLength = Int32.MaxValue
            Context.Response.Write(js.Serialize(c))
        Catch ex As Exception
            cf.fbs.AddErr("#" & ex.Message)
            Context.Response.Write(cf.fbs.LogErr)
        End Try
    End Sub
    <WebMethod()>
    Public Sub GetCarOpcJson(codcar As String, adicionavazio As Integer)
        Try
            Dim o() As CarOpc = cf.GetCarOpc(codcar, adicionavazio)
            Dim js As New JavaScriptSerializer
            js.MaxJsonLength = Int32.MaxValue
            Context.Response.Write(js.Serialize(o))
        Catch ex As Exception
            cf.fbs.AddErr("#" & ex.Message)
            Context.Response.Write(cf.fbs.LogErr)
        End Try
    End Sub
    <WebMethod()>
    Public Sub GetCarOpcByCod(codcar As String, adicionavazio As Integer)
        Try
            Dim o As CarOpc = cf.GetCarOpcByCod(codcar, adicionavazio)
            Dim js As New JavaScriptSerializer
            js.MaxJsonLength = Int32.MaxValue
            Context.Response.Write(js.Serialize(o))
        Catch ex As Exception
            cf.fbs.AddErr("#" & ex.Message)
            Context.Response.Write(cf.fbs.LogErr)
        End Try
    End Sub

    <WebMethod()>
    Public Sub GetCarCatJson(codcar As String, adicionavazio As Integer)
        Try
            Dim c() As CarCat = cf.GetCarCat(codcar, adicionavazio)
            Dim js As New JavaScriptSerializer
            js.MaxJsonLength = Int32.MaxValue
            Context.Response.Write(js.Serialize(c))
        Catch ex As Exception
            cf.fbs.AddErr("#" & ex.Message)
            Context.Response.Write(cf.fbs.LogErr)
        End Try
    End Sub
    <WebMethod()>
    Public Sub GetCarTxt(codcar As String)
        Try
            Context.Response.Write(cf.GetLinkCarTxt(codcar))
        Catch ex As Exception
            cf.fbs.AddErr("#" & cf.fbs.LogErr)
        End Try
    End Sub

End Class