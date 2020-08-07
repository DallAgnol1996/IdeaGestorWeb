
    module.exports = {
        props: {
            label: {
                default: "Label",
                type: String
            },
            labelsize: {
                default: "3",
                type: String
            },
            inputsize: {
                default: "9",
                type: String
            },
            colsize: {
                default: "sm",
                type: String
            },
            disabled: {
                default: "0",
                type: String
            },
            value: "0"
        },
        data() {
            return {
            };
        },
        methods: {
            SelecionaCheck() {
                if (this.$refs.check.checked === true) {
                    this.value = 1;
                }
                else {
                    this.value = 0;
                }
                this.$emit('input', this.value);
            }
        }
    }
