<template>
    <div class="content">
        <div class="card-content">
            <div class="card">
                <h4 class="card-header" style="text-align: center;">{{titulo}}</h4>
                <div class="card-body">

                    <div class="form-row">
                        <div class="form-group col-sm-6">
                            <vp-inputtext :value="cliente.cod" @input="cliente.cod=$event" label="Codigo:" placeholder="Código" labelsize="9" inputsize="9" ></vp-inputtext>
                        </div>
                        <div class="form-group col-sm-6">
                            <vp-inputtext :value="cliente.des" @input="cliente.des=$event" label="Descrição:" placeholder="Descrição" labelsize="9" inputsize="9" ></vp-inputtext>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-sm-6">
                            <vp-select :value="cliente.idref" @input="cliente.idref=$event" label="Referente:" labelsize="9" inputsize="9" :itens="listaref"></vp-select>
                        </div>
                        <div class="form-group col-sm-6">
                            <vp-inputtext :value="cliente.email" @input="cliente.email=$event" label="Email:" placeholder="Email" labelsize="9" inputsize="9" ></vp-inputtext>
                        </div>
                    </div>

                    <vp-message :tpalert='tpalert' :operacao="titulo +' '+ operacao" v-if="saveOk"></vp-message>

                </div>
                <div class="card-footer text-muted">
                    <div class="col-sm">
                        <div class="grupo-btn">
                            <button type="button" class="btn btn-default btn-sm" @click.prevent.stop="mostralog()"><i class="fa fa-list"></i>&nbsp;Log</button>
                            <button type="button" class="btn btn-defaultd btn-sm" @click.prevent.stop="excluir()"><i class="fa fa-trash"></i>&nbsp;Excluir</button>
                            <button type="button" class="btn btn-defaults btn-sm" @click.prevent.stop="salvar()"><i class="fa fa-save"></i>&nbsp;Salvar</button>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="btnreturn">
                            <button type="button" class="btn btn-default btn-sm" @click.prevent.stop="voltar()"><i class="fa fa-arrow-circle-left"></i>&nbsp;Voltar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class='modalfadeIn' v-if="showModal" @close="showModal = false">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalLogLabel">{{titulo}}</h5>
                        <button type="button" class="close" @click="showModal = false">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <h6>Acessos</h6>
                        </div>
                        <div class="row">
                            <vp-logeventos nomeevento="acesso" :chaveevento="this.cliente.cod"></vp-logeventos>
                        </div>
                        <div class="row">
                            <h6>Alterações</h6>
                        </div>
                        <div class="row">
                            <vp-logcadastro nometabela="clientes" :chavetabela="this.cliente.cod"></vp-logcadastro>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script src="../js/vuecomponents/vpalteracliente.js"></script>