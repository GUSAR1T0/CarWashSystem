<template>
    <LoadingContainer :loading-state="loadingIsActive">
        <template slot="content">
            <el-card shadow="never">
                <div slot="header">
                    <div class="header">
                        <span><b style="font-size: 32px">{{ carWash.name }}</b></span>
                        <div style="margin-left: auto">
                            <el-button class="functional-button" type="primary" icon="el-icon-back"
                                       @click="$router.push(`/profile/car-wash`)"/>
                            <el-button class="functional-button" type="primary" icon="el-icon-circle-close"
                                       @click="openDeleteDialog(carWash)"/>
                        </div>
                    </div>
                </div>
                <el-tabs>
                    <el-tab-pane label="General Info">
                        <CarWashGeneralInfo :car-wash="carWash"/>
                    </el-tab-pane>
                    <el-tab-pane label="Services">
                        <CarWashServices :car-wash-id="carWash.id" :services="carWash.services" :editable="true"/>
                    </el-tab-pane>
                    <el-tab-pane label="Appointments">
                        <CarWashAppointments :car-wash-id="carWash.id" :appointments="appointments"/>
                    </el-tab-pane>
                </el-tabs>
                <ConfirmationDialog :dialog-status="deleteDialogStatus"
                                    :confirmation-text="getConfirmationTextOnDeleteCarWash"
                                    :cancel-click-action="cancelDeleteDialog"
                                    :submit-click-action="deleteCarWash"/>
            </el-card>
        </template>
    </LoadingContainer>
</template>

<style scoped src="@/styles/companyProfile.css">
</style>

<script>
    import LoadingContainer from "@/components/core/LoadingContainer";
    import ConfirmationDialog from "@/components/core/ConfirmationDialog";
    import CarWashGeneralInfo from "@/components/company-profile/CarWashGeneralInfo";
    import CarWashServices from "@/components/company-profile/CarWashServices";
    import CarWashAppointments from "@/components/company-profile/CarWashAppointments";
    import { DELETE_CAR_WASH_REQUEST, GET_HTTP_REQUEST, RESET_CAR_WASH_REQUEST } from "@/constants/actions";
    import { renderErrorNotificationMessage } from "@/extensions/utils";
    import { GET_APPOINTMENTS_TO_CAR_WASH_ENDPOINT, GET_CAR_WASH_ENDPOINT } from "@/constants/endpoints";
    import format from "string-format";

    export default {
        name: "CarWashView",
        components: {
            LoadingContainer,
            ConfirmationDialog,
            CarWashGeneralInfo,
            CarWashServices,
            CarWashAppointments
        },
        data() {
            return {
                loadingIsActive: true,
                deleteDialogStatus: {
                    visible: false,
                    item: null
                },
                carWash: {
                    workingHours: {
                        monday: {},
                        tuesday: {},
                        wednesday: {},
                        thursday: {},
                        friday: {},
                        saturday: {},
                        sunday: {}
                    },
                    services: []
                },
                appointments: []
            };
        },
        methods: {
            loadCarWash(id) {
                this.loadingIsActive = true;
                this.$store.dispatch(GET_HTTP_REQUEST, {
                    endpoint: format(`${GET_CAR_WASH_ENDPOINT}?s=true`, {carWashId: id})
                }).then(firstResponse => {
                    this.carWash = firstResponse.data;
                    this.$store.dispatch(GET_HTTP_REQUEST, {
                        endpoint: format(GET_APPOINTMENTS_TO_CAR_WASH_ENDPOINT, {carWashId: id})
                    }).then(secondResponse => {
                        this.loadingIsActive = false;
                        this.appointments = secondResponse.data;
                    }).catch(error => {
                        this.$notify.error({
                            title: "Failed to load appointments data",
                            duration: 10000,
                            message: renderErrorNotificationMessage(this.$createElement, error.response)
                        });
                    });
                }).catch(error => {
                    this.$notify.error({
                        title: "Failed to load car wash data",
                        duration: 10000,
                        message: renderErrorNotificationMessage(this.$createElement, error.response)
                    });
                });
            },
            openDeleteDialog(item) {
                this.deleteDialogStatus.visible = true;
                this.deleteDialogStatus.item = item;
            },
            cancelDeleteDialog() {
                this.deleteDialogStatus.visible = false;
                this.deleteDialogStatus.item = null;
            },
            getConfirmationTextOnDeleteCarWash() {
                return `Are you sure that you want to delete "${this.deleteDialogStatus.item.name}" car wash?`;
            },
            deleteCarWash(button) {
                button.loading = true;
                this.$store.dispatch(DELETE_CAR_WASH_REQUEST, this.deleteDialogStatus.item.id).then(data => {
                    button.loading = false;
                    this.deleteDialogStatus.visible = false;
                    this.$notify.info({
                        title: "Action is successful",
                        message: `Car wash "${data.name}" (ID: ${data.id}) was removed`
                    });
                }).catch(error => {
                    button.loading = false;
                    this.$notify.error({
                        title: "Failed to delete car wash",
                        duration: 10000,
                        message: renderErrorNotificationMessage(this.$createElement, error.response)
                    });
                });
            }
        },
        mounted() {
            this.$store.commit(RESET_CAR_WASH_REQUEST);
            this.loadCarWash(this.$route.params.id);
        },
        beforeRouteUpdate(to, from, next) {
            this.$store.commit(RESET_CAR_WASH_REQUEST);
            this.loadCarWash(to.params.id);
            next();
        }
    };
</script>