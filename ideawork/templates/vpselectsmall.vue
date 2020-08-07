<template>
    <select v-bind:value="value" v-on:input="$emit('input', $event.target.value)" :disabled="disabled=='1'" style="width:150px; height:25px;">
        <option v-for="item in itens" :value="item.cod">{{item.des}}</option>
    </select>
</template>
<script src="../js/vuecomponents/vpselect.js"></script>
