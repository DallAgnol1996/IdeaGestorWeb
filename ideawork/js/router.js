//navbar
var vpnavbar = httpVueLoader('templates/vpnavbar.vue');
vpnavbar = Vue.component('vp-navbar', vpnavbar);

//home
var vphome = httpVueLoader('templates/vphome.vue');
vphome = Vue.component('vp-home', vphome);

var vplistausuarios = httpVueLoader('templates/vplistausuarios.vue');
vplistausuarios = Vue.component('vp-listausuarios', vplistausuarios);
var vplistaclientes = httpVueLoader('templates/vplistaclientes.vue');
vplistaclientes = Vue.component('vp-listaclientes', vplistaclientes);
var vplistajobs = httpVueLoader('templates/vplistajobs.vue');
vplistajobs = Vue.component('vp-listajobs', vplistajobs);
var vplistachecklist = httpVueLoader('templates/vplistachecklist.vue');
vplistachecklist = Vue.component('vp-listachecklist', vplistachecklist);


var vpalterausuario = httpVueLoader('templates/vpalterausuario.vue');
vpalterausuario = Vue.component('vp-alterausuario', vpalterausuario);
var vpalteracliente = httpVueLoader('templates/vpalteracliente.vue');
vpalteracliente = Vue.component('vp-alteracliente', vpalteracliente);
var vpalterajob = httpVueLoader('templates/vpalterajob.vue');
vpalterajob = Vue.component('vp-alterajob', vpalterajob);
var vpalterachecklist = httpVueLoader('templates/vpalterachecklist.vue');
vpalterachecklist = Vue.component('vp-alterachecklist', vpalterachecklist);
var vpalteragrupochecklist = httpVueLoader('templates/vpalteragrupochecklist.vue');
vpalteragrupochecklist = Vue.component('vp-alteragrupochecklist', vpalteragrupochecklist);
//base
var vpimagedialog = httpVueLoader('templates/vpimagedialog.vue');
vpimagedialog = Vue.component('vp-imagedialog', vpimagedialog);
var vpinputpassword = httpVueLoader('templates/vpinputpassword.vue');
vpinputpassword = Vue.component('vp-inputpassword', vpinputpassword);
var vpinputdata = httpVueLoader('templates/vpinputdata.vue');
vpinputdata = Vue.component('vp-inputdata', vpinputdata);
var vpinputnumber = httpVueLoader('templates/vpinputnumber.vue');
vpinputnumber = Vue.component('vp-inputnumber', vpinputnumber);
var vpinputcheckbox = httpVueLoader('templates/vpinputcheckbox.vue');
vpinputcheckbox = Vue.component('vp-inputcheckbox', vpinputcheckbox);
var vpinputtextarea = httpVueLoader('templates/vpinputtextarea.vue');
vpinputtextarea = Vue.component('vp-inputtextarea', vpinputtextarea);
var vpinputtext = httpVueLoader('templates/vpinputtext.vue');
vpinputtext = Vue.component('vp-inputtext', vpinputtext);
var vpmessage = httpVueLoader('templates/vpmessage.vue');
vpmessage = Vue.component('vp-message', vpmessage);
var vpinputupload = httpVueLoader('templates/vpinputupload.vue');
vpinputupload = Vue.component('vp-inputupload', vpinputupload);
var vpselect = httpVueLoader('templates/vpselect.vue');
vpselect = Vue.component('vp-select', vpselect);
var vpselectsmall = httpVueLoader('templates/vpselectsmall.vue');
vpselectsmall = Vue.component('vp-selectsmall', vpselectsmall);
var vpinfo = httpVueLoader('templates/vpinfo.vue');
vpinfo = Vue.component('vp-info', vpinfo);
var vpuploadsinglefile = httpVueLoader('templates/vpuploadsinglefile.vue');
vpuploadsinglefile = Vue.component('vp-uploadsinglefile', vpuploadsinglefile);

//log
var vplogcadastro = httpVueLoader('templates/vplogcadastro.vue');
vplogcadastro = Vue.component('vp-logcadastro', vplogcadastro);
var vplogeventos = httpVueLoader('templates/vplogeventos.vue');
vplogeventos = Vue.component('vp-logeventos', vplogeventos);

var vpalteracatalogocaracteristicas = httpVueLoader('templates/vpalteracatalogocaracteristicas.vue');
var vpCatalogoCaracteristicas = httpVueLoader('templates/vpCatalogoCaracteristicas.vue');
vpCatalogoCaracteristicas = Vue.component('vp-catalogocaracteristicas', vpCatalogoCaracteristicas);
var vpAlteraCatalogoProdutos = httpVueLoader('templates/vpAlteraCatalogoProdutos.vue');


var routes = [
    { path: '/', component: vphome },
    { path: '/home', name: 'home', component: vphome },
    { path: '/listausuarios', name: 'listausuarios', component: vplistausuarios },
    { path: '/listaclientes', name: 'listaclientes', component: vplistaclientes },
    { path: '/listajobs', name: 'listajobs', component: vplistajobs },
    { path: '/listachecklist', name: 'listachecklist', component: vplistachecklist },
    { path: '/alterausuario', name: 'alterausuario', component: vpalterausuario },
    { path: '/alteracliente', name: 'alteracliente', component: vpalteracliente },
    { path: '/alterajob', name: 'alterajob', component: vpalterajob },
    { path: '/alterachecklist', name: 'alterachecklist', component: vpalterachecklist },
    { path: '/vpalteragrupochecklist', name: 'vpalteragrupochecklist', component: vpalteragrupochecklist },
    { path: '/CatalogoCaracteristicas', name: 'CatalogoCaracteristicas', component: vpCatalogoCaracteristicas, params: { tipo: "" } },
    { path: '/alteracatalogocaracteristicas', name: 'alteracatalogocaracteristicas', component: vpalteracatalogocaracteristicas }

];

var approuter = new VueRouter({
    routes // short for routes: routes
});