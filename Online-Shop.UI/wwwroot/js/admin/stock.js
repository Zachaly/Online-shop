const app = new Vue({
    el: "#app",
    data: {
        loading: false,
        products: [],
        newStock: {
            productId: 0,
            description: "Size",
            quantity: 0
        },
        selectedProduct: null,
    },
    mounted() {
        this.getStock();
    },
    methods: {
        getStock() {
            this.loading = true;
            axios.get("/Admin/stocks").
                then(res => this.products = res.data).
                catch(error => console.log(error)).
                then(() => this.loading = false);
        },
        selectProduct(product) {
            this.selectedProduct = product;
            this.newStock.productId = product.id;
        },
        addStock() {
            this.loading = true;
            axios.post("/Admin/stocks", this.newStock).
                then(res => this.selectedProduct.stock.push(res.data)).
                catch(error => console.log(error)).
                then(() => this.loading = false);
        },
        updateStock() {
            this.loading = true;
            let viewModel = this.selectedProduct.stock.map(stock => {
                return {
                    id: stock.id,
                    description: stock.description,
                    quantity: stock.quantity,
                    productId: this.selectedProduct.id,
                }
            });
            axios.put("/Admin/stocks", {
                stock: viewModel,
            }).then(res => console.log(res)).
                catch(error => console.log(error)).
                then(() => this.loading = false);
                
        },
        deleteStock(id, index) {
            this.loading = true;
            axios.delete("/Admin/stocks/" + id).
                then(res => this.selectedProduct.stock.splice(index, 1)).
                catch(error => console.log(error)).
                then(() => this.loading = false);
        }
    }
})