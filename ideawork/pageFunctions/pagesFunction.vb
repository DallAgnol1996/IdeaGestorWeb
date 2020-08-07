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
        Public cartelladown As String = ""
        Public cartellacatalogo As String = ""
        Public cartellasito As String = ""
        Public OdbcDr As sqlDataReader
        Public OdbcDr2 As sqlDataReader
        Public emailfinanceirocliente As String = ""
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            fbs.ClearErr()
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
            cartelladown = AppSettings("CartellaDownloadSito")

            cartellasito = AppSettings("CartellaSito")
            cartellacatalogo = Replace(cartellasito & "\catalogos\" & empresabase & "\", "\\", "\")
            emailfinanceirocliente = AppSettings("emailfinanceirocliente")
            If IsNothing(Session("proidea_username")) = True Then
                Session("proidea_tempurl") = Request.Url.AbsoluteUri
                Response.Redirect("/")
            Else
                Session("proidea_tempurl") = Nothing
            End If
        End Sub
    End Class
End Namespace