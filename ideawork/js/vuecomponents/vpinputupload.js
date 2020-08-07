
    module.exports = {
        props: {
            label: {
                default: "Anexar Arquivos",
                type: String
            },
            tipo: {
                default: "0",
                type: String
            },
            ref1: {
                default: "",
                type: String
            },
            ref2: {
                default: "",
                type: String
            },
            ref3: {
                default: "",
                type: String
            },
            ref4: {
                default: "",
                type: String
            },
            pastasave: {
                default: "",
                type: String
            },
            useuploadicon: {
                default: "1",
                type: String
            }
        },
        data() {
            return {

            };
        },
        methods: {
            inseriranexos() {
                var p = {};
                p["idanexoexcluir"] = 0;
                p["tipo"] = this.tipo;
                p["ref1"] = this.ref1;
                p["ref2"] = this.ref2;
                p["ref3"] = this.ref3;
                p["ref4"] = this.ref4;
                p["pastasave"] = this.pastasave;
                $.get("pages/upload.aspx", p, function (data) {
                    $("#divpopup").html(data);
                    $("#divpopup").attr("class", "divpopupclass");
                    $("#divpopup").show();
                });
            }
        }
    }
