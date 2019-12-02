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
                            <el-button class="functional-button" type="primary" icon="el-icon-edit"
                                       @click="$router.push(`/profile/car-wash/${carWash.id}`)"/>
                            <el-button class="functional-button" type="primary" icon="el-icon-circle-close"
                                       @click="openDeleteDialog(carWash)"/>
                        </div>
                    </div>
                </div>
                <CarWashInfoRow name="Description" style="padding-bottom: 20px">
                    <template slot="value">
                        <div v-if="carWash.description" class="description">
                            {{ carWash.description }}
                        </div>
                        <div v-else>—</div>
                    </template>
                </CarWashInfoRow>
                <el-divider/>
                <CarWashInfoRow name="Contacts" style="padding: 20px 0">
                    <template slot="value">
                        <el-table :data="[carWash]">
                            <el-table-column width="auto">
                                <template slot="header">
                                    <div class="advantage-header">Email Address</div>
                                </template>
                                <template slot-scope="scope">
                                    <a v-if="scope.row.email" :href="`mailto:${scope.row.email}`">
                                        <el-button class="functional-button contacts-button">
                                            {{ scope.row.email }}
                                        </el-button>
                                    </a>
                                    <div v-else>—</div>
                                </template>
                            </el-table-column>
                            <el-table-column width="auto">
                                <template slot="header">
                                    <div class="advantage-header">Phone</div>
                                </template>
                                <template slot-scope="scope">
                                    <a v-if="scope.row.phone" :href="`tel:${scope.row.phone}`">
                                        <el-button class="functional-button contacts-button">
                                            {{ scope.row.phone }}
                                        </el-button>
                                    </a>
                                    <div v-else>—</div>
                                </template>
                            </el-table-column>
                        </el-table>
                    </template>
                </CarWashInfoRow>
                <el-divider/>
                <CarWashInfoRow name="Location" style="padding: 20px 0">
                    <template slot="value">
                        <yandex-map :settings="getYandexMapsSettings" :coords="getCoordinates(carWash)"
                                    zoom="15" class="map">
                            <ymap-marker :marker-id="carWash.id" :coords="getCoordinates(carWash)"/>
                        </yandex-map>
                        <div style="padding-top: 25px; width: 100%; text-align: center">{{ carWash.location }}</div>
                    </template>
                </CarWashInfoRow>
                <el-divider/>
                <CarWashInfoRow name="Working Hours" style="padding: 20px 0">
                    <template slot="value">
                        <el-table :data="[carWash]">
                            <el-table-column width="auto" header-align="center">
                                <template slot="header">
                                    <div class="advantage-header">Mo</div>
                                </template>
                                <template slot-scope="scope" style="white-space: pre-line;">
                                    <div style="white-space: pre-line; text-align: center">
                                        {{ getFormattedHours(scope.row.workingHours.monday) }}
                                    </div>
                                </template>
                            </el-table-column>
                            <el-table-column width="auto" header-align="center">
                                <template slot="header">
                                    <div class="advantage-header">Tu</div>
                                </template>
                                <template slot-scope="scope">
                                    <div style="white-space: pre-line; text-align: center">
                                        {{ getFormattedHours(scope.row.workingHours.tuesday) }}
                                    </div>
                                </template>
                            </el-table-column>
                            <el-table-column width="auto" header-align="center">
                                <template slot="header">
                                    <div class="advantage-header">We</div>
                                </template>
                                <template slot-scope="scope">
                                    <div style="white-space: pre-line; text-align: center">
                                        {{ getFormattedHours(scope.row.workingHours.wednesday) }}
                                    </div>
                                </template>
                            </el-table-column>
                            <el-table-column width="auto" header-align="center">
                                <template slot="header">
                                    <div class="advantage-header">Th</div>
                                </template>
                                <template slot-scope="scope">
                                    <div style="white-space: pre-line; text-align: center">
                                        {{ getFormattedHours(scope.row.workingHours.thursday) }}
                                    </div>
                                </template>
                            </el-table-column>
                            <el-table-column width="auto" header-align="center">
                                <template slot="header">
                                    <div class="advantage-header">Fr</div>
                                </template>
                                <template slot-scope="scope">
                                    <div style="white-space: pre-line; text-align: center">
                                        {{ getFormattedHours(scope.row.workingHours.friday) }}
                                    </div>
                                </template>
                            </el-table-column>
                            <el-table-column width="auto" header-align="center">
                                <template slot="header">
                                    <div class="advantage-header">Sa</div>
                                </template>
                                <template slot-scope="scope">
                                    <div style="white-space: pre-line; text-align: center">
                                        {{ getFormattedHours(scope.row.workingHours.saturday) }}
                                    </div>
                                </template>
                            </el-table-column>
                            <el-table-column width="auto" header-align="center">
                                <template slot="header">
                                    <div class="advantage-header">Su</div>
                                </template>
                                <template slot-scope="scope">
                                    <div style="white-space: pre-line; text-align: center">
                                        {{ getFormattedHours(scope.row.workingHours.sunday) }}
                                    </div>
                                </template>
                            </el-table-column>
                        </el-table>
                    </template>
                </CarWashInfoRow>
                <el-divider/>
                <el-table :data="[carWash]" style="padding-top: 20px">
                    <el-table-column width="auto">
                        <template slot="header">
                            <div class="advantage-header">Cafe</div>
                        </template>
                        <template slot-scope="scope">
                            <TagForBoolean :value="scope.row.hasCafe"/>
                        </template>
                    </el-table-column>
                    <el-table-column width="auto">
                        <template slot="header">
                            <div class="advantage-header">Rest Zone</div>
                        </template>
                        <template slot-scope="scope">
                            <TagForBoolean :value="scope.row.hasRestZone"/>
                        </template>
                    </el-table-column>
                    <el-table-column width="auto">
                        <template slot="header">
                            <div class="advantage-header">Parking</div>
                        </template>
                        <template slot-scope="scope">
                            <TagForBoolean :value="scope.row.hasParking"/>
                        </template>
                    </el-table-column>
                    <el-table-column width="auto">
                        <template slot="header">
                            <div class="advantage-header">WC</div>
                        </template>
                        <template slot-scope="scope">
                            <TagForBoolean :value="scope.row.hasWC"/>
                        </template>
                    </el-table-column>
                    <el-table-column width="auto">
                        <template slot="header">
                            <div class="advantage-header">Card Payment</div>
                        </template>
                        <template slot-scope="scope">
                            <TagForBoolean :value="scope.row.hasCardPayment"/>
                        </template>
                    </el-table-column>
                </el-table>
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

