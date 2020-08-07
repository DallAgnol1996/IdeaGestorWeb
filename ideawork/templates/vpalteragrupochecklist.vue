<template>
    <div class="card-content">
        <div class="card">
            <h6 class="card-header groupcheck" style="text-align: center;">Nome do Grupo</h6>
            <div class="card-body">
                <div class="container">
                    <div class="row">
                        <div class="col-sm-1">
                            <button type="button" class="btn btn-defaults btn-sm" @click.prevent.stop="addItem()"><i class="fa fa-plus-circle"></i> Item</button>
                        </div>
                        <div class="col-sm-2">
                            <vp-inputtext :value="itemchecklist.cod" @input="itemchecklist.cod=$event" label="Codigo:" placeholder="Código"></vp-inputtext>
                        </div>
                        <div class="col-sm-3">
                            <vp-inputtext :value="itemchecklist.des" @input="itemchecklist.des=$event" label="Descrição:" placeholder="Descrição"></vp-inputtext>
                        </div>
                        <div class="col-sm-3">
                            <vp-inputtext :value="itemchecklist.fotoobrigatoria" @input="itemchecklist.fotoobrigatoria=$event" label="Referência:" placeholder="^Referência"></vp-inputtext>
                        </div>
                        <div class="col-sm-1">
                            Foto Obrigatória
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script src="../js/vuecomponents/vpalteragrupochecklist.js"></script>
