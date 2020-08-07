<template>
    <div class="row">
        <label :class="'col-' + colsize + '-' + labelsize + ' control-label'">{{label}}</label>
        <div :class="'col-' + colsize + '-' + inputsize">
            <input type="text" ref="number" :class="'form-control ' + classalert " :placeholder="placeholder" @keyup.enter="checknumero()" v-bind:value="value" v-on:input="$emit('input', $event.target.value)" />
        </div>
        <label :class="'col-' + colsize + '-' + infosize + ' control-label'" class="minmax">{{minmax}}</label>
    </div>
</template>

<script src="../js/vuecomponents/vpinputnumber.js"></script>