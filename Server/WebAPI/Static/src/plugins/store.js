import Vue from "vue";
import Vuex from "vuex";
import initialization from "@/store/initialization";
import httpClient from "@/store/httpClient";
import authentication from "@/store/authentication";
import companyProfile from "@/store/companyProfile";
import carWashList from "@/store/carWashList";
import carWashEdit from "@/store/carWashEdit";

Vue.use(Vuex);

export default new Vuex.Store({
    state: {},
    mutations: {},
    actions: {},
    modules: {initialization, httpClient, authentication, companyProfile, carWashList, carWashEdit}
});
