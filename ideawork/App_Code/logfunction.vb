Imports CommonClass
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



Public Class logfunction

    Public fbs As FormaBase.FunzioniBase
    Public FolderEmail As String = ""
    Public empresabase As String = ""
    Public usuarioacesso As String = ""
    Public Sub New()
        fbs = New FormaBase.FunzioniBase()
        fbs.ClearErr()
        Try
            empresabase = HttpContext.Current.Request.Cookies("proidea_empresabase").Value.ToString
        Catch ex As Exception
            empresabase = ""
        End Try
        fbs.connString = Replace(AppSettings("ConnessioneDB"), "$(empresabase)", empresabase)
        fbs.TipoDatabase = 0
        fbs.defaultdatabase = empresabase
        fbs.IsSite = True
        FolderEmail = AppSettings("FolderEmails")
        fbs.TipoDatabase = AppSettings("TipoDatabase")
        fbs.mail_password = AppSettings("mail_password")
        fbs.mail_sender = "IdeaWork"
        fbs.mail_username = AppSettings("mail_utente")
        fbs.mail_smtp = AppSettings("mail_smtp")
        fbs.mail_porta = fbs.cnum(AppSettings("mail_porta"))
        Try
            usuarioacesso = HttpContext.Current.Request.Cookies("PROIDEA_USERNAME_ACESSO").Value.ToString
        Catch ex As Exception
            usuarioacesso = ""
        End Try



    End Sub


    Public Sub SaveLogCadastros(usuario As String, nometabela As String, chavetabela As String, objold As Object, objnew As Object, Optional isnewrecord As Boolean = False)
        Try
            ' fazer alguma coisa
            Dim l As New loginfo
            If usuarioacesso = "" Then usuarioacesso = usuario
            l.usuario = usuarioacesso
            l.nometabela = nometabela
            l.chavetabela = chavetabela
            If isnewrecord = False Then
                l.acao = "UPDATE"
            Else
                l.acao = "CREATE"
            End If

            If IsNothing(objold) = False And IsNothing(objnew) = False Then
                ' Get the type of FieldsClass.
                Dim fieldsType As Type = objold.GetType()
                ' Get an array of FieldInfo objects.
                Dim fields As FieldInfo() = fieldsType.GetFields(BindingFlags.Public Or BindingFlags.Instance)
                ' Display the values of the fields.
                For i As Integer = 0 To fields.Length - 1
                    If fields(i).FieldType.IsArray = False And (fields(i).FieldType.IsClass = False Or fields(i).FieldType.Name = "String") Then
                        l.coluna = fields(i).Name
                        If fields(i).GetValue(objold) <> fields(i).GetValue(objnew) Then
                            l.valorantigo = fields(i).GetValue(objold)
                            l.valornovo = fields(i).GetValue(objnew)
                            WriteLogCadastros(l)
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            fbs.AddErr("SaveLogCadastros:" & ex.Message)
            fbs.CloseDatabase()
        End Try
    End Sub
    Public Sub SaveLogCadastroExcluido(usuario As String, nometabela As String, chavetabela As String, objetoexcluido As Object)
        ' fazer alguma coisa
        Dim l As New loginfo
        If usuarioacesso = "" Then usuarioacesso = usuario
        l.usuario = usuarioacesso
        l.nometabela = nometabela
        l.chavetabela = chavetabela
        l.acao = "DELETE"
        If IsNothing(objetoexcluido) = False Then
            ' Get the type of FieldsClass.
            Dim fieldsType As Type = objetoexcluido.GetType()
            ' Get an array of FieldInfo objects.
            Dim fields As FieldInfo() = fieldsType.GetFields(BindingFlags.Public Or BindingFlags.Instance)
            ' Display the values of the fields.
            For i As Integer = 0 To fields.Length - 1
                If fields(i).FieldType.IsArray = False And (fields(i).FieldType.IsClass = False Or fields(i).FieldType.Name = "String") Then
                    l.coluna = fields(i).Name
                    l.valorantigo = fields(i).GetValue(objetoexcluido)
                    WriteLogCadastros(l)
                End If
            Next
        End If
    End Sub
    Public Sub WriteLogCadastros(log As loginfo)
        Try
            fbs.OpenDatabase()
            If usuarioacesso = "" Then usuarioacesso = log.usuario
            log.usuario = usuarioacesso
            Dim sql As String = ""
            sql = "INSERT INTO LOGCADASTROS (ID,usuario,nometabela,chavetabela,coluna,acao,valorantigo,valornovo,data) "
            sql = sql & " VALUES "
            sql = sql & " ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')"
            Dim maxid As Integer = fbs.cnum(fbs.GetQueryValue("select max(id) from LOGCADASTROS")) + 1
            With log
                If .data = "" Then .data = fbs.fData(DateTime.Now())
                sql = String.Format(sql, maxid, .usuario, .nometabela, .chavetabela, .coluna, .acao, .valorantigo, .valornovo, .data)
            End With
            fbs.ExecuteSql(sql)
            fbs.CloseDatabase()
        Catch ex As Exception
            fbs.AddErr("WriteLogCadastros:" & ex.Message)
            fbs.CloseDatabase()
        End Try

    End Sub

    Public Function GetLogCadastro(tabela As String, chavetabela As String, showimporttxt As Integer) As loginfo()
        Dim l() As loginfo
        l = Nothing
        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader
        Dim i As Integer = 0
        Dim filtrotabela As String = ""
        If UCase(tabela) = "PRCAB" Then
            ' ACRESCENTADO ISSO PARA MOSTRAR O LOG DE EXCLUÇÃO DE ITEM QUE NÃO DAVA PARA VER
            filtrotabela = String.Format(" (nometabela like '{0}' and (chavetabela like '{1}' or chavetabela like '{1}|%')) or (nometabela='PRLIN' AND ACAO='DELETE' AND  chavetabela like '{1}|%') ", tabela, chavetabela)
        ElseIf tabela.IndexOf(",") > -1 Then
            filtrotabela = filtrotabela & String.Format(" (nometabela like '{0}' or nometabela like '{1}') and (chavetabela like '{2}' or chavetabela like '{2}|%')   ", tabela.Split(",")(0), tabela.Split(",")(1), chavetabela)
        Else
            filtrotabela = String.Format(" nometabela like '{0}' and (chavetabela like '{1}' or chavetabela like '{1}|%')  ", tabela, chavetabela)
        End If



        If showimporttxt = 0 Then
            rc = fbs.GetQueryRecordsetLite(String.Format("Select * from LOGCADASTROS where {0} and acao<>'IMPORTTXT' order by data desc", filtrotabela))
        Else
            rc = fbs.GetQueryRecordsetLite(String.Format("Select * from LOGCADASTROS where {0} order by data desc", filtrotabela))
        End If

        While rc.Read()
            ReDim Preserve l(i)
            l(i) = New loginfo
            With l(i)
                .id = rc.Item("id").ToString
                .usuario = rc.Item("usuario").ToString
                .nometabela = rc.Item("nometabela").ToString
                .chavetabela = rc.Item("chavetabela").ToString
                .acao = rc.Item("acao").ToString
                .coluna = rc.Item("coluna").ToString
                .valorantigo = rc.Item("valorantigo").ToString
                .valornovo = rc.Item("valornovo").ToString
                .data = rc.Item("data").ToString
            End With
            i = i + 1
        End While
        rc.Close()
        fbs.CloseDatabase()
        Return l
    End Function


    Public Sub WriteLogEventos(log As logeventos)
        Try
            fbs.OpenDatabase()
            If usuarioacesso = "" Then usuarioacesso = log.usuario
            log.usuario = usuarioacesso
            Dim sql As String = ""
            sql = "INSERT INTO LOGEVENTOS (id,usuario,data,nomeevento,chaveevento,obs) "
            sql = sql & " VALUES "
            sql = sql & " ({0},'{1}','{2}','{3}','{4}','{5}')"

            With log
                If .data = "" Then .data = fbs.fData(DateTime.Now())
                Dim maxid As Integer = fbs.cnum(fbs.GetQueryValue("select max(id) from LOGEVENTOS")) + 1
                sql = String.Format(sql, maxid, .usuario, .data, .nomeevento, .chaveevento, .obs)
            End With
            fbs.ExecuteSql(sql)
        Catch ex As Exception
            fbs.AddErr("WriteLogEventos:" & ex.Message)
        End Try
        fbs.CloseDatabase()
    End Sub


    Public Function GetLogEventos(nomeevento As String, chaveevento As String) As logeventos()
        Dim l() As logeventos
        l = Nothing
        fbs.OpenDatabase()
        Dim rc As SQLiteDataReader
        Dim i As Integer = 0

        rc = fbs.GetQueryRecordsetLite(String.Format("Select * from logeventos where nomeevento='{0}' and chaveevento='{1}' order by data desc", nomeevento, chaveevento))

        While rc.Read()
            ReDim Preserve l(i)
            l(i) = New logeventos
            With l(i)
                .id = rc.Item("id").ToString
                .usuario = rc.Item("usuario").ToString
                .data = rc.Item("data").ToString
                .nomeevento = rc.Item("nomeevento").ToString
                .chaveevento = rc.Item("chaveevento").ToString
                .obs = rc.Item("obs").ToString
            End With
            i = i + 1
        End While
        rc.Close()
        fbs.CloseDatabase()
        Return l
    End Function
End Class

