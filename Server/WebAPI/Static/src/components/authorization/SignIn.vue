<template>
    <el-card shadow="hover" class="sign-in">
        <el-form :model="signInForm" :rules="signInRules" ref="signInForm" label-position="top"
                 @submit.native.prevent="submitForm('signInForm')">
            <el-row class="auth-field-element" type="flex" justify="center">
                <el-col :xs="24" :sm="20" :md="16" :lg="12" :xl="8">
                    <el-form-item prop="email">
                        <slot name="label"><strong class="auth-label-item">Company Email Address</strong></slot>
                        <el-input v-model="signInForm.email" clearable></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row class="auth-field-element" type="flex" justify="center">
                <el-col :xs="24" :sm="20" :md="16" :lg="12" :xl="8">
                    <el-form-item prop="password">
                        <slot name="label"><strong class="auth-label-item">Password</strong></slot>
                        <el-input v-model="signInForm.password" show-password clearable></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row class="auth-field-element" type="flex" justify="center">
                <el-col :xs="24" :sm="20" :md="16" :lg="12" :xl="8">
                    <el-form-item>
                        <el-button type="primary" ref="signInButton" class="auth-button" native-type="submit">
                            <strong>Sign In</strong>
                        </el-button>
                    </el-form-item>
                </el-col>
            </el-row>
        </el-form>
    </el-card>
</template>

<style scoped src="@/styles/authorization.css">
</style>

<script>
    import { SIGN_IN_REQUEST } from "@/constants/actions";
    import { renderErrorNotificationMessage } from "@/extensions/utils";
    import { mapGetters } from "vuex";

    let signInForm = {
        email: "",
        password: ""
    };

    export default {
        name: "SignIn",
        data() {
            return {
                signInForm,
                signInRules: {
                    email: [
                        {required: true, message: "Email address is required", trigger: "change"},
                        {type: "email", message: "Please, input correct email address", trigger: "change"}
                    ],
                    password: [
                        {required: true, message: "Password is required", trigger: "change"}
                    ]
                }
            };
        },
        computed: {
            ...mapGetters([
                "getCompanyName"
            ])
        },
        methods: {
            submitForm(formName) {
                this.$refs.signInButton.loading = true;
                this.$refs[formName].validate(valid => {
                    if (!valid) {
                        this.$refs.signInButton.loading = false;
                        return false;
                    }

                    this.$store.dispatch(SIGN_IN_REQUEST, this.signInForm).then(() => {
                        this.$refs.signInButton.loading = false;
                        this.$router.push("/");
                        const h = this.$createElement;
                        this.$notify.success({
                            title: "Successful authorization",
                            message: h("div", null, [
                                "You are signed in as \"",
                                h("strong", null, this.getCompanyName),
                                "\" company representative"
                            ])
                        });
                    }).catch(error => {
                        this.$refs.signInButton.loading = false;
                        this.$notify.error({
                            title: "Failed to sign in",
                            duration: 10000,
                            message: renderErrorNotificationMessage(this.$createElement, error.response)
                        });
                    });
                });
            }
        }
    };
</script>
