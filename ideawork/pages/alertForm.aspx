<%@ Page Language="vb" AutoEventWireup="false" Inherits="proidea.asp" ValidateRequest="false" CodeFile="../pageFunctions/pagesFunction.vb" EnableSessionState="True" %>

<%

    Dim mess As String = Replace(Request("alertmessage"), "/n", "<br>")
    Dim voltainicio As Integer = fbs.cnum(Request("voltainicio"))
    Dim fclose As String = "CloseDivpopup()"
    If voltainicio = 1 Then
        fclose = "GoToDefault()"
    End If
     %>
<div>
    <p><%=mess %></p>
    <p><button type="submit" onclick="<%=fclose %>" class="btn btn-formasoft pull-right " >Fechar</button></p>
</div>
