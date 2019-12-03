import { GET_COMPANY_PROFILE_ENDPOINT, UPDATE_COMPANY_PROFILE_ENDPOINT } from "@/constants/endpoints";
import {
    GET_COMPANY_PROFILE_REQUEST,
    GET_HTTP_REQUEST,
    PUT_HTTP_REQUEST,
    UPDATE_COMPANY_PROFILE_REQUEST
} from "@/constants/actions";

export default {
    state: {
        name: null,
        email: null,
        phone: null
    },
    getters: {
        getCompanyProfileForm: state => {
            return {
                name: state.name,
                email: state.email,
                phone: state.phone
            };
        }
    },
    mutations: {
        [GET_COMPANY_PROFILE_REQUEST]: (state, data) => {
            state.name = data.name;
            state.email = data.email;
            state.phone = data.phone;
        }
    },
    actions: {
        [GET_COMPANY_PROFILE_REQUEST]: ({commit, dispatch}) => {
            return new Promise((resolve, reject) => {
                dispatch(GET_HTTP_REQUEST, {
                    endpoint: GET_COMPANY_PROFILE_ENDPOINT
                }).then(response => {
                    commit(GET_COMPANY_PROFILE_REQUEST, response.data);
                    resolve();
                }).catch(error => reject(error));
            });
        },
        [UPDATE_COMPANY_PROFILE_REQUEST]: ({dispatch}, model) => {
            return new Promise((resolve, reject) => {
                dispatch(PUT_HTTP_REQUEST, {
                    endpoint: UPDATE_COMPANY_PROFILE_ENDPOINT,
                    data: model
                }).then(() => resolve()).catch(error => reject(error));
            });
        }
    }
};