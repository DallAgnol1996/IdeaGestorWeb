<template>
    <div class="form-group">
        <button class="btn-upload-proidea btn-default"><i class="fa fa-upload" aria-hidden="true"></i>&nbsp;{{this.desbutton}}</button>
            <input type="file" ref="file" class="btn-upload"  v-on:change="handleFileUpload()"/>{{title}}
    </div>
</template>
<script src="../js/vuecomponents/vpuploadsinglefile.js"></script>
