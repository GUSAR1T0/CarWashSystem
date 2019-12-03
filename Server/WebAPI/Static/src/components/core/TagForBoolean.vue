<template>
    <el-tag :type="value ? 'success' : 'danger'" effect="dark" class="tag">
        <i v-if="value" class="el-icon-success"></i>
        <i v-else class="el-icon-error"></i>
    </el-tag>
</template>

<style>
    .tag {
        font-size: 20px;
        width: 100%;
        text-align: center;
    }
</style>

<script>
    export default {
        name: "TagForBoolean",
        props: {
            value: Boolean
        }
    };
</script>