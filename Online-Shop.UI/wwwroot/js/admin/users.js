const app = new Vue({
    el: "#app",
    data: {
        loading: false,
        users: [],
        userModel: { username: ""}
    },
    mounted() {
        this.getUsers();
    },
    methods: {
        createUser() {
            this.loading = true;
            axios.post("/users", this.userModel).
                then(res => {
                    console.log(res);
                    this.users.push(res.data);
                }).
                catch(error => console.log(error)).
                then(() => this.loading = false);
        },
        getUsers() {
            this.loading = true;
            axios.get("/users").
                then(res => this.users = res.data).
                catch(error => console.log(error)).
                then(() => this.loading = false);
        },
        deleteUser(id, index) {
            this.loading = true;
            axios.delete("/users/" + id).then(res => {
                console.log(res);
                this.users.splice(index, 1);
            }).catch(error => console.log(error)).
                then(() => this.loading = false);
        }
    }
})