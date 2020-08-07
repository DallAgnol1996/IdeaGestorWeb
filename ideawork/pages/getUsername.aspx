<%@ Page Language="vb" AutoEventWireup="false" Inherits="proidea.asp" ValidateRequest="false" CodeFile="../pageFunctions/pagesFunctionnologin.vb" EnableSessionState="True" %>

<%
    If IsNothing(Session("proidea_username")) = True Then
        Response.Write("")
    Else
        Response.Write(Session("proidea_username"))
    End If

%>
