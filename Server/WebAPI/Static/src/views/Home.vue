<template>
    <div class="home">
        <el-tabs :before-leave="load">
            <el-tab-pane label="Home" name="home">
                <h1>HOME</h1>
            </el-tab-pane>
            <el-tab-pane label="Schedule" name="schedule">
                <CarWashSchedule :events="events"/>
            </el-tab-pane>
        </el-tabs>
    </div>
</template>

<script>
    import CarWashSchedule from "@/components/company-profile/CarWashSchedule";

    export default {
        name: "Home",
        components: {CarWashSchedule},
        data() {
            return {
                events: [],
                i: 0
            };
        },
        methods: {
            load(active) {
                if (active === "schedule") {
                    this.events.push({
                        title: "event " + this.i,
                        start: `2019-12-07T1${this.i}:00:00`,
                        end: `2019-12-07T1${this.i + 1}:00:00`,
                        allDay: false
                    });
                    this.i++;
                }
            }
        }
    };
</script>
