
module.exports = {
    data() {
        return {
            desusuario: "",
            empresabase: ""
        };
    },
    created: function () {
        var vm = this;
        vm.empresabase = GetEmpresaBase();
        var desusuario = $("#desusername").val();
        this.desusuario = desusuario;
    },
    methods: {
    }
};