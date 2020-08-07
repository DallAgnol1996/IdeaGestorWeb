
module.exports = {
    data() {
        return {
            itensfiltered: [],
            itens: [],
            filter_cod: '',
            filter_desCliente: '',
            filter_desTecnico: '',
            filter_dataHoraCriacao: '',
            filter_dataJob: '',
            filter_horaJob: '',
            filter_des: '',
            filter_status: '',
            titulo: 'Lista Jobs',
            listastatus: []
        }
    },
    created: function () {
    },
    mounted: function () {
        var vm = this;
        $.ajax({
            type: "POST",
            url: '/ws/consulta.asmx/GetListaJobs',
            data: {},
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
            element = event.currentTarget
            var orderType = cnum(element.getAttribute('vp-ordertype'));
            if (orderType <= 0) {
                this.itensfiltered.sort(function (a, b) {
                    return a[chave].toString().toUpperCase().localeCompare(b[chave].toString().toUpperCase())
                });
                element.setAttribute('vp-ordertype', 1);
            }
            else {
                this.itensfiltered.sort(function (a, b) {
                    return b[chave].toString().toUpperCase().localeCompare(a[chave].toString().toUpperCase())
                });
                element.setAttribute('vp-ordertype', 0);
            }
        },
        filtrartabela: function () {
            var vm = this;
            vm.itensfiltered = [];
            vm.itens.filter(function (item) {
                var contavalid = 0;
                var totvalid = 2;
                contavalid = contavalid + ValueIsInFilter(item.cod, vm.filter_cod);
                contavalid = contavalid + ValueIsInFilter(item.des, vm.filter_des);
                if (contavalid === totvalid) {
                    vm.itensfiltered.push(item);
                }
            });
        },
        GoToJob(item) {
            app.$router.push({ name: "alterajob", params: { "cod": item.cod } });
        }
    }
}
