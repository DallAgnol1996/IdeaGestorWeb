
    module.exports = {
        props: {
            thumb: String,
            full: String,
            fullWidth: Number,
            fullHeight: Number
        },
        data: function data() {
            return {
                loaded: false,
                appearedDialog: false
            };
        },
        methods: {
            showDialog: function showDialog() {
                this.appearedDialog = true;
            },
            hideDialog: function hideDialog() {
                this.appearedDialog = false;
            },
            enter: function enter() {
                this.animateImage(
                    this.$refs.thumb,
                    this.$refs.full);
            },

            leave: function leave() {
                this.animateImage(
                    this.$refs.full,
                    this.$refs.thumb);

            },

            onLoadFull: function onLoadFull() {
                this.loaded = true;
            },

            animateImage: function animateImage(startEl, destEl) {
                var _this = this;
                var start = this.getBoundForDialog(startEl);
                this.setStart(start);

                this.$nextTick(function () {
                    var dest = _this.getBoundForDialog(destEl);
                    _this.setDestination(start, {
                        top: dest.top,
                        left: dest.left,
                        width: dest.width || _this.fullWidth,
                        height: dest.height || _this.fullHeight
                    });

                });
            },

            getBoundForDialog: function getBoundForDialog(el) {
                var bound = el.getBoundingClientRect();
                var dialog = this.$refs.dialog;
                return {
                    top: bound.top + dialog.scrollTop,
                    left: bound.left + dialog.scrollLeft,
                    width: bound.width,
                    height: bound.height
                };

            },

            setStart: function setStart(start) {
                var el = this.$refs.animate;
                el.style.left = start.left + 'px';
                el.style.top = start.top + 'px';
                el.style.width = start.width + 'px';
                el.style.height = start.height + 'px';
                el.style.transitionDuration = '0s';
                el.style.transform = '';
            },

            setDestination: function setDestination(start, dest) {
                var el = this.$refs.animate;
                el.style.transitionDuration = '';

                var translate = 'translate(' + (dest.left - start.left) + 'px, ' + (dest.top - start.top) + 'px)';
                var scale = 'scale(' + dest.width / start.width + ', ' + dest.height / start.height + ')';
                el.style.transform = translate + ' ' + scale;
            }
        }
    }
