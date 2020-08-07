
module.exports = {
    data() {
        return {
            c: {
                cab: {
                    cod: "", des: ""
                }
                ,
                grupos: [{
                    itens: []
                }]
            },
            novogrupo: { id: 0, ord: 0, des: '' },
            mostralogtabela: "0",
            showModal: false,
            showNovoGrupo: false,
            saveOk: false,
            editGroup: false,
            grupoedit: 0,
            tpalert: 'success',
            operacao: '',
            idAccordion: 0,
            showAccordion: false,
            titulo: "Check-list",
            listaativo: [{ cod: '0', des: "Sim" },{ cod: '1', des: "Não" }],
            listacodchecklist: []
        }
    },
    mounted: function () {
        var vm = this;
        if (this.$route.params.cod != null && this.$route.params.cod != '') {
            var cod = this.$route.params.cod;
            $.ajax({
                type: "POST",
                url: '/ws/consulta.asmx/GetChecklist',
                data: { cod: cod },
                dataType: "json",
                success: function (data, tpresult, obj) {
                    vm.c = data;
                },
                error: function (data) {
                    console.log(data.responseText);
                }
            });
        }
        else {

            $.ajax({
                type: "POST",
                url: '/ws/consulta.asmx/GetNewCheckList',
                data: {},
                dataType: "json",
                success: function (data, tpresult, obj) {
                    vm.c = data;
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
            app.$router.push({ name: "listachecklist" });
        },
        entra() {
            this.editGroup = true;
        },
        mostrarGrupo() {
            this.idAccordion = 0;
            if (this.showAccordion) {
                this.showAccordion = false;
            } else {
                this.showAccordion = true;
            }
        },
        abreAccordion(id) {
            this.idAccordion = id;
        },
        addItem(g) {
            debugger
            var maxiditem = 0;
            if (g.itens != null) {
                for (var i = 0; i < g.itens.length; i++) {
                    if (cnum(g.itens[i].id) >= maxiditem) {
                        maxiditem = cnum(g.itens[i].id) + 1;
                    }
                }
            }
            //var position = g.id - 1;
            var newitem = {
                codchecklist: g.codchecklist,
                idgrupo: g.id,
                id: maxiditem,
                ord: maxiditem,
                des: "",
                referencia: "",
                fotoobrigatoria: 0
            }
            g.itens.push(newitem);
           // this.c.grupos[position].itens.push(newitem)

        },

        excluiropcao(g,i) {
            debugger
            var vm = this;
            var u = JSON.stringify(i);
            var usuario = $("#username").val();
            $.ajax({
                type: "POST",
                url: 'ws/altera.asmx/Deleteitemchecklist',
                data: { usuario: usuario, item: u },
                dataType: "json",
                success: function (data, tpresult, obj) {
                    vm.saveOk = true;
                    vm.tpalert = 'warning';
                    vm.operacao = data.messagge;
                    var position = 0;
                    for (var j = 0; j < g.itens.length; j++) {
                        if (cnum(g.itens[j].id) == i.id) {
                            position = j;
                            break;

                        }
                    }
                    
                    g.itens.splice(position, 1)
                },
                error: function (data) {
                    console.log(data.responseText);
                    vm.tpalert = 'danger',
                        vm.saveOk = false;
                    vm.operacao = "Erro " + data.iderro + " " + data.messagge;
                }
            });


        },
        addGrupo() {

            this.showNovoGrupo = true;
        },
        editarGrupo(id) {
            this.grupoedit = id;
        },
        salvarGrupo() {
            debugger
            var maxidgrupo = 0;
            for (var i = 0; i < this.c.grupos.length; i++) {
                if (cnum(this.c.grupos[i].id) >= maxidgrupo) {
                    maxidgrupo = cnum(this.c.grupos[i].id) + 1;
                }
            }
            this.showNovoGrupo = false;
            var newgrupo = {
                codchecklist: this.c.cab.cod,
                id: maxidgrupo,
                des: this.novogrupo.des,
                ord: this.novogrupo.ord,
                itens: []
            }
            this.c.grupos.push(newgrupo);
        },
        salvar() {

            var vm = this;
            var c = JSON.stringify(this.c);
            var usuario = $("#username").val();
            $.ajax({
                type: "POST",
                url: 'ws/altera.asmx/SaveChecklist',
                data: { usuario: usuario, checklist: c },
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
