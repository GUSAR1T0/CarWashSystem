<template>
    <div>
        <el-button class="functional-button" type="primary" icon="el-icon-plus"
                   @click="addServicePriceRow" :disabled="isDisabledEditAndAddButtons">
            Add New Service
        </el-button>
        <el-table :data="services" style="padding-top: 20px">
            <el-table-column width="auto" header-align="center" align="center">
                <template slot="header">
                    <div class="table-header">Operations</div>
                </template>
                <template slot-scope="scope">
                    <el-button v-if="!scope.row.editable" type="primary" icon="el-icon-edit"
                               @click="editServicePriceRow(scope.row.id)" :disabled="isDisabledEditAndAddButtons"/>
                    <el-button v-else :ref="`storeButton-${carWashId}-${scope.row.id}`" type="primary"
                               icon="el-icon-success"
                               @click="storeServicePriceRow(scope.row.id)"/>
                    <el-button :ref="`deleteButton-${carWashId}-${scope.row.id}`" type="primary" icon="el-icon-error"
                               @click="deleteServicePriceRow(scope.row.id)"/>
                </template>
            </el-table-column>
            <el-table-column width="100px" header-align="center" align="center">
                <template slot="header">
                    <div class="table-header">Active</div>
                </template>
                <template slot-scope="scope">
                    <TagForBoolean v-if="!scope.row.editable" :value="scope.row.isAvailable"/>
                    <el-switch v-else v-model="scope.row.isAvailable"/>
                </template>
            </el-table-column>
            <el-table-column width="auto" header-align="center" align="center">
                <template slot="header">
                    <div class="table-header">Service Name</div>
                </template>
                <template slot-scope="scope">
                    <b v-if="!scope.row.editable" class="table-cell">{{ scope.row.serviceName }}</b>
                    <el-input v-else v-model="scope.row.serviceName" clearable/>
                </template>
            </el-table-column>
            <el-table-column width="auto" header-align="center" align="center">
                <template slot="header">
                    <div class="table-header">Description</div>
                </template>
                <template slot-scope="scope">
                    <div v-if="!scope.row.editable">
                        <div v-if="scope.row.description" class="table-cell">
                            {{ scope.row.description }}
                        </div>
                        <div v-else class="table-cell">—</div>
                    </div>
                    <el-input v-else type="textarea" :rows="3" v-model="scope.row.description"
                              clearable maxlength="1024" show-word-limit/>
                </template>
            </el-table-column>
            <el-table-column width="auto" header-align="center" align="center">
                <template slot="header">
                    <div class="table-header">Price</div>
                </template>
                <template slot-scope="scope">
                    <div v-if="!scope.row.editable" class="table-cell">{{ scope.row.price }}</div>
                    <el-input v-else v-model="scope.row.price" clearable/>
                </template>
            </el-table-column>
            <el-table-column width="auto" header-align="center" align="center">
                <template slot="header">
                    <div class="table-header">Duration</div>
                </template>
                <template slot-scope="scope">
                    <div v-if="!scope.row.editable" class="table-cell">{{ scope.row.duration }}</div>
                    <el-time-select v-else placeholder="Duration (minutes)" style="width: 100%;"
                                    v-model="scope.row.duration"
                                    :picker-options="{start: '00:00', step: '00:15', end: '12:00'}"/>
                </template>
            </el-table-column>
        </el-table>
    </div>
</template>

<style scoped src="@/styles/companyProfile.css">
</style>

