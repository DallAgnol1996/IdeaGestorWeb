<%@ Page Language="vb" AutoEventWireup="false" Inherits="proidea.asp" ValidateRequest="false" CodeFile="pageFunctions/pagesFunction.vb" EnableSessionState="True" %>

<!DOCTYPE html>
<html lang="pt-br">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="IdeaWork" content="IdeaWork">
    <meta author="Proidea Ltda">
    <title>IdeaWork</title>

    <!-- Required Stylesheets -->
    <link href="css/bootstrap.min.css" type="text/css" rel="stylesheet">
    <link href="css/sidebar.css" type="text/css" rel="stylesheet">
    <link href="css/style.css" type="text/css" rel="stylesheet">
    <link href="css/proidea.css" type="text/css" rel="stylesheet">
    <link rel="shortcut icon" href="img/favicon-proidea.png">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <!-- Required scripts -->

    <script src="js/vue.min.js"></script>
    <script src="js/vue-router.min.js"></script>
    <script src="js/httpVueLoader.min.js"></script>


    <script src="js/jquery-3.3.1.min.js"></script>
    <script src="js/jquery-ui.min.js"></script>
    <script src="js/bootstrap.min.js"></script>

</head>
<body class="bodyinicial">
    <!-- Our application root element -->
    <div id="infogeral" style="display: none">
        <input type="text" id="username" value="<%=Session("proidea_username") %>" />
        <input type="text" id="usernameacesso" value="<%=Session("proidea_username_acesso") %>" />
        <input type="text" id="desusername" value="<%=Session("proidea_des_username") %>" />
        <input type="text" id="isusuariointerno" value="<%=Session("proidea_isusuario_interno") %>" />
        <input type="text" id="empresabase" value="<%=empresabase %>" />
    </div>
    <div id="app">
        <vp-navbar></vp-navbar>
        <router-view></router-view>
    </div>
    <footer style="display: none">
        <div class=" row rodape page-footer ">
            <div class="traco col-sm-5"></div>
            <div class="logorodape col-sm-2">
                <a href="http://www.proidea.com.br" target="_blank">
                    <img src="catalogos/<%=empresabase %>/img/rodape.png"></a>
            </div>
            <div class="traco col-sm-5"></div>
        </div>
    </footer>
    <div id="divpopup" class="divpopupclass"></div>
    <script src="js/enginebase.min.js"></script>
    <script src="js/util.js"></script>
</body>
</html>
