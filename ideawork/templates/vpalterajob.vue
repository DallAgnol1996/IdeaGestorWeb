<template>
    <div class="content">
        <div class="card-content">
            <div class="card">
                <h4 class="card-header" style="text-align: center;">{{titulo}}</h4>
                <div class="card-body">
                    <div class="form-row">
                        <div class="form-group col-sm-4">
                            <vp-inputtext :value="job.cod" @input="job.cod=$event" label="Codigo:" placeholder="Código"  inputsize="12" labelsize="12" :disabled="1"></vp-inputtext>
                        </div>
                        <div class="form-group col-sm-4">
                            <vp-select :value="job.idCliente" @input="job.idCliente=$event" label="Cliente:" inputsize="12" labelsize="12" :itens="listaCliente"></vp-select>
                        </div>
                        <div class="form-group col-sm-4">
                            <vp-select :value="job.idUsuario" @input="job.idUsuario=$event" label="Técnico:" inputsize="12" labelsize="12" :itens="listaTecnico"></vp-select>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-sm-4">
                            <vp-inputtext :value="job.usuarioCriacao" @input="job.usuarioCriacao=$event" label="Usuário Criação:" placeholder="Usuário Criação" inputsize="12" labelsize="12" :disabled="1"></vp-inputtext>
                        </div>
                        <div class="form-group col-sm-4">
                            <vp-inputtext :value="job.dataHoraCriacao" @input="job.dataHoraCriacao=$event" label="Data/Hora Criação:" placeholder="Data/Hora Criação" inputsize="12" labelsize="12" :disabled="1"></vp-inputtext>
                        </div>
                        <div class="form-group col-sm-2">
                            <vp-inputtext :value="job.dataJob" @input="job.dataJob=$event" label="Data Job:" placeholder="Data Job" inputsize="12" labelsize="12" ></vp-inputtext>
                        </div>
                        <div class="form-group col-sm-2">
                            <vp-inputtext :value="job.horaJob" @input="job.horaJob=$event" label="Hora Job:" placeholder="Hora Job" inputsize="12" labelsize="12" ></vp-inputtext>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-sm-6">
                            <vp-select :value="job.idstatus" @input="job.idstatus=$event" label="Status:" inputsize="12" labelsize="12" :itens="listastatus"></vp-select>
                        </div>
                        <div class="form-group col-sm-6">
                            <vp-select :value="job.codCheckList" @input="job.codCheckList=$event" label="Check-list:" labelsize="12" inputsize="12" :itens="listachecklist"></vp-select>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-sm-12">
                            <vp-inputtextarea :value="job.des" @input="job.des=$event" label="Descrição:" placeholder="Descrição" inputsize="12" labelsize="8"></vp-inputtextarea>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-sm-12">
                            <vp-inputtextarea :value="job.obs" @input="job.obs=$event" label="Observação:" placeholder="Observação" inputsize="12" labelsize="9"></vp-inputtextarea>
                        </div>
                    </div>



                    <vp-message :operacao="titulo +' '+ operacao" v-if="saveOk"></vp-message>

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
                            <vp-logeventos nomeevento="acesso" :chaveevento="this.usuario.cod"></vp-logeventos>
                        </div>
                        <div class="row">
                            <h6>Alterações</h6>
                        </div>
                        <div class="row">
                            <vp-logcadastro nometabela="usuarios" :chavetabela="this.usuario.cod"></vp-logcadastro>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script src="../js/vuecomponents/vpalterajob.js"></script>
