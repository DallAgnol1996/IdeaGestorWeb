
    module.exports = {
        data() {
            return {
                desusuario: "",
                isActive: true
            }
        },
        created: function () {
            var desusuario = $("#desusername").val();
            this.desusuario = desusuario;
        },
        methods: {
            SetBodyBranco: function () {
                $("body").attr("class", "bodybranco");
              //  $("footer").hide();
            },
            SetBodyInicial: function () {
                $("body").attr("class", "bodyinicial");
               // $("footer").show();
            },
            alterClass: function (numero) {
                var idCheck = "#" + numero
                $("li").attr("class", "inactive");
                $(idCheck).attr("class", "active");
                // $("footer").show();
            }
        }
    }
