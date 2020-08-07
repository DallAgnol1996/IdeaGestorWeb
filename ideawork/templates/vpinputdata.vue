<template>
    <div class="row">
        <label :class="'col-' + colsize + '-' + labelsize + ' control-label'">{{label}}</label>
        <div :class="'col-' + colsize + '-' + inputsize">
            <input type='date' class="form-control" v-bind:value="value" v-on:input="$emit('input', $event.target.value)" :disabled="disabled=='1'" />
        </div>
    </div>
</template>

<script src="../js/vuecomponents/vpinputdata.js"></script>