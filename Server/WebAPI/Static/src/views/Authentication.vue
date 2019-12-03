<template>
    <div id="auth">
        <el-tabs :stretch="true" class="auth-tabs">
            <el-tab-pane>
                <span slot="label">Sign In</span>
                <SignIn/>
            </el-tab-pane>
            <el-tab-pane>
                <span slot="label">Sign Up</span>
                <SignUp/>
            </el-tab-pane>
        </el-tabs>
    </div>
</template>

<style>
    .auth-tabs.el-tabs > .el-tabs__header > .el-tabs__nav-wrap > .el-tabs__nav-scroll > .el-tabs__nav > .el-tabs__item {
        line-height: 36px !important;
        font-size: 18px !important;
    }
</style>

<script>
    import SignIn from "@/components/authorization/SignIn.vue";
    import SignUp from "@/components/authorization/SignUp.vue";

    export default {
        name: "Authentication",
        components: {
            SignIn,
            SignUp
        }
    };
</script>
