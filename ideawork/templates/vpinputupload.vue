<template>
    <button type="submit" @click="inseriranexos()" class="btn btn-default">
        <span v-if="useuploadicon==1" class="glyphicon glyphicon-upload" aria-hidden="true"></span>{{label}}
    </button>
</template>
<script src="../js/vuecomponents/vpinputupload.js"></script>