<template>
    <div class="bootstrap-table logtable">
        <div class="table-responsive form-group">
            <table id="table" class="table table-hover">
                <thead class="thead-dark">
                    <tr>
                        <th @click.prevent.stop="ordenatabela('usuario',$event)" vp-ordertype="0"><b>Usuario</b><span class="glyphicon glyphicon-sort"></span></th>
                        <th @click.prevent.stop="ordenatabela('data',$event)" vp-ordertype="0"><b>Data</b><span class="glyphicon glyphicon-sort"></span></th>
                        <th @click.prevent.stop="ordenatabela('nomeevento',$event)" vp-ordertype="0"><b>Evento</b><span class="glyphicon glyphicon-sort"></span></th>
                        <th @click.prevent.stop="ordenatabela('chaveevento',$event)" vp-ordertype="0"><b>Chave Evento</b><span class="glyphicon glyphicon-sort"></span></th>
                        <th @click.prevent.stop="ordenatabela('obs',$event)" vp-ordertype="0"><b>obs</b><span class="glyphicon glyphicon-sort"></span></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item in itensfiltered " :key="item.id">
                        <td scope="col">{{item.usuario}}</td>
                        <td scope="col">{{item.data}}</td>
                        <td scope="col">{{item.nomeevento}}</td>
                        <td scope="col">{{item.chaveevento}}</td>
                        <td scope="col">{{item.obs}}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script src="../js/vuecomponents/vplogeventos.js"></script>
