<template>
    <div class="row">
        <label :class="'col-' + colsize + '-' + labelsize + ' control-label'">{{label}}</label>
        <div :class="'col-' + colsize + '-' + inputsize">
            <input type="password" class="form-control" :placeholder="placeholder" v-bind:value="value" v-on:input="$emit('input', $event.target.value)" />
        </div>
    </div>
</template>


<script src="../js/vuecomponents/vpinputpassword.js"></script>