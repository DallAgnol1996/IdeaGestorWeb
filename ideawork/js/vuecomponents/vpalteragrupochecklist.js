
module.exports = {
    data() {
        return {
            itemchecklist: {
                codchecklist: '',
                idgrupo: '',
                id: '',
                ord: '',
                des: '',
                referencia: '',
                fotoobrigatoria: ''
            },
            mostralogtabela: "0",
            showModal: false,
            saveOk: false,
            tpalert: 'success',
            operacao: '',
            titulo: "Item Check-list",
            listacodchecklist: []
        }
    },
    created: function () {

    },
    mounted: function () {

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
            app.$router.push({ name: "listachecklist"});
        },
        salvar() {
            debugger
            var vm = this;
            var u = JSON.stringify(this.cabchecklist);
            var usuario = $("#username").val();
            $.ajax({
                type: "POST",
                url: 'ws/altera.asmx/Savecabchecklist',
                data: { usuario: usuario, cabcheck: u },
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
            var u = JSON.stringify(this.cabchecklist);
            var usuario = $("#username").val();
            $.ajax({
                type: "POST",
                url: 'ws/altera.asmx/Deletecabchecklist',
                data: { usuario: usuario, cabcheck: u },
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
