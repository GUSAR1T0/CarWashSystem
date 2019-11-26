import Vue from "vue";
import App from "@/App.vue";
import store from "@/plugins/store";
import router from "@/plugins/router";
import "@/plugins/element/element";
import { SET_PATH_FOR_REDIRECTION } from "@/constants/actions";

Vue.config.productionTip = false;

new Vue({
    store,
    router,
    render: h => h(App),
    beforeCreate() {
        this.$store.dispatch(SET_PATH_FOR_REDIRECTION, window.location.pathname);
    }
}).$mount("#app");
