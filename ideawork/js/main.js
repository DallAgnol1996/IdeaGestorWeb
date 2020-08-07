

var app = new Vue({
    el: "#app",
    router: approuter,
    data: {
        empresabase: ""
    },
    created: function () {
        Vue.config.devtools = true;
        var vm = this;
        vm.empresabase = GetEmpresaBase();
    },
    methods: {
    },
    components: {

        // componentes de log 
        'vp-logcadastro': vplogcadastro,
        'vp-logeventos': vplogeventos,
        // componentes de base
        'vp-inputext': vpinputtext,
        'vp-inputnumber': vpinputnumber,
        'vp-inputdata': vpinputdata,
        'vp-inputupload': vpinputupload,
        'vp-inputtextarea': vpinputtextarea,
        'vp-select': vpselect,
        'vp-info': vpinfo,
        'vp-inputcheckbox': vpinputcheckbox,
        'vp-inputpassword': vpinputpassword,
        'vp-uploadsinglefile': vpuploadsinglefile,
        'vp-imagedialog': vpimagedialog
    }
});


