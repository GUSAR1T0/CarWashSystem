<template>
    <LoadingContainer :loading-state="loadingIsActive">
        <template slot="content">
            <div class="header first-line">
                <span style="font-size: 24px">Total: <b style="font-size: 28px">{{ getCarWashList.length }}</b></span>
                <div style="margin-left: auto">
                    <el-button class="functional-button" type="primary" icon="el-icon-circle-plus-outline"
                               @click="$router.push('/profile/car-wash/new')"/>
                </div>
            </div>
            <el-card shadow="never" v-for="(carWash, index) in getCarWashList" v-bind:key="carWash.id"
                     :style="getStyleOfCard(index, getCarWashList.length)">
                <div slot="header">
                    <div class="header">
                        <span><b style="font-size: 32px">{{ carWash.name }}</b></span>
                        <div style="margin-left: auto">
                            <el-button class="functional-button" type="primary" icon="el-icon-circle-close"
                                       @click="openDeleteDialog(carWash)"/>
                        </div>
                    </div>
                </div>
                <el-collapse v-model="activeCollapseItems">
                    <el-collapse-item title="General Info" name="generalInfo">
                        <CarWashGeneralInfo :car-wash="carWash"/>
                    </el-collapse-item>
                    <el-collapse-item title="Service Prices" name="servicePrices">
                        <CarWashServices :car-wash-id="carWash.id" :services="carWash.services"/>
                    </el-collapse-item>
                </el-collapse>
                <ConfirmationDialog :dialog-status="deleteDialogStatus"
                                    :confirmation-text="getConfirmationTextOnDeleteCarWash"
                                    :cancel-click-action="cancelDeleteDialog"
                                    :submit-click-action="deleteCarWash"/>
            </el-card>
            <div v-if="!getCarWashList || getCarWashList.length === 0">
                The company doesn't have any car wash
            </div>
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
    import { mapGetters } from "vuex";
    import { DELETE_CAR_WASH_REQUEST, GET_CAR_WASH_LIST_REQUEST, RESET_CAR_WASH_REQUEST } from "@/constants/actions";
    import { renderErrorNotificationMessage } from "@/extensions/utils";

    export default {
        name: "CarWashList",
        components: {
            LoadingContainer,
            ConfirmationDialog,
            CarWashGeneralInfo,
            CarWashServices
        },
        data() {
            return {
                loadingIsActive: true,
                activeCollapseItems: [ "" ],
                deleteDialogStatus: {
                    visible: false,
                    item: null
                }
            };
        },
        computed: {
            ...mapGetters([
                "getCarWashList"
            ])
        },
        methods: {
            loadCarWashList() {
                this.loadingIsActive = true;
                this.$store.dispatch(GET_CAR_WASH_LIST_REQUEST).then(() => {
                    this.loadingIsActive = false;
                }).catch(error => {
                    this.loadingIsActive = false;
                    this.$notify.error({
                        title: "Failed to load company profile",
                        duration: 10000,
                        message: renderErrorNotificationMessage(this.$createElement, error.response)
                    });
                });
            },
            getStyleOfCard(index, max) {
                if (max > 2) {
                    if (index === 0) {
                        return "margin-bottom: 35px";
                    } else if (index === max - 1) {
                        return "margin-top: 35px";
                    } else {
                        return "margin: 35px 0";
                    }
                } else if (max === 2 && index === 0) {
                    return "margin-bottom: 35px";
                }
                return "";
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
                    this.deleteDialogStatus.visible = false;
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
            this.loadCarWashList();
        },
        beforeRouteUpdate(to, from, next) {
            this.$store.commit(RESET_CAR_WASH_REQUEST);
            this.loadCarWashList();
            next();
        }
    };
</script>