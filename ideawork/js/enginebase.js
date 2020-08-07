function ordenaArrayNumeros(arraystring, separador) {
    if (arraystring.indexOf(";") > -1) {
        arraystring = arraystring.split(separador);
        arraystring.sort(function (a, b) { return a - b; });
        arraystring = arraystring.join(separador);
    }
    return arraystring;
}

function removeDuplicados(array) {
    return array.filter((a, b) => array.indexOf(a) === b)
}


// Converte caracteres para zero e limita um numero a duas casas decimais
function cnum(num) {

    if ($.isNumeric(num) === false) {
        num = 0;
    } else {
        num = parseFloat(num);
    }
    return num;
}

function imageExists(image_url) {
    var txfind = "," + image_url.toUpperCase() + ",";
    if (app.listimg.indexOf(txfind) > 0) {
        return true;
    }
    else {
        console.log("O arquivo " + image_url + " não existe no servidor");
        return false;
    }
}

function completarCotas(num, medida, id, orient) {
    // parametros
    // num = valor do input onde o usuario coloca os numero de dobradicas filettos etc 
    // medida = ao paramentro recebido através da function load, tamanho do vidro, porta etc
    // id = id utilizado para o input, a functio, varre os input, soma-os, divide pela Medida e distribui a sobra para os inputs vazios 
    // orient = 1 = divisoes verticais, 2 = para divisoes horizontais  
    try {
        var i = 0;
        var v = [];
        var inicio;

        //verifica se existe existe o elemento comecado em zero
        if ($("#" + id + "0").length) {
            inicio = 0;
        } else {
            inicio = 1;
        }

        for (i = inicio; i <= num; i++) {
            v[i] = cnum($("#" + id + i).val());
        }

        var soma = arraysoma(v);

        var vazios = 0;
        for (i = inicio; i <= num; i++) {
            if (cnum($("#" + id + i).val()) === 0) {
                ++vazios;
            }
        }
        var result = parseFloat((medida - soma) / vazios).toFixed(2);

        // se existe o elemento zero  
        if ($("#" + id + "0").length) {
            inicio = 0;
            for (i = inicio; i <= num; i++) {
                if (cnum($("#" + id + i).val()) === 0) {
                    cnum($("#" + id + i).val(result));
                }
                if (orient === 1) {
                    // divide na altura

                    $("#" + id + i + "_cell").css("height", cnum($("#" + id + i).val()) / medida * 100 + "%");


                }
                if (orient === 2) {

                    // divide na largura
                    $("#" + id + i + "_cell").css("width", cnum($("#" + id + i).val()) / medida * 100 + "%");
                }

            }

        } else {
            //se não existe o elemento zero
            inicio = 1;
            //p = 1

            //loop para achar em qual numero o primeiro elemento comeca 
            // while (!$("#" + id + p++).length) {
            // }
            // var res = parseInt(p) + parseInt(num)

            for (i = inicio; i <= num; i++) {
                if (cnum($("#" + id + i).val()) === 0) {
                    cnum($("#" + id + i).val(result));
                }
                if (orient === 1) {
                    // divide na altura

                    $("#" + id + i + "_cell").css("height", cnum($("#" + id + i).val()) / medida * 100 + "%");
                }
                if (orient === 2) {
                    // divide na largura
                    $("#" + id + i + "_cell").css("width", cnum($("#" + id + i).val()) / medida * 100 + "%");
                }
            }
        }
    }
    catch (ex) {
        console.log(ex.message);
    }
}



function msgERR(mensagem) {

    alertform(mensagem);

}

function arraysoma(arrayValues) {
    var arraysoma = 0;
    if (arrayValues.length > 0) {
        for (i in arrayValues) {
            //se for numerico efetua a soma dos elementos 
            if (typeof arrayValues[i] === "number") {
                arraysoma += arrayValues[i];
            }
        }

    }
    return arraysoma;
}

//metodo string para substituir um caracter recorrente em uma string. Modo de utilizacao var string.trocarDePara.

String.prototype.trocarDePara = function (de, para) {
    var str = this;
    var pos = str.indexOf(de);
    while (pos > -1) {
        str = str.replace(de, para);
        pos = str.indexOf(de);
    }
    return str;
};

function alerta() {
    var qtdA = arguments.length;
    var string = "";
    for (i = 0; i < qtdA; i++) {
        string = string.concat(arguments[i]) + "\n";
    }
    alertform(string);
}
//ver se um elemento jquery existe
function verSeExiste(elemento) {
    if (elemento.length) {
        return true;
    } else {
        return false;
    }
}
function replaceAll(txt, replace, with_this) {
    if (txt != null && txt != '') {
        while (txt.indexOf(replace) > -1) {
            txt = txt.replace(replace, with_this);
        }
        return txt;
    }
    else {
        return '';
    }
}

