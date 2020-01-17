<template>
    <el-table :data="appointments">
        <el-table-column width="200px" header-align="center" align="center">
            <template slot="header">
                <div class="table-header">Operations</div>
            </template>
            <template slot-scope="scope">
                <el-button class="functional-button" type="primary" icon="el-icon-right"
                           @click="$router.push(`/profile/car-wash/${carWashId}/appointment/${scope.row.id}`)"/>
            </template>
        </el-table-column>
        <el-table-column width="auto" header-align="center" align="center">
            <template slot="header">
                <div class="table-header">Client Info</div>
            </template>
            <template slot-scope="scope">
                <b style="font-size: 20px">
                    {{ scope.row.fullName }}
                </b>
                <div style="font-size: 18px">
                    {{ getCarBrandModelName(scope.row.carModelId) }}
                </div>
                <div style="font-size: 18px">
                    {{ scope.row.carGovernmentPlate }}
                </div>
            </template>
        </el-table-column>
        <el-table-column width="auto" header-align="center" align="center">
            <template slot="header">
                <div class="table-header">Start Time</div>
            </template>
            <el-table-column width="auto" header-align="center" align="center">
                <template slot="header">
                    <div class="table-header">Requested</div>
                </template>
                <template slot-scope="scope">
                    <div style="font-size: 20px">{{ scope.row.requestedStartDate }}</div>
                    <div style="font-size: 20px">{{ scope.row.requestedStartTime }}</div>
                </template>
            </el-table-column>
            <el-table-column width="auto" header-align="center" align="center">
                <template slot="header">
                    <div class="table-header">Approved</div>
                </template>
                <template slot-scope="scope">
                    <div v-if="scope.row.approvedStartDate && scope.row.approvedStartTime">
                        <div style="font-size: 20px">{{ scope.row.approvedStartDate }}</div>
                        <div style="font-size: 20px">{{ scope.row.approvedStartTime }}</div>
                    </div>
                    <div v-else>—</div>
                </template>
            </el-table-column>
        </el-table-column>
        <el-table-column width="auto" header-align="center" align="center">
            <template slot="header">
                <div class="table-header">Status</div>
            </template>
            <template slot-scope="scope">
                <div style="font-size: 22px">{{ getStatusName(scope.row.status) }}</div>
            </template>
        </el-table-column>
    </el-table>
</template>

<style scoped src="@/styles/companyProfile.css">
</style>

<script>
    import { mapGetters } from "vuex";
    import { getAppointmentStatusModels, getCarBrandModelName } from "@/extensions/utils";

    export default {
        name: "CarWashAppointments",
        props: {
            carWashId: Number,
            appointments: Array
        },
        computed: {
            ...mapGetters([
                "getCarBrandModelsModels",
                "getAppointmentStatusModels"
            ])
        },
        methods: {
            getCarBrandModelName(carModelId) {
                return getCarBrandModelName(this.getCarBrandModelsModels, carModelId);
            },
            getStatusName(statusId) {
                return getAppointmentStatusModels(this.getAppointmentStatusModels, statusId);
            }
        }
    };
</script>