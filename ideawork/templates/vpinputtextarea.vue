<template>
    <div class="form-group">
        <label :class="'col-' + colsize + '-' + labelsize + ' control-label'">{{label}}</label>
        <div :class="'col-' + colsize + '-' + inputsize">
            <textarea :rows="rownumber" :cols="colnumber" class="form-control" :placeholder="placeholder" v-bind:value="value" v-on:input="$emit('input', $event.target.value)" :disabled="disabled=='1'"></textarea>
        </div>
    </div>
</template>


<script src="../js/vuecomponents/vpinputtextarea.js"></script>