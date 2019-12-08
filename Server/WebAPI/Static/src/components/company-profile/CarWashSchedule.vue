<template>
    <FullCalendar :plugins="calendarPlugins" :event-sources="x" :default-view="defaultView"
                  :timeFormat="timeCalendarFormat" :slot-label-format="timeViewFormat"
                  :event-time-format="timeViewFormat" :first-day="firstDay" :slot-duration="slotDuration"
                  :all-day-slot="allDaySlot" :selectable="selectable"
                  ref="calendar"
    />
</template>

<style scoped lang='scss'>
    @import '~@fullcalendar/core/main.css';
    @import '~@fullcalendar/daygrid/main.css';
    @import '~@fullcalendar/timegrid/main.css';
</style>

<script>
    import FullCalendar from "@fullcalendar/vue";
    import timeGridPlugin from "@fullcalendar/timegrid";

    export default {
        name: "CarWashSchedule",
        props: {
            events: Array
        },
        components: {
            FullCalendar
        },
        data() {
            return {
                calendarPlugins: [ timeGridPlugin ],
                defaultView: "timeGridWeek",
                firstDay: 1, // Monday
                timeCalendarFormat: "H(:mm)", // 24-hour clock
                timeViewFormat: {hour: "numeric", minute: "2-digit", hour12: false}, // 24-hour clock
                slotDuration: "00:15:00",
                allDaySlot: false,
                selectable: true,
            };
        },
        computed: {
            x() {
                return [ {
                    events: this.events,
                    color: "#631D76",
                    textColor: "white"
                } ];
            }
        }
    };
</script>