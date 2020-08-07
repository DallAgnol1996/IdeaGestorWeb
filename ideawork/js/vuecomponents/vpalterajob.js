
module.exports = {
    data() {
        return {
            job: {
                cod: '',
                idCliente: '',
                desCliente: '',
                idUsuario: '',
                desUsuario: '',
                usuarioCriacao: '',
                dataHoraCriacao: '',
                dataJob: '',
                horaJob: '',
                idstatus: '',
                status: '',
                des: '',
                obs: '',
                codCheckList: ''
            },
            mostralogtabela: "0",
            showModal: false,
            saveOk: false,
            tpalert: 'success',
            operacao: '',
            titulo: "Job",
            listaCliente: [],
            listaTecnico: [],
            listastatus: [],
            listachecklist: []
        }
    },
    created: function () {
        var vm = this;
        $.ajax({
            type: "POST",
            url: 'ws/CONSULTA.asmx/GetListacabchecklist',
            data: {},
            dataType: "json",
            success: function (data, tpresult, obj) {
                vm.listachecklist = data;

            },
            error: function (data) {
                console.log(data.responseText);
            }
        });

        var vm = this;
        $.ajax({
            type: "POST",
            url: 'ws/CONSULTA.asmx/GetListaClientes',
            data: {},
            dataType: "json",
            success: function (data, tpresult, obj) {
                vm.listaCliente = data;

            },
            error: function (data) {
                console.log(data.responseText);
            }
        });
        var vm = this;
        $.ajax({
            type: "POST",
            url: 'ws/CONSULTA.asmx/GetListaUsuarios',
            data: {},
            dataType: "json",
            success: function (data, tpresult, obj) {
                vm.listaTecnico = data;

            },
            error: function (data) {
                console.log(data.responseText);
            }
        });
        var vm = this;
        $.ajax({
            type: "POST",
            url: 'ws/CONSULTA.asmx/GetCarOpcJson',
            data: { codcar: "STATUS", adicionavazio: 0 },
            dataType: "json",
            success: function (data, tpresult, obj) {
                vm.listastatus = data;

            },
            error: function (data) {
                console.log(data.responseText);
            }
        });
    },
    mounted: function () {
        debugger
        var vm = this;
        if (this.$route.params.cod != null && this.$route.params.cod != '') {
            var cod = this.$route.params.cod;
            $.ajax({
                type: "POST",
                url: '/ws/consulta.asmx/GetJobByCod',
                data: { cod: cod },
                dataType: "json",
                success: function (data, tpresult, obj) {
                    vm.job = data;
                },
                error: function (data) {
                    console.log(data.responseText);
                }
            });
        }
        else {
            $.ajax({
                type: "POST",
                url: '/ws/consulta.asmx/GetNewJob',
                data: { usuario: GetUsuarioAcesso() },
                dataType: "json",
                success: function (data, tpresult, obj) {
                    vm.job = data;
                },
                error: function (data) {
                    console.log(data.responseText);
                }
            });
        }
    },
    methods: {
        mostralog() {
            this.mostralogtabela = 1;
            this.showModal = true;
        },
        fecharlogtabela() {
            this.mostralogtabela = 0;
            this.showModal = false;
        },
        voltar() {
            app.$router.push({ name: "listajobs"});
        },
        salvar() {
            var vm = this;
            var u = JSON.stringify(this.job);
            var usuario = $("#username").val();
            $.ajax({
                type: "POST",
                url: 'ws/altera.asmx/SaveJob',
                data: { usuario: usuario, infojob: u },
                dataType: "json",
                success: function (data, tpresult, obj) {
                    vm.saveOk = true;
                    vm.tpalert = 'success';
                    vm.saveOk = true;
                    vm.operacao = data.messagge;
                },
                error: function (data) {
                    console.log(data.responseText);
                    vm.tpalert = 'danger',
                    vm.saveOk = false;
                    vm.operacao = "Erro " + data.iderro + " " + data.messagge;
                }
            });
        },
        excluir() {
            var vm = this;
            var u = JSON.stringify(this.job);
            var usuario = $("#username").val();
            $.ajax({
                type: "POST",
                url: 'ws/altera.asmx/DeleteJob',
                data: { usuario: usuario, infojob: u },
                dataType: "json",
                success: function (data, tpresult, obj) {
                    vm.saveOk = true;
                    vm.tpalert = 'warning';
                    vm.saveOk = true;
                    vm.operacao = data.messagge;
                },
                error: function (data) {
                    console.log(data.responseText);
                    vm.tpalert = 'danger',
                    vm.saveOk = false;
                    vm.operacao = "Erro " + data.iderro + " " + data.messagge;
                }
            });
        }
    }
}
