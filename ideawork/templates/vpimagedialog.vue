<template>
    <div class="image-dialog">
        <button class="image-dialog-trigger" type="button" @click="showDialog"><img class="image-dialog-thumb" ref="thumb" :src="thumb" /></button>
        <transition name="dialog" @enter="enter" @leave="leave">
            <div class="image-dialog-background" v-show="appearedDialog" ref="dialog">
                <button class="image-dialog-close" type="button" @click="hideDialog" aria-label="Close"></button><img class="image-dialog-animate" ref="animate" :class="{ loading: !loaded }" :src="loaded ? full : thumb" /><img class="image-dialog-full" ref="full" :src="appearedDialog &amp;&amp; full" :width="fullWidth" :height="fullHeight" @load="onLoadFull" />
            </div>
        </transition>
    </div>
</template>

<script src="../js/vuecomponents/vpimagedialog.js"></script>