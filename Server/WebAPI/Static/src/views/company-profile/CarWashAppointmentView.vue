<template>
    <LoadingContainer :loading-state="loadingIsActive">
        <template slot="content">
            <el-card shadow="never">
                <div slot="header">
                    <div class="header">
                        <span><b style="font-size: 32px">Appointment Info</b></span>
                        <div style="margin-left: auto">
                            <el-button class="functional-button" type="primary" icon="el-icon-back"
                                       @click="$router.push(`/profile/car-wash/${carWashId}`)"/>
                        </div>
                    </div>
                </div>
                <div>
                    <el-row>
                        <el-col span="12">
                            <CompanyProfileInfoRow name="Client" :value="appointment.fullName" style="padding: 20px 0"/>
                            <CompanyProfileInfoRow name="Car"
                                                   :value="`${getCarBrandModelName(appointment.carModelId)} (${appointment.carGovernmentPlate})`"
                                                   style="padding: 20px 0"/>
                        </el-col>
                        <el-col span="12">
                            <CompanyProfileInfoRow name="Status"
                                                   :value="getStatusName(appointment.status)"
                                                   style="padding: 20px 0"/>
                            <CompanyProfileInfoRow name="Start Time"
                                                   style="padding: 20px 0">
                                <template slot="value">
                                    <el-row type="flex" justify="center">
                                        <el-col span="12" style="text-align: center">
                                            <b style="font-size: 16px">Requested</b>
                                            <div style="text-align: center; padding-top: 20px;">
                                                <div>{{ appointment.requestedStartDate }}</div>
                                                <div>{{ appointment.requestedStartTime }}</div>
                                            </div>
                                        </el-col>
                                        <el-col span="12" style="text-align: center">
                                            <b style="font-size: 16px">Approved</b>
                                            <div v-if="appointment.approvedStartDate && appointment.approvedStartTime"
                                                 style="text-align: center; padding-top: 20px;">
                                                <div>{{ appointment.approvedStartDate }}</div>
                                                <div>{{ appointment.approvedStartTime }}</div>
                                            </div>
                                            <div v-else style="text-align: center; padding-top: 20px;">—</div>
                                        </el-col>
                                    </el-row>
                                </template>
                            </CompanyProfileInfoRow>
                        </el-col>
                    </el-row>
                </div>
                <div>
                    <CompanyProfileInfoRow name="Chosen services" style="padding-top: 20px">
                        <template slot="value">
                            <CarWashServices :car-wash-id="carWashId" :services="appointment.carWashServices"
                                             :editable="false" style="padding-top: 0"/>
                        </template>
                    </CompanyProfileInfoRow>
                </div>
                <div>
                    <CompanyProfileInfoRow name="History" style="padding-top: 40px">
                        <template slot="value">
                            <div v-if="appointment.history.length > 0" style="padding-top: 20px">
                                <el-timeline>
                                    <el-timeline-item :timestamp="record.timestamp" placement="top"
                                                      v-for="record in appointment.history" :key="record">
                                        <el-card>
                                            <b>{{ record.action }}</b>
                                        </el-card>
                                    </el-timeline-item>
                                </el-timeline>
                            </div>
                            <div v-else>—</div>
                        </template>
                    </CompanyProfileInfoRow>
                </div>
            </el-card>
        </template>
    </LoadingContainer>
</template>

<style scoped src="@/styles/companyProfile.css">
</style>

<script>
    import { mapGetters } from "vuex";
    import {
        getAppointmentStatusModels,
        getCarBrandModelName,
        renderErrorNotificationMessage
    } from "@/extensions/utils";
    import LoadingContainer from "@/components/core/LoadingContainer";
    import CompanyProfileInfoRow from "@/components/company-profile/CompanyProfileInfoRow";
    import CarWashServices from "@/components/company-profile/CarWashServices";
    import { GET_HTTP_REQUEST } from "@/constants/actions";
    import format from "string-format";
    import { GET_APPOINTMENT_TO_CAR_WASH_ENDPOINT } from "@/constants/endpoints";

    export default {
        name: "CarWashAppointmentView",
        components: {
            LoadingContainer,
            CompanyProfileInfoRow,
            CarWashServices
        },
        data() {
            return {
                loadingIsActive: true,
                carWashId: 0,
                appointment: {
                    carWashServices: [],
                    history: []
                }
            };
        },
        computed: {
            ...mapGetters([
                "getCarBrandModelsModels",
                "getAppointmentStatusModels"
            ])
        },
        methods: {
            loadAppointment(id) {
                this.loadingIsActive = true;
                this.$store.dispatch(GET_HTTP_REQUEST, {
                    endpoint: format(GET_APPOINTMENT_TO_CAR_WASH_ENDPOINT, {
                        carWashId: this.carWashId,
                        appointmentId: id
                    })
                }).then(firstResponse => {
                    this.appointment = firstResponse.data;
                    this.loadingIsActive = false;
                }).catch(error => {
                    this.$notify.error({
                        title: "Failed to load appointment data",
                        duration: 10000,
                        message: renderErrorNotificationMessage(this.$createElement, error.response)
                    });
                });
            },
            getCarBrandModelName(carModelId) {
                return getCarBrandModelName(this.getCarBrandModelsModels, carModelId);
            },
            getStatusName(statusId) {
                return getAppointmentStatusModels(this.getAppointmentStatusModels, statusId);
            }
        },
        mounted() {
            this.carWashId = this.$route.params.id;
            this.loadAppointment(this.$route.params.appointmentId);
        },
        beforeRouteUpdate(to, from, next) {
            this.carWashId = to.params.id;
            this.loadAppointment(to.params.appointmentId);
            next();
        }
    };
</script>