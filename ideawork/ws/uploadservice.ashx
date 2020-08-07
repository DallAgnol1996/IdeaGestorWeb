<%@ WebHandler Language="VB" Class="uploadservice" %>

Imports System.Web
Imports System.IO

Public Class uploadservice : Implements IHttpHandler
    Dim cf As New commonfunction
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If context.Request.Files.Count > 0 Then

            Dim files As HttpFileCollection = context.Request.Files
            Dim i As Integer = 0
            Dim file As HttpPostedFile = files(i)
            Dim fname As String = ""
            If HttpContext.Current.Request.Browser.Browser.ToUpper() = "IE" OrElse HttpContext.Current.Request.Browser.Browser.ToUpper() = "INTERNETEXPLORER" Then
                Dim testfiles As String() = file.FileName.Split(New Char() {"\"c})
                fname = testfiles(testfiles.Length - 1)
            Else
                fname = file.FileName
            End If
            If context.Request("nomefile") <> "" Then
                If context.Request("nomefile").IndexOf(".") <= 0 Then
                    fname = context.Request("nomefile") & System.IO.Path.GetExtension(fname)
                Else
                    fname = context.Request("nomefile")
                End If
            End If
            Dim pastasalva As String = context.Request("pastasalva")
            If pastasalva.IndexOf(":") <= 0 Then
                pastasalva = getdir(context.Request("pastasalva"))
            End If
            fname = Path.Combine(pastasalva, fname)
            file.SaveAs(fname)
            context.Response.ContentType = "Text"
            context.Response.Write(System.IO.Path.GetFileName(fname))
        End If

    End Sub
    Function getdir(pastasalva As String) As String
        Dim d As String = System.Configuration.ConfigurationManager.AppSettings("CartellaCatalogos")
        d = d & "\" & cf.empresabase & "\" & pastasalva
        If System.IO.Directory.Exists(d) = False Then
            Microsoft.VisualBasic.FileSystem.MkDir(d)
        End If
        Return d
    End Function
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
End Class