<template>
    <div class="bootstrap-table logtable">
        <div class="table-responsive form-group">
            <table id="table" class="table table-hover">
                <thead class="thead-dark">
                    <tr>
                        <th @click.prevent.stop="ordenatabela('usuario',$event)" vp-ordertype="0"><b>Usuario</b><span class="glyphicon glyphicon-sort"></span></th>
                        <th @click.prevent.stop="ordenatabela('data',$event)" vp-ordertype="0"><b>Data</b><span class="glyphicon glyphicon-sort"></span></th>
                        <th @click.prevent.stop="ordenatabela('chavetabela',$event)" vp-ordertype="0"><b>ChaveTabela</b><span class="glyphicon glyphicon-sort"></span></th>
                        <th @click.prevent.stop="ordenatabela('acao',$event)" vp-ordertype="0"><b>Açao</b><span class="glyphicon glyphicon-sort"></span></th>
                        <th @click.prevent.stop="ordenatabela('coluna',$event)" vp-ordertype="0"><b>Coluna</b><span class="glyphicon glyphicon-sort"></span></th>
                        <th @click.prevent.stop="ordenatabela('valorantigo',$event)" vp-ordertype="0"><b>Valor Antigo</b><span class="glyphicon glyphicon-sort"></span></th>
                        <th @click.prevent.stop="ordenatabela('valornovo',$event)" vp-ordertype="0"><b>Valor Novo</b><span class="glyphicon glyphicon-sort"></span></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item in itensfiltered " :key="item.id">
                        <td scope="col">{{item.usuario}}</td>
                        <td scope="col">{{item.data}}</td>
                        <td scope="col">{{item.chavetabela}}</td>
                        <td scope="col">{{item.acao}}</td>
                        <td scope="col">{{item.coluna}}</td>
                        <td scope="col">{{item.valorantigo}}</td>
                        <td scope="col">{{item.valornovo}}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>
<script src="../js/vuecomponents/vplogcadastro.js"></script>

