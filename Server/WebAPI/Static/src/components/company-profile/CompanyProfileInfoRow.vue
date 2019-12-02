<template>
    <div>
        <el-row type="flex" justify="center" align="middle" class="info-row">
            <el-col :span="10" style="text-align: left;">
                <b class="info-row-name">{{ name }}</b>
            </el-col>
            <el-col :span="14" style="text-align: left;">
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
        padding: 20px 0;
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