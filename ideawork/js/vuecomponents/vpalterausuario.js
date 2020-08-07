
module.exports = {
    data() {
        return {
            usuario: {
                cod: '',
                des: ''
            },
            mostralogtabela: "0",
            showModal: false,
            saveOk: false,
            tpalert: 'success',
            operacao: '',
            titulo: "Usuário",
            listaativo: [{ cod: '0', des: "Não" }, { cod: '1', des: "Sím" }],
            listatipos: [{ cod: '1', des: "Adm" }, { cod: '2', des: "Outro" }]
        }
    },
    created: function () {
        var vm = this;
        $.ajax({
            type: "POST",
            url: 'ws/CONSULTA.asmx/GetCarOpcJson',
            data: { codcar: "TIPOUSUARIO", adicionavazio: 0 },
            dataType: "json",
            success: function (data, tpresult, obj) {
                vm.listatipos = data;

            },
            error: function (data) {
                console.log(data.responseText);
            }
        });

    },
    mounted: function () {
        var vm = this;
        if (this.$route.params.cod != null && this.$route.params.cod != '') {
            var cod = this.$route.params.cod;
            $.ajax({
                type: "POST",
                url: '/ws/consulta.asmx/getusuariobycod',
                data: { cod: cod },
                dataType: "json",
                success: function (data, tpresult, obj) {
                    vm.usuario = data;
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
            app.$router.push({ name: "listausuarios"});
        },
        salvar() {
            debugger
            var vm = this;
            var u = JSON.stringify(this.usuario);
            var usuario = $("#username").val();
            $.ajax({
                type: "POST",
                url: 'ws/altera.asmx/SaveUsuario',
                data: { usuario: usuario, infousuario: u },
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
            debugger
            var vm = this;
            var u = JSON.stringify(this.usuario);
            var usuario = $("#username").val();
            $.ajax({
                type: "POST",
                url: 'ws/altera.asmx/DeleteUsuario',
                data: { usuario: usuario, infousuario: u },
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
