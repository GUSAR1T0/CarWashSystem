<template>
    <LoadingContainer :loading-state="loadingIsActive">
        <template slot="content">
            <b class="edit-page-title">Edit Company Profile</b>
            <el-form class="edit-page-form" :model="model" :rules="rules" ref="form" label-width="200px"
                     @submit.native.prevent="submitForm('form')">
                <el-row class="edit-field-element" type="flex" justify="center" align="middle">
                    <el-col :xs="24" :sm="20" :md="16" :lg="16" :xl="16">
                        <el-form-item prop="name" label="Name">
                            <el-input v-model="model.name" clearable></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row class="edit-field-element" type="flex" justify="center" align="middle">
                    <el-col :xs="24" :sm="20" :md="16" :lg="16" :xl="16">
                        <el-form-item prop="email" label="Email Address">
                            <el-input v-model="model.email" clearable></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row class="edit-field-element" type="flex" justify="center" align="middle">
                    <el-col :xs="24" :sm="20" :md="16" :lg="16" :xl="16">
                        <el-form-item prop="phone" label="Phone Number">
                            <el-input v-model="model.phone" clearable></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row class="auth-field-element" type="flex" justify="center" align="middle">
                    <el-col :xs="24" :sm="20" :md="16" :lg="16" :xl="16">
                        <el-form-item>
                            <el-row type="flex" justify="center" align="middle" :gutter="20">
                                <el-col :span="12">
                                    <el-button type="primary" plain @click="$router.push('/profile')"
                                               style="width: 100%">
                                        <b style="font-size: 20px">Go to Profile</b>
                                    </el-button>
                                </el-col>
                                <el-col :span="12">
                                    <el-button type="danger" plain ref="formButton" native-type="submit"
                                               style="width: 100%">
                                        <b style="font-size: 20px">Submit</b>
                                    </el-button>
                                </el-col>
                            </el-row>
                        </el-form-item>
                    </el-col>
                </el-row>
            </el-form>
        </template>
    </LoadingContainer>
</template>

<style>
    .el-form-item__label {
        font-size: 20px !important;
        font-weight: bold;
    }
</style>

<style scoped src="@/styles/companyProfile.css">
</style>

<script>
    import LoadingContainer from "@/components/core/LoadingContainer";
    import { mapGetters } from "vuex";
    import { GET_COMPANY_PROFILE_REQUEST, UPDATE_COMPANY_PROFILE_REQUEST } from "@/constants/actions";
    import { renderErrorNotificationMessage } from "@/extensions/utils";

    export default {
        name: "CompanyProfileEdit",
        components: {
            LoadingContainer
        },
        data() {
            return {
                loadingIsActive: true,
                model: {},
                rules: {
                    name: [
                        {required: true, message: "Please, input name", trigger: "change"},
                        {min: 1, max: 50, message: "Length should be from 1 to 50", trigger: "change"}
                    ],
                    email: [
                        {type: "email", message: "Please, input correct email address", trigger: "change"},
                        {min: 3, max: 255, message: "Length should be from 3 to 255", trigger: "change"}
                    ],
                    phone: [
                        {
                            pattern: /\D*([2-9]\d{2})(\D*)(\d{3})(\D*)(\d{4})\D*/,
                            message: "Please, input correct phone number",
                            trigger: "change"
                        },
                        {min: 5, max: 32, message: "Length should be from 5 to 32", trigger: "change"}
                    ]
                },
            };
        },
        computed: {
            ...mapGetters([
                "getCompanyProfileForm"
            ])
        },
        methods: {
            loadCompanyProfile() {
                this.loadingIsActive = true;
                this.$store.dispatch(GET_COMPANY_PROFILE_REQUEST).then(() => {
                    this.loadingIsActive = false;
                    this.model = this.getCompanyProfileForm;
                }).catch(error => {
                    this.$notify.error({
                        title: "Failed to load company profile",
                        duration: 10000,
                        message: renderErrorNotificationMessage(this.$createElement, error.response)
                    });
                });
            },
            submitForm(formName) {
                this.$refs.formButton.loading = true;
                this.$refs[formName].validate((valid) => {
                    if (!valid) {
                        this.$refs.formButton.loading = false;
                        return false;
                    }

                    this.$store.dispatch(UPDATE_COMPANY_PROFILE_REQUEST, this.model).then(() => {
                        this.$refs.formButton.loading = false;
                        this.$router.push("/profile");
                        this.$notify.info({
                            title: "Action is successful",
                            message: "Company profile was updated"
                        });
                    }).catch(error => {
                        this.$refs.formButton.loading = false;
                        this.$notify.error({
                            title: "Failed to update company profile",
                            duration: 10000,
                            message: renderErrorNotificationMessage(this.$createElement, error.response)
                        });
                    });
                });
            }
        },
        mounted() {
            this.loadCompanyProfile();
        },
        beforeRouteUpdate(to, from, next) {
            this.loadCompanyProfile();
            next();
        }
    };
</script>