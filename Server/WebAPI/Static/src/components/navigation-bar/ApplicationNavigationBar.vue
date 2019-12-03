<template>
    <div class="nav">
        <el-menu class="el-nav-menu-horizontal" :router="true" :default-active="$route.path" mode="horizontal">
            <el-menu-item class="el-nav-menu-horizontal-header el-nav-menu-horizontal-main-item" index="/">
                <Title/>
            </el-menu-item>
            <!-- B: Pages -->
            <el-submenu class="el-nav-menu-horizontal-pages el-nav-menu-horizontal-main-item"
                        index="Pages">
                <template slot="title">
                    <i class="el-icon-files"></i>
                </template>
                <el-menu-item-group>
                    <span slot="title" class="el-nav-menu-horizontal-group-title">
                        <strong>Pages</strong>
                    </span>
                    <el-divider/>
                    <el-menu-item index="/about">
                        <span slot="title" class="el-nav-menu-horizontal-item">About</span>
                    </el-menu-item>
                </el-menu-item-group>
            </el-submenu>
            <!-- E: Pages -->
            <!-- B: Account -->
            <el-submenu class="el-nav-menu-horizontal-account el-nav-menu-horizontal-main-item"
                        index="Account">
                <template slot="title">
                    <i class="el-icon-user-solid"></i>
                </template>
                <el-menu-item-group>
                    <span slot="title" class="el-nav-menu-horizontal-group-title">
                        <strong>Account</strong>
                    </span>
                    <el-divider/>
                    <el-menu-item index="/profile">
                        <span slot="title" class="el-nav-menu-horizontal-item">
                            Company Profile
                        </span>
                    </el-menu-item>
                    <el-menu-item index="/profile/car-wash">
                        <span slot="title" class="el-nav-menu-horizontal-item">
                            Car Wash List
                        </span>
                    </el-menu-item>
                    <el-menu-item index="" @click="logoutDialogStatus.visible = true">
                        <span slot="title" class="el-nav-menu-horizontal-item">Sign Out</span>
                    </el-menu-item>
                </el-menu-item-group>
            </el-submenu>
            <!-- E: Account -->
        </el-menu>
        <ConfirmationDialog :dialog-status="logoutDialogStatus"
                            :confirmation-text="() => 'Are you sure that you want to sign out?'"
                            :cancel-click-action="() => logoutDialogStatus.visible = false"
                            :submit-click-action="logoutAction"/>
    </div>
</template>

<style>
    .el-divider--horizontal {
        margin: 5px 0 5px 0 !important;
    }

    .el-menu--horizontal > .el-menu-item {
        line-height: 54px !important;
    }

    .el-nav-menu-horizontal-main-item:hover,
    .el-nav-menu-horizontal-main-item.is-active,
    .el-nav-menu-horizontal-main-item:hover .el-submenu__title > i,
    .el-nav-menu-horizontal-main-item.is-active .el-submenu__title > i,
    .el-nav-menu-horizontal-main-item.is-opened .el-submenu__title > i,
    .el-menu-item:hover > span,
    .el-menu-item.is-active > span,
    .el-menu-item.is-opened > span {
        color: #631D76 !important;
    }
</style>

<style scoped>
    .el-nav-menu-horizontal-group-title {
        margin-left: -25px;
        text-transform: uppercase;
        font-size: 12px;
        color: #2c3e50;
    }

    .el-nav-menu-horizontal-item {
        padding: 5px;
        font-size: 18px;
    }
</style>

<script>
    import { SIGN_OUT_REQUEST } from "@/constants/actions";
    import Title from "@/components/navigation-bar/Title";
    import ConfirmationDialog from "@/components/core/ConfirmationDialog";

    export default {
        name: "ApplicationNavigationBar",
        components: {
            Title,
            ConfirmationDialog
        },
        data() {
            return {
                logoutDialogStatus: {
                    visible: false
                }
            };
        },
        methods: {
            logoutAction(button) {
                button.loading = true;
                this.$store.dispatch(SIGN_OUT_REQUEST).then(() => {
                    button.loading = false;
                    this.$router.push("/auth").catch(() => {
                    });
                    this.$notify.success({
                        title: "Successful log out",
                        message: "You are signed out"
                    });
                }).catch(() => {
                    button.loading = false;
                    this.$router.push("/auth").catch(() => {
                    });
                    this.$notify.error({
                        title: "Unsuccessful log out",
                        message: "Operation is failed"
                    });
                });
            }
        }
    };
</script>