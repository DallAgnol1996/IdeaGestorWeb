<template>
    <div class="content">
        <div class="card-content">
            <div class="card">
                <h4 class="card-header" style="text-align: center;">{{titulo}}</h4>
                <div class="card-body">
                    <div class="row form-group">
                        <div class="col-sm-12">
                            <button type="button" class="btn btn-default btn-sm"><router-link :to="{name:'alterachecklist',params:{cod:''}}"><a class="dropdown-item">Adicionar Check-list</a></router-link></button>
                        </div>
                    </div>

                    <div class="table-responsive-sm">
                        <table class="table table-hover">
                            <thead>
                                <tr class="table-active">
                                    <th scope="col" @click.prevent.stop="ordenatabela('cod',$event)" vp-ordertype="0">Código<i class="fa fa-sort" aria-hidden="true" style="padding-left:5px;"></i></th>
                                    <th scope="col" @click.prevent.stop="ordenatabela('des',$event)" vp-ordertype="0">Descrição<i class="fa fa-sort" aria-hidden="true" style="padding-left:5px;"></i></i></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr class="trfiltros">
                                    <th><input class="form-control form-control-sm" v-model="filter_cod" placeholder="Filtrar Código" @keyup.enter="filtrartabela()" /></th>
                                    <th><input class="form-control form-control-sm" v-model="filter_des" placeholder="Filtrar Descrição" @keyup.enter="filtrartabela()" /></th>
                                </tr>
                                <tr v-for="item in itensfiltered " :key="item.cod" @click="GoToCheckList(item)">
                                    <th scope="row">{{item.cod}}</th>
                                    <td>{{item.des}}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script src="../js/vuecomponents/vplistachecklist.js"></script>