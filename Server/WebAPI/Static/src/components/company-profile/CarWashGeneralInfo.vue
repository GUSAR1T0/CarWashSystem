<template>
    <div>
        <el-button class="functional-button" type="primary" icon="el-icon-edit"
                   @click="$router.push(`/profile/car-wash/${carWash.id}`)">
            Edit General Info
        </el-button>
        <CarWashInfoRow name="Description" style="padding: 20px 0">
            <template slot="value">
                <div v-if="carWash.description" class="description">{{ carWash.description }}</div>
                <div v-else>—</div>
            </template>
        </CarWashInfoRow>
        <el-divider/>
        <CarWashInfoRow name="Contacts" style="padding: 20px 0">
            <template slot="value">
                <el-table :data="[carWash]">
                    <el-table-column width="auto">
                        <template slot="header">
                            <div class="table-header">Email Address</div>
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
                            <div class="table-header">Phone</div>
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
                <div style="padding-top: 25px; width: 100%; text-align: center">{{ carWash.location }}
                </div>
            </template>
        </CarWashInfoRow>
        <el-divider/>
        <CarWashInfoRow name="Working Hours" style="padding: 20px 0">
            <template slot="value">
                <el-table :data="[carWash]">
                    <el-table-column width="auto" header-align="center">
                        <template slot="header">
                            <div class="table-header">Mo</div>
                        </template>
                        <template slot-scope="scope" style="white-space: pre-line;">
                            <div style="white-space: pre-line; text-align: center">
                                {{ getFormattedHours(scope.row.workingHours.monday) }}
                            </div>
                        </template>
                    </el-table-column>
                    <el-table-column width="auto" header-align="center">
                        <template slot="header">
                            <div class="table-header">Tu</div>
                        </template>
                        <template slot-scope="scope">
                            <div style="white-space: pre-line; text-align: center">
                                {{ getFormattedHours(scope.row.workingHours.tuesday) }}
                            </div>
                        </template>
                    </el-table-column>
                    <el-table-column width="auto" header-align="center">
                        <template slot="header">
                            <div class="table-header">We</div>
                        </template>
                        <template slot-scope="scope">
                            <div style="white-space: pre-line; text-align: center">
                                {{ getFormattedHours(scope.row.workingHours.wednesday) }}
                            </div>
                        </template>
                    </el-table-column>
                    <el-table-column width="auto" header-align="center">
                        <template slot="header">
                            <div class="table-header">Th</div>
                        </template>
                        <template slot-scope="scope">
                            <div style="white-space: pre-line; text-align: center">
                                {{ getFormattedHours(scope.row.workingHours.thursday) }}
                            </div>
                        </template>
                    </el-table-column>
                    <el-table-column width="auto" header-align="center">
                        <template slot="header">
                            <div class="table-header">Fr</div>
                        </template>
                        <template slot-scope="scope">
                            <div style="white-space: pre-line; text-align: center">
                                {{ getFormattedHours(scope.row.workingHours.friday) }}
                            </div>
                        </template>
                    </el-table-column>
                    <el-table-column width="auto" header-align="center">
                        <template slot="header">
                            <div class="table-header">Sa</div>
                        </template>
                        <template slot-scope="scope">
                            <div style="white-space: pre-line; text-align: center">
                                {{ getFormattedHours(scope.row.workingHours.saturday) }}
                            </div>
                        </template>
                    </el-table-column>
                    <el-table-column width="auto" header-align="center">
                        <template slot="header">
                            <div class="table-header">Su</div>
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
                    <div class="table-header">Cafe</div>
                </template>
                <template slot-scope="scope">
                    <TagForBoolean :value="scope.row.hasCafe"/>
                </template>
            </el-table-column>
            <el-table-column width="auto">
                <template slot="header">
                    <div class="table-header">Rest Zone</div>
                </template>
                <template slot-scope="scope">
                    <TagForBoolean :value="scope.row.hasRestZone"/>
                </template>
            </el-table-column>
            <el-table-column width="auto">
                <template slot="header">
                    <div class="table-header">Parking</div>
                </template>
                <template slot-scope="scope">
                    <TagForBoolean :value="scope.row.hasParking"/>
                </template>
            </el-table-column>
            <el-table-column width="auto">
                <template slot="header">
                    <div class="table-header">WC</div>
                </template>
                <template slot-scope="scope">
                    <TagForBoolean :value="scope.row.hasWC"/>
                </template>
            </el-table-column>
            <el-table-column width="auto">
                <template slot="header">
                    <div class="table-header">Card Payment</div>
                </template>
                <template slot-scope="scope">
                    <TagForBoolean :value="scope.row.hasCardPayment"/>
                </template>
            </el-table-column>
        </el-table>
    </div>
</template>

<style scoped src="@/styles/companyProfile.css">
</style>

<style scoped>
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
        white-space: pre-wrap;
    }
</style>

<script>
    import CarWashInfoRow from "@/components/company-profile/CarWashInfoRow";
    import TagForBoolean from "@/components/core/TagForBoolean";
    import { yandexMap, ymapMarker } from "vue-yandex-maps";
    import { mapGetters } from "vuex";

    export default {
        name: "CarWashGeneralInfo",
        props: {
            carWash: Object
        },
        components: {
            CarWashInfoRow,
            TagForBoolean,
            yandexMap,
            ymapMarker
        },
        computed: {
            ...mapGetters([
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
            getFormattedHours(hours) {
                return hours.startTime && hours.stopTime ? hours.startTime + "\n" + hours.stopTime : "—";
            },
            getCoordinates(carWash) {
                return [ carWash.coordinateX, carWash.coordinateY ];
            },
        }
    };
</script>