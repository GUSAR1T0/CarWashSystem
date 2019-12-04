<template>
    <div>
        <el-button class="functional-button" type="primary" icon="el-icon-plus"
                   @click="$router.push(`/profile/car-wash/${carWash.id}`)">
            Add New Service
        </el-button>
        <el-table :data="servicePrices" style="padding-top: 20px">
            <el-table-column width="auto">
                <template slot="header">
                    <div class="table-header">Operations</div>
                </template>
                <el-button type="primary" icon="el-icon-edit"/>
            </el-table-column>
            <el-table-column width="auto">
                <template slot="header">
                    <div class="table-header">Service Name</div>
                </template>
                <template slot-scope="scope">
                    {{ scope.row.serviceName }}
                </template>
            </el-table-column>
            <el-table-column width="auto">
                <template slot="header">
                    <div class="table-header">Description</div>
                </template>
                <template slot-scope="scope">
                    <div v-if="scope.row.description">
                        {{ scope.row.description }}
                    </div>
                    <div v-else>—</div>
                </template>
            </el-table-column>
            <el-table-column width="auto">
                <template slot="header">
                    <div class="table-header">Price</div>
                </template>
                <template slot-scope="scope">
                    {{ scope.row.price }}
                </template>
            </el-table-column>
            <el-table-column width="auto">
                <template slot="header">
                    <div class="table-header">Duration</div>
                </template>
                <template slot-scope="scope">
                    {{ scope.row.duration }}
                </template>
            </el-table-column>
            <el-table-column width="auto">
                <template slot="header">
                    <div class="table-header">Is Available?</div>
                </template>
                <template slot-scope="scope">
                    {{ scope.row.isAvailable }}
                </template>
            </el-table-column>
        </el-table>
    </div>
</template>

<style scoped src="@/styles/companyProfile.css">
</style>

<script>
    export default {
        name: "CarWashServicePrices",
        props: {
            servicePrices: Array
        }
    };
</script>