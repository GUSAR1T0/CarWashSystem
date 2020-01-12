<template>
    <LoadingContainer :loading-state="loadingIsActive">
        <template slot="content">
            <b style="font-size: 40px">{{ model.name }}</b>
            <div class="divider">
                <el-divider/>
            </div>
            <div>
                <el-button class="functional-button" type="primary" icon="el-icon-edit"
                           @click="$router.push('/profile/edit')">
                    Edit Company Profile
                </el-button>
                <el-button class="functional-button" type="primary"
                           @click="$router.push('/profile/car-wash')">
                    Car Wash List
                </el-button>
            </div>
            <CompanyProfileInfoRow name="Contacts" style="padding-top: 20px">
                <template slot="value">
                    <el-table :data="[model]">
                        <el-table-column width="auto">
                            <template slot="header">
                                <div class="table-header">Email Address</div>
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
                                <div class="table-header">Phone</div>
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
            </CompanyProfileInfoRow>
        </template>
    </LoadingContainer>
</template>

<style scoped src="@/styles/companyProfile.css">
</style>

<style scoped>
    .divider {
        margin: 25px 0 30px 0 !important;
    }
</style>

<script>
    import { mapGetters } from "vuex";
    import LoadingContainer from "@/components/core/LoadingContainer";
    import CompanyProfileInfoRow from "@/components/company-profile/CompanyProfileInfoRow";
    import { GET_COMPANY_PROFILE_REQUEST } from "@/constants/actions";
    import { renderErrorNotificationMessage } from "@/extensions/utils";

    export default {
        name: "CompanyProfile",
        components: {
            LoadingContainer,
            CompanyProfileInfoRow
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