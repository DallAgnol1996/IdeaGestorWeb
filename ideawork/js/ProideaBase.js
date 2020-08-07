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
