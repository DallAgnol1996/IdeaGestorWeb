<%@ Page Language="vb" AutoEventWireup="false" Inherits="proidea.asp" ValidateRequest="false" CodeFile="../pageFunctions/pagesFunction.vb" EnableSessionState="True" %>
<%
    Dim idanexoexcluir As Integer = fbs.cnum(Request("idanexoexcluir"))
    If idanexoexcluir > 0 Then
        fbs.OpenDatabase()
        fbs.ExecuteSql("delete From anexos where id=" & idanexoexcluir)
        fbs.CloseDatabase()
    End If
     %>
<script>
    function fechadivpopup() {
        $("#divpopup").html("");
        $("#divpopup").hide("slow");
    }
    </script>
    <div class="col-sm-12 padding-10 text-center ">
        <button class="btn btn-proidea pull-right" onclick="javascript:fechadivpopup()" id="btn-sair-img">Fechar</button>
    </div>
<iframe frameborder="0" width="100%" height="650px" src="pages/upload_html.aspx<%=Me.Context.Request.Url.Query %>"></iframe>   
