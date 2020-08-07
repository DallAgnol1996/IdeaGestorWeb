<template>
    <div class="row altheight">
        <div class="col-sm-12  form-group text-center">
            <h1>{{titulo}}</h1>
        </div>
        <div class="table-responsive">
            <table id="table" class="table table-hover">
                <thead class="thead-dark">
                    <tr>
                        <th @click.prevent.stop="ordenatabela('usuario',$event)" class="tab3 borda-esquerda" vp-ordertype="0"><b>Usuário</b></a></th>
                        <th @click.prevent.stop="ordenatabela('des',$event)" class="tab4" vp-ordertype="0"><b>Descrição</b></th>
                        <th @click.prevent.stop="ordenatabela('valor',$event)" class="tab2" vp-ordertype="0"><b>Valor</b></th>
                        <th @click.prevent.stop="ordenatabela('tipo',$event)" class="tab2" vp-ordertype="0"><b>Tipo</b></a></th>
                        <th @click.prevent.stop="ordenatabela('coeficiente',$event)" class="tab2" vp-ordertype="0"><b>Coeficiente</b></th>
                        <th @click.prevent.stop="ordenatabela('grupo',$event)" class="tab2 borda-direita" vp-ordertype="0"><b>Grupo</b></th>
                    </tr>
                    <tr class="trfiltros">
                        <th><input class="form-control" v-model="filter_usuario" placeholder="Filtrar Usuário" @keyup.enter="filtrartabela()" /></th>
                        <th><input class="form-control" v-model="filter_des" placeholder="Filtrar Descrição" @keyup.enter="filtrartabela()" /></th>
                        <th><input class="form-control" v-model="filter_valor" placeholder="Filtrar Valor" @keyup.enter="filtrartabela()" /></th>
                        <th><input class="form-control" v-model="filter_tipo" placeholder="Filtrar Tipo" @keyup.enter="filtrartabela()" /></th>
                        <th><input class="form-control" v-model="filter_coeficiente" placeholder="Filtrar Coeficiente" @keyup.enter="filtrartabela()" /></th>
                        <th><input class="form-control" v-model="filter_grupo" placeholder="Filtrar Grupo" @keyup.enter="filtrartabela()" /></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="markup in itensfiltered ">
                        <td scope="col"><b>{{markup.usuario}}</b></td>
                        <td scope="col"><b>{{markup.des}}</b></td>
                        <td scope="col" class="numtab">{{markup.valor}}</td>
                        <td scope="col" class="numtab">{{markup.tipo}}</td>
                        <td scope="col" class="numtab">{{markup.coeficiente}}</td>
                        <td scope="col" class="numtab">{{markup.grúpo}}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>


</template>

<script src="../js/vuecomponents/vploginsmarkup.js"></script>