function alertform(msg, voltainicio, respostaYesNo, functionYes, functionNo) {
    if (msg !== "") {
        $.ajax({
            type: "POST",
            url: "pages/getUsername.aspx",
            data: {},
            dataType: "text",
            success: function (data, tpresult, obj) {
                if (data.length > 0) {
                    msg = replaceAll(msg, "#", "");
                    msg = replaceAll(msg, "<br>", "/n");
                    msg = replaceAll(msg, "<p>", "");
                    msg = replaceAll(msg, "</p>", "/n");
                    $.ajax({
                        type: "POST",
                        url: "pages/alertform.aspx",
                        data: { alertmessage: msg, voltainicio: voltainicio, respostaYesNo: respostaYesNo, functionYes: functionYes, functionNo: functionNo },
                        dataType: "text",
                        success: function (data, tpresult, obj) {
                            $("#divpopup").attr("class", "divpopupclasssmaller");
                            $("#divpopup").html(data);
                            $("#divpopup").show();
                        },
                        error: function (data) {
                            console.log(data.responseText);
                        }
                    });
                }
                else {
                    location.reload();
                }
            },
            error: function (data) {
                console.log(data.responseText);
            }
        });



    }

}



function CloseDivpopup() {
    $("#divpopup").hide();
    $("#divpopup").attr("class", "divpopupclass");

}

function downloadarquivo(url) {
    try {
        window.open(url, "", "_target=_blank");
    }
    catch (err) {
        alertform(err.Message);
    }
}

function ValueIsInFilter(listOfvalues, valueToFind) {
    if (valueToFind !== '' && valueToFind !== null) {
        if (listOfvalues.toString().toUpperCase().indexOf(valueToFind.toString().toUpperCase()) > -1) {
            return 1;
        }
        else {
            return 0;
        }
    }
    else {
        return 1;
    }
}


function GetValParametro(listaparametros, nomeparametro, separador) {
    var ret = "";
    listaparametros = separador + separador + listaparametros;
    var sep1 = separador + nomeparametro + "=";
    if (listaparametros.indexOf(sep1) > -1) {
        ret = listaparametros.split(sep1)[1].split(separador)[0];
    }
    return ret;
}

function SetValParametro(listaparametros, nomeparametro, valor, separador) {
    var ret = "";
    var ret1 = "";
    var ret2 = "";
    var sep1 = separador + nomeparametro + "=";
    if (listaparametros.indexOf(sep1) > -1) {
        ret1 = listaparametros.split(sep1)[0] + sep1 + valor;
        ret2 = listaparametros.split(sep1)[1];
        if (ret2.indexOf(separador) > 0) {
            ret2 = ret2.substring(ret2.indexOf(separador));
        }
        else {
            ret2 = "";
        }
        ret = ret1 + separador + ret2 + separador;
    }
    else {
        ret = listaparametros + sep1 + valor + separador;
    }
    ret = replaceAll(ret, separador + separador + separador, separador);
    ret = replaceAll(ret, separador + separador, separador);
    return ret;
}

function GetUsuario() {
    return $("#username").val();
}

function GetUsuarioAcesso() {
    return $("#usernameacesso").val();
}

function GetIsUsuarioInterno() {
    return cnum($("#isusuariointerno").val());

}

function GetEmpresaBase() {
    return $("#empresabase").val();
}

function getNewGuid() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16)
            .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
}


window.onbeforeunload = closingCode;

function closingCode() {
    // do something...
    return null;
}

function SetDecimais(valor, ndecimais) {
    if (cnum(valor) != 0) {
        return cnum(valor).toFixed(ndecimais);
    }
    else {
        return 0.00;
    }
}

function SplitText(text, sep) {
    if (text != null && text != "") {
        return text.split(sep);
    }
    else {
        return "";
    }
}

function printpag(pagina, parametri) {
    try {
        pagina = "print/" + pagina + ".aspx";
        $.get(pagina, parametri, function (data) {
            if (data.substring(0, 1) == '#') {
                alertform(data);
            }
            else {
                window.open(data, "", "_target=_blank");
            }
        });
    }
    catch (err) {
        alertform(err.Message);
    }

}

function menosde(a, b) {
    if (a < b) {
        return true;
    }
    else {
        return false;
    }
}

function encodeBase64(tx) {
    var tx64 = btoa(tx);
    tx64 = replaceAll(tx64, "=", "XXXigualXXX");
    tx64 = replaceAll(tx64, ",", "XXXvirgulaXXX");
    tx64 = replaceAll(tx64, ";", "XXXpuntovirgulaXXX");
    return tx64;
}

function decodeBase64(tx64) {
    tx64 = replaceAll(tx64, "XXXigualXXX", "=");
    tx64 = replaceAll(tx64, "XXXvirgulaXXX", ",");
    tx64 = replaceAll(tx64, "XXXpuntovirgulaXXX", ";");
    return atob(tx64);
}

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



function excluiranexo(idanexo) {
    var p = {};
    p["idanexoexcluir"] = idanexo;
    $.get("upload.aspx", p, function (data) {
        $("#divpopup").html(data);
        $("#anexo_" + idanexo).hide();
        $("#divpopup").show();
    });
}