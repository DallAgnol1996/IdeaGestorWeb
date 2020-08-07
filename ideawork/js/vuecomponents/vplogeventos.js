
    module.exports = {
        props: {
            nomeevento: {
                default: ""
            },
            chaveevento: {
                default: ""
            }
        },
        data() {
            return {
                itensfiltered: [],
                itens: [],
            };
        },
        created: function () {
            var vm = this;
            $.ajax({
                type: "POST",
                url: '/ws/LogConsulta.asmx/GetLogEventos',
                data: { nomeevento: vm.nomeevento, chaveevento: vm.chaveevento },
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
        }
    }
