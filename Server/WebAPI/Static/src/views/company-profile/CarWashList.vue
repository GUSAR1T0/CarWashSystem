<template>
    <LoadingContainer :loading-state="loadingIsActive">
        <template slot="content">
            <div class="header first-line">
                <span style="font-size: 24px">Total: <b style="font-size: 28px">{{ list.length }}</b></span>
                <div style="margin-left: auto">
                    <el-button class="functional-button" type="primary" icon="el-icon-circle-plus-outline"
                               @click="$router.push('/profile/car-wash/edit/new')"/>
                </div>
            </div>
            <el-table :data="list">
                <el-table-column width="200px" header-align="center" align="center">
                    <template slot="header">
                        <div class="table-header">Operations</div>
                    </template>
                    <template slot-scope="scope">
                        <el-button class="functional-button" type="primary" icon="el-icon-right"
                                   @click="$router.push(`/profile/car-wash/${scope.row.id}`)"/>
                        <el-button class="functional-button" type="primary" icon="el-icon-circle-close"
                                   @click="openDeleteDialog(scope.row)"/>
                        <ConfirmationDialog :dialog-status="deleteDialogStatus"
                                            :confirmation-text="getConfirmationTextOnDeleteCarWash"
                                            :cancel-click-action="cancelDeleteDialog"
                                            :submit-click-action="deleteCarWash"/>
                    </template>
                </el-table-column>
                <el-table-column width="auto" header-align="center" align="center">
                    <template slot="header">
                        <div class="table-header">Name</div>
                    </template>
                    <template slot-scope="scope">
                        <b style="font-size: 28px">{{ scope.row.name }}</b>
                    </template>
                </el-table-column>
                <el-table-column width="auto" header-align="center" align="center">
                    <template slot="header">
                        <div class="table-header">Location</div>
                    </template>
                    <template slot-scope="scope">
                        <div style="font-size: 20px">{{ scope.row.location }}</div>
                    </template>
                </el-table-column>
            </el-table>
        </template>
    </LoadingContainer>
</template>

<style scoped src="@/styles/companyProfile.css">
</style>

<script>
    import LoadingContainer from "@/components/core/LoadingContainer";
    import ConfirmationDialog from "@/components/core/ConfirmationDialog";
    import { mapGetters } from "vuex";
    import { DELETE_CAR_WASH_REQUEST, GET_CAR_WASH_LIST_REQUEST } from "@/constants/actions";
    import { renderErrorNotificationMessage } from "@/extensions/utils";

    export default {
        name: "CarWashList",
        components: {
            LoadingContainer,
            ConfirmationDialog
        },
        data() {
            return {
                loadingIsActive: true,
                deleteDialogStatus: {
                    visible: false,
                    item: null
                },
                list: []
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
                    this.list = this.getCarWashList;
                }).catch(error => {
                    this.$notify.error({
                        title: "Failed to load company profile",
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
            this.loadCarWashList();
        },
        beforeRouteUpdate(to, from, next) {
            this.loadCarWashList();
            next();
        }
    };
</script>