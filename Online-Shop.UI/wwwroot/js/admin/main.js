let app = new Vue({
    el: '#app',
    data: {
        price: 0,
        showPrice: true,
    },
    computed: {
        formatPrice: function() {
            return this.price + "$";
        }
    },
    methods: {
        togglePrice: function () {
            this.showPrice = !this.showPrice;
        }
    }
});