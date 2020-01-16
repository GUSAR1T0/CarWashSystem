<template>
    <div id="app">
        <ApplicationNavigationBar v-if="isAuthenticated"/>
        <AuthenticationNavigationBar v-else/>
        <el-container class="app-container" v-if="!loadingIsActive">
            <el-main class="app-main">
                <router-view/>
            </el-main>
            <el-footer class="app-footer" height="auto">
                Copyright © {{ getYearsForFooter }} VXDESIGN.STORE
            </el-footer>
        </el-container>
    </div>
</template>

<style src="@/styles/application.css">
</style>

<script>
    import { ON_LOAD_REQUEST, RESET_PATH_FOR_REDIRECTION } from "@/constants/actions";
    import ApplicationNavigationBar from "@/components/navigation-bar/ApplicationNavigationBar";
    import AuthenticationNavigationBar from "@/components/navigation-bar/AuthenticationNavigationBar";
    import { mapGetters } from "vuex";

    export default {
        components: {
            ApplicationNavigationBar,
            AuthenticationNavigationBar
        },
        data() {
            return {
                loadingIsActive: true
            };
        },
        computed: {
            ...mapGetters([
                "isAuthenticated"
            ]),
            getYearsForFooter() {
                let firstYear = 2019;
                let currentYear = new Date().getFullYear();
                return `${firstYear + (currentYear !== firstYear ? `-${currentYear}` : "")}`;
            }
        },
        mounted() {
            const loading = this.$loading({
                lock: true,
                spinner: "el-icon-loading",
                background: "rgba(255, 255, 255, 1)",
                customClass: "main-loading-spinner-custom"
            });

            let completeLoading = () => {
                loading.close();
                this.loadingIsActive = false;
            };

            this.$store.dispatch(ON_LOAD_REQUEST, this.$store.getters.getPathForRedirection).then(redirectTo => {
                if (!this.isAuthenticated && redirectTo !== "/auth") {
                    redirectTo = "/auth";
                } else if (this.isAuthenticated && redirectTo === "/auth") {
                    redirectTo = "/";
                }
                
                this.$router.push(redirectTo).then(() => completeLoading()).catch(() => {
                    // TODO: Error case handling
                    completeLoading();
                });
            }).catch(() => {
                this.$router.push("/auth").then(() => completeLoading()).catch(() => {
                    // TODO: Error case handling
                    completeLoading();
                });
            });
            this.$store.dispatch(RESET_PATH_FOR_REDIRECTION);
        }
    };
</script>
