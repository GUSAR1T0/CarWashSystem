<template>
    <div id="auth">
        <el-tabs :stretch="true">
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
    #auth {
        margin-top: -30px;
    }

    .el-tabs__item {
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
