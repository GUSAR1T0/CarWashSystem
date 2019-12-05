import Vue from "vue";
import VueRouter from "vue-router";
import Home from "@/views/Home.vue";

Vue.use(VueRouter);

const routes = [
    {
        path: "/",
        name: "home",
        component: Home
    },
    {
        path: "/about",
        name: "about",
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ "../views/About.vue")
    },
    {
        path: "/auth",
        name: "authentication",
        component: () => import(/* webpackChunkName: "auth" */ "../views/Authentication.vue")
    },
    {
        path: "/profile",
        name: "companyProfile",
        component: () => import(/* webpackChunkName: "profile" */ "../views/company-profile/CompanyProfile.vue")
    },
    {
        path: "/profile/edit",
        name: "companyProfileEdit",
        component: () => import(/* webpackChunkName: "company-profile-edit" */ "../views/company-profile/CompanyProfileEdit.vue")
    },
    {
        path: "/profile/car-wash",
        name: "carWashList",
        component: () => import(/* webpackChunkName: "car-wash-list" */ "../views/company-profile/CarWashList.vue")
    },
    {
        path: "/profile/car-wash/:id",
        name: "carWashView",
        component: () => import(/* webpackChunkName: "car-wash-view" */ "../views/company-profile/CarWashView.vue")
    },
    {
        path: "/profile/car-wash/edit/:id",
        name: "carWashGeneralInfoEdit",
        component: () => import(/* webpackChunkName: "car-wash-general-info-edit" */ "../views/company-profile/CarWashGeneralInfoEdit.vue")
    }
];

const router = new VueRouter({
    mode: "history",
    base: process.env.BASE_URL,
    routes
});

export default router;
