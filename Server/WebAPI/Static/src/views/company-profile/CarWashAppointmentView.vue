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
                            <CompanyProfileInfoRow name="Client" style="padding: 20px 0">
                                <template slot="value">
                                    <div style="padding-bottom: 20px">
                                        {{ appointment.fullName }}
                                    </div>
                                    <el-table :data="[appointment]" style="width: 90%">
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
                                                <div v-else class="table-cell">—</div>
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
                                                <div v-else class="table-cell">—</div>
                                            </template>
                                        </el-table-column>
                                    </el-table>
                                </template>
                            </CompanyProfileInfoRow>
                            <CompanyProfileInfoRow name="Car"
                                                   :value="`${getCarBrandModelName(appointment.carModelId)} (${appointment.carGovernmentPlate})`"
                                                   style="padding: 20px 0"/>
                        </el-col>
                        <el-col span="12">
                            <CompanyProfileInfoRow name="Status"
                                                   style="padding: 20px 0">
                                <template slot="value">
                                    <el-button class="functional-button" type="info">
                                        {{ getStatusName(appointment.status) }}
                                    </el-button>

                                    <!-- B: OPENED -->
                                    <el-button v-if="appointment.status === 1" class="functional-button" type="primary"
                                               ref="approveButton" @click="changeStatus(2)">
                                        Approve
                                    </el-button>
                                    <el-button v-if="appointment.status === 1" class="functional-button" type="primary"
                                               @click="setNewStartTimeClickAction">
                                        Set New Start Time
                                    </el-button>
                                    <!-- E: OPENED -->

                                    <!-- B: APPROVED -->
                                    <el-button v-if="appointment.status === 2" class="functional-button" type="primary"
                                               ref="inProgressButton" @click="changeStatus(4)">
                                        In Progress
                                    </el-button>
                                    <!-- E: APPROVED -->

                                    <!-- B: OPENED / APPROVED / REQUEST IS REQUIRED -->
                                    <el-button
                                            v-if="appointment.status === 1 || appointment.status === 2 || appointment.status === 3"
                                            class="functional-button" type="warning"
                                            @click="setCancelStatusClickAction">
                                        Cancel
                                    </el-button>
                                    <!-- E: OPENED / APPROVED / REQUEST IS REQUIRED -->

                                    <!-- B: IN PROGRESS -->
                                    <el-button v-if="appointment.status === 4" class="functional-button" type="success"
                                               ref="processedButton" @click="changeStatus(5)">
                                        Complete
                                    </el-button>
                                    <el-button v-if="appointment.status === 4" class="functional-button" type="danger"
                                               ref="incidentButton" @click="changeStatus(6)">
                                        Incident
                                    </el-button>
                                    <!-- E: IN PROGRESS -->

                                    <el-dialog :visible.sync="dialogNewStartTimeStatus.visible" width="75%"
                                               style="text-align: center">
                                        <span slot="title" class="modal-title">Set New Start Time</span>
                                        <h1 style="font-size: 32px">
                                            Choose new start time:
                                        </h1>

                                        <el-row style="padding-bottom: 40px" :gutter="0">
                                            <el-col :span="12">
                                                <el-date-picker
                                                        v-model="dialogNewStartTimeStatus.startDate"
                                                        type="date"
                                                        placeholder="Select date"
                                                        :picker-options="dateOptions"
                                                        value-format="yyyy-MM-dd"
                                                        style="width: 100%">
                                                </el-date-picker>
                                            </el-col>
                                            <el-col :span="12">
                                                <el-time-select
                                                        v-model="dialogNewStartTimeStatus.startTime"
                                                        placeholder="Select time"
                                                        :picker-options="timeOptions"
                                                        style="width: 100%">
                                                </el-time-select>
                                            </el-col>
                                        </el-row>

                                        <el-row type="flex" justify="center" align="middle" :gutter="20">
                                            <el-col :span="12">
                                                <el-button type="primary" plain @click="cancelNewStartTimeClickAction"
                                                           style="width: 100%;">
                                                    <b style="font-size: 20px">Cancel</b>
                                                </el-button>
                                            </el-col>
                                            <el-col :span="12">
                                                <el-button type="danger" plain ref="dialogNewStartTimeButton"
                                                           @click="submitNewStartTimeClickAction"
                                                           style="width: 100%;">
                                                    <b style="font-size: 20px">Submit</b>
                                                </el-button>
                                            </el-col>
                                        </el-row>
                                    </el-dialog>

                                    <el-dialog :visible.sync="dialogCancelStatus.visible" width="75%"
                                               style="text-align: center">
                                        <span slot="title" class="modal-title">Cancel Appointment</span>
                                        <h1 style="font-size: 32px">
                                            Write some comment before cancel:
                                        </h1>

                                        <el-input v-model="dialogCancelStatus.comment" type="textarea" :rows="3"
                                                  clearable style="padding-bottom: 40px"/>

                                        <el-row type="flex" justify="center" align="middle" :gutter="20">
                                            <el-col :span="12">
                                                <el-button type="primary" plain @click="cancelCancelStatusClickAction"
                                                           style="width: 100%;">
                                                    <b style="font-size: 20px">Cancel</b>
                                                </el-button>
                                            </el-col>
                                            <el-col :span="12">
                                                <el-button type="danger" plain ref="dialogCancelStatusButton"
                                                           @click="submitCancelStatusClickAction"
                                                           style="width: 100%;">
                                                    <b style="font-size: 20px">Submit</b>
                                                </el-button>
                                            </el-col>
                                        </el-row>
                                    </el-dialog>
                                </template>
                            </CompanyProfileInfoRow>
                            <CompanyProfileInfoRow name="Start Time" style="padding: 20px 0">
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
                <el-divider/>
                <div style="padding: 20px 0">
                    <CompanyProfileInfoRow name="Chosen services">
                        <template slot="value">
                            <CarWashServices :car-wash-id="carWashId" :services="appointment.carWashServices"
                                             :editable="false" style="padding-top: 0"/>
                            <el-row style="padding-top: 20px">
                                <el-col span="12">
                                    <CompanyProfileInfoRow name="Total Price" :value="appointment.totalPrice"/>
                                </el-col>
                                <el-col span="12">
                                    <CompanyProfileInfoRow name="Total Duration" :value="appointment.totalDuration"/>
                                </el-col>
                            </el-row>
                        </template>
                    </CompanyProfileInfoRow>
                </div>
                <el-divider/>
                <div style="padding: 20px 0">
                    <CompanyProfileInfoRow name="History">
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
    import { GET_HTTP_REQUEST, PUT_HTTP_REQUEST } from "@/constants/actions";
    import format from "string-format";
    import {
        APPROVE_APPOINTMENT_TO_CAR_WASH_ENDPOINT,
        CANCEL_APPOINTMENT_TO_CAR_WASH_ENDPOINT,
        GET_APPOINTMENT_TO_CAR_WASH_ENDPOINT,
        IN_PROGRESS_APPOINTMENT_TO_CAR_WASH_ENDPOINT, INCIDENT_APPOINTMENT_TO_CAR_WASH_ENDPOINT,
        PROCESSED_APPOINTMENT_TO_CAR_WASH_ENDPOINT,
        REQUIRE_RESPONSE_APPOINTMENT_TO_CAR_WASH_ENDPOINT
    } from "@/constants/endpoints";

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
                },
                dialogNewStartTimeStatus: {
                    visible: false,
                    startDate: null,
                    startTime: null
                },
                dialogCancelStatus: {
                    visible: false,
                    comment: ""
                },
                dateOptions: {
                    disabledDate(time) {
                        return time.getTime() < Date.now();
                    }
                },
                timeOptions: {start: "00:00", step: "00:15", end: "23:45"}
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
                }).then(response => {
                    this.appointment = response.data;
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
            },
            changeStatus(statusId) {
                let button, endpoint;

                if (statusId === 2) {
                    button = this.$refs.approveButton;
                    endpoint = APPROVE_APPOINTMENT_TO_CAR_WASH_ENDPOINT;
                }

                if (statusId === 4) {
                    button = this.$refs.inProgressButton;
                    endpoint = IN_PROGRESS_APPOINTMENT_TO_CAR_WASH_ENDPOINT;
                }

                if (statusId === 5) {
                    button = this.$refs.processedButton;
                    endpoint = PROCESSED_APPOINTMENT_TO_CAR_WASH_ENDPOINT;
                }

                if (statusId === 6) {
                    button = this.$refs.incidentButton;
                    endpoint = INCIDENT_APPOINTMENT_TO_CAR_WASH_ENDPOINT;
                }

                button.loading = true;
                this.$store.dispatch(PUT_HTTP_REQUEST, {
                    endpoint: format(endpoint, {
                        carWashId: this.carWashId,
                        appointmentId: this.appointment.id
                    })
                }).then(() => {
                    button.loading = false;
                    this.loadAppointment(this.appointment.id);
                }).catch(error => {
                    button.loading = false;
                    this.$notify.error({
                        title: "Failed to change status",
                        duration: 10000,
                        message: renderErrorNotificationMessage(this.$createElement, error.response)
                    });
                });
            },
            setNewStartTimeClickAction() {
                this.dialogNewStartTimeStatus.startDate = this.appointment.startDate;
                this.dialogNewStartTimeStatus.startTime = this.appointment.requestedStartTime;
                this.dialogNewStartTimeStatus.visible = true;
            },
            cancelNewStartTimeClickAction() {
                this.dialogNewStartTimeStatus.visible = false;
            },
            submitNewStartTimeClickAction() {
                this.$refs.dialogNewStartTimeButton.loading = true;
                this.$store.dispatch(PUT_HTTP_REQUEST, {
                    endpoint: format(REQUIRE_RESPONSE_APPOINTMENT_TO_CAR_WASH_ENDPOINT, {
                        carWashId: this.carWashId,
                        appointmentId: this.appointment.id
                    }),
                    data: {
                        startTime: this.dialogNewStartTimeStatus.startDate + "T" + this.dialogNewStartTimeStatus.startTime + ":00"
                    }
                }).then(() => {
                    this.$refs.dialogNewStartTimeButton.loading = false;
                    this.dialogNewStartTimeStatus.visible = false;
                    this.loadAppointment(this.appointment.id);
                }).catch(error => {
                    this.$refs.dialogNewStartTimeButton.loading = false;
                    this.$notify.error({
                        title: "Failed to set new start time",
                        duration: 10000,
                        message: renderErrorNotificationMessage(this.$createElement, error.response)
                    });
                });
            },
            setCancelStatusClickAction() {
                this.dialogCancelStatus.comment = "";
                this.dialogCancelStatus.visible = true;
            },
            cancelCancelStatusClickAction() {
                this.dialogCancelStatus.visible = false;
            },
            submitCancelStatusClickAction() {
                this.$refs.dialogCancelStatusButton.loading = true;
                this.$store.dispatch(PUT_HTTP_REQUEST, {
                    endpoint: format(CANCEL_APPOINTMENT_TO_CAR_WASH_ENDPOINT, {
                        carWashId: this.carWashId,
                        appointmentId: this.appointment.id
                    }),
                    data: {
                        comment: this.dialogCancelStatus.comment
                    }
                }).then(() => {
                    this.$refs.dialogCancelStatusButton.loading = false;
                    this.dialogCancelStatus.visible = false;
                    this.loadAppointment(this.appointment.id);
                }).catch(error => {
                    this.$refs.dialogCancelStatusButton.loading = false;
                    this.$notify.error({
                        title: "Failed to cancel appointment",
                        duration: 10000,
                        message: renderErrorNotificationMessage(this.$createElement, error.response)
                    });
                });
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