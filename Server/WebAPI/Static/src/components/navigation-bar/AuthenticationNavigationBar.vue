<template>
    <div class="nav">
        <el-menu class="el-nav-menu-horizontal" :router="true" :default-active="$route.path" mode="horizontal">
            <el-menu-item class="el-nav-menu-horizontal-header el-nav-menu-horizontal-main-item" index="/auth">
                <Title/>
            </el-menu-item>
        </el-menu>
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
    .el-nav-menu-horizontal-main-item.is-active {
        color: #631D76 !important;
    }
</style>

<script>
    import Title from "@/components/navigation-bar/Title";

    export default {
        name: "AuthenticationNavigationBar",
        components: {
            Title
        }
    };
</script>