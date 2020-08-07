<template>
    <div class="row">
        <label :class="'col-' + colsize + '-' + labelsize + ' control-label'">{{label}}</label>
        <div :class="'col-' + colsize + '-' + inputsize">
            <input type='checkbox' class="form-control" ref="check" @click="SelecionaCheck()" :disabled="disabled=='1'" />
        </div>
    </div>
</template>

<script src="../js/vuecomponents/vpinputcheckbox.js"></script>