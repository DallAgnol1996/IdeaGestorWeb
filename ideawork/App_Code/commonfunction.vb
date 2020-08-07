Imports CommonClass
Imports logfunction
Imports System.Configuration.ConfigurationManager
Imports Microsoft.VisualBasic
Imports System.Text
Imports System.IO
Imports System.Net
Imports System.Xml
Imports System.Xml.Linq
Imports System.Web
Imports System.Data.SQLite
Imports System.Reflection
Imports System.Web.Script.Serialization


Public Class commonfunction

    Public fbs As FormaBase.FunzioniBase
    Public lg As New logfunction
    Public FolderEmail As String = ""
    Public FolderDown As String = ""
    Public FolderSite As String = ""
    Public empresabase As String = ""

    Public Sub New()
        fbs = New FormaBase.FunzioniBase()
        fbs.ClearErr()
        Try
            empresabase = HttpContext.Current.Request.Cookies("PROIDEA_EMPRESABASE").Value.ToString
        Catch ex As Exception
            empresabase = ""
        End Try
        fbs.connString = Replace(AppSettings("ConnessioneDB"), "$(empresabase)", empresabase)
        fbs.defaultdatabase = empresabase
        fbs.IsSite = True
        FolderEmail = AppSettings("FolderEmails")
        FolderSite = AppSettings("CartellaSito")
        FolderDown = AppSettings("CartellaDownload") & empresabase & "\"
        fbs.TipoDatabase = AppSettings("TipoDatabase")
        fbs.mail_password = AppSettings("mail_password")
        fbs.mail_sender = "IdeaAcabamentos"
        fbs.mail_username = AppSettings("mail_utente")
        fbs.mail_smtp = AppSettings("mail_smtp")
        fbs.mail_porta = fbs.cnum(AppSettings("mail_porta"))
    End Sub

    Public Function GetItemCheckLIstByCodGrupoId(codchecklist As Integer, idgrupo As Integer, id As Integer, Optional closeDB As Boolean = True) As itenschecklist

        Dim l As New itenschecklist

        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader

        rc = fbs.GetQueryRecordsetLite("Select * from itenschecklist where codchecklist='" & codchecklist & "' and idgrupo='" & idgrupo & "' and id='" & id & "'")
        If rc.Read() Then

            With l
                .codchecklist = rc.Item("codchecklist").ToString
                .idgrupo = rc.Item("idgrupo").ToString
                .id = rc.Item("id").ToString
                .des = rc.Item("des").ToString
                .ord = rc.Item("ord").ToString
                .referencia = rc.Item("referencia").ToString
                .fotoobrigatoria = rc.Item("fotoobrigatoria").ToString

            End With

        End If
        rc.Close()
        If closeDB = True Then
            fbs.CloseDatabase()
        End If

        Return l

    End Function
    Public Sub SaveItemchecklist(usuario As String, item As itenschecklist)
        Try
            fbs.OpenDatabase()
            Dim sql As String = ""
            Dim iold As itenschecklist = GetItemCheckLIstByCodGrupoId(item.codchecklist, item.idgrupo, item.id, False)
            Dim isnewrecord As Boolean = False
            If iold.id = 0 Then
                sql = "INSERT INTO itenschecklist (`codchecklist`,`idgrupo`,`id`,`ord`,`des`,`referencia`,`fotoobrigatoria`) "
                sql = sql & " VALUES "
                sql = sql & " ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')"
                isnewrecord = True
            Else
                sql = "update itenschecklist set ord='{3}',des='{4}',referencia='{5}',fotoobrigatoria='{6}' "
                sql = sql & " where codchecklist='{0}' and idgrupo='{1}' and id='{2}' "
            End If
            With item
                sql = String.Format(sql, .codchecklist, .idgrupo, .id, .ord, .des, .referencia, .fotoobrigatoria)
            End With
            fbs.ExecuteSql(sql)
            If fbs.LogErr = "" Then
                lg.SaveLogCadastros(usuario, "itenschecklist", item.codchecklist & "|" & item.idgrupo & "|" & item.id, iold, item, isnewrecord)
            End If
        Catch ex As Exception
            fbs.AddErr("#SaveItemchecklist:" & ex.Message)
        End Try
        fbs.CloseDatabase()
    End Sub
    Public Sub Savegrupochecklist(usuario As String, u As grupochecklist)
        Try
            fbs.OpenDatabase()
            Dim sql As String = ""
            Dim uold As grupochecklist = GetgrupochecklistByCod(u.codchecklist, u.id, False)
            Dim isnewrecord As Boolean = False
            If uold.id = 0 Then
                sql = "INSERT INTO grupochecklist (`codchecklist`,`id`,`des`,`ord`) "
                sql = sql & " VALUES "
                sql = sql & " ('{0}','{1}','{2}','{3}')"
                isnewrecord = True
            Else
                sql = "update grupochecklist set des='{2}',ord='{3}' "
                sql = sql & " where codchecklist='{0}'   and id='{1}' "
            End If
            With u
                sql = String.Format(sql, .codchecklist, .id, .des, .ord)
            End With
            fbs.ExecuteSql(sql)
            If fbs.LogErr = "" Then
                lg.SaveLogCadastros(usuario, "grupochecklist", u.codchecklist & "|" & u.id, uold, u, isnewrecord)
            End If
        Catch ex As Exception
            fbs.AddErr("#Savegrupochecklist:" & ex.Message)
        End Try
        fbs.CloseDatabase()
    End Sub

    Public Sub Deletegrupochecklist(usuario As String, u As grupochecklist)
        Try
            fbs.OpenDatabase()
            Dim sql As String = ""
            sql = "delete from grupochecklist "
            sql = sql & " where id='{0}'"
            With u
                sql = String.Format(sql, .id)
            End With
            fbs.ExecuteSql(sql)
            If fbs.LogErr = "" Then
                lg.SaveLogCadastroExcluido(usuario, "grupochecklist", u.id, u)
            End If
        Catch ex As Exception
            fbs.AddErr("#Deletegrupochecklist:" & ex.Message)
        End Try
        fbs.CloseDatabase()
    End Sub

    Public Sub Deleteitemchecklist(usuario As String, u As itenschecklist)
        Try
            fbs.OpenDatabase()
            Dim sql As String = ""
            sql = "delete from itenschecklist "
            sql = sql & " where codchecklist={0} and idgrupo={1} and id={2}"
            With u
                sql = String.Format(sql, .codchecklist, .idgrupo, .id)
            End With
            fbs.ExecuteSql(sql)
            If fbs.LogErr = "" Then
                lg.SaveLogCadastroExcluido(usuario, "grupochecklist", u.codchecklist, u)
            End If
        Catch ex As Exception
            fbs.AddErr("#Deleteitemchecklist:" & ex.Message)
        End Try
        fbs.CloseDatabase()
    End Sub

    Public Function GetgrupochecklistByCod(codchecklist As Integer, id As Integer, Optional closedb As Boolean = True) As grupochecklist

        Dim l As New grupochecklist
        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader
        Dim i As Integer = 0
        rc = fbs.GetQueryRecordsetLite(String.Format("Select * from grupochecklist  where codchecklist='{0}' and id='{1}'", codchecklist, id))
        If rc.Read() = True Then
            With l
                .id = rc.Item("id").ToString
                .des = rc.Item("des").ToString
                .ord = rc.Item("ord").ToString
                .codchecklist = rc.Item("codchecklist").ToString
            End With
        End If
        rc.Close()
        If closedb = True Then
            fbs.CloseDatabase()
        End If

        Return l
    End Function

    Public Function GetListagrupochecklist(codchecklist As Integer, Optional closedb As Boolean = True) As grupochecklist()

        Dim l() As grupochecklist
        l = Nothing
        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader
        Dim i As Integer = 0
        rc = fbs.GetQueryRecordsetLite("Select * from grupochecklist where codchecklist='" & codchecklist & "' order by ord")
        While rc.Read()
            ReDim Preserve l(i)
            l(i) = New grupochecklist
            With l(i)
                .id = rc.Item("id").ToString
                .des = rc.Item("des").ToString
                .ord = rc.Item("ord").ToString
                .codchecklist = rc.Item("codchecklist").ToString
            End With
            i = i + 1
        End While
        rc.Close()
        fbs.CloseDatabase()
        Return l

    End Function

    Public Function GetListacabchecklist() As cabchecklist()

        Dim l() As cabchecklist
        l = Nothing
        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader
        Dim i As Integer = 0
        rc = fbs.GetQueryRecordsetLite("Select * from cabchecklist  order by cod")
        While rc.Read()
            ReDim Preserve l(i)
            l(i) = New cabchecklist
            With l(i)
                .cod = rc.Item("cod").ToString
                .des = rc.Item("des").ToString
                .obs = rc.Item("obs").ToString
                .ativo = rc.Item("ativo").ToString
            End With
            i = i + 1
        End While
        rc.Close()
        fbs.CloseDatabase()
        Return l

    End Function

    Public Function GetcabchecklistByCod(cod As Integer, Optional closedb As Boolean = True) As cabchecklist

        Dim l As New cabchecklist
        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader
        Dim i As Integer = 0
        rc = fbs.GetQueryRecordsetLite(String.Format("Select * from cabchecklist  where cod='{0}'", cod))
        If rc.Read() = True Then
            With l
                .cod = rc.Item("cod")
                .des = rc.Item("des").ToString
                .obs = rc.Item("obs").ToString
                .ativo = rc.Item("ativo").ToString
            End With
        End If
        rc.Close()
        If closedb = True Then
            fbs.CloseDatabase()
        End If

        Return l
    End Function

    Public Function GetChecklistByCod(cod As Integer, Optional closedb As Boolean = True) As checklist
        Dim c As New checklist
        c.cab = GetcabchecklistByCod(cod, closedb)
        c.grupos = GetListagrupochecklist(cod, closedb)
        For Each g In c.grupos
            g.itens = GetItensCheckListByCodEgrupo(cod, g.id, closedb)
        Next
        Return c
    End Function

    Public Function GetItensCheckListByCodEgrupo(codchecklist As Integer, idgrupo As Integer, Optional closedb As Boolean = True) As itenschecklist()

        Dim l() As itenschecklist
        l = Nothing
        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader
        Dim i As Integer = 0
        rc = fbs.GetQueryRecordsetLite("Select * from itenschecklist where codchecklist='" & codchecklist & "' and idgrupo='" & idgrupo & "' order by ord")
        While rc.Read()
            ReDim Preserve l(i)
            l(i) = New itenschecklist
            With l(i)
                .codchecklist = rc.Item("codchecklist").ToString
                .idgrupo = rc.Item("idgrupo").ToString
                .id = rc.Item("id").ToString
                .des = rc.Item("des").ToString
                .ord = rc.Item("ord").ToString
                .referencia = rc.Item("referencia").ToString
                .fotoobrigatoria = rc.Item("fotoobrigatoria").ToString

            End With
            i = i + 1
        End While
        rc.Close()
        fbs.CloseDatabase()
        Return l

    End Function

    Public Sub SaveCheckList(usuario As String, c As checklist)
        Try

            Savecabchecklist(usuario, c.cab)
            For Each g In c.grupos
                Savegrupochecklist(usuario, g)
                For Each i In g.itens
                    SaveItemchecklist(usuario, i)
                Next
            Next
        Catch ex As Exception
            fbs.AddErr("#erro:" & ex.Message)
        End Try

    End Sub
    Public Sub Savecabchecklist(usuario As String, u As cabchecklist)
        Try
            fbs.OpenDatabase()
            Dim sql As String = ""
            Dim uold As cabchecklist = GetcabchecklistByCod(u.cod, False)
            Dim isnewrecord As Boolean = False
            If uold.cod = 0 Then
                sql = "INSERT INTO cabchecklist (`cod`,`des`,`obs`,`ativo`) "
                sql = sql & " VALUES "
                sql = sql & " ('{0}','{1}','{2}','{3}')"
                isnewrecord = True
            Else
                sql = "update cabchecklist set des='{1}',obs='{2}',ativo='{3}' "
                sql = sql & " where cod='{0}' "
            End If
            With u
                sql = String.Format(sql, .cod, .des, .obs, .ativo)
            End With
            fbs.ExecuteSql(sql)
            If fbs.LogErr = "" Then
                lg.SaveLogCadastros(usuario, "cabchecklist", u.cod, uold, u, isnewrecord)
            End If
        Catch ex As Exception
            fbs.AddErr("#Savecabchecklist:" & ex.Message)
        End Try
        fbs.CloseDatabase()
    End Sub

    Public Sub Deletecabchecklist(usuario As String, u As cabchecklist)
        Try
            fbs.OpenDatabase()
            Dim sql As String = ""
            sql = "delete from cabchecklist "
            sql = sql & " where cod='{0}'"
            With u
                sql = String.Format(sql, .cod)
            End With
            fbs.ExecuteSql(sql)
            If fbs.LogErr = "" Then
                lg.SaveLogCadastroExcluido(usuario, "cabchecklist", u.cod, u)
            End If
        Catch ex As Exception
            fbs.AddErr("#Deletecabchecklist:" & ex.Message)
        End Try
        fbs.CloseDatabase()
    End Sub

    Public Function GetListaJobs() As Job()

        Dim l() As Job
        l = Nothing
        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader
        Dim i As Integer = 0
        rc = fbs.GetQueryRecordsetLite("Select * from job  order by cod")
        While rc.Read()
            ReDim Preserve l(i)
            l(i) = New Job
            With l(i)
                .cod = rc.Item("cod").ToString
                .idCliente = rc.Item("idCliente").ToString
                .desCliente = GetClienteByCod(.idCliente, False).des
                .idUsuario = rc.Item("idUsuario").ToString
                .desUsuario = GetUsuarioByCod(.idUsuario, False).des
                .usuarioCriacao = rc.Item("usuarioCriacao").ToString
                .dataHoraCriacao = fbs.rData(rc.Item("dataHoraCriacao").ToString)
                .dataJob = fbs.rData(rc.Item("dataJob").ToString)
                .horaJob = rc.Item("horaJob").ToString
                .idstatus = rc.Item("idstatus").ToString

                .status = GetCarOpcByCod("STATUS", .idstatus, False).des
                .des = rc.Item("des").ToString
                .obs = rc.Item("obs").ToString
            End With
            i = i + 1
        End While
        rc.Close()
        fbs.CloseDatabase()
        Return l

    End Function

    Public Function GetJobByCod(cod As String, Optional closedb As Boolean = True) As Job

        Dim l As New Job
        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader
        Dim i As Integer = 0
        rc = fbs.GetQueryRecordsetLite(String.Format("Select * from job  where cod='{0}'", cod))
        If rc.Read() = True Then
            With l
                .cod = rc.Item("cod").ToString
                .idCliente = rc.Item("idCliente").ToString
                .desCliente = GetClienteByCod(.idCliente, False).des
                .idUsuario = rc.Item("idUsuario").ToString
                .desUsuario = GetUsuarioByCod(.idUsuario, False).des
                .usuarioCriacao = rc.Item("usuarioCriacao").ToString
                .dataHoraCriacao = fbs.rData(rc.Item("dataHoraCriacao").ToString)
                .dataJob = fbs.rData(rc.Item("dataJob").ToString)
                .horaJob = rc.Item("horaJob").ToString
                .idstatus = rc.Item("idstatus").ToString
                .status = ""
                .des = rc.Item("des").ToString
                .obs = rc.Item("obs").ToString
            End With
        End If
        rc.Close()
        If closedb = True Then
            fbs.CloseDatabase()
        End If

        Return l
    End Function


    Public Function GetListaClientes() As Clientes()
        Dim l() As Clientes
        l = Nothing
        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader
        Dim i As Integer = 0
        rc = fbs.GetQueryRecordsetLite("Select * from clientes  order by des")
        While rc.Read()
            ReDim Preserve l(i)
            l(i) = New Clientes
            With l(i)
                .cod = rc.Item("cod").ToString
                .des = rc.Item("des").ToString
                .idref = rc.Item("idref").ToString
                .ref = GetUsuarioByCod(.idref, False).des
                .email = rc.Item("email").ToString
            End With
            i = i + 1
        End While
        rc.Close()
        fbs.CloseDatabase()
        Return l

    End Function

    Public Function GetClienteByCod(cod As String, Optional closedb As Boolean = True) As Clientes

        Dim l As New Clientes
        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader
        Dim i As Integer = 0
        rc = fbs.GetQueryRecordsetLite(String.Format("Select * from clientes  where cod='{0}'", cod))
        If rc.Read() = True Then
            With l
                .cod = rc.Item("cod").ToString
                .des = rc.Item("des").ToString
                .idref = rc.Item("idref").ToString
                .ref = GetUsuarioByCod(.idref, False).des
                .email = rc.Item("email").ToString
            End With
        End If
        rc.Close()
        If closedb = True Then
            fbs.CloseDatabase()
        End If

        Return l
    End Function

    Public Sub SaveCliente(usuario As String, u As Clientes)
        Try
            fbs.OpenDatabase()
            Dim sql As String = ""
            Dim uold As Clientes = GetClienteByCod(u.cod, False)
            Dim isnewrecord As Boolean = False
            If uold.cod = "" Then
                sql = "INSERT INTO clientes (`cod`,`des`,`idref`,`email`) "
                sql = sql & " VALUES "
                sql = sql & " ('{0}','{1}','{2}','{3}')"
                isnewrecord = True
            Else
                sql = "update clientes set des='{1}',idref='{2}',email='{3}' "
                sql = sql & " where cod='{0}' "
            End If
            With u
                sql = String.Format(sql, .cod, .des, .idref, .email)
            End With
            fbs.ExecuteSql(sql)
            If fbs.LogErr = "" Then
                lg.SaveLogCadastros(usuario, "clientes", u.cod, uold, u, isnewrecord)
            End If
        Catch ex As Exception
            fbs.AddErr("#SaveUsuario:" & ex.Message)
        End Try
        fbs.CloseDatabase()
    End Sub

    Public Sub DeleteCliente(usuario As String, u As Clientes)
        Try
            fbs.OpenDatabase()
            Dim sql As String = ""
            sql = "delete from clientes "
            sql = sql & " where cod='{0}'"
            With u
                sql = String.Format(sql, .cod)
            End With
            fbs.ExecuteSql(sql)
            If fbs.LogErr = "" Then
                lg.SaveLogCadastroExcluido(usuario, "clientes", u.cod, u)
            End If
        Catch ex As Exception
            fbs.AddErr("#DeleteCliente:" & ex.Message)
        End Try
        fbs.CloseDatabase()
    End Sub

    Public Function GetProssimoJob() As String
        fbs.OpenDatabase()
        Dim newCod As String = fbs.cnum(fbs.GetQueryValue("select max(cod) from job")) + 1
        fbs.CloseDatabase()
        Return newCod
    End Function

    Public Function GetUProssimoCheckList() As String
        fbs.OpenDatabase()
        Dim newCod As String = fbs.cnum(fbs.GetQueryValue("select max(cod) from cabchecklist")) + 1
        fbs.CloseDatabase()
        Return newCod
    End Function

    Public Sub SaveJob(usuario As String, u As Job)
        Try
            fbs.OpenDatabase()
            Dim sql As String = ""
            Dim uold As New Job
            Dim isnewrecord As Boolean = False
            Dim dateNow As String = fbs.fData(DateTime.Now())

            If u.cod = 0 Then
                ' novo
                u.cod = GetProssimoJob()
            Else
                uold = GetJobByCod(u.cod, False)
            End If

            If uold.cod = 0 Then
                sql = "INSERT INTO JOB (`cod`,`idCliente`,`usuarioCriacao`,`dataHoraCriacao`,`dataJob`,`horaJob`,`idStatus`,`obs`,`des`,`idUsuario`,`codCheckList`) "
                sql = sql & " VALUES "
                sql = sql & " ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')"
                isnewrecord = True
            Else
                sql = "update job set idCliente='{1}',usuarioCriacao='{2}',dataHoraCriacao='{3}',dataJob='{4}',horaJob='{5}',idStatus='{6}',obs='{7}',des='{8}',idUsuario='{9}',codCheckList='{10}' "
                sql = sql & " where cod='{0}' "
            End If
            With u
                sql = String.Format(sql, .cod, .idCliente, usuario, dateNow, fbs.fData(.dataJob), .horaJob, .idstatus, .obs, .des, .idUsuario, .codCheckList)
            End With
            fbs.ExecuteSql(sql)
            If fbs.LogErr = "" Then
                lg.SaveLogCadastros(usuario, "JOB", u.cod, uold, u, isnewrecord)
            End If
        Catch ex As Exception
            fbs.AddErr("#SaveJob:" & ex.Message)
        End Try
        fbs.CloseDatabase()
    End Sub

    Public Sub DeleteJob(usuario As String, u As Job)
        Try
            fbs.OpenDatabase()
            Dim sql As String = ""
            sql = "delete from job "
            sql = sql & " where cod='{0}'"
            With u
                sql = String.Format(sql, .cod)
            End With
            fbs.ExecuteSql(sql)
            If fbs.LogErr = "" Then
                lg.SaveLogCadastroExcluido(usuario, "job", u.cod, u)
            End If
        Catch ex As Exception
            fbs.AddErr("#DeleteJob:" & ex.Message)
        End Try
        fbs.CloseDatabase()
    End Sub

    Public Function GetListaUsuarios() As usuarios()

        Dim l() As usuarios
        l = Nothing
        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader
        Dim i As Integer = 0
        rc = fbs.GetQueryRecordsetLite("Select * from usuarios  order by des")
        While rc.Read()
            ReDim Preserve l(i)
            l(i) = New usuarios
            With l(i)
                .cod = rc.Item("cod").ToString
                .des = rc.Item("des").ToString
                .senha = rc.Item("senha").ToString
                .tipo = rc.Item("tipo").ToString
                .destipo = "admin" ' GetCarOpcByCod("T_TIPOUSUARIO", .tipo, False).des
                .ativo = rc.Item("ativo").ToString
                .email = rc.Item("email").ToString
            End With
            i = i + 1
        End While
        rc.Close()
        fbs.CloseDatabase()
        Return l

    End Function

    Public Function GetUsuarioByCod(cod As String, Optional closedb As Boolean = True) As usuarios

        Dim l As New usuarios
        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader
        Dim i As Integer = 0
        rc = fbs.GetQueryRecordsetLite(String.Format("Select * from usuarios  where cod='{0}'", cod))
        If rc.Read() = True Then
            With l
                .cod = rc.Item("cod").ToString
                .des = rc.Item("des").ToString
                .senha = rc.Item("senha").ToString
                .tipo = rc.Item("tipo").ToString
                .destipo = "admin" ' GetCarOpcByCod("T_TIPOUSUARIO", .tipo, False).des
                .ativo = rc.Item("ativo").ToString
                .email = rc.Item("email").ToString
            End With
        End If
        rc.Close()
        If closedb = True Then
            fbs.CloseDatabase()
        End If

        Return l
    End Function

    Public Sub SaveUsuario(usuario As String, u As usuarios)
        Try
            fbs.OpenDatabase()
            Dim sql As String = ""
            Dim uold As usuarios = GetUsuarioByCod(u.cod, False)
            Dim isnewrecord As Boolean = False
            If uold.cod = "" Then
                sql = "INSERT INTO usuarios (`cod`,`des`,`senha`,`tipo`,`ativo`,`email`) "
                sql = sql & " VALUES "
                sql = sql & " ('{0}','{1}','{2}','{3}','{4}','{5}')"
                isnewrecord = True
            Else
                sql = "update usuarios set des='{1}',senha='{2}',tipo='{3}',ativo='{4}',email='{5}' "
                sql = sql & " where cod='{0}' "
            End If
            With u
                sql = String.Format(sql, .cod, .des, .senha, .tipo, .ativo, .email)
            End With
            fbs.ExecuteSql(sql)
            If fbs.LogErr = "" Then
                lg.SaveLogCadastros(usuario, "usuarios", u.cod, uold, u, isnewrecord)
            End If
        Catch ex As Exception
            fbs.AddErr("#SaveUsuario:" & ex.Message)
        End Try
        fbs.CloseDatabase()
    End Sub

    Public Sub DeleteUsuario(usuario As String, u As usuarios)
        Try
            fbs.OpenDatabase()
            Dim sql As String = ""
            sql = "delete from usuarios "
            sql = sql & " where cod='{0}'"
            With u
                sql = String.Format(sql, .cod)
            End With
            fbs.ExecuteSql(sql)
            If fbs.LogErr = "" Then
                lg.SaveLogCadastroExcluido(usuario, "usuarios", u.cod, u)
            End If
        Catch ex As Exception
            fbs.AddErr("#DeleteUsuario:" & ex.Message)
        End Try
        fbs.CloseDatabase()
    End Sub


    Public Function GetCabCarByCod(cod As String, Optional closeDb As Boolean = True) As CarCab
        Dim c As New CarCab

        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader
        rc = fbs.GetQueryRecordsetLite(String.Format("Select * from CarCab where cod='{0}' order by cod", cod))
        If rc.Read = True Then
            With c
                .cod = rc.Item("cod").ToString
                .des = rc.Item("des").ToString
                .img = rc.Item("img").ToString
                .campos = rc.Item("campos").ToString
                .carref = rc.Item("carref").ToString
                .tipopreview = rc.Item("tipopreview").ToString
                .tipo = rc.Item("tipo").ToString
            End With
        End If
        rc.Close()
        If closeDb = True Then
            fbs.CloseDatabase()
        End If
        Return c
    End Function

    Public Function GetCarCab(tipo As String) As CarCab()
        Dim c() As CarCab
        c = Nothing
        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader
        Dim i As Integer = 0
        If tipo = "" Then
            rc = fbs.GetQueryRecordsetLite("Select * from CarCab order by cod")
        Else
            rc = fbs.GetQueryRecordsetLite(String.Format("Select * from CarCab where tipo like '{0}' order by cod", tipo))
        End If

        While rc.Read()
            ReDim Preserve c(i)
            c(i) = New CarCab
            With c(i)
                .cod = rc.Item("cod").ToString
                .des = rc.Item("des").ToString
                .img = rc.Item("img").ToString
                .campos = rc.Item("campos").ToString
                .carref = rc.Item("carref").ToString
                .tipopreview = rc.Item("tipopreview").ToString
                .tipo = rc.Item("tipo").ToString
            End With
            i = i + 1
        End While
        rc.Close()
        fbs.CloseDatabase()
        Return c
    End Function
    Public Sub SaveCarCab(usuario As String, car As CarCab)
        Try
            fbs.OpenDatabase()
            Dim sql As String = ""
            Dim carold As CarCab = GetCabCarByCod(car.cod, False)

            Dim isnewrecord As Boolean = False
            If carold.cod = "" Then
                sql = "INSERT INTO carcab (cod,des,img,carref,campos,tipopreview,tipo) "
                sql = sql & " VALUES "
                sql = sql & " ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')"
                isnewrecord = True
            Else
                sql = "update carcab set des='{1}',img='{2}',carref='{3}',campos='{4}',tipopreview='{5}',tipo='{6}' "
                sql = sql & " where cod='{0}' "
            End If
            With car
                sql = String.Format(sql, .cod, .des, .img, .carref, .campos, .tipopreview, .tipo)
            End With
            fbs.ExecuteSql(sql)
            If fbs.LogErr = "" Then
                lg.SaveLogCadastros(usuario, "CarCab", car.cod, carold, car, isnewrecord)
            End If

        Catch ex As Exception
            fbs.AddErr("#SaveCarCab:" & ex.Message)
        End Try
        fbs.CloseDatabase()
    End Sub

    Public Sub DeleteCarCab(usuario As String, car As CarCab)
        Try
            fbs.OpenDatabase()
            Dim sql As String = ""
            Dim carold As CarCab = GetCabCarByCod(car.cod, False)
            If car.cod = "" Then
                fbs.AddErr("#DeleteCarCab: Carateristica não pode ser excluida, não está salva")
            Else
                sql = "delete from carcab "
                sql = sql & " where cod='{0}'"
                With car
                    sql = String.Format(sql, .cod)
                End With
                fbs.ExecuteSql(sql)
                If fbs.LogErr = "" Then
                    lg.SaveLogCadastroExcluido(usuario, "CarCab", car.cod, car)
                End If
            End If
            Dim opcs() As CarOpc = GetCarOpc(car.cod, 0)
            For Each opc In opcs
                DeleteCarOpc(usuario, opc)
            Next
        Catch ex As Exception
            fbs.AddErr("#DeleteCarCab:" & ex.Message)
        End Try
        fbs.CloseDatabase()
    End Sub

    Public Function GetCarOpc(codcar As String, adicionavazio As Integer, Optional filtrocod As String = "") As CarOpc()
        Dim o() As CarOpc
        o = Nothing

        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader
        Dim i As Integer = 0

        If adicionavazio = 2 Then
            ReDim Preserve o(i)
            o(i) = New CarOpc
            o(i).codcar = codcar
            i = i + 1
        End If
        Dim carreferencia As String = codcar
        Dim c As CarCab = GetCabCarByCod(codcar, False)
        If c.carref <> "" Then carreferencia = c.carref
        If filtrocod <> "" Then
            If adicionavazio = 1 Then
                rc = fbs.GetQueryRecordsetLite(String.Format("Select * from CarOpc where codcar='{0}' and cod in ({1}) order by ord,des", carreferencia, filtrocod))
            Else
                rc = fbs.GetQueryRecordsetLite(String.Format("Select * from CarOpc where codcar='{0}' and cod in ({1}) and ativo=1 order by ord,des", carreferencia, filtrocod))
            End If
        Else
            If adicionavazio = 1 Then
                rc = fbs.GetQueryRecordsetLite(String.Format("Select * from CarOpc where codcar='{0}' order by ord,des", carreferencia))
            Else
                rc = fbs.GetQueryRecordsetLite(String.Format("Select * from CarOpc where codcar='{0}' and ativo=1 order by ord,des", carreferencia))
            End If
        End If

        While rc.Read()
            ReDim Preserve o(i)
            o(i) = New CarOpc
            With o(i)
                If rc.Item("cod").ToString <> "" Then
                    .codcar = codcar
                    .cod = rc.Item("cod").ToString
                    .des = rc.Item("des").ToString
                    .img = rc.Item("img").ToString
                    .coditem = rc.Item("coditem").ToString
                    .ord = fbs.cnum(rc.Item("ord").ToString)
                    .cat = rc.Item("cat").ToString
                    .linkdoc = rc.Item("linkdoc").ToString
                    .campos = rc.Item("campos").ToString
                    .ativo = rc.Item("ativo").ToString
                    If .cat = "" Then
                        .nivel = 0
                    Else
                        .nivel = .cat.Split(".").Length
                    End If
                    If .campos.IndexOf(",") > -1 Then
                        .cAdd = Split(.campos, ",")
                    End If
                End If
            End With
            i = i + 1
        End While
        rc.Close()
        fbs.CloseDatabase()
        If adicionavazio = 1 Then
            ReDim Preserve o(i)
            o(i) = New CarOpc
            o(i).codcar = codcar
        End If
        Return o
    End Function


    Public Function GetCarOpcByCod(codcar As String, cod As String, Optional closeDb As Boolean = True) As CarOpc
        Dim o As New CarOpc
        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader
        Dim carreferencia As String = codcar
        Dim c As CarCab = GetCabCarByCod(codcar, False)
        If c.carref <> "" Then carreferencia = c.carref
        rc = fbs.GetQueryRecordsetLite(String.Format("Select * from CarOpc where codcar='{0}' and cod='{1}'", carreferencia, cod))
        If rc.Read() Then
            With o
                .codcar = codcar
                .cod = rc.Item("cod").ToString
                .des = rc.Item("des").ToString
                .img = rc.Item("img").ToString
                .coditem = rc.Item("coditem").ToString
                .ord = fbs.cnum(rc.Item("ord").ToString)
                .cat = rc.Item("cat").ToString
                .linkdoc = rc.Item("linkdoc").ToString
                .campos = rc.Item("campos").ToString
                .ativo = rc.Item("ativo").ToString
                If .campos <> "" Then
                    .cAdd = Split(.campos, ",")
                End If
                If .cat = "" Then
                    .nivel = 0
                Else
                    .nivel = .cat.Split(".").Length
                End If
            End With
        End If
        rc.Close()
        If closeDb = True Then
            fbs.CloseDatabase()
        End If

        Return o
    End Function


    Public Function GetCarCatByCod(codcar As String, cod As String, Optional closeDb As Boolean = True) As CarCat
        Dim c As New CarCat
        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader
        Dim carreferencia As String = codcar
        Dim cc As CarCab = GetCabCarByCod(codcar, False)
        If cc.carref <> "" Then carreferencia = cc.carref
        rc = fbs.GetQueryRecordsetLite(String.Format("Select * from carcat where codcar='{0}' and cod='{1}'", carreferencia, cod))
        If rc.Read() Then
            With c
                .codcar = codcar
                .cod = rc.Item("cod").ToString
                .des = rc.Item("des").ToString
                .img = rc.Item("img").ToString
                .nivel = .cod.Split(".").Length
            End With
        End If
        rc.Close()
        If closeDb = True Then
            fbs.CloseDatabase()
        End If

        Return c
    End Function
    Public Sub SaveCarOpc(usuario As String, opc As CarOpc, Optional tabela As String = "CAROPC")
        Try
            If opc.cod <> "" Then
                fbs.OpenDatabase()
                Dim sql As String = ""
                Dim carreferencia As String = opc.codcar
                Dim cc As CarCab = GetCabCarByCod(opc.codcar, False)
                If cc.carref <> "" Then carreferencia = cc.carref
                Dim opcold As CarOpc = GetCarOpcByCod(carreferencia, opc.cod, True)

                Dim isnewrecord As Boolean = False
                If opcold.cod = "" Then
                    sql = "INSERT INTO " & tabela & " (codcar,cod,des,img,coditem,ord,cat,linkdoc,campos,ativo) "
                    sql = sql & " VALUES "
                    sql = sql & " ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')"
                    isnewrecord = True
                Else
                    sql = "update " & tabela & "  set des='{2}',img='{3}',coditem='{4}',ord='{5}',cat='{6}',linkdoc='{7}',campos='{8}',ativo='{9}' "
                    sql = sql & " where codcar='{0}' and cod='{1}'  "
                End If
                With opc
                    sql = String.Format(sql, carreferencia, .cod, .des, .img, .coditem, fbs.cnum(.ord), .cat, .linkdoc, .campos, .ativo)
                End With
                fbs.ExecuteSql(sql)
                If fbs.LogErr = "" And UCase(tabela) = "CAROPC" Then
                    lg.SaveLogCadastros(usuario, tabela, carreferencia & "|" & opc.cod, opcold, opc, isnewrecord)
                End If
            Else
                fbs.AddErr("#SaveCarOpc: Codigo opção invalido ou ausente")
            End If
        Catch ex As Exception
            fbs.AddErr("#SaveCarOpc:" & ex.Message)
        End Try

        fbs.CloseDatabase()
    End Sub
    Public Sub DeleteCarOpc(usuario As String, opc As CarOpc)
        Try
            fbs.OpenDatabase()
            Dim sql As String = ""
            Dim carreferencia As String = opc.codcar
            Dim cc As CarCab = GetCabCarByCod(opc.codcar, False)
            If cc.carref <> "" Then carreferencia = cc.carref
            Dim opcold As CarOpc = GetCarOpcByCod(carreferencia, opc.cod, False)
            If opcold.cod = "" Then
                fbs.AddErr("#DeleteCarOpc: Carateristica não pode ser excluida, não está salva")
            Else
                sql = "delete from caropc "
                sql = sql & " where codcar='{0}' and cod='{1}'"
                With opc
                    sql = String.Format(sql, carreferencia, .cod)
                End With
                fbs.ExecuteSql(sql)
                If fbs.LogErr = "" Then
                    lg.SaveLogCadastroExcluido(usuario, "CarOpc", carreferencia & "|" & opc.cod, opc)
                End If
            End If

        Catch ex As Exception
            fbs.AddErr("#DeleteCarOpc:" & ex.Message)
        End Try

        fbs.CloseDatabase()
    End Sub
    Public Sub DeleteCarCat(usuario As String, cat As CarCat)
        Try
            fbs.OpenDatabase()
            Dim sql As String = ""
            Dim carreferencia As String = cat.codcar
            Dim cc As CarCab = GetCabCarByCod(cat.codcar, False)
            If cc.carref <> "" Then cat.codcar = cc.carref

            Dim catold As CarCat = GetCarCatByCod(cat.codcar, cat.cod, False)
            Dim test As String = fbs.GetQueryValue(String.Format("select cat from caropc where codcar='{0}' and cat='{1}'", cat.codcar, cat.cod))
            If catold.cod = "" Then
                fbs.AddErr("#DeleteCarCat: Categoria não pode ser excluida, não está salva")
            ElseIf test <> "" Then
                fbs.AddErr("#DeleteCarCat: Categoria não pode ser excluida porque está sendo usada")
            Else
                sql = "delete from carcat "
                sql = sql & " where codcar='{0}' and cod='{1}'"
                With cat
                    sql = String.Format(sql, .codcar, .cod)
                End With
                fbs.ExecuteSql(sql)
                If fbs.LogErr = "" Then
                    lg.SaveLogCadastroExcluido(usuario, "CarCat", cat.codcar & "|" & cat.cod, cat)
                End If
            End If
        Catch ex As Exception
            fbs.AddErr("#DeleteCarOpc:" & ex.Message)
        End Try
        fbs.CloseDatabase()
    End Sub

    Public Function GetCarCat(codcar As String, Optional adicionavazio As Integer = 0) As CarCat()
        Dim c() As CarCat
        c = Nothing
        Dim carreferencia As String = codcar
        Dim cc As CarCab = GetCabCarByCod(codcar, False)
        If cc.carref <> "" Then carreferencia = cc.carref
        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader
        Dim i As Integer = 0
        rc = fbs.GetQueryRecordsetLite(String.Format("Select * from carcat where codcar='{0}' order by cod", carreferencia))
        While rc.Read()
            ReDim Preserve c(i)
            c(i) = New CarCat
            With c(i)
                .codcar = rc.Item("codcar").ToString
                .cod = rc.Item("cod").ToString
                .des = rc.Item("des").ToString
                .img = rc.Item("img").ToString
                .nivel = .cod.Split(".").Length
            End With
            i = i + 1
        End While
        rc.Close()
        fbs.CloseDatabase()
        If adicionavazio = 1 Then
            ReDim Preserve c(i)
            c(i) = New CarCat
            c(i).codcar = carreferencia
        End If
        Return c
    End Function


    Public Sub SaveCarCat(usuario As String, cat As CarCat)
        Try
            fbs.OpenDatabase()
            Dim sql As String = ""
            Dim carreferencia As String = cat.codcar
            Dim cc As CarCab = GetCabCarByCod(cat.codcar, False)
            If cc.carref <> "" Then cat.codcar = cc.carref
            Dim catold As CarCat = GetCarCatByCod(cat.codcar, cat.cod, True)
            Dim isnewrecord As Boolean = False
            If catold.cod = "" Then
                sql = "INSERT INTO carcat (codcar,cod,des,img) "
                sql = sql & " VALUES "
                sql = sql & " ('{0}','{1}','{2}','{3}')"
                isnewrecord = True
            Else
                sql = "update carcat set des='{2}',img='{3}' "
                sql = sql & " where codcar='{0}' and cod='{1}'  "
            End If
            With cat
                sql = String.Format(sql, .codcar, .cod, .des, .img)
            End With
            fbs.ExecuteSql(sql)
            If fbs.LogErr = "" Then
                lg.SaveLogCadastros(usuario, "CarCat", cat.codcar & "|" & cat.cod, catold, cat, isnewrecord)
            End If
        Catch ex As Exception
            fbs.AddErr("#SaveCarCat:" & ex.Message)
        End Try
        fbs.CloseDatabase()
    End Sub
    Public Function GetLinkCarTxt(codcar As String) As String
        Dim link As String = ""
        fbs.OpenDatabase()
        Dim cab As CarCab = GetCabCarByCod(codcar)
        Dim opcs() As CarOpc = GetCarOpc(codcar, 0)
        Dim colunas() As String
        ReDim Preserve colunas(6)
        colunas(0) = "cod"
        colunas(1) = "des"
        colunas(2) = "cat"
        colunas(3) = "img"
        colunas(4) = "ord"
        colunas(5) = "linkdoc"
        colunas(6) = "coditem"
        Dim i As Integer = 7
        Dim colextra() As String = Split(cab.campos, ",")
        For Each col In colextra
            If col <> "" Then
                ReDim Preserve colunas(i)
                colunas(i) = col
                i = i + 1
            End If
        Next
        Dim sep As String = vbTab
        Dim s As New StringBuilder()
        s.AppendLine(Join(colunas, sep))
        Dim l As String = ""
        For Each opc In opcs
            With opc
                l = String.Concat(.cod, sep, .des, sep, .cat, sep, .img, sep, .ord, sep, .linkdoc, sep, .coditem, sep, Replace(.campos, ",", sep))
            End With
            s.AppendLine(l)
        Next
        Dim nomearquivo As String = AppSettings("CartellaCatalogos") & "\" & empresabase & "\exporttxt\export_car_" & codcar & ".txt"
        If System.IO.File.Exists(nomearquivo) = True Then
            System.IO.File.Delete(nomearquivo)
        End If
        fbs.WriteTextFileEncoding(nomearquivo, s.ToString, Encoding.GetEncoding("ISO-8859-1"))
        'backup para segurança
        System.IO.File.Copy(nomearquivo, AppSettings("CartellaCatalogos") & "\" & empresabase & "\exporttxt\export_car_" & codcar & "_" & fbs.fData(DateTime.Now) & ".txt")
        link = PercursoAbsoluto(nomearquivo)
        fbs.CloseDatabase()
        Return link
    End Function

    Public Sub ImportCarTxt(usuario As String, codcar As String)
        Try
            fbs.OpenDatabase()
            Dim opcs() As CarOpc
            opcs = Nothing
            Dim cab As CarCab = GetCabCarByCod(codcar)
            Dim sep As String = vbTab
            Dim i As Integer = 0
            Dim totcolunas As Integer = 0
            Dim contal As Integer = 0
            Dim contac As Integer = 0
            Dim linhas() As String = Split(fbs.ReadTextFileEncoding(AppSettings("CartellaCatalogos") & "\" & empresabase & "\importtxt\import_car_" & codcar & ".txt", Encoding.GetEncoding("ISO-8859-1")), vbCrLf)
            System.IO.File.Copy(AppSettings("CartellaCatalogos") & "\" & empresabase & "\importtxt\import_car_" & codcar & ".txt", AppSettings("CartellaCatalogos") & "\" & empresabase & "\importtxt\import_car_" & codcar & "_" & fbs.fData(DateTime.Now) & ".txt")
            Dim listacodigosnovos As String = "***"

            For Each linha In linhas
                If linha <> "" Then
                    Dim c() As String = Split(linha, sep)
                    If i = 0 Then
                        ' cabecalho
                        Dim isok As Boolean = True
                        If UCase(c(0)) <> "COD" Then isok = False
                        If UCase(c(1)) <> "DES" Then isok = False
                        If UCase(c(2)) <> "CAT" Then isok = False
                        If UCase(c(3)) <> "IMG" Then isok = False
                        If UCase(c(4)) <> "ORD" Then isok = False
                        If UCase(c(5)) <> "LINKDOC" Then isok = False
                        If UCase(c(6)) <> "CODITEM" Then isok = False
                        Dim colextra() As String = Split(cab.campos, ",")
                        Dim ii As Integer = 7
                        For Each col In colextra
                            If col <> "" Then
                                If UCase(c(ii)) <> UCase(col) Then isok = False
                                ii = ii + 1
                            End If
                        Next
                        totcolunas = ii - 1
                        If isok = False Then
                            fbs.AddErr("#ImportCarTxt: O Cabecalho do arquivo txt não é conforme aos campos da caracteristica")
                        End If
                    Else
                        ReDim Preserve c(totcolunas)
                        If c(0) <> "" Then
                            ReDim Preserve opcs(contal)
                            opcs(contal) = New CarOpc
                            With opcs(contal)
                                .codcar = codcar
                                .cod = c(0)
                                listacodigosnovos = listacodigosnovos & UCase(.cod) & "*"
                                .des = c(1)
                                .cat = c(2)
                                .img = c(3)
                                .ord = fbs.cnum(c(4))
                                .linkdoc = c(5)
                                .coditem = c(6)
                                For contac = 7 To totcolunas
                                    .campos = .campos & c(contac) & ","
                                Next
                            End With
                            contal = contal + 1
                        End If
                    End If
                    i = i + 1
                End If
            Next
            If IsNothing(opcs) = False Then
                Dim opcsold() As CarOpc = GetCarOpc(codcar, 0)
                Dim js As New JavaScriptSerializer

                js.MaxJsonLength = Int32.MaxValue
                fbs.WriteTextFile(AppSettings("CartellaCatalogos") & "\" & empresabase & "\importtxt\Bak_import_car_" & codcar & fbs.fData(DateTime.Now) & ".txt", js.Serialize(opcsold))
                If fbs.LogErr = "" Then
                    Dim logimport As New loginfo
                    logimport.acao = "IMPORTTXT"
                    logimport.chavetabela = codcar
                    logimport.nometabela = "CAROPC"
                    logimport.coluna = "ALL"
                    logimport.usuario = usuario
                    logimport.valorantigo = js.Serialize(opcsold)
                    logimport.valornovo = js.Serialize(opcs)
                    lg.WriteLogCadastros(logimport)

                    ' atualização dos novos
                    For Each opc In opcs
                        SaveCarOpc(usuario, opc)
                    Next
                    ' limpeza dos excluidos
                    For Each opc In opcsold
                        If InStr(listacodigosnovos, "*" & opc.cod & "*", CompareMethod.Text) <= 0 Then
                            DeleteCarOpc(usuario, opc)
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            fbs.AddErr("#" & ex.Message)
        End Try
        fbs.CloseDatabase()
    End Sub
    Function PercursoAbsoluto(percurso As String) As String
        Return Replace(UCase(percurso), UCase(AppSettings("CartellaSito")), "")
    End Function
    Public Function cns(numero As Double) As String
        Return Replace(numero, ",", ".")
    End Function

    Public Function fval(val As String, Optional moeda As String = "R$") As String
        val = Math.Round(CDbl(val), 2)
        Dim vv() As String = val.Split(",")
        ReDim Preserve vv(3)
        Dim d As String = vv(1)
        If Len(d) = 0 Then d = "00"
        If Len(d) = 1 Then d = d & "0"
        val = vv(0)
        If fbs.cnum(Int(val)) > 1000 Then
            val = CStr(Int(val / 1000)) & "." & Right("000" & CStr(Int(CInt(val) - Int(val / 1000) * 1000)), 3)
        Else
            val = Int(val)
        End If
        Return moeda & " " & val & "," & d
    End Function
    Public Function IsFeriado(ByVal Data As Date, feriados As String, Optional closeDb As Boolean = True) As Boolean
        Dim Anno As Integer, Mese As Integer, Giorno As Integer
        Dim feriado As Boolean = False
        Anno = Data.Year
        Mese = Data.Month
        Giorno = Data.Day
        If Mese = 1 And Giorno = 1 Then       '1 gennaio
            feriado = True
        ElseIf Mese = 4 And Giorno = 21 Then         'tiradentes
            feriado = True
        ElseIf Mese = 5 And Giorno = 1 Then         'dia do trabalho
            feriado = True
        ElseIf Mese = 9 And Giorno = 7 Then         'independencia
            feriado = True
        ElseIf Mese = 10 And Giorno = 12 Then         'nossa senhora aparecida
            feriado = True
        ElseIf Mese = 11 And Giorno = 2 Then         'finados
            feriado = True
        ElseIf Mese = 11 And Giorno = 15 Then         'proclamacao republica
            feriado = True
        ElseIf Mese = 12 And Giorno = 25 Then         '25 dicembre
            feriado = True
        Else
            If feriados = "" Then
                ' così lo fa una volta sola per chiamata
                fbs.OpenDatabase()
                Dim rc As SQLiteDataReader

                rc = fbs.GetQueryRecordsetLite("select cod from caropc where codcar='T_FERIADOS'")
                While rc.Read
                    feriados = feriados & "*" & rc.Item("cod") & "*"
                End While
                rc.Close()
                If closeDb = True Then
                    fbs.CloseDatabase()
                End If
            End If
            If InStr(feriados, "*" & Data.ToString("yyyy-MM-dd") & "*", CompareMethod.Text) > 0 Then
                feriado = True
            End If
            If Data.DayOfWeek = DayOfWeek.Saturday Or Data.DayOfWeek = DayOfWeek.Sunday Then
                feriado = True
            End If
        End If
        Return feriado
    End Function

    Function addDiasUteis(data As Date, diasentrega As Integer, Optional closeDb As Boolean = True) As Date
        Dim i As Integer = 0
        Dim feriados As String = ""
        Dim datateste As Date = data
        Dim dataentrega As Date = data
        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader

        rc = fbs.GetQueryRecordsetLite("select cod from caropc where codcar='T_FERIADOS'")
        While rc.Read
            feriados = feriados & "*" & rc.Item("cod") & "*"
        End While
        rc.Close()
        If closeDb = True Then
            fbs.CloseDatabase()
        End If
        Do
            datateste = datateste.AddDays(1)
            If IsFeriado(datateste, feriados, False) = False Then
                i = i + 1
                If i = diasentrega Then
                    dataentrega = datateste
                    Exit Do
                End If
            End If
        Loop
        Return dataentrega

    End Function
End Class


Public Class ScaricaURL
    Private m_strURL As String

    Public Sub SetURL(ByVal strURL As String)
        m_strURL = strURL
    End Sub

    Public Function Scarica() As String
        Dim wreq As WebRequest = WebRequest.Create(m_strURL)
        wreq.Timeout = 600000
        Dim wres As WebResponse = wreq.GetResponse()
        Dim iBuffer As Integer = 0
        Dim buffer(128) As Byte
        Dim stream As Stream = wres.GetResponseStream()
        iBuffer = stream.Read(buffer, 0, 128)
        Dim strRes As New StringBuilder("")
        While iBuffer <> 0
            strRes.Append(Encoding.UTF8.GetString(buffer, 0, iBuffer))
            iBuffer = stream.Read(buffer, 0, 128)
        End While
        Return strRes.ToString()
    End Function

    Public Function ScaricaPost(param As String) As String
        Dim myWebClient As New WebClient
        Dim myStream As Stream = myWebClient.OpenRead(m_strURL)
        Dim SR As StreamReader = New StreamReader(myStream)
        Dim out As String = SR.ReadToEnd()
        SR.Close()
        Return out
    End Function

End Class