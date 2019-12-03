<template>
    <el-card shadow="hover" class="sign-up">
        <el-form :model="signUpForm" :rules="signUpRules" ref="signUpForm" label-position="top"
                 @submit.native.prevent="submitForm('signUpForm')">
            <el-row class="auth-field-element" type="flex" justify="center">
                <el-col :xs="24" :sm="20" :md="16" :lg="12" :xl="8">
                    <el-form-item prop="name">
                        <slot name="label"><strong class="auth-label-item">Company Name</strong></slot>
                        <el-input v-model="signUpForm.name" clearable></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row class="auth-field-element" type="flex" justify="center">
                <el-col :xs="24" :sm="20" :md="16" :lg="12" :xl="8">
                    <el-form-item prop="email">
                        <slot name="label"><strong class="auth-label-item">Company Email Address</strong></slot>
                        <el-input v-model="signUpForm.email" clearable></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row class="auth-field-element" type="flex" justify="center">
                <el-col :xs="24" :sm="20" :md="16" :lg="12" :xl="8">
                    <el-form-item prop="password">
                        <slot name="label"><strong class="auth-label-item">Password</strong></slot>
                        <el-input v-model="signUpForm.password" show-password clearable></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row class="auth-field-element" type="flex" justify="center">
                <el-col :xs="24" :sm="20" :md="16" :lg="12" :xl="8">
                    <el-form-item prop="passwordConfirmation">
                        <slot name="label"><strong class="auth-label-item">Password Confirmation</strong></slot>
                        <el-input v-model="signUpForm.passwordConfirmation" show-password clearable></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row class="auth-field-element" type="flex" justify="center">
                <el-col :xs="24" :sm="20" :md="16" :lg="12" :xl="8">
                    <el-form-item>
                        <el-button type="primary" ref="signUpButton" class="auth-button" native-type="submit">
                            <strong>Sign Up</strong>
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
    import { SignUpValidations } from "@/extensions/validations";
    import { SIGN_UP_REQUEST } from "@/constants/actions";
    import { mapGetters } from "vuex";
    import { renderErrorNotificationMessage } from "@/extensions/utils";

    let signUpForm = {
        name: "",
        email: "",
        password: "",
        passwordConfirmation: ""
    };
    let validations = new SignUpValidations(signUpForm);

    export default {
        name: "SingUp",
        data() {
            return {
                signUpForm,
                signUpRules: {
                    name: [
                        {required: true, message: "Please, input the company name", trigger: "change"},
                        {min: 1, max: 50, message: "Length should be from 1 to 50", trigger: "change"}
                    ],
                    email: [
                        {required: true, message: "Please, input the email address", trigger: "change"},
                        {type: "email", message: "Please, input correct email address", trigger: "change"},
                        {min: 3, max: 255, message: "Length should be from 3 to 255", trigger: "change"}
                    ],
                    password: [
                        {required: true, validator: validations.validatePassword, trigger: "change"}
                    ],
                    passwordConfirmation: [
                        {required: true, validator: validations.validatePasswordConfirmation, trigger: "change"}
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
                this.$refs.signUpButton.loading = true;
                this.$refs[formName].validate((valid) => {
                    if (!valid) {
                        this.$refs.signUpButton.loading = false;
                        return false;
                    }

                    this.$store.dispatch(SIGN_UP_REQUEST, this.signUpForm).then(() => {
                        this.$refs.signUpButton.loading = false;
                        this.$router.push("/");
                        const h = this.$createElement;
                        this.$notify.success({
                            title: "Successful registration",
                            message: h("div", null, [
                                "You are signed up as \"",
                                h("strong", null, this.getCompanyName),
                                "\" company representative"
                            ])
                        });
                    }).catch(error => {
                        this.$refs.signUpButton.loading = false;
                        this.$notify.error({
                            title: "Failed to sign up",
                            duration: 10000,
                            message: renderErrorNotificationMessage(this.$createElement, error.response)
                        });
                    });
                });
            }
        }
    };
</script>
