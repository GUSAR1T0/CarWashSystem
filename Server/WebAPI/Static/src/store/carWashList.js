import { GET_CAR_WASH_LIST_ENDPOINT, DELETE_CAR_WASH_ENDPOINT } from "@/constants/endpoints";
import {
    DELETE_CAR_WASH_REQUEST,
    DELETE_HTTP_REQUEST,
    GET_CAR_WASH_LIST_REQUEST,
    GET_HTTP_REQUEST
} from "@/constants/actions";
import format from "string-format";

export default {
    state: {
        list: []
    },
    getters: {
        getCarWashList: state => {
            return state.list;
        }
    },
    mutations: {
        [GET_CAR_WASH_LIST_REQUEST]: (state, data) => {
            state.list = data;
        }
    },
    actions: {
        [GET_CAR_WASH_LIST_REQUEST]: ({commit, dispatch}) => {
            return new Promise((resolve, reject) => {
                dispatch(GET_HTTP_REQUEST, {
                    endpoint: GET_CAR_WASH_LIST_ENDPOINT
                }).then(response => {
                    commit(GET_CAR_WASH_LIST_REQUEST, response.data);
                    resolve();
                }).catch(error => reject(error));
            });
        },
        [DELETE_CAR_WASH_REQUEST]: ({dispatch}, id) => {
            return new Promise((resolve, reject) => {
                dispatch(DELETE_HTTP_REQUEST, {
                    endpoint: format(DELETE_CAR_WASH_ENDPOINT, {id: id})
                }).then(response => {
                    dispatch(GET_CAR_WASH_LIST_REQUEST)
                        .then(() => resolve(response.data))
                        .catch(error => reject(error));
                }).catch(error => reject(error));
            });
        }
    }
};