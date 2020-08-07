
module.exports = {
    data() {
        return {
            itensfiltered: [],
            itens: [],
            item: [],
            filter_cod: '',
            filter_des: '',
            filter_img: '',
            filter_cat: '',
            filter_coditem: '',
            filter_ord: '',
            filter_linkdoc: '',
            titulo: "Característica",
            car: {
                cod: "",
                des: "",
                img: "",
                campos: "",
                carref: "",
                tipopreview: "",
                tipo: "",
                ativo: "",
            },
            ativossimnao: [{ cod: '1', des: 'Sím' }, { cod: '0', des: 'Não' }],
            mostradetalhe: 0,
            mostracategorias: 0,
            mostralogtabela: 0,
            saveOk: false,
            tpalert: 'success',
            operacao: '',
            mostrapreview: 0,
            colsitem: [],
            categorias: [],
            itemdetalhe: [],
            tipopreviews: [
                { cod: "IMAGENS", des: "Imagens" }, { cod: "LISTAS", des: "Lista simples" }
            ],
            tiposcaracteristicas: [{ cod: "C", des: "Caracteristica" }, { cod: "T", des: "Tabela" }, { cod: "F", des: "Filtro Inicial" }]
        };
    },
    mounted: function () {
        if (this.$route.params.cod != null) {
            var cod = app.$route.params.cod;
            this.loadcar(cod);
            this.loadopcs(cod);
            this.loadcategorias(cod);
        }
        else {
            this.car.tipo = this.$route.params.tipo;
            this.definetitulo();
        }

    },
    methods: {
        definetitulo: function () {
            if (this.car.tipo === "C") {
                this.titulo = "Cadastro Caracteristica";
            }
            else if (this.car.tipo === "T") {
                this.titulo = "Cadastro Tabela";
            }
            if (this.car.tipo === "F") {
                this.titulo = "Cadastro Filtro";
            }
        },
        loadcar: function (cod) {
            var vm = this;
            $.ajax({
                type: "POST",
                url: '/ws/consulta.asmx/GetCabCarByCodJson',
                data: { cod: cod },
                dataType: "json",
                success: function (data, tpresult, obj) {
                    vm.car = data;
                    vm.definetitulo();
                },
                error: function (data) {
                    console.log(data.responseText);
                }
            });
        },
        loadopcs: function (cod) {
            var vm = this;
            $.ajax({
                type: "POST",
                url: '/ws/consulta.asmx/GetCarOpcJson',
                data: { codcar: cod, adicionavazio: 1 },
                dataType: "json",
                success: function (data, tpresult, obj) {
                    vm.itens = data;
                    vm.itensfiltered = data;
                },
                error: function (data) {
                    console.log(data.responseText);
                }
            });
        },
        loadcategorias: function (cod) {
            var vm = this;
            $.ajax({
                type: "POST",
                url: '/ws/consulta.asmx/GetCarCatJson',
                data: { codcar: cod, adicionavazio: 1 },
                dataType: "json",
                success: function (data, tpresult, obj) {
                    vm.categorias = data;
                },
                error: function (data) {
                    console.log(data.responseText);
                }
            });
        },
        ordenatabela: function (chave, event) {
            element = event.currentTarget;
            var orderType = cnum(element.getAttribute('vp-ordertype'));
            if (orderType <= 0) {
                this.itensfiltered.sort(function (a, b) {
                    return a[chave].toString().toUpperCase().localeCompare(b[chave].toString().toUpperCase());
                });
                element.setAttribute('vp-ordertype', 1);
            }
            else {
                this.itensfiltered.sort(function (a, b) {
                    return b[chave].toString().toUpperCase().localeCompare(a[chave].toString().toUpperCase());
                });
                element.setAttribute('vp-ordertype', 0);
            }
        },
        filtrartabela: function () {
            var vm = this;
            vm.itensfiltered = [];
            vm.itens.filter(function (item) {
                var contavalid = 0;
                var totvalid = 7;
                contavalid = contavalid + ValueIsInFilter(item.cod, vm.filter_cod);
                contavalid = contavalid + ValueIsInFilter(item.des, vm.filter_des);
                contavalid = contavalid + ValueIsInFilter(item.coditem, vm.filter_coditem);
                contavalid = contavalid + ValueIsInFilter(item.ord, vm.filter_ord);
                contavalid = contavalid + ValueIsInFilter(item.img, vm.filter_img);
                contavalid = contavalid + ValueIsInFilter(item.cat, vm.filter_cat);
                contavalid = contavalid + ValueIsInFilter(item.linkdoc, vm.filter_linkdoc);
                if (contavalid === totvalid) {
                    vm.itensfiltered.push(item);
                }
            });

        },
        salvar: function () {
            var c = JSON.stringify(this.car);
            var usuario = $("#username").val();
            var vm = this;
            $.ajax({
                type: "POST",
                url: 'ws/altera.asmx/SaveCarCab',
                data: { usuario: usuario, carateristica: c },
                dataType: "json",
                success: function (data, tpresult, obj) {
                    vm.tpalert = 'success';
                    vm.saveOk = true;
                    vm.operacao = data.messagge;
                    vm.loadopcs(vm.car.cod);
                    // app.$router.push({ name: "CatalogoCaracteristicas" });
                },
                error: function (data) {
                    console.log(data.responseText);
                    vm.tpalert = 'danger',
                    vm.saveOk = false;
                    vm.operacao = "Erro " + data.iderro + " " + data.messagge;
                }
            });
        },
        excluir: function () {
            var c = JSON.stringify(this.car);
            var usuario = $("#username").val();
            $.ajax({
                type: "POST",
                url: 'ws/altera.asmx/DeleteCarCab',
                data: { usuario: usuario, carateristica: c },
                dataType: "json",
                success: function (data, tpresult, obj) {
                    vm.tpalert = 'warning';
                    vm.saveOk = true;
                    vm.operacao = data.messagge;
                },
                error: function (data) {
                    vm.tpalert = 'danger',
                    vm.saveOk = false;
                    vm.operacao = "Erro " + data.iderro + " " + data.messagge;
                }
            });
        },

        salvaopcao: function (item) {
            var o = JSON.stringify(item);
            var usuario = $("#username").val();
            var vm = this;
            $.ajax({
                type: "POST",
                url: 'ws/altera.asmx/SaveCarOpc',
                data: { usuario: usuario, opcao: o },
                dataType: "text",
                success: function (data, tpresult, obj) {
                    alertform(data);
                    vm.loadopcs(item.codcar);

                },
                error: function (data) {
                    console.log(data.responseText);
                }
            });
        },
        detalheopcao: function (item) {
            var cc = this.car.campos.split(",");
            var ccvalue = item.campos.split(",");
            var i = 0;
            this.colsitem = [];
            ccvalue.lenght = cc.length;
            for (i = 0; i < cc.length; i++) {
                this.colsitem.lenght = this.colsitem.length + 1;
                this.colsitem[i] = [];
                this.colsitem[i].des = cc[i];
                this.colsitem[i].value = ccvalue[i];
                this.colsitem[i].id = i;
            }
            this.mostradetalhe = 1;
            this.itemdetalhe = item;
        },
        salvardetalhe: function () {
            var i = 0;
            this.itemdetalhe.campos = "";
            for (i = 0; i < this.colsitem.lenght; i++) {
                this.itemdetalhe.campos = this.itemdetalhe.campos + replaceAll(this.colsitem[i].value, ",", ".") + ",";
                this.mostradetalhe = 0;
            }
            this.salvaopcao(this.itemdetalhe);
        },
        fechardetalhe: function () {
            this.mostradetalhe = 0;
        },
        excluiropcao: function (item) {
            var o = JSON.stringify(item);
            var usuario = $("#username").val();
            var vm = this;
            $.ajax({
                type: "POST",
                url: 'ws/altera.asmx/DeleteCarOpc',
                data: { usuario: usuario, opcao: o },
                dataType: "text",
                success: function (data, tpresult, obj) {
                    alertform(data);
                    vm.loadopcs(item.codcar);

                },
                error: function (data) {
                    console.log(data.responseText);
                }
            });

        },
        showcategorias: function () {
            this.mostracategorias = 1;
        },
        salvacategoria: function (categoria) {
            var c = JSON.stringify(categoria);
            var usuario = $("#username").val();
            var vm = this;
            $.ajax({
                type: "POST",
                url: 'ws/altera.asmx/SaveCarCat',
                data: { usuario: usuario, categoria: c },
                dataType: "text",
                success: function (data, tpresult, obj) {
                    alertform(data);
                    vm.loadcategorias(categoria.codcar);
                },
                error: function (data) {
                    console.log(data.responseText);
                }
            });
        },
        excluircategoria: function (categoria) {
            var c = JSON.stringify(categoria);
            var usuario = $("#username").val();
            var vm = this;
            $.ajax({
                type: "POST",
                url: 'ws/altera.asmx/DeleteCarCat',
                data: { usuario: usuario, categoria: c },
                dataType: "text",
                success: function (data, tpresult, obj) {
                    alertform(data);
                    vm.loadcategorias(categoria.codcar);

                },
                error: function (data) {
                    console.log(data.responseText);
                }
            });
        },
        fecharcategorias: function () {
            this.mostracategorias = 0;
        },
        exportar: function () {
            var vm = this;
            $.ajax({
                type: "POST",
                url: '/ws/consulta.asmx/GetCarTxt',
                data: { codcar: vm.car.cod },
                dataType: "text",
                success: function (data, tpresult, obj) {
                    downloadarquivo(data);
                },
                error: function (data) {
                    console.log(data.responseText);
                }
            });
        },
        importararquivo: function () {

            var usuario = $("#username").val();
            var vm = this;
            $.ajax({
                type: "POST",
                url: 'ws/altera.asmx/ImportCarTxt',
                data: { usuario: usuario, codcar: vm.car.cod },
                dataType: "text",
                success: function (data, tpresult, obj) {
                    alertform(data);
                    vm.loadopcs(vm.car.cod);
                },
                error: function (data) {
                    console.log(data.responseText);
                }
            });
        },
        fecharlogtabela: function () {
            this.mostralogtabela = 0;
        },
        mostralog: function () {
            this.mostralogtabela = 1;
        },
        showpreview: function () {
            this.mostrapreview = 1;
        },
        fecharpreview: function () {
            this.mostrapreview = 0;
        },
        voltar() {
            app.$router.push({ name: "CatalogoCaracteristicas", params: { tipo: 'T' } });
        }

    }
}