<script>
    import TagForBoolean from "@/components/core/TagForBoolean";
    import {
        ADD_CAR_WASH_SERVICE_ENDPOINT,
        DELETE_CAR_WASH_SERVICE_ENDPOINT,
        GET_CAR_WASH_SERVICE_LIST_ENDPOINT,
        UPDATE_CAR_WASH_SERVICE_ENDPOINT
    } from "@/constants/endpoints";
    import { DELETE_HTTP_REQUEST, GET_HTTP_REQUEST, POST_HTTP_REQUEST, PUT_HTTP_REQUEST } from "@/constants/actions";
    import format from "string-format";
    import { renderErrorNotificationMessage } from "@/extensions/utils";

    export default {
        name: "CarWashServicePrices",
        props: {
            carWashId: Number,
            services: Array
        },
        components: {
            TagForBoolean
        },
        computed: {
            isDisabledEditAndAddButtons() {
                return this.services.filter(item => item.editable === true).length > 0;
            }
        },
        methods: {
            addServicePriceRow() {
                if (this.services.filter(item => item.editable === true).length === 0) {
                    this.services.push({id: 0, isAvailable: true, editable: true});
                }
            },
            editServicePriceRow(id) {
                if (this.services.filter(item => item.editable === true).length === 0) {
                    this.services = this.services.map(item => {
                        if (item.id === id) {
                            let updated = item;
                            updated.editable = true;
                            return updated;
                        }
                        return item;
                    });
                }
            },
            storeServicePriceRow(id) {
                let button = this.$refs[`storeButton-${this.carWashId}-${id}`];
                button.loading = true;
                let item = this.services.filter(item => item.id === id)[0];
                if (id === 0) {
                    this.$store.dispatch(POST_HTTP_REQUEST, {
                        endpoint: format(ADD_CAR_WASH_SERVICE_ENDPOINT, {
                            carWashId: this.carWashId
                        }),
                        data: {
                            serviceName: item.serviceName,
                            description: item.description,
                            price: parseFloat(item.price),
                            duration: item.duration,
                            isAvailable: item.isAvailable
                        }
                    }).then(firstResponse => {
                        this.$store.dispatch(GET_HTTP_REQUEST, {
                            endpoint: format(GET_CAR_WASH_SERVICE_LIST_ENDPOINT, {
                                carWashId: this.carWashId
                            })
                        }).then(secondResponse => {
                            button.loading = false;
                            this.services = secondResponse.data;
                            this.$notify.info({
                                title: "Action is successful",
                                message: `Car wash service "${firstResponse.data.serviceName}" (ID: ${firstResponse.data.id}) was added`
                            });
                        }).catch(error => {
                            button.loading = false;
                            this.$notify.error({
                                title: "Failed to load car wash services after add",
                                duration: 10000,
                                message: renderErrorNotificationMessage(this.$createElement, error.response)
                            });
                        });
                    }).catch(error => {
                        button.loading = false;
                        this.$notify.error({
                            title: "Failed to add car wash service",
                            duration: 10000,
                            message: renderErrorNotificationMessage(this.$createElement, error.response)
                        });
                    });
                } else {
                    this.$store.dispatch(PUT_HTTP_REQUEST, {
                        endpoint: format(UPDATE_CAR_WASH_SERVICE_ENDPOINT, {
                            carWashId: this.carWashId,
                            serviceId: id
                        }),
                        data: {
                            id: item.id,
                            serviceName: item.serviceName,
                            description: item.description,
                            price: parseFloat(item.price),
                            duration: item.duration,
                            isAvailable: item.isAvailable
                        }
                    }).then(firstResponse => {
                        this.$store.dispatch(GET_HTTP_REQUEST, {
                            endpoint: format(GET_CAR_WASH_SERVICE_LIST_ENDPOINT, {
                                carWashId: this.carWashId
                            })
                        }).then(secondResponse => {
                            button.loading = false;
                            this.services = secondResponse.data;
                            this.$notify.info({
                                title: "Action is successful",
                                message: `Car wash service "${firstResponse.data.serviceName}" (ID: ${firstResponse.data.id}) was updated`
                            });
                        }).catch(error => {
                            button.loading = false;
                            this.$notify.error({
                                title: "Failed to load car wash services after update",
                                duration: 10000,
                                message: renderErrorNotificationMessage(this.$createElement, error.response)
                            });
                        });
                    }).catch(error => {
                        button.loading = false;
                        this.$notify.error({
                            title: "Failed to update car wash service",
                            duration: 10000,
                            message: renderErrorNotificationMessage(this.$createElement, error.response)
                        });
                    });
                }
            },
            deleteServicePriceRow(id) {
                if (id === 0) {
                    this.services = this.services.filter(e => e.id !== 0);
                } else {
                    if (this.services.filter(e => e.id === id && e.editable === true).length > 0) {
                        this.services = this.services.map(item => {
                            if (item.id === id) {
                                let updated = item;
                                updated.editable = false;
                                return updated;
                            }
                            return item;
                        });
                    } else {
                        let button = this.$refs[`deleteButton-${this.carWashId}-${id}`];
                        button.loading = true;
                        this.$store.dispatch(DELETE_HTTP_REQUEST, {
                            endpoint: format(DELETE_CAR_WASH_SERVICE_ENDPOINT, {
                                carWashId: this.carWashId,
                                serviceId: id
                            })
                        }).then(firstResponse => {
                            this.$store.dispatch(GET_HTTP_REQUEST, {
                                endpoint: format(GET_CAR_WASH_SERVICE_LIST_ENDPOINT, {
                                    carWashId: this.carWashId
                                })
                            }).then(secondResponse => {
                                button.loading = false;
                                this.services = secondResponse.data;
                                this.$notify.info({
                                    title: "Action is successful",
                                    message: `Car wash service "${firstResponse.data.serviceName}" (ID: ${firstResponse.data.id}) was removed`
                                });
                            }).catch(error => {
                                button.loading = false;
                                this.$notify.error({
                                    title: "Failed to load car wash services after delete",
                                    duration: 10000,
                                    message: renderErrorNotificationMessage(this.$createElement, error.response)
                                });
                            });
                        }).catch(error => {
                            button.loading = false;
                            this.$notify.error({
                                title: "Failed to delete car wash service",
                                duration: 10000,
                                message: renderErrorNotificationMessage(this.$createElement, error.response)
                            });
                        });
                    }
                }
            }
        }
    };
</script>