<%@ Page Language="vb" AutoEventWireup="false" Inherits="proidea.asp" ValidateRequest="false" CodeFile="pageFunctions/pagesFunctionlogin.vb" EnableSessionState="True" %>

<script language="VB" runat="server"> 
    Protected Sub FaiLoginIniziale(ByVal sender As Object, ByVal e As System.EventArgs)
        If Username.Text <> "" And Password.Text <> "" Then
            Dim reslogin As String = VerificaLogin(Username.Text, Password.Text)
            If reslogin = "" Then
                Session("proidea_username") = Username.Text
                Session("proidea_username_acesso") = Username.Text
                Session("proidea_password") = Password.Text
                If IsNothing(Session("proidea_tempurl")) = False Then
                    Response.Redirect(Session("proidea_tempurl"))
                Else
                    Response.Redirect("base.aspx")
                End If
            Else
                label_login.Text = reslogin
                label_login.Visible = True
            End If
        Else
            label_login.Text = ""
        End If
    End Sub

    Sub EnviaSenhaPorEmail()

        If Username.Text <> "" Then
            Dim res As String = EnviaSenhaUsuario(Username.Text)
            If res = "" Then
                label_login.Text = "A senha fui enviada ao seu endereço de e-mail"
            Else
                label_login.Text = "Falha no envio da senha: " & res
            End If
        Else
            label_login.Text = "Inserir o nome do usuario"

        End If
        label_login.Visible = True
    End Sub
</script>



<!DOCTYPE html>
<html lang="pt-br">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="IdeaWork" content="Sistema para check-list">
    <meta author="Proidea Ltda">
    <title>IdeaWork</title>
    <!-- Required Stylesheets -->
    <link type="text/css" rel="stylesheet" href="css/bootstrap.min.css" />
    <link type="text/css" rel="stylesheet" href="CSS/custom_loginpage.css" />
    <link type="text/css" rel="stylesheet" href="CSS/background.css" />
    <link rel="shortcut icon" href="img/favicon-proidea.png">
    <link rel="icon" href="img/favicon-proidea.png">
</head>
<body>
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="wrap">
                    <form class="login" runat="server">
                        <div class="linha"></div>
                        <div class="logoAt" >
                            <img src="catalogos/<%=empresabase %>/img/logo-login.png" />
                        </div>
                        <div class="linha"></div>
                        <asp:TextBox type="text" placeholder="Usuario" runat="server" ID="Username" />
                        <asp:TextBox type="password" placeholder="Senha" runat="server" ID="Password" />
                        <asp:Button Text="Entrar" class="btn btn-proidea btn-sm" runat="server" OnClick="FaiLoginIniziale" />
                        <asp:Button runat="server" OnClick="EnviaSenhaPorEmail" Text="Esqueci a senha" CssClass="EsqueciASenha"></asp:Button>
                        <br />
                        <asp:Label runat="server" ID="label_login" class="row danger" Visible="false" />
                        
                    </form>
                </div>
            </div>
        </div>
        <div class="rodape row">
            <div class="traco col-sm-5"></div>
            <div class="logorodape col-sm-2"><a href="http://www.proidea.com.br" target="_blank">
                <img src="img/logo-rodape.png"></a></div>
            <div class="traco col-sm-5"></div>
        </div>
    </div>
</body>
</html>
