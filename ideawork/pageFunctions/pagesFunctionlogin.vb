Imports System.IO
Imports System.Configuration.ConfigurationManager
Imports System.Convert
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Web.UI
Imports System.Xml
Imports System.Web.Script.Serialization
Imports System.Data.sqlclient

Namespace proidea
    Partial Class Asp
        Inherits System.Web.UI.Page
        Public fbs As New FormaBase.FunzioniBase()
        Public empresabase As String
        Public emailfinanceirocliente As String = ""
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            fbs.ClearErr()
            ' UCase(Request.Url.AbsoluteUri.Split("/")(2))
            If UCase(Request.Url.AbsoluteUri.Split("/")(2).Split(".")(0)) = "UMINUM" Then
                empresabase = "UMINUM"
            Else
                empresabase = "PROIDEA"
            End If
            If AppSettings("empresateste") <> "" Then
                empresabase = AppSettings("empresateste")
            End If
            Session.Timeout = 480
            fbs.connString = Replace(AppSettings("ConnessioneDB"), "$(empresabase)", empresabase)
            fbs.defaultdatabase = empresabase
            fbs.IsSite = True
            fbs.TipoDatabase = AppSettings("TipoDatabase")
            fbs.mail_password = AppSettings("mail_password")
            fbs.mail_sender = "Configurador2d"
            fbs.mail_username = AppSettings("mail_utente")
            fbs.mail_smtp = AppSettings("mail_smtp")
            fbs.mail_porta = fbs.cnum(AppSettings("mail_porta"))
            emailfinanceirocliente = AppSettings("emailfinanceirocliente")
        End Sub

        Function EnviaSenhaUsuario(username As String) As String
            Dim res As String = ""
            fbs.OpenDatabase()
            Dim senhausuario As String = fbs.GetQueryValue(String.Format("select senha from usuarios where (cod='{0}') ", username))
            Dim emailusuario As String = fbs.GetQueryValue(String.Format("select email from usuarios where (cod='{0}') ", username))
            If senhausuario <> "" And emailusuario.IndexOf("@") > 0 Then
                fbs.SendMail("suporte@proidea.com.br", emailusuario, "Envio senha ideawork", "A sua senha é: " & senhausuario)
                res = fbs.LogErr
            ElseIf senhausuario = "" Then
                res = "Usuario não encontrado. Contatar " & empresabase
            ElseIf emailusuario = "" Then
                res = "Usuario não tem email cadastrada. Contatar " & empresabase
            End If
            fbs.CloseDatabase()
            Return res
        End Function
        Function VerificaLogin(username As String, passwordCheck As String) As String
            fbs.OpenDatabase()

            Dim res As String = ""
            Response.Cookies("proidea_username_acesso").Expires = DateTime.Now.AddDays(-1)
            Response.Cookies("proidea_empresabase").Expires = DateTime.Now.AddDays(-1)
            Dim cookie As New HttpCookie("proidea_username_acesso", username)
            Dim cookie2 As New HttpCookie("proidea_empresabase", empresabase)
            cookie.Expires = DateTime.Now.AddDays(1)
            cookie2.Expires = DateTime.Now.AddDays(1)

            Response.AppendCookie(cookie)
            Response.AppendCookie(cookie2)
            Dim lg As New logfunction()
            Dim lgdados As New CommonClass.logeventos
            lgdados.usuario = username
            lgdados.nomeevento = "acesso"
            lgdados.chaveevento = username

            Dim password As String = fbs.GetQueryValue(String.Format("select senha from usuarios where (cod='{0}') ", username))
            If Trim(UCase(password)) <> Trim(UCase(passwordCheck)) Or password = "" Or passwordCheck = "" Then
                res = "Usuário ou senha inválido!"
                lgdados.obs = "Tentativa de acesso ao sistema com dados invalidos"
            Else
                ' carica parametri sessione

                Dim tipo As Integer = fbs.cnum(fbs.GetQueryValue(String.Format("select tipo from usuarios where (cod='{0}') ", username)))
                Dim ativo As Integer = fbs.cnum(fbs.GetQueryValue(String.Format("select ativo from usuarios where (cod='{0}') ", username)))
                If ativo <= 0 Then
                    res = "Usuario Inativo!"
                    lgdados.obs = "Tentativa de acesso ao sistema com usuario inativo"
                Else
                    Session("proidea_des_username") = fbs.GetQueryValue(String.Format("select des from usuarios where (cod='{0}') ", username))
                    If tipo < 100 Then
                        Session("proidea_isusuario_interno") = 1
                    Else
                        Session("proidea_isusuario_interno") = 0
                    End If
                    lgdados.obs = "Acesso ao sistema efetuado com sucesso"
                End If
            End If
            lg.WriteLogEventos(lgdados)
            fbs.CloseDatabase()
            Return res
        End Function
    End Class
End Namespace