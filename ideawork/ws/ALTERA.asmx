<%@ WebService Language="VB"  Class="CadastroAltera" %>
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
Public Class CadastroAltera
    Inherits System.Web.Services.WebService
    Dim cf As New commonfunction

    <WebMethod()>
    Public Sub Deleteitemchecklist(usuario As String, item As String)
        Dim result As New CommonClass.erro
        Dim js As New JavaScriptSerializer
        js.MaxJsonLength = Int32.MaxValue
        Try
            Dim u As itenschecklist = js.Deserialize(Of itenschecklist)(item)
            cf.Deleteitemchecklist(usuario, u)
            If cf.fbs.LogErr <> "" Then
                result.iderro = 1
                result.deserro = cf.fbs.LogErr
                result.infoerro = "Deleteitemchecklist:" & usuario & " " & item
                result.SaveErro()
            Else
                result.iderro = 0
                result.messagge = u.des & " excluído com sucesso!"
            End If
        Catch ex As Exception
            result.iderro = 2
            result.deserro = "#" & ex.Message
            result.infoerro = "Deleteitemchecklist:" & usuario & " " & item
            result.ex = ex
            result.SaveErro()
        End Try
        Context.Response.Write(js.Serialize(result))
    End Sub

    <WebMethod()>
    Public Sub SaveChecklist(usuario As String, checklist As String)
        Dim result As New CommonClass.erro
        Dim js As New JavaScriptSerializer
        js.MaxJsonLength = Int32.MaxValue
        Try
            Dim c As checklist = js.Deserialize(Of checklist)(checklist)
            cf.SaveCheckList(usuario, c)

            If cf.fbs.LogErr <> "" Then
                result.iderro = 1
                result.deserro = cf.fbs.LogErr
                result.infoerro = "Savechecklist:" & usuario & " " & checklist
                result.SaveErro()

            Else
                result.iderro = 0
                result.messagge = c.cab.des & " salvo com sucesso!"

            End If
        Catch ex As Exception
            result.iderro = 2
            result.deserro = "#" & ex.Message
            result.infoerro = "Savechecklist:" & usuario & " " & checklist
            result.ex = ex
            result.SaveErro()

        End Try
        Context.Response.Write(js.Serialize(result))
    End Sub

    <WebMethod()>
    Public Sub Savegrupochecklist(usuario As String, grupocheck As String)
        Dim result As New CommonClass.erro
        Dim js As New JavaScriptSerializer
        js.MaxJsonLength = Int32.MaxValue
        Try
            Dim u As grupochecklist = js.Deserialize(Of grupochecklist)(grupocheck)
            cf.Savegrupochecklist(usuario, u)

            If cf.fbs.LogErr <> "" Then
                result.iderro = 1
                result.deserro = cf.fbs.LogErr
                result.infoerro = "Savegrupochecklist:" & usuario & " " & grupocheck
                result.SaveErro()

            Else
                result.iderro = 0
                result.messagge = u.des & " salvo com sucesso!"

            End If
        Catch ex As Exception
            result.iderro = 2
            result.deserro = "#" & ex.Message
            result.infoerro = "Savecabchecklist:" & usuario & " " & grupocheck
            result.ex = ex
            result.SaveErro()

        End Try
        Context.Response.Write(js.Serialize(result))
    End Sub
    <WebMethod()>
    Public Sub Deletegrupochecklist(usuario As String, grupocheck As String)
        Dim result As New CommonClass.erro
        Dim js As New JavaScriptSerializer
        js.MaxJsonLength = Int32.MaxValue
        Try
            Dim u As grupochecklist = js.Deserialize(Of grupochecklist)(grupocheck)
            cf.Deletegrupochecklist(usuario, u)
            If cf.fbs.LogErr <> "" Then
                result.iderro = 1
                result.deserro = cf.fbs.LogErr
                result.infoerro = "DeleteCliente:" & usuario & " " & grupocheck
                result.SaveErro()
            Else
                result.iderro = 0
                result.messagge = u.des & " excluído com sucesso!"
            End If
        Catch ex As Exception
            result.iderro = 2
            result.deserro = "#" & ex.Message
            result.infoerro = "Deletegrupochecklist:" & usuario & " " & grupocheck
            result.ex = ex
            result.SaveErro()
        End Try
        Context.Response.Write(js.Serialize(result))
    End Sub

    <WebMethod()>
    Public Sub Savecabchecklist(usuario As String, cabcheck As String)
        Dim result As New CommonClass.erro
        Dim js As New JavaScriptSerializer
        js.MaxJsonLength = Int32.MaxValue
        Try
            Dim u As cabchecklist = js.Deserialize(Of cabchecklist)(cabcheck)
            cf.Savecabchecklist(usuario, u)

            If cf.fbs.LogErr <> "" Then
                result.iderro = 1
                result.deserro = cf.fbs.LogErr
                result.infoerro = "Savecabchecklist:" & usuario & " " & cabcheck
                result.SaveErro()

            Else
                result.iderro = 0
                result.messagge = u.des & " salvo com sucesso!"

            End If
        Catch ex As Exception
            result.iderro = 2
            result.deserro = "#" & ex.Message
            result.infoerro = "Savecabchecklist:" & usuario & " " & cabcheck
            result.ex = ex
            result.SaveErro()

        End Try
        Context.Response.Write(js.Serialize(result))
    End Sub
    <WebMethod()>
    Public Sub Deletecabchecklist(usuario As String, cabcheck As String)
        Dim result As New CommonClass.erro
        Dim js As New JavaScriptSerializer
        js.MaxJsonLength = Int32.MaxValue
        Try
            Dim u As cabchecklist = js.Deserialize(Of cabchecklist)(cabcheck)
            cf.Deletecabchecklist(usuario, u)
            If cf.fbs.LogErr <> "" Then
                result.iderro = 1
                result.deserro = cf.fbs.LogErr
                result.infoerro = "DeleteCliente:" & usuario & " " & cabcheck
                result.SaveErro()
            Else
                result.iderro = 0
                result.messagge = u.des & " excluído com sucesso!"
            End If
        Catch ex As Exception
            result.iderro = 2
            result.deserro = "#" & ex.Message
            result.infoerro = "Deletecabchecklist:" & usuario & " " & cabcheck
            result.ex = ex
            result.SaveErro()
        End Try
        Context.Response.Write(js.Serialize(result))
    End Sub

    <WebMethod()>
    Public Sub SaveCliente(cliente As String, infousuario As String)
        Dim result As New CommonClass.erro
        Dim js As New JavaScriptSerializer
        js.MaxJsonLength = Int32.MaxValue
        Try
            Dim u As Clientes = js.Deserialize(Of Clientes)(infousuario)
            cf.SaveCliente(cliente, u)

            If cf.fbs.LogErr <> "" Then
                result.iderro = 1
                result.deserro = cf.fbs.LogErr
                result.infoerro = "SaveCliente:" & cliente & " " & infousuario
                result.SaveErro()

            Else
                result.iderro = 0
                result.messagge = u.des & " salvo com sucesso!"

            End If
        Catch ex As Exception
            result.iderro = 2
            result.deserro = "#" & ex.Message
            result.infoerro = "SaveCliente:" & cliente & " " & infousuario
            result.ex = ex
            result.SaveErro()

        End Try
        Context.Response.Write(js.Serialize(result))
    End Sub
    <WebMethod()>
    Public Sub DeleteCliente(cliente As String, infousuario As String)
        Dim result As New CommonClass.erro
        Dim js As New JavaScriptSerializer
        js.MaxJsonLength = Int32.MaxValue
        Try
            Dim u As Clientes = js.Deserialize(Of Clientes)(infousuario)
            cf.DeleteCliente(cliente, u)
            If cf.fbs.LogErr <> "" Then
                result.iderro = 1
                result.deserro = cf.fbs.LogErr
                result.infoerro = "DeleteCliente:" & cliente & " " & infousuario
                result.SaveErro()
            Else
                result.iderro = 0
                result.messagge = u.des & " excluído com sucesso!"
            End If
        Catch ex As Exception
            result.iderro = 2
            result.deserro = "#" & ex.Message
            result.infoerro = "DeleteCliente:" & cliente & " " & infousuario
            result.ex = ex
            result.SaveErro()
        End Try
        Context.Response.Write(js.Serialize(result))
    End Sub
    <WebMethod()>
    Public Sub SaveJob(usuario As String, infojob As String)
        Dim result As New CommonClass.erro
        Dim js As New JavaScriptSerializer
        js.MaxJsonLength = Int32.MaxValue
        Try
            Dim u As Job = js.Deserialize(Of Job)(infojob)
            cf.SaveJob(usuario, u)
            If cf.fbs.LogErr <> "" Then
                result.iderro = 1
                result.deserro = cf.fbs.LogErr
                result.infoerro = "SaveJob:" & usuario & " " & infojob
                result.SaveErro()
            Else
                result.iderro = 0
                result.messagge = u.des & " Salvo!"
            End If
        Catch ex As Exception
            result.iderro = 2
            result.deserro = "#" & ex.Message
            result.infoerro = "SaveJob:" & usuario & " " & infojob
            result.ex = ex
            result.SaveErro()
        End Try
        Context.Response.Write(js.Serialize(result))
    End Sub
    <WebMethod()>
    Public Sub DeleteJob(usuario As String, infojob As String)
        Dim result As New CommonClass.erro
        Dim js As New JavaScriptSerializer
        js.MaxJsonLength = Int32.MaxValue
        Try
            Dim u As Job = js.Deserialize(Of Job)(infojob)
            cf.DeleteJob(usuario, u)
            If cf.fbs.LogErr <> "" Then
                result.iderro = 1
                result.deserro = cf.fbs.LogErr
                result.infoerro = "DeleteJob:" & usuario & " " & infojob
                result.SaveErro()
            Else
                result.iderro = 0
                result.messagge = u.des & " excluído com sucesso!"
            End If
        Catch ex As Exception
            result.iderro = 2
            result.deserro = "#" & ex.Message
            result.infoerro = "DeleteJob:" & usuario & " " & infojob
            result.ex = ex
            result.SaveErro()
        End Try
        Context.Response.Write(js.Serialize(result))
    End Sub
    <WebMethod()>
    Public Sub SaveUsuario(usuario As String, infousuario As String)
        Dim result As New CommonClass.erro
        Dim js As New JavaScriptSerializer
        js.MaxJsonLength = Int32.MaxValue
        Try
            Dim u As usuarios = js.Deserialize(Of usuarios)(infousuario)
            cf.SaveUsuario(usuario, u)
            If cf.fbs.LogErr <> "" Then
                result.iderro = 1
                result.deserro = cf.fbs.LogErr
                result.infoerro = "SaveUsuário:" & usuario & " " & infousuario
                result.SaveErro()
            Else
                result.iderro = 0
                result.messagge = u.des & " Salvo!"
            End If
        Catch ex As Exception
            result.iderro = 2
            result.deserro = "#" & ex.Message
            result.infoerro = "SaveCliente:" & usuario & " " & infousuario
            result.ex = ex
            result.SaveErro()
        End Try
        Context.Response.Write(js.Serialize(result))
    End Sub
    <WebMethod()>
    Public Sub DeleteUsuario(usuario As String, infousuario As String)
        Dim result As New CommonClass.erro
        Dim js As New JavaScriptSerializer
        js.MaxJsonLength = Int32.MaxValue
        Try
            Dim u As usuarios = js.Deserialize(Of usuarios)(infousuario)
            cf.DeleteUsuario(usuario, u)
            If cf.fbs.LogErr <> "" Then
                result.iderro = 1
                result.deserro = cf.fbs.LogErr
                result.infoerro = "DeleteUsuario:" & usuario & " " & infousuario
                result.SaveErro()
            Else
                result.iderro = 0
                result.messagge = u.des & " excluído com sucesso!"
            End If
        Catch ex As Exception
            result.iderro = 2
            result.deserro = "#" & ex.Message
            result.infoerro = "SaveCliente:" & usuario & " " & infousuario
            result.ex = ex
            result.SaveErro()
        End Try
        Context.Response.Write(js.Serialize(result))
    End Sub
    <WebMethod()>
    Public Sub SaveCarCab(usuario As String, carateristica As String)
        Dim result As New CommonClass.erro
        Dim js As New JavaScriptSerializer
        js.MaxJsonLength = Int32.MaxValue
        Try
            Dim car As CarCab = js.Deserialize(Of CarCab)(carateristica)
            cf.SaveCarCab(usuario, car)
            If cf.fbs.LogErr <> "" Then
                result.iderro = 1
                result.deserro = cf.fbs.LogErr
                result.infoerro = "SaveCarCab:" & usuario & " " & carateristica
                result.SaveErro()
            Else
                result.iderro = 0
                result.messagge = car.des & " salvo com sucesso!"
            End If
        Catch ex As Exception
            result.iderro = 2
            result.deserro = "#" & ex.Message
            result.infoerro = "SaveCarCab:" & usuario & " " & carateristica
            result.ex = ex
            result.SaveErro()
        End Try
        Context.Response.Write(js.Serialize(result))
    End Sub

    <WebMethod()>
    Public Sub DeleteCarCab(usuario As String, carateristica As String)
        Dim result As New CommonClass.erro
        Dim js As New JavaScriptSerializer
        js.MaxJsonLength = Int32.MaxValue
        Try
            Dim car As CarCab = js.Deserialize(Of CarCab)(carateristica)
            cf.DeleteCarCab(usuario, car)
            If cf.fbs.LogErr <> "" Then
                result.iderro = 1
                result.deserro = cf.fbs.LogErr
                result.infoerro = "DeleteCarCab:" & usuario & " " & carateristica
                result.SaveErro()
            Else
                result.iderro = 0
                result.messagge = car.des & " excluído com sucesso!"
            End If
        Catch ex As Exception
            result.iderro = 2
            result.deserro = "#" & ex.Message
            result.infoerro = "SaveCarCab:" & usuario & " " & carateristica
            result.ex = ex
            result.SaveErro()
        End Try
        Context.Response.Write(js.Serialize(result))
    End Sub

    <WebMethod()>
    Public Sub SaveCarOpc(usuario As String, opcao As String)
        Dim result As New CommonClass.erro
        Dim js As New JavaScriptSerializer
        js.MaxJsonLength = Int32.MaxValue
        Try
            Dim opc As CarOpc = js.Deserialize(Of CarOpc)(opcao)
            cf.SaveCarOpc(usuario, opc)
            If cf.fbs.LogErr <> "" Then
                result.iderro = 1
                result.deserro = cf.fbs.LogErr
                result.infoerro = "SaveCarOpc:" & usuario & " " & opcao
                result.SaveErro()
            Else
                result.iderro = 0
                result.messagge = opc.des & " salva com sucesso!"
            End If
        Catch ex As Exception
            result.iderro = 2
            result.deserro = "#" & ex.Message
            result.infoerro = "SaveCarOpc:" & usuario & " " & opcao
            result.ex = ex
            result.SaveErro()
        End Try
        Context.Response.Write(js.Serialize(result))
    End Sub
    <WebMethod()>
    Public Sub DeleteCarOpc(usuario As String, opcao As String)
        Dim result As New CommonClass.erro
        Dim js As New JavaScriptSerializer
        js.MaxJsonLength = Int32.MaxValue
        Try
            Dim opc As CarOpc = js.Deserialize(Of CarOpc)(opcao)
            cf.DeleteCarOpc(usuario, opc)
            If cf.fbs.LogErr <> "" Then
                result.iderro = 1
                result.deserro = cf.fbs.LogErr
                result.infoerro = "DeleteCarOpc:" & usuario & " " & opcao
                result.SaveErro()
            Else
                result.iderro = 0
                result.messagge = opc.des & " excluída com sucesso!"
            End If
        Catch ex As Exception
            result.iderro = 2
            result.deserro = "#" & ex.Message
            result.infoerro = "DeleteCarOpc:" & usuario & " " & opcao
            result.ex = ex
            result.SaveErro()
        End Try
        Context.Response.Write(js.Serialize(result))
    End Sub
    <WebMethod()>
    Public Sub SaveCarCat(usuario As String, categoria As String)
        Dim result As New CommonClass.erro
        Dim js As New JavaScriptSerializer
        js.MaxJsonLength = Int32.MaxValue
        Try
            Dim cat As CarCat = js.Deserialize(Of CarCat)(categoria)
            cf.SaveCarCat(usuario, cat)
            If cf.fbs.LogErr <> "" Then
                result.iderro = 1
                result.deserro = cf.fbs.LogErr
                result.infoerro = "SaveCarCat:" & usuario & " " & categoria
                result.SaveErro()
            Else
                result.iderro = 0
                result.messagge = cat.des & " salva com sucesso!"
            End If
        Catch ex As Exception
            result.iderro = 2
            result.deserro = "#" & ex.Message
            result.infoerro = "SaveCarCat:" & usuario & " " & categoria
            result.ex = ex
            result.SaveErro()
        End Try
        Context.Response.Write(js.Serialize(result))
    End Sub
    <WebMethod()>
    Public Sub DeleteCarCat(usuario As String, categoria As String)
        Dim result As New CommonClass.erro
        Dim js As New JavaScriptSerializer
        js.MaxJsonLength = Int32.MaxValue
        Try
            Dim cat As CarCat = js.Deserialize(Of CarCat)(categoria)
            cf.DeleteCarCat(usuario, cat)
            If cf.fbs.LogErr <> "" Then
                result.iderro = 1
                result.deserro = cf.fbs.LogErr
                result.infoerro = "DeleteCarCat:" & usuario & " " & categoria
                result.SaveErro()
            Else
                result.iderro = 0
                result.messagge = cat.des & " excluída com sucesso!"
            End If
        Catch ex As Exception
            result.iderro = 2
            result.deserro = "#" & ex.Message
            result.infoerro = "SaveCarCat:" & usuario & " " & categoria
            result.ex = ex
            result.SaveErro()
        End Try
        Context.Response.Write(js.Serialize(result))
    End Sub
    <WebMethod()>
    Public Sub ImportCarTxt(usuario As String, codcar As String)
        Try
            cf.ImportCarTxt(usuario, codcar)
            If cf.fbs.LogErr <> "" Then
                Context.Response.Write("#" & cf.fbs.LogErr)
            Else
                Context.Response.Write("Importação concluida com sucesso!")
            End If
        Catch ex As Exception
            cf.fbs.AddErr("#" & ex.Message)
            Context.Response.Write(cf.fbs.LogErr)
        End Try
    End Sub


End Class