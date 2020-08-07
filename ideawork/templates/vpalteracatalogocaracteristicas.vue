<template>
    <!--<div class="content">
         <div class="container">
             <div class="form altheight">
                 <div class="row  form-group text-center">
                     <div class="col-sm-12 form-group text-center">
                         <h1>{{titulo}}</h1>
                     </div>
                 </div>
                 <div class="orc">
                     <div class="text-title"><label>Filtro</label></div>
                     <div class="box-orc alt">
                         <div class="row  form-group">
                             <div class="col-sm-6"><vp-inputtext :value="car.cod" @input="car.cod=$event" label="Codigo:" placeholder="Inserir Codigo" labelsize="3" inputsize="8"></vp-inputtext></div>
                             <div class="col-sm-6"><vp-inputtext :value="car.des" @input="car.des=$event" label="Descrição:" placeholder="Inserir Descrição" labelsize="3" inputsize="8"></vp-inputtext></div>
                         </div>
                         <div class="row form-group">
                             <div class="col-sm-4 ccimg"><vp-inputtext :value="car.img" @input="car.img=$event" label="Imagem:" placeholder="Nome Imagem" labelsize="5" inputsize="6" disabled="1"></vp-inputtext></div>
                             <div class="col-sm-2"><vp-uploadsinglefile :value="car.img" @input="car.img=$event" :pastasalva="'caracteristicas/' + car.cod + '/'" :nomefile="'header_car_' + car.cod"></vp-uploadsinglefile></div>
                             <div class="col-sm-6 tipcat"><vp-select :value="car.tipo" @input="car.tipo=$event" :itens="tiposcaracteristicas" labelsize="3" inputsize="8" label="Tipo Característica" disabled='1'></vp-select></div>
                         </div>
                         <div class="row form-group">
                             <div class="col-sm-6"><vp-inputtext :value="car.carref" @input="car.carref=$event" label="Car.Referencia" placeholder="Inserir Nome caracteristica de referencia" labelsize="3" inputsize="8"></vp-inputtext></div>
                             <div class="col-sm-6"><vp-select :value="car.tipopreview" @input="car.tipopreview=$event" :itens="tipopreviews" labelsize="3" inputsize="8" label="Tipo Preview"></vp-select></div>
                         </div>
                         <div class="row form-group ca">
                             <div class="col-sm-12"><vp-inputtext :value="car.campos" @input="car.campos=$event" label="Campos Adicionais:" placeholder="Inserir Campos Adicionais" labelsize="2" inputsize="10"></vp-inputtext></div>
                         </div>
                     </div>
                 </div>
                 <div class="row form-group">
                     <div class="col-sm-12">
                         <button type="button" class="btn btn-orc float-right marginleft50" @click.prevent.stop="salvar()">Salvar</button>
                         <button type="button" class="btn btn-orc float-right marginleft50 btn-danger" @click.prevent.stop="excluir()">Excluir</button>
                         <button type="button" class="btn btn-orc float-right marginleft50" @click.prevent.stop="showcategorias()">Categorias</button>
                         <button type="button" class="btn btn-orc float-right marginleft50" @click.prevent.stop="exportar()">Exportar txt</button>
                         <button type="button" class="btn btn-orc float-right marginleft50" @click.prevent.stop="mostralog()">Log</button>
                         <button type="button" class="btn btn-orc float-right marginleft50" @click.prevent.stop="showpreview()">Preview</button>
                         <vp-uploadsinglefile exefunction="importar" desbutton="Importar txt" :pastasalva="'importtxt/'" :nomefile="'import_car_' + car.cod" v-on:importar="importararquivo()"></vp-uploadsinglefile>
                     </div>
                 </div>
                 <div class="bootstrap-table">
                     <div class="table-responsive form-group">
                         <table id="table" class="table table-hover table-striped">
                             <thead class="thead-dark">
                                 <tr>
                                     <th class="tab2" @click.prevent.stop="ordenatabela('cod',$event)" vp-ordertype="0"><b>Cod</b><span class="glyphicon glyphicon-sort"></span></th>
                                     <th class="tab3" @click.prevent.stop="ordenatabela('des',$event)" vp-ordertype="0"><b>Des</b><span class="glyphicon glyphicon-sort"></span></th>
                                     <th class="tab3">Imagem</th>
                                     <th class="tab3" @click.prevent.stop="ordenatabela('cat',$event)" vp-ordertype="0"><b>Categoria</b><span class="glyphicon glyphicon-sort"></span></th>
                                     <th class="tab2" @click.prevent.stop="ordenatabela('coditem',$event)" vp-ordertype="0"><b>Cod.Item</b><span class="glyphicon glyphicon-sort"></span></th>
                                     <th class="tab1" @click.prevent.stop="ordenatabela('ord',$event)" vp-ordertype="0"><b>Ord.</b><span class="glyphicon glyphicon-sort"></span></th>
                                     <th class="tab1">C.Add</th>
                                     <th class="tab1">Salva</th>
                                     <th class="tab1">Excluir</th>
                                 </tr>
                                 <tr>
                                     <th class="tab2"><input class="form-control" v-model="filter_cod" placeholder="filtrar codigo" @keyup.enter="filtrartabela()" /></th>
                                     <th class="tab3"><input class="form-control" v-model="filter_des" placeholder="filtrar Descrição" @keyup.enter="filtrartabela()" /></th>
                                     <th></th>
                                     <th><input class="form-control" v-model="filter_cat" placeholder="filtrar categoria" @keyup.enter="filtrartabela()" /></th>
                                     <th><input class="form-control" v-model="filter_coditem" placeholder="filtrar cod.item" @keyup.enter="filtrartabela()" /></th>
                                     <th><input class="form-control tab1" v-model="filter_ord" placeholder="filtrar ordinamento" @keyup.enter="filtrartabela()" /></th>
                                     <th class="tab1"></th>
                                     <th class="tab1"></th>
                                     <th class="tab1"></th>
                                 </tr>
                             </thead>
                             <tbody>
                                 <tr v-for="item in itensfiltered" :key="item.id">
                                     <td scope="col"><input type="text" v-model="item.cod" class="form-control tab2" /></td>
                                     <td scope="col"><input type="text" v-model="item.des" class="form-control tab3" /></td>
                                     <td scope="col"><img :src="'../catalogos/' + app.empresabase + '/caracteristicas/'+item.codcar+'/'+ item.img" width="50" height="50" /></vp-uploadsinglefile></figure><vp-uploadsinglefile :value="item.img" @input="item.img=$event" :pastasalva="'caracteristicas/' + item.codcar + '/'" :nomefile="'opc_' + item.cod" :title="item.img"></td>
                                     <td scope="col"><vp-select :value="item.cat" @input="item.cat=$event" label="" :itens="categorias" labelsize="0" inputsize="12"></vp-select></td>
                                     <td scope="col"><input type="text" v-model="item.coditem" class="form-control" /></td>
                                     <td scope="col"><input type="text" v-model="item.ord" class="form-control tab1" /></td>
                                     <td scope="col"><button type="button" class="btn btn-default" @click="detalheopcao(item)"><span class="glyphicon glyphicon-plus"></span></button></td>
                                     <td scope="col"><button type="button" class="btn btn-default" @click="salvaopcao(item)"><span class="glyphicon glyphicon-floppy-save"></span></button></td>
                                     <td scope="col"><button type="button" class="btn btn-danger" @click="excluiropcao(item)"><span class="	glyphicon glyphicon-remove"></span></button></td>
                                 </tr>
                             </tbody>
                         </table>
                     </div>
                 </div>
                 <div class="form-group divpopupclassvisible" v-if="this.mostradetalhe=='1'">
                     <div class="row bootstrap-table">
                         <div class="table-responsive form-group">
                             <table id="table" class="table table-hover">
                                 <thead class="thead-dark">
                                     <tr>
                                         <th scope="col">Coluna</th>
                                         <th scope="col">Valor</th>
                                     </tr>
                                 </thead>
                                 <tbody>
                                     <tr>
                                         <td>Link Documentação:</td>
                                         <td><input type="text" v-model="itemdetalhe.linkdoc" class="form-control" /></td>
                                     </tr>
                                     <tr>
                                         <td>Opção ativa:</td>
                                         <td>
                                             <select class="form-control" v-model="itemdetalhe.ativo">
                                                 <option value="0">Não</option>
                                                 <option value="1">Sím</option>
                                             </select>
                                         </td>
                                     </tr>
                                     <tr>
                                 <thead class="thead-dark">
                                 <th colspan="2">Informações costumizadas</th>
                                 </thead>
                                 </tr>
                                 <tr v-for="col in colsitem">
                                     <td>{{col.id}} - {{col.des}}</td>
                                     <td><input type="text" v-model="col.value" class="form-control" /></td>
                                 </tr>
                                 </tbody>
                             </table>
                         </div>
                     </div>

                     <div class="row float-right">
                         <button type="button" class="btn btn-default" @click.prevent.stop="fechardetalhe()">Fechar</button>
                         <button type="button" class="btn btn-default marginleft50" @click.prevent.stop="salvardetalhe(itemdetalhe)">Salvar</button>
                     </div>
                 </div>
                 <div class="form-group divpopupclassvisible" v-if="this.mostracategorias=='1'">
                     <div class="row bootstrap-table">
                         <div class="table-responsive form-group">
                             <table id="table" class="table table-hover">
                                 <thead class="thead-dark">
                                     <tr>
                                         <th>Codigo</th>
                                         <th>Descrição</th>
                                         <th>Imagem</th>
                                         <th>Salva</th>
                                         <th>Exclui</th>
                                     </tr>
                                 </thead>
                                 <tbody>
                                     <tr v-for="categoria in categorias">
                                         <td><input type="text" v-model="categoria.cod" class="form-control" /></td>
                                         <td><input type="text" v-model="categoria.des" class="form-control" /></td>
                                         <td><vp-uploadsinglefile :value="categoria.img" @input="categoria.img=$event" :pastasalva="'caracteristicas/' + car.cod + '/'" :nomefile="'cat_' + categoria.cod" :title="categoria.img"></vp-uploadsinglefile></td>
                                         <td><button type="button" class="btn btn-default" @click="salvacategoria(categoria)"><span class="glyphicon glyphicon-floppy-save"></span></button></td>
                                         <td><button type="button" class="btn btn-danger" @click="excluircategoria(categoria)"><span class="	glyphicon glyphicon-remove"></span></button></td>
                                     </tr>
                                 </tbody>
                             </table>
                         </div>
                     </div>
                     <div class="row float-right">
                         <button type="button" class="btn btn-default" @click.prevent.stop="fecharcategorias()">Fechar</button>
                     </div>
                 </div>
                 <div class="form-group divpopupclassvisible" v-if="this.mostralogtabela=='1'">
                     <div class="row float-right">
                         <button type="button" class="btn btn-default" @click.prevent.stop="fecharlogtabela()">Fechar</button>
                     </div>
                     <vp-logcadastro nometabela="carcab,caropc" :chavetabela="this.car.cod"></vp-logcadastro>
                 </div>
                 <div class="form-group divpopupclassvisible" v-if="this.mostrapreview=='1'">
                     <div class="row justify-content-center">
                         <button type="button" class="btn btn-default" @click.prevent.stop="fecharpreview()">Fechar Preview caracteristica</button>
                     </div>
                     <br>
                     <br>
                     <div class="row">
                         <vp-carpreview :codcar="this.car.cod"></vp-carpreview>
                     </div>
                 </div>
             </div>
         </div>
         </div>-->

    <div class="content">
        <div class="card-content">
            <div class="card">
                <h4 class="card-header" style="text-align: center;">{{titulo}}</h4>
                <div class="card-body">
                    <div class="form-row">
                        <div class="form-group col-sm-4">
                            <vp-inputtext :value="car.cod" @input="car.cod=$event" label="Codigo:" placeholder="Codigo" labelsize="12" inputsize="12"></vp-inputtext>
                        </div>
                        <div class="form-group col-sm-4">
                            <vp-inputtext :value="car.des" @input="car.des=$event" label="Descrição:" placeholder="Descrição" labelsize="12" inputsize="12"></vp-inputtext>
                        </div>
                        <div class="form-group col-sm-4">
                            <vp-select :value="car.tipo" @input="car.tipo=$event" :itens="tiposcaracteristicas" labelsize="12" inputsize="12" label="Tipo Característica" disabled='1'></vp-select>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-sm-4">
                            <vp-select :value="car.tipopreview" @input="car.tipopreview=$event" :itens="tipopreviews" labelsize="12" inputsize="12" label="Tipo Preview"></vp-select>
                        </div>
                        <div class="form-group col-sm-4">
                            <vp-inputtext :value="car.carref" @input="car.carref=$event" label="Car.Referencia" placeholder="Nome caracteristica de referencia" labelsize="12" inputsize="12"></vp-inputtext>
                        </div>
                        <div class="form-group col-sm-4">
                            <vp-inputtext :value="car.campos" @input="car.campos=$event" label="Campos Adicionais:" placeholder="Campos Adicionais" labelsize="12" inputsize="12"></vp-inputtext>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-sm-4">
                            <vp-inputtext :value="car.img" @input="car.img=$event" label="Imagem:" placeholder="Nome Imagem" labelsize="12" inputsize="12" disabled="1"></vp-inputtext>
                        </div>
                        <div class="form-group col-sm-8">
                            <div class="upload-form">
                                <vp-uploadsinglefile :value="car.img" @input="car.img=$event" :pastasalva="'caracteristicas/' + car.cod + '/'" :nomefile="'header_car_' + car.cod" labelsize="12" inputsize="12" label="Upload arquivo:"></vp-uploadsinglefile>
                            </div>
                        </div>
                    </div>

                    <vp-message :tpalert='tpalert' :operacao="titulo +' '+ operacao" v-if="saveOk"></vp-message>
                    <hr />
                    <div class="form-row">
                        <hr />

                        <div class="col-sm-6">
                            <div class="btn-export">
                                <vp-uploadsinglefile exefunction="importar" desbutton="Importar txt" :pastasalva="'importtxt/'" :nomefile="'import_car_' + car.cod" v-on:importar="importararquivo()"></vp-uploadsinglefile>
                            </div>
                        </div>
                        <div class="col-sm">
                            <div class="grupo-btn">
                                <button type="button" class="btn btn-default btn-sm" @click.prevent.stop="exportar()"><i class="fa fa-download"></i>&nbsp;Exportar txt</button>
                                <button type="button" class="btn btn-default btn-sm" @click.prevent.stop="showpreview()"><i class="fa fa-file-image-o"></i>&nbsp;Preview</button>
                                <button type="button" class="btn btn-default btn-sm" @click.prevent.stop="mostralog()"><i class="fa fa-list"></i>&nbsp;Log</button>
                                <button type="button" class="btn btn-default btn-sm" @click.prevent.stop="showcategorias()"><i class="fa fa-tags"></i>&nbsp;Categorias</button>
                                <button type="button" class="btn btn-defaultd btn-sm" @click.prevent.stop="excluir()"><i class="fa fa-trash"></i>&nbsp;Excluir</button>
                                <button type="button" class="btn btn-defaults btn-sm" @click.prevent.stop="salvar()"><i class="fa fa-save"></i>&nbsp;Salvar</button>
                            </div>
                        </div>
                    </div>

                    <hr />

                    <div class="table-responsive">
                        <table class="table table-bordered table-striped">
                            <thead>
                                <tr>
                                    <th scope="col" @click.prevent.stop="ordenatabela('cod',$event)" vp-ordertype="0"><b>Cod </b><i class="fa fa-sort"></i></th>
                                    <th scope="col" @click.prevent.stop="ordenatabela('des',$event)" vp-ordertype="0"><b>Des </b><i class="fa fa-sort"></i></th>
                                    <th scope="col">Imagem</th>
                                    <th scope="col" @click.prevent.stop="ordenatabela('cat',$event)" vp-ordertype="0"><b>Categoria </b><i class="fa fa-sort"></i></th>
                                    <th scope="col" @click.prevent.stop="ordenatabela('coditem',$event)" vp-ordertype="0"><b>Cod.Item </b><i class="fa fa-sort"></i></th>
                                    <th scope="col" @click.prevent.stop="ordenatabela('ord',$event)" vp-ordertype="0"><b>Ord. </b><i class="fa fa-sort"></i></th>
                                    <th scope="col">Ações</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <th><input v-model="filter_cod" placeholder="Codigo" @keyup.enter="filtrartabela()" /></th>
                                    <th><input v-model="filter_des" placeholder="Descrição" @keyup.enter="filtrartabela()" /></th>
                                    <th></th>
                                    <th><input v-model="filter_cat" placeholder="Categoria" @keyup.enter="filtrartabela()" /></th>
                                    <th><input v-model="filter_coditem" placeholder="Cod.item" @keyup.enter="filtrartabela()" /></th>
                                    <th><input v-model="filter_ord" placeholder="Ordem" @keyup.enter="filtrartabela()" /></th>
                                    <th></th>
                                </tr>
                                <tr v-for="item in itensfiltered" :key="item.id">
                                    <td scope="col"><input type="text" v-model="item.cod" /></td>
                                    <td scope="col"><input type="text" v-model="item.des" /></td>
                                    <td scope="col">
                                        <!--<img :src="'../catalogos/' + app.empresabase + '/caracteristicas/'+item.codcar+'/'+ item.img" width="50" height="50" /></vp-uploadsinglefile></figure><vp-uploadsinglefile :value="item.img" @input="item.img=$event" :pastasalva="'caracteristicas/' + item.codcar + '/'" :nomefile="'opc_' + item.cod" :title="item.img">-->
                                    </td>
                                    <td scope="col"><vp-selectsmall :value="item.cat" @input="item.cat=$event" label="" :itens="categorias" labelsize="0" inputsize="12"></vp-selectsmall></td>
                                    <td scope="col"><input type="text" v-model="item.coditem" /></td>
                                    <td scope="col"><input type="text" v-model="item.ord" /></td>
                                    <td scope="col">
                                        <div class="btn-group btn-sm" role="group" aria-label="Basic example">
                                            <button type="button" class="btn btn-default btn-sm" @click="detalheopcao(item)"><i class="fa fa-plus-circle" title="Adicionar"></i></button>
                                            <button type="button" class="btn btn-defaults btn-sm" @click="salvaopcao(item)"><i class="fa fa-save" title="Salvar"></i></button>
                                            <button type="button" class="btn btn-defaultd btn-sm" @click="excluiropcao(item)"><i class="fa fa-trash" title="Excluir"></i></button>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>

                <div class="card-footer text-muted">
                    <div class="col-sm-6">
                        <div class="btnreturn">
                            <button type="button" class="btn btn-default btn-sm" @click.prevent.stop="voltar()"><i class="fa fa-arrow-circle-left"></i>&nbsp;Voltar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--Modal Detalhes-->
        <div class='modalfadeIn' v-if="this.mostradetalhe=='1'" @close="showModal = false">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" @click.prevent.stop="fechardetalhe()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row bootstrap-table">
                            <div class="table-responsive form-group">
                                <table id="table" class="table table-hover">
                                    <thead class="thead-dark">
                                        <tr>
                                            <th scope="col">Coluna</th>
                                            <th scope="col">Valor</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Link Documentação:</td>
                                            <td><input type="text" v-model="itemdetalhe.linkdoc" class="form-control" /></td>
                                        </tr>
                                        <tr>
                                            <td>Opção ativa:</td>
                                            <td>
                                                <select class="form-control" v-model="itemdetalhe.ativo">
                                                    <option value="0">Não</option>
                                                    <option value="1">Sím</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                    <thead class="thead-dark">
                                    <th colspan="2">Informações costumizadas</th>
                                    </thead>
                                    </tr>
                                    <tr v-for="col in colsitem">
                                        <td>{{col.id}} - {{col.des}}</td>
                                        <td><input type="text" v-model="col.value" class="form-control" /></td>
                                    </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" @click.prevent.stop="fechardetalhe()">Fechar</button>
                        <button type="button" class="btn btn-defaults" @click.prevent.stop="salvardetalhe(itemdetalhe)">Salvar</button>
                    </div>


                </div>
            </div>
        </div>

        <!--Modal log-->
        <div class='modalfadeIn' v-if="this.mostralogtabela=='1'" @close="showModal = false">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalLogLabel">{{titulo}}</h5>
                        <button type="button" class="close" @click.prevent.stop="fecharlogtabela()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <vp-logcadastro nometabela="carcab,caropc" :chavetabela="this.car.cod"></vp-logcadastro>
                    </div>
                </div>
            </div>
        </div>

        <!--Modal preview-->
        <div class='modalfadeIn' v-if="this.mostrapreview=='1'" @close="showModal = false">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" @click.prevent.stop="fecharpreview()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <vp-carpreview :codcar="this.car.cod"></vp-carpreview>
                    </div>
                </div>
            </div>
        </div>

        <!--Modal categorias-->
        <div class='modalfadeIn' v-if="this.mostracategorias=='1'" @close="showModal = false">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" @click.prevent.stop="fecharcategorias()">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row bootstrap-table">
                            <div class="table-responsive form-group">
                                <table id="table" class="table table-hover">
                                    <thead class="thead-dark">
                                        <tr>
                                            <th>Codigo</th>
                                            <th>Descrição</th>
                                            <th>Imagem</th>
                                            <th>Salva</th>
                                            <th>Exclui</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="categoria in categorias">
                                            <td><input type="text" v-model="categoria.cod" class="form-control" /></td>
                                            <td><input type="text" v-model="categoria.des" class="form-control" /></td>
                                            <td><vp-uploadsinglefile :value="categoria.img" @input="categoria.img=$event" :pastasalva="'caracteristicas/' + car.cod + '/'" :nomefile="'cat_' + categoria.cod" :title="categoria.img"></vp-uploadsinglefile></td>
                                            <td><button type="button" class="btn btn-default" @click="salvacategoria(categoria)"><span class="glyphicon glyphicon-floppy-save"></span></button></td>
                                            <td><button type="button" class="btn btn-danger" @click="excluircategoria(categoria)"><span class="	glyphicon glyphicon-remove"></span></button></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>



    </div>
</template>
<script src="../js/vuecomponents/vpalteracatalogocaracteristicas.js"></script>