<style scoped>
    .advantage-header {
        text-transform: uppercase;
    }

    .map {
        width: auto;
        height: 300px;
    }

    .description {
        border-left: 3px solid #631D76;
        line-height: 1.8em;
        margin: 0 1em;
        padding: 0 1em;
        position: relative;
    }
</style>

<script>
    import LoadingContainer from "@/components/core/LoadingContainer";
    import ConfirmationDialog from "@/components/core/ConfirmationDialog";
    import CarWashInfoRow from "@/components/company-profile/CarWashInfoRow";
    import TagForBoolean from "@/components/core/TagForBoolean";
    import { mapGetters } from "vuex";
    import { DELETE_CAR_WASH_REQUEST, GET_CAR_WASH_LIST_REQUEST } from "@/constants/actions";
    import { renderErrorNotificationMessage } from "@/extensions/utils";
    import { yandexMap, ymapMarker } from "vue-yandex-maps";

    export default {
        name: "CarWashList",
        components: {
            LoadingContainer,
            ConfirmationDialog,
            CarWashInfoRow,
            TagForBoolean,
            yandexMap,
            ymapMarker
        },
        data() {
            return {
                loadingIsActive: true,
                deleteDialogStatus: {
                    visible: false,
                    item: null
                }
            };
        },
        computed: {
            ...mapGetters([
                "getCarWashList",
                "getYandexMapsApiKey"
            ]),
            getYandexMapsSettings() {
                return {
                    apiKey: this.getYandexMapsApiKey,
                    lang: "en_US",
                    coordorder: "latlong",
                    version: "2.1"
                };
            }
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
            getFormattedHours(hours) {
                return hours.startTime && hours.stopTime ? hours.startTime + "\n" + hours.stopTime : "—";
            },
            getCoordinates(carWash) {
                return [ carWash.coordinateX, carWash.coordinateY ];
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
            this.loadCarWashList();
        },
        beforeRouteUpdate(to, from, next) {
            this.loadCarWashList();
            next();
        }
    };
</script>