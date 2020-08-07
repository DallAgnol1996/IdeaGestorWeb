function excluiranexo(idanexo) {
    var p = {};
    p["idanexoexcluir"] = idanexo;
    $.get("upload.aspx", p, function (data) {
        $("#divpopup").html(data);
        $("#anexo_" + idanexo).hide();
        $("#divpopup").show();
    });
}