<template>
    <el-dialog :visible.sync="dialogStatus.visible" width="75%" style="text-align: center" @closed="closed">
        <span slot="title" class="modal-title">Confirmation</span>
        <h1 v-if="dialogStatus.visible" style="font-size: 32px">{{ confirmationText() }}</h1>
        <h3 v-if="dialogStatus.visible && additionalText" class="confirmation-dialog-header">{{ additionalText() }}</h3>
        <el-row type="flex" justify="center" align="middle" :gutter="20">
            <el-col :span="12">
                <el-button type="primary" plain @click="cancelClickAction" class="confirmation-button">
                    <b style="font-size: 20px">Cancel</b>
                </el-button>
            </el-col>
            <el-col :span="12">
                <el-button type="danger" plain ref="dialogButton" @click="submitClickAction($refs.dialogButton)"
                           class="confirmation-button">
                    <b style="font-size: 20px">Submit</b>
                </el-button>
            </el-col>
        </el-row>
    </el-dialog>
</template>

<style scoped>
    .confirmation-dialog-header {
        text-align: center;
        margin-bottom: 55px;
    }

    .confirmation-button {
        width: 100%;
    }
</style>

<script>
    export default {
        name: "ConfirmationDialog",
        props: {
            dialogStatus: Object,
            confirmationText: Function,
            additionalText: Function,
            cancelClickAction: Function,
            submitClickAction: Function,
            closed: Function
        }
    };
</script>