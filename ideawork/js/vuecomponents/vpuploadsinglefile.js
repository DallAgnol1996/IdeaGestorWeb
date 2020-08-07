
    module.exports = {
        props: {
            pastasalva: {
                default: "",
                required: true,
                type: String
            },
            nomefile: {
                default: "",
                required: true,
                type: String
            },
            title: {
                default: ""
            },
            exefunction: {
                default: ""
            },
            desbutton: {
                default: ""
            }
        },
        data() {
            return {
                file: '',
                value: '',
                thisnomefile: '',
                thispastasalva: ''
            };
        },
        methods: {
            handleFileUpload() {
                this.file = this.$refs.file.files[0];
                // Initialize the form data
                var formData = new FormData();
                // Add the form data we need to submit
                this.nomefile = replaceAll(this.nomefile, "/", "__");
                

                formData.append('file', this.file);
                formData.append('pastasalva', this.pastasalva);
                formData.append('nomefile', this.nomefile);
                var vm = this;
                //   Make the request to the POST /single-file URL            
                $.ajax({
                    url: 'ws/UploadService.ashx',
                    type: 'POST',
                    data: formData,
                    success: function (data) {
                        vm.$emit('input', data);
                        if (vm.exefunction !== "") {
                            vm.$emit(vm.exefunction);
                        }
                        // vm.$refs.file.files[0].name = '';
                    },
                    cache: false,
                    contentType: false,
                    processData: false,
                    xhr: function () { // Custom XMLHttpRequest
                        var myXhr = $.ajaxSettings.xhr();
                        if (myXhr.upload) { // Avalia se tem suporte a propriedade upload
                            myXhr.upload.addEventListener('progress', function () {
                                /* faz alguma coisa durante o progresso do upload */
                            }, false);
                        }
                        return myXhr;
                    }
                });
            }
        }
    }
