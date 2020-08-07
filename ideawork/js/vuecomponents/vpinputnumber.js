
    module.exports = {
        props: {
            label: {
                default: "Label",
                type: String
            },
            placeholder: {
                default: "placeholder",
                type: String
            },
            labelsize: {
                default: "3",
                type: String
            },
            inputsize: {
                default: "3",
                type: String
            },
            infosize: {
                default: "6",
                type: String
            },
            min: {
                default: "0",
                type: String
            },
            max: {
                default: "99999",
                type: String
            },
            colsize: {
                default: "sm",
                type: String
            },
            value: ""
        },
        data() {
            return {
                minmax: "",
                testenumero: "",
                classalert: ""
            }
        },
        created: function () {
            this.minmax = "";
            if (cnum(this.max) !== 99999) {
                this.minmax = "Min." + this.min + " Max." + this.max;
            }
        },
        methods: {
            checknumero: function () {
                if (this.$refs.number.value !== "" && (cnum(this.$refs.number.value) < cnum(this.min) || cnum(this.$refs.number.value) > cnum(this.max))) {
                    alertform("Numero Errado! Verificar");
                    this.classalert = "is-invalid";
                }
                else {
                    this.classalert = "";
                }
            }
        },
        mounted: function () {
            this.checknumero();
        }
    }
