<template>
    <div>
        <el-row type="flex" justify="center" align="middle" class="info-row">
            <el-col style="text-align: left;">
                <b class="info-row-name">{{ name }}</b>
            </el-col>
        </el-row>
        <el-row type="flex" justify="center" align="middle">
            <el-col style="text-align: left;">
                <div v-if="value" class="info-row-value">
                    {{ value }}
                </div>
                <div v-else-if="!!$slots['value']" class="info-row-value">
                    <slot name="value"></slot>
                </div>
                <div v-else class="info-row-value">
                    —
                </div>
            </el-col>
        </el-row>
    </div>
</template>

<style>
    .info-row {
        padding-bottom: 20px;
    }

    .info-row-name {
        font-size: 24px;
    }

    .info-row-value {
        font-size: 28px;
    }
</style>

<script>
    export default {
        name: "CompanyProfileInfoRow",
        props: {
            name: String,
            value: String
        }
    };
</script>