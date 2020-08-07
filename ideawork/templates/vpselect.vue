<template>
    <div class="form-group">
        <label :class="'col-' + colsize + '-' + labelsize + ' control-label'">{{label}}</label>
        <div :class="'col-' + colsize + '-' + inputsize">
            <select class="form-control" v-bind:value="value" v-on:input="$emit('input', $event.target.value)" :disabled="disabled=='1'" :size="size">
                <option v-for="item in itens" :value="item.cod">{{item.des}}</option>
            </select>
        </div>
    </div>
</template>
<script src="../js/vuecomponents/vpselect.js"></script>
