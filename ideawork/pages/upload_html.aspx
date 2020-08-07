<%@ Page Language="vb" AutoEventWireup="false" Inherits="proidea.asp" ValidateRequest="false" CodeFile="../pageFunctions/pagesFunction.vb" EnableSessionState="True" %>


<script runat="server">


    Dim anexo As New CommonClass.anexos

    Function getdirAnexos() As String
        Dim d As String = System.Configuration.ConfigurationManager.AppSettings("CartellaDownloadsito")
        If anexo.pastasave <> "" Then
            d = System.Configuration.ConfigurationManager.AppSettings("CartellaCatalogos")
            d = d & "\" & empresabase & "\" & anexo.pastasave
        Else
            d = d & "\" & empresabase & "\" & anexo.tipo & "_" & anexo.ref1 & "\"
        End If

        Return d
    End Function
    Sub LoadClassAnexo()
        anexo.id = fbs.cnum(Request("id"))
        anexo.tipo = fbs.cnum(Request("tipo"))
        anexo.ref1 = Request("ref1")
        anexo.ref2 = Request("ref2")
        anexo.ref3 = Request("ref3")
        anexo.ref4 = Request("ref4")
        anexo.pastasave = Request("pastasave")
        If IsNothing(anexo.pastasave) = True Then anexo.pastasave = ""
        anexo.codusuario = Session("proidea_username")
    End Sub
    Protected Sub upload(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim x, y As Object
        Dim i As Long
        LoadClassAnexo()
        Dim controlli() As Object = New Object() {FileUp1}
        Dim xLbl() As Object = New Object() {Lb1}
        Dim dTmp As String = getdirAnexos()
        For i = 0 To controlli.Length - 1
            x = controlli(i)
            y = xLbl(i)
            Try
                If System.IO.Directory.Exists(dTmp) = False Then
                    MkDir(dTmp)
                End If
                fbs.OpenDatabase()

                Dim nomefileas As String = dTmp & x.FileName
                Dim nomefile As String = System.IO.Path.GetFileNameWithoutExtension(nomefileas)
                If System.IO.File.Exists(nomefileas) = True Then
                    nomefile = nomefile & "_" & fbs.fData(DateTime.Now()) & System.IO.Path.GetExtension(nomefileas)
                    nomefileas = dTmp & nomefile
                End If
                x.SaveAs(nomefileas)
                If anexo.pastasave = "" Then
                    anexo.arquivo = "../" & UnMapPath(nomefileas)
                    Try
                        fbs.OpenDatabase()
                        anexo.id = fbs.cnum(fbs.GetQueryValue("select TOP 1  id from anexos order by id desc ")) + 1
                        anexo.data = fbs.fData(DateTime.Now())
                        Dim sql As String = ""
                        sql = "INSERT INTO anexos (id,tipo,ref1,ref2,ref3,ref4,Arquivo,data,codusuario) "
                        sql = sql & " VALUES "
                        sql = sql & " ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')"
                        With anexo
                            sql = String.Format(sql, .id, .tipo, .ref1, .ref2, .ref3, .ref4, .arquivo, .data, .codusuario)
                        End With
                        fbs.ExecuteSql(sql)
                    Catch ex As Exception
                        fbs.AddErr("SaveAnexos:" & ex.Message)
                    End Try
                    If fbs.LogErr <> "" Then
                        y.text = fbs.LogErr
                    Else
                        y.text = "ok"
                    End If
                End If

            Catch ex As Exception
                y.Text = "Error: " & ex.Message.ToString()
            End Try
        Next
        fbs.CloseDatabase()
    End Sub

    Function UnMapPath(percorso As String)
        Dim sRoot As String, sPath As String
        sRoot = Server.MapPath("/")
        sPath = Mid(percorso, Len(sRoot) + 1)
        Return Replace(sPath, "\", "/")
    End Function

    Protected Sub loadupload()
        LoadClassAnexo()
        Dim i As Integer = 0

        Dim filtroanexos As String = " where id like '%' "
        If anexo.id <> 0 Then filtroanexos = filtroanexos & " and id=" & anexo.id
        If anexo.ref1 <> "" Then filtroanexos = filtroanexos & " and ref1='" & anexo.ref1 & "' "
        If anexo.ref2 <> "" Then filtroanexos = filtroanexos & " and ref2='" & anexo.ref2 & "' "
        If anexo.ref3 <> "" Then filtroanexos = filtroanexos & " and ref3='" & anexo.ref3 & "' "
        If anexo.ref4 <> "" Then filtroanexos = filtroanexos & " and ref4='" & anexo.ref4 & "' "
        Dim an() As CommonClass.anexos
        an = Nothing
        fbs.OpenDatabase()
        Dim rc As System.Data.sqlclient.sqlDataReader
        rc = fbs.GetQueryRecordsetsql(String.Format("Select * from anexos {0}", filtroanexos))

        While rc.Read()
            ReDim Preserve an(i)
            an(i) = New CommonClass.anexos
            With an(i)
                .id = rc.Item("id").ToString
                .tipo = rc.Item("id").ToString
                .ref1 = rc.Item("ref1").ToString
                .ref2 = rc.Item("ref2").ToString
                .ref3 = rc.Item("ref3").ToString
                .ref4 = rc.Item("ref4").ToString
                .arquivo = rc.Item("arquivo").ToString
                .codusuario = rc.Item("codusuario").ToString
                .data = rc.Item("data").ToString
                .desdata = fbs.rData(.data)
                .arquivorootbase = Replace(.arquivo, "../", "")
                .nomearquivo = System.IO.Path.GetFileName(.arquivo)
            End With
            i = i + 1
        End While
        rc.Close()


        If IsNothing(an) = False Then
            For i = 0 To UBound(an)
                Dim r As New HtmlTableRow()
                Dim c1 As New HtmlTableCell()
                Dim c2 As New HtmlTableCell()
                c1.InnerHtml = String.Format("<a href='{0}' target='_blank'>{1}</a>", an(i).arquivo, System.IO.Path.GetFileName(an(i).arquivo))
                r.Cells.Add(c1)
                c2.InnerHtml = String.Format("<button  class=""btn btn-default"" onclick=""javascript:excluiranexo({0})"" ><span class=""glyphicon glyphicon-trash""></span></button>", an(i).id)
                r.Cells.Add(c2)
                r.ID = "anexo_" & an(i).id
                attachstable.Rows.Add(r)
            Next
        End If
        If fbs.LogErr <> "" Then
            Lb1.Text = fbs.LogErr
        End If
        fbs.CloseDatabase()
    End Sub
</script>
<%

    loadupload()
%>


<html>
<head>
    <title>Upload Files</title>
    <!-- Bootstrap core CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous">
    <link href="../css/proideabase.css" rel="stylesheet">
</head>
<body runat="server" class="bodybranco">
    <div class="row">
        <div class="col-sm-12">
            <h3 class="align-content-center aneximg">ANEXAR IMAGENS / DOCUMENTOS</h3>
        </div>
    </div>
  <div class="boxupload">
    <div class="col-sm-12 row contenanexo">

            <table class="col-sm-12 table table-bordered table-hover table-striped" id="attachstable" runat="server">
                <tr>
                    <th class="col-sm-10">NOME ANEXO</th>
                    <th class="col-sm-2">EXCLUIR</th>
                </tr>
            </table>

    </div>

    <form id="form_upload" runat="server" class="form">
        <div class="row">
            <div class="col-sm-12 pull-right">
                    <asp:FileUpload ID="FileUp1" runat="server" />
            </div>
        </div>
</div>          
        <asp:Button runat="server" OnClick="upload" Text="Adicionar arquivo" class="col-sm-2 btn btn-proidea" />
        <div class="col-sm-12 form-group">
            <asp:Label for="inputName3" runat="server" ID="Lb1" class="col-sm-12 control-label logtext"></asp:Label>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script>
    <script src="../js/upload.js"></script>
</body>
</html>

