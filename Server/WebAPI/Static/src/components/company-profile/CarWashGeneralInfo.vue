<template>
    <div style="padding-top: 20px">
        <el-button class="functional-button" type="primary" icon="el-icon-edit"
                   @click="$router.push(`/profile/car-wash/edit/${carWash.id}`)">
            Edit General Info
        </el-button>
        <CompanyProfileInfoRow name="Description" style="padding: 20px 0">
            <template slot="value">
                <div v-if="carWash.description" class="description">{{ carWash.description }}</div>
                <div v-else>—</div>
            </template>
        </CompanyProfileInfoRow>
        <el-divider/>
        <CompanyProfileInfoRow name="Contacts" style="padding: 20px 0">
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
                            <div v-else class="table-cell">—</div>
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
                            <div v-else class="table-cell">—</div>
                        </template>
                    </el-table-column>
                </el-table>
            </template>
        </CompanyProfileInfoRow>
        <el-divider/>
        <CompanyProfileInfoRow name="Location" style="padding: 20px 0">
            <template slot="value">
                <div style="width: 100%; text-align: center">{{ carWash.location }}
                </div>
                <div class="map-load-switch">
                    <el-switch v-model="loadMap" active-text="Turn on map" inactive-text="Turn off map"/>
                </div>
                <div v-if="!!loadMap && getCoordinates(carWash)" style="padding-top: 20px;">
                    <yandex-map :settings="getYandexMapsSettings" :coords="getCoordinates(carWash)"
                                zoom="15" class="map">
                        <ymap-marker :marker-id="carWash.id" :coords="getCoordinates(carWash)"/>
                    </yandex-map>
                </div>
            </template>
        </CompanyProfileInfoRow>
        <el-divider/>
        <CompanyProfileInfoRow name="Working Hours" style="padding: 20px 0">
            <template slot="value">
                <el-table :data="[carWash]">
                    <el-table-column width="auto" header-align="center" align="center">
                        <template slot="header">
                            <div class="table-header">Mo</div>
                        </template>
                        <template slot-scope="scope">
                            <div class="table-cell">
                                {{ getFormattedHours(scope.row.workingHours.monday) }}
                            </div>
                        </template>
                    </el-table-column>
                    <el-table-column width="auto" header-align="center" align="center">
                        <template slot="header">
                            <div class="table-header">Tu</div>
                        </template>
                        <template slot-scope="scope">
                            <div class="table-cell">
                                {{ getFormattedHours(scope.row.workingHours.tuesday) }}
                            </div>
                        </template>
                    </el-table-column>
                    <el-table-column width="auto" header-align="center" align="center">
                        <template slot="header">
                            <div class="table-header">We</div>
                        </template>
                        <template slot-scope="scope">
                            <div class="table-cell">
                                {{ getFormattedHours(scope.row.workingHours.wednesday) }}
                            </div>
                        </template>
                    </el-table-column>
                    <el-table-column width="auto" header-align="center" align="center">
                        <template slot="header">
                            <div class="table-header">Th</div>
                        </template>
                        <template slot-scope="scope">
                            <div class="table-cell">
                                {{ getFormattedHours(scope.row.workingHours.thursday) }}
                            </div>
                        </template>
                    </el-table-column>
                    <el-table-column width="auto" header-align="center" align="center">
                        <template slot="header">
                            <div class="table-header">Fr</div>
                        </template>
                        <template slot-scope="scope">
                            <div class="table-cell">
                                {{ getFormattedHours(scope.row.workingHours.friday) }}
                            </div>
                        </template>
                    </el-table-column>
                    <el-table-column width="auto" header-align="center" align="center">
                        <template slot="header">
                            <div class="table-header">Sa</div>
                        </template>
                        <template slot-scope="scope">
                            <div class="table-cell">
                                {{ getFormattedHours(scope.row.workingHours.saturday) }}
                            </div>
                        </template>
                    </el-table-column>
                    <el-table-column width="auto" header-align="center" align="center">
                        <template slot="header">
                            <div class="table-header">Su</div>
                        </template>
                        <template slot-scope="scope">
                            <div class="table-cell">
                                {{ getFormattedHours(scope.row.workingHours.sunday) }}
                            </div>
                        </template>
                    </el-table-column>
                </el-table>
            </template>
        </CompanyProfileInfoRow>
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

    .map-load-switch {
        width: 100%;
        text-align: center;
        padding-top: 10px;
    }
</style>

<script>
    import CompanyProfileInfoRow from "@/components/company-profile/CompanyProfileInfoRow";
    import TagForBoolean from "@/components/core/TagForBoolean";
    import { yandexMap, ymapMarker } from "vue-yandex-maps";
    import { mapGetters } from "vuex";

    export default {
        name: "CarWashGeneralInfo",
        props: {
            carWash: Object
        },
        components: {
            CompanyProfileInfoRow,
            TagForBoolean,
            yandexMap,
            ymapMarker
        },
        data() {
            return {
                loadMap: false
            };
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
                if (!!carWash.coordinateX && !!carWash.coordinateY) {
                    return [ carWash.coordinateX, carWash.coordinateY ];
                }
                return undefined;
            },
        }
    };
</script>