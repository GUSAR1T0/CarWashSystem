<template>
    <div id="app">
        <el-container class="app-container" v-if="!loadingIsActive">
            <el-header class="app-header" height="auto">
                <h1 style="font-size: 30px">CAR WASH SYSTEM</h1>
            </el-header>
            <el-main class="app-main">
                <router-view/>
            </el-main>
            <el-footer class="app-footer" height="auto">
                Copyright © {{ getYearsForFooter }} VXDESIGN.STORE
            </el-footer>
        </el-container>
    </div>
</template>

<style>
    * {
        font-family: 'Avenir', Helvetica, Arial, sans-serif;
    }

    html, body {
        height: 100%;
    }

    body {
        margin: 0;
    }

    #app {
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
        display: flex;
        flex-direction: column;
        width: 100%;
        height: 100%;
    }

    .app-header {
        padding-top: 20px !important;
    }

    .app-footer {
        margin: 20px 0;
        font-size: 14px;
        text-align: center;
        display: flex;
        align-items: center;
        justify-content: center;
    }

    .main-loading-spinner-custom > div > .el-icon-loading {
        font-size: 48px;
    }

    .main-loading-spinner-custom > div > .el-loading-text {
        font-size: 24px;
    }
</style>

<script>
    import { mapGetters } from "vuex";
    import { ON_LOAD_REQUEST, RESET_PATH_FOR_REDIRECTION } from "@/constants/actions";

    export default {
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
