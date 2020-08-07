<%@ Page Language="vb" AutoEventWireup="false" Inherits="proidea.asp" ValidateRequest="false" CodeFile="../pageFunctions/pagesFunctionnologin.vb" EnableSessionState="True" %>

<script language="VB" runat="server"> 


    Protected Sub CriaBanco()
        fbs.OpenDatabase()

        Dim Fbt As New FormaBase.FTableEdit

        ' ********************* anexos ************************************
        Dim c() As FormaBase.FTableEdit.tcol = {}
        Dim k() As FormaBase.FTableEdit.tkey = {}
        Dim tp As New FormaBase.FTableEdit.tcolType
        Dim tb As New FormaBase.FTableEdit.FTable
        ReDim Preserve c(5)
        c(0) = New FormaBase.FTableEdit.tcol("cod", tp.tnvarchar(50, fbs.TipoDatabase), 50, "")
        c(1) = New FormaBase.FTableEdit.tcol("des", tp.tnvarchar(200, fbs.TipoDatabase), 50, "")
        c(2) = New FormaBase.FTableEdit.tcol("senha", tp.tnvarchar(50, fbs.TipoDatabase), 50, "")
        c(3) = New FormaBase.FTableEdit.tcol("tipo", tp.tinteger(fbs.TipoDatabase), 20, 0)
        c(4) = New FormaBase.FTableEdit.tcol("ativo", tp.tinteger(fbs.TipoDatabase), 20, 0)
        c(5) = New FormaBase.FTableEdit.tcol("email", tp.tnvarchar(200, fbs.TipoDatabase), 20, "")

        ReDim Preserve k(0)
        k(0) = New FormaBase.FTableEdit.tkey("cod", 0, "")
        tb = Fbt.CreateTable("USUARIOS", c, k, fbs)

        ReDim Preserve c(8)
        c(0) = New FormaBase.FTableEdit.tcol("ID", tp.tinteger(fbs.TipoDatabase), 0, 0)
        c(1) = New FormaBase.FTableEdit.tcol("USUARIO", tp.tnvarchar(200, fbs.TipoDatabase), 0, "")
        c(2) = New FormaBase.FTableEdit.tcol("NOMETABELA", tp.tnvarchar(200, fbs.TipoDatabase), 0, "")
        c(3) = New FormaBase.FTableEdit.tcol("CHAVETABELA", tp.tnvarchar(500, fbs.TipoDatabase), 0, "")
        c(4) = New FormaBase.FTableEdit.tcol("ACAO", tp.tnvarchar(200, fbs.TipoDatabase), 0, "")
        c(5) = New FormaBase.FTableEdit.tcol("COLUNA", tp.tnvarchar(20, fbs.TipoDatabase), 0, "")
        c(6) = New FormaBase.FTableEdit.tcol("VALORANTIGO", tp.tnvarchar("MAX", fbs.TipoDatabase), 0, "")
        c(7) = New FormaBase.FTableEdit.tcol("VALORNOVO", tp.tnvarchar("MAX", fbs.TipoDatabase), 0, "")
        c(8) = New FormaBase.FTableEdit.tcol("DATA", tp.tnvarchar(20, fbs.TipoDatabase), 0, "")


        ReDim Preserve k(0)
        k(0) = New FormaBase.FTableEdit.tkey("ID", 0, "")

        tb = Fbt.CreateTable("LOGCADASTROS", c, k, fbs)


        ReDim Preserve c(5)
        c(0) = New FormaBase.FTableEdit.tcol("ID", tp.tinteger(fbs.TipoDatabase), 0, 0)
        c(1) = New FormaBase.FTableEdit.tcol("USUARIO", tp.tnvarchar(200, fbs.TipoDatabase), 0, "")
        c(2) = New FormaBase.FTableEdit.tcol("DATA", tp.tnvarchar(20, fbs.TipoDatabase), 0, "")
        c(3) = New FormaBase.FTableEdit.tcol("NOMEEVENTO", tp.tnvarchar(500, fbs.TipoDatabase), 0, "")
        c(4) = New FormaBase.FTableEdit.tcol("CHAVEEVENTO", tp.tnvarchar(500, fbs.TipoDatabase), 0, "")
        c(5) = New FormaBase.FTableEdit.tcol("OBS", tp.tnvarchar("MAX", fbs.TipoDatabase), 0, "")

        ReDim Preserve k(0)
        k(0) = New FormaBase.FTableEdit.tkey("ID", 0, "")

        tb = Fbt.CreateTable("LOGEVENTOS", c, k, fbs)



        ReDim Preserve c(6)
        c(0) = New FormaBase.FTableEdit.tcol("COD", tp.tnvarchar(50, fbs.TipoDatabase), 0, "")
        c(1) = New FormaBase.FTableEdit.tcol("DES", tp.tnvarchar(200, fbs.TipoDatabase), 0, "")
        c(2) = New FormaBase.FTableEdit.tcol("IMG", tp.tnvarchar(500, fbs.TipoDatabase), 0, "")
        c(3) = New FormaBase.FTableEdit.tcol("CAMPOS", tp.tnvarchar("MAX", fbs.TipoDatabase), 0, "")
        c(4) = New FormaBase.FTableEdit.tcol("CARREF", tp.tnvarchar(30, fbs.TipoDatabase), 0, "")
        c(5) = New FormaBase.FTableEdit.tcol("TIPOPREVIEW", tp.tnvarchar(10, fbs.TipoDatabase), 0, "")
        c(6) = New FormaBase.FTableEdit.tcol("TIPO", tp.tnvarchar(10, fbs.TipoDatabase), 0, "")

        ReDim Preserve k(0)
        k(0) = New FormaBase.FTableEdit.tkey("COD", 0, "")
        tb = Fbt.CreateTable("CARCAB", c, k, fbs)

        ReDim Preserve c(3)
        c(0) = New FormaBase.FTableEdit.tcol("CODCAR", tp.tnvarchar(50, fbs.TipoDatabase), 0, "")
        c(1) = New FormaBase.FTableEdit.tcol("COD", tp.tnvarchar(50, fbs.TipoDatabase), 0, "")
        c(2) = New FormaBase.FTableEdit.tcol("DES", tp.tnvarchar(200, fbs.TipoDatabase), 0, "")
        c(3) = New FormaBase.FTableEdit.tcol("IMG", tp.tnvarchar(200, fbs.TipoDatabase), 0, "")

        ReDim Preserve k(1)
        k(0) = New FormaBase.FTableEdit.tkey("CODCAR", 0, "")
        k(1) = New FormaBase.FTableEdit.tkey("COD", 1, "")
        tb = Fbt.CreateTable("CARCAT", c, k, fbs)

        ReDim Preserve c(9)
        c(0) = New FormaBase.FTableEdit.tcol("CODCAR", tp.tnvarchar(50, fbs.TipoDatabase), 0, "")
        c(1) = New FormaBase.FTableEdit.tcol("COD", tp.tnvarchar(50, fbs.TipoDatabase), 0, "")
        c(2) = New FormaBase.FTableEdit.tcol("CAT", tp.tnvarchar(30, fbs.TipoDatabase), 0, "")
        c(3) = New FormaBase.FTableEdit.tcol("DES", tp.tnvarchar(200, fbs.TipoDatabase), 0, "")
        c(4) = New FormaBase.FTableEdit.tcol("IMG", tp.tnvarchar(200, fbs.TipoDatabase), 0, "")
        c(5) = New FormaBase.FTableEdit.tcol("CODITEM", tp.tnvarchar(50, fbs.TipoDatabase), 0, "")
        c(6) = New FormaBase.FTableEdit.tcol("ORD", tp.tinteger(fbs.TipoDatabase), 0, 0)
        c(7) = New FormaBase.FTableEdit.tcol("LINKDOC", tp.tnvarchar(200, fbs.TipoDatabase), 0, "")
        c(8) = New FormaBase.FTableEdit.tcol("CAMPOS", tp.tnvarchar("MAX", fbs.TipoDatabase), 0, "")
        c(9) = New FormaBase.FTableEdit.tcol("ATIVO", tp.tinteger(fbs.TipoDatabase), 0, 1)


        ReDim Preserve k(1)
        k(0) = New FormaBase.FTableEdit.tkey("CODCAR", 0, "")
        k(1) = New FormaBase.FTableEdit.tkey("COD", 1, "")
        tb = Fbt.CreateTable("CAROPC", c, k, fbs)
        fbs.CloseDatabase()

        ReDim Preserve c(3)
        c(0) = New FormaBase.FTableEdit.tcol("cod", tp.tnvarchar(50, fbs.TipoDatabase), 50, "")
        c(1) = New FormaBase.FTableEdit.tcol("des", tp.tnvarchar(200, fbs.TipoDatabase), 50, "")
        c(2) = New FormaBase.FTableEdit.tcol("idref", tp.tnvarchar(50, fbs.TipoDatabase), 50, "")
        c(3) = New FormaBase.FTableEdit.tcol("email", tp.tnvarchar(200, fbs.TipoDatabase), 20, "")

        ReDim Preserve k(0)
        k(0) = New FormaBase.FTableEdit.tkey("cod", 0, "")
        tb = Fbt.CreateTable("CLIENTES", c, k, fbs)

        ReDim Preserve c(10)
        c(0) = New FormaBase.FTableEdit.tcol("cod", tp.tnvarchar(50, fbs.TipoDatabase), 50, "")
        c(1) = New FormaBase.FTableEdit.tcol("idCliente", tp.tnvarchar(50, fbs.TipoDatabase), 50, "")
        c(2) = New FormaBase.FTableEdit.tcol("idUsuario", tp.tnvarchar(50, fbs.TipoDatabase), 50, "")
        c(3) = New FormaBase.FTableEdit.tcol("usuarioCriacao", tp.tnvarchar(20, fbs.TipoDatabase), 20, "")
        c(4) = New FormaBase.FTableEdit.tcol("dataHoraCriacao", tp.tnvarchar(20, fbs.TipoDatabase), 20, "")
        c(5) = New FormaBase.FTableEdit.tcol("dataJob", tp.tnvarchar(20, fbs.TipoDatabase), 20, "")
        c(6) = New FormaBase.FTableEdit.tcol("horaJob", tp.tnvarchar(20, fbs.TipoDatabase), 20, "")
        c(7) = New FormaBase.FTableEdit.tcol("idStatus", tp.tinteger(fbs.TipoDatabase), 20, 0)
        c(8) = New FormaBase.FTableEdit.tcol("obs", tp.tnvarchar(500, fbs.TipoDatabase), 500, "")
        c(9) = New FormaBase.FTableEdit.tcol("des", tp.tnvarchar(500, fbs.TipoDatabase), 500, "")
        c(10) = New FormaBase.FTableEdit.tcol("codCheckList", tp.tinteger(fbs.TipoDatabase), 20, 0)

        ReDim Preserve k(0)
        k(0) = New FormaBase.FTableEdit.tkey("cod", 0, "")
        tb = Fbt.CreateTable("JOB", c, k, fbs)


        ReDim Preserve c(3)
        c(0) = New FormaBase.FTableEdit.tcol("cod", tp.tinteger(fbs.TipoDatabase), 20, 0)
        c(1) = New FormaBase.FTableEdit.tcol("des", tp.tnvarchar(200, fbs.TipoDatabase), 50, "")
        c(2) = New FormaBase.FTableEdit.tcol("obs", tp.tnvarchar(50, fbs.TipoDatabase), 50, "")
        c(3) = New FormaBase.FTableEdit.tcol("ativo", tp.tinteger(fbs.TipoDatabase), 20, 0)

        ReDim Preserve k(0)
        k(0) = New FormaBase.FTableEdit.tkey("cod", 0, "")
        tb = Fbt.CreateTable("CABCHECKLIST", c, k, fbs)

        ReDim Preserve c(3)
        c(0) = New FormaBase.FTableEdit.tcol("codchecklist", tp.tinteger(fbs.TipoDatabase), 20, 0)
        c(1) = New FormaBase.FTableEdit.tcol("id", tp.tinteger(fbs.TipoDatabase), 20, 0)
        c(2) = New FormaBase.FTableEdit.tcol("des", tp.tnvarchar(50, fbs.TipoDatabase), 50, "")
        c(3) = New FormaBase.FTableEdit.tcol("ord", tp.tinteger(fbs.TipoDatabase), 20, 0)

        ReDim Preserve k(1)
        k(0) = New FormaBase.FTableEdit.tkey("codchecklist", 0, "")
        k(1) = New FormaBase.FTableEdit.tkey("id", 1, "")
        tb = Fbt.CreateTable("GRUPOCHECKLIST", c, k, fbs)


        ReDim Preserve c(6)
        c(0) = New FormaBase.FTableEdit.tcol("codchecklist", tp.tinteger(fbs.TipoDatabase), 20, 0)
        c(1) = New FormaBase.FTableEdit.tcol("idgrupo", tp.tinteger(fbs.TipoDatabase), 20, 0)
        c(2) = New FormaBase.FTableEdit.tcol("id", tp.tinteger(fbs.TipoDatabase), 20, 0)
        c(3) = New FormaBase.FTableEdit.tcol("ord", tp.tinteger(fbs.TipoDatabase), 20, 0)
        c(4) = New FormaBase.FTableEdit.tcol("des", tp.tnvarchar(50, fbs.TipoDatabase), 50, "")
        c(5) = New FormaBase.FTableEdit.tcol("referencia", tp.tnvarchar(50, fbs.TipoDatabase), 50, "")
        c(6) = New FormaBase.FTableEdit.tcol("fotoobrigatoria", tp.tinteger(fbs.TipoDatabase), 20, 0)


        ReDim Preserve k(0)
        k(0) = New FormaBase.FTableEdit.tkey("codchecklist", 0, "")
        k(1) = New FormaBase.FTableEdit.tkey("idgrupo", 1, "")
        k(2) = New FormaBase.FTableEdit.tkey("id", 2, "")
        tb = Fbt.CreateTable("ITENSCHECKLIST", c, k, fbs)

    End Sub


</script>

<%
    CriaBanco()
    If fbs.LogErr <> "" Then
        Response.Write(fbs.LogErr)
    Else
        Response.Write("ok")
    End If


%>

