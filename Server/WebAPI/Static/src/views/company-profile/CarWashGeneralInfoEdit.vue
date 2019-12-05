<template>
    <LoadingContainer :loading-state="loadingIsActive">
        <template slot="content">
            <b class="edit-page-title">{{ !isNew ? 'Edit' : 'Create' }} Car Wash</b>
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
                <el-row class="edit-field-element" type="flex" justify="center" align="middle">
                    <el-col :xs="24" :sm="20" :md="16" :lg="16" :xl="16">
                        <el-form-item prop="location" label="Location">
                            <el-autocomplete style="width: 100%" v-model="model.location"
                                             :fetch-suggestions="loadLocations" placeholder="Enter an address"/>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row class="edit-field-element" type="flex" justify="center" align="middle">
                    <el-col :xs="24" :sm="20" :md="16" :lg="16" :xl="16">
                        <el-form-item prop="coordinates" label="Coordinates">
                            <el-row type="flex" justify="center" align="middle" :gutter="20">
                                <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12">
                                    <el-input v-model="model.coordinateX" clearable></el-input>
                                </el-col>
                                <el-col :xs="12" :sm="12" :md="12" :lg="12" :xl="12">
                                    <el-input v-model="model.coordinateY" clearable></el-input>
                                </el-col>
                            </el-row>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row class="edit-field-element" type="flex" justify="center"
                        align="middle">
                    <el-col :xs="24" :sm="20" :md="16" :lg="16" :xl="16">
                        <el-form-item prop="description" label="Description">
                            <el-input type="textarea" :rows="3" v-model="model.description" clearable
                                      maxlength="1024" show-word-limit></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row class="edit-field-element" type="flex" justify="center" align="middle">
                    <el-col :xs="24" :sm="20" :md="16" :lg="16" :xl="16">
                        <el-form-item prop="workingHours" label="Working Hours">
                            <b style="font-size: 20px">Monday</b>
                            <el-row type="flex" justify="center" align="middle" :gutter="20" style="padding: 10px 0">
                                <el-col :xs="2" :sm="2" :md="2" :lg="2" :xl="2">
                                    <el-checkbox v-model="model.workingHours.monday.isChecked"/>
                                </el-col>
                                <el-col :xs="8" :sm="8" :md="8" :lg="8" :xl="8">
                                    <el-time-select placeholder="Start Time" style="width: 100%;"
                                                    v-model="model.workingHours.monday.startTime"
                                                    :disabled="!model.workingHours.monday.isChecked"
                                                    :picker-options="timeOptions"/>
                                </el-col>
                                <el-col :xs="8" :sm="8" :md="8" :lg="8" :xl="8">
                                    <el-time-select placeholder="Stop Time" style="width: 100%;"
                                                    v-model="model.workingHours.monday.stopTime"
                                                    :disabled="!model.workingHours.monday.isChecked"
                                                    :picker-options="timeOptions"/>
                                </el-col>
                            </el-row>
                            <b style="font-size: 20px">Tuesday</b>
                            <el-row type="flex" justify="center" align="middle" :gutter="20" style="padding: 10px 0">
                                <el-col :xs="2" :sm="2" :md="2" :lg="2" :xl="2">
                                    <el-checkbox v-model="model.workingHours.tuesday.isChecked"/>
                                </el-col>
                                <el-col :xs="8" :sm="8" :md="8" :lg="8" :xl="8">
                                    <el-time-select placeholder="Start Time" style="width: 100%;"
                                                    v-model="model.workingHours.tuesday.startTime"
                                                    :disabled="!model.workingHours.tuesday.isChecked"
                                                    :picker-options="timeOptions"/>
                                </el-col>
                                <el-col :xs="8" :sm="8" :md="8" :lg="8" :xl="8">
                                    <el-time-select placeholder="Stop Time" style="width: 100%;"
                                                    v-model="model.workingHours.tuesday.stopTime"
                                                    :disabled="!model.workingHours.tuesday.isChecked"
                                                    :picker-options="timeOptions"/>
                                </el-col>
                            </el-row>
                            <b style="font-size: 20px">Wednesday</b>
                            <el-row type="flex" justify="center" align="middle" :gutter="20" style="padding: 10px 0">
                                <el-col :xs="2" :sm="2" :md="2" :lg="2" :xl="2">
                                    <el-checkbox v-model="model.workingHours.wednesday.isChecked"/>
                                </el-col>
                                <el-col :xs="8" :sm="8" :md="8" :lg="8" :xl="8">
                                    <el-time-select placeholder="Start Time" style="width: 100%;"
                                                    v-model="model.workingHours.wednesday.startTime"
                                                    :disabled="!model.workingHours.wednesday.isChecked"
                                                    :picker-options="timeOptions"/>
                                </el-col>
                                <el-col :xs="8" :sm="8" :md="8" :lg="8" :xl="8">
                                    <el-time-select placeholder="Stop Time" style="width: 100%;"
                                                    v-model="model.workingHours.wednesday.stopTime"
                                                    :disabled="!model.workingHours.wednesday.isChecked"
                                                    :picker-options="timeOptions"/>
                                </el-col>
                            </el-row>
                            <b style="font-size: 20px">Thursday</b>
                            <el-row type="flex" justify="center" align="middle" :gutter="20" style="padding: 10px 0">
                                <el-col :xs="2" :sm="2" :md="2" :lg="2" :xl="2">
                                    <el-checkbox v-model="model.workingHours.thursday.isChecked"/>
                                </el-col>
                                <el-col :xs="8" :sm="8" :md="8" :lg="8" :xl="8">
                                    <el-time-select placeholder="Start Time" style="width: 100%;"
                                                    v-model="model.workingHours.thursday.startTime"
                                                    :disabled="!model.workingHours.thursday.isChecked"
                                                    :picker-options="timeOptions"/>
                                </el-col>
                                <el-col :xs="8" :sm="8" :md="8" :lg="8" :xl="8">
                                    <el-time-select placeholder="Stop Time" style="width: 100%;"
                                                    v-model="model.workingHours.thursday.stopTime"
                                                    :disabled="!model.workingHours.thursday.isChecked"
                                                    :picker-options="timeOptions"/>
                                </el-col>
                            </el-row>
                            <b style="font-size: 20px">Friday</b>
                            <el-row type="flex" justify="center" align="middle" :gutter="20" style="padding: 10px 0">
                                <el-col :xs="2" :sm="2" :md="2" :lg="2" :xl="2">
                                    <el-checkbox v-model="model.workingHours.friday.isChecked"/>
                                </el-col>
                                <el-col :xs="8" :sm="8" :md="8" :lg="8" :xl="8">
                                    <el-time-select placeholder="Start Time" style="width: 100%;"
                                                    v-model="model.workingHours.friday.startTime"
                                                    :disabled="!model.workingHours.friday.isChecked"
                                                    :picker-options="timeOptions"/>
                                </el-col>
                                <el-col :xs="8" :sm="8" :md="8" :lg="8" :xl="8">
                                    <el-time-select placeholder="Stop Time" style="width: 100%;"
                                                    v-model="model.workingHours.friday.stopTime"
                                                    :disabled="!model.workingHours.friday.isChecked"
                                                    :picker-options="timeOptions"/>
                                </el-col>
                            </el-row>
                            <b style="font-size: 20px">Saturday</b>
                            <el-row type="flex" justify="center" align="middle" :gutter="20" style="padding: 10px 0">
                                <el-col :xs="2" :sm="2" :md="2" :lg="2" :xl="2">
                                    <el-checkbox v-model="model.workingHours.saturday.isChecked"/>
                                </el-col>
                                <el-col :xs="8" :sm="8" :md="8" :lg="8" :xl="8">
                                    <el-time-select placeholder="Start Time" style="width: 100%;"
                                                    v-model="model.workingHours.saturday.startTime"
                                                    :disabled="!model.workingHours.saturday.isChecked"
                                                    :picker-options="timeOptions"/>
                                </el-col>
                                <el-col :xs="8" :sm="8" :md="8" :lg="8" :xl="8">
                                    <el-time-select placeholder="Stop Time" style="width: 100%;"
                                                    v-model="model.workingHours.saturday.stopTime"
                                                    :disabled="!model.workingHours.saturday.isChecked"
                                                    :picker-options="timeOptions"/>
                                </el-col>
                            </el-row>
                            <b style="font-size: 20px">Sunday</b>
                            <el-row type="flex" justify="center" align="middle" :gutter="20" style="padding: 10px 0">
                                <el-col :xs="2" :sm="2" :md="2" :lg="2" :xl="2">
                                    <el-checkbox v-model="model.workingHours.sunday.isChecked"/>
                                </el-col>
                                <el-col :xs="8" :sm="8" :md="8" :lg="8" :xl="8">
                                    <el-time-select placeholder="Start Time" style="width: 100%;"
                                                    v-model="model.workingHours.sunday.startTime"
                                                    :disabled="!model.workingHours.sunday.isChecked"
                                                    :picker-options="timeOptions"/>
                                </el-col>
                                <el-col :xs="8" :sm="8" :md="8" :lg="8" :xl="8">
                                    <el-time-select placeholder="Stop Time" style="width: 100%;"
                                                    v-model="model.workingHours.sunday.stopTime"
                                                    :disabled="!model.workingHours.sunday.isChecked"
                                                    :picker-options="timeOptions"/>
                                </el-col>
                            </el-row>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row class="edit-field-element" type="flex" justify="center" align="middle">
                    <el-col :xs="24" :sm="20" :md="16" :lg="16" :xl="16">
                        <el-form-item prop="advantages" label="Advantages">
                            <el-checkbox label="Cafe" v-model="model.hasCafe"/>
                            <el-checkbox label="Rest Zone" v-model="model.hasRestZone"/>
                            <el-checkbox label="Parking" v-model="model.hasParking"/>
                            <el-checkbox label="WC" v-model="model.hasWC"/>
                            <el-checkbox label="Card Payment" v-model="model.hasCardPayment"/>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row class="auth-field-element" type="flex" justify="center" align="middle">
                    <el-col :xs="24" :sm="20" :md="16" :lg="16" :xl="16">
                        <el-form-item>
                            <el-row type="flex" justify="center" align="middle" :gutter="20">
                                <el-col :span="12">
                                    <el-button type="primary" plain style="width: 100%"
                                               @click="$router.push(`/profile/car-wash${!isNew ? '/' + model.id : ''}`)">
                                        <b style="font-size: 20px">Go to {{ !isNew ? 'Car Wash' : 'List' }}</b>
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
    import { ADD_CAR_WASH_REQUEST, GET_CAR_WASH_REQUEST, UPDATE_CAR_WASH_REQUEST } from "@/constants/actions";
    import { mapGetters } from "vuex";
    import LoadingContainer from "@/components/core/LoadingContainer";
    import Dadata from "dadata-suggestions";
    import { renderErrorNotificationMessage } from "@/extensions/utils";

    export default {
        name: "CarWashGeneralInfoEdit",
        components: {
            LoadingContainer
        },
        data() {
            return {
                loadingIsActive: true,
                isNew: true,
                model: {
                    workingHours: {
                        monday: {},
                        tuesday: {},
                        wednesday: {},
                        thursday: {},
                        friday: {},
                        saturday: {},
                        sunday: {}
                    }
                },
                timeOptions: {start: "00:00", step: "00:15", end: "23:45"},
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
                    ],
                    location: [
                        {required: true, message: "Please, input the location", trigger: "change"}
                    ],
                    coordinates: [
                        {
                            required: true, validator: (rule, value, callback) => {
                                if (this.model.coordinateX === "" || this.model.coordinateY === "") {
                                    callback(new Error("Please, input the location"));
                                } else {
                                    callback();
                                }
                            }, trigger: "change"
                        }
                    ]
                },
            };
        },
        computed: {
            ...mapGetters([
                "getCarWashForm",
                "getDadataApiKey"
            ])
        },
        methods: {
            loadCarWashData(id) {
                this.loadingIsActive = true;
                if (id !== "new") {
                    this.isNew = false;
                    this.$store.dispatch(GET_CAR_WASH_REQUEST, id).then(() => {
                        this.loadingIsActive = false;
                        this.model = this.getCarWashForm;
                    }).catch(error => {
                        this.$notify.error({
                            title: "Failed to load car wash data",
                            duration: 10000,
                            message: renderErrorNotificationMessage(this.$createElement, error.response)
                        });
                    });
                } else {
                    this.model = this.getCarWashForm;
                    this.loadingIsActive = false;
                }
            },
            loadLocations(query, callback) {
                if (query !== "") {
                    let dadata = new Dadata(this.getDadataApiKey);
                    dadata.address({
                        query: query,
                        count: 10
                    }).then(data => {
                        callback(data.suggestions);
                    }).catch(error => {
                        this.$notify.error({
                            title: "Failed to load suggestions for location",
                            duration: 10000,
                            message: renderErrorNotificationMessage(this.$createElement, error.response)
                        });
                    });
                } else {
                    callback([]);
                }
            },
            submitForm(formName) {
                this.$refs.formButton.loading = true;
                this.$refs[formName].validate((valid) => {
                    if (!valid) {
                        this.$refs.formButton.loading = false;
                        return false;
                    }

                    if (this.isNew) {
                        this.$store.dispatch(ADD_CAR_WASH_REQUEST, this.model).then(response => {
                            this.$refs.formButton.loading = false;
                            this.$router.push(`/profile/car-wash/${response.id}`);
                            this.$notify.info({
                                title: "Action is successful",
                                message: `Car wash "${response.name}" (ID: ${response.id}) was added`
                            });
                        }).catch(error => {
                            this.$refs.formButton.loading = false;
                            this.$notify.error({
                                title: "Failed to add car wash",
                                duration: 10000,
                                message: renderErrorNotificationMessage(this.$createElement, error.response)
                            });
                        });
                    } else {
                        this.$store.dispatch(UPDATE_CAR_WASH_REQUEST, this.model).then(response => {
                            this.$refs.formButton.loading = false;
                            this.$router.push(`/profile/car-wash/${response.id}`);
                            this.$notify.info({
                                title: "Action is successful",
                                message: `Car wash "${response.name}" (ID: ${response.id}) was updated`
                            });
                        }).catch(error => {
                            this.$refs.formButton.loading = false;
                            this.$notify.error({
                                title: "Failed to update car wash",
                                duration: 10000,
                                message: renderErrorNotificationMessage(this.$createElement, error.response)
                            });
                        });
                    }
                });
            }
        },
        mounted() {
            this.loadCarWashData(this.$route.params.id);
        },
        beforeRouteUpdate(to, from, next) {
            this.loadCarWashData(to.params.id);
            next();
        }
    };
</script>