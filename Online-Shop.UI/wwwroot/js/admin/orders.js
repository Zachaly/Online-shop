let app = new Vue({
    el: "#app",
    data: {
        loading: false,
        status: 0,
        orders: [],
        selectedOrder: null,
    },
    mounted() {
        this.getOrders();
    },
    watch: {
        status: function () {
            this.getOrders();
        }
    },
    methods: {
        getOrders() {
            this.loading = true;
            axios.get("/orders?status=" + this.status).
                then(res => this.orders = res.data).
                catch(error => console.log(error)).
                then(() => this.loading = false);
        },
        selectOrder(id) {
            this.loading = true;
            axios.get("/orders/" + id).
                then(res => {
                    console.log(res.data);
                    this.selectedOrder = res.data;
                }).catch(error => console.log(error)).
                then(() => this.loading = false);
        },
        updateOrder(id) {
            this.loading = true;
            axios.put("/orders/" + id).
                then(res => {
                    console.log(res);
                    this.selectedOrder = null;
                    this.getOrders();
                }).catch(error => console.log(error)).
                then(() => this.loading = false)
        }
    }
})