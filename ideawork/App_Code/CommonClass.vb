Imports Microsoft.VisualBasic

Public Class CommonClass

    Public Class erro
        Public iderro As Integer = 0
        Public messagge As String = ""
        Public infoerro As String = ""
        Public deserro As String = ""
        Public ex As Object = Nothing
        Public Sub SaveErro()

            ' aqui podemos gerenciar o salva do log no database
        End Sub
    End Class
    Public Class loginfo
        Public id As Integer = 0
        Public usuario As String = ""
        Public nometabela As String = ""
        Public chavetabela As String = ""
        Public acao As String = ""
        Public coluna As String = ""
        Public valorantigo As String = ""
        Public valornovo As String = ""
        Public data As String = ""
    End Class
    Public Class logeventos
        Public id As Integer = 0
        Public usuario As String = ""
        Public data As String = ""
        Public nomeevento As String = ""
        Public chaveevento As String = ""
        Public obs As String = ""
    End Class

    Public Class anexos
        Public id As Integer = 0
        Public tipo As Integer = 0 ' 1=anexo orcamento, 2=anexo linha orcamento
        Public ref1 As String = "0"
        Public ref2 As String = "0"
        Public ref3 As String = "0"
        Public ref4 As String = "0"
        Public pastasave As String = ""
        Public arquivo As String = ""
        Public data As String = ""
        Public codusuario As String = ""
        Public desdata As String = ""
        Public arquivorootbase As String = ""
        Public nomearquivo As String = ""
    End Class

    Public Class usuarios
        Public cod As String = ""
        Public des As String = ""
        Public senha As String = ""
        Public tipo As Integer = 0
        Public destipo As String = ""
        Public ativo As String = 0
        Public email As String = ""
    End Class
    Public Class CarCab
        Public cod As String = ""
        Public des As String = ""
        Public img As String = ""
        Public campos As String = ""
        Public carref As String = ""
        Public tipopreview As String = ""
        Public tipo As String = ""
        Public valor As String = ""
    End Class

    Public Class CarOpc
        Public codcar As String = ""
        Public cod As String = ""
        Public des As String = ""
        Public cat As String = ""
        Public img As String = ""
        Public coditem As String = ""
        Public ord As Integer = 0
        Public linkdoc As String = ""
        Public campos As String = ""
        Public nivel As Integer = 0
        Public cAdd() As String = {}
        Public ativo As Integer = 1
    End Class
    Public Class CarCat
        Public codcar As String = ""
        Public cod As String = ""
        Public des As String = ""
        Public img As String = ""
        Public nivel As Integer = 0
    End Class
    Public Class Clientes
        Public cod As String = ""
        Public des As String = ""
        Public idref As String = ""
        Public ref As String = ""
        Public email As String = ""
    End Class



    Public Class Job
        Public cod As Integer = 0
        Public idCliente As String = ""
        Public desCliente As String = ""
        Public idUsuario As String = ""
        Public desUsuario As String = ""
        Public usuarioCriacao As String = ""
        Public dataHoraCriacao As String = ""
        Public dataJob As String = ""
        Public horaJob As String = ""
        Public idstatus As Integer = 0
        Public status As String = ""
        Public des As String = ""
        Public obs As String = ""
        Public codCheckList As Integer = 0
    End Class
    Public Class cabchecklist
        Public cod As Integer = 0
        Public des As String = ""
        Public obs As String = ""
        Public ativo As Integer = 0
    End Class

    Public Class grupochecklist
        Public codchecklist As Integer = 0
        Public id As Integer = 0
        Public des As String = ""
        Public ord As Integer = 0
        Public itens() As itenschecklist
        Sub New()
            codchecklist = 0
            id = 0
            des = ""
            ord = 0
            ReDim Preserve itens(-1)
        End Sub
    End Class

    Public Class itenschecklist
        Public codchecklist As Integer = 0
        Public idgrupo As Integer = 0
        Public id As Integer = 0
        Public ord As Integer = 0
        Public des As String = ""
        Public referencia As String = ""
        Public fotoobrigatoria As Integer = 0
        Public executado As Integer = -1
        Public nomefoto As String = ""
        Public obs As String = ""
    End Class
    Public Class checklist
        Public cab As cabchecklist
        Public grupos() As grupochecklist
        Sub New()
            cab = New cabchecklist
            ReDim Preserve grupos(-1)
        End Sub

    End Class



End Class
