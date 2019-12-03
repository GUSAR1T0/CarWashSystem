import { ADD_CAR_WASH_ENDPOINT, GET_CAR_WASH_ENDPOINT, UPDATE_CAR_WASH_ENDPOINT } from "@/constants/endpoints";
import {
    GET_CAR_WASH_REQUEST,
    UPDATE_CAR_WASH_REQUEST,
    ADD_CAR_WASH_REQUEST,
    GET_HTTP_REQUEST, RESET_CAR_WASH_REQUEST, PUT_HTTP_REQUEST, POST_HTTP_REQUEST
} from "@/constants/actions";
import format from "string-format";

export default {
    state: {
        id: null,
        name: null,
        email: null,
        phone: null,
        location: null,
        coordinateX: null,
        coordinateY: null,
        description: null,
        hasCafe: null,
        hasRestZone: null,
        hasParking: null,
        hasWC: null,
        hasCardPayment: null,
        workingHours: {
            monday: {
                startTime: null,
                stopTime: null,
                isChecked: false
            },
            tuesday: {
                startTime: null,
                stopTime: null,
                isChecked: false
            },
            wednesday: {
                startTime: null,
                stopTime: null,
                isChecked: false
            },
            thursday: {
                startTime: null,
                stopTime: null,
                isChecked: false
            },
            friday: {
                startTime: null,
                stopTime: null,
                isChecked: false
            },
            saturday: {
                startTime: null,
                stopTime: null,
                isChecked: false
            },
            sunday: {
                startTime: null,
                stopTime: null,
                isChecked: false
            }
        }
    },
    getters: {
        getCarWashForm: state => {
            return {
                id: state.id,
                name: state.name,
                email: state.email,
                phone: state.phone,
                location: state.location,
                coordinateX: state.coordinateX,
                coordinateY: state.coordinateY,
                description: state.description,
                hasCafe: state.hasCafe,
                hasRestZone: state.hasRestZone,
                hasParking: state.hasParking,
                hasWC: state.hasWC,
                hasCardPayment: state.hasCardPayment,
                workingHours: state.workingHours
            };
        }
    },
    mutations: {
        [GET_CAR_WASH_REQUEST]: (state, data) => {
            state.id = data.id;
            state.name = data.name;
            state.email = data.email;
            state.phone = data.phone;
            state.location = data.location;
            state.coordinateX = data.coordinateX;
            state.coordinateY = data.coordinateY;
            state.description = data.description;
            state.hasCafe = data.hasCafe;
            state.hasRestZone = data.hasRestZone;
            state.hasParking = data.hasParking;
            state.hasWC = data.hasWC;
            state.hasCardPayment = data.hasCardPayment;
            state.workingHours.monday.startTime = data.workingHours.monday.startTime;
            state.workingHours.monday.stopTime = data.workingHours.monday.stopTime;
            state.workingHours.monday.isChecked = !!data.workingHours.monday.startTime && !!data.workingHours.monday.stopTime;
            state.workingHours.tuesday.startTime = data.workingHours.tuesday.startTime;
            state.workingHours.tuesday.stopTime = data.workingHours.tuesday.stopTime;
            state.workingHours.tuesday.isChecked = !!data.workingHours.tuesday.startTime && !!data.workingHours.tuesday.stopTime;
            state.workingHours.wednesday.startTime = data.workingHours.wednesday.startTime;
            state.workingHours.wednesday.stopTime = data.workingHours.wednesday.stopTime;
            state.workingHours.wednesday.isChecked = !!data.workingHours.wednesday.startTime && !!data.workingHours.wednesday.stopTime;
            state.workingHours.thursday.startTime = data.workingHours.thursday.startTime;
            state.workingHours.thursday.stopTime = data.workingHours.thursday.stopTime;
            state.workingHours.thursday.isChecked = !!data.workingHours.thursday.startTime && !!data.workingHours.thursday.stopTime;
            state.workingHours.friday.startTime = data.workingHours.friday.startTime;
            state.workingHours.friday.stopTime = data.workingHours.friday.stopTime;
            state.workingHours.friday.isChecked = !!data.workingHours.friday.startTime && !!data.workingHours.friday.stopTime;
            state.workingHours.saturday.startTime = data.workingHours.saturday.startTime;
            state.workingHours.saturday.stopTime = data.workingHours.saturday.stopTime;
            state.workingHours.saturday.isChecked = !!data.workingHours.saturday.startTime && !!data.workingHours.saturday.stopTime;
            state.workingHours.sunday.startTime = data.workingHours.sunday.startTime;
            state.workingHours.sunday.stopTime = data.workingHours.sunday.stopTime;
            state.workingHours.sunday.isChecked = !!data.workingHours.sunday.startTime && !!data.workingHours.sunday.stopTime;
        },
        [RESET_CAR_WASH_REQUEST]: (state) => {
            state.id = null;
            state.name = null;
            state.email = null;
            state.phone = null;
            state.location = null;
            state.coordinateX = null;
            state.coordinateY = null;
            state.description = null;
            state.hasCafe = null;
            state.hasRestZone = null;
            state.hasParking = null;
            state.hasWC = null;
            state.hasCardPayment = null;
            state.workingHours.monday.startTime = null;
            state.workingHours.monday.stopTime = null;
            state.workingHours.monday.isChecked = false;
            state.workingHours.tuesday.startTime = null;
            state.workingHours.tuesday.stopTime = null;
            state.workingHours.tuesday.isChecked = false;
            state.workingHours.wednesday.startTime = null;
            state.workingHours.wednesday.stopTime = null;
            state.workingHours.wednesday.isChecked = false;
            state.workingHours.thursday.startTime = null;
            state.workingHours.thursday.stopTime = null;
            state.workingHours.thursday.isChecked = false;
            state.workingHours.friday.startTime = null;
            state.workingHours.friday.stopTime = null;
            state.workingHours.friday.isChecked = false;
            state.workingHours.saturday.startTime = null;
            state.workingHours.saturday.stopTime = null;
            state.workingHours.saturday.isChecked = false;
            state.workingHours.sunday.startTime = null;
            state.workingHours.sunday.stopTime = null;
            state.workingHours.sunday.isChecked = false;
        }
    },
    actions: {
        [GET_CAR_WASH_REQUEST]: ({commit, dispatch}, id) => {
            return new Promise((resolve, reject) => {
                dispatch(GET_HTTP_REQUEST, {
                    endpoint: format(GET_CAR_WASH_ENDPOINT, {id: id})
                }).then(response => {
                    commit(GET_CAR_WASH_REQUEST, response.data);
                    resolve();
                }).catch(error => reject(error));
            });
        },
        [ADD_CAR_WASH_REQUEST]: ({dispatch}, model) => {
            return new Promise((resolve, reject) => {
                model.id = undefined;
                model.coordinateX = parseFloat(model.coordinateX);
                model.coordinateY = parseFloat(model.coordinateY);
                dispatch(POST_HTTP_REQUEST, {
                    endpoint: ADD_CAR_WASH_ENDPOINT,
                    data: model
                }).then(response => resolve(response.data)).catch(error => reject(error));
            });
        },
        [UPDATE_CAR_WASH_REQUEST]: ({dispatch}, model) => {
            return new Promise((resolve, reject) => {
                model.coordinateX = parseFloat(model.coordinateX);
                model.coordinateY = parseFloat(model.coordinateY);
                dispatch(PUT_HTTP_REQUEST, {
                    endpoint: format(UPDATE_CAR_WASH_ENDPOINT, {id: model.id}),
                    data: model
                }).then(response => resolve(response.data)).catch(error => reject(error));
            });
        }
    }
};