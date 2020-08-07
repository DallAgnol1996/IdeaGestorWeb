
    module.exports = {
        data() {
            return {
                itensfiltered: [],
                itens: [],
                filter_cod: '',
                filter_des: '',
                tipo: '',
                desbotton: "",
                titulo: "Lista Tabelas"
            };
        },
        created: function () {

            var vm = this;
            vm.tipo = "";

            if (this.$route.params.tipo != null) {
                vm.tipo = this.$route.params.tipo;
            }

            if (vm.tipo !== "") {
                if (vm.tipo === "C") {
                    vm.desbotton = "Cadastrar nova caracteristica";
                    vm.titulo = "Lista caracteristicas";
                }
                else if (vm.tipo === "F") {
                    vm.desbotton = "Cadastrar novo filtro inicial";
                    vm.titulo = "Lista filtros";
                }
                else if (vm.tipo === "T") {
                    vm.desbotton = "Cadastrar nova tabela";
                    vm.titulo = "Lista tabelas";
                }
                $.ajax({
                    type: "POST",
                    url: '/ws/consulta.asmx/GetCarCabJson',
                    data: { tipo: vm.tipo },
                    dataType: "json",
                    success: function (data, tpresult, obj) {
                        vm.itens = data;
                        vm.itensfiltered = data;
                    },
                    error: function (data) {
                        console.log(data.responseText);
                    }
                });
            }
        },
        methods: {
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
            GoToCaracteristica: function (familia) {
                app.$router.push({ name: "alteracatalogocaracteristicas", params: { "cod": familia.cod } });
                //app.$router.go();
            },
            filtrartabela: function () {
                var vm = this;
                vm.itensfiltered = [];
                vm.itens.filter(function (item) {
                    var validcod = false;
                    var validdes = false;

                    if (vm.filter_cod !== '' && vm.filter_cod !== null) {
                        if (item.cod.toString().toUpperCase().indexOf(vm.filter_cod.toString().toUpperCase()) > -1) {
                            validcod = true;
                        }
                    }
                    else {
                        validcod = true;
                    }
                    if (vm.filter_des !== '' && vm.filter_des !== null) {
                        if (item.des.toString().toUpperCase().indexOf(vm.filter_des.toString().toUpperCase()) > -1) {
                            validdes = true;
                        }
                    }
                    else {
                        validdes = true;
                    }

                    if (validcod === true && validdes === true) {
                        vm.itensfiltered.push(item);
                    }
                });

            }
        }
    }
