
module.exports = {
    data() {
        return {
            cliente: {
                cod: '',
                des: '',
                email: '',
            },
            mostralogtabela: "0",
            showModal: false,
            saveOk: false,
            tpalert: 'success',
            operacao: '',
            titulo: "Cliente",
            listaref: []
        }
    },
    created: function () {
        var vm = this;
        $.ajax({
            type: "POST",
            url: 'ws/CONSULTA.asmx/GetListaUsuarios',
            data: {},
            dataType: "json",
            success: function (data, tpresult, obj) {
                vm.listaref = data;

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
                url: '/ws/consulta.asmx/GetClienteByCod',
                data: { cod: cod },
                dataType: "json",
                success: function (data, tpresult, obj) {
                    vm.cliente = data;
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
            app.$router.push({ name: "listaclientes" });
        },
        salvar() {
            var vm = this;
            var u = JSON.stringify(this.cliente);
            var cliente = $("#username").val();
            $.ajax({
                type: "POST",
                url: 'ws/altera.asmx/SaveCliente',
                data: { cliente: cliente, infousuario: u },
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
            var u = JSON.stringify(this.cliente);
            var cliente = $("#username").val();
            $.ajax({
                type: "POST",
                url: 'ws/altera.asmx/DeleteCliente',
                data: { cliente: cliente, infousuario: u },
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
