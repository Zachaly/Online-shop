const app = new Vue({
    el: '#app',
    data: {
        editing: false,
        loading: false,
        id: 0,
        objectIndex: 0,
        products: [],
        productModel: { id: 0, name: "Product Name", description: "Product description", value: 0.00 }
    },
    mounted() {
        this.getProducts();
    },
    methods: {
        getProducts() {
            this.loading = true;
            axios.get("/products").
                then(res => this.products = res.data).
                catch(error => console.log(error)).
                then(() => this.loading = false);
        },
        getProduct(id) {
            this.loading = true;
            axios.get("/products/" + id,).
                then(res => {
                    console.log(res);
                    let product = res.data;
                    this.productModel = {
                        id: product.id,
                        name: product.name,
                        description: product.description,
                        value: product.value,
                    }
                }).
                catch(error => console.log(error)).
                then(() => this.loading = false);
        },
        createProduct() {
            this.loading = true;
            axios.post("/products", this.productModel).
                then(res => {
                    console.log(res.data);
                    this.products.push(res.data);
                }).
                catch(error => console.log(error)).
                then(() => {
                    this.loading = false;
                    this.editing = false;
                });;
        },
        editProduct(product, index) {
            this.objectIndex = index;
            this.getProduct(product.id);
            this.editing = true;
            console.log(this.editing);
        },
        updateProduct() {
            this.loading = true;
            axios.put("/products", this.productModel).
                then(res => {
                    console.log(res.data);
                    this.products.splice(this.objectIndex, 1, res.data);
                }).
                catch(error => console.log(error)).
                then(() => {
                    this.loading = false;
                    this.editing = false;
                });;
        },
        deleteProduct(id, index) {
            this.loading = true;
            axios.delete("/products/" + id,).
                then(() => this.products.splice(index, 1)).
                catch(error => console.log(error)).
                then(() => this.loading = false);
        },
        cancel() {
            this.editing = false;
        },
        newProduct() {
            this.editing = true;
            this.productModel.id = 0;
        },
    },
    computed: {

    }
});