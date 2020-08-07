<template>
    <div class="content">
        <div class="card-content">
            <div class="card">
                <h4 class="card-header" style="text-align: center;">{{titulo}}</h4>
                <div class="card-body">
                    <div class="row form-group">
                        <div class="col-sm-12">
                            <button type="button" class="btn btn-default btn-sm"><router-link :to="{name:'alterajob',params:{cod:''}}"><a class="dropdown-item">Cadastrar Job</a></router-link></button>
                        </div>
                    </div>

                    <div class="table-responsive-sm">
                        <table class="table table-hover">
                            <thead>
                                <tr class="table-active">
                                    <th scope="col" @click.prevent.stop="ordenatabela('cod',$event)" vp-ordertype="0"><b>Cod</b><i class="fa fa-sort" aria-hidden="true" style="padding-left:5px;"></i></a></th>
                                    <th scope="col" @click.prevent.stop="ordenatabela('desCliente',$event)" vp-ordertype="0"><b>Cliente</b><i class="fa fa-sort" aria-hidden="true" style="padding-left:5px;"></i></th>
                                    <th scope="col" @click.prevent.stop="ordenatabela('desTecnico',$event)" vp-ordertype="0"><b>Técnico</b><i class="fa fa-sort" aria-hidden="true" style="padding-left:5px;"></i></th>
                                    <th scope="col" @click.prevent.stop="ordenatabela('dataHoraCriacao',$event)" vp-ordertype="0"><b>Data</b><i class="fa fa-sort" aria-hidden="true" style="padding-left:5px;"></i></th>
                                    <th scope="col" @click.prevent.stop="ordenatabela('dataJob',$event)" vp-ordertype="0"><b>Dt Inst</b><i class="fa fa-sort" aria-hidden="true" style="padding-left:5px;"></i></th>
                                    <th scope="col" @click.prevent.stop="ordenatabela('horaJob',$event)" vp-ordertype="0"><b>Hr</b><i class="fa fa-sort" aria-hidden="true" style="padding-left:5px;"></i></th>
                                    <th scope="col" @click.prevent.stop="ordenatabela('des',$event)" vp-ordertype="0"><b>Descrição</b><i class="fa fa-sort" aria-hidden="true" style="padding-left:5px;"></i></th>
                                    <th scope="col" @click.prevent.stop="ordenatabela('status',$event)" vp-ordertype="0"><b>Status</b><i class="fa fa-sort" aria-hidden="true" style="padding-left:5px;"></i></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr class="trfiltros">
                                    <th><input class="form-control form-control-sm" v-model="filter_cod" placeholder="Código" @keyup.enter="filtrartabela()" size="2"/></th>
                                    <th><input class="form-control form-control-sm" v-model="filter_desCliente" placeholder="Cliente" @keyup.enter="filtrartabela()" size="20"/></th>
                                    <th><input class="form-control form-control-sm" v-model="filter_desTecnico" placeholder="Técnico" @keyup.enter="filtrartabela()" size="20"/></th>
                                    <th><input class="form-control form-control-sm" v-model="filter_dataHoraCriacao" placeholder="Data" @keyup.enter="filtrartabela()" size="8"/></th>
                                    <th><input class="form-control form-control-sm" v-model="filter_dataJob" placeholder="Dt Inst." @keyup.enter="filtrartabela()" size="8"/></th>
                                    <th><input class="form-control form-control-sm" v-model="filter_horaJob" placeholder="Hr Inst." @keyup.enter="filtrartabela()" size="4"/></th>
                                    <th><input class="form-control form-control-sm" v-model="filter_des" placeholder="Descrição" @keyup.enter="filtrartabela()" size="100"/></th>
                                    <th><input class="form-control form-control-sm" v-model="filter_status" placeholder="Status" @keyup.enter="filtrartabela()" size="4" /></th>
                                </tr>
                                <tr v-for="item in itensfiltered " :key="item.cod" @click="GoToJob(item)">
                                    <th scope="row">{{item.cod}}</th>
                                    <td>{{item.desCliente}}</td>
                                    <td>{{item.desUsuario}}</td>
                                    <td>{{item.dataHoraCriacao}}</td>
                                    <td>{{item.dataJob}}</td>
                                    <td>{{item.horaJob}}</td>
                                    <td>{{item.des}}</td>
                                    <td>
                                        <span class="badge badge-danger" v-if="item.idstatus == '0'">{{item.status}}</span>
                                        <span class="badge badge-warning" v-if="item.idstatus == '1'">{{item.status}}</span>
                                        <span class="badge badge-success" v-if="item.idstatus == '2'">{{item.status}}</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

</template>

<script src="../js/vuecomponents/vplistajobs.js"></script>