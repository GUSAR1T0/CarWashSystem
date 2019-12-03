<template>
    <LoadingContainer :loading-state="loadingIsActive">
        <template slot="content">
            <div class="header first-line">
                <span style="font-size: 24px">Company Profile</span>
                <div style="margin-left: auto">
                    <el-button class="functional-button" type="primary" icon="el-icon-edit"
                               @click="$router.push('/profile/edit')"/>
                </div>
            </div>
            <CompanyProfileInfoRow name="Company Name" :value="model.name" style="padding-bottom: 20px"/>
            <el-divider/>
            <CarWashInfoRow name="Contacts" style="padding-top: 20px">
                <template slot="value">
                    <el-table :data="[model]">
                        <el-table-column width="auto">
                            <template slot="header">
                                <div class="advantage-header">Email Address</div>
                            </template>
                            <template slot-scope="scope">
                                <a v-if="scope.row.email" :href="`mailto:${scope.row.email}`">
                                    <el-button class="functional-button contacts-button">
                                        {{ scope.row.email }}
                                    </el-button>
                                </a>
                                <div v-else>—</div>
                            </template>
                        </el-table-column>
                        <el-table-column width="auto">
                            <template slot="header">
                                <div class="advantage-header">Phone</div>
                            </template>
                            <template slot-scope="scope">
                                <a v-if="scope.row.phone" :href="`tel:${scope.row.phone}`">
                                    <el-button class="functional-button contacts-button">
                                        {{ scope.row.phone }}
                                    </el-button>
                                </a>
                                <div v-else>—</div>
                            </template>
                        </el-table-column>
                    </el-table>
                </template>
            </CarWashInfoRow>
        </template>
    </LoadingContainer>
</template>

<style scoped src="@/styles/companyProfile.css">
</style>

<script>
    import { mapGetters } from "vuex";
    import LoadingContainer from "@/components/core/LoadingContainer";
    import CompanyProfileInfoRow from "@/components/company-profile/CompanyProfileInfoRow";
    import CarWashInfoRow from "@/components/company-profile/CarWashInfoRow";
    import { GET_COMPANY_PROFILE_REQUEST } from "@/constants/actions";
    import { renderErrorNotificationMessage } from "@/extensions/utils";

    export default {
        name: "CompanyProfile",
        components: {
            LoadingContainer,
            CompanyProfileInfoRow,
            CarWashInfoRow
        },
        data() {
            return {
                loadingIsActive: true,
                model: {
                    name: null,
                    email: null,
                    phone: null
                }
            };
        },
        computed: {
            ...mapGetters([
                "getCompanyProfileForm"
            ])
        },
        methods: {
            loadProfile() {
                this.loadingIsActive = true;
                this.$store.dispatch(GET_COMPANY_PROFILE_REQUEST).then(() => {
                    this.loadingIsActive = false;
                    let model = this.getCompanyProfileForm;
                    this.model.name = model.name;
                    this.model.email = model.email;
                    this.model.phone = model.phone;
                }).catch(error => {
                    this.loadingIsActive = false;
                    this.$notify.error({
                        title: "Failed to load company profile",
                        duration: 10000,
                        message: renderErrorNotificationMessage(this.$createElement, error.response)
                    });
                });
            }
        },
        mounted() {
            this.loadProfile();
        },
        beforeRouteUpdate(to, from, next) {
            this.loadProfile();
            next();
        }
    };
</script>