<template>
    <div class="nav">
        <el-menu class="el-nav-menu-horizontal" :router="true" :default-active="$route.path" mode="horizontal">
            <el-menu-item class="el-nav-menu-horizontal-header" index="/">
                <div style="font-size: 30px">
                    CAR WASH SYSTEM
                </div>
            </el-menu-item>
            <!-- B: Pages -->
            <el-submenu class="el-nav-menu-horizontal-users" index="Pages">
                <template slot="title">
                    <i class="el-icon-files"></i>
                </template>
                <el-menu-item-group>
                    <span slot="title" class="el-nav-menu-horizontal-group-title">Pages</span>
                    <el-divider/>
                    <el-menu-item index="/about">
                        <span slot="title" class="el-nav-menu-horizontal-item">About</span>
                    </el-menu-item>
                </el-menu-item-group>
            </el-submenu>
            <!-- E: Pages -->
            <!-- B: Account -->
            <el-submenu class="el-nav-menu-horizontal-users" index="Account">
                <template slot="title">
                    <i class="el-icon-user-solid"></i>
                </template>
                <el-menu-item-group>
                    <span slot="title" class="el-nav-menu-horizontal-group-title">Account</span>
                    <el-divider/>
                    <el-menu-item index="">
                        <span slot="title" class="el-nav-menu-horizontal-item">Company: "<strong>{{ getCompanyName }}</strong>"</span>
                    </el-menu-item>
                    <el-menu-item index="" @click="logoutAction">
                        <span slot="title" class="el-nav-menu-horizontal-item">Sign Out</span>
                    </el-menu-item>
                </el-menu-item-group>
            </el-submenu>
            <!-- E: Account -->
        </el-menu>
    </div>
</template>

<style>
    .el-divider--horizontal {
        margin: 5px 0 10px 0 !important;
    }
</style>

<style scoped>
    .el-nav-menu-horizontal-group-title {
        margin-left: -25px;
        text-transform: uppercase;
        font-size: 12px;
    }
    
    .el-nav-menu-horizontal-item {
        padding: 5px;
        font-size: 18px;
    }
</style>

<script>
    import { SIGN_OUT_REQUEST } from "@/constants/actions";
    import { mapGetters } from "vuex";

    export default {
        name: "NavigationBar",
        computed: {
            ...mapGetters([
                "getCompanyName"
            ])
        },
        methods: {
            logoutAction() {
                this.$store.dispatch(SIGN_OUT_REQUEST).then(() => {
                    this.$router.push("/auth").catch(() => {
                    });
                    this.$notify.success({
                        title: "You are logged out",
                        message: "Waiting for you again"
                    });
                }).catch(() => {
                    this.$router.push("/auth").catch(() => {
                    });
                });
            }
        }
    };
</script>