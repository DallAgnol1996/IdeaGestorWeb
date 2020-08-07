<template>
    <div class="content">
        <div class="card-content">
            <div class="card">
                <h4 class="card-header" style="text-align: center;">{{titulo}}</h4>
                <div class="card-body">

                    <div class="form-row">
                        <div class="form-group col-sm-6">
                            <vp-inputtext :value="c.cab.cod" disabled="1" @input="c.cab.cod=$event" label="Codigo:" placeholder="Código" inputsize="12" labelsize="12" :disabled="1"></vp-inputtext>
                        </div>
                        <div class="form-group col-sm-6">
                            <vp-select :value="c.cab.ativo" @input="c.cab.ativo=$event" label="Ativo:" inputsize="12" labelsize="12" :itens="listaativo"></vp-select>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-sm-12">
                            <vp-inputtext :value="c.cab.des" @input="c.cab.des=$event" label="Descrição:" placeholder="Descrição" inputsize="12" labelsize="12"></vp-inputtext>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-sm-12">
                            <vp-inputtext :value="c.cab.obs" @input="c.cab.obs=$event" label="Observação:" placeholder="Observação" inputsize="12" labelsize="12"></vp-inputtext>
                        </div>
                    </div>

                    <button type="button" class="btn btn-defaults btn-sm" @click.prevent.stop="addGrupo()"><i class="fa fa-plus-circle"></i>&nbsp;Adicionar Grupo</button>

                    <div class="grupo-btn">
                        <button type="button" class="btn btn-default btn-sm" @click.prevent.stop="mostrarGrupo()"><i class="fa fa-chevron-circle-down"></i>&nbsp;Abrir/Fechar Todos</button>
                    </div>

                    <div id="accordion">
                        <div class="card" v-for="g in c.grupos">
                            <div class="card-header groupcheck" id="headingOne">
                                <h5 class="mb-0">
                                    <button class="btn btn-link" @click.prevent.stop="abreAccordion(g.id)">
                                        Ordem: {{g.ord}} - Grupo: {{g.des}}  <i class="fa fa-pencil-square-o" style="padding-left:15px;" @click.prevent.stop="editarGrupo(g.id)"></i>
                                    </button>
                                </h5>
                                <div class="row groupedit" v-if="grupoedit == g.id">
                                    <label>Ordem: </label>
                                    <input type="text" v-model="g.ord" />
                                    <label>Descrição: </label>
                                    <input type="text" v-model="g.des" />
                                    <button class="btn btn-link"><i class="fa fa-check-circle" @click.prevent.stop="editarGrupo(g)"></i></button>
                                </div>
                            </div>

                            <div v-ifxxxx="idAccordion == g.id || showAccordion == true" class="collapse show" aria-labelledby="headingOne" data-parent="#accordion">
                                <div class="card-body">
                                    <div class="table-responsive listinput">
                                        <table class="table table-bordered">
                                            <thead>
                                                <tr>
                                                    <th>Ordem</th>
                                                    <th>Descrição</th>
                                                    <th>Referência</th>
                                                    <th>Foto Obrigatória</th>
                                                    <th>Ações</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="i in g.itens">
                                                    <td scope="col" class="align-middle">
                                                        <input class="inputline" type="text" v-model="i.ord" size="2" />
                                                    </td>
                                                    <td scope="col" class="align-middle"><input class="inputline" type="text" v-model="i.des" size="80" /></td>
                                                    <td scope="col" class="align-middle"><input class="inputline" type="text" v-model="i.referencia" size="40" /></td>
                                                    <td scope="col" class="align-middle" style="padding-left:20px;">
                                                        <div class="form-check form-check-inline">
                                                            <input class="form-check-input" type="radio" id="sim" value="1" v-model="i.fotoobrigatoria">
                                                            <label class="form-check-label" for="sim">Sim</label>
                                                        </div>
                                                        <div class="form-check form-check-inline">
                                                            <input class="form-check-input" type="radio" id="nao" value="0" v-model="i.fotoobrigatoria">
                                                            <label class="form-check-label" for="nao">Não</label>
                                                        </div>
                                                    </td>
                                                    <td scope="col" class="align-middle">
                                                        <div class="btn-group btn-sm" role="group" aria-label="Basic example">
                                                            <button type="button" class="btn btn-defaultd btn-sm actlist" @click="excluiropcao(g,i)"><i class="fa fa-trash" title="Excluir"></i></button>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr><td><button type="button" class="btn btn-defaults btn-sm" @click.prevent.stop="addItem(g)"><i class="fa fa-plus-circle"></i>&nbsp;Adicionar Item</button></td></tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <vp-message :operacao="titulo +' '+ operacao" v-if="saveOk"></vp-message>

                </div>
            <div class="card-footer text-muted">
                <div class="col-sm">
                    <div class="grupo-btn">
                        <button type="button" class="btn btn-default btn-sm" @click.prevent.stop="mostralog()"><i class="fa fa-list"></i>&nbsp;Log</button>
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
                        <h6>Alterações</h6>
                    </div>
                    <div class="row">
                        <vp-logcadastro nometabela="cabchecklist" :chavetabela="this.c.cab.cod"></vp-logcadastro>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class='modalfadeIn' v-if="showNovoGrupo" @close="showNovoGrupo = false">
        <div class="modal-dialog modal-md" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="modalLogLabel">Adicionar Grupo</h5>
                    <button type="button" class="close" @click="showNovoGrupo = false">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <vp-inputtext :value="novogrupo.ord" @input="novogrupo.ord=$event" label="Ordem:" placeholder="Ordem"></vp-inputtext>
                    </div>
                    <div class="form-group">
                        <vp-inputtext :value="novogrupo.des" @input="novogrupo.des=$event" label="Descrição:" placeholder="Descrição"></vp-inputtext>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-defaults btn-sm" @click.prevent.stop="salvarGrupo()"><i class="fa fa-save"></i>&nbsp;Salvar</button>
                </div>
            </div>
        </div>
    </div>
    </div>
</template>
<script src="../js/vuecomponents/vpalterachecklist.js"></script>